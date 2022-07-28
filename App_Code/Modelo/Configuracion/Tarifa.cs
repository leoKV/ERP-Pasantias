using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Descripción breve de Tarifa
/// </summary>
public class Tarifa
{
    public Tarifa()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    //atributos de la clase
    public string sConcepto { get; set; }
    public decimal dValorAduana { get; set; }
    public decimal dImpuestosPagados { get; set; }
    public decimal dImpuestosAfianzados { get; set; }
    public decimal dImpuestosSubsidiados { get; set; }
    public decimal dGastosComprobados { get; set; }
    public decimal dPartidaPresupuestal { get; set; }
    public decimal dGastosDirectos { get; set; }
    public bool bMostrarBoton { get; set; }
    public ICobroBehavior objCobroBehavior { get; set; }
    public decimal dSubTotal { get; set; }
    public decimal dIva { get; set; }
    public decimal dRetencion { get; set; }
    public decimal dTotal { get; set; }
    public int iIdConfTarifa { get; set; }
    public string iIdAduana { get; set; }
    public decimal dPesoBruto { get; set; }
    public int iIdFolioTransitorio { get; set; }
    public int iIdCliente { get; set; }
    public int iIdTipoMaterial { get; set; }
    public int iIdTipoTarifa { get; set; }
    public int iIdServicio { get; set; }
    public int iIdTipoRegla { get; set; }
    public int iIdTipoOperacion { get; set; }
    public int iIdMoneda { get; set; }
    public int iIdTipoRegimen { get; set; }
    public int iIdTipoDocumento { get; set; }
    public decimal dCantidad { get; set; }
    public string sMonto { get; set; }
    public string sCobroMinimo { get; set; }
    public int iIdFormula { get; set; }
    public string sMaxMin { get; set; }
    private DataTable dt_MaxMin { get; set; }
    private DataTable dt_Aduanas { get; set; }
    public DataTable dt_Tarifas { get; set; }
    public string sIdBaseCalculo { get; set; }
    public string sIdTipoRegla { get; set; }
    public int iIdTipoRangoTarifa { get; set; }
    public string sIdTarifa { get; set; }
    public int iIdTarifa { get; set; }
    public int iIdTarifaReferencia { get; set; }
    public int iIva { get; set; }
    public int iIdIva { get; set; }
    public int iRetencion { get; set; }
    public decimal dMonto { get; set; }
    public int iIdTipoMontoPorcentaje { get; set; }
    public string sFolioTransitorio { get; set; }
    public string sIdSubReferencia { get; set; }
    public int iAccion { get; set; }
    public int iEliminada { get; set; }
    public int iIdTipoFactura { get; set; }
    public int iIdMaterial { get; set; }
    public int iIdSubReferencia { get; set; }
    public int iIdUsuario { get; set; }
    //atributos de respuesta
    public int iResultado { get; set; }
    public string sMensaje { get; set; }
    public string sFileName { get; set; }
    public decimal iCantidadColocada { get; set; }
    public decimal dAnticipos { get; set; }
    public decimal dImpuestosFinanciadosPorNAD { get; set; }

    /// <summary>
    /// Método para guardar la configuración de tarifa
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_GuardarConfiguracionTarifa(Tarifa objTarifa)
    {
        //instancia Conexión
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarConfiguracionTarifa", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //pasar parametros al sp
                objConexion.agregarParametroSP("@iIdTarifa", SqlDbType.Int, objTarifa.iIdTarifa.ToString());
                objConexion.agregarParametroSP("@iIdFormula", SqlDbType.Int, objTarifa.iIdFormula.ToString());
                //objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objTarifa.iIdAduana.ToString());
                objConexion.agregarParametroSPTabla("@dt_Aduana", objTarifa.dt_Aduanas);
                objConexion.agregarParametroSP("@iIdCliente", SqlDbType.Int, objTarifa.iIdCliente.ToString());
                objConexion.agregarParametroSP("@iIdTipoMaterial", SqlDbType.Int, objTarifa.iIdTipoMaterial.ToString());
                objConexion.agregarParametroSP("@iIdTipoTarifa", SqlDbType.Int, objTarifa.iIdTipoTarifa.ToString());
                objConexion.agregarParametroSP("@iIdServicio", SqlDbType.Int, objTarifa.iIdServicio.ToString());
                objConexion.agregarParametroSP("@iIdTipoRegla", SqlDbType.Int, objTarifa.iIdTipoRegla.ToString());
                objConexion.agregarParametroSP("@iIdTipoOperacion", SqlDbType.Int, objTarifa.iIdTipoOperacion.ToString());
                objConexion.agregarParametroSP("@iIdMoneda", SqlDbType.Int, objTarifa.iIdMoneda.ToString());
                objConexion.agregarParametroSP("@iIdTipoRegimen", SqlDbType.Int, objTarifa.iIdTipoRegimen.ToString());
                objConexion.agregarParametroSP("@iIdTipoDocumento", SqlDbType.Int, objTarifa.iIdTipoDocumento.ToString());
                objConexion.agregarParametroSP("@sMonto", SqlDbType.Float, objTarifa.sMonto);
                objConexion.agregarParametroSP("@sCobroMinimo", SqlDbType.Float, objTarifa.sCobroMinimo);
                objConexion.agregarParametroSPTabla("@dt_MaxMin", objTarifa.dt_MaxMin);
                objConexion.agregarParametroSP("@sIdTipoRegla", SqlDbType.VarChar, objTarifa.sIdTipoRegla);
                objConexion.agregarParametroSP("@iIdTipoRangoTarifa", SqlDbType.VarChar, objTarifa.iIdTipoMontoPorcentaje.ToString());
                objConexion.agregarParametroSP("@sIdBaseCalculo", SqlDbType.VarChar, objTarifa.sIdBaseCalculo.ToString());
                objConexion.agregarParametroSP("@iIva", SqlDbType.VarChar, objTarifa.iIva.ToString());
                objConexion.agregarParametroSP("@iRetencion", SqlDbType.VarChar, objTarifa.iRetencion.ToString());
                objConexion.agregarParametroSP("@iIdTipoMontoPorcentaje", SqlDbType.VarChar, objTarifa.iIdTipoMontoPorcentaje.ToString());
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, objTarifa.iIdUsuario.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@sResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objTarifa.iResultado = 1;
                    objTarifa.sMensaje = "Configuración de tarifa guardada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTarifa.iResultado = 0;
                    objTarifa.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //mensaje de error al caer en una excepción
                objTarifa.iResultado = 0;
                objTarifa.sMensaje = "Error con su base de datos, " + ex.Message.ToString();
            }
        }
        else
        {
            //mensaje de error con la base de datos
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Error con su base de datos, no existe procedimiento almacenado";
        }
    }

    /// <summary>
    /// Función usada para obtener las tarifas por el tipo de material de una subreferencia
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_ObtenerTarifasPorMaterial(Tarifa objTarifa)
    {
        try
        {
            //Se instancia clase de conexion
            Conexion objConexion = new Conexion();
            //Se declara la query
            string sQuery = "SELECT " +
                            "    (SELECT CONVERT(VARCHAR(6),cantidad) FROM tTarifaReferencia ti WHERE ti.idConfTarifa = tct.idConfTarifa AND ti.idFolioSubReferencia IN  (Select tfsr.idFolioSubReferencia FROM tFolioTransitorioSubReferencia tfsr WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + ")) AS [txtCantidad],  " +
                            "    tct.idConfTarifa AS [idConfTarifa],  " +
                            "    (SELECT nombre FROM cTipoMaterial ctm WHERE ctm.idTipoMaterial = tct.idTipoMaterial) AS [Material],  " +
                            "    (SELECT nombre FROM cTipoTarifa ctt WHERE ctt.idTipoTarifa = tct.idTipoTarifa) AS [Tarifa],  " +
                            "    (SELECT descripcion FROM cServicio cs WHERE cs.idServicio = tct.idServicio) AS [Servicio],  " +
                            "    '<input ' + " +
                            "    'type=''number'' id=''htxtCantidad' + CONVERT(VARCHAR(10), tct.idConfTarifa) + ''' ' +  " +
                            " (CASE WHEN   " +
                            "    (SELECT COUNT(*) FROM tTarifaReferencia ttr WHERE ttr.idConfTarifa = tct.idConfTarifa AND ttr.idFolioSubReferencia IN (Select tfsr.idFolioSubReferencia FROM tFolioTransitorioSubReferencia tfsr WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + ")) <> 0  " +
                            "    THEN  " +
                            "        'value=''' + (SELECT CONVERT(VARCHAR(6),cantidad) FROM tTarifaReferencia ti WHERE ti.idConfTarifa = tct.idConfTarifa AND ti.idFolioSubReferencia IN  (Select tfsr.idFolioSubReferencia FROM tFolioTransitorioSubReferencia tfsr WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + ")) + ''''  " +
                            "    ELSE  " +
                            "        ''  " +
                            "    END) +  " +
                            "' name=''htxtCantidad' + CONVERT(VARCHAR(10), tct.idConfTarifa) + ''' class=''input-sm form-control'' onchange=''' + " +
                                                                                                                                                "        (CASE WHEN  " +
                                                                                                                                                            "            (SELECT trt.idTipoRangoTarifa FROM tReglaTarifa trt WHERE trt.idConfTarifa = tct.idConfTarifa) = 1 " +
                                                                                                                                                            "         THEN " +
                                                                                                                                                            "            'javascript:fn_CambioCantidad(' + CONVERT(VARCHAR(10), tct.idConfTarifa) + ', event)'  " +
                                                                                                                                                            "         ELSE " +
                                                                                                                                                            "            'javascript:fn_CambioCantidadPorcentaje(' + CONVERT(VARCHAR(10), tct.idConfTarifa) + ', event," + objTarifa.iIdSubReferencia + ")'  " +
                                                                                                                                                            "         END) + '''>' [Cantidad]," +
                            "    '' AS [Subtotal],  " +
                            "    '' AS [IVA],  " +
                            "    '' AS [Total],  " +
                            "    '<div class=''text-center''>' +  " +
                            "    '<span class=''fa fa-check-square ' +  " +
                            "        (CASE WHEN   " +
                            "            (SELECT COUNT(*) FROM tTarifaReferencia ttr WHERE ttr.idConfTarifa = tct.idConfTarifa AND ttr.idFolioSubReferencia IN (Select tfsr.idFolioSubReferencia FROM tFolioTransitorioSubReferencia tfsr WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + ")) < 1 " +
                            "            THEN  " +
                            "                'fa-green-sm'  " +
                            "            ELSE  " +
                            "                'fa-red-sm'  " +
                            "            END) " +
                            "    + '''' +   " +
                            "        'onclick=''' + " +
                            "        (CASE WHEN  " +
                            "            (SELECT trt.idTipoRangoTarifa FROM tReglaTarifa trt WHERE trt.idConfTarifa = tct.idConfTarifa) = 1 " +
                            "         THEN " +
                            "            'javascript:fn_AgregarTarifa'  " +
                            "         ELSE " +
                            "            'javascript:fn_AgregarTarifaReferenciaPorcentaje'  " +
                            "         END) + " +
                            "        '(' + CONVERT(VARCHAR(10), tct.idConfTarifa) + ', event)''></span>'	+  " +
                            "    '</div>' AS [Agregar]  " +
                            "FROM tConfiguracionTarifa tct  " +
                            "WHERE idCliente = (SELECT idClienteContable   " +
                            "                    FROM tSubReferencia   " +
                            "                    WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + ")  " +
                            "        AND  " +
                            "        (SELECT COUNT(*)  " +
                            "        FROM tConfiguracionTarifaAduan tcta  " +
                            "        WHERE tcta.idConfTarifa = tct.idConfTarifa  " +
                            "                AND   " +
                            "                tcta.idAduana = (SELECT idAduana FROM tSubReferencia WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + ")) > 0  " +
                            "        AND   " +
                            "        idTipoMaterial = " + objTarifa.iIdTipoMaterial + " " +
                            "        AND   " +
                            "        idEstatus != 2 " +
                            "        AND " +
                            "        tct.idTipoOperacion = (SELECT idTipoOperacion FROM tSubReferencia WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + ")";
            //Se ejecuta la consulta
            objTarifa.dt_Tarifas = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            // Obtener calculos
            objTarifa.fn_ObtenerCalculoTarifa(objTarifa);
            //Se pone el resultado en bueno
            objTarifa.iResultado = 1;
        }
        catch (Exception ex) //Si hubo un error
        {
            objTarifa.sMensaje = ex.Message;
            objTarifa.iResultado = 0;
        }
    }

    /// <summary>
    /// Función para calcular el costo de una tarifa
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_ObtenerCalculoTarifa(Tarifa objTarifa)
    {
        // Nuevo datatable con los calculos incluidos
        DataTable dt_Resultado = new DataTable();
        dt_Resultado.Columns.AddRange(new DataColumn[]{
            new DataColumn("Material"),
            new DataColumn("Tarifa"),
            new DataColumn("Servicio"),
            new DataColumn("Cantidad"),
            new DataColumn("Subtotal"),
            new DataColumn("IVA"),
            new DataColumn("Total"),
            new DataColumn("Agregar")
        });
        // Recorrer los row del datatable
        foreach (DataRow objRow in objTarifa.dt_Tarifas.Rows)
        {
            // Se obtiene el id de configuracion y la cantidad
            int liIdConfiguracion = int.Parse(objRow[1].ToString());
            int ldCantidad = int.Parse(objRow[0].ToString() == "" ? "-1" : objRow[0].ToString());
            //Se comprueba que en la cantidad tenga algo
            if (ldCantidad != -1)
            {
                // Se asignan al objeto tarifa
                objTarifa.dCantidad = ldCantidad;
                objTarifa.iIdConfTarifa = liIdConfiguracion;
                // Se obtiene el subtotal
                objTarifa.fn_ObtenerSubTotal(objTarifa);
                // Se calcula el iva
                objTarifa.fn_ObtenerIva(objTarifa);
                // Se calcula el total
                objTarifa.fn_ObtenerTotal(objTarifa);
                // Se agrega el nuevo row al datatable
                DataRow objRowAgregar = dt_Resultado.NewRow();
                objRowAgregar["Material"] = objRow[2];
                objRowAgregar["Tarifa"] = objRow[3];
                objRowAgregar["Servicio"] = objRow[4];
                objRowAgregar["Cantidad"] = objRow[5];
                objRowAgregar["Subtotal"] = "<input value='$" + objTarifa.dSubTotal + "' type='text' id='htxtSubtotal" + liIdConfiguracion + "' name='htxtSubtotal" + liIdConfiguracion + "'  class='input-sm form-control' disabled>";
                objRowAgregar["IVA"] = "<input value='$" + objTarifa.dIva + "' type='text' id='htxtIva" + liIdConfiguracion + "' name='htxtIva" + liIdConfiguracion + "'  class='input-sm form-control' disabled>";
                objRowAgregar["Total"] = "<input value='$" + objTarifa.dTotal + "' type='text' id='htxtTotal" + liIdConfiguracion + "' name='htxtTotal" + liIdConfiguracion + "'  class='input-sm form-control' disabled>";
                objRowAgregar["Agregar"] = objRow[9];
                dt_Resultado.Rows.Add(objRowAgregar);
            }
            else
            {
                // Se agrega el nuevo row al datatable
                DataRow objRowAgregar = dt_Resultado.NewRow();
                objRowAgregar["Material"] = objRow[2];
                objRowAgregar["Tarifa"] = objRow[3];
                objRowAgregar["Servicio"] = objRow[4];
                objRowAgregar["Cantidad"] = objRow[5];
                objRowAgregar["Subtotal"] = "<input  type='text' id='htxtSubtotal" + liIdConfiguracion + "' name='htxtSubtotal" + liIdConfiguracion + "'  class='input-sm form-control' disabled>";
                objRowAgregar["IVA"] = "<input  type='text' id='htxtIva" + liIdConfiguracion + "' name='htxtIva" + liIdConfiguracion + "'  class='input-sm form-control' disabled>";
                objRowAgregar["Total"] = "<input  type='text' id='htxtTotal" + liIdConfiguracion + "' name='htxtTotal" + liIdConfiguracion + "'  class='input-sm form-control' disabled>";
                objRowAgregar["Agregar"] = objRow[9];
                dt_Resultado.Rows.Add(objRowAgregar);
            }
        }
        //Asignar el datatable
        objTarifa.dt_Tarifas = dt_Resultado;
    }

    /// <summary>
    /// método para colocar los maximos y minimos en una tabla
    /// </summary>
    /// <param name="objTarifa"></param>
    /// <returns></returns>
    public void fn_ObtenerTablaMaxMin(Tarifa objTarifa)
    {
        try
        {
            //instancia clase utilerias
            Utilerias objUtilerias = new Utilerias();
            //crea un arreglo para las columnas de la tabla
            string[] arrCol = new string[] { "sMin", "sMax", "sVal", "sIva" };
            //inicia una tabla vacia
            objTarifa.dt_MaxMin = new DataTable();
            //crea la estructura de la tabla
            objUtilerias.fn_GeneraDataTable(objTarifa.dt_MaxMin, arrCol);
            //valida que los máximos y minimos no este vacío
            if (objTarifa.sMaxMin != "")
            {
                //crea un arreglo de valores máximos y mínimos
                string[] arrMaximosMinimos = objTarifa.sMaxMin.Split('|');
                //recorre cada indice del arreglo de máximos y mínimos
                foreach (string sMaximoMinimo in arrMaximosMinimos)
                {
                    //crea un arreglo del máximo y minimo
                    string[] arrMaximoMinimo = sMaximoMinimo.Split(',');
                    //agrega un nuevo renglón a la tabla
                    objTarifa.dt_MaxMin.Rows.Add(arrMaximoMinimo[0].ToString().Trim(),
                                                 arrMaximoMinimo[1].ToString().Trim(),
                                                 arrMaximoMinimo[2].ToString().Trim(),
                                                 arrMaximoMinimo[3].ToString().Trim());
                }
            }
        }
        catch (Exception ex)
        {
            //instancia clase utilerias
            Utilerias objUtilerias = new Utilerias();
            //crea un arreglo para las columnas de la tabla
            string[] arrCol = new string[] { "sMin", "sMax", "sVal" };
            //inicia una tabla vacia
            objTarifa.dt_MaxMin = new DataTable();
            //crea la estructura de la tabla
            objUtilerias.fn_GeneraDataTable(objTarifa.dt_MaxMin, arrCol);
        }
    }


    /// <summary>
    /// método para colocar las aduanas seleccionadas
    /// </summary>
    /// <param name="objTarifa"></param>
    /// <returns></returns>
    public void fn_ObtenerTablaAduanas(Tarifa objTarifa)
    {
        try
        {
            //instancia clase utilerias
            Utilerias objUtilerias = new Utilerias();
            //crea un arreglo para las columnas de la tabla
            string[] arrCol = new string[] { "idAduana", "aduana", "denominacion" };
            //inicia una tabla vacia
            objTarifa.dt_Aduanas = new DataTable();
            //crea la estructura de la tabla
            objUtilerias.fn_GeneraDataTable(objTarifa.dt_Aduanas, arrCol);
            //valida que las aduanas no este vacío
            if (objTarifa.iIdAduana != "")
            {
                //crea un arreglo de aduanas
                string[] arrAduanas = objTarifa.iIdAduana.Split(',');
                //recorre cada indice del arreglo de máximos y mínimos
                foreach (string sAduana in arrAduanas)
                {
                    //agrega un nuevo renglón a la tabla
                    objTarifa.dt_Aduanas.Rows.Add(sAduana.ToString().Trim(), "", "");
                }
            }
        }
        catch (Exception ex)
        {
            //instancia clase utilerias
            Utilerias objUtilerias = new Utilerias();
            //crea un arreglo para las columnas de la tabla
            string[] arrCol = new string[] { "sAduana" };
            //inicia una tabla vacia
            objTarifa.dt_Aduanas = new DataTable();
            //crea la estructura de la tabla
            objUtilerias.fn_GeneraDataTable(objTarifa.dt_Aduanas, arrCol);
        }
    }

    /// <summary>
    /// Método para obtener la configuración de tarifa
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_ObtenerDatosTarifa(Tarifa objTarifa)
    {
        //clase para desencriptar
        Security secIdTarifa = new Security(sIdTarifa);
        //desencripta idTariga
        objTarifa.iIdTarifa = int.Parse(secIdTarifa.DesEncriptar());
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "iIdCliente", "iIdAduana", "iIdTipoOperacion", "iIdTipoMaterial", "iIdTipoTarifa",
                                  "iIdServicio", "iIdMoneda", "iIdTipoRegimen", "iIdTipoDocumento", "iIdTipoRegla",
                                  "sMonto", "iIva","iRetencion","sCobroMinimo", "sIdTipoRegla","iIdFormula","iIdTipoMontoPorcentaje",
                                  "sMaxMin","sIdBaseCalculo"};
        //Se crea la consulta
        string sQuery = "select tct.idCliente iIdCliente," +
                        " isnull(STUFF(" +
                        " (SELECT CAST('|' AS varchar(MAX)) +  convert(varchar,idAduana)" +
                        " FROM tConfiguracionTarifaAduan trt2" +
                        " where trt2.idConfTarifa = tct.idConfTarifa" +
                        " ORDER BY convert(varchar,idAduana)" +
                        " FOR XML PATH('')" +
                        " ), 1, 1, ''),'') iIdAduana," +
                        " tct.idTipoOperacion iIdTipoOperacion," +
                        " tct.idTipoMaterial iIdTipoMaterial,tct.idTipoTarifa iIdTipoTarifa, tct.idServicio iIdServicio," +
                        " tct.idMoneda iIdMoneda, trt.idRegimen iIdTipoRegimen,trt.idClaveDocumento iIdTipoDocumento," +
                        " trt.idTipoRegla iIdTipoRegla, trt.valor sMonto,ci.cveIva iIva,cr.cveRetencion iRetencion,trt.cobroMinimo sCobroMinimo," +
                        " ctrt.nombre sIdTipoRegla, trt.idFormula iIdFormula, trt.idTipoRangoTarifa iIdTipoMontoPorcentaje," +
                        " isnull(STUFF(" +
                        " (SELECT CAST('|' AS varchar(MAX)) +  CAST(CAST(valorMinimo AS DECIMAL(38,18)) as VARCHAR(40))+','+CAST(CAST(valorMaximo AS DECIMAL(38,18)) as VARCHAR(40))+','+convert(varchar,valor)+','+convert(varchar,iva)" +
                        " FROM trangotarifa trt2" +
                        " where trt2.idReglaTarifa = trt.idReglaTarifa" +
                        " ORDER BY valorMinimo" +
                        " FOR XML PATH('')" +
                        " ), 1, 1, ''),'') sMaxMin," +
                        " (select formula from tformulatarifa tft where tft.idFormula = trt.idFormula) sIdBaseCalculo" +
                        " from tConfiguracionTarifa tct " +
                        " join tReglaTarifa trt on trt.idConfTarifa = tct.idConfTarifa" +
                        " join cTipoReglaTarifa ctrt on ctrt.idTipoRegla = trt.idTipoRegla" +
                        " join cIva ci on ci.idIva =  trt.idIva" +
                        " join cRetencion cr on cr.idRetencion = trt.idRetencion" +
                        " where tct.idConfTarifa = " + objTarifa.iIdTarifa + "";
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<Tarifa>(sQuery, arrAtributos, objTarifa);
        // Se asigna el monto porcentaje a iIdTipoRangoTarifa
        objTarifa.iIdTipoRangoTarifa = objTarifa.iIdTipoMontoPorcentaje;
        //Se asigna el resultado
        objTarifa.iResultado = 1;
    }

    /// <summary>
    /// Función para generar boton de guardar cuando se tarifica
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_GenerarBotonGuardar(Tarifa objTarifa)
    {
        // Instancia conexion
        Conexion objConexion = new Conexion();

        // Se crea la consulta
        string sQuery = "IF NOT EXISTS(SELECT * " +
                        "            FROM tTarifaReferencia ttr " +
                        "                 JOIN " +
                        "                 tFolioTransitorioSubReferencia tftsr " +
                        "                 ON ttr.idFolioSubReferencia = tftsr.idFolioSubReferencia " +
                        "                 JOIN  " +
                        "                 tSubReferenciaAdicional tsra " +
                        "                 ON tftsr.idSubReferencia = tsra.idSubReferencia " +
                        "            WHERE tsra.idTipoMaterial IN (SELECT tct.idTipoMaterial   " +
                        "					                      FROM tConfiguracionTarifa tct " +
                        "					                      WHERE tct.idConfTarifa = ttr.idConfTarifa) " +
                        "                  AND tftsr.idSubReferencia = " + objTarifa.iIdSubReferencia + " " +
                        "  ) OR NOT EXISTS (SELECT * " +
                        "                    FROM tFolioTransitorioSubReferencia ti " +
                        "                         JOIN " +
                        "                         tFolioTransitorio tfti " +
                        "                         ON ti.idFolioTransitorio = tfti.idFolioTransitorio " +
                        "                    WHERE tfti.idEstatusFT = (SELECT tie.idEstatusFT " +
                        "                                         FROM cEstatusFolioTransitorio tie " +
                        "                                         WHERE nombre = 'Proceso tarificación') " +
                        "                    AND ti.idFolioTransitorio = (SELECT TOP 1 tfi.idFolioTransitorio " +
                        "                                                 FROM tFolioTransitorioSubReferencia tfi " +
                        "                                                 WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + " ) " +
                        "                   ) " +
                        "  BEGIN " +
                        "    SELECT 'disabled' " +
                        "  END";
        // Se ejecuta la consulta
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Se verifoca que se haya realizado correctamente la consulta
        if (arrResultado[0] == "1")
        {
            // Asigno el resultado a una variable
            objTarifa.sMensaje = "<div class='row'>" +
                                 "  <div class='col-lg-11 text-right'>" +
                                 "      <button onclick='javascript:fn_TarificarSubReferencia();' type='button' id='hbtnGuardarTarifa' class='btn btn-greenS btn-sm input-sm' " + arrResultado[1] +
                                 "      ><span class='glyphicon glyphicon-ok'></span>&nbsp;Tarificar</button>" +
                                 "  </div>" +
                                 "</div>";
            objTarifa.iResultado = 1;
        }
    }

    /// <summary>
    /// Función para agregar una tarifa a una subreferencia
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_AgregarTarifaReferencia(Tarifa objTarifa)
    {
        //instancia Conexión
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_AgregarTarifaReferencia", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //pasar parametros al sp
                objConexion.agregarParametroSP("@sIdConfTarifa", SqlDbType.Int, objTarifa.iIdConfTarifa.ToString());
                objConexion.agregarParametroSP("@sIdSubReferencia", SqlDbType.Int, objTarifa.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@dCantidad", SqlDbType.Int, objTarifa.dCantidad.ToString());
                objConexion.agregarParametroSP("@dSubtotal", SqlDbType.Float, objTarifa.dSubTotal.ToString());
                objConexion.agregarParametroSP("@dIva", SqlDbType.Float, objTarifa.dIva.ToString());
                objConexion.agregarParametroSP("@dRetencion", SqlDbType.Float, objTarifa.dRetencion.ToString());
                objConexion.agregarParametroSP("@dTotal", SqlDbType.Float, objTarifa.dTotal.ToString());
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@sResOut");

                int iMostrarBoton = 0;

                if (sResOut[0] == "1" && int.TryParse(sResOut[1], out iMostrarBoton))
                {
                    //Se retorna el mensaje de éxito
                    objTarifa.iResultado = 1;
                    objTarifa.sMensaje = "Tarifa asignada con éxito.";

                    // Se comprueba si se debe de activar el botón de Guardar
                    objTarifa.bMostrarBoton = iMostrarBoton == 1;

                    // Se verifica si dio error por el tipo de moneda
                    if (sResOut[1] == "10")
                    {
                        objTarifa.iResultado = 0;
                        objTarifa.sMensaje = "La tarifa no es del mismo tipo de moneda que las otras configuradas para la subreferencia";
                    }
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTarifa.iResultado = 0;
                    objTarifa.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //mensaje de error al caer en una excepción
                objTarifa.iResultado = 0;
                objTarifa.sMensaje = "Error con su base de datos, " + ex.Message.ToString();
            }
        }
        else
        {
            //mensaje de error con la base de datos
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Error con su base de datos, no existe procedimiento almacenado";
        }
    }

    public void fn_RecalcularComision(Tarifa objTarifa)
    {
        try
        {
            Conexion objConexion = new Conexion();
            string sQuery = @"DELETE FROM tComisionFolioTransitorio WHERE idFolioTransitorio=" + objTarifa.iIdFolioTransitorio;
            objConexion.ejecutarComando(sQuery);
            objTarifa.iResultado = 1;
            objTarifa.sMensaje = "Tarifa recalculada con éxito.";
        }
        catch (Exception ex)
        {
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = ex.Message;
        }
    }

    /// <summary>
    /// Función para generar la tabla de tarifas segun la subreferencia
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_GenerarTablaTarifas(Tarifa objTarifa)
    {
        // Instancia conexion 
        Conexion objConexion = new Conexion();
        // Query
        string sQuery = "SELECT tsr.refOperativa [Subreferencia], " +
                        "       ctm.nombre + ' - ' + cs.descripcion [Tarifa], " +
                        "       cto.tipoOperacion [Operacion]," +
                        "       ttr.cantidad [Cantidad], " +
                        "       ttr.idTarifaReferencia [IdTarifa], " +
                        "       ttr.idConfTarifa [idConftarifa], " +
                        "       '<div class=''text-center''><span class=''fa fa-trash fa-red-sm'' " +
                        "        onclick=''fn_ConfirmarEliminarTarifa(' + CONVERT(VARCHAR(19),ttr.idTarifaReferencia) + ', event)''></span></div>' [Controles]" +
                        "FROM  " +
                        "    tTarifaReferencia ttr " +
                        "    JOIN " +
                        "    tFolioTransitorioSubReferencia tftsr " +
                        "    ON " +
                        "    ttr.idFolioSubReferencia = tftsr.idFolioSubReferencia " +
                        "    JOIN " +
                        "    tSubReferencia tsr " +
                        "    ON tftsr.idSubReferencia = tsr.idSubReferencia " +
                        "    JOIN " +
                        "    tConfiguracionTarifa tct " +
                        "    ON ttr.idConfTarifa = tct.idConfTarifa " +
                        "    JOIN " +
                        "    cTipoOperacion cto " +
                        "    ON tct.idTipoOperacion = cto.idTipoOperacion " +
                        "    JOIN " +
                        "    cServicio cs " +
                        "    ON tct.idServicio = cs.idServicio " +
                        "    JOIN " +
                        "    cTipoMaterial ctm " +
                        "    ON tct.idTipoMaterial = ctm.idTipoMaterial " +
                        "WHERE tsr.idSubReferencia = " + objTarifa.iIdSubReferencia;
        // Ejecutamos la query
        objTarifa.dt_Tarifas = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
        // Realizar calculos
        objTarifa.fn_CalcularCostosData(objTarifa);
    }

    /// <summary>
    /// Función para generar la tabla de tarifas segun la subreferencia
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_GenerarTablaTarifasFlat(Tarifa objTarifa)
    {
        // Instancia conexion 
        Conexion objConexion = new Conexion();
        // Query
        string sQuery = "SELECT " +
                       "    idServicio AS Servicio, " +
                       "    subtotal AS Subtotal, " +
                       "    ISNULL(iva, 0) AS IVA, " +
                       "    total AS Total, " +
                       "    '' AS Nueva, " +
                       "    '' AS Eliminar, " +
                       "    idTarifaFlat AS idTarifa " +
                       "FROM tTarifasFlat tf " +
                       "WHERE idSubreferencia = " + objTarifa.iIdSubReferencia;
        // Ejecutamos la query
        objTarifa.dt_Tarifas = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
        // Iniciamos contador en 0
        int iIndex = 0;
        // Acomodamos la datatable para luego obtener el html en una nueva datatable
        DataTable dtCloned = dt_Tarifas.Clone();
        dtCloned.Columns[0].DataType = typeof(string);
        dtCloned.Columns[1].DataType = typeof(string);
        dtCloned.Columns[2].DataType = typeof(string);
        dtCloned.Columns[3].DataType = typeof(string);
        dtCloned.Columns[4].DataType = typeof(string);
        dtCloned.Columns[5].DataType = typeof(string);
        dtCloned.Columns[6].DataType = typeof(string);
        // Recorrer datatable original
        foreach (DataRow objRow in dt_Tarifas.Rows)
        {
            // Aumentamos contador
            iIndex++;
            // Query para crear el select de servicio
            sQuery = "SELECT " +
                     "      idServicio, " +
                     "      cveSunServicio + '-' + descripcion " +
                     "  FROM cServicio " +
                     "  WHERE idServicio = " + objRow[0] + " " + " " +
                     "  UNION " +
                     "  SELECT " +
                     "      idServicio, " +
                     "      cveSunServicio + '-' + descripcion " +
                     "  FROM cServicio " +
                     "  WHERE idServicio != " + objRow[0] + " " + " AND cveSunServicio LIKE 'NCO%'  ";
            // Obtener combo
            Utilerias objUtilerias = new Utilerias();
            objUtilerias.sQuery = sQuery;
            objUtilerias.sNombre = "hsclFlat" + iIndex;
            objUtilerias.fn_GeneraCombobox(objUtilerias);
            // Clonar row
            dtCloned.ImportRow(objRow);
            // Asignar combo 
            dtCloned.Rows[dtCloned.Rows.Count - 1][0] = objUtilerias.sContenido;
            // Agregar texto a inputs
            dtCloned.Rows[dtCloned.Rows.Count - 1][1] = "<input type='number' id='htxtCantidadFlat" + iIndex + "' value='" + (decimal.Parse(objRow[0].ToString())).ToString("0.####") + "' name='htxtCantidadFlat" + iIndex + "' class='input-sm form-control'>";
            dtCloned.Rows[dtCloned.Rows.Count - 1][2] = "<input type='number' id='htxtSubtotalFlat" + iIndex + "' value='" + (decimal.Parse(objRow[1].ToString())).ToString("0.####") + "' name='htxtSubtotalFlat" + iIndex + "' class='input-sm form-control' onchange='javascript:fn_Recalcular(event)'>";
            dtCloned.Rows[dtCloned.Rows.Count - 1][3] = "<select id='hslcIVAFlat" + iIndex + "' name='hslcIVAFlat" + iIndex + "' data-width='100%' data-live-search='true' class='selectpicker' tabindex='-98' onchange='javascript:fn_Recalcular(event)' disabled>" +
                                                            "<option value='0'>0%</option>" +
                                                            "<option value='8'>8%</option>" +
                                                            "<option value='16'>16%</option>" +
                                                        "</select>";
            dtCloned.Rows[dtCloned.Rows.Count - 1][4] = "<input type='text' id='htxtTotalFlat" + iIndex + "' value='" + (decimal.Parse(objRow[3].ToString())).ToString("0.####") + "' name='htxtTotalFlat" + iIndex + "' class='input-sm form-control' disabled>";
            dtCloned.Rows[dtCloned.Rows.Count - 1][5] = "<span class='glyphicon glyphicon-floppy-disk fa-green-sm' onclick='javascript:fn_GuardarTarifaEditarFlat(event," + objRow[6].ToString() + ");'></span>";
            dtCloned.Rows[dtCloned.Rows.Count - 1][6] = "<span class='fa fa-minus-circle fa-red-sm center icon-delete' onclick='javascript:fn_EliminarTarifaFlat(event," + objRow[6].ToString() + ");'></span>";
        }
        // Asignar nueva datatable a la original
        objTarifa.dt_Tarifas = dtCloned;
    }

    /// <summary>
    /// Función para obtener el costo que va a tener la tarifa
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_CalcularCostosData(Tarifa objTarifa)
    {
        // Nuevo datatable con los calculos incluidos
        DataTable dt_Resultado = new DataTable();
        dt_Resultado.Columns.AddRange(new DataColumn[]{
            new DataColumn("Referencia administrativa"),
            new DataColumn("Tarifa"),
            new DataColumn("Operacion"),
            new DataColumn("Cantidad"),
            new DataColumn("Subtotal"),
            new DataColumn("IVA"),
            new DataColumn("Total"),
            new DataColumn("Controles")
        });
        // Recorrer los row del datatable
        foreach (DataRow objRow in objTarifa.dt_Tarifas.Rows)
        {
            // Se obtiene el id de configuracion y la cantidad
            int liIdConfiguracion = int.Parse(objRow[5].ToString());
            int ldCantidad = int.Parse(objRow[3].ToString());
            // Se asignan al objeto tarifa
            objTarifa.dCantidad = ldCantidad;
            objTarifa.iIdConfTarifa = liIdConfiguracion;
            // Se obtiene el subtotal
            objTarifa.fn_ObtenerSubTotal(objTarifa);
            // Se calcula el iva
            objTarifa.fn_ObtenerIva(objTarifa);
            // Se calcula el total
            objTarifa.fn_ObtenerTotal(objTarifa);
            // Se agrega el nuevo row al datatable
            DataRow objRowAgregar = dt_Resultado.NewRow();
            objRowAgregar["Referencia administrativa"] = objRow[0];
            objRowAgregar["Tarifa"] = objRow[1];
            objRowAgregar["Operacion"] = objRow[2];
            objRowAgregar["Cantidad"] = objRow[3];
            objRowAgregar["Subtotal"] = "$" + objTarifa.dSubTotal;
            objRowAgregar["IVA"] = "$" + objTarifa.dIva;
            objRowAgregar["Total"] = "$" + objTarifa.dTotal;
            objRowAgregar["Controles"] = objRow[6];
            dt_Resultado.Rows.Add(objRowAgregar);
            // Limpiar objeto
            objTarifa.dSubTotal = 0;
        }
        //Asignar el datatable
        objTarifa.dt_Tarifas = dt_Resultado;
    }

    /// <summary>
    /// Función para quitar una tarifa a una subreferencia
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_QuitarTarifaReferenciaId(Tarifa objTarifa)
    {
        //instancia Conexión
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_QuitarTarifaReferenciaId", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //pasar parametros al sp
                objConexion.agregarParametroSP("@iIdTarifaReferencia", SqlDbType.Int, objTarifa.iIdTarifaReferencia.ToString());
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@sResOut");
                int iMostrarBoton = 0;

                if (sResOut[0] == "1" && int.TryParse(sResOut[1], out iMostrarBoton))
                {
                    //Se retorna el mensaje de éxito
                    objTarifa.iResultado = 1;
                    objTarifa.sMensaje = "Tarifa quitada con éxito.";

                    // Se comprueba si se debe de activar el botón de Guardar
                    objTarifa.bMostrarBoton = iMostrarBoton == 1;
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTarifa.iResultado = 0;
                    objTarifa.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //mensaje de error al caer en una excepción
                objTarifa.iResultado = 0;
                objTarifa.sMensaje = "Error con su base de datos, " + ex.Message.ToString();
            }
        }
        else
        {
            //mensaje de error con la base de datos
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Error con su base de datos, no existe procedimiento almacenado";
        }
    }

    /// <summary>
    /// Función para quitar una tarifa a una subreferencia
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_QuitarTarifaReferencia(Tarifa objTarifa)
    {
        //Verifica si tiene la tarifa asignada
        if (objTarifa.iIdTarifaReferencia != 0)
        {
            // Metodo para quitar tarifa por id
            fn_QuitarTarifaReferenciaId(objTarifa);
            // Se retorna
            return;
        }
        //instancia Conexión
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_QuitarTarifaReferencia", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //pasar parametros al sp
                objConexion.agregarParametroSP("@sIdConfTarifa", SqlDbType.Int, objTarifa.iIdConfTarifa.ToString());
                objConexion.agregarParametroSP("@sIdSubReferencia", SqlDbType.Int, objTarifa.iIdSubReferencia.ToString());
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@sResOut");

                int iMostrarBoton = 0;

                if (sResOut[0] == "1" && int.TryParse(sResOut[1], out iMostrarBoton))
                {
                    //Se retorna el mensaje de éxito
                    objTarifa.iResultado = 1;
                    objTarifa.sMensaje = "Tarifa quitada con éxito.";

                    // Se comprueba si se debe de activar el botón de Guardar
                    objTarifa.bMostrarBoton = iMostrarBoton == 1;
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTarifa.iResultado = 0;
                    objTarifa.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //mensaje de error al caer en una excepción
                objTarifa.iResultado = 0;
                objTarifa.sMensaje = "Error con su base de datos, " + ex.Message.ToString();
            }
        }
        else
        {
            //mensaje de error con la base de datos
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Error con su base de datos, no existe procedimiento almacenado";
        }
    }

    /// <summary>
    /// Método para asignar una tarifa a una referencia
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_AsignarTarifaReferencia(Tarifa objTarifa)
    {
        //instancia Conexión
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_AsignarTarifaReferencia", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //pasar parametros al sp
                objConexion.agregarParametroSP("@sIdTarifa", SqlDbType.VarChar, objTarifa.sIdTarifa.ToString());
                objConexion.agregarParametroSP("@sIdSubReferencia", SqlDbType.VarChar, objTarifa.sIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@sFolioTransitorio", SqlDbType.VarChar, objTarifa.sFolioTransitorio.ToString());
                objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objTarifa.iIdAduana.ToString());
                objConexion.agregarParametroSP("@iIdCliente", SqlDbType.Int, objTarifa.iIdCliente.ToString());
                objConexion.agregarParametroSP("@idTipoFactura", SqlDbType.Int, objTarifa.iIdTipoFactura.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@sResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objTarifa.iResultado = 1;
                    objTarifa.sMensaje = "Tarifa asignada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTarifa.iResultado = 0;
                    objTarifa.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //mensaje de error al caer en una excepción
                objTarifa.iResultado = 0;
                objTarifa.sMensaje = "Error con su base de datos, " + ex.Message.ToString();
            }
        }
        else
        {
            //mensaje de error con la base de datos
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Error con su base de datos, no existe procedimiento almacenado";
        }
    }

    /// <summary>
    /// Método para modificar la asignación de tarifa a una referencia
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_ModificarTarifaReferencia(Tarifa objTarifa)
    {
        //instancia Conexión
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_ModificarTarifaReferencia", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //pasar parametros al sp
                objConexion.agregarParametroSP("@aIdConfTarifa", SqlDbType.VarChar, objTarifa.sIdTarifa.ToString());
                objConexion.agregarParametroSP("@iIdTarifaReferencia", SqlDbType.VarChar, objTarifa.iIdTarifaReferencia.ToString());
                objConexion.agregarParametroSP("@iAccion", SqlDbType.VarChar, objTarifa.iAccion.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@sResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objTarifa.iResultado = 1;
                    objTarifa.sMensaje = "Tarifa " + (objTarifa.iAccion == 1 ? "actualizada" : "eliminada") + " con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTarifa.iResultado = 0;
                    objTarifa.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //mensaje de error al caer en una excepción
                objTarifa.iResultado = 0;
                objTarifa.sMensaje = "Error con su base de datos, " + ex.Message.ToString();
            }
        }
        else
        {
            //mensaje de error con la base de datos
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Error con su base de datos, no existe procedimiento almacenado";
        }
    }

    /// <summary>
    /// Método para validar que el folio transitorio no exista
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_ValidarFolioTransitorio(Tarifa objTarifa)
    {
        try
        {
            //instancia conexión
            Conexion objConexion = new Conexion();
            //variable squery
            string sQuery = "select COUNT(*) from tTarifaReferencia where folioTransitorio = '" + objTarifa.sFolioTransitorio + "'";
            //Consulta los folios
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            //valida que la consulta se hiciera correctamente
            if (sRes[0] == "1")
            {
                if (Convert.ToInt16(sRes[1].ToString()) == 0)
                {
                    objTarifa.iResultado = 1;
                    objTarifa.sMensaje = "No existe el folio transitorio";
                }
                else
                {
                    objTarifa.iResultado = 0;
                    objTarifa.sMensaje = "Ya existe el folio transitorio: " + objTarifa.sFolioTransitorio;
                }
            }
            else
            {
                objTarifa.iResultado = 0;
                objTarifa.sMensaje = sRes[0].ToString();
            }
        }
        catch (Exception ex)
        {
            //atrapa la excepcion
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = ex.Message.ToString();
        }
    }

    /// <summary>
    /// Metodo para eliminar la tarifa de la lista
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_EliminarTarifa(Tarifa objTarifa)
    {
        //Se instancia la clase conexion
        Conexion objConecion = new Conexion();
        //Se crea la consulta
        string sQuery = "UPDATE tConfiguracionTarifa SET idEstatus=2 WHERE idConfTarifa=CONVERT(INT,dbo.fn_DecodeB64('" + objTarifa.sIdTarifa + "'))";
        //Se ejecuta la consulta
        string sRes = objConecion.ejecutarComando(sQuery);
        //Se valida el resultado
        if (sRes == "1")
        {
            //Se retorna el sResultado 
            objTarifa.iResultado = 1;
            //Se retorna mensaje de éxito
            objTarifa.sMensaje = "Tarifa eliminada con éxito.";
        }
        else
        {
            //Se retorna el sResultado 
            objTarifa.iResultado = 0;
            //Se retorna mensaje de error
            objTarifa.sMensaje = sRes;
        }
    }

    /// <summary>
    /// Función usada para instanciar el comportamiento para el cobro
    /// </summary>
    /// <param name="objTarifa"></param>
    private void fn_InstanciarComportamientoBehavior(Tarifa objTarifa)
    {
        // Se obtiene el tipo de regla
        Conexion objConexion = new Conexion();
        // Query para obtener el tipo de regla
        string sQuery = "SELECT (CASE WHEN  ctrt.nombre = 'Rango' " +
                        "THEN      " +
                        "    CASE WHEN trt.idTipoRangoTarifa IS NULL OR trt.idTipoRangoTarifa = 1      " +
                        "    THEN " +
                        "         '3'   " +
                        "    ELSE      " +
                        "       CASE WHEN trt.idTipoRangoTarifa = 3 " +
                        "       THEN " +
                        "           '5' " +
                        "       ELSE " +
                        "         '4'    " +
                        "       END " +
                        "    END   " +
                        "ELSE CASE WHEN  ctrt.nombre = 'Fijo'       " +
                        "THEN           " +
                        "    CASE WHEN trt.idTipoRangoTarifa IS NULL  OR trt.idTipoRangoTarifa = 1         " +
                        "    THEN              " +
                        "        '1'          " +
                        "    ELSE              " +
                        "        '2'          " +
                        "    END           " +
                        " END " +
                        " END) AS [Algo] FROM tReglaTarifa trt      " +
                        " JOIN      cTipoReglaTarifa ctrt       " +
                        " ON trt.idTipoRegla = ctrt.idTipoRegla  " +
                        " WHERE  trt.idConfTarifa = " + objTarifa.iIdConfTarifa;
        // Se obtiene el tipo de regla
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Se verifica que todo halla ido bien
        if (arrResultado[0] == "1")
        {
            objTarifa.iResultado = 1;
            // Switch por tipo de regla
            switch (arrResultado[1])
            {
                case "1": // Monto simple
                    objTarifa.objCobroBehavior = new CobroMontoSimpleBehavior();
                    break;
                case "2": // Porciento simple
                    objTarifa.objCobroBehavior = new CobroPorcientoSimpleBehavior();
                    break;
                case "3": // Monto rangos
                    objTarifa.objCobroBehavior = new CobroMontoRangoBehavior();
                    break;
                case "4": // Porciento rangos
                    objTarifa.objCobroBehavior = new CobroPorcientoRangoBehavior();
                    break;
                case "5": // Monto - porciento
                    objTarifa.objCobroBehavior = new CobroMontoPorcientoBehavior();
                    break;
                default:
                    //Se instancia un cobro simple como default
                    objTarifa.objCobroBehavior = new CobroMontoSimpleBehavior();
                    //Se retorna el sResultado 
                    objTarifa.iResultado = 0;
                    //Se retorna mensaje de error
                    objTarifa.sMensaje = "Error al obtener tipo de regla";
                    break;
            }
        }
        else // Error
        {
            //Se retorna el sResultado 
            objTarifa.iResultado = 0;
            //Se retorna mensaje de error
            objTarifa.sMensaje = "Error al obtener tipo de regla";
        }
    }

    public void fn_ObtenerCantidad(Tarifa objTarifa)
    {

    }

    /// <summary>
    /// Función para obtener el subtotal
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_ObtenerSubTotal(Tarifa objTarifa)
    {
        //objTarifa.fn_ObtenerCantidad(objTarifa);
        objTarifa.fn_InstanciarComportamientoBehavior(objTarifa);
        objCobroBehavior.fn_ObtenerSubTotal(objTarifa);
        //objTarifa.dCantidad = objTarifa.iCantidadColocada;
    }

    /// <summary>
    /// Función para obtener el iva a pagar
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_ObtenerIva(Tarifa objTarifa)
    {
        // Instanciar conexion
        Conexion objConexion = new Conexion();
        // Query para obtener el iva
        string sQuery = "SELECT ci.iva " +
                        "FROM tReglaTarifa trt " +
                        "     JOIN " +
                        "     cIva ci " +
                        "     ON trt.idIva = ci.idIva " +
                        "WHERE trt.idConfTarifa = " + objTarifa.iIdConfTarifa;
        // Se obtiene el resultado
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Se verifica que se halla realizado correctamente
        if (arrResultado[0] == "1")
        {
            objTarifa.dIva = decimal.Parse(arrResultado[1]);
            //Se realiza calculo del iva
            objTarifa.dIva = objTarifa.dSubTotal * objTarifa.dIva / 100;
            objTarifa.iResultado = 1;
        }
        else // Error
        {
            //Se retorna el sResultado 
            objTarifa.iResultado = 0;
            //Se retorna mensaje de error
            objTarifa.sMensaje = "Error al obtener el iva";
        }
    }

    /// <summary>
    /// Función para obtener la retención a pagar
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_ObtenerRetencion(Tarifa objTarifa)
    {
        // Instanciar conexion
        Conexion objConexion = new Conexion();
        // Query para obtener el iva
        string sQuery = "SELECT cr.retencion " +
                        "FROM tReglaTarifa trt " +
                        "     JOIN " +
                        "     cRetencion cr " +
                        "     ON trt.idRetencion = cr.idRetencion " +
                        "WHERE trt.idConfTarifa = " + objTarifa.iIdConfTarifa;
        // Se obtiene el resultado
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Se verifica que se halla realizado correctamente
        if (arrResultado[0] == "1")
        {
            objTarifa.dRetencion = decimal.Parse(arrResultado[1]);
            //Se realiza calculo del iva
            objTarifa.dRetencion = objTarifa.dSubTotal * objTarifa.dRetencion / 100;
            objTarifa.iResultado = 1;
        }
        else // Error
        {
            //Se retorna el sResultado 
            objTarifa.iResultado = 0;
            //Se retorna mensaje de error
            objTarifa.sMensaje = "Error al obtener la retencion";
        }
    }

    /// <summary>
    /// Función usada para obtener el total
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_ObtenerTotal(Tarifa objTarifa)
    {
        objTarifa.dTotal = (objTarifa.dSubTotal + objTarifa.dIva) - objTarifa.dRetencion;
    }

    public void fn_ObtenerMonto(Tarifa objTarifa)
    {
        if (objTarifa.bMostrarBoton)
        {
            objTarifa.dMonto = objTarifa.dSubTotal / objTarifa.iCantidadColocada;
        }
        else
        {
            objTarifa.dMonto = objTarifa.dSubTotal / objTarifa.dCantidad;
        }
    }

    public void fn_ComprobarInformacionTarificar(Tarifa objTarifa)
    {
        // Pone resultado como 1 
        objTarifa.iResultado = 1;
        // Instancia conexion a la base de datos
        Conexion objConexion = new Conexion();
        // Query para obtener los campos necesarios
        string sQuery = "SELECT cct.nombre " +
                        "FROM " +
                        "    cCalculoTarifa cct " +
                        "    JOIN " +
                        "    dbo.fn_Split((SELECT tft.formula " +
                        "    FROM tConfiguracionTarifa tct " +
                        "         JOIN  " +
                        "         tReglaTarifa trt " +
                        "         ON tct.idConfTarifa = trt.idConfTarifa " +
                        "         JOIN " +
                        "         tFormulaTarifa tft " +
                        "         ON trt.idFormula = tft.idFormula " +
                        "    WHERE tct.idConfTarifa = " + objTarifa.iIdConfTarifa + "), ',') AS formula " +
                        "    ON cct.idCarlculoTarifa = formula.value";
        // Obtener lista de campos
        List<string> lstCampos = objConexion.ejecutarConsultaRegistroMultiples(sQuery);
        // Comprueba resultado
        if (lstCampos[0] == "1")
        {
            // Verifica que halla traido información
            if (lstCampos.Count <= 1)
            {
                // Pone mensaje de error
                objTarifa.sMensaje = "Base de calculo no configurada";
                // Pone resultado de error
                objTarifa.iResultado = 0;
                // Termina el proceso
                return;
            }
            // Obtener la moneda de la tarifa
            sQuery = "SELECT idMoneda " +
                      "  FROM tConfiguracionTarifa " +
                      "  WHERE idConfTarifa = " + objTarifa.iIdConfTarifa;
            // Obtener moneda
            objConexion.ejecutaRecuperaObjeto<Tarifa>(sQuery, new string[] { "iIdMoneda" }, objTarifa);
            // Ciclo para recorrer los campos
            for (int i = 1; i < lstCampos.Count; i++)
            {
                // Verificar que campo es
                switch (lstCampos[i].Trim())
                {
                    case "Valor aduana":
                        objTarifa.fn_ComprobarValorAduana(objTarifa);
                        break;
                    case "Impuestos pagados":
                        objTarifa.fn_ComprobarImpuestosPagados(objTarifa);
                        break;
                    case "Impuestos afianzados":
                        objTarifa.fn_ComprobarImpuestosAfianzados(objTarifa);
                        break;
                    case "Impuestos subsidiados":
                        objTarifa.fn_ComprobarImpuestosSubsidiados(objTarifa);
                        break;
                    case "Gastos comprobados":
                        objTarifa.fn_ComprobarGastosComprobados(objTarifa);
                        break;
                    case "Impuestos financiados por NAD":
                        objTarifa.fn_ComprobarImpuestosFinanciadosPorNAD(objTarifa);
                        break;
                    case "Peso bruto":
                        objTarifa.fn_ComprobarPesoBruto(objTarifa);
                        break;
                }
            }
        }
        objTarifa.fn_CalcularAnticipos(objTarifa);
    }

    private void fn_CalcularAnticipos(Tarifa objTarifa)
    {
        // Instanciar conexion
        Conexion objConexion = new Conexion();
        // Query
        string sQuery = @"SELECT ISNULL(SUM(monto),0)
                          FROM tAnticipo
                          WHERE idSubreferencia= " + objTarifa.iIdSubReferencia;
        // Ejecutar la query
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Comprueba que no haya habido error
        if (arrResultado[0] == "1")
        {
            if (arrResultado[1] == "0" || String.IsNullOrEmpty(arrResultado[1]))
            {
                //Se retorna el sResultado en caso de error
                objTarifa.iResultado = 0;
                //Se retorna mensaje de error
                objTarifa.sMensaje += "Anticipos,";
            }
            else
            {
                // Guardar valor
                objTarifa.dAnticipos = decimal.Parse(arrResultado[1]);
            }
        }
        else
        {
            //Se retorna el sResultado 
            objTarifa.iResultado = 0;
            //Se retorna mensaje de error
            objTarifa.sMensaje = "Error al comprobar los anticipos";
        }
    }

    private void fn_ComprobarValorAduana(Tarifa objTarifa)
    {
        // Instanciar conexion
        Conexion objConexion = new Conexion();
        // Query
        string sQuery = "SELECT ISNULL(tp.valorAduana,0) " +
                        "FROM tPedimento tp " +
                        "WHERE CONVERT(VARCHAR, tp.idAduana) + CONVERT(VARCHAR, tp.idPatente) + CONVERT(VARCHAR, tp.pedimento) = (SELECT CONVERT(VARCHAR, ts.idAduana) + CONVERT(VARCHAR, ts.idPatente) + CONVERT(VARCHAR, ts.pedimento) FROM tSubReferencia ts WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + ")";
        // Ejecutar la query
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Comprueba que no haya habido error
        if (arrResultado[0] == "1")
        {
            if (arrResultado[1] == "0" || String.IsNullOrEmpty(arrResultado[1]))
            {
                //Se retorna el sResultado en caso de error
                objTarifa.iResultado = 0;
                //Se retorna mensaje de error
                objTarifa.sMensaje += "Valor aduana,";
            }
            else
            {
                // Guardar valor
                objTarifa.dValorAduana = decimal.Parse(arrResultado[1]);
            }
        }
        else
        {
            //Se retorna el sResultado 
            objTarifa.iResultado = 0;
            //Se retorna mensaje de error
            objTarifa.sMensaje = "Error al comprobar valor aduana";
        }
    }

    private void fn_ComprobarImpuestosPagados(Tarifa objTarifa)
    {
        // Instanciar conexion
        Conexion objConexion = new Conexion();
        // Query
        string sQuery = "SELECT ISNULL(tp.impPagados,0) " +
                 "FROM tPedimento tp " +
                 "WHERE CONVERT(VARCHAR, tp.idAduana) + CONVERT(VARCHAR, tp.idPatente) + CONVERT(VARCHAR, tp.pedimento) = (SELECT CONVERT(VARCHAR, ts.idAduana) + CONVERT(VARCHAR, ts.idPatente) + CONVERT(VARCHAR, ts.pedimento) FROM tSubReferencia ts WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + ")";

        // Ejecutar la query
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Comprueba que no haya habido error
        if (arrResultado[0] == "1")
        {
            if (arrResultado[1] == "0" || String.IsNullOrEmpty(arrResultado[1]))
            {
                //Se retorna el sResultado en caso de error
                objTarifa.iResultado = 0;
                //Se retorna mensaje de error
                objTarifa.sMensaje += "Impuestos pagados,";
            }
            else
            {
                // Guardar valor
                objTarifa.dImpuestosPagados = decimal.Parse(arrResultado[1]);
            }
        }
        else
        {
            //Se retorna el sResultado 
            objTarifa.iResultado = 0;
            //Se retorna mensaje de error
            objTarifa.sMensaje = "Error al comprobar valor de impuestos pagados";
        }
    }

    private void fn_ComprobarImpuestosAfianzados(Tarifa objTarifa)
    {
        // Instanciar conexion
        Conexion objConexion = new Conexion();
        // Query
        string sQuery = "SELECT ISNULL(tp.impAfianzados,0) " +
                        "FROM tPedimento tp " +
                        "WHERE CONVERT(VARCHAR, tp.idAduana) + CONVERT(VARCHAR, tp.idPatente) + CONVERT(VARCHAR, tp.pedimento) = (SELECT CONVERT(VARCHAR, ts.idAduana) + CONVERT(VARCHAR, ts.idPatente) + CONVERT(VARCHAR, ts.pedimento) FROM tSubReferencia ts WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + ")";

        // Ejecutar la query
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Comprueba que no haya habido error
        if (arrResultado[0] == "1")
        {
            if (arrResultado[1] == "0" || String.IsNullOrEmpty(arrResultado[1]))
            {
                //Se retorna el sResultado en caso de error
                objTarifa.iResultado = 0;
                //Se retorna mensaje de error
                objTarifa.sMensaje += "Impuestos afianzados,";
            }
            else
            {
                // Guardar valor
                objTarifa.dImpuestosAfianzados = decimal.Parse(arrResultado[1]);
            }
        }
        else
        {
            //Se retorna el sResultado 
            objTarifa.iResultado = 0;
            //Se retorna mensaje de error
            objTarifa.sMensaje = "Error al comprobar valor de impuestos afianzados";
        }
    }

    private void fn_ComprobarImpuestosSubsidiados(Tarifa objTarifa)
    {
        // Instanciar conexion
        Conexion objConexion = new Conexion();
        // Query
        string sQuery = "SELECT ISNULL(tp.impuestosSubcidiados,0) " +
                        "FROM tPedimento tp " +
                        "WHERE CONVERT(VARCHAR, tp.idAduana) + CONVERT(VARCHAR, tp.idPatente) + CONVERT(VARCHAR, tp.pedimento) = (SELECT CONVERT(VARCHAR, ts.idAduana) + CONVERT(VARCHAR, ts.idPatente) + CONVERT(VARCHAR, ts.pedimento) FROM tSubReferencia WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + ")";

        // Ejecutar la query
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Comprueba que no haya habido error
        if (arrResultado[0] == "1")
        {
            if (arrResultado[1] == "0" || String.IsNullOrEmpty(arrResultado[1]))
            {
                //Se retorna el sResultado en caso de error
                objTarifa.iResultado = 0;
                //Se retorna mensaje de error
                objTarifa.sMensaje += "Impuestos subsidiados,";
            }
            else
            {
                // Guardar valor
                objTarifa.dImpuestosSubsidiados = decimal.Parse(arrResultado[1]);
            }
        }
        else
        {
            //Se retorna el sResultado 
            objTarifa.iResultado = 0;
            //Se retorna mensaje de error
            objTarifa.sMensaje = "Error al comprobar valor de impuestos subsidiados";
        }
    }

    private void fn_ComprobarGastosComprobados(Tarifa objTarifa)
    {
        // Instanciar conexion
        Conexion objConexion = new Conexion();
        // Query
        string sQuery = @"SELECT ISNULL(SUM(suma),0)
	                        FROM (
		                        SELECT 
		                        CASE WHEN 
			                        tf.moneda = (SELECT cveMoneda FROM cMoneda WHERE idMoneda=(SELECT idMoneda FROM tConfiguracionTarifa WHERE idConfTarifa=" + objTarifa.iIdConfTarifa + @"))
		                        THEN
			                        tf.monto
		                        ELSE
			                        CASE WHEN
				                        tf.moneda = 'USD'
			                        THEN
				                        tf.monto * ( SELECT TOP 1 tipocambio
							                            FROM tFacturaDirectaCompras_ION tfdc
							                            WHERE tfdc.idFactura=tf.idFactura)
			                        ELSE
				                        tf.monto / ( SELECT TOP 1 tipocambio
							                            FROM tFacturaDirectaCompras_ION tfdc
							                            WHERE tfdc.idFactura=tf.idFactura)
			                        END
		                        END AS suma
		                        FROM tFactura tf
		                        WHERE idSubReferencia = " + objTarifa.iIdSubReferencia
                                + @" GROUP BY tf.moneda, tf.idFactura, tf.monto
	                        )AS Source";
        // Ejecutar la query
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Comprueba que no haya habido error
        if (arrResultado[0] == "1")
        {
            if (arrResultado[1] == "0" || String.IsNullOrEmpty(arrResultado[1]))
            {
                //Se retorna el sResultado en caso de error
                objTarifa.iResultado = 0;
                //Se retorna mensaje de error
                objTarifa.sMensaje += "Gastos comprobados,";
            }
            else
            {
                // Guardar valor
                objTarifa.dGastosComprobados = decimal.Parse(arrResultado[1]);
                /*
                // Sumar impuestos
                sQuery = "SELECT ISNULL(SUM(suma), 0) " +
                         "   FROM ( " +
                         "   SELECT  " +
                         "       CASE WHEN " +
                         "           tpe.idMoneda = " + objTarifa.iIdMoneda + " " +
                         "       THEN  " +
                         "           SUM(ti.importe) " +
                         "       ELSE " +
                         "           CASE WHEN " +
                         "               tpe.idMoneda = 2 " +
                         "           THEN " +
                         "               SUM(ti.importe) * " + HttpContext.Current.Session["fTipoCambio"] + " " +
                         "           ELSE " +
                         "               SUM(ti.importe) / " + HttpContext.Current.Session["fTipoCambio"] + " " +
                         "           END " +
                         "       END suma " +
                         "   FROM tImpuesto ti " +
                         "        JOIN " +
                         "        tSubReferencia ts " +
                         "        ON ti.idSubreferenciaN = ts.idSubReferencia " +
                         "        JOIN " +
                         "        tPolizaEgreso tpe " +
                         "        ON ti.idPoliza = tpe.idPolizaEgreso " +
                         "   WHERE " +
                         "       ts.idSubReferencia = " + objTarifa.iIdSubReferencia + " " +
                         "       AND " +
                         "       tpe.idTipoPago = 21 " +
                         "       AND " +
                         "       tpe.idEstatusPoliza = 1 " +
                         "   GROUP BY " +
                         "       tpe.idMoneda) AS source " +
                         "   ";
                arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
                if (arrResultado[0] == "1")
                {
                    if (arrResultado[1] != "0" && !String.IsNullOrEmpty(arrResultado[1]))
                    {
                        objTarifa.dGastosComprobados += decimal.Parse(arrResultado[1]);
                    }
                }
                else
                {
                    //Se retorna el sResultado 
                    objTarifa.iResultado = 0;
                    //Se retorna mensaje de error
                    objTarifa.sMensaje = "Error al comprobar valor de gastos comprobados";
                }*/
            }
        }
        else
        {
            //Se retorna el sResultado 
            objTarifa.iResultado = 0;
            //Se retorna mensaje de error
            objTarifa.sMensaje = "Error al comprobar valor de gastos comprobados";
        }
    }

    private void fn_ComprobarPesoBruto(Tarifa objTarifa)
    {
        // Instanciar conexion
        Conexion objConexion = new Conexion();
        // Query
        string sQuery = "SELECT ISNULL(tp.pesoBruto,0) " +
                        "FROM tPedimento tp " +
                        "WHERE CONVERT(VARCHAR, tp.idAduana) + CONVERT(VARCHAR, tp.idPatente) + CONVERT(VARCHAR, tp.pedimento) = (SELECT CONVERT(VARCHAR, ts.idAduana) + CONVERT(VARCHAR, ts.idPatente) + CONVERT(VARCHAR, ts.pedimento) FROM tSubReferencia WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + ")";

        // Ejecutar la query
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Comprueba que no haya habido error
        if (arrResultado[0] == "1")
        {
            if (arrResultado[1] == "0" || String.IsNullOrEmpty(arrResultado[1]))
            {
                //Se retorna el sResultado en caso de error
                objTarifa.iResultado = 0;
                //Se retorna mensaje de error
                objTarifa.sMensaje += "Peso bruto,";
            }
            else
            {
                // Guardar valor
                objTarifa.dPesoBruto = decimal.Parse(arrResultado[1]);
            }
        }
        else
        {
            //Se retorna el sResultado 
            objTarifa.iResultado = 0;
            //Se retorna mensaje de error
            objTarifa.sMensaje = "Error al comprobar valor de peso bruto";
        }
    }

    private void fn_ComprobarImpuestosFinanciadosPorNAD(Tarifa objTarifa)
    {
        // Instanciar conexion
        Conexion objConexion = new Conexion();
        // Query
        string sQuery = @"SELECT SUM(importe)
                        FROM tImpuesto
                        WHERE idSubreferenciaN=" + objTarifa.iIdSubReferencia;

        // Ejecutar la query
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Comprueba que no haya habido error
        if (arrResultado[0] == "1")
        {
            if (arrResultado[1] == "0" || String.IsNullOrEmpty(arrResultado[1]))
            {
                //Se retorna el sResultado en caso de error
                objTarifa.iResultado = 0;
                //Se retorna mensaje de error
                objTarifa.sMensaje += "Partida presupuestal,";
            }
            else
            {
                // Guardar valor
                objTarifa.dImpuestosFinanciadosPorNAD = decimal.Parse(arrResultado[1]);
            }
        }
        else
        {
            //Se retorna el sResultado 
            objTarifa.iResultado = 0;
            //Se retorna mensaje de error
            objTarifa.sMensaje = "Error al comprobar valor de partida presupuestal";
        }
    }

    private void fn_ComprobarGastosDirectos(Tarifa objTarifa)
    {
        // Instanciar conexion
        Conexion objConexion = new Conexion();
        // Query
        string sQuery = "SELECT ISNULL(tp.GastosDirectosCliente,0) " +
                        "FROM tPedimento tp " +
                        "WHERE CONVERT(VARCHAR, tp.idAduana) + CONVERT(VARCHAR, tp.idPatente) + CONVERT(VARCHAR, tp.pedimento) = (SELECT CONVERT(VARCHAR, ts.idAduana) + CONVERT(VARCHAR, ts.idPatente) + CONVERT(VARCHAR, ts.pedimento) FROM tSubReferencia WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + ")";

        // Ejecutar la query
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Comprueba que no haya habido error
        if (arrResultado[0] == "1")
        {
            if (arrResultado[1] == "0" || String.IsNullOrEmpty(arrResultado[1]))
            {
                //Se retorna el sResultado en caso de error
                objTarifa.iResultado = 0;
                //Se retorna mensaje de error
                objTarifa.sMensaje += "Gastos directos,";
            }
            else
            {
                // Guardar valor
                objTarifa.dGastosDirectos = decimal.Parse(arrResultado[1]);
            }
        }
        else
        {
            //Se retorna el sResultado 
            objTarifa.iResultado = 0;
            //Se retorna mensaje de error
            objTarifa.sMensaje = "Error al comprobar valor de gastos directos";
        }
    }

    public void fn_EliminarTarifaflat(Tarifa objTarifa)
    {
        // Instanciar conexion
        Conexion objConexion = new Conexion();
        // query para eliminar
        string sQuery = "DELETE tTarifasFlat WHERE idSubreferencia = " + objTarifa.iIdSubReferencia + " AND idServicio = " + iIdServicio;
        // Ejecuta query
        string sResultado = objConexion.ejecutarComando(sQuery);
        // Verifica resultado
        if (sResultado != "1")
        {
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Error al eliminar la tarifa";
        }
        else
        {
            objTarifa.iResultado = 1;
        }
    }

    public void fn_GuardarTarifaFlat(Tarifa objTarifa)
    {
        //instancia Conexión
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarTarifaFlat", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //pasar parametros al sp
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objTarifa.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@iIdServicio", SqlDbType.Int, objTarifa.iIdServicio.ToString());
                objConexion.agregarParametroSP("@iCantidad", SqlDbType.Int, objTarifa.dCantidad.ToString());
                objConexion.agregarParametroSP("@dSubtotal", SqlDbType.Decimal, objTarifa.dSubTotal.ToString());
                objConexion.agregarParametroSP("@dIva", SqlDbType.Decimal, objTarifa.dIva.ToString());
                objConexion.agregarParametroSP("@dTotal", SqlDbType.Decimal, objTarifa.dTotal.ToString());
                objConexion.agregarParametroSP("@iEditando", SqlDbType.TinyInt, objTarifa.iAccion.ToString());
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@sResOut");

                int iMostrarBoton = 0;

                if (sResOut[0] == "1" && int.TryParse(sResOut[0], out iMostrarBoton))
                {
                    //Se retorna el mensaje de éxito
                    objTarifa.iResultado = 1;
                    objTarifa.sMensaje = "Tarifa asignada con éxito.";
                    // Se retorna el id 
                    objTarifa.iIdTarifa = int.Parse(sResOut[0].ToString());
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTarifa.iResultado = 0;
                    objTarifa.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //mensaje de error al caer en una excepción
                objTarifa.iResultado = 0;
                objTarifa.sMensaje = "Error con su base de datos, " + ex.Message.ToString();
            }
        }
        else
        {
            //mensaje de error con la base de datos
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Error con su base de datos, no existe procedimiento almacenado";
        }
    }

    public void fn_ComprobarInformacionPedimento(Tarifa objTarifa)
    {
        // Instancia conexion
        Conexion objConexion = new Conexion();
        // Query para comprobar pedimento
        string sQuery = "SELECT " +
                        "    CASE WHEN EXISTS  " +
                        "        (SELECT " +
                        "            * " +
                        "        FROM " +
                        "            tFolioTransitorioSubReferencia tfts " +
                        "            INNER JOIN " +
                        "            tPedimento tp " +
                        "            ON " +
                        "            tfts.idSubReferencia = tp.idSubReferencia " +
                        "        WHERE " +
                        "            tfts.idFolioTransitorio = (SELECT idFolioTransitorio  " +
                        "			                           FROM tFolioTransitorioSubReferencia " +
                        "			                           WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + ") " +
                        "            AND tp.idEstatus != (SELECT idEstatus FROM cEstatus WHERE nomEstatus = 'Consultado')) " +
                        "    THEN  " +
                        "        '-1' " +
                        "    ELSE " +
                        "        '1' " +
                        "END";
        // Realizar consulta
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Comprobar resultado consulta
        if (arrResultado[0] != "1")
        {
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Problema de conexión con la base de datos";
        }
        else
        {
            if (arrResultado[1] == "1")
            {
                objTarifa.iResultado = 1;
            }
            else
            {
                objTarifa.iResultado = -1;
            }
        }
    }

    public void fn_GenerarTablaAlCosto(Tarifa objTarifa)
    {
        // Instanciar conexion
        Conexion objConexion = new Conexion();
        // Query para obtener la tabla al costo
        string sQuery = "EXECUTE pa_GuardarAlCostoFlat " + objTarifa.iIdSubReferencia;
        // Variable almacenar si hubo gastos al cost
        bool bServicios = false;
        // Variable para almacenar suma
        decimal dSuma = 0;
        // Realizar consulta
        if (objTarifa.dt_Tarifas == null || objTarifa.dt_Tarifas.Rows.Count == 0)
        {
            objTarifa.dt_Tarifas = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            // Acomodamos la datatable para luego obtener el html en una nueva datatable
            DataTable dtCloned = dt_Tarifas.Clone();
            dtCloned.Columns[0].DataType = typeof(string);
            dtCloned.Columns[1].DataType = typeof(string);
            dtCloned.Columns[2].DataType = typeof(string);
            dtCloned.Columns[3].DataType = typeof(string);
            dtCloned.Columns[4].DataType = typeof(string);
            dtCloned.Columns[5].DataType = typeof(string);
            dtCloned.Columns[6].DataType = typeof(string);
            dtCloned.Columns[7].DataType = typeof(string);
            foreach (DataRow row in objTarifa.dt_Tarifas.Rows)
            {
                dtCloned.ImportRow(row);
            }
            bServicios = objTarifa.dt_Tarifas.Rows.Count != 0;
            if (bServicios)
            {
                // Recorrer resultado
                foreach (DataRow row in dt_Tarifas.Rows)
                {
                    // Sumar al total
                    dSuma += decimal.Parse(row[7].ToString());
                }
            }
            //objTarifa.dt_Tarifas = dtCloned;
        }
        else
        {
            // Datatable almacenar datos
            DataTable dtbRes = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            // Verificar si existen datos
            bServicios = dtbRes.Rows.Count != 0;
            // Acomodamos la datatable para luego obtener el html en una nueva datatable
            DataTable dtCloned = dtbRes.Clone();
            dtCloned.Columns[0].DataType = typeof(string);
            dtCloned.Columns[1].DataType = typeof(string);
            dtCloned.Columns[2].DataType = typeof(string);
            dtCloned.Columns[3].DataType = typeof(string);
            dtCloned.Columns[4].DataType = typeof(string);
            dtCloned.Columns[5].DataType = typeof(string);
            dtCloned.Columns[6].DataType = typeof(string);
            dtCloned.Columns[7].DataType = typeof(string);
            foreach (DataRow row in dtbRes.Rows)
            {
                dtCloned.ImportRow(row);
            }
            // Si hay datos realizar suma de los mismos
            if (bServicios)
            {
                // Recorrer resultado
                foreach (DataRow row in dtbRes.Rows)
                {
                    // Sumar al total
                    dSuma += decimal.Parse(row[7].ToString());
                }
                // Merge
                //objTarifa.dt_Tarifas.Merge(dtCloned);
            }
        }
        // Si hubo servicios al costo calcula el servicio
        if (bServicios)
        {
            // Declarar nuevo renglon de la tabla
            DataRow objRow = objTarifa.dt_Tarifas.NewRow();
            // Consulta el servicio
            sQuery = "SELECT cs.descripcion " +
                     "   FROM  " +
                     "       tClienteServicio tcs " +
                     "       INNER JOIN " +
                     "       tClienteAduana tca " +
                     "       ON tca.idClienteAduana = tcs.idClienteAduana " +
                     "       INNER JOIN " +
                     "       tSubReferencia ts " +
                     "       ON tca.idCliente = ISNULL(ts.idClienteContable,  " +
                     "		                          ts.idClienteOperativo) " +
                     "       AND tca.idAduana = ts.idAduana " +
                     "       INNER JOIN " +
                     "       cServicio cs " +
                     "       ON tcs.idServicio = cs.idServicio " +
                     "   WHERE " +
                     "       ts.idSubReferencia = " + objTarifa.iIdSubReferencia;
            String[] arrRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            // Comprueba todo salio bien
            if (arrRes[0] == "1")
            {
                if (String.IsNullOrEmpty(arrRes[1]))
                    objRow[0] = "SERVICIOS DE COMERCIO EXTERIOR";
                else
                    objRow[0] = arrRes[1];
            }
            else
            {
                // Servicio
                objRow[0] = "SERVICIOS DE COMERCIO EXTERIOR";
            }
            // Cantidad
            objRow[1] = 1;
            // Subtotal
            decimal dAnticipo = 0;
            // Consulta el anticipo
            sQuery = "SELECT SUM(monto) " +
                     "FROM tAnticipo " +
                     "WHERE idSubreferencia = " + objTarifa.iIdSubReferencia;
            arrRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            // Comprueba todo salio bien
            if (arrRes[0] == "1")
            {
                dAnticipo = decimal.Parse((String.IsNullOrEmpty(arrRes[1]) ? "0" : arrRes[1]));
            }
            decimal dPorcentaje = 3;
            // Consulta el porcentaje
            sQuery = "SELECT comision " +
                     "FROM tCliente " +
                     "WHERE idCliente = ISNULL((SELECT idClienteContable FROM tSubReferencia WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + "), " +
                     "(SELECT idClienteOperativo FROM tSubReferencia WHERE idSubReferencia = " + objTarifa.iIdSubReferencia + "))";
            arrRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            // Comprueba todo salio bien
            if (arrRes[0] == "1")
            {
                dPorcentaje = decimal.Parse((String.IsNullOrEmpty(arrRes[1]) ? "0" : arrRes[1]));
            }
            decimal dSubtotal = (dSuma) * dPorcentaje / 100; // Agregar anticipo (restar)
            objRow[2] = dSubtotal;
            // Iva
            objRow[3] = dSubtotal * (decimal)0.16;
            // Total
            objRow[4] = dSubtotal * (decimal)1.16;
            // Guardar
            objRow[5] = "";
            // Eliminar
            objRow[6] = "";
            // Agregar renglon
            objTarifa.dt_Tarifas.Rows.Add(objRow);
        }
    }

    public bool fn_RestarImpuestos(Tarifa objTarifa)
    {
        // Instanciar conexion
        Conexion objConexion = new Conexion();
        string sQuery = @"SELECT cveSunServicio
                        FROM cServicio
                        WHERE idServicio=(SELECT idServicio 
				                          FROM tconfiguraciontarifa 
				                          WHERE idConfTarifa=" + objTarifa.iIdConfTarifa + @")";
        string[] arrRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Comprueba todo salio bien
        if (arrRes[0] == "1")
        {
            if (arrRes[1] == "NCO000030")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public Utilerias fn_ObtenerTablaTarifasAsignadas(Tarifa objTarifa)
    {
        // Instancia clase de utilerias
        Utilerias objUtilerias = new Utilerias();
        // Instancia clase de conexion
        Conexion objConexion = new Conexion();
        // String de query
        string sQuery = "SELECT " +
                        "    (SELECT refOperativa FROM tSubReferencia tsr WHERE tsr.idSubReferencia = tfts.idSubReferencia), " +
                        "    (SELECT nombre FROM cTipoTarifa ctt WHERE ctt.idTipoTarifa = tct.idTipoTarifa), " +
                        "    (SELECT tipoOperacion FROM cTipoOperacion cto WHERE cto.idTipoOperacion = tct.idTipoOperacion), " +
                        "    (SELECT nombre FROM cTipoMaterial ctm WHERE ctm.idTipoMaterial = tct.idTipoMaterial), " +
                        "    (SELECT nombre FROM cTipoReglaTarifa ctrt WHERE ctrt.idTipoRegla = (SELECT TOP 1 idTipoRegla FROM tReglaTarifa trt WHERE trt.idConfTarifa = tct.idConfTarifa )), " +
                        "    ttr.cantidad, " +
                        "    ttr.subtotal / ttr.cantidad, " +
                        "    ttr.subtotal, " +
                        "    CONVERT(VARCHAR, (SELECT TOP 1 (SELECT iva FROM cIva ci WHERE ci.idIva = trt.idIva) FROM tReglaTarifa trt WHERE trt.idConfTarifa = tct.idConfTarifa)) + '%', " +
                        "    CONVERT(VARCHAR, (SELECT TOP 1 (SELECT retencion FROM cRetencion cr WHERE cr.idRetencion = trt.idRetencion) FROM tReglaTarifa trt WHERE trt.idConfTarifa = tct.idConfTarifa)) + '%', " +
                        "    ttr.iva, " +
                        "    ISNULL(ttr.retencion,0), " +
                        "    ttr.total, " +
                        "    '<div class=''text-center''><span id=''hbtnQuitarTarifa' + CONVERT(VARCHAR, ttr.idTarifaReferencia) + ''' dataid=''' +  CONVERT(VARCHAR, ttr.idTarifaReferencia) + ''' class=''fa fa-trash btn-quitar fa-red-sm''></span></div>' " +
                        "FROM " +
                        "    tTarifaReferencia ttr " +
                        "    JOIN " +
                        "    tFolioTransitorioSubReferencia tfts " +
                        "    ON ttr.idFolioSubReferencia = tfts.idFolioSubReferencia " +
                        "    JOIN " +
                        "    tConfiguracionTarifa tct " +
                        "    ON ttr.idConfTarifa = tct.idConfTarifa " +
                        "WHERE " +
                        "    idFolioTransitorio = " + objTarifa.iIdFolioTransitorio;
        // Obtener datatable
        DataTable dtbTarifasAsignadas = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
        // Generar tabla
        objUtilerias.sNombre = "htblTarifasAsignadas";
        objUtilerias.arrColumnasFiltro = new string[] { "Subreferencia", "Tarifa", "Operacion", "Material", "Regla" };
        objUtilerias.arrColumnasSinFiltro = new string[] { "Cantidad", "Monto", "Subtotal", "IVA", "Retencion", "Monto IVA", "Monto Retencion", "Total", "Controles" };
        objUtilerias.fn_GeneraTabla(objUtilerias, dtbTarifasAsignadas);
        // Retornar objeto utilerias
        return objUtilerias;
    }

    public Utilerias fn_ObtenerTablaTarifasManuales(Tarifa objTarifa)
    {
        // Instancia clase de utilerias
        Utilerias objUtilerias = new Utilerias();
        // Instancia clase de conexion
        Conexion objConexion = new Conexion();
        // String de query
        string sQuery = "SELECT " +
                        "    (SELECT refOperativa FROM tSubReferencia tsr WHERE tsr.idSubReferencia = ttm.idSubreferencia), " +
                        "    (SELECT cveSunServicio + '-' + descripcion FROM cServicio cs WHERE cs.idServicio = ttm.idServicio), " +
                        "    FORMAT(ttm.cantidad, 'g18') Cantidad, " +
                        "    FORMAT(ttm.subtotal / ttm.cantidad, 'g18') Monto, " +
                        "    CONVERT(VARCHAR, FORMAT((SELECT iva FROM cIva ci WHERE ci.idIva = ttm.idIva), 'g18')) + '%', " +
                        "    FORMAT(ttm.subtotal, 'g18'), " +
                        "    FORMAT((SELECT iva FROM cIva ci WHERE ci.idIva = ttm.idIva) * ttm.subtotal / 100 , 'g18'), " +
                        "    FORMAT(ttm.total, 'g18'), " +
                        "    '<div class=''text-center''><span id=''hbtnEditarTarifaManual'' dataid=''' + CONVERT(VARCHAR, ttm.idTarifaManual) + ''' class=''fa fa-pencil btn-editar-manual fa-green-sm''></span><span id=''hbtnQuitarTarifaManual'' dataid=''' + CONVERT(VARCHAR, ttm.idTarifaManual) + ''' class=''fa fa-trash btn-quitar-manual fa-red-sm''></span></div>' " +
                        "FROM tTarifasManuales ttm " +
                        "    WHERE idSubreferencia IN ( " +
                        "        SELECT idSubreferencia " +
                        "        FROM tFolioTransitorioSubReferencia " +
                        "        WHERE idFolioTransitorio = " + objTarifa.iIdFolioTransitorio + " " +
                        "    )";
        // Obtener datatable
        DataTable dtbTarifasAsignadas = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
        // Generar tabla
        objUtilerias.sNombre = "htblTarifasManuales";
        objUtilerias.arrColumnasFiltro = new string[] { "Subreferencia", "Servicio" };
        objUtilerias.arrColumnasSinFiltro = new string[] { "Cantidad", "Monto", "IVA", "Subtotal", "Monto IVA", "Total", "Controles" };
        objUtilerias.fn_GeneraTabla(objUtilerias, dtbTarifasAsignadas);
        // Retornar objeto utilerias
        return objUtilerias;
    }

    public void fn_GuardarTarifaManual(Tarifa objTarifa)
    {
        //instancia Conexión
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarTarifaManual", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //pasar parametros al sp
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objTarifa.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@iIdServicio", SqlDbType.Int, objTarifa.iIdServicio.ToString());
                objConexion.agregarParametroSP("@dCantidad", SqlDbType.Decimal, objTarifa.dCantidad.ToString());
                objConexion.agregarParametroSP("@dMonto", SqlDbType.Decimal, objTarifa.dMonto.ToString());
                objConexion.agregarParametroSP("@iIdIva", SqlDbType.Int, objTarifa.iIdIva.ToString());
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@sResOut");

                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objTarifa.iResultado = 1;
                    objTarifa.sMensaje = "Tarifa asignada con éxito.";
                    // Se retorna el id 
                    objTarifa.iIdTarifa = int.Parse(sResOut[0].ToString());
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTarifa.iResultado = 0;
                    objTarifa.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //mensaje de error al caer en una excepción
                objTarifa.iResultado = 0;
                objTarifa.sMensaje = "Error con su base de datos, " + ex.Message.ToString();
            }
        }
        else
        {
            //mensaje de error con la base de datos
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Error con su base de datos, no existe procedimiento almacenado";
        }
    }

    public void fn_ActualizarTarifaManual(Tarifa objTarifa)
    {
        //instancia Conexión
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_ActualizarTarifaManual", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //pasar parametros al sp
                objConexion.agregarParametroSP("@iIdTarifaReferencia", SqlDbType.Int, objTarifa.iIdTarifaReferencia.ToString());
                objConexion.agregarParametroSP("@iIdServicio", SqlDbType.Int, objTarifa.iIdServicio.ToString());
                objConexion.agregarParametroSP("@dCantidad", SqlDbType.Decimal, objTarifa.dCantidad.ToString());
                objConexion.agregarParametroSP("@dMonto", SqlDbType.Decimal, objTarifa.dMonto.ToString());
                objConexion.agregarParametroSP("@iIdIva", SqlDbType.Int, objTarifa.iIdIva.ToString());
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@sResOut");

                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objTarifa.iResultado = 1;
                    objTarifa.sMensaje = "Tarifa actualizada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTarifa.iResultado = 0;
                    objTarifa.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //mensaje de error al caer en una excepción
                objTarifa.iResultado = 0;
                objTarifa.sMensaje = "Error con su base de datos, " + ex.Message.ToString();
            }
        }
        else
        {
            //mensaje de error con la base de datos
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Error con su base de datos, no existe procedimiento almacenado";
        }
    }

    public void fn_QuitarTarifaManual(Tarifa objTarifa)
    {
        //Se agregra un parametro de tipo int sIdUsuario

        //instancia Conexión
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_QuitarTarifaManual", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //pasar parametros al sp
                objConexion.agregarParametroSP("@iIdTarifaManual", SqlDbType.Int, objTarifa.iIdTarifaReferencia.ToString());
                //objConexion.agregarParametroSP("@sIdUsuario", SqlDbType.Int, sIdUsuario.ToString());
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@sResOut");

                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objTarifa.iResultado = 1;
                    objTarifa.sMensaje = "Tarifa eliminada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTarifa.iResultado = 0;
                    objTarifa.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //mensaje de error al caer en una excepción
                objTarifa.iResultado = 0;
                objTarifa.sMensaje = "Error con su base de datos, " + ex.Message.ToString();
            }
        }
        else
        {
            //mensaje de error con la base de datos
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Error con su base de datos, no existe procedimiento almacenado";
        }
    }

    public Utilerias fn_ObtenerTablaTarifasAlCosto(Tarifa objTarifa)
    {
        // Instancia clase de utilerias
        Utilerias objUtilerias = new Utilerias();
        // Instancia clase de conexion
        Conexion objConexion = new Conexion();
        // String de query
        string sQuery = "SELECT * FROM " +
                        "(SELECT " +
                        "    tf.UUID uuid, " +
                        "    tf.noFactura noFactura, " +
                        "    (SELECT TOP 1 nomProveedor FROM tProveedor tp WHERE tp.idProveedor = tf.idProveedor) proveedor, " +
                        "    (SELECT TOP 1 (SELECT TOP 1 concepto FROM cConcepto cc WHERE cc.idConcepto = tpsc.idConcepto) FROM tProvServicioConcepto tpsc WHERE tpsc.idProvServicioConcepto = tsf.idProvServicioConcepto) concepto, " +
                        "    (SELECT TOP 1 (SELECT TOP 1 cveSunServicio + '-' + descripcion FROM cServicio cs WHERE cs.idServicio = tpsc.idServicio) FROM tProvServicioConcepto tpsc WHERE tpsc.idProvServicioConcepto = tsf.idProvServicioConcepto) cve, " +
            "    (tsf.subtotal - tsf.descuento) subtotal, " +
                        "    tsf.porcentajeIVA porcentaje, " +
                        "    tsf.iva iva, " +
                        "    tsf.total total " +
                        "FROM  " +
                        "    tFactura tf " +
                        "    JOIN " +
                        "    tServicioFactura tsf " +
                        "    ON tf.idFactura = tsf.idFactura " +
                        "WHERE  " +
                        "    tf.idSubReferencia IN ( " +
                        "        SELECT " +
                        "            tfts.idSubReferencia " +
                        "        FROM " +
                        "            tFolioTransitorioSubReferencia tfts " +
                        "        WHERE " +
                        "            tfts.idFolioTransitorio = " + objTarifa.iIdFolioTransitorio + " " +
                        "    ) AND " +
                        "    idCliente IN ( " +
                        "        SELECT tc.idCliente " +
                        "        FROM tCliente tc " +
                        "        WHERE tc.rfc IN ( " +
                        "            SELECT rfc " +
                        "            FROM tComitente " +
                        "        )" +
                        "    ) AND tf.idEstatusFactura != 20 AND ISNULL(tf.honorarios,0)=0" +
                        " UNION " + // Agregar columna de totales
                        "SELECT " +
                        "    'Totales' uuid, " +
                        "    '-' noFactura, " +
                        "    '-' proveedor, " +
                        "    '-' concepto, " +
                        "    '-' cve, " +
            "    ISNULL(SUM(ISNULL((tsf.subtotal - tsf.descuento), 0)),0) subtotal, " +
                        "    0 porcentaje, " +
                        "    ISNULL(SUM(ISNULL(tsf.iva, 0)),0) iva, " +
                        "    ISNULL(SUM(ISNULL(tsf.total, 0)),0) total " +
                        "FROM  " +
                        "    tFactura tf " +
                        "    JOIN " +
                        "    tServicioFactura tsf " +
                        "    ON tf.idFactura = tsf.idFactura " +
                        "WHERE  " +
                        "    tf.idSubReferencia IN ( " +
                        "        SELECT " +
                        "            tfts.idSubReferencia " +
                        "        FROM " +
                        "            tFolioTransitorioSubReferencia tfts " +
                        "        WHERE " +
                        "            tfts.idFolioTransitorio = " + objTarifa.iIdFolioTransitorio + " " +
                        "    ) AND " +
                        "    idCliente IN ( " +
                        "        SELECT tc.idCliente " +
                        "        FROM tCliente tc " +
                        "        WHERE tc.rfc IN ( " +
                        "            SELECT rfc " +
                        "            FROM tComitente " +
                        "        )" +
                        "    ) AND tf.idEstatusFactura != 20 AND ISNULL(tf.honorarios,0)=0) tbl";
        // Obtener datatable
        DataTable dtbTarifasAlCosto = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
        // Verifica si hubo al menos un gasto para generar la comision
        if (dtbTarifasAlCosto.Rows.Count > 1)
        {
            // Comprueba si ya se inserto la comision
            sQuery = "SELECT " +
                     "       (SELECT cveSunServicio + '-' + descripcion FROM cServicio cs WHERE cs.idServicio = tcft.idServicio), " +
                     "       tcft.subtotal, " +
                     "       (SELECT iva FROM cIva ci WHERE ci.idIva = tcft.idIva), " +
                     "       (SELECT iva FROM cIva ci WHERE ci.idIva = tcft.idIva) * tcft.subtotal / 100, " +
                     "       tcft.total " +
                     "   FROM tComisionFolioTransitorio tcft " +
                     "   WHERE idFolioTransitorio = " + objTarifa.iIdFolioTransitorio;
            // Ejecuta la consulta
            DataTable dtbComision = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            // Verifica si ya existe
            if (dtbComision.Rows.Count > 0)
            {
                // Obtener la comision
                DataRow objRow = dtbTarifasAlCosto.NewRow();
                objRow[0] = "-";
                objRow[1] = "-";
                objRow[2] = "-";
                objRow[3] = "COMISION";
                objRow[4] = dtbComision.Rows[0][0];
                objRow[5] = dtbComision.Rows[0][1];
                objRow[6] = dtbComision.Rows[0][2];
                objRow[7] = dtbComision.Rows[0][3];
                objRow[8] = dtbComision.Rows[0][4];
                dtbTarifasAlCosto.Rows.Add(objRow);
            }
            else
            {
                // Obtener comision
                sQuery = "SELECT comision " +
                         "   FROM tCliente  " +
                         "   WHERE idCliente = ( " +
                         "       SELECT tft.idCliente " +
                         "       FROM tFolioTransitorio tft " +
                         "       WHERE tft.idFolioTransitorio = " + objTarifa.iIdFolioTransitorio + " " +
                         "   )";
                //  Ejecuta consulta
                string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
                // Verifica resultado
                if (arrResultado[0] == "1" && !String.IsNullOrEmpty(arrResultado[1]))
                {
                    // Verifica si se le cobra comision
                    if (double.Parse(arrResultado[1]) >= 0)
                    {
                        double dComision = double.Parse(arrResultado[1]);
                        string sConcepto = "";

                        sQuery = @"SELECT case when (dbo.fn_IdentificaAplicaProcesoConsultoria(" + objTarifa.iIdFolioTransitorio + @") = 1) 
		                                    then
			                                    csc.cveSunServicio+'-'+csc.descripcion
		                                    else
			                                    cs.cveSunServicio+'-'+cs.descripcion  
		                                    end as servicio
                                    FROM tClienteServicioComision tcsc
                                    INNER JOIN cServicio cs ON tcsc.idServicio = cs.idServicio
                                    LEFT JOIN cServicio csc ON tcsc.idServicioFacturarConsultoria = csc.idServicio
                                    WHERE idCliente=(SELECT idCliente 
				                                    FROM tFolioTransitorio
				                                    WHERE idFolioTransitorio=" + objTarifa.iIdFolioTransitorio + @")";

                        arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);

                        if (arrResultado[1] != "")
                        {
                            sConcepto = arrResultado[1];
                        }
                        else
                        {

                            sQuery = @"SELECT dbo.fn_IdentificaAplicaProcesoConsultoria(" + objTarifa.iIdFolioTransitorio + @")";

                            arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);

                            if (arrResultado[1] == "1")
                            { // Si aplica proceso consultoria, se regitsra el servicio NCS000036.
                                sConcepto = "NCS000036-GASTO DE COMERCIO EXTERIOR";
                            }
                            else
                            { // Si no aplica se registra el servicio NCO000019
                                sConcepto = "NCO000019-SERVICIOS DE COMERCIO EXTERIOR";
                            }
                        }

                        sQuery = "SELECT " +
                                 "       (SELECT cs.cveSunServicio + '-' + cs.descripcion FROM cServicio cs) " +
                                 "   FROM  " +
                                 "       tClienteServicio tcs " +
                                 "       JOIN " +
                                 "       tClienteAduana tca " +
                                 "       ON tcs.idClienteAduana = tca.idClienteAduana " +
                                 "   WHERE " +
                                 "       tca.idCliente = ( " +
                                 "           SELECT tft.idCliente " +
                                 "           FROM tFolioTransitorio tft " +
                                 "           WHERE tft.idFolioTransitorio = " + objTarifa.iIdFolioTransitorio + " " +
                                 "       ) AND " +
                                 "       tca.idAduana = ( " +
                                 "           SELECT TOP 1 tsr.idAduana " +
                                 "           FROM tFolioTransitorioSubReferencia tfts " +
                                 "                JOIN " +
                                 "                tSubReferencia tsr " +
                                 "                ON tfts.idSubReferencia = tsr.idSubReferencia " +
                                 "           WHERE tfts.idFolioTransitorio = " + objTarifa.iIdFolioTransitorio + " " +
                                 "       )";
                        // Ejecutar consulta
                        arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
                        // Verifica resultado de la query
                        if (arrResultado[0] == "1")
                        {
                            if (arrResultado[1] != "")
                            {
                                sConcepto = arrResultado[1];
                            }
                        }
                        // Agrega row a la datatable
                        DataRow objRow = dtbTarifasAlCosto.NewRow();
                        objRow[0] = "-";
                        objRow[1] = "-";
                        objRow[2] = "-";
                        objRow[3] = "COMISION";
                        objRow[4] = sConcepto;
                        double dTotalSuma = dComision / 100 * dtbTarifasAlCosto.AsEnumerable().Where(x => x.Field<string>(0) == "Totales").Sum(x => x.Field<double>(5));
                        objRow[5] = dTotalSuma;
                        objRow[6] = "0.16";
                        objRow[7] = (dTotalSuma * 0.16);
                        objRow[8] = dTotalSuma + (dTotalSuma * 0.16);
                        dtbTarifasAlCosto.Rows.Add(objRow);
                        // Insertar comision
                        objTarifa.iIdServicio = -1;
                        objTarifa.iIdIva = -1;
                        objTarifa.dSubTotal = (decimal)dTotalSuma;
                        objTarifa.dTotal = (decimal)(dTotalSuma + dTotalSuma * 0.16);
                        objTarifa.sConcepto = sConcepto;
                        objTarifa.fn_ActualizarComision(objTarifa);
                    }
                }
            }
        }
        else
        {
            dtbTarifasAlCosto = new DataTable();
        }
        // Generar tabla
        objUtilerias.sNombre = "htblTarifasAlCosto";
        objUtilerias.arrColumnasFiltro = new string[] { "UUID Factura", "No Factura", "Proveedor", "Servicio", "Servicio a facturar" };
        objUtilerias.arrColumnasSinFiltro = new string[] { "Subtotal", "IVA", "Monto IVA", "Total" };
        objUtilerias.fn_GeneraTabla(objUtilerias, dtbTarifasAlCosto);
        // Retornar objeto utilerias
        return objUtilerias;
    }

    public void fn_ActualizarComision(Tarifa objTarifa)
    {
        //instancia Conexión
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_ActualizarComision", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //pasar parametros al sp
                objConexion.agregarParametroSP("@iIdFolioTransitorio", SqlDbType.Int, objTarifa.iIdFolioTransitorio.ToString());
                objConexion.agregarParametroSP("@iIdServicio", SqlDbType.Int, objTarifa.iIdServicio.ToString());
                objConexion.agregarParametroSP("@dSubtotal", SqlDbType.Decimal, objTarifa.dSubTotal.ToString());
                objConexion.agregarParametroSP("@dTotal", SqlDbType.Decimal, objTarifa.dTotal.ToString());
                objConexion.agregarParametroSP("@iIdIva", SqlDbType.Int, objTarifa.iIdIva.ToString());
                objConexion.agregarParametroSP("@sConcepto", SqlDbType.VarChar, objTarifa.sConcepto.ToString());
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@sResOut");

                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objTarifa.iResultado = 1;
                    objTarifa.sMensaje = "Comision actualizada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTarifa.iResultado = 0;
                    objTarifa.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //mensaje de error al caer en una excepción
                objTarifa.iResultado = 0;
                objTarifa.sMensaje = "Error con su base de datos, " + ex.Message.ToString();
            }
        }
        else
        {
            //mensaje de error con la base de datos
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Error con su base de datos, no existe procedimiento almacenado";
        }
    }

    public DataTable fn_ObtenerTablaGastosTerceros(Tarifa objTarifa)
    {
        // Instanciar clase conexion
        Conexion objConexion = new Conexion();
        string sQuery;


        sQuery = "SELECT " +
                    "        (SELECT tsr.refOperativa FROM tSubReferencia tsr WHERE tsr.idSubReferencia = tf.idSubReferencia) AS Subreferencia, " +
                    "        tf.UUID AS UUID, " +
                    "        tf.noFactura AS NoFactura, " +
                    "        (SELECT nomProveedor FROM tProveedor tp WHERE tp.idProveedor = tf.idProveedor) AS Proveedor, " +
                    "        tf.fechaFactura AS FechaFactura, " +
                    "        (SELECT TOP 1 (SELECT TOP 1 concepto FROM cConcepto cc WHERE cc.idConcepto = tpsc.idConcepto) FROM tProvServicioConcepto tpsc WHERE tpsc.idProvServicioConcepto = tsf.idProvServicioConcepto) AS Servicio, " +
                    "        (SELECT TOP 1 (SELECT TOP 1 cveSunServicio + '-' + descripcion FROM cServicio cs WHERE cs.idServicio = tpsc.idServicio) FROM tProvServicioConcepto tpsc WHERE tpsc.idProvServicioConcepto = tsf.idProvServicioConcepto) AS ServicioFacturar,  " +
                    "        '<input type=\"text\" dataid=''' + CONVERT(VARCHAR, tsf.idServicioFactura) + ''' id=\"htxtSubtotal'+CONVERT(VARCHAR, tsf.idServicioFactura)+'\" class=\"input-sm decimales form-control\" onblur=\"javascript:fn_CalcularTotal(this);\" onKeyPress=\"return validarNumeros(event,this)\" value =\"'+CONVERT(VARCHAR,tsf.subtotal)+'\" readonly=\"readonly\">' AS Subtotal, " +
                    "        CONVERT(VARCHAR,tsf.porcentajeIVA) AS IVA, " +
                    "        '<input type=\"text\" dataid=''' + CONVERT(VARCHAR, tsf.idServicioFactura) + ''' id=\"htxtIva'+CONVERT(VARCHAR, tsf.idServicioFactura)+'\" class=\"input-sm decimales form-control\" onblur=\"javascript:fn_CalcularTotal(this);\" onKeyPress=\"return validarNumeros(event,this)\" value =\"'+CONVERT(VARCHAR,tsf.iva)+'\" readonly=\"readonly\">' AS MontoIVA," +
                    "        isnull(isnull((select top 1 tipocambio from tFacturaDirectaCompras_ION  where idFactura=tf.idFactura),tf.tipoCambio),null) AS fTipoCambio, " +
                    "        '<input type=\"text\" id=\"htxtMonto'+CONVERT(VARCHAR, tsf.idServicioFactura)+'\" class=\"input-sm decimales form-control\" onKeyPress=\"return validarNumeros(event,this)\" value =\"'+CONVERT(VARCHAR,tsf.total)+'\" readonly=\"readonly\">' AS Total, " +
                    "        '<div><div class=\"text-center\"><span dataid=''' + CONVERT(VARCHAR, tsf.idServicioFactura) + ''' class=\"fa fa-pencil fa-green-sm btn-editar-monto tooltipAzul\" font-size:=\" 18px;\" onclick=\"javascript:fn_HabilitarEdicion(this);\"><span class=\"tooltip-top tooltiptextAzul\">Editar monto</span></span>" +
                    "        <div id=\"hdivMostrarOcultar'+CONVERT(VARCHAR, tsf.idServicioFactura)+'\" class=\"hidden\"><span id=\"hbtnGuardarEdicion\" dataid=''' + CONVERT(VARCHAR, tsf.idServicioFactura) + ''' class=\"fa fa-save fa-green-sm tooltipAzul btn-guardar-monto\" font-size:=\" 18px;\" onclick=\"javascript:fn_GuardarActualizarMonto(this);\"><span class=\"tooltip-top tooltiptextAzul\">Guardar monto</span></span>" +
                    "        <span id=\"hbtnCancelarEdicion\" class=\"fa fa-times fa-red-sm tooltipAzul\" font-size:=\" 18px;\" onclick=\"javascript:fn_LlenarTablaGastosTerceros();\"><span class=\"tooltip-top tooltiptextAzul\">Cancelar edicion</span></span></div>" +
                    //"        <span id=''hbtnIgnorarServicio'' dataid=''' + CONVERT(VARCHAR, tsf.idServicioFactura) + ''' class=''fa fa-times hidden btn-ignorar-servicio fa-' + (CASE WHEN tsf.idServicioFactura IN (SELECT tfeg.idServicioFactura FROM tFolioExluirGastos tfeg WHERE tfeg.idFolioTransitorio = " + objTarifa.iIdFolioTransitorio + ") THEN 'blue' ELSE 'red' END) + '-sm'' data-toggle=\"tooltip\" data-placement=\"top\" title =\"Ignorar Servicio\" ></span>" +
                    "        </div></div>' AS Controles " +
                    "    FROM tFactura tf JOIN tServicioFactura tsf ON tf.idFactura = tsf.idFactura " +
                    "        JOIN tCliente tc ON tf.idCliente = tc.idCliente WHERE tf.idSubReferencia = " + objTarifa.iIdSubReferencia + " AND tc.rfc NOT IN ( " +
                    "            SELECT rfc FROM tComitente ) AND tsf.idServicioFactura NOT IN ( " +
                    "            SELECT	 tetg.idServicioFactura FROM tEnvioTraficoGastos tetg ) ";

        // Ejecutar la consulta
        DataTable dtbGastos = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
        // Retornar datatable 
        return dtbGastos;
    }

    public void fn_ExcluirServicio(Tarifa objTarifa)
    {
        //instancia Conexión
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_ExcluirServicio", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //pasar parametros al sp
                objConexion.agregarParametroSP("@iIdFolioTransitorio", SqlDbType.Int, objTarifa.iIdFolioTransitorio.ToString());
                objConexion.agregarParametroSP("@iIdServicioFactura", SqlDbType.Int, objTarifa.iIdServicio.ToString());
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@sResOut");

                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objTarifa.iResultado = 1;
                    objTarifa.sMensaje = "Servicio excluido con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTarifa.iResultado = 0;
                    objTarifa.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //mensaje de error al caer en una excepción
                objTarifa.iResultado = 0;
                objTarifa.sMensaje = "Error con su base de datos, " + ex.Message.ToString();
            }
        }
        else
        {
            //mensaje de error con la base de datos
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Error con su base de datos, no existe procedimiento almacenado";
        }
    }

    public DataTable fn_ObtenerTablaImpuestos(Tarifa objTarifa)
    {
        // Instanciar clase conexion
        Conexion objConexion = new Conexion();
        // Consulta para obtener la datatable
        string sQuery = "SELECT " +
                        "        (SELECT tsr.refOperativa FROM tSubReferencia tsr WHERE tsr.idSubReferencia = ti.idSubreferencia), " +
                        "        (SELECT numPoliza FROM tPolizaEgreso tpe WHERE tpe.idPolizaEgreso = ti.idPoliza), " +
                        "        (SELECT  " +
                        "            (SELECT aduana FROM cAduana ca WHERE ca.idAduana = tp.idAduana) + '-' + " +
                        "            (SELECT patente FROM cPatente cp WHERE cp.idPatente = tp.idPatente) + '-' + " +
                        "            tp.pedimento " +
                        "        FROM tPedimento tp WHERE tp.idPedimento = ti.idPedimento), " +
                        "        CONVERT(VARCHAR, ti.fechaPago), " +
                        "         case when " +
                        "                        (select SC.cveSunServicio + ' - ' + descripcion from cServicio SC where SC.idServicio = (select top 1 tSC.idServicio from tServicioContribucion tSC where tSC.idContribucion =  ti.idServicio and tSC.idClientePrimario = tsr.idClienteOperativo and tSC.idClienteSecundario = tsr.idClienteContable )) is null " +
                        "                        then " +
                        "                         (select SC.cveSunServicio + ' - ' + descripcion from cServicio SC where SC.idServicio = (select top 1 tSC.idServicio from tServicioContribucion tSC where tSC.idContribucion =  ti.idServicio and tSC.idClientePrimario is null and tSC.idClienteSecundario is null )) " +
                        "                         else  " +
                        "                        (select SC.cveSunServicio + ' - ' + descripcion from cServicio SC where SC.idServicio = (select top 1 tSC.idServicio from tServicioContribucion tSC where tSC.idContribucion =  ti.idServicio and tSC.idClientePrimario = tsr.idClienteOperativo and tSC.idClienteSecundario = tsr.idClienteContable )) " +
                        "                        end , " +
                        "        CONVERT(VARCHAR, ti.Importe) " +
                        "    FROM " +
                        "        tImpuesto ti " +
                        "        JOIN " +
                        "        tSubReferencia tsr " +
                        "        ON ti.idSubReferenciaN = tsr.idSubReferencia " +
                        "        JOIN " +
                        "        tPolizaEgreso tp " +
                        "        ON ti.idPoliza = tp.idPolizaEgreso " +
                        "    WHERE " +
                        "        tsr.idSubreferencia = " + objTarifa.iIdSubReferencia + "  AND ti.idImpuesto NOT IN ( " +
                        "            SELECT	 " +
                        "                teti.idImpuesto " +
                        "            FROM " +
                        "                tEnvioTraficoImpuestos teti " +
                        "        ) " +
                        "    AND ti.idFormaPago = 1";
        // Ejecutar la consulta
        DataTable dtbGastos = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
        // Retornar datatable 
        return dtbGastos;
    }

    public DataTable fn_ObtenerTablaAnticipos(Tarifa objTarifa)
    {
        // Instanciar clase conexion
        Conexion objConexion = new Conexion();
        // Consulta para obtener la datatable
        string sQuery = "SELECT " +
                        "    (SELECT tsr.refOperativa FROM tSubReferencia tsr WHERE tsr.idSubReferencia = ta.idSubreferencia), " +
                        "    ta.montoTransaccion, " +
                        "    ta.fechaAnticipo, " +
                        "    (SELECT top 1 FECHA_INV FROM tAnticiposClientes_ION taci WHERE taci.refAdministrativa = (SELECT refAdministrativa FROM tSubReferencia tsr WHERE tsr.idSubReferencia = ta.idSubreferencia) and taci.monto = ta.monto and taci.fechaAnticipo = ta.fechaAnticipo order by  idAnticiposClientes_ION desc), " +
                        "    '<input id=''htxtGastos' + CONVERT(VARCHAR, ta.idAnticipo) + ''' type=''text'' dataID=''' + CONVERT(VARCHAR, ta.idAnticipo) + ''' class=''input-sm form-control htxtAnticipo'' value=''' + case when fGastosTerceros != 0 then CONVERT(VARCHAR,fGastosTerceros,128) else CONVERT(VARCHAR, fGastosTerceros) end + '''>', " +
                        "    '<input id=''htxtImpuestos' + CONVERT(VARCHAR, ta.idAnticipo) + ''' type=''text'' dataID=''' + CONVERT(VARCHAR, ta.idAnticipo) + ''' class=''input-sm form-control htxtAnticipo'' value=''' + CONVERT(VARCHAR, ta.fImpuestos) + '''>', " +
                        "    '<div class=''text-center''><span dataid=''' + CONVERT(VARCHAR, ta.idAnticipo) + ''' class=''fa fa-check btn-guardar-asignacion fa-green-sm''></span></div>' " +
                        "FROM " +
                        "    tAnticipo ta " +
                        "WHERE " +
                        "    ta.idSubreferencia = " + objTarifa.iIdSubReferencia;
        // Ejecutar la consulta
        DataTable dtbGastos = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
        // Retornar datatable 
        return dtbGastos;
    }

    private void fn_GenerarExcel(DataTable dtbExcel)
    {
        var lines = new List<string>();

        string[] columnNames = dtbExcel.Columns
            .Cast<DataColumn>()
            .Select(column => column.ColumnName)
            .ToArray();

        var header = string.Join(",", columnNames.Select(name => "\"" + name + "\""));
        lines.Add(header);

        var valueLines = dtbExcel.AsEnumerable()
            .Select(row => string.Join(",", row.ItemArray.Select(val => "\"" + val + "\"")));

        lines.AddRange(valueLines);

        File.WriteAllLines(HttpContext.Current.Server.MapPath("excel.csv"), lines);
    }

    public Utilerias fn_CargaMasiva(Tarifa objTarifa, bool bGuardar)
    {
        Utilerias objUtilerias = new Utilerias();
        objUtilerias.sNombre = "htblImpreciciones";
        // Obtiene la informacion a utilizar, proveniente de base de datos y del excel cargado
        DataTable dtbTarifas = fn_ObtenerTarifasExcel(objTarifa);
        // Quitar duplicados
        dtbTarifas = dtbTarifas.Select().Distinct().CopyToDataTable();
        // Validar excel
        bool bValido = objTarifa.fn_ValidarExcel(dtbTarifas);
        // Verifica si hubo impreciciones
        if (bValido)
        {
            objUtilerias.arrColumnasSinFiltro = new string[]
            {
                "descripcion", "valor", "correccion"
            };
            objUtilerias.fn_GeneraTabla(objUtilerias, dtbTarifas);
        }
        else
        {
            // Verifica si se tiene que guardar o primero se manda a confirmar
            if (bGuardar)
            {
                // Guardar en la base de datos

            }
            else
            {
                fn_GenerarExcel(dtbTarifas);
                // Mandar datatable para confirmar la adicion de las tarifas
                objUtilerias.arrColumnasFiltro = new string[]
                {
                    "Cliente", "RFC", "Aduana",
                    "Tipo de operación", "Tipo de tarifa", "Tipo de Material",
                    "Servicio a facturar", "Tipo", "Tarifa",  "VA", "GC", "IP", "IA", "IS", "PP", "GD", "IVA"


                };
                objUtilerias.arrColumnasSinFiltro = new string[] { };
                objUtilerias.fn_GeneraTabla(objUtilerias, dtbTarifas);
                objUtilerias.sContenido = "<div class='table-responsive'>" + objUtilerias.sContenido + "</div>";
            }
        }

        return objUtilerias;
    }

    #region Validaciones Carga Masiva

    private void fn_AgregarErrorRow(DataRow oRow, List<DataRow> lstRowsError, string sError, int iColumna)
    {
        oRow[iColumna] = sError;
        if (!lstRowsError.Contains(oRow))
        {
            lstRowsError.Add(oRow);
        }
    }

    private bool fn_ValidarColumnasCarga(DataTable dtbExcel, List<DataRow> lstRowsError,
                                        string sQuery, int iPosicion, string sMensajeVacio,
                                        string sMensajeNoEncontrado)
    {
        // Booleano que retorna en caso de error
        bool bValorRetorno = true;
        // Obtener todos los objetos contra los cuales se va a validar
        Conexion objConexion = new Conexion();
        DataTable dtbElementos = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);

        foreach (DataRow oRowExcel in dtbExcel.Rows)
        {
            // Verificar si el elemento esta vacio
            if (String.IsNullOrEmpty(oRowExcel[iPosicion].ToString()))
            {
                fn_AgregarErrorRow(oRowExcel, lstRowsError, sMensajeVacio, iPosicion);
                bValorRetorno = false;
                continue;
            }
            // Verificar si el elemento existe en la base de datos
            bool bExiste = false;
            foreach (DataRow oRowElemento in dtbElementos.Rows)
            {
                if (oRowExcel[iPosicion].ToString().Trim() == oRowElemento[1].ToString().Trim())
                {
                    bExiste = true;
                    break;
                }
            }
            if (!bExiste)
            {
                fn_AgregarErrorRow(oRowExcel,
                                   lstRowsError,
                                   sMensajeNoEncontrado + ": " + oRowExcel[iPosicion].ToString().Trim(),
                                   iPosicion);
                bValorRetorno = false;
            }
        }
        return bValorRetorno;
    }

    private bool fn_ValidarColumnasCarga<T>(DataTable dtbExcel, List<DataRow> lstRowsError,
                                            int iPosicion, string sMensajeVacio,
                                            string sMensajeNoCompatible)
    {
        // Booleano que retorna en caso de error
        bool bValorRetorno = true;

        foreach (DataRow oRowExcel in dtbExcel.Rows)
        {
            // Verificar si el elemento esta vacio
            if (String.IsNullOrEmpty(oRowExcel[iPosicion].ToString()))
            {
                fn_AgregarErrorRow(oRowExcel, lstRowsError, sMensajeVacio, iPosicion);
                bValorRetorno = false;
                continue;
            }
            try
            {
                var valorConvertido = (T)oRowExcel[iPosicion];
            }
            catch (InvalidCastException ice)
            {
                // Verificar si el elemento puede ser casteado al tipo especificado
                fn_AgregarErrorRow(oRowExcel, lstRowsError, sMensajeNoCompatible + ": " + oRowExcel[iPosicion].ToString().Trim(), iPosicion);
            }
        }
        return bValorRetorno;
    }

    private bool fn_ValidarClientesCarga(DataTable dtbExcel, List<DataRow> lstRowsError)
    {
        return
            fn_ValidarColumnasCarga(dtbExcel,
            lstRowsError,
            @"SELECT idCliente, rfc FROM tCliente",
            1,
            "RFC VACIO",
            "RFC NO ENCONTRADO");
    }

    private bool fn_ValidarAduanasCarga(DataTable dtbExcel, List<DataRow> lstRowsError)
    {
        return
            fn_ValidarColumnasCarga(dtbExcel,
            lstRowsError,
            @"SELECT idAduana, aduana FROM cAduana",
            2,
            "ADUANA VACIA",
            "ADUANA NO ENCONTRADA");
    }

    private bool fn_ValidarOperacionCarga(DataTable dtbExcel, List<DataRow> lstRowsError)
    {
        return
            fn_ValidarColumnasCarga(dtbExcel,
            lstRowsError,
            @"SELECT idTipoOperacion, tipoOperacion FROM cTipoOperacion",
            3,
            "TIPO OPERACION VACIO",
            "TIPO OPERACION NO ENCONTRADO");
    }

    private bool fn_ValidarTarifaCarga(DataTable dtbExcel, List<DataRow> lstRowsError)
    {
        return
            fn_ValidarColumnasCarga(dtbExcel,
            lstRowsError,
            @"SELECT idTipoTarifa, nombre FROM cTipoTarifa",
            4,
            "TARIFA VACIA",
            "TARIFA NO ENCONTRADA");
    }

    private bool fn_ValidarMaterialCarga(DataTable dtbExcel, List<DataRow> lstRowsError)
    {
        return
            fn_ValidarColumnasCarga(dtbExcel,
            lstRowsError,
            @"SELECT idTipoMaterial, nombre FROM cTipoMaterial",
            5,
            "MATERIAL VACIO",
            "MATERIAL NO ENCONTRADO");
    }

    private bool fn_ValidarServicioCarga(DataTable dtbExcel, List<DataRow> lstRowsError)
    {
        return
            fn_ValidarColumnasCarga(dtbExcel,
            lstRowsError,
            @"SELECT idServicio, descripcion FROM cServicio",
            6,
            "SERVICIO VACIO",
            "SERVICIO NO ENCONTRADO");
    }

    private bool fn_ValidarReglaCarga(DataTable dtbExcel, List<DataRow> lstRowsError)
    {
        return
            fn_ValidarColumnasCarga(dtbExcel,
            lstRowsError,
            @"SELECT idTipoRangoTarifa, nombre FROM cTipoRangoTarifa",
            7,
            "TIPO VACIO",
            "TIPO NO ENCONTRADO");
    }

    private bool fn_ValidarMontoCarga(DataTable dtbExcel, List<DataRow> lstRowsError)
    {
        return
            fn_ValidarColumnasCarga<double>(dtbExcel,
            lstRowsError,
            8,
            "MONTO VACIO",
            "MONTO NO VALIDO");
    }

    private bool fn_ValidarMonedaCarga(DataTable dtbExcel, List<DataRow> lstRowsError)
    {
        return
            false;
    }

    private bool fn_ValidarIvaCarga(DataTable dtbExcel, List<DataRow> lstRowsError)
    {
        return false;
    }

    private bool fn_ValidarRetencionCarga(DataTable dtbExcel, List<DataRow> lstRowsError)
    {
        return false;
    }

    private bool fn_ValidarCobroCarga(DataTable dtbExcel, List<DataRow> lstRowsError)
    {
        return false;
    }

    private bool fn_ValidarBaseCarga(DataTable dtbExcel, List<DataRow> lstRowsError)
    {
        return false;
    }

    private bool fn_ValidarValorCarga(DataTable dtbExcel, List<DataRow> lstRowsError)
    {
        return false;
    }

    private Task fn_ValidarEnTask(Func<DataTable, List<DataRow>, bool> fn_AEjecutar, TaskFactory<bool> oTaskFactory, DataTable dtbExcel, List<DataRow> lstErrores)
    {
        return oTaskFactory.StartNew(() =>
        {
            return fn_AEjecutar(dtbExcel, lstErrores);
        });
    }

    private bool fn_ValidarExcel(DataTable dtbExcel)
    {
        // Se crea una lista para almacenar los errores
        List<DataRow> lstErrores = new List<DataRow>();
        /*
         * Se crea un arreglo de tareas que se ejecutaran, eso se hace por medio
         * del metodo fn_ValidarEnTask que funciona de tal manera que crea una nueva 
         * tarea y ejecuta el metodo que se le pasa en el primer parametro
         * */
        Task[] arrTareas = new Task[]
        {
            fn_ValidarEnTask(fn_ValidarClientesCarga, Task<bool>.Factory, dtbExcel, lstErrores),
            fn_ValidarEnTask(fn_ValidarAduanasCarga, Task<bool>.Factory, dtbExcel, lstErrores),
            fn_ValidarEnTask(fn_ValidarOperacionCarga, Task<bool>.Factory, dtbExcel, lstErrores),
            fn_ValidarEnTask(fn_ValidarTarifaCarga, Task<bool>.Factory, dtbExcel, lstErrores),
            fn_ValidarEnTask(fn_ValidarMaterialCarga, Task<bool>.Factory, dtbExcel, lstErrores),
            fn_ValidarEnTask(fn_ValidarServicioCarga, Task<bool>.Factory, dtbExcel, lstErrores),
            fn_ValidarEnTask(fn_ValidarReglaCarga, Task<bool>.Factory, dtbExcel, lstErrores),
            fn_ValidarEnTask(fn_ValidarMonedaCarga, Task<bool>.Factory, dtbExcel, lstErrores),
            fn_ValidarEnTask(fn_ValidarMontoCarga, Task<bool>.Factory, dtbExcel, lstErrores),
            fn_ValidarEnTask(fn_ValidarIvaCarga, Task<bool>.Factory, dtbExcel, lstErrores),
            fn_ValidarEnTask(fn_ValidarRetencionCarga, Task<bool>.Factory, dtbExcel, lstErrores),
            fn_ValidarEnTask(fn_ValidarCobroCarga, Task<bool>.Factory, dtbExcel, lstErrores),
            fn_ValidarEnTask(fn_ValidarBaseCarga, Task<bool>.Factory, dtbExcel, lstErrores),
            fn_ValidarEnTask(fn_ValidarValorCarga, Task<bool>.Factory, dtbExcel, lstErrores),
        };

        // Esperar a que todas las tareas terminen
        Task.WaitAll(arrTareas);

        // Recorrer los resultados de las tareas para ver si estan bien
        foreach (Task<bool> oTarea in arrTareas)
        {
            // Si alguna de las tareas retorno falso, el metodo de validar retorna entonces falso
            if (!oTarea.Result)
                return false;
        }

        // Retorna true como caso base
        return true;
    }

    #endregion

    private DataTable fn_ObtenerTarifasExcel(Tarifa objTarifa)
    {
        string sRuta = HttpContext.Current.Server.MapPath("../../Documentos/Tarifas/" + objTarifa.sFileName.Split('.')[0] + "/" + objTarifa.sFileName);
        ExcelData objExcelData = new ExcelData(sRuta);
        return objExcelData.getData(true).CopyToDataTable();
    }

    public int LevenshteinDistance(string s, string t, out double porcentaje)
    {
        porcentaje = 0;

        // d es una tabla con m+1 renglones y n+1 columnas
        int costo = 0;
        int m = s.Length;
        int n = t.Length;
        int[,] d = new int[m + 1, n + 1];

        // Verifica que exista algo que comparar
        if (n == 0) return m;
        if (m == 0) return n;

        // Llena la primera columna y la primera fila.
        for (int i = 0; i <= m; d[i, 0] = i++) ;
        for (int j = 0; j <= n; d[0, j] = j++) ;


        /// recorre la matriz llenando cada unos de los pesos.
        /// i columnas, j renglones
        for (int i = 1; i <= m; i++)
        {
            // recorre para j
            for (int j = 1; j <= n; j++)
            {
                /// si son iguales en posiciones equidistantes el peso es 0
                /// de lo contrario el peso suma a uno.
                costo = (s[i - 1] == t[j - 1]) ? 0 : 1;
                d[i, j] = System.Math.Min(System.Math.Min(d[i - 1, j] + 1,  //Eliminacion
                              d[i, j - 1] + 1),                             //Insercion 
                              d[i - 1, j - 1] + costo);                     //Sustitucion
            }
        }

        /// Calculamos el porcentaje de cambios en la palabra.
        if (s.Length > t.Length)
            porcentaje = ((double)d[m, n] / (double)s.Length);
        else
            porcentaje = ((double)d[m, n] / (double)t.Length);
        return d[m, n];
    }

    public bool fn_CalcularParecidoPalabra(string sBase, string sComparador, double dPorcentajeAceptable, out double dPorcentajePenalizacion)
    {
        // Comparamos si son iguales y devolvemos
        if (sBase == sComparador)
        {
            dPorcentajePenalizacion = 0;
            return true;
        }

        // Declaramos variables de coincidencias
        int iCoincidencias = 0;
        int iComparaciones = 0;

        // Recorre la palabra base
        for (int i = 0; i < sBase.Length; i++)
        {
            // Verifica si se puede tomar un par en base al tamaño restante
            if (sBase.Length - i >= 2)
            {
                // Se toma un par de letras y se busca en el comparador
                if (sComparador.Contains(sBase.Substring(i, 2)))
                {
                    // Si se contiene se suma 1 al numero de coincidencias
                    iCoincidencias++;
                }
                // Se suma 1 al total de comparaciones
                iComparaciones++;
            }
        }

        // Penalizar por diferencia de tamaño
        int iDiferencia = sBase.Length - sComparador.Length;
        iDiferencia = Math.Abs(iDiferencia);

        // Por cada 2 letras de diferencia se hace 1 penalizacion
        iDiferencia /= 2;
        iCoincidencias -= iDiferencia;

        // Si las comparaciones es 0 se devuelve 0
        if (iComparaciones == 0)
        {
            dPorcentajePenalizacion = 1;
            return false;
        }

        // Se calcula el porcentaje
        double dPorcentaje = Convert.ToDouble(iCoincidencias) / Convert.ToDouble(iComparaciones);
        dPorcentajePenalizacion = 1 - dPorcentaje;
        // Si el porcentaje es mayor al porcentaje aceptado se devuelve true
        return dPorcentaje >= dPorcentajeAceptable;
    }

    public bool fn_CalcularParecidoFrase(string sBase, string sComparador, double dPorcentajeAceptable, out double dPorcentaje)
    {
        // Declaramos variables de coincidencias
        int iCoincidencias = 0;
        int iComparaciones = 0;

        // Declaramos variable de penalizacion por diferencia de palabras parecidas
        double dPorcentajePenalizacion = 0;
        double dPenalizacion = 0;

        // Recorre cada palabra de la palabra base
        foreach (string sPalabra in sBase.Split(' '))
        {
            if (sPalabra.Length < 3)
                continue;
            // Verifica si existe en el comparador
            foreach (string sPalabraComparador in sComparador.Split(' '))
            {
                if (sPalabraComparador.Length < 3)
                    continue;

                LevenshteinDistance(sPalabra, sPalabraComparador, out dPorcentajePenalizacion);

                if (dPorcentajePenalizacion < 0.25)
                {
                    dPenalizacion += dPorcentajePenalizacion;
                    iCoincidencias++;
                    break;
                }
            }
            iComparaciones++;
        }

        // Si las comparaciones es 0 se devuelve 0
        if (iComparaciones == 0)
        {
            dPorcentaje = 1;
            return true;
        }

        // Se calcula el porcentaje
        dPorcentaje = (Convert.ToDouble(iCoincidencias) / Convert.ToDouble(iComparaciones)) - dPenalizacion;
        // Si el porcentaje es mayor al porcentaje aceptable se devuelve true
        return dPorcentaje >= dPorcentajeAceptable;
    }


}
