using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.SessionState;

/// <summary>
/// Descripción breve de ToolsImpuesto
/// </summary>
public class ToolsImpuesto
{
    public string sResultado { get; private set; }
    public float fProgreso { get; set; }
    public string sFileName { get; set; }
    public bool fCompletado { get; set; }

    #region Datatable factura
    public DataTable dtbPoliza { get; set; }
    public DataTable dtbImpuestos { get; set; }
    #endregion

    public List<string> lstErrores { get; set; }

    public ToolsImpuesto()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public void fn_GeneraSelectTablas(ToolsImpuesto objToolsImpuesto)
    {
        Utilerias objUtilerias = new Utilerias();
        objUtilerias.sQuery = @"SELECT ROW_NUMBER() OVER(ORDER BY tbls.name), tbls.name
                                FROM sys.tables tbls";
        objUtilerias.sNombre = "hsclTablas";
        objUtilerias.fn_GeneraCombobox(objUtilerias);
        objToolsImpuesto.sResultado = objUtilerias.sContenido;
    }

    private DataTable fn_ObtenerDtbExcel(ToolsImpuesto objToolsImpuesto, HttpContext oContext)
    {
        string sRuta = oContext.Server.MapPath("../../Documentos/Tools/" + objToolsImpuesto.sFileName.Split('.')[0] + "/" + objToolsImpuesto.sFileName);
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
                    select row).Distinct().CopyToDataTable();
        // Aumentar progreso en 10%
        fn_AumentarProgreso((HttpSessionState)((object[])arrParametros)[0], 10);
        // Obtener campos requeridos por SP facturas
        this.fn_ObtenerCamposPoliza(dtbExcel);
        // Aumentar progreso en 10%
        fn_AumentarProgreso((HttpSessionState)((object[])arrParametros)[0], 10);
        // Obtener campos requeridos por SP Servicio facturas
        this.fn_ObtenerCamposImpuesto(dtbExcel);
        // Aumentar progreso en 10%
        fn_AumentarProgreso((HttpSessionState)((object[])arrParametros)[0], 10);
        // Insertar las facturas
        this.fn_InsertarPolizas((HttpSessionState)((object[])arrParametros)[0]);
    }
    /// <summary>
    /// inserta los valores en BD
    /// </summary>
    /// <param name="objSesion"></param>
    private void fn_InsertarPolizas(HttpSessionState objSesion)
    {
        lstErrores = new List<string>();
        // Calcular porcentaje que cada factura va a aumentar
        float fPorcentaje = 50.0f / this.dtbPoliza.Rows.Count;
        foreach (DataRow objRow in this.dtbPoliza.Rows)
        {
            // Insertar la factura
            string sInsertada = this.fn_InsertarPoliza(objRow);
            while (sInsertada.Contains("pa_GuardarPolizaDesdeLQUI"))
            {
                sInsertada = this.fn_InsertarPoliza(objRow);
                Thread.Sleep(5000);
            }
            // Si se inserto la factura inserta los servicios
            if (sInsertada == "1")
            {
                var oRes = from row in dtbImpuestos.AsEnumerable()
                           where row.Field<object>(11).ToString() == objRow[2].ToString()
                           select row;
                foreach (DataRow objRowServicio in oRes)
                {
                    sInsertada = this.fn_InsertarImpuestos(objRowServicio);
                    while (sInsertada.Contains("pa_GuardarServicioPolizaDesdeLQUI"))
                    {
                        sInsertada = this.fn_InsertarImpuestos(objRowServicio);
                        Thread.Sleep(5000);
                    }
                }
            }
            if (sInsertada != "1" && sInsertada != "2")
            {
                // Si hubo error se agrega al log
                lstErrores.Add("Error factura: " + objRow[2].ToString() + " " + sInsertada);
            }
            fn_AumentarProgreso(objSesion, fPorcentaje);
        }
        objSesion["lstErrores"] = lstErrores;
        objSesion["fCompletado"] = true;
    }

    private string fn_InsertarImpuestos(DataRow objRowServicioTarifa)
    {
        //Instanciamos clase de conexión
        Conexion oConexion = new Conexion();
        //Generamos el SP
        string sRes = oConexion.generarSP("pa_GuardarImpuestosLQUI", 0);
        //Creamos arreglo para guardar resultado del store
        string[] sResOut;
        //Si el procedimiento se generó correctamente
        if (sRes == "1")
        {
            // Se obtiene objeto datetime
            DateTime objTime;
            try
            {
                objTime = Convert.ToDateTime(objRowServicioTarifa[3].ToString());
            }
            catch (Exception e)
            {
                // Ajustar fecha
                string sFecha = objRowServicioTarifa[3].ToString().Substring(0, 10);
                try
                {
                    objTime = DateTime.ParseExact(sFecha, "dd-MM-yyyy",
                                    CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    objTime = DateTime.ParseExact(sFecha, "yyyy-dd-MM",
                                    CultureInfo.InvariantCulture);
                }
            }
            //Ejecutamos el procedimiento
            oConexion.agregarParametroSP("@sRefAdministrativa", SqlDbType.VarChar, objRowServicioTarifa[0].ToString());
            oConexion.agregarParametroSP("@sRefOperativa", SqlDbType.VarChar, objRowServicioTarifa[1].ToString());
            oConexion.agregarParametroSP("@sRFCCliente", SqlDbType.VarChar, objRowServicioTarifa[2].ToString());
            oConexion.agregarParametroSP("@sFechaPago", SqlDbType.VarChar, objTime.ToString().Substring(0, 10));
            oConexion.agregarParametroSP("@sTipoOperacion", SqlDbType.VarChar, objRowServicioTarifa[4].ToString());
            oConexion.agregarParametroSP("@sAduana", SqlDbType.VarChar, objRowServicioTarifa[5].ToString());
            oConexion.agregarParametroSP("@sPatente", SqlDbType.VarChar, objRowServicioTarifa[6].ToString());
            oConexion.agregarParametroSP("@fEfectivo", SqlDbType.Decimal, objRowServicioTarifa[7].ToString());
            oConexion.agregarParametroSP("@sCuentaBancaria", SqlDbType.VarChar, objRowServicioTarifa[8].ToString());
            oConexion.agregarParametroSP("@sServicio", SqlDbType.VarChar, objRowServicioTarifa[9].ToString());
            oConexion.agregarParametroSP("@fImporte", SqlDbType.Decimal, objRowServicioTarifa[10].ToString());
            oConexion.agregarParametroSP("@sNumPoliza", SqlDbType.VarChar, objRowServicioTarifa[11].ToString());
            oConexion.agregarParametroSP("@sPedimento", SqlDbType.VarChar, objRowServicioTarifa[12].ToString());

            sResOut = oConexion.ejecutarProcOUTPUT_STRING("@sResOut");



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
        else
        {
            return "Error al generar el procedimiento pa_GuardarServicioPolizaDesdeLQUI no existe";
        }

    }
    /// <summary>
    /// inserta la poliza de egreso en BD
    /// </summary>
    /// <param name="objRowTarifa"></param>
    /// <returns></returns>
    private string fn_InsertarPoliza(DataRow objRowTarifa)
    {
        //Instanciamos clase de conexión
        Conexion oConexion = new Conexion();
        //Generamos el SP
        string sRes = oConexion.generarSP("pa_GuardarPolizaDesdeLQUI", 0);
        //Creamos arreglo para guardar resultado del store
        string[] sResOut;
        //Si el procedimiento se generó correctamente
        if (sRes == "1")
        {
            // Se obtiene objeto datetime
            DateTime objTime;
            try
            {
                objTime = Convert.ToDateTime(objRowTarifa[3].ToString());
            }
            catch (Exception e)
            {
                // Ajustar fecha
                string sFecha = objRowTarifa[3].ToString().Substring(0, 10);
                try
                {
                    objTime = DateTime.ParseExact(sFecha, "dd-MM-yyyy",
                                    CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    objTime = DateTime.ParseExact(sFecha, "yyyy-dd-MM",
                                    CultureInfo.InvariantCulture);
                }
            }
            //Ejecutamos el procedimiento
           
            oConexion.agregarParametroSP("@sRefAdministrativa", SqlDbType.VarChar, objRowTarifa[0].ToString());
            oConexion.agregarParametroSP("@sRefOperativa", SqlDbType.VarChar, objRowTarifa[1].ToString());
            oConexion.agregarParametroSP("@sNumPoliza", SqlDbType.VarChar, objRowTarifa[2].ToString());
            oConexion.agregarParametroSP("@sFecha", SqlDbType.VarChar, objTime.ToString().Substring(0, 10));
            oConexion.agregarParametroSP("@sRFCCliente", SqlDbType.VarChar, objRowTarifa[4].ToString());
            oConexion.agregarParametroSP("@sCuentaBancaria", SqlDbType.VarChar, objRowTarifa[5].ToString());
            oConexion.agregarParametroSP("@fImporte", SqlDbType.Decimal, objRowTarifa[6].ToString());
            oConexion.agregarParametroSP("@sPedimento", SqlDbType.VarChar, objRowTarifa[7].ToString());

            sResOut = oConexion.ejecutarProcOUTPUT_STRING("@sResOut");
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
        return "Error al generar el procedimiento pa_GuardarPolizaDesdeLQUI";
    }
    /// <summary>
    /// metodo para obtener referencia
    /// </summary>
    /// <param name="dtbExcel"></param>
    private void fn_ObtenerCamposPoliza(DataTable dtbExcel)
    {
        this.dtbPoliza = new DataTable();
        dtbPoliza.Columns.AddRange(
            new DataColumn[] {
                 new DataColumn("sRefAdministrativa"),
                new DataColumn("sRefOperativa"),
                new DataColumn("sNumPoliza"),
                new DataColumn("sFecha"),
                new DataColumn("sRFCCliente"),
                new DataColumn("sCuentaBancaria"),
                new DataColumn("fImporte"),
                new DataColumn("sPedimento")

            }
        );
        var res = from row in dtbExcel.AsEnumerable()
                  let array = new object[]
                  {
                             row.Field<object>(0),
                             row.Field<object>(2),
                            row.Field<object>(3),
                            row.Field<object>(6),
                            row.Field<object>(9),
                            row.Field<object>(5),
                            row.Field<object>(7),
                            row.Field<object>(14),
                  }
                  select array;

        foreach (object[] item in res)
        {
            string[] arrRes = new string[8];
            if (item[3] == null || item[3].ToString() == "")
                break;
            if (item[0] == null || item[0].ToString().Trim() == "#N/A") {
                item[0] = "";
            }
                //    continue;
                for (int i = 0; i < 8; i++)
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
            dtbPoliza.Rows.Add(arrRes);
        }

        var ores = (from row in dtbPoliza.AsEnumerable()
                    group row by new
                    {
                       
                       sRefAdministrativa = row[0],
                        sRefOperativa = row[1],
                        sNumPoliza = row[2],
                        sFecha = row[3],
                        sRFCCliente = row[4],
                        sCuentaBancaria = row[5],
                        sPedimento = row[7],
                    } into rowGroup
                    select new object[]
                    {
                  rowGroup.Key.sRefAdministrativa,           
                  rowGroup.Key.sRefOperativa,
                  rowGroup.Key.sNumPoliza,
                  rowGroup.Key.sFecha,
                  rowGroup.Key.sRFCCliente,
                  rowGroup.Key.sCuentaBancaria,                 
                  rowGroup.Sum(r => float.Parse(r.Field<object>(6).ToString())),
                  rowGroup.Key.sPedimento,
                    }).ToList();
        dtbPoliza.Rows.Clear();
        foreach (object[] row in ores)
        {
            dtbPoliza.Rows.Add(row);
        }
    }

    
    /// <summary>
    /// metodo para obtener lso datos del excel
    /// </summary>
    /// <param name="dtbExcel"></param>
    private void fn_ObtenerCamposImpuesto(DataTable dtbExcel)
    {
        this.dtbImpuestos = new DataTable();
        dtbImpuestos.Columns.AddRange(
            new DataColumn[] {
               
                new DataColumn("sRefAdministrativa"),
                new DataColumn("sRefOperativa"),
                new DataColumn("sRFCCliente"),
                new DataColumn("sFechaPago"),
                new DataColumn("sTipoOperacion"),
                new DataColumn("sAduana"),
                new DataColumn("sPatente"),
                new DataColumn("fEfectivo"),
                new DataColumn("sCuentaBancaria"),
                new DataColumn("sServicio"),
                new DataColumn("fImporte"),
                 new DataColumn("sNumPoliza"),
                new DataColumn("sPedimento"),
            }
        );
        var res = from row in dtbExcel.AsEnumerable()
                  //where row.Field<object>(0).ToString().Trim() != "#N/A"
                  let array = new object[]
                  {
                            row.Field<object>(0),
                             row.Field<object>(2),
                            row.Field<object>(9),
                            row.Field<object>(10),
                            row.Field<object>(11),
                            row.Field<object>(12),
                            row.Field<object>(13),
                            row.Field<object>(15),
                            row.Field<object>(17),
                            row.Field<object>(18),
                            row.Field<object>(19),
                            row.Field<object>(3),
                            row.Field<object>(1),

                  }
                  select array;

        foreach (object[] item in res)
        {
            
            string[] arrRes = new string[13];
            // acomoda la ref administrativa
            if (item[0] == null || item[0].ToString().Trim() == "#N/A")
            {
                item[0] = "";
            }
            // cuando la referencia operativa sea vacia sale
            if (item[1] == null || item[1].ToString() == "0")
                break;
            for (int i = 0; i < 13; i++)
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
            dtbImpuestos.Rows.Add(arrRes);
        }

        
    }
}