using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.SessionState;

/// <summary>
/// Descripción breve de Tools
/// </summary>
public class Tools
{
    public string sResultado { get; private set; }
    public float fProgreso { get; set; }
    public string sFileName { get; set; }
    public bool fCompletado { get; set; }

    #region Datatable factura
    public DataTable dtbFactura { get; set; }
    public DataTable dtbServiciosFactura { get; set; }
    #endregion

    public List<string> lstErrores { get; set; }
    public string sHTMLError { get; set; }
    public int iResultado { get; set; }
    public string sMensaje { get; set; }
    public DataTable dtbResultado { get; set; }

    public Tools()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public void fn_GeneraSelectTablas(Tools objTools)
    {
        Utilerias objUtilerias = new Utilerias();
        objUtilerias.sQuery = @"SELECT ROW_NUMBER() OVER(ORDER BY tbls.name), tbls.name
                                FROM sys.tables tbls";
        objUtilerias.sNombre = "hsclTablas";
        objUtilerias.fn_GeneraCombobox(objUtilerias);
        objTools.sResultado = objUtilerias.sContenido;
    }

    private DataTable fn_ObtenerDtbExcel(Tools objTools, HttpContext oContext)
    {
        string sRuta = oContext.Server.MapPath("../../Documentos/Tools/" + objTools.sFileName.Split('.')[0] + "/" + objTools.sFileName);
        ExcelData objExcelData = new ExcelData(sRuta);
        return objExcelData.getData(true).CopyToDataTable();
    }

    private void fn_AumentarProgreso(HttpSessionState objSesion, float fProgresoAumentar)
    {
        float fProgreso = (float)objSesion["fProgresoCarga"];
        fProgreso += fProgresoAumentar;
        if (fProgreso >= 80)
            objSesion.Add("lstErrores", lstErrores);
        objSesion["fProgresoCarga"] = fProgreso;
    }

    public void fn_ProcesaArchivo(object arrParametros)
    {
        // Obtiene la informacion a utilizar, proveniente de base de datos y del excel cargado
        DataTable dtbExcel = fn_ObtenerDtbExcel(this, (HttpContext)((object[])arrParametros)[1]);
        // Quitar duplicados y vacios
        dtbExcel = (from row in dtbExcel.AsEnumerable()
                    where row.Field<object>(3) != null && !String.IsNullOrEmpty(row.Field<object>(3).ToString().Trim())
                    select row).CopyToDataTable();
        // Aumentar progreso en 10%
        fn_AumentarProgreso((HttpSessionState)((object[])arrParametros)[0], 10);
        // Obtener campos requeridos por SP facturas
        this.fn_ObtenerCamposFactura(dtbExcel);
        // Aumentar progreso en 10%
        fn_AumentarProgreso((HttpSessionState)((object[])arrParametros)[0], 10);
        // Obtener campos requeridos por SP Servicio facturas
        this.fn_ObtenerServiciosFactura(dtbExcel);
        // Aumentar progreso en 10%
        fn_AumentarProgreso((HttpSessionState)((object[])arrParametros)[0], 10);
        // Insertar las facturas
        this.fn_InsertarFacturas((HttpSessionState)((object[])arrParametros)[0]);
    }

    private void fn_InsertarFacturas(HttpSessionState objSesion)
    {
        lstErrores = new List<string>();
        // Calcular porcentaje que cada factura va a aumentar
        float fPorcentaje = 50.0f / this.dtbFactura.Rows.Count;
        foreach (DataRow objRow in this.dtbFactura.Rows)
        {
            // Insertar la factura
            string sInsertada = this.fn_InsertarFactura(objRow);
            while (sInsertada.Contains("pa_GuardarFacturaDesdeLQUI"))
            {
                sInsertada = this.fn_InsertarFactura(objRow);
                Thread.Sleep(5000);
            }
            // Si se inserto la factura inserta los servicios
            if (sInsertada == "1")
            {
                var oRes = from row in dtbServiciosFactura.AsEnumerable()
                           where row.Field<object>(0).ToString() == objRow[7].ToString()
                           select row;
                foreach (DataRow objRowServicio in oRes)
                {
                    sInsertada = this.fn_InsertarServiciosFactura(objRowServicio);
                    while (sInsertada.Contains("pa_GuardarServicioFacturaDesdeLQUI"))
                    {
                        sInsertada = this.fn_InsertarServiciosFactura(objRowServicio);
                        Thread.Sleep(5000);
                    }
                }
            }
            if (sInsertada != "1" && sInsertada != "2")
            {
                // Si hubo error se agrega al log
                lstErrores.Add("Error factura: " + objRow["sNoFactura"].ToString() + " " + sInsertada);
            }
            fn_AumentarProgreso(objSesion, fPorcentaje);
        }
        objSesion["lstErrores"] = lstErrores;
        objSesion["fCompletado"] = true;
    }

    private string fn_InsertarServiciosFactura(DataRow objRowServicioTarifa)
    {
        //Instanciamos clase de conexión
        Conexion oConexion = new Conexion();
        //Generamos el SP
        string sRes = oConexion.generarSP("pa_GuardarServicioFacturaDesdeLQUI", 0);
        //Creamos arreglo para guardar resultado del store
        string[] sResOut;
        //Si el procedimiento se generó correctamente
        if (sRes == "1")
        {
            //Ejecutamos el procedimiento
            oConexion.agregarParametroSP("@sNoFactura", SqlDbType.VarChar, objRowServicioTarifa[0].ToString());
            oConexion.agregarParametroSP("@sCveServicio", SqlDbType.VarChar, objRowServicioTarifa[1].ToString());
            oConexion.agregarParametroSP("@fIVA", SqlDbType.Float, objRowServicioTarifa[2].ToString());
            oConexion.agregarParametroSP("@fRetencion", SqlDbType.Float, objRowServicioTarifa[3].ToString());
            oConexion.agregarParametroSP("@fSubtotal", SqlDbType.Float, objRowServicioTarifa[4].ToString());
            oConexion.agregarParametroSP("@fTotal", SqlDbType.Float, objRowServicioTarifa[5].ToString());
            oConexion.agregarParametroSP("@fDescuento", SqlDbType.Float, objRowServicioTarifa[6].ToString());
            oConexion.agregarParametroSP("@fPorcentajeIva", SqlDbType.Float, objRowServicioTarifa[7].ToString());
            oConexion.agregarParametroSP("@sSerie", SqlDbType.VarChar, objRowServicioTarifa[8].ToString());
            sResOut = oConexion.ejecutarProcOUTPUT_INT("@iResOut");




            return sResOut[0].ToString();
        }
        else
        {
            return "Error al generar el procedimiento pa_GuardarServicioFacturaDesdeLQUI no existe";
        }

    }

    private string fn_InsertarFactura(DataRow objRowTarifa)
    {
        //Instanciamos clase de conexión
        Conexion oConexion = new Conexion();
        //Generamos el SP
        string sRes = oConexion.generarSP("pa_GuardarFacturaDesdeLQUI", 0);
        //Creamos arreglo para guardar resultado del store
        string[] sResOut;
        //Si el procedimiento se generó correctamente
        if (sRes == "1")
        {
            // Se obtiene objeto datetime
            DateTime objTime;
            try
            {
                objTime = Convert.ToDateTime(objRowTarifa[0].ToString());
            }
            catch (Exception e)
            {
                // Ajustar fecha
                string sFecha = objRowTarifa[0].ToString().Substring(0, 10);
                try
                {
                    objTime = DateTime.ParseExact(sFecha, "dd-MM-yyyy",
                                    CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    try
                    {
                        objTime = DateTime.ParseExact(sFecha, "yyyy-dd-MM",
                                    CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        objTime = DateTime.ParseExact(sFecha, "yyyy-MM-dd",
                                CultureInfo.InvariantCulture);
                    }
                }
            }
            //Ejecutamos el procedimiento
            oConexion.agregarParametroSP("@dFechaFactura", SqlDbType.VarChar, objTime.ToString("o").Substring(0, 19));
            oConexion.agregarParametroSP("@fMonto", SqlDbType.Float, objRowTarifa[1].ToString());
            oConexion.agregarParametroSP("@sPais", SqlDbType.VarChar, objRowTarifa[2].ToString());
            oConexion.agregarParametroSP("@sRFCProveedor", SqlDbType.VarChar, objRowTarifa[3].ToString());
            oConexion.agregarParametroSP("@sRefAdministrativa", SqlDbType.VarChar, objRowTarifa[4].ToString());
            oConexion.agregarParametroSP("@sUUID", SqlDbType.VarChar, objRowTarifa[5].ToString());
            oConexion.agregarParametroSP("@sMoneda", SqlDbType.VarChar, objRowTarifa[6].ToString());
            oConexion.agregarParametroSP("@sNoFactura", SqlDbType.VarChar, objRowTarifa[7].ToString());
            oConexion.agregarParametroSP("@sSerie", SqlDbType.VarChar, objRowTarifa[8].ToString());
            oConexion.agregarParametroSP("@sRFCCliente", SqlDbType.VarChar, objRowTarifa[9].ToString());
            oConexion.agregarParametroSP("@fIva", SqlDbType.Float, objRowTarifa[10].ToString());
            oConexion.agregarParametroSP("@fRetencion", SqlDbType.Float, objRowTarifa[11].ToString());
            oConexion.agregarParametroSP("@fSubtotal", SqlDbType.Float, objRowTarifa[12].ToString());
            oConexion.agregarParametroSP("@fDescuento", SqlDbType.Float, objRowTarifa[13].ToString());
            oConexion.agregarParametroSP("@sFormaPago", SqlDbType.VarChar, objRowTarifa[14].ToString());
            oConexion.agregarParametroSP("@sMetodoPago", SqlDbType.VarChar, objRowTarifa[15].ToString());
            oConexion.agregarParametroSP("@sRefOperativa", SqlDbType.VarChar, objRowTarifa[16].ToString());
            sResOut = oConexion.ejecutarProcOUTPUT_INT("@iResOut");
            if (sResOut[1] == "1")
            {
                return sResOut[1].ToString();
            }
            else
            {
                if (sResOut[1] == "2")
                {
                    return sResOut[1].ToString();
                }
                else
                {

                    return sResOut[0].ToString();
                }
            }
        }
        return "Error al generar el procedimiento pa_GuardarFacturaDesdeLQUI";
    }

    private void fn_ObtenerCamposFactura(DataTable dtbExcel)
    {
        this.dtbFactura = new DataTable();
        dtbFactura.Columns.AddRange(
            new DataColumn[] {
                new DataColumn("dFechaFactura"),
                new DataColumn("fMonto"),
                new DataColumn("sPais"),
                new DataColumn("sRFCProveedor"),
                new DataColumn("sRefAdministrativa"),
                new DataColumn("sUUID"),
                new DataColumn("sMoneda"),
                new DataColumn("sNoFactura"),
                new DataColumn("sSerie"),
                new DataColumn("sRFCCliente"),
                new DataColumn("fIva"),
                new DataColumn("fRetencion"),
                new DataColumn("fSubtotal"),
                new DataColumn("fDescuento"),
                new DataColumn("sFormaPago"),
                new DataColumn("sMetodoPago"),
                new DataColumn("sRefOperativa")
            }
        );
        var res = from row in dtbExcel.AsEnumerable()
                  let array = new object[]
                  {
                            row.Field<object>(8),
                            row.Field<object>(14),
                            row.Field<object>(18),
                            row.Field<object>(5),
                            row.Field<object>(0),
                            row.Field<object>(7),
                            row.Field<object>(9),
                            row.Field<object>(3),
                            row.Field<object>(4),
                            row.Field<object>(6),
                            row.Field<object>(11),
                            row.Field<object>(12),
                            row.Field<object>(10),
                            row.Field<object>(13),
                            row.Field<object>(15),
                            row.Field<object>(17),
                            row.Field<object>(2)
                  }
                  select array;

        foreach (object[] item in res)
        {
            string[] arrRes = new string[17];
            if (item[3] == null || item[3].ToString() == "")
                break;
            if (item[0] == null || item[0].ToString().Trim() == "#N/D")
            {
                item[0] = "";
            }
            for (int i = 0; i < 17; i++)
            {
                if (item[i] == null)
                {
                    arrRes[i] = "";
                }
                else
                {
                    arrRes[i] = item[i].ToString();
                }

            }
            dtbFactura.Rows.Add(arrRes);
        }

        var ores = (from row in dtbFactura.AsEnumerable()
                    group row by new
                    {
                        sFechaFactura = row[0],
                        sPais = row[2],
                        sRFCProveedor = row[3],
                        sRefAdministrativa = row[4],
                        sUUID = row[5],
                        sMoneda = row[6],
                        sNoFactura = row[7],
                        sSerie = row[8],
                        sRFCCliente = row[9],
                        fDescuento = row[13],
                        sFormaPago = row[14],
                        sMetodoPago = row[15],
                        sRefOperativa = row[16],
                    } into rowGroup
                    select new object[]
                    {
                  rowGroup.Key.sFechaFactura,
                  rowGroup.Sum(r => float.Parse(r.Field<object>(1).ToString())),
                  rowGroup.Key.sPais,
                  rowGroup.Key.sRFCProveedor,
                  rowGroup.Key.sRefAdministrativa,
                  rowGroup.Key.sUUID,
                  rowGroup.Key.sMoneda,
                  rowGroup.Key.sNoFactura,
                  rowGroup.Key.sSerie,
                  rowGroup.Key.sRFCCliente,
                  rowGroup.Sum(r => float.Parse(r.Field<object>(10).ToString())),
                  rowGroup.Sum(r => float.Parse(r.Field<object>(11).ToString())),
                  rowGroup.Sum(r => float.Parse(r.Field<object>(12).ToString())),
                  rowGroup.Sum(r => float.Parse(r.Field<object>(13).ToString())),
                  rowGroup.Key.sFormaPago,
                  rowGroup.Key.sMetodoPago,
                  rowGroup.Key.sRefOperativa
                    }).ToList();
        dtbFactura.Rows.Clear();
        foreach (object[] row in ores)
        {
            dtbFactura.Rows.Add(row);
        }
    }

    private void fn_ObtenerServiciosFactura(DataTable dtbExcel)
    {
        this.dtbServiciosFactura = new DataTable();
        dtbServiciosFactura.Columns.AddRange(
            new DataColumn[] {
                new DataColumn("sNoFactura"),
                new DataColumn("sCveServicio"),
                new DataColumn("fIVA"),
                new DataColumn("fRetencion"),
                new DataColumn("fSubtotal"),
                new DataColumn("fTotal"),
                new DataColumn("fDescuento"),
                new DataColumn("fPorcentajeIva"),
                new DataColumn("sSerie")
            }
        );
        var res = from row in dtbExcel.AsEnumerable()
                      //where row.Field<object>(0).ToString().Trim() != "#N/A"
                  let array = new object[]
                  {
                            row.Field<object>(3),
                            row.Field<object>(20),
                            row.Field<object>(11),
                            row.Field<object>(12),
                            row.Field<object>(10),
                            row.Field<object>(14),
                            row.Field<object>(13),
                            row.Field<object>(30),
                            row.Field<object>(4)
                  }
                  select array;

        foreach (object[] item in res)
        {
            string[] arrRes = new string[9];
            if (item[0] == null || item[0].ToString() == "0")
                break;
            for (int i = 0; i < 9; i++)
            {
                if (item[i] == null)
                {
                    arrRes[i] = "";
                }
                else
                {
                    arrRes[i] = item[i].ToString();
                }

            }
            dtbServiciosFactura.Rows.Add(arrRes);
        }
    }

    /// <summary>
    /// Método para guardar la informacion del excel cargado por el usuario
    /// </summary>
    /// <param name="sNombre"></param>
    /// <param name="iIdUsuario"></param>
    /// <param name="iType">1=Carga Normal desde sistema 2=Carga desde WS automatica</param>
    /// <returns></returns>
    
    //Variable indentificadora para el tipo de hoja de excel
    string tipoTabla = "";
    public void fn_CargarArchivoProv(string sNombre, int iIdUsuario,int iType,Tools objTools)
    {
        objTools.lstErrores = new List<string>();
        //Se obtiene el archivo almacenado
        DataTable dtbAGuias;
        DataTable dtbImpo;
        DataTable dtbExpo;

        //Se llama el método para obtener las tablas
        Tuple<DataTable, DataTable, DataTable> objVariasTablas;
        objVariasTablas = fn_ObtenerTablaExcel(sNombre,iType);
        //Se asignan las tablas correspondientes
        dtbAGuias = objVariasTablas.Item1;
        dtbImpo = objVariasTablas.Item2;
        dtbExpo = objVariasTablas.Item3;

        // Quitar duplicados y vacios
        dtbAGuias = fn_QuitarVaciosTabla(dtbAGuias);
        dtbImpo = fn_QuitarVaciosTabla(dtbImpo);
        dtbExpo = fn_QuitarVaciosTabla(dtbExpo);

        //Se combina la informacion Version 1.2
        //Se extrae de Importacion datos para facturas en Asignacion de Guias
        dtbAGuias = fn_ObtenerInfoGuiaImp(dtbAGuias,dtbImpo);

        //Se carga la información por tabla Guias
        fn_GuardarInformacionBD(dtbAGuias, iIdUsuario,objTools);
        string[] arrResGuia = new string[2];
        arrResGuia[0] = objTools.iResultado.ToString();
        arrResGuia[1] = objTools.sMensaje.ToString();

        //Se carga la información por tabla Importacion
        fn_GuardarInformacionBD(dtbImpo, iIdUsuario, objTools);
        string[] arrResImpo = new string[2];
        arrResImpo[0] = objTools.iResultado.ToString();
        arrResImpo[1] = objTools.sMensaje.ToString();

        //Se carga la información por tabla Exportacion
        fn_GuardarInformacionBD(dtbExpo, iIdUsuario, objTools);
        string[] arrResExpo = new string[2];
        arrResExpo[0] = objTools.iResultado.ToString();
        arrResExpo[1] = objTools.sMensaje.ToString();

        //Se asigna mensaje global
        if (arrResGuia[0] == "1" && arrResImpo[0] == "1" && arrResExpo[0] == "1")
        {
            //Si todos pasaron se asigna resultado correcto
            objTools.iResultado = 1;
            objTools.sMensaje = "Información guardada de forma correcta";
        }
        else
        {
            //Se asigna error
            objTools.iResultado = 0;
            objTools.sMensaje = "Asignación Guias: " + arrResGuia[1] + ", Importación: " + arrResImpo[1] + ", Exportación:" + arrResExpo[1];
        }
        string sTabla = "<table> ";
        string alertEncabezadoError = "style = 'color: #8d6e09; background-color: #f4ce59; border-color: #bce8f1;''";
        sTabla += "<tr><th " + alertEncabezadoError + ">Pestaña</th>" +
                    "<th " + alertEncabezadoError + ">Observacion</th>" +
                 "</tr>";
        foreach(string sElemento in objTools.lstErrores)
        {
            sTabla += "<tr>"+sElemento+
                    "</tr>";
        }
        sTabla += "</table> ";
        




        string sEncabezado = "<div class='container-fluid fondo-gris-2'>" +
                                "<div class='row'>" +
                                    "<div class='col-lg-2 col-md-2 col-sm-2 col-xs-2'>" +
                                        "<tr><th><strong>Pestaña</strong></th></tr>" +
                                    "</div>" +
                                "<div class='col-lg-10 col-md-10 col-sm-10 col-xs-10'>" +
                                    "<tr><th><strong>Observacion</strong></th></tr>" +
                            "</div>" +
                        "</div></div>";
        objTools.sHTMLError = sTabla;
        objTools.dtbResultado = null;
        //Se regresa el obejto
    }

    /// <summary>
    /// Método que regresa en tablas las hojas del libro de excel cargado
    /// </summary>
    /// <param name="sNombre"></param>
    /// <returns></returns>
    private Tuple<DataTable, DataTable, DataTable> fn_ObtenerTablaExcel(string sNombre,int iType)
    {
        string sRuta = "";
        string sCarpeta = "";
        //Se valida si el tipo 1=Carga normal 2=Carga WS
        if (iType == 1)
        {
            //Se crea la ruta donde esta el archivo
            sRuta = HttpContext.Current.Server.MapPath("../../Documentos/Tools/" + sNombre.Split('.')[0] + "/" + sNombre);
            sCarpeta = HttpContext.Current.Server.MapPath("../../Documentos/Tools/" + sNombre.Split('.')[0]);
        }
        else
        {
            //Se crea la ruta donde esta el archivo
            sRuta = HttpContext.Current.Server.MapPath("../Documentos/Tools/" + sNombre.Split('.')[0] + "/" + sNombre);
            sCarpeta = HttpContext.Current.Server.MapPath("../Documentos/Tools/" + sNombre.Split('.')[0]);
        }
        
        DataTable dtbAsignacionGuias;
        DataTable dtbImportacion;
        DataTable dtbExportacion;
        //Se abre el archivo con la ruta
        using (var stream = File.Open(sRuta, FileMode.Open, FileAccess.Read))
        {
            //Se leen los datos del excel
            using (var reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                //Se convierte a tabla los datos
                var result = reader.AsDataSet();
                //Se asigna cada tabla obtenida a su correpondiente
                dtbAsignacionGuias = result.Tables[0];
                dtbImportacion = result.Tables[1];
                dtbExportacion = result.Tables[2];
            }
        }
        //Se elimina el archivo
        if (File.Exists(sRuta))
        {
            File.Delete(sRuta);
        }
        //Se elimina la carpeta
        if (Directory.Exists(sCarpeta))
        {
            Directory.Delete(sCarpeta);
        }
        //Se regresan las tablas con datos
        return Tuple.Create(dtbAsignacionGuias, dtbImportacion, dtbExportacion);
    }

    /// <summary>
    /// Método que permite quitar los espacios vacios al final y se quitan las 2 filas iniciales
    /// </summary>
    /// <param name="dtbTabla"></param>
    /// <returns></returns>
    private DataTable fn_QuitarVaciosTabla(DataTable dtbTabla)
    {
        //Se asigna la tabla con los datos especificos
        dtbTabla = (from row in dtbTabla.AsEnumerable()
                    where row.Field<object>(3) != null && !String.IsNullOrEmpty(row.Field<object>(3).ToString().Trim())
                    select row).CopyToDataTable();
        //Se regresa la tabla
        return dtbTabla;
    }

    /// <summary>
    /// Método que permite almacenar la información en Base de Datos
    /// </summary>
    /// <param name="dtbTablaInsert"></param>
    /// <param name="iIdUsuario"></param>
    /// <returns></returns>
    private void fn_GuardarInformacionBD(DataTable dtbTablaInsert, int iIdUsuario,Tools objTools)
    {
        //Se instancia la clase
        Conexion objConexion = new Conexion();
        //Se crea el objeto a devolver
        string[] arrResultado = new string[2];
        try
        {
            fn_ConvertirTablaGeneral(objTools, dtbTablaInsert);
        }
        catch (Exception ex)
        {

            //string sError = "<div class='container-fluid fondo-gris-1'>" +
            //                  "<div class='row'>" +
            //                    "<div class='col-lg-2 col-md-2 col-sm-2 col-xs-12'>" +
            //                        "<tr><th>" + tipoTabla + "</th></tr>" +
            //                    "</div>" +
            //                    "<div class='col-lg-10 col-md-10 col-sm-10 col-xs-10'>" +
            //                        "<tr>" +
            //                        "<th> Ocurrio un error al cargar los  datos de la pestaña mencionada, favor de revisar el conflicto y/o contactar al administrador.</th>" +
            //                        "</tr>" +
            //                    "</div>" +
            //                  "</div>" +
            //                "</div>";

            string sError = "<tr><th>" + tipoTabla + " </th>" +
                            "<th>Ocurrio un error al cargar los  datos de la pestaña mencionada, favor de revisar el conflicto y/o contactar al administrador.</th>" +
                            "</tr>";
            objTools.lstErrores.Add(sError);
            return;
        }
        
        //****Una vez llena la tabla se almacena en Base de datos
        //Se llama el procedure
        string sRes = objConexion.generarSP("pa_GuardarInformacionReporte", 0);
        //Se valida si existe el procedure
        if (sRes == "1")
        {
            //Se agregan los parametros que se van a recibir
            objConexion.agregarParametroSPTabla("@dtbInfoReporte", objTools.dtbResultado);
            objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, iIdUsuario.ToString());
            //Se ejecuta el proc
            //string[] sResOut = { "" };
            string[] sResOut = objConexion.ejecutarProcOUTPUT_STRING("@iResOut");
            //Se valida si se ejecuto correctamente
            if (sResOut[0] == "1")
            {
                //Se valida si el proc si guardo la info
                if (sResOut[1] == "1")
                {
                    //Se asigna exito
                    arrResultado[0] = "1";
                    arrResultado[1] = "Informacíon guardada con exito";
                }
                else
                {
                    //Se asigna error
                    arrResultado[0] = "0";
                    arrResultado[1] = "Ocurrio un error, favor de contactar al administrador";
                }
            }
            else
            {
                //Se asigna error
                arrResultado[0] = "0";
                arrResultado[1] = "Ocurrio un error, favor de contactar al administrador";
            }
        }
        else
        {
            //Se asigna el resultado
            arrResultado[0] = "0";
            arrResultado[1] = "No se encontro el procedimiento";
        }
        objTools.iResultado =int.Parse(arrResultado[0]);;
        objTools.sMensaje = arrResultado[1];
    }

    /// <summary>
    /// Método para convertir las tablas en una general con los parametros especificados
    /// </summary>
    /// <param name="dtbTablaInsert"></param>
    /// <returns></returns>
    private void fn_ConvertirTablaGeneral(Tools objTools, DataTable dtbTablaInsert)
    {
        //Se quita la primer fila
        dtbTablaInsert.Rows.RemoveAt(0);
        //Se crea la tabla
        DataTable tblGeneral = new DataTable();
        tblGeneral.Columns.AddRange(new DataColumn[]{
            new DataColumn("sFactura",              Type.GetType("System.String")),
            new DataColumn("sAerolinea",            Type.GetType("System.String")),
            new DataColumn("sCliente",              Type.GetType("System.String")),
            new DataColumn("sFecha",                Type.GetType("System.String")),
            new DataColumn("sGuiaMaster",           Type.GetType("System.String")),
            new DataColumn("sGuiaHouse",            Type.GetType("System.String")),
            new DataColumn("iBultos",               Type.GetType("System.Int32")),
            new DataColumn("fPeso",                 Type.GetType("System.Decimal")),
            new DataColumn("sFechaEntrada",         Type.GetType("System.String")),
            new DataColumn("sFechaSalida",          Type.GetType("System.String")),
            new DataColumn("fValorUSD",             Type.GetType("System.Decimal")),
            new DataColumn("fValorMXN",             Type.GetType("System.Decimal")),
            new DataColumn("fSubtotal",             Type.GetType("System.Decimal")),
            new DataColumn("fIva",                  Type.GetType("System.Decimal")),
            new DataColumn("fTotal",                Type.GetType("System.Decimal")),
            new DataColumn("iDiasAlmacenaje",       Type.GetType("System.Int32")),
            new DataColumn("fAlmacenaje",           Type.GetType("System.Decimal")),
            new DataColumn("fPrevios",              Type.GetType("System.Decimal")),
            new DataColumn("fManejo",               Type.GetType("System.Decimal")),
            new DataColumn("fCustodia",             Type.GetType("System.Decimal")),
            new DataColumn("fManejoValores",        Type.GetType("System.Decimal")),
            new DataColumn("sInspeccionSeguridad",  Type.GetType("System.String")),
            new DataColumn("sRegimen",              Type.GetType("System.String")),
        });
        


        //Variable de columnas
        int iColumnas = dtbTablaInsert.Columns.Count;
        //Se crea variable para object
        var sRes = from fila in dtbTablaInsert.AsEnumerable()
                   let arrFila = new object[] { "" }
                   select arrFila;//Registro nulo solo para inicializar la variable de forma correcta
        //Se valida cuantas columnas trae la tabla ya que asignacion de guias tiene menos que IMPO y EXPO
        if (iColumnas == 17)//Tabla asignacion guia
        {
            #region ObtenerColumnas
            //Se obtienen los datos de la tabla
            sRes = from fila in dtbTablaInsert.AsEnumerable()
                   let arrFila = new object[]
                   {
                        fila.Field<object>(0)!=null?fila.Field<object>(0):"",
                        fila.Field<object>(4),
                        fila.Field<object>(3),
                        fila.Field<object>(1),
                        fila.Field<object>(5),
                        fila.Field<object>(6),
                        0,//Bultos
                        fila.Field<object>(13),//Peso
                        fila.Field<object>(10),//FechaENtrada
                        fila.Field<object>(11),//FechaSalida
                        fila.Field<object>(14),//ValorUSD
                        fila.Field<object>(15),//ValorMXN
                        fila.Field<object>(7),//Subtotal
                        fila.Field<object>(8),//Iva
                        fila.Field<object>(9),//Total
                        fila.Field<object>(12),//Dias almacenaje
                        0,//Almacenaje
                        0,//previos
                        0,//manejo
                        0,//custodia
                        0,//manejo valores
                        "",//inspeccion seguridad
                        "NA"//regimen
                   }
                   select arrFila;
            tipoTabla = "Asignacion de Guia";
            #endregion
        }
        else if (iColumnas == 20)//IMPO
        {
            #region ObtenerColumnas
            //Se obtienen los datos de la tabla
            sRes = from fila in dtbTablaInsert.AsEnumerable()
                   let arrFila = new object[]
                   {
                           fila.Field<object>(0)!=null?fila.Field<object>(0):"",
                           fila.Field<object>(1),
                           fila.Field<object>(2),
                           "",
                           fila.Field<object>(3),
                           fila.Field<object>(4),
                           fila.Field<object>(5),
                           fila.Field<object>(6),
                           fila.Field<object>(7),
                           fila.Field<object>(8),
                           fila.Field<object>(9),
                           fila.Field<object>(10),
                           fila.Field<object>(11),
                           fila.Field<object>(12),
                           fila.Field<object>(13),
                           fila.Field<object>(14),
                           fila.Field<object>(15),
                           fila.Field<object>(16),
                           fila.Field<object>(17),
                           fila.Field<object>(18),
                           fila.Field<object>(19),
                           "","Imp"
                   }
                   select arrFila;
            tipoTabla = "Importacion";
            #endregion
        }
        else //EXPO
        {
            #region ObtenerColumnas
            //Se obtienen los datos de la tabla
            sRes = from fila in dtbTablaInsert.AsEnumerable()
                   let arrFila = new object[]
                   {
                           fila.Field<object>(0)!=null?fila.Field<object>(0):"",
                           fila.Field<object>(1),
                           fila.Field<object>(2),
                           "","",
                           fila.Field<object>(3),
                           fila.Field<object>(4),
                           fila.Field<object>(5),
                           fila.Field<object>(6),
                           fila.Field<object>(7),
                           fila.Field<object>(8),
                           fila.Field<object>(9),
                           fila.Field<object>(10),
                           fila.Field<object>(11),
                           fila.Field<object>(12),
                           0,0,0,
                           fila.Field<object>(13),
                           0,
                           fila.Field<object>(14),
                           fila.Field<object>(15),"Exp"
                   }
                   select arrFila;
            tipoTabla = "Exportacion";
            #endregion
        }

        //Variable de bandera
        bool bBandera = true;
        string sAeroLinea = "";
        string sGuiaHouse = "";
        
        //Se recorren los datos obtenidos
        foreach (object[] registro in sRes)
        {
            //Se crea variable para obtener los resultados por columna
            string[] sResultado = new string[tblGeneral.Columns.Count];
            DataRow fila = tblGeneral.NewRow();

            //Se recorre cada columna de la tabla
            for (int i = 0; i < tblGeneral.Columns.Count; i++)
            {
                //string sColummna = registro[i].ToString();
                
                //Se valida que la primera columna traiga factura
                //de no ser asi no se asignan los datos porque se trabaja con el no Factura
                if (i == 0) {
                    if (String.IsNullOrWhiteSpace(registro[i].ToString()))
                    {
                        bBandera = false;
                    }
                    else
                    {
                        bBandera=true;
                    }
                }
                sAeroLinea = i == 1 ? registro[i].ToString() : sAeroLinea;
                sGuiaHouse = i == 5 ? registro[i].ToString() : sGuiaHouse;

                //Registros en entero
                if (i == 6 || i == 15) {
                    if (String.IsNullOrEmpty(registro[i].ToString()))
                    {
                        fila[i] = 0;
                    }
                    else
                    {
                        fila[i] = int.Parse(registro[i].ToString());
                    }
                    
                } else if (i == 8 || i == 9) {//Registros tipo fecha
                    if (String.IsNullOrEmpty(registro[i].ToString()))
                    {
                        fila[i] = "";
                    }
                    else
                    {
                        fila[i] = DateTime.Parse(registro[i].ToString()).ToString("yyyy-MM-dd");
                    }
                }
                else if (i == 7 || i == 10 || i == 11 || i == 12 
                    || i == 13 || i == 14 || i == 16 || i == 17 
                    || i == 18 || i == 19 || i == 20)//Registros decimales
                {
                    //Se valida si es nulo
                    if (String.IsNullOrEmpty(registro[i].ToString()))
                    {
                        fila[i] = 0;
                    }
                    else
                    {
                        fila[i] = decimal.Parse(registro[i].ToString());
                    }
                }
                else //Registros string
                {
                    fila[i] = registro[i];
                }

            }
            //Se valida si la bandera es verdadera y leer los demas campos
            if (bBandera)
            {
                //Se asigna la fila a la tabla
                tblGeneral.Rows.Add(fila);

            }
            else
            {
                //string TablaError = "<div class='container-fluid fondo-gris-1'>" +
                //                        "<div class='row'>" +
                //                            "<div class='col-lg-2 col-md-2 col-sm-2 col-xs-2'>" +
                //                                "<tr><th>" + tipoTabla + "</th></tr>" +
                //                            "</div>" +
                //                            "<div class='col-lg-10 col-md-10 col-sm-10 col-xs-10'>" +
                //                                "<tr><th> En la pestaña existe una Aero Linea: "+ sAeroLinea + " y una Guia House: "+ sGuiaHouse +" que no contiene " +
                //                                "numero de factura.</th></tr>" +
                //                            "</div>" +
                //                        "</div>" +
                //                    "</div>";
                
                string TablaError = "<tr><th>" + tipoTabla + " </th>" +
                    "                   <th>En la pestaña existe una Aero Linea: " + sAeroLinea + " y una Guia House: " + sGuiaHouse + " que no contiene " +
                                            "numero de factura.</th>" +
                                     "</tr>";

                objTools.lstErrores.Add(TablaError);             
            }
        }
        objTools.dtbResultado = tblGeneral;

    }

    /// <summary>
    /// Método para comparar y asignar la informacion de Importacion en 
    /// Guia tomando en cuenta la Guia House
    /// </summary>
    /// <param name="dtbGuia">Tabla pestaña Asignación Guia</param>
    /// <param name="dtbImpo">Tabla pestaña Importación</param>
    /// <returns></returns>
    private DataTable fn_ObtenerInfoGuiaImp(DataTable dtbGuia, DataTable dtbImpo)
    {
        string sValorObtener = "";
        bool bBandera;
        //Se agregan 6 columnas vacias a la tabla Guia para guardar los datos
        dtbGuia.Columns.Add(new DataColumn("sFechaEntrada",     typeof(string)));
        dtbGuia.Columns.Add(new DataColumn("sFechaDespacho",    typeof(string)));
        dtbGuia.Columns.Add(new DataColumn("dAlamacenaje",      typeof(string)));
        dtbGuia.Columns.Add(new DataColumn("iPeso",             typeof(string)));
        dtbGuia.Columns.Add(new DataColumn("dValorUSD",         typeof(string)));
        dtbGuia.Columns.Add(new DataColumn("dValorMXN",         typeof(string)));
        dtbGuia.Columns.Add(new DataColumn("No Aplica",         typeof(string))); //Esta columna solo se creo para diferencia Expo de Guia
        
        //Se recorre la tabla de Guia para asignar los datos faltantes
        foreach (DataRow dtFilaGuia in dtbGuia.Rows)
        {
            //Se asigna un valor defecto a la variable
            bBandera = false;
            
            //Se asigna el valor que se va a obtener
            sValorObtener = dtFilaGuia[6].ToString();

            //Se recorre la tabla de Impo
            foreach (DataRow dtFilaImp in dtbImpo.Rows)
            {
                //Cuando coincida el valor se asignan los datos
                if (sValorObtener == dtFilaImp[4].ToString())
                {
                    //Se asignan los datos faltantes
                    //Fecha Entrada
                    dtFilaGuia[10] = dtFilaImp[7].ToString();
                    //Fecha Despacho
                    dtFilaGuia[11] = dtFilaImp[8].ToString();
                    //Dias almacenaje
                    dtFilaGuia[12] = dtFilaImp[14].ToString();
                    //Peso Guia
                    dtFilaGuia[13] = dtFilaImp[6].ToString();
                    //Valor USD
                    dtFilaGuia[14] = dtFilaImp[9].ToString();
                    //Valor MXN
                    dtFilaGuia[15] = dtFilaImp[10].ToString();
                    dtFilaGuia[16] = "0";
                    bBandera = true;
                }
            }
            //Se valida si se obtuvo un resultado en la busqueda
            //Si no hubo coincidencias se asigna en vacio
            if (!bBandera)
            {
                dtFilaGuia[10] = "";
                dtFilaGuia[11] = "";
                dtFilaGuia[12] = "";
                dtFilaGuia[13] = "";
                dtFilaGuia[14] = "";
                dtFilaGuia[15] = "";
                dtFilaGuia[16] = "";
                //Se llena una lista con aquellos datos que no tenian
                //información para mandar un alerta
            }
        }
        return dtbGuia;
    }
}