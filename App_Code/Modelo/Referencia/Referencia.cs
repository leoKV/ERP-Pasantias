using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Referencia
/// </summary>
public class Referencia
{
    public Referencia() { }

    //Se declaran las variables de la clase Referencia
    public int iIdFolioT { get; set; }
    public string arrIdNotaCredito { get; set; }
    public int iIdReferencia { get; set; }
    public int iIdSubReferencia { get; set; }
    public string sIdSubReferencia { get; set; }
    public string sNoReferenciaAdmin { get; set; }
    public string sNoReferenciaOpe { get; set; }
    public string sTipoOperacion { get; set; }
    public string sDenominacion { get; set; }
    public int iIdTipoOperacion { get; set; }
    public string sTipoReferencia { get; set; }
    public int iTipoReferencia { get; set; }
    public string sFechaAlta { get; set; }
    public string sEstatus { get; set; }
    // ALS
    public string sFechacerrada { get; set; }
    public int iIdEstatus { get; set; }
    public string sPedimento { get; set; }
    public int iPedimento { get; set; }
    public string sClienteOpe { get; set; }
    public int iIdClienteOperativo { get; set; }
    public int iIdClienteContable { get; set; }
    public string sClienteConta { get; set; }
    public string sCvePedimento { get; set; }
    public int iIdCvePedimento { get; set; }
    public int iIdPlanta { get; set; }
    public string sPlanta { get; set; }
    public string arrIdFactura { get; set; }
    public int iTotal { get; set; }
    public int iIdPatente { get; set; }
    public string sPatente { get; set; }
    public int iIdAduana { get; set; }
    public string sAduana { get; set; }
    public int iReferencia { get; set; }
    public string[] arrPlantasCliente { get; set; }
    public int iIdFactura { get; set; }
    public string sClienteOpeRfc { get; set; }
    public int iTotalServiciosReferencia { get; set; }
    public string sTotalSolicitudTransferencia { get; set; }
    public int iTotalServiciosFactura { get; set; }
    public int iIdServicioReferencia { get; set; }
    public string sMotivo { get; set; }
    public string sPedimentoAdi { get; set; }
    public int iCosto { get; set; }
    public DataTable dtbSubReferencias { get; set; }
    public List<string> lstAduanas { get; set; }

    public string sCVEPedimento { get; set; }
    public string sFechaETA { get; set; }
    public string sFechaPago { get; set; }
    public string sImpPagados { get; set; }
    public string sImpuestosSubcidiados { get; set; }
    public string sPesoBruto { get; set; }
    public string sPesoNeto { get; set; }
    public string sVolumen { get; set; }
    public string sDescripcionMercancia { get; set; }
    public string sClaseMercancia { get; set; }
    public string sMarcaMercancia { get; set; }
    public string sObservaciones { get; set; }
    public string sValorAduana { get; set; }
    public string sBulto { get; set; }
    public string sImpAfianzados { get; set; }
    public string sPartidaPresupuestal { get; set; }
    public string sBarco { get; set; }
    public string sNumeroViaje { get; set; }
    public string sEmbarqueHouse { get; set; }
    public string sEmbarqueMaster { get; set; }
    public string sTransportista { get; set; }
    public string sOficina { get; set; }
    public string sNumeroPlantaDestino { get; set; }
    public string sGastosDirectosCliente { get; set; }
    public int iIdTipoReferencia { get; set; }
    public string sTotalFactura { get; set; }
    public string sTotalFacturaUSD { get; set; }
    public int iIdServicio { get; set; }
    public int iType { get; set; }
    public int iIdOficina { get; set; }
    public int iEspecial { get; set; }

    public int idTarifaReferencia { get; set; }
    public string sFolioTransitorio { get; set; }
    public string sNombreTarifa { get; set; }
    public string sCampo { get; set; }
    public int iImpuesto { get; set; }
    public string sTotalImpuesto { get; set; }
    public int iAnticipo { get; set; }
    public int iFactura { get; set; }
    public int iNotasC { get; set; }

    public int iIdAanticipo { get; set; }
    public double fGastoTercero { get; set; }

    //Se declaran atributos generales
    public int iResultado { get; set; }
    public string sMensaje { get; set; }
    public string sContenido { get; set; }
    public int iIdUsuario { get; set; }

    public int iValido { get; set; }
    public string sRuta { get; set; }
    public string sRutaDat { get; set; }
    public string sFechaInicio { get; set; }
    public string sFechaFin { get; set; }

    //variables para Corresponsales
    public string sTotalCuentaGastos { get; set; }
    public string sExtension { get; set; }
    public List<int> lstSubReferencias { get; private set; }
    public string sRutaDG { get; set; }

    //Variables REFfact
    public string sFacturasRelacionadas { get; set; }
    public int iCantFacturasRelacionadas { get; set; }
    public int iTipoSeleccion { get; set; }

    //Variable para las facturas
    public string sFacturas { get; set; }
    public string sIdNuevaSubreferencia { get; set; }

    //Variables REFIMPUESTOS
    public string sImpuestosRelacionados { get; set; }
    public int iCantImpuestosRelacionados { get; set; }
    public string sImpuestos { get; set; }
    public int iIdImpuesto { get; set; }

    //Variables HONORARIOS
    public string sHonorariosRelacionados { get; set; }
    public int iCantHonorariosRelacionados { get; set; }

    //Variables p. reporte
    public string sFechaElaboracion { get; set; }
    public string sCliente { get; set; }
    public string sGastosTerceros { get; set; }
    public string sGastosNoComprobados { get; set; }
    public string sTotalGastos { get; set; }
    public string sAnticipo { get; set; }
    public string sTotal { get; set; }
    public string sFechaImportacion { get; set; }
    public string sFechaEntrada { get; set; }
    public string sMaterial { get; set; }
    public string sCierreOperativo { get; set; }
    public string sIdEstatus { get; set; }
    //AGREGADOS REPORTE DEPOSITOS EN GARANTIA
    public string sFechaAnt { get; set; }
    public string sMonedaAn { get; set; }
    public string sTipoCambioAn { get; set; }
    public string sFechastf { get; set; }
    public string sTMonedastf { get; set; }
    public string sTCambiostf { get; set; }
    public string sMontoTstf { get; set; }
    public string sFechaDemora { get; set; }
    public string sTMonedaDemora { get; set; }
    public string sTCambioDemora { get; set; }
    public string sRfcBeneficiario { get; set; }
    public string sBeneficiario { get; set; }
    public string sMonedaDep { get; set; }
    public string sTcambioDep { get; set; }

    public int iTieneIdSubReferencia { get; set; }

    //Tabla para mostrar desglose de facturas
    public string sDesgloseFacturas { get; set; }
    public string sDemoras { get; internal set; }
    public string sPerdidaCambiaria { get; internal set; }
    public string sRecuperacion { get; internal set; }
    public string sFechaRecuperacion { get; internal set; }
    public string sReembolso { get; internal set; }
    public string sFechaReembolso { get; internal set; }

    /// <summary>
    /// Método obtener servicios
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ObtenerRelacionServicios(Referencia objReferencia)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sNoReferenciaAdmin", "sTipoOperacion", "sAduana", "sPatente", "sPedimento", "sClienteOpe", "sTipoReferencia" };
        //Se crea la consulta
        string sQuery = "SELECT refAdministrativa AS sNoReferenciaAdmin, (SELECT ct.tipoOperacion FROM cTipoOperacion ct WHERE ct.idTipoOperacion=tsubr.idTipoOperacion) AS sTipoOperacion, " +
        "(SELECT ca.aduana FROM cAduana ca WHERE ca.idAduana=tsubr.idAduana) AS sAduana, (SELECT cp.patente FROM cPatente cp WHERE cp.idPatente=tsubr.idPatente) AS sPatente, " +
        "CASE WHEN pedimento = 0 THEN null ELSE pedimento END AS sPedimento, (SELECT tc.nomCliente FROM tCliente tc WHERE tc.idCliente=tsubr.idClienteOperativo) AS sClienteOpe, " +
        "(SELECT ct.nomTipoReferencia FROM cTipoReferencia ct WHERE ct.idTipoReferencia=tsubr.idTipoReferencia) AS sTipoReferencia FROM tSubReferencia tsubr WHERE tsubr.idSubReferencia=" + objReferencia.iIdSubReferencia;
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<Referencia>(sQuery, arrAtributos, objReferencia);
        //Se asigna el resultado
        objReferencia.iResultado = 1;
    }

    /// <summary>
    /// Método para validar que la servicio referencia no exista
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ValidarServicioReferernciaExiste(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "SELECT COUNT(*) FROM tServicioReferencia WHERE idSubReferencia=" + objReferencia.iIdSubReferencia + " and idServicio=" + objReferencia.iIdServicio;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objReferencia.iResultado = int.Parse(sRes[1]);
        //Se retorna mensaje en caso de que el ciente ya este asignado
        objReferencia.sMensaje = "El servicio ya está asignado a la referencia.";
    }

    /// <summary>
    /// Método para guardar el servicio de la referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_GuardarServicioReferencia(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarServicioReferenciaManual", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objReferencia.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@iIdServicio", SqlDbType.Int, objReferencia.iIdServicio.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    objReferencia.sMensaje = "Servicio guardado con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para guardar el relación servicio aduana
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_RelacionServicioAduana(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarRelacionServicioAduana", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objReferencia.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@iIdServicio", SqlDbType.Int, objReferencia.iIdServicio.ToString());
                objConexion.agregarParametroSP("@iType", SqlDbType.Int, objReferencia.iType.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    //Se verifica la accion que se realizó
                    if (objReferencia.iType == 1)
                        objReferencia.sMensaje = "Aduana asginada con éxito.";
                    else
                        objReferencia.sMensaje = "Aduana desasignada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para guardar el relación servicio cliente
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_RelacionServicioCliente(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarRelacionServicioCliente", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objReferencia.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@iIdServicio", SqlDbType.Int, objReferencia.iIdServicio.ToString());
                objConexion.agregarParametroSP("@iType", SqlDbType.Int, objReferencia.iType.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    //Se verifica la accion que se realizó
                    if (objReferencia.iType == 1)
                        objReferencia.sMensaje = "Cliente asginado con éxito.";
                    else
                        objReferencia.sMensaje = "Cliente desasignado con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para obtener datos de la referencia
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_ObtenerDatosReferencia(Referencia objReferencia)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sNoReferenciaAdmin", "sNoReferenciaOpe", "sPedimento", "sTipoOperacion", "sTipoReferencia", "sAduana", "sFechaAlta", "sEstatus", "sFechacerrada", "sCvePedimento", "sClienteOpe", "sClienteConta", "sPlanta", "sClienteOpeRfc", "sPedimentoAdi", "iCosto", "iEspecial" };
        //Se crea la consulta
        string sQuery = @"SELECT
                              tsubr.refAdministrativa AS sNoReferenciaAdmin,
                              tsubr.refOperativa AS sNoReferenciaOpe,
                              (SELECT ca.aduana FROM cAduana ca WHERE ca.idAduana=tsubr.idAduana) + '-' + (SELECT cp.patente FROM cPatente cp WHERE cp.idPatente=tsubr.idPatente) + ( case when (tsubr.pedimento<>0) then'-' + CONVERT(VARCHAR(10),tsubr.pedimento) END ) AS sPedimento, 
                              (SELECT ct.tipoOperacion FROM cTipoOperacion ct WHERE ct.idTipoOperacion=tsubr.idTipoOperacion) AS sTipoOperacion, 
                              (SELECT ct.nomTipoReferencia FROM cTipoReferencia AS ct WHERE ct.idTipoReferencia=tsubr.idTipoReferencia) AS sTipoReferencia,
                              (select aduana from cAduana where idAduana=tsubr.idAduana) AS sAduana ,
                              CONVERT(VARCHAR(10),tsubr.fechaAlta,103) AS sFechaAlta, 
                              (SELECT ce.nomEstatusReferencia FROM cEstatusReferencia ce WHERE ce.idEstatusReferencia=tsubr.idEstatusReferencia) AS sEstatus,
                              (select max(CONVERT(varchar,fecha,103)) fecha from tLogSubReferencia where idSubReferencia=tsubr.idSubReferencia and idEstatusReferencia in (16) group by idSubReferencia) AS sFechacerrada,
                              (SELECT cc.cve FROM cClavesPedimento cc WHERE cc.idCvePedimento=tsubr.idCvePedimento) AS sCvePedimento, 
                              (SELECT tc.nomCliente FROM tCliente tc WHERE tc.idCliente=tsubr.idClienteOperativo) AS sClienteOpe, 
                              (SELECT tc.nomCliente FROM tCliente tc WHERE tc.idCliente=tsubr.idClienteContable) AS sClienteConta, 
                              (SELECT tp.nomPlanta FROM tPlanta tp WHERE tp.idPlanta=tsubr.idPlanta) AS sPlanta, 
                              (SELECT tc.rfc FROM tCliente tc WHERE tc.idCliente=tsubr.idClienteContable) AS sClienteOpeRfc,
                              isnull(tsubr.pedimento,'') AS sPedimentoAdi ,
                              isnull((SELECT CASE WHEN (tc.rfc in(select rfc from tComitente)) 
                              THEN 1 ELSE tc.Costo END FROM tCliente tc WHERE tc.idCliente = tsubr.idClienteContable),'0') AS iCosto,
                              (select isnull(especial,0) from tCliente tc where tc.idCliente=tsubr.idClienteContable) AS iEspecial 
                              FROM tReferencia tr inner join tSubReferencia tsubr ON tr.idReferencia=tsubr.idReferencia 
                              WHERE tsubr.idSubReferencia="+ objReferencia.iIdSubReferencia;
        //string sQuery = "SELECT tsubr.refAdministrativa AS sNoReferenciaAdmin, tsubr.refOperativa AS sNoReferenciaOpe, (SELECT ca.aduana FROM cAduana ca WHERE ca.idAduana=tsubr.idAduana) + '-' + (SELECT cp.patente FROM cPatente cp WHERE cp.idPatente=tsubr.idPatente) + ( case when (tsubr.pedimento<>0) then'-' + CONVERT(VARCHAR(10),tsubr.pedimento) END ) AS sPedimento, (SELECT ct.tipoOperacion FROM cTipoOperacion ct WHERE ct.idTipoOperacion=tsubr.idTipoOperacion) AS sTipoOperacion, " +
        //"(SELECT ct.nomTipoReferencia FROM cTipoReferencia AS ct WHERE ct.idTipoReferencia=tsubr.idTipoReferencia) AS sTipoReferencia,(select aduana from cAduana where idAduana=tsubr.idAduana) AS sAduana ,CONVERT(VARCHAR(10),tsubr.fechaAlta,103) AS sFechaAlta, (SELECT ce.nomEstatusReferencia FROM cEstatusReferencia ce WHERE ce.idEstatusReferencia=tsubr.idEstatusReferencia) AS sEstatus, (SELECT cc.cve FROM cClavesPedimento cc WHERE cc.idCvePedimento=tsubr.idCvePedimento) AS sCvePedimento, " +
        //"(SELECT tc.nomCliente FROM tCliente tc WHERE tc.idCliente=tsubr.idClienteOperativo) AS sClienteOpe, (SELECT tc.nomCliente FROM tCliente tc WHERE tc.idCliente=tsubr.idClienteContable) AS sClienteConta, (SELECT tp.nomPlanta FROM tPlanta tp WHERE tp.idPlanta=tsubr.idPlanta) AS sPlanta, (SELECT tc.rfc FROM tCliente tc WHERE tc.idCliente=tsubr.idClienteContable) AS sClienteOpeRfc,isnull(tsubr.pedimento,'') AS sPedimentoAdi " +
        //",isnull((SELECT CASE WHEN (tc.rfc in(select rfc from tComitente)) " +
        //"THEN 1 ELSE tc.Costo END FROM tCliente tc WHERE tc.idCliente = tsubr.idClienteContable),'0') AS iCosto," +
        //"(select isnull(especial,0) from tCliente tc where tc.idCliente=tsubr.idClienteContable) AS iEspecial " +
        //"FROM tReferencia tr inner join tSubReferencia tsubr ON tr.idReferencia=tsubr.idReferencia " +
        //" WHERE tsubr.idSubReferencia=" + objReferencia.iIdSubReferencia;
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<Referencia>(sQuery, arrAtributos, objReferencia);
        //Se asigna el resultado
        objReferencia.iResultado = 1;
    }

    /// <summary>
    /// Método para guardar el referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_GuardarReferencia(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarReferenciaEditada", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objReferencia.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@dFechaAlta", SqlDbType.DateTime, objReferencia.sFechaAlta);
                objConexion.agregarParametroSP("@iIdEstatus", SqlDbType.Int, objReferencia.iIdEstatus.ToString());
                objConexion.agregarParametroSP("@iIdTipoOperacion", SqlDbType.Int, objReferencia.iIdTipoOperacion.ToString());
                objConexion.agregarParametroSP("@iIdCvePedimento", SqlDbType.Int, objReferencia.iIdCvePedimento.ToString());
                objConexion.agregarParametroSP("@iIdPlanta", SqlDbType.Int, objReferencia.iIdPlanta.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    objReferencia.sMensaje = "Referencia guardada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para guardar el referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_AgregarFacturaReferencia(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_AgregarFacturaReferencia", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objReferencia.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@arrIdFactura", SqlDbType.VarChar, objReferencia.arrIdFactura + ",");
                objConexion.agregarParametroSP("@iTotal", SqlDbType.Int, objReferencia.iTotal.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    objReferencia.sMensaje = "Facturas agregadas con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para validar número de referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ValidarNumeroReferencia(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        //string sQuery = "SELECT COUNT(*) FROM tSubReferencia tsr WHERE tsr.refAdministrativa='" + objReferencia.sNoReferenciaAdmin + "' and tsr.refOperativa='" + objReferencia.sNoReferenciaOpe + "'";
        string sQuery = "SELECT COUNT(*) FROM tSubReferencia tsr WHERE tsr.refAdministrativa='" + objReferencia.sNoReferenciaAdmin + "'";
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // revisa si la referencia administrativa ya existe
        if (int.Parse(sRes[1]) > 0)
        {
            // ya existe
            sQuery = "SELECT COUNT(*) FROM tSubReferencia tsr WHERE tsr.refOperativa='" + objReferencia.sNoReferenciaOpe + "'";
            sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            // revisa si la referencia operativa ya existe
            if (int.Parse(sRes[1]) > 0)
            {
                // ya existe
                sQuery = "SELECT COUNT(*) FROM tSubReferencia tsr WHERE tsr.refAdministrativa='" + objReferencia.sNoReferenciaAdmin + "' and tsr.refOperativa='" + objReferencia.sNoReferenciaOpe + "'";
                sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
                // revisa si ya existe relación entre referencia administrativa y operativa
                if (int.Parse(sRes[1]) > 0)
                {
                    // regresa 1 por que es correcto y va a actualizar la referencia
                    iResultado = 1;

                    objReferencia.sMensaje = "Referencia Correcta";
                }
                else
                {
                    // regresa resultado negativo
                    iResultado = 0;
                    // regresa error por que estan las referencias pero no estan juntas
                    objReferencia.sMensaje = "Tanto la referencia administrativa como Operativa existen en el sistema," +
                        "sin embargo, tienen diferente combinación, favor de validar con el administrador";
                }
            }
            else
            {
                // regresa resultado negativo
                iResultado = 0;
                //la referencia operativa no existe, por eso sacamos con la que ya esta la administrativa
                sQuery = "SELECT top 1 refOperativa FROM tSubReferencia tsr WHERE tsr.refAdministrativa='" + objReferencia.sNoReferenciaAdmin + "'";
                sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
                // regresa error por que estan la administrativa pero con otra operativa
                objReferencia.sMensaje = " la referencia administrativa existe ya en el sistema con la Operativa: " + sRes[1] +
                    " no se puede tener la misma referencia administrativa con diferente operativa";
            }
        }
        else
        {
            // valida si la referencia operativa ya existe
            sQuery = "SELECT COUNT(*) FROM tSubReferencia tsr WHERE tsr.refOperativa='" + objReferencia.sNoReferenciaOpe + "'";
            sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            // revisa si la referencia operativa ya existe
            if (int.Parse(sRes[1]) > 0)
            {
                //la referencia administrativa no existe, pero si la operativa
                sQuery = "SELECT top 1 refAdministrativa FROM tSubReferencia tsr WHERE tsr.refOperativa='" + objReferencia.sNoReferenciaOpe + "'";
                sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
                // regresa error por que estan la operativa pero con otra administrativa
                objReferencia.sMensaje = " la referencia Operativa existe ya en el sistema con la Administrativa: " + sRes[1] +
                    " no se puede tener la misma referencia Operativa con diferente Administrativa";
            }
            else
            {
                // no la operativa y ni la administrativa estan, por lo cual es una nueva referencia
                // regresa 1 por que es correcto y va a actualizar la referencia
                iResultado = 1;
                objReferencia.sMensaje = "Referencia Correcta";
            }
        }
    }

    public void fn_CerrarReferencias(Referencia objReferencia)
    {
        try
        {
            ExcelData objExcelData = new ExcelData(objReferencia.sRuta);
            DataTable dtExcel = objExcelData.getData(true).CopyToDataTable();
            dtExcel = (from row in dtExcel.AsEnumerable()
                       where row.Field<object>(0) != null && !String.IsNullOrEmpty(row.Field<object>(0).ToString().Trim())
                       select row).CopyToDataTable();
            string sRefOperativa, sTipoFolio, sFolioTransitorio, sFallidas = "";
            objReferencia.lstSubReferencias = new List<int>();
            objReferencia.sEstatus = "Cancelada";

            foreach (DataRow dr in dtExcel.Rows)
            {
                objReferencia.sNoReferenciaOpe = dr.ItemArray[0].ToString();
                if (objReferencia.fn_ValidarCancelacion(objReferencia))
                {
                    objReferencia.fn_CambiarEstatusReferencia(objReferencia);
                }
                else
                {
                    sFallidas = sFallidas + objReferencia.sNoReferenciaOpe + " " + objReferencia.sMensaje + "| ";
                }
                //sRefOperativa = dr.ItemArray[0].ToString();
                //if (fn_RevisarEstatusReferencia(sRefOperativa, objReferencia))
                //{
                //    objReferencia.lstSubReferencias.Add(objReferencia.iIdSubReferencia);
                //    objReferencia.fn_CerrarReferencia(sRefOperativa, objFolioTransitorio.iIdUsuario.ToString());
                //    objReferencia.lstSubReferencias.Clear();
                //}
                //else
                //{
                //    sFallidas = sFallidas + sRefOperativa + ": Se encuentra en un estatus diferente a Abierta. | ";
                //}
            }
            objReferencia.iResultado = 1;
            objReferencia.sObservaciones = sFallidas.TrimEnd('|');
            objReferencia.sMensaje = "Se han procesado todas las referencias.";
        }
        catch (Exception ex)
        {
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "Ha ocurrido un error al procesar las referencias: " + ex.Message;
        }
    }

    private bool fn_ValidarCancelacion(Referencia objReferencia)
    {
        try
        {
            Conexion objConexion = new Conexion();
            string sQuery = @"SELECT COUNT(refOperativa) FROM tSubReferencia WHERE refOperativa='" + objReferencia.sNoReferenciaOpe + "'";
            string[] sRes;
            sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            if (int.Parse(sRes[1]) > 0)
            {
                sQuery = @"SELECT COUNT(refOperativa) FROM tSubReferencia WHERE idEstatusReferencia IN (2,15) AND refOperativa='" + objReferencia.sNoReferenciaOpe + "'";
                sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
                if (int.Parse(sRes[1]) > 0)
                {
                    sQuery = @"SELECT COUNT(tsr.refOperativa) FROM tSubReferencia tsr
                                INNER JOIN tSolicitudTransferencia tst
	                                ON tst.idSubreferencia=tsr.idSubReferencia
                                WHERE refOperativa='" + objReferencia.sNoReferenciaOpe + "' AND tst.idEstatusSolicitudTrans!=5";
                    sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
                    if (int.Parse(sRes[1]) < 1)
                    {
                        sQuery = @"SELECT COUNT(tsr.refOperativa) FROM tSubReferencia tsr
                                    INNER JOIN tFactura tf 
	                                    ON tf.idSubReferencia=tsr.idSubReferencia
                                    WHERE refOperativa='" + objReferencia.sNoReferenciaOpe + "'";
                        sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
                        if (int.Parse(sRes[1]) < 1)
                        {
                            sQuery = @"SELECT COUNT(tsr.refOperativa) FROM tSubReferencia tsr
                                        INNER JOIN tImpuesto ti
	                                        ON ti.idSubreferenciaN=tsr.idSubReferencia
                                        WHERE refOperativa='" + objReferencia.sNoReferenciaOpe + "'";
                            sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
                            if (int.Parse(sRes[1]) < 1)
                            {
                                return true;
                            }
                            else
                            {
                                objReferencia.iResultado = 0;
                                objReferencia.sMensaje = "La referencia cuenta con gastos.";
                                return false;
                            }
                        }
                        else
                        {
                            objReferencia.iResultado = 0;
                            objReferencia.sMensaje = "La referencia cuenta con gastos.";
                            return false;
                        }
                    }
                    else
                    {
                        objReferencia.iResultado = 0;
                        objReferencia.sMensaje = "La referencia cuenta con gastos.";
                        return false;
                    }
                }
                else
                {
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = "La referencia se encuentra en un estatus diferente a abierta.";
                    return false;
                }
            }
            else
            {
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = "La referencia no existe en NADSI.";
                return false;
            }
        }
        catch (Exception ex)
        {
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "Ocurrió un error al consultar la referencia";
            return false;
        }
    }


    /// <summary>
    /// Metdod obtener clientes contables
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ObtenerClientesContables(Referencia objReferencia)
    {
        string sQuery = "SELECT DISTINCT rc.idClienteSec, c.nomCliente FROM tRelacionCliente rc INNER JOIN tCliente c ON rc.idClienteSec=c.idCliente WHERE rc.idClientePri = " + objReferencia.iIdClienteOperativo;

        //Se crea la variable para almacenar datos
        DataSet dsDatosClientesContables;
        //Se instancia la clase conexión
        Conexion objConexion = new Conexion();
        //Se ejecuta la consulta para obtener los datos
        dsDatosClientesContables = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "clienteContable");
        //Se verifica que se tengan datos
        if (dsDatosClientesContables.Tables["clienteContable"].Rows.Count > 0)
        {
            //Se crea option vacío
            objReferencia.sContenido = "<option value=''></option>";
            //Se recorren los registros
            foreach (DataRow registro in dsDatosClientesContables.Tables["clienteContable"].Rows)
            {
                objReferencia.sContenido += "<option value='" + registro[0] + "'>" + registro[1] + "</option>";
            }
        }
        else
        {
            if (objReferencia.iIdTipoReferencia == 35)
            {
                sQuery = "select DISTINCT idCliente,nomCliente from tCliente where idCliente=" + objReferencia.iIdClienteOperativo;
                dsDatosClientesContables = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "clienteContable");
                //Se verifica que se tengan datos
                if (dsDatosClientesContables.Tables["clienteContable"].Rows.Count > 0)
                {
                    //Se crea option vacío
                    objReferencia.sContenido = "<option value=''></option>";
                    //Se recorren los registros
                    foreach (DataRow registro in dsDatosClientesContables.Tables["clienteContable"].Rows)
                    {
                        objReferencia.sContenido += "<option value='" + registro[0] + "'>" + registro[1] + "</option>";
                    }
                }
            }
        }
    }

    /// <summary>
    /// metodo obtener plantas del cliente operativo
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ObtenerPlantasClienteOperativo(Referencia objReferencia)
    {
        string sQuery = "SELECT DISTINCT tc.idPlanta, p.nomPlanta FROM tClientePlanta tc INNER JOIN tPlanta p ON tc.idPlanta=p.idPlanta WHERE tc.idCliente= " + objReferencia.iIdClienteOperativo;
        DataSet dsDatosPlantas;
        Conexion objConexion = new Conexion();
        dsDatosPlantas = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "plantasCliente");
        if (dsDatosPlantas.Tables["plantasCliente"].Rows.Count > 0)
        {
            //Se crea option vacío
            objReferencia.sContenido = "<option value=''></option>";
            foreach (DataRow registro in dsDatosPlantas.Tables["plantasCliente"].Rows)
            {
                objReferencia.sContenido += "<option value='" + registro[0] + "'>" + registro[1] + "</option>";
            }
        }
    }

    /// <summary>
    /// metodo obtener clientes operativos de la aduana
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ObtenerClientesOperativo(Referencia objReferencia)
    {
        string sQuery = "select tc.idcliente, tc.nomcliente from tClienteAduana tca inner join tCliente tc on tc.idcliente = tca.idcliente  where tca.idaduana =" + objReferencia.iIdAduana;
        DataSet dsDatosClientesOperativo;
        Conexion objConexion = new Conexion();
        dsDatosClientesOperativo = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "clientesOperativo");
        if (dsDatosClientesOperativo.Tables["clientesOperativo"].Rows.Count > 0)
        {
            //Se crea option vacío
            objReferencia.sContenido = "<option value=''></option>";
            foreach (DataRow registro in dsDatosClientesOperativo.Tables["clientesOperativo"].Rows)
            {
                objReferencia.sContenido += "<option value='" + registro[0] + "'>" + registro[1] + "</option>";
            }
        }
    }


    /// <summary>
    /// Método para obtener datos de la referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ObtenerDatosReferenciaAlta(Referencia objReferencia)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "iReferencia", "iIdPatente", "iIdAduana", "iIdTipoOperacion", "sNoReferenciaAdmin", "sNoReferenciaOpe", "iIdTipoReferencia", "sPedimento", "iIdCvePedimento", "iIdClienteOperativo", "iIdClienteContable", "iIdPlanta" };
        //Se crea la consulta
        //string sQuery = "SELECT CASE WHEN (SELECT COUNT(*) FROM tReferencia tr WHERE tr.refAdministrativa=tsubr.refAdministrativa) > 0 THEN 1 ELSE 0 END AS iReferencia, (SELECT tr.idPatente FROM tReferencia tr WHERE tr.idReferencia=tsubr.idReferencia) AS iIdPatente, (SELECT tr.idAduana FROM tReferencia tr WHERE tr.idReferencia=tsubr.idReferencia) AS iIdAduana, (SELECT tr.idTipoOperacion FROM tReferencia tr WHERE tr.idReferencia=tsubr.idReferencia) AS iIdTipoOperacion, " +
        //"tsubr.refAdministrativa AS sNoReferenciaAdmin,tsubr.refOperativa AS sNoReferenciaOpe,  tsubr.idTipoReferencia AS iIdTipoReferencia, CASE WHEN tsubr.pedimento = 0 THEN '' ELSE CONVERT(VARCHAR(7),tsubr.pedimento) END AS sPedimento,tsubr.idCvePedimento AS iIdCvePedimento,tsubr.idClienteOperativo AS iIdClienteOperativo,tsubr.idClienteContable AS iIdClienteContable,tsubr.idPlanta AS iIdPlanta FROM tSubReferencia tsubr WHERE tsubr.idSubReferencia=" + objReferencia.iIdSubReferencia;

        string sQuery = "SELECT CASE WHEN (SELECT COUNT(*) FROM tReferencia tr WHERE tr.refAdministrativa=tsubr.refAdministrativa) > 0 THEN 1 ELSE 0 END AS iReferencia, idPatente AS iIdPatente, idAduana AS iIdAduana, idTipoOperacion AS iIdTipoOperacion, " +
       "tsubr.refAdministrativa AS sNoReferenciaAdmin,tsubr.refOperativa AS sNoReferenciaOpe,  tsubr.idTipoReferencia AS iIdTipoReferencia, CASE WHEN tsubr.pedimento = 0 THEN '' ELSE CONVERT(VARCHAR(7),tsubr.pedimento) END AS sPedimento,tsubr.idCvePedimento AS iIdCvePedimento,tsubr.idClienteOperativo AS iIdClienteOperativo,tsubr.idClienteContable AS iIdClienteContable,tsubr.idPlanta AS iIdPlanta FROM tSubReferencia tsubr WHERE tsubr.idSubReferencia=" + objReferencia.iIdSubReferencia;

        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<Referencia>(sQuery, arrAtributos, objReferencia);
        //Se asigna el resultado
        objReferencia.iResultado = 1;
    }

    public void fn_ObtenerDatosSubReferenciaAlta(Referencia objReferencia)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "iReferencia", "iIdPatente", "iIdAduana", "iIdTipoOperacion", "sNoReferenciaAdmin", "sNoReferenciaOpe", "sPedimento", "iIdCvePedimento", "iIdClienteOperativo", "iIdClienteContable", "iIdPlanta" };
        //Se crea la consulta
        string sQuery = "SELECT CASE WHEN (SELECT COUNT(*) FROM tReferencia tr WHERE tr.refAdministrativa=tsubr.refAdministrativa) > 0 THEN 1 ELSE 0 END AS iReferencia, (SELECT tr.idPatente FROM tReferencia tr WHERE tr.idReferencia=tsubr.idReferencia)  AS iIdPatente,(SELECT tr.idAduana FROM tReferencia tr WHERE tr.idReferencia=tsubr.idReferencia)  AS iIdAduana,  (SELECT tr.idTipoOperacion FROM tReferencia tr WHERE tr.idReferencia=tsubr.idReferencia)  AS iIdTipoOperacion," +
                        "  tsubr.refAdministrativa  AS sNoReferenciaAdmin, tsubr.refOperativa AS sNoReferenciaOpe, CASE WHEN (SELECT COUNT(*) FROM tReferencia tr WHERE tr.refAdministrativa=tsubr.refAdministrativa) > 1  THEN '' " +
                        "else CASE WHEN tsubr.pedimento = 0 THEN '' ELSE CONVERT(VARCHAR(7),tsubr.pedimento) END end AS sPedimento, CASE WHEN (SELECT COUNT(*) FROM tReferencia tr WHERE tr.refAdministrativa=tsubr.refAdministrativa) > 0  THEN ''  else  tsubr.idCvePedimento end AS iIdCvePedimento, tsubr.idClienteOperativo as  iIdClienteOperativo, CASE WHEN (SELECT COUNT(*) FROM tReferencia tr WHERE tr.refAdministrativa=tsubr.refAdministrativa) > 0  THEN '' else tsubr.idClienteContable end AS iIdClienteContable, CASE WHEN (SELECT COUNT(*) FROM tReferencia tr WHERE tr.refAdministrativa=tsubr.refAdministrativa) > 0 " +
                        "THEN '' else tsubr.idPlanta end AS iIdPlanta  FROM tSubReferencia tsubr WHERE tsubr.idSubReferencia=" + objReferencia.iIdSubReferencia;
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<Referencia>(sQuery, arrAtributos, objReferencia);
        //Se asigna el resultado
        objReferencia.iResultado = 1;
    }


    /// <summary>
    /// Método para buscar una referencia matriz y obtener informacion 
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_BuscarReferenciaMatriz(Referencia objReferencia)
    {

        // iReferencia va dentro de la consulta
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "iReferencia", "iIdPatente", "iIdAduana", "iIdTipoOperacion" };
        //Se crea la consulta
        string sQuery = "SELECT COUNT(*) as iReferencia, tr.idPatente,tr.idAduana,tr.idTipoOperacion FROM tReferencia tr WHERE tr.refAdministrativa='" + objReferencia.sNoReferenciaAdmin +
                 "' group by idAduana, idPatente, idTipoOperacion";
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<Referencia>(sQuery, arrAtributos, objReferencia);
        //Se asigna el resultado
        objReferencia.iResultado = 1;
    }

    /// <summary>
    /// Método para obtener plantas del cliente
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ObtenerPlantasCliente(Referencia objReferencia)
    {
        //Se crea query
        string sQuery = "SELECT DISTINCT tc.idPlanta FROM tClientePlanta tc WHERE tc.idCliente != " + objReferencia.iIdClienteOperativo;
        //Se crea variable tipo lista
        List<string> lsLlenadoDeCampos;
        //Se abre la conexión
        Conexion obj_conexion = new Conexion();
        //Se ejecuta la consulta
        lsLlenadoDeCampos = obj_conexion.ejecutarConsultaRegistroMultiples(sQuery);
        //Se verifica que la consulta arroje datos
        if (lsLlenadoDeCampos[0].Equals("1"))
        {
            if (lsLlenadoDeCampos.Count > 1)
            {
                //Se obtienen las plantas
                arrPlantasCliente = lsLlenadoDeCampos.ToArray();
            }
        }
    }

    /// <summary>
    /// Método para eliminar factura de la referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_EliminarFacturaReferencia(Referencia objReferencia)
    {
        //Se valida si la factura de puede eliminar
        if (!fn_ValidarFacturaNotaCredito(objReferencia.iIdFactura))
        {
            //Se retorna el mensaje de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "La  factura no se puede desasignar debido a que esta relacionada a una Nota de Crédito";
            return;
        }

        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_EliminarFacturaReferencia", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objReferencia.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@iIdFactura", SqlDbType.Int, objReferencia.iIdFactura.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    objReferencia.sMensaje = "Factura Desasignada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
        /**
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "IF (SELECT idEstatusFactura FROM tFactura WHERE idFactura=" + objReferencia.iIdFactura + ") != 3 UPDATE tFactura SET idSubReferencia = null, idEstatusFactura=1 WHERE idFactura=" + objReferencia.iIdFactura + " ELSE UPDATE tFactura SET idSubReferencia = null WHERE idFactura=" + objReferencia.iIdFactura + "";
        string sRes = objConexion.ejecutarComando(sQuery);
        //Se verifica el resultado
        if (sRes == "1")
        {
            sQuery = "UPDATE tSubReferencia SET idEstatusReferencia=2 WHERE idSubReferencia=" + objReferencia.iIdSubReferencia;
            sRes = objConexion.ejecutarComando(sQuery);
            //
            if (sRes == "1")
            {
                //Se retorna el sResultado 
                objReferencia.iResultado = 1;
                //Se retorna mensaje de éxito
                objReferencia.sMensaje = "Factura eliminada con éxito.";
            }
        }
        else
        {
            //Se retorna el sResultado 
            objReferencia.iResultado = 0;
            //Se retorna mensaje de error
            objReferencia.sMensaje = sRes;
        }
         
         **/
    }

    /// <summary>
    /// Método para validar servicios de la referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ValidarServiciosReferencia(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para obtener total servicios refrencia
        string sQuery = "SELECT COUNT(DISTINCT tsr.idServicio) FROM tServicioReferencia tsr WHERE tsr.idSubReferencia=" + objReferencia.iIdSubReferencia;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objReferencia.iTotalServiciosReferencia = int.Parse(sRes[1]);
        //sQuery para obtener total servicios facturas
        sQuery = "SELECT COUNT(DISTINCT tpsc.idServicio) FROM tServicioFactura tsf inner join tProvServicioConcepto tpsc ON tsf.idProvServicioConcepto=tpsc.idProvServicioConcepto WHERE (SELECT COUNT(*) FROM tFactura tf WHERE tf.idFactura=tsf.idFactura and tf.idSubReferencia=" + objReferencia.iIdSubReferencia + ") <> 0";
        sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objReferencia.iTotalServiciosFactura = int.Parse(sRes[1]);
        //Se validan los servicios de la referencia y de la factura
        if (objReferencia.iTotalServiciosReferencia == objReferencia.iTotalServiciosFactura)
        {
            objReferencia.iResultado = 1;
        }
        else if (objReferencia.iTotalServiciosReferencia > objReferencia.iTotalServiciosFactura)
        {
            objReferencia.iResultado = 2;
            objReferencia.sMensaje = "Faltan facturas de algunos servicios de la referencia. ¿Desea continuar?";
        }
        else
        {
            objReferencia.iResultado = 3;
            objReferencia.sMensaje = "Se registraron servicios de más. ¿Desea continuar?";
        }
    }

    /// <summary>
    /// Método para enviar a tarificar referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_EnviarTarificar(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_EnviarTarificarReferencia", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objReferencia.iIdSubReferencia.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    objReferencia.sMensaje = "Referencia enviada correctamente a tarificar.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para enviar a tarificar referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_EnviarFacturar(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_EnviarFacturarReferencia", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objReferencia.iIdSubReferencia.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    objReferencia.sMensaje = "Referencia enviada correctamente a Facturar.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para enviar a pendiente por validar
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_EnviarPendienteValidar(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_EnviarPendienteValidarTarifa", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objReferencia.iIdSubReferencia.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    objReferencia.sMensaje = "Referencia enviada correctamente a pendiente por validar.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para obtener total de facturas
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ObtenerTotalFacturas(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "SELECT distinct  case when ( " +
           " (SELECT count(*) from tSubReferencia tsbr  WHERE tsbr.idEstatusReferencia not in (7, 3, 4, 9, 12, 13) and tsbr.idSubReferencia =  " + objReferencia.iIdSubReferencia + " ) = 0) " +
            "			then 0 " +

             "           else " +
                "			case when(idRol = 5) " +

                 "               then " +
                    "				case when (select tsbr.idEstatusReferencia from tSubReferencia tsbr where idSubReferencia =  " + objReferencia.iIdSubReferencia + "  ) = 8 " +

            "                            then 0 " +

             "                           else 2 " +

            "                            end " +

             "                   else " +
                "						case when (select tsbr.idEstatusReferencia from tSubReferencia tsbr where idSubReferencia =  " + objReferencia.iIdSubReferencia + " ) = 8 " +

                 "                       then 3 " +

                  "                      else " +
                                            //       "							case when (select count(*) from tFactura where idEstatusFactura not in (6, 13, 14, 16, 17, 21) and idSubReferencia =  " + objReferencia.iIdSubReferencia + " ) > 0 " +

                                            //        "                           then " +

                                            //                                           " 0 " +

                                            //          "                         else " +
                                            //                                            " 1 " +

                                            //"                                   end " +
                                            " 1 " +
              "                          end " +

               "                end " +
                "       end " +
                " FROM tUsuarioComitenteRol tuor inner join tUsuarioComitente tuo ON tuor.idUsuarioComitente = tuo.idUsuarioComitente WHERE tuo.idUsuario = " + objReferencia.iIdUsuario;
        //"SELECT  case when ((SELECT COUNT(*) FROM tFactura tf join tSubReferencia tsbr on tf.idSubReferencia= tsbr.idSubReferencia WHERE tsbr.idEstatusReferencia not in (7,3,4,9,12,13) and tsbr.idSubReferencia= " + objReferencia.iIdSubReferencia + "  )=0) " +
        //                "then 0	else case when(idRol=5) then case when (select tsbr.idEstatusReferencia from tSubReferencia tsbr where idSubReferencia =  " + objReferencia.iIdSubReferencia + " ) = 8 then 0 else 2 end else case when (select tsbr.idEstatusReferencia from tSubReferencia tsbr where idSubReferencia = " + objReferencia.iIdSubReferencia + ") = 8 then 3 else 1 end end end " +
        //                    "FROM tUsuarioComitenteRol tuor inner join tUsuarioComitente tuo ON tuor.idUsuarioComitente=tuo.idUsuarioComitente WHERE tuo.idUsuario= " + objReferencia.iIdUsuario;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objReferencia.iResultado = int.Parse(sRes[1]);
    }

    /// <summary>
    /// Método para obtener total de facturas
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ObtenerEstatusReferencia(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "SELECT  case when ((select tsbr.idEstatusReferencia from tSubReferencia tsbr where idSubReferencia = " + objReferencia.iIdSubReferencia + ") in (1,3,4,6,7,8,9,10,12,13,14)) then 0 else 1 end ";
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objReferencia.iResultado = int.Parse(sRes[1]);
    }

    /// <summary>
    /// Método para obtener total de facturas
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ObtenerEstatusGastosTerceros(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "select case when ((select tsbr.idEstatusReferencia from tSubReferencia tsbr join cTipoReferencia ct on ct.idTipoReferencia=tsbr.idTipoReferencia where idSubReferencia =  " + objReferencia.iIdSubReferencia + " and ct.aplica='GT y T')  in (10,9,8,3,7,4,12,13)) then 0 else " +
                           " case when (idRol<>5) then case when (((SELECT COUNT(*) FROM tFactura tf join tSubReferencia tsbr on tf.idSubReferencia= tsbr.idSubReferencia where tsbr.idSubReferencia =  " + objReferencia.iIdSubReferencia + ")= 0) AND ((select tsbr.idEstatusReferencia from tSubReferencia tsbr join cTipoReferencia ct on ct.idTipoReferencia=tsbr.idTipoReferencia where idSubReferencia = " + objReferencia.iIdSubReferencia + ") not  in (10,9,8,3,7,4,12,13))) then 1	else 0	end	else 0 end " +
                            "end FROM tUsuarioComitenteRol tuor inner join tUsuarioComitente tuo ON tuor.idUsuarioComitente=tuo.idUsuarioComitente WHERE tuo.idUsuario = " + objReferencia.iIdUsuario;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objReferencia.iResultado = int.Parse(sRes[1]);
    }
    /// <summary>
    /// metodo para obtener el numero de solicitudes de transferencia que tiene una subreferencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ObtenerNumeroSolicitudesTransferencia(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "SELECT case when (SELECT COUNT(*) FROM tSolicitudTransferencia  tst where tst.idSubreferencia= tsr.idSubReferencia) = 0 THEN 0 ELSE 1 END  FROM tSubReferencia tsr where tsr.idSubReferencia =" + objReferencia.iIdSubReferencia;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objReferencia.iResultado = int.Parse(sRes[1]);
    }


    /// <summary>
    /// Método para generar tabla de referencias
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_GenerarTablaReferencias(Referencia objReferencia)
    {
        //Se verifica el contenido
        if (objReferencia.sContenido != "0")
        {
            //Consulta para obtener datos
            string sQuery = "SELECT tr.idReferencia AS iIdReferencia, tr.refAdministrativa AS sNoRefAdministrativa, tr.refOperativa AS sNoRefOperativa, CONVERT(VARCHAR(10),tr.fechaAlta,103) AS sFechaAlta, (SELECT ce.nomEstatusReferencia FROM cEstatusReferencia ce WHERE ce.idEstatusReferencia=tr.idEstatusReferencia) AS sEstatus, (SELECT ce.color FROM cEstatusReferencia ce WHERE ce.idEstatusReferencia=tr.idEstatusReferencia) AS sColor, tr.idEstatusReferencia AS iIdEstatus, (SELECT (SELECT tc.nomCliente FROM tCliente tc WHERE tc.idCliente=idClienteContable) FROM tSubReferencia tsubr WHERE tsubr.refAdministrativa=tr.refAdministrativa and tsubr.refOperativa=tr.refOperativa) AS sCliente, (SELECT CASE WHEN tsubr.pedimento = 0 THEN null ELSE tsubr.pedimento END FROM tSubReferencia tsubr WHERE tsubr.refAdministrativa=tr.refAdministrativa and tsubr.refOperativa=tr.refOperativa) AS sPedimento FROM tReferencia tr " + objReferencia.sContenido;
            //Se crea la variable para almacenar datos
            DataSet dsDatos;
            //Se instancia la clase conexión
            Conexion objConexion = new Conexion();
            //Se ejecuta la consulta para obtener los embalajes
            dsDatos = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "referencias");
            //Se abre la tabla
            objReferencia.sContenido = "<table class='table datatable table-responsive' id='htblReferencias' border='1' bordercolor='#ddd' style='border-radius: 15px !important;'>";
            //Se crea el encabezado
            objReferencia.sContenido += "<thead class='theadHistorial'>" +
                    "<tr>" +
                        "<th style='width:17%'>Referencia administrativa</th>" +
                        "<th style='width:17%'>Referencia operativa</th>" +
                        "<th style='width:17%'>Cliente contable</th>" +
                        "<th style='width:16%'>Pedimento</th>" +
                        "<th style='width:16%'>Fecha entrada</th>" +
                        "<th style='width:16%'>Estatus</th>" +
                    "</tr>" +
                "</thead>";
            //Se abre el body
            objReferencia.sContenido += "<tbody>";
            //Se verifica que se tengan embalajes asignados
            if (dsDatos.Tables["referencias"].Rows.Count > 0)
            {
                foreach (DataRow registro in dsDatos.Tables["referencias"].Rows)
                {
                    //Se crea la referencia
                    objReferencia.sContenido += "<tr id='" + registro["iIdReferencia"] + "' style='background-color:#fff;' onclick='fn_ToggleDatatable(this.id)'>" +
                            "<td style='width:17%;' id='td-" + registro["iIdReferencia"] + "' class='" + registro["sColor"] + " tdMadre'><span class='fa fa-caret-right " + registro["sColor"] + "'></span> " + registro["sNoRefAdministrativa"] + "</td>" +
                            "<td style='width:17%' class='tdMadre'>" + registro["sNoRefOperativa"] + "</td>" +
                            "<td style='width:17%' class='tdMadre'>" + registro["sCliente"] + "</td>" +
                            "<td style='width:16%' class='tdMadre'>" + registro["sPedimento"] + "</td>" +
                            "<td style='width:16%' class='tdMadre'>" + registro["sFechaAlta"] + "</td>" +
                            "<td style='width:16%' class='tdMadre'>" + registro["sEstatus"] + "</td>" +
                        "</tr>";
                    //Se crean las subreferencias
                    //Consulta para obtener datos
                    sQuery = "SELECT tsubr.idReferencia AS iIdReferencia, tsubr.refAdministrativa AS sNoRefAdministrativa, tsubr.refOperativa AS sNoRefOperativa, CONVERT(VARCHAR(10),tsubr.fechaAlta,103) AS sFechaAlta, (SELECT ce.nomEstatusReferencia FROM cEstatusReferencia ce WHERE ce.idEstatusReferencia=tsubr.idEstatusReferencia) AS sEstatus, (SELECT ce.color FROM cEstatusReferencia ce WHERE ce.idEstatusReferencia=tsubr.idEstatusReferencia) AS sColor, (SELECT tc.nomCliente FROM tCliente tc WHERE tc.idCliente=tsubr.idClienteOperativo) AS sCliente, CASE WHEN tsubr.pedimento != 0 THEN (SELECT ca.aduana FROM cAduana ca WHERE ca.idAduana=tsubr.idAduana) + '-' + (SELECT cp.patente FROM cPatente cp WHERE cp.idPatente=tsubr.idPatente) + '-' + CONVERT(VARCHAR(10),tsubr.pedimento) ELSE '' END AS sPedimento FROM tSubReferencia tsubr WHERE tsubr.idReferencia=" + registro["iIdReferencia"];
                    //Se crea la variable para almacenar datos
                    DataSet dsDatos1;
                    //Se ejecuta la consulta para obtener los embalajes
                    dsDatos1 = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "subreferencias");
                    //Se verifica que se tengan embalajes asignados
                    if (dsDatos1.Tables["subreferencias"].Rows.Count > 0)
                    {
                        foreach (DataRow registro1 in dsDatos1.Tables["subreferencias"].Rows)
                        {
                            //Se crea la subreferencia
                            objReferencia.sContenido += "<tr>" +
                                "<td class='collapse' id='R-" + registro["iIdReferencia"] + "' colspan='6'>" +
                                    "<table class='table'>" +
                                        "<tbody>" +
                                            "<tr style='background-color:#ddd;'>" +
                                                "<td style='width:17%' class='" + registro1["sColor"] + "'>" + registro1["sNoRefAdministrativa"] + "</td>" +
                                                "<td style='width:17%'>" + registro1["sNoRefOperativa"] + "</td>" +
                                                "<td style='width:17%'>" + registro1["sCliente"] + "</td>" +
                                                "<td style='width:16%'>" + registro1["sPedimento"] + "</td>" +
                                                "<td style='width:16%'>" + registro1["sFechaAlta"] + "</td>" +
                                                "<td style='width:16%'>" + registro1["sEstatus"] + "</td>" +
                                            "</tr>" +
                                        "</tbody>" +
                                    "</table>" +
                                "</td>" +
                            "</tr>";
                        }
                    }
                }
            }
            else
            {
                objReferencia.sContenido += "<tr style='background-color:#fff;'><td colspan='6' class='text-center'><b>No hay datos para mostrar</b></td></tr>";
            }
            //Se cierra el body
            objReferencia.sContenido += "</tbody>";
            //Se crea footer
            objReferencia.sContenido += "<tfoot class='tfootHistorial'><tr><td colspan='6'></td></tr></tfoot>";
            //Se cierra la tabla
            objReferencia.sContenido += "</table>";
        }
        else
        {
            //Se crea contenido inicio
            objReferencia.sContenido = "<div style='color: #3c763d; background-color: #90C63D; border-color: #d6e9c6;font-size:20px;border-radius: 5px;' class='text-center'><b>Seleccione un rango de fechas para obtener las referencias</b></div>";
        }
    }

    /// <summary>
    /// Método para cambiar estatus del servicio referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_CambiarEstatusServicio(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_CambiarEstatusServicioReferencia", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdServicioReferencia", SqlDbType.Int, objReferencia.iIdServicioReferencia.ToString());
                objConexion.agregarParametroSP("@sMotivo", SqlDbType.VarChar, objReferencia.sMotivo);
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    if (objReferencia.iType == 1)
                    {
                        objReferencia.sMensaje = "Servicio cancelado.";
                    }
                    else
                    {
                        objReferencia.sMensaje = "Servicio activado.";
                    }
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }


    /// <summary>
    /// Método para cambiar estatus del servicio referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_CambiarEstatusReferencia(Referencia objReferencia, int sIdUsuario)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("dbo.pa_CambiarEstatusReferencia", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objReferencia.iIdReferencia.ToString());
                objConexion.agregarParametroSP("@sEstatus", SqlDbType.Int, objReferencia.sEstatus.ToString());
                objConexion.agregarParametroSP("@sIdUsuario", SqlDbType.Int, sIdUsuario.ToString());
                objConexion.agregarParametroSP("@sMotivo", SqlDbType.VarChar, objReferencia.sMotivo);
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                  //Se retorna el mensaje de éxito 
                  objReferencia.sMensaje = "Referencia actualizada correctamente.";
                    
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }













    /// <summary>
    /// Método para guardar el historial de la referencia
    /// </summary>
    /// <returns>Retorna respuesta dependiendo si se ejecutó con éxito el procedimiento</returns>
    public void fn_GuardarHistorialReferencia(string sIdRefLogisitica, string sRefAdministrativaMatriz, string sRefOperativaMatriz, string sRefAdministrativa, string sRefOperativa, string sAduana, string sPatente, string sPedimento,
                                              string sTipoOperacion, string sTipoReferencia, string sCvePedimento, string sRFCClientePrimario, string sIdPlantaClientePrimario,
                                              string sRFCClienteSecundario, string sIdPlantaClienteSecundario, string sFechaAlta, string sInstancia, string iIdEstatus, string sEstatusReferencia, string sMensaje, string sTipoMaterial)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarHistorialReferencia", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdRefLogisitica", SqlDbType.Int, sIdRefLogisitica);
                objConexion.agregarParametroSP("@sRefAdministrativaMatriz", SqlDbType.VarChar, sRefAdministrativaMatriz);
                objConexion.agregarParametroSP("@sRefOperativaMatriz", SqlDbType.VarChar, sRefOperativaMatriz);
                objConexion.agregarParametroSP("@sRefAdministrativa", SqlDbType.VarChar, sRefAdministrativa);
                objConexion.agregarParametroSP("@sRefOperativa", SqlDbType.VarChar, sRefOperativa);
                objConexion.agregarParametroSP("@sAduana", SqlDbType.VarChar, sAduana);
                objConexion.agregarParametroSP("@sPatente", SqlDbType.VarChar, sPatente);
                objConexion.agregarParametroSP("@sPedimento", SqlDbType.VarChar, sPedimento);
                objConexion.agregarParametroSP("@sTipoOperacion", SqlDbType.VarChar, sTipoOperacion);
                objConexion.agregarParametroSP("@sTipoReferencia", SqlDbType.VarChar, sTipoReferencia);
                objConexion.agregarParametroSP("@sCvePedimento", SqlDbType.VarChar, sCvePedimento);
                objConexion.agregarParametroSP("@sRFCClientePrimario", SqlDbType.VarChar, sRFCClientePrimario);
                objConexion.agregarParametroSP("@sIdPlantaClientePrimario", SqlDbType.VarChar, sIdPlantaClientePrimario);
                objConexion.agregarParametroSP("@sRFCClienteSecundario", SqlDbType.VarChar, sRFCClienteSecundario);
                objConexion.agregarParametroSP("@sIdPlantaClienteSecundario", SqlDbType.VarChar, sIdPlantaClienteSecundario);
                objConexion.agregarParametroSP("@sFechaAlta", SqlDbType.VarChar, sFechaAlta);
                objConexion.agregarParametroSP("@sInstancia", SqlDbType.VarChar, sInstancia);
                objConexion.agregarParametroSP("@iIdEstatus", SqlDbType.Int, iIdEstatus);
                objConexion.agregarParametroSP("@sEstatusReferencia", SqlDbType.VarChar, sEstatusReferencia);
                objConexion.agregarParametroSP("@sTipoMaterial", SqlDbType.VarChar, sTipoMaterial);
                objConexion.agregarParametroSP("@sMensaje", SqlDbType.VarChar, sMensaje);
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {

                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    /// <summary>
    /// Método para consultar las subReferencias a tarificar
    /// </summary>
    /// <param name="objTarifa"></param>
    public List<Referencia> fn_ConsultaSubReferencias(Referencia objReferencia)
    {
        //inicia lista
        List<Referencia> lReferencia = new List<Referencia>();
        //agrega un objeto null en la primer posición 
        lReferencia.Add(null);
        try
        {
            //instancia conexión
            Conexion objConexion = new Conexion();
            //arreglo de columnas a consultar
            string[] aColumnas = new string[] { "idTarifaReferencia", "sFolioTransitorio", "sNoReferenciaAdmin", "sNoReferenciaOpe", "sNombreTarifa", "sCampo" };
            //variable squery
            string sQuery = "select ttr.idTarifaReferencia,ttr.folioTransitorio as sFolioTransitorio," +
                            " tsr.refAdministrativa as sRefAdministrativa,tsr.refOperativa as sRefOperativa," +
                            " ctt.nombre as sNombreTarifa," +
                            " '<div class=\"text-center\">'+" +
                            " '<span class=\"fa fa-pencil-square fa-green-sm col-lg-6\" onclick=\"javascript:fn_ObtenerTarifa('''+convert(varchar,ttr.idAduana)+''','''+convert(varchar,ttr.idCliente)+''','''+CONVERT(varchar,tct.idConfTarifa)+''','''+ CONVERT(varchar,tc.razonSocial) +''','''+CONVERT(varchar,ca.aduana)+'-'+ca.denominacion +''','''+CONVERT(varchar,ttr.idTarifaReferencia)+''');\" ></span>'+" +
                            " '<span data-toggle=\"modal\" data-target=\"#dialogEliminarTarifa\" class=\"fa fa-trash fa-red-sm col-lg-6\" onclick=\"javascript:fn_ObtenerId('''+CONVERT(varchar,ttr.idTarifaReferencia)+''','''+ CONVERT(varchar,tc.razonSocial) +''','''+CONVERT(varchar,ca.aduana)+'-'+ca.denominacion +''','''+CONVERT(varchar,ctt.nombre)+''','''+CONVERT(varchar,tsr.refAdministrativa)+ ' | ' + CONVERT(varchar,tsr.refOperativa) +''');\"> '+" +
                            " '</span>'+" +
                            " '</div>' sEditarEliminar" +
                            " from tTarifaReferencia ttr " +
                            " join tSubReferencia tsr on ttr.idSubReferencia = tsr.idSubReferencia" +
                            " join tReferencia tr on tr.idReferencia = tsr.idReferencia" +
                            " join tConfiguracionTarifa tct on ttr.idConfTarifa = tct.idConfTarifa" +
                            " join cTipoTarifa ctt on tct.idTipoTarifa = ctt.idTipoTarifa" +
                            " join tCliente tc on ttr.idCliente = tc.idCliente" +
                            " join cAduana ca on ttr.idAduana = ca.idAduana" +
                            " where ttr.idEstatus = 1 and tsr.idReferencia = " + objReferencia.iIdReferencia + " and tsr.idSubReferencia != " + objReferencia.iIdSubReferencia + "";
            //llena lista
            string sRespuesta = objConexion.ejecutaRecuperaObjetoLista<Referencia>(sQuery, aColumnas, lReferencia);
            //valida si la consulta se realiza correctamente
            if (sRespuesta == "1")
            {
                //asigna respuesta
                objReferencia.iResultado = 1;
                objReferencia.sMensaje = "Consultado correctamente";
                //agrega objeto a la lista 
                lReferencia[0] = objReferencia;
            }
            else
            {
                //asigna respuesta
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = sRespuesta;
                //agrega objeto a la lista 
                lReferencia[0] = objReferencia;
            }
        }
        catch (Exception ex)
        {
            //atrapa la excepcion
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = ex.Message.ToString();
            //agrega objeto a la lista 
            lReferencia[0] = objReferencia;
        }
        return lReferencia;
    }

    /// <summary>
    /// Método para obtener datos fiscales
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_ObtenerDatosFiscalesSubReferencia(Referencia objReferencia)
    {
        try
        {
            //instancia conexión
            Conexion objConexion = new Conexion();
            //arreglo de atributos
            string[] aAtributos = new string[] { "sNoReferenciaOpe", "sFechaAlta", "sTipoOperacion" };
            //variable squery
            string sQuery = "select tsub.refOperativa as sNoReferenciaOpe," +
                            " convert(varchar,convert(date,tsub.fechaAlta)) as sFechaAlta,cto.tipoOperacion as sTipoOperacion from  tSubReferencia tsub" +
                            " join cTipoOperacion cto on tsub.idTipoOperacion = cto.idTipoOperacion" +
                            " where tsub.idSubReferencia = " + objReferencia.iIdReferencia;
            //Consulta los folios
            string sRes = objConexion.ejecutaRecuperaObjeto<Referencia>(sQuery, aAtributos, objReferencia);
            //valida que la consulta se hiciera correctamente
            if (sRes == "1")
            {
                //manda mensajes de correcto
                objReferencia.iResultado = 1;
                objReferencia.sMensaje = "Datos fiscales consultados correctamente";
            }
            else
            {
                //manda mensajes de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = sRes.ToString();
            }
        }
        catch (Exception ex)
        {
            //manda mensajes de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "Excepción al consultar datos fiscales: " + ex.Message.ToString(); ;
        }
    }

    /// <summary>
    /// Método para obtener datos fiscales
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_ObtenerDatosFiscales(Referencia objReferencia)
    {
        try
        {
            //instancia conexión
            Conexion objConexion = new Conexion();
            //arreglo de atributos
            string[] aAtributos = new string[] { "sFolioTransitorio", "sNoReferenciaOpe", "sFechaAlta", "sTipoOperacion", "sAduana", "sDenominacion", "sPlanta" };
            //variable squery
            string sQuery = " select " +
                            " tft.folioTransitorio as sFolioTransitorio, " +
                            " tsub.refOperativa as sNoReferenciaOpe, " +
                            " convert(varchar,convert(date,tsub.fechaAlta)) as sFechaAlta, " +
                            " cto.tipoOperacion as sTipoOperacion, " +
                            " (select aduana from cAduana  where idAduana = (select idAduana from tPedimento tp where tp.idSubReferencia = tsub.idSubReferencia)) as sAduana, " +
                            " (select denominacion from cAduana  where idAduana = (select idAduana from tPedimento tp where tp.idSubReferencia = tsub.idSubReferencia)) as sDenominacion, " +
                            " (select nomPlanta from tPlanta where idPlanta = (select idPlanta from tsubreferencia tSREF where tSREF.idSubReferencia = tsub.idSubReferencia)) as sPlanta  " +
                            " from tReferencia tr   " +
                            " join tSubReferencia tsub on tr.idReferencia = tsub.idReferencia  " +
                            " join tFolioTransitorioSubReferencia tfts on tfts.idSubReferencia = tsub.idSubReferencia " +
                            " join tFolioTransitorio tft on tft.idFolioTransitorio = tfts.idFolioTransitorio " +
                            " join cTipoOperacion cto on tsub.idTipoOperacion = cto.idTipoOperacion  " +
                            " where tft.idFolioTransitorio = " + objReferencia.iIdFolioT;
            //Consulta los folios
            string sRes = objConexion.ejecutaRecuperaObjeto<Referencia>(sQuery, aAtributos, objReferencia);
            //valida que la consulta se hiciera correctamente
            if (sRes == "1")
            {
                //manda mensajes de correcto
                objReferencia.iResultado = 1;
                objReferencia.sMensaje = "Datos fiscales consultados correctamente";
            }
            else
            {
                //manda mensajes de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = sRes.ToString();
            }
        }
        catch (Exception ex)
        {
            //manda mensajes de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "Excepción al consultar datos fiscales: " + ex.Message.ToString(); ;
        }
    }

    /// <summary>
    /// Metodo para obtener las subreferencias de una referencia
    /// </summary>
    /// <param name="objReferencia">El objeto referencia</param>
    public void fn_ObtenerSubReferencias(Referencia objReferencia)
    {
        try
        {
            //instancia conexión
            Conexion objConexion = new Conexion();
            //variable squery
            string sQuery = "SELECT refAdministrativa AS [Referencia Administrativa], " +
                             "          refOperativa AS [Referencia Operativa], " +
                             "          tc.nomCliente AS [Cliente], " +
                             "          ca.aduana + ' - ' + ca.denominacion AS [Aduana], " +
                             "          '<i id=\"idSb' + CONVERT(VARCHAR(12), idSubReferencia) + '\" class=\"fa fa-plus-square fa-green-sm col-lg-12\" style=\"text-align:center;\" onclick=\"javascript:fn_AgregarSubReferencia(' +  " +
                             "           CONVERT(VARCHAR(12), idSubReferencia) " +
                             "           + ')\"></i>' AS [Seleccionar] " +
                             "   FROM tSubReferencia ts " +
                             "       JOIN  " +
                             "        tCliente tc " +
                             "       ON ts.idClienteContable = tc.idCliente " +
                             "       JOIN " +
                             "        cAduana ca " +
                             "       ON ts.idAduana = ca.idAduana " +
                             "   WHERE idReferencia = (SELECT interna.idReferencia  " +
                             "                         FROM tSubReferencia interna " +
                             "                         WHERE interna.idSubReferencia = " + objReferencia.iIdSubReferencia + ") " +
                             "        AND idSubReferencia NOT IN (SELECT idSubReferencia " +
                             "									  FROM tFolioTransitorioSubReferencia tftsi " +
                             "                                    JOIN  " +
                             "                                    tFolioTransitorio tfti " +
                             "                                    ON tftsi.idFolioTransitorio = tfti.idFolioTransitorio " +
                             "                                    WHERE idEstatusFT != (SELECT idEstatusFT FROM cEstatusFolioTransitorio " +
                             "                                                          WHERE nombre = 'Eliminado')) " +
                             "       AND idEstatusReferencia IN " +
                             "      (SELECT idEstatusReferencia FROM cEstatusReferencia WHERE nomEstatusReferencia = 'Listo para tarificar' OR nomEstatusReferencia = 'Saldos Iniciales')";
            //Consulta los folios
            dtbSubReferencias = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            //manda mensajes de correcto
            objReferencia.iResultado = 1;
            objReferencia.sMensaje = "Consulta realizada correctamente";
        }
        catch (Exception ex)
        {
            //manda mensajes de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "Excepción al consultar referencias : " + ex.Message.ToString(); ;
        }
    }

    /// <summary>
    /// Obtener las subreferencias de una referencia cuando se quieren editar
    /// </summary>
    /// <param name="objReferencia">Objeto referencia</param>
    public void fn_ObtenerSubReferenciasEditar(Referencia objReferencia)
    {
        try
        {
            //instancia conexión
            Conexion objConexion = new Conexion();
            //variable squery
            string sQuery = "SELECT refAdministrativa AS [Referencia Administrativa], " +
                             "          refOperativa AS [Referencia Operativa], " +
                             "          tc.nomCliente AS [Cliente], " +
                             "          ca.aduana + ' - ' + ca.denominacion AS [Aduana], " +
                             "          '<i id=\"idSb' + CONVERT(VARCHAR(12), idSubReferencia) + '\" class=\"fa fa-plus-square fa-green-sm col-lg-12\" style=\"text-align:center;\" onclick=\"javascript:fn_AgregarSubReferencia(' +  " +
                             "           CONVERT(VARCHAR(12), idSubReferencia) " +
                             "           + ')\"></i>' AS [Seleccionar] " +
                             "   FROM tSubReferencia ts " +
                             "       JOIN  " +
                             "        tCliente tc " +
                             "       ON ts.idClienteContable = tc.idCliente " +
                             "       JOIN " +
                             "        cAduana ca " +
                             "       ON ts.idAduana = ca.idAduana " +
                             "   WHERE idReferencia = (SELECT interna.idReferencia  " +
                             "                         FROM tSubReferencia interna " +
                             "                         WHERE interna.idSubReferencia = " + objReferencia.iIdSubReferencia + ") " +
                             "        AND idSubReferencia NOT IN (SELECT idSubReferencia " +
                             "									  FROM tFolioTransitorioSubReferencia tftsi " +
                             "                                    JOIN  " +
                             "                                    tFolioTransitorio tfti " +
                             "                                    ON tftsi.idFolioTransitorio = tfti.idFolioTransitorio " +
                             "                                    WHERE idEstatusFT != (SELECT idEstatusFT FROM cEstatusFolioTransitorio " +
                             "                                                          WHERE nombre = 'Eliminado') " +
                             "                                          AND tfti.folioTransitorio != '" + objReferencia.sFolioTransitorio + "'" +
                             "                                          ) " +
                             "       AND idEstatusReferencia IN (7, 9, 14) ";
            //Consulta los folios
            dtbSubReferencias = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            //manda mensajes de correcto
            objReferencia.iResultado = 1;
            objReferencia.sMensaje = "Consulta realizada correctamente";
        }
        catch (Exception ex)
        {
            //manda mensajes de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "Excepción al consultar referencias : " + ex.Message.ToString(); ;
        }
    }

    /// <summary>
    /// Metodo para obtener las subreferencias varias
    /// </summary>
    /// <param name="objReferencia">Objeto referencia</param>
    public void fn_ObtenerSubReferenciasVarias(Referencia objReferencia)
    {
        //Se hace join de la lista de aduanas
        string sAduanas = string.Join(",", objReferencia.lstAduanas);

        try
        {
            //instancia conexión
            Conexion objConexion = new Conexion();
            //variable squery
            string sQuery = "SELECT  (CASE WHEN (SELECT COUNT(*) FROM tSubReferencia tin WHERE tin.idReferencia = tsr.idReferencia AND idEstatusReferencia = 7 ) > 1 " +
                             "  THEN '<i id=\"idRe' + CONVERT(VARCHAR(12), tsr.idSubReferencia) + " +
                             "       '\"  class=\"fa fa-chevron-down fa-md col-lg-12\" style=\"text-align:center;\" onclick=\"javascript:fn_ObtenerSubReferenciasRow(' +   " +
                             "       CONVERT(VARCHAR(12), idSubReferencia) +  ', event)\">' + tsr.refAdministrativa + '</i>'  " +
                             "   ELSE  " +
                             "       tsr.refAdministrativa  " +
                             "   END) AS [Referencia Administrativa],  " +
                             "   tsr.refOperativa AS [Referencia Operativa],  " +
                             "   tc.nomCliente AS [Cliente],  " +
                             "   (ca.aduana + '-' + ca.denominacion) AS [Aduana],  " +
                             "   (CASE WHEN tsr.idSubReferencia NOT IN  " +
                             "       (SELECT idSubReferencia  " +
                             "       FROM tFolioTransitorioSubReferencia tftsri  " +
                             "       JOIN tFolioTransitorio tfti ON tftsri.idFolioTransitorio = tfti.idFolioTransitorio  " +
                             "       WHERE idEstatusFT != (SELECT idEstatusFT  " +
                             "	                            FROM cEstatusFolioTransitorio  " +
                             "		                        WHERE nombre = 'Eliminado')) " +
                             "   THEN " +
                             "       ('<i id=\"idSb' + CONVERT(VARCHAR(12), idSubReferencia) + '\" class=\"fa fa-check-square fa-green-sm col-lg-12\" style=\"text-align:center;\" onclick=\"javascript:fn_AgregarSubReferencia(' +   " +
                             "       CONVERT(VARCHAR(12), idSubReferencia) +  " +
                             "       ')\"></i>') " +
                             "   ELSE " +
                             "       'Asignado' " +
                             "   END) AS [Seleccionar]  " +
                             "   FROM tSubReferencia tsr  " +
                             "   JOIN  " +
                             "   tCliente tc  " +
                             "   ON tsr.idClienteContable = tc.idCliente  " +
                             "   JOIN  " +
                             "   cAduana ca  " +
                             "   ON tsr.idAduana = ca.idAduana  " +
                             "   WHERE " +
                             "   idEstatusReferencia IN (SELECT idEstatusReferencia FROM cEstatusReferencia WHERE nomEstatusReferencia = 'Listo para tarificar') " +
                             "   AND   " +
                             "   idCliente = " + objReferencia.iIdClienteContable + " " +
                             "   AND  " +
                             "   tsr.idAduana IN (" + sAduanas + ")";
            //Consulta los folios
            dtbSubReferencias = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            //manda mensajes de correcto
            objReferencia.iResultado = 1;
            objReferencia.sMensaje = "Consulta realizada correctamente";
        }
        catch (Exception ex)
        {
            //manda mensajes de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "Excepción al consultar referencias : " + ex.Message.ToString(); ;
        }
    }

    /// <summary>
    /// Metodo para obtener las subreferencias varias cuando se quiere editar
    /// </summary>
    /// <param name="objReferencia">Objeto referencia</param>
    public void fn_ObtenerSubReferenciasVariasEdit(Referencia objReferencia)
    {
        //Se hace join de la lista de aduanas
        string sAduanas = string.Join(",", objReferencia.lstAduanas);

        try
        {
            //instancia conexión
            Conexion objConexion = new Conexion();
            //variable squery
            string sQuery = "SELECT  (CASE WHEN (SELECT COUNT(*) FROM tSubReferencia tin WHERE tin.idReferencia = tsr.idReferencia AND idEstatusReferencia = 7 ) > 1 " +
                             "  THEN '<i id=\"idRe' + CONVERT(VARCHAR(12), tsr.idSubReferencia) + " +
                             "       '\"  class=\"fa fa-chevron-down fa-md col-lg-12\" style=\"text-align:center;\" onclick=\"javascript:fn_ObtenerSubReferenciasRow(' +   " +
                             "       CONVERT(VARCHAR(12), idSubReferencia) +  ', event)\">' + tsr.refAdministrativa + '</i>'  " +
                             "   ELSE  " +
                             "       tsr.refAdministrativa  " +
                             "   END) AS [Referencia Administrativa],  " +
                             "   tsr.refOperativa AS [Referencia Operativa],  " +
                             "   tc.nomCliente AS [Cliente],  " +
                             "   (ca.aduana + '-' + ca.denominacion) AS [Aduana],  " +
                             "   (CASE WHEN tsr.idSubReferencia NOT IN  " +
                             "       (SELECT idSubReferencia  " +
                             "       FROM tFolioTransitorioSubReferencia tftsri  " +
                             "       JOIN tFolioTransitorio tfti ON tftsri.idFolioTransitorio = tfti.idFolioTransitorio  " +
                             "       WHERE idEstatusFT != (SELECT idEstatusFT  " +
                             "	                            FROM cEstatusFolioTransitorio  " +
                             "		                        WHERE nombre = 'Eliminado') " +
                             "              AND tfti.folioTransitorio != '" + objReferencia.sFolioTransitorio + "') " +
                             "   THEN " +
                             "       ('<i id=\"idSb' + CONVERT(VARCHAR(12), idSubReferencia) + '\" class=\"fa fa-check-square fa-green-sm col-lg-12\" style=\"text-align:center;\" onclick=\"javascript:fn_AgregarSubReferencia(' +   " +
                             "       CONVERT(VARCHAR(12), idSubReferencia) +  " +
                             "       ')\"></i>') " +
                             "   ELSE " +
                             "       'Asignado' " +
                             "   END) AS [Seleccionar]  " +
                             "   FROM tSubReferencia tsr  " +
                             "   JOIN  " +
                             "   tCliente tc  " +
                             "   ON tsr.idClienteContable = tc.idCliente  " +
                             "   JOIN  " +
                             "   cAduana ca  " +
                             "   ON tsr.idAduana = ca.idAduana  " +
                             "   WHERE " +
                             "   idEstatusReferencia IN (3, 7, 9, 14) " +
                             "   AND   " +
                             "   idClienteContable = " + objReferencia.iIdClienteContable + " " +
                             "   AND  " +
                             "   tsr.idAduana IN (" + sAduanas + ")";
            //Consulta los folios
            dtbSubReferencias = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            //manda mensajes de correcto
            objReferencia.iResultado = 1;
            objReferencia.sMensaje = "Consulta realizada correctamente";
        }
        catch (Exception ex)
        {
            //manda mensajes de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "Excepción al consultar referencias : " + ex.Message.ToString(); ;
        }
    }

    /// <summary>
    /// Obtener los child rows deuna referencia
    /// </summary>
    /// <param name="objReferencia">Objeto referencia</param>
    public void fn_ObtenerSubReferenciasVariasChild(Referencia objReferencia)
    {
        try
        {
            //instancia conexión
            Conexion objConexion = new Conexion();
            //variable squery
            string sQuery = "SELECT refAdministrativa AS [Referencia Administrativa], " +
                            "       refOperativa AS [Referencia operativa], " +
                            "       (SELECT '<span style=\"text-align:center;\" class=\"col-lg-12\">' + CONVERT(VARCHAR(10),COUNT(*)) + '</span>'  " +
                            "        FROM tFactura tf  " +
                            "        WHERE tf.idSubReferencia = idSubReferencia) AS [Facturas], " +
                            "   (CASE WHEN idSubReferencia NOT IN  " +
                             "       (SELECT idSubReferencia  " +
                             "       FROM tFolioTransitorioSubReferencia tftsri  " +
                             "       JOIN tFolioTransitorio tfti ON tftsri.idFolioTransitorio = tfti.idFolioTransitorio  " +
                             "       WHERE idEstatusFT != (SELECT idEstatusFT  " +
                             "	                            FROM cEstatusFolioTransitorio  " +
                             "		                        WHERE nombre = 'Eliminado') " +
                             "              AND tfti.folioTransitorio != '" + objReferencia.sFolioTransitorio + "') " +
                             "   THEN " +
                             "       ('<i id=\"idSb' + CONVERT(VARCHAR(12), idSubReferencia) + '\" class=\"fa fa-check-square fa-green-sm col-lg-12\" style=\"text-align:center;\" onclick=\"javascript:fn_AgregarSubReferencia(' +   " +
                             "       CONVERT(VARCHAR(12), idSubReferencia) +  " +
                             "       ')\"></i>') " +
                             "   ELSE " +
                             "       'Asignado' " +
                             "   END) AS [Seleccionar]  " +
                            "FROM tSubReferencia " +
                            "WHERE idReferencia = (SELECT TOP 1 idReferencia FROM tSubReferencia WHERE idSubReferencia = " + iIdSubReferencia + ") " +
                            "AND " +
                            "idSubReferencia != (SELECT MIN(interna.idSubReferencia) FROM tSubReferencia interna WHERE (SELECT TOP 1 idReferencia FROM tSubReferencia WHERE idSubReferencia = " + iIdSubReferencia + ") " + " = interna.idReferencia GROUP BY interna.idReferencia) " +
                            "AND " +
                            "idSubReferencia NOT IN (SELECT idSubReferencia FROM tFolioTransitorioSubReferencia) " +
                            "AND " +
                            "idEstatusReferencia = (SELECT idEstatusReferencia FROM cEstatusReferencia WHERE nomEstatusReferencia = 'Listo para tarificar')";
            //Consulta los folios
            dtbSubReferencias = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            //Convierte la datatable en tabla html
            string sHTML;
            sHTML = "<table class='table-responsive dataTable htblSubReferenciasChild'>";
            sHTML += "<thead>";
            sHTML += "<th>Referencia Administrativa</th>";
            sHTML += "<th>Referencia Operativa</th>";
            sHTML += "<th>Facturas</th>";
            sHTML += "<th>Seleccionar</th>";
            sHTML += "</thead>";
            sHTML += "<tbody>";
            foreach (DataRow oRow in dtbSubReferencias.Rows)
            {
                sHTML += "<tr>";
                for (int i = 0; i < 4; i++)
                    sHTML += "<td>" + oRow[i] + "</td>";
                sHTML += "</tr>";
            }
            sHTML += "</tbody>";
            sHTML += "</table>";
            sMensaje = sHTML;
            //manda mensajes de correcto
            objReferencia.iResultado = 1;
        }
        catch (Exception ex)
        {
            //manda mensajes de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "Excepción al consultar referencias : " + ex.Message.ToString(); ;
        }
    }

    /// <summary>
    /// Obtener las subreferencias de una referencia para editar
    /// </summary>
    /// <param name="objReferencia">Objeto referencia</param>
    public void fn_ObtenerSubReferenciasVariasChildEdit(Referencia objReferencia)
    {
        try
        {
            //instancia conexión
            Conexion objConexion = new Conexion();
            //variable squery
            string sQuery = "SELECT refAdministrativa AS [Referencia Administrativa], " +
                            "       refOperativa AS [Referencia operativa], " +
                            "       (SELECT '<span style=\"text-align:center;\" class=\"col-lg-12\">' + CONVERT(VARCHAR(10),COUNT(*)) + '</span>'  " +
                            "        FROM tFactura tf  " +
                            "        WHERE tf.idSubReferencia = idSubReferencia) AS [Facturas], " +
                            "        ('<i id=\"idSb' + CONVERT(VARCHAR(12), idSubReferencia) + '\" class=\"fa fa-check-square fa-green-sm col-lg-12\" style=\"text-align:center;\" onclick=\"javascript:fn_AgregarSubReferencia(' +  " +
                            "        CONVERT(VARCHAR(12), idSubReferencia) + " +
                            "        ')\"></i>') AS [Seleccionar] " +
                            "FROM tSubReferencia " +
                            "WHERE idReferencia = (SELECT TOP 1 idReferencia FROM tSubReferencia WHERE idSubReferencia = " + iIdSubReferencia + ") " +
                            "AND " +
                            "idSubReferencia != (SELECT MIN(interna.idSubReferencia) FROM tSubReferencia interna WHERE (SELECT TOP 1 idReferencia FROM tSubReferencia WHERE idSubReferencia = " + iIdSubReferencia + ") " + " = interna.idReferencia GROUP BY interna.idReferencia) " +
                            "AND " +
                            "idEstatusReferencia = (SELECT idEstatusReferencia FROM cEstatusReferencia WHERE nomEstatusReferencia = 'Listo para tarificar')";
            //Consulta los folios
            dtbSubReferencias = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            //Convierte la datatable en tabla html
            string sHTML;
            sHTML = "<table class='table-responsive dataTable htblSubReferenciasChild'>";
            sHTML += "<thead>";
            sHTML += "<th>Referencia Administrativa</th>";
            sHTML += "<th>Referencia Operativa</th>";
            sHTML += "<th>Facturas</th>";
            sHTML += "<th>Seleccionar</th>";
            sHTML += "</thead>";
            sHTML += "<tbody>";
            foreach (DataRow oRow in dtbSubReferencias.Rows)
            {
                sHTML += "<tr>";
                for (int i = 0; i < 4; i++)
                    sHTML += "<td>" + oRow[i] + "</td>";
                sHTML += "</tr>";
            }
            sHTML += "</tbody>";
            sHTML += "</table>";
            sMensaje = sHTML;
            //manda mensajes de correcto
            objReferencia.iResultado = 1;
        }
        catch (Exception ex)
        {
            //manda mensajes de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "Excepción al consultar referencias : " + ex.Message.ToString(); ;
        }
    }

    /// <summary>
    /// Método que genera un reporte para el cliente FCA
    /// </summary>
    /// <param name="objReferencia"></param>
    [Obsolete("NO USAR - se cambio el proceso a la clase de Reportes.")]
    public void fn_generaReporteFCA(Referencia objReferencia)
    {
        try
        {
            // Instancia clases
            DataTable objDatos = new DataTable();
            Conexion objConexion = new Conexion();
            List<string> lstColumnasExcel = new List<string>();

            // Variables
            int iContador = 0, iColumna = 1, iFila = 2, numFilas = 0;
            string sQuery = "", sCadena = "";
            string[] sResultado = { };
            bool bGrupo1 = true;

            // Ruta de Archivo de Error.
            string sFecha = DateTime.Now.ToString("ddMMyyyyhhmmss");
            string sRutaArchivoError = HttpContext.Current.Server.MapPath("../../Documentos/GastosPropios/Reportes/ReporteFCA/ReporteFCA" + sFecha + ".xlsx");
            ExcelPackage objReporteExcel = new ExcelPackage(new FileInfo(sRutaArchivoError));
            var objHojaReporte = objReporteExcel.Workbook.Worksheets.Add("Reporte");
            objReferencia.sRuta = "../../Documentos/GastosPropios/Reportes/ReporteFCA/ReporteFCA" + sFecha + ".xlsx";

            // Se llena lista con columnas para excel.
            lstColumnasExcel.Add("Factura AA");
            lstColumnasExcel.Add("Moneda");
            lstColumnasExcel.Add("RFC");
            lstColumnasExcel.Add("Serie");
            lstColumnasExcel.Add("Folio");
            lstColumnasExcel.Add("Factura");
            lstColumnasExcel.Add("Proveedor");
            lstColumnasExcel.Add("Tipo Doc");
            lstColumnasExcel.Add("Razón Social");
            lstColumnasExcel.Add("Monto");
            lstColumnasExcel.Add("Fecha Emisión");
            lstColumnasExcel.Add("Fecha Recepción COFIDI");

            // Colores de Celdas.
            Color obj_colorBlanco = System.Drawing.ColorTranslator.FromHtml("#fff");
            Color obj_colorAzulRey = System.Drawing.ColorTranslator.FromHtml("#002060");

            // Crea encabezado de Excel.
            foreach (var columna in lstColumnasExcel)
            {
                // Se agregan encabezados de Excel.
                objHojaReporte.Cells[1, iContador + 1].Value = columna.ToUpper();
                objHojaReporte.Cells[1, iContador + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                objHojaReporte.Cells[1, iContador + 1].Style.Font.Color.SetColor(obj_colorBlanco);
                objHojaReporte.Cells[1, iContador + 1].Worksheet.DefaultColWidth = 24;
                objHojaReporte.Cells[1, iContador + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                objHojaReporte.Cells[1, iContador + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Distributed;


                if (bGrupo1) // Coloca color azul Rey.
                {
                    objHojaReporte.Cells[1, iContador + 1].Style.Fill.BackgroundColor.SetColor(obj_colorAzulRey);
                }

                iContador++;
            }

            sQuery = "SELECT '' [Factura AA], '' [Moneda], '' [RFC], '' [Serie], '' [Folio], '' [Factura], '61535' [Proveedor], 1 [TipoDoc], '' [Razón Social], '' [Monto], '' [Fecha emisión], " +
                "'' [Fecha recepcion COFIDI] FROM tPolizaEgreso tpe INNER JOIN tImpuesto ti on tpe.idPolizaEgreso = TI.idPoliza INNER JOIN tSubReferencia TSR ON TI.idSubreferenciaN = TSR.idSubReferencia " +
                "UNION SELECT '', CASE WHEN tf.moneda is null THEN (SELECT cveMoneda FROM cMoneda WHERE idMoneda=(SELECT tpe.idMoneda FROM tPolizaEgreso tpe where tpe.idPolizaEgreso=ti.idPoliza)) " +
                "ELSE TF.moneda END Moneda, CASE WHEN tf.idProveedor is null THEN 'N/A' ELSE (SELECT rfc FROM tProveedor WHERE idProveedor=TF.idProveedor) END RFC, CASE WHEN TF.serie IS NULL THEN 'N/A' ELSE tf.serie END SERIE, " +
                "CASE WHEN TF.noFactura IS NULL THEN (SELECT tpe.numPoliza FROM tPolizaEgreso tpe where tpe.idPolizaEgreso=ti.idPoliza) ELSE TF.noFactura END FOLIO, " +
                "CASE WHEN TF.noFactura IS NULL THEN (SELECT tpe.numPoliza FROM tPolizaEgreso tpe where tpe.idPolizaEgreso=ti.idPoliza) ELSE TF.serie+TF.noFactura END FACTURA, " +
                "CASE WHEN TF.noFactura IS NOT NULL THEN 'TERCERO' ELSE (CASE WHEN TI.idPoliza IS NOT NULL THEN 'OTRO' END) END PROVEEDOR, " +
                "1 TIPODOC, CASE WHEN TF.noFactura IS NOT NULL THEN (SELECT nomProveedor FROM tProveedor WHERE idProveedor = TF.idProveedor) ELSE (CASE WHEN TI.idPoliza IS NOT NULL THEN 'TESORERIA DE LA FEDERACIÓN' END) END PROVEEDOR, " +
                "CASE WHEN TF.noFactura IS NOT NULL THEN tf.monto ELSE TI.Importe END MONTO, CASE WHEN TF.noFactura IS NOT NULL THEN CONVERT(VARCHAR(MAX),tf.fechaFactura) ELSE CONVERT(VARCHAR(MAX),TI.fechaPago) END FECHAEMISIÓN, " +
                "'' FECHARECEPCIÓNCOFIDI FROM tSubReferencia tsr LEFT JOIN tFactura tf ON tsr.idSubReferencia = tf.idSubReferencia LEFT JOIN tImpuesto ti ON ti.idSubreferenciaN = tsr.idSubReferencia " +
                "LEFT JOIN tPolizaEgreso tpe ON tpe.idPolizaEgreso = (SELECT idPoliza FROM tImpuesto WHERE idSubreferenciaN=tsr.idSubReferencia) WHERE tsr.idSubReferencia = " + objReferencia.iIdSubReferencia;
            // Coloca datos de consulta en Datatable
            objDatos = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            // Se obtiene el número de filas que contiene el datable
            numFilas = objDatos.Rows.Count;
            // Llena excel con la información de la vista.
            foreach (DataRow objFila in objDatos.Rows)
            {
                iColumna = 1;
                //iContador = 0;
                foreach (DataColumn objColumn in objDatos.Columns)
                {
                    objHojaReporte.Cells[iFila, iColumna].Value = objFila[objColumn.ToString()].ToString();
                    objHojaReporte.Cells[iFila, iColumna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    // Se incrementan columnas.
                    //iContador++;
                    iColumna++;
                }
                iFila++;
            }
            objReporteExcel.Save();

            this.iResultado = 1;
            this.sMensaje = "Reporte generado correctamente.";

        }
        catch (Exception ex)
        {
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = ex.Message;
        }
    }

    public void fn_MostrarBotonesCambioEstatus(Referencia objReferencia)
    {
        try
        {
            Conexion objConexion = new Conexion();
            string sQuery = @"SELECT CASE WHEN " + objReferencia.iIdUsuario + @" IN (SELECT DISTINCT idusuario 
                            FROM tUsuarioComitente
                            WHERE idUsuarioComitente IN(SELECT idUsuarioComitente

                                                        FROM tUsuarioComitenteRol WHERE idRol = 14
                            )) THEN CASE WHEN idEstatusReferencia IN (2) THEN '1' WHEN idEstatusReferencia IN (12,7,3,4,8,14,9) THEN '2' ELSE '0' END ELSE '0' END " +
                                            "FROM tSubReferencia WHERE idSubReferencia=" + objReferencia.iIdSubReferencia;
            string[] sRes;

            sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            if (sRes[0] == "1")
            {
                objReferencia.iResultado = int.Parse(sRes[1]);
            }
            else
            {
                objReferencia.iResultado = 0;
            }
        }
        catch (Exception ex)
        {
            //manda mensajes de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "Excepción al ejecutar el proceso : " + ex.Message.ToString(); ;
        }
    }

    public void fn_CambiarEstatusReferencia(Referencia objReferencia)
    {
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_ActualizarEstatusReferencia", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                if (objReferencia.iIdSubReferencia != 0 && String.IsNullOrEmpty(objReferencia.sNoReferenciaOpe))
                {
                    objConexion.agregarParametroSP("@iIdSubreferencia", SqlDbType.VarChar, objReferencia.iIdSubReferencia.ToString());
                    objReferencia.iTieneIdSubReferencia = 1;
                }
                else
                {
                    objConexion.agregarParametroSP("@iIdSubreferencia", SqlDbType.VarChar, objReferencia.sNoReferenciaOpe.ToString());
                    objReferencia.iTieneIdSubReferencia = 0;
                }
                objConexion.agregarParametroSP("@sEstatus", SqlDbType.VarChar, objReferencia.sEstatus);
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.VarChar, objReferencia.iIdUsuario.ToString());
                objConexion.agregarParametroSP("@bTieneIdSubReferencia", SqlDbType.VarChar, objReferencia.iTieneIdSubReferencia.ToString());
                //Se ejecuta el SP
                string[] sResOut = objConexion.ejecutarProcOUTPUT_STRING("@sResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    objReferencia.sMensaje = "Estatus cambiado con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sResOut[1];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }

    public void fn_EsClienteFCA(Referencia objReferencia)
    {
        try
        {
            Conexion objConexion = new Conexion();
            string sQuery = "SELECT CASE WHEN (idClienteContable IN (831,2186,3981)) " + // OR idClienteOperativo IN (831,2186)
                            "AND idEstatusReferencia NOT IN (6) THEN 1 ELSE 0 END FROM tSubReferencia " +
                            "WHERE idSubReferencia=" + objReferencia.iIdSubReferencia;
            string[] sRes;

            sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            if (sRes[0] == "1")
            {
                objReferencia.iResultado = int.Parse(sRes[1]);
            }
            else
            {
                objReferencia.iResultado = 0;
            }
        }
        catch (Exception ex)
        {
            //manda mensajes de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "Excepción al consultar el cliente : " + ex.Message.ToString(); ;
        }
    }

    /// <summary>
    /// Obtener las subreferencias  de un folio
    /// </summary>
    /// <param name="objReferencia">Objeto referencia</param>
    public void fn_ObtenerSubReferenciasFolioChild(Referencia objReferencia)
    {
        try
        {
            //instancia conexión
            Conexion objConexion = new Conexion();
            //variable squery
            string sQuery = "SELECT refAdministrativa AS [Referencia Administrativa], " +
                            "       refOperativa AS [Referencia operativa], " +
                            "       (SELECT '<span style=\"text-align:center;\" class=\"col-lg-12\">' + CONVERT(VARCHAR(10),COUNT(*)) + '</span>'  " +
                            "        FROM tFactura tf  " +
                            "        WHERE tf.idSubReferencia = idSubReferencia) AS [Facturas] " +
                            "FROM tSubReferencia " +
                            "WHERE idReferencia = (SELECT TOP 1 idReferencia FROM tSubReferencia WHERE idSubReferencia = " + iIdSubReferencia + ") " +
                            "AND " +
                            "idSubReferencia != (SELECT MIN(interna.idSubReferencia) FROM tSubReferencia interna WHERE (SELECT TOP 1 idReferencia FROM tSubReferencia WHERE idSubReferencia = " + iIdSubReferencia + ") " + " = interna.idReferencia GROUP BY interna.idReferencia) " +
                            "AND " +
                            "idSubReferencia IN (SELECT idSubReferencia FROM tFolioTransitorioSubReferencia) ";
            //Consulta los folios
            dtbSubReferencias = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            //Convierte la datatable en tabla html
            string sHTML;
            sHTML = "<table class='table-responsive dataTable htblSubReferenciasChild'>";
            sHTML += "<thead>";
            sHTML += "<th>Referencia Administrativa</th>";
            sHTML += "<th>Referencia Operativa</th>";
            sHTML += "<th>Facturas</th>";
            sHTML += "</thead>";
            sHTML += "<tbody>";
            foreach (DataRow oRow in dtbSubReferencias.Rows)
            {
                sHTML += "<tr>";
                for (int i = 0; i < 3; i++)
                    sHTML += "<td>" + oRow[i] + "</td>";
                sHTML += "</tr>";
            }
            sHTML += "</tbody>";
            sHTML += "</table>";
            sMensaje = sHTML;
            //manda mensajes de correcto
            objReferencia.iResultado = 1;
        }
        catch (Exception ex)
        {
            //manda mensajes de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "Excepción al consultar referencias : " + ex.Message.ToString(); ;
        }
    }
    public void fn_CrearRefNoFacturable(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_CrearRefNoFacturable", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objReferencia.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@iIdFactura", SqlDbType.Int, objReferencia.iIdFactura.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    objReferencia.sMensaje = "Referencia  guardada con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }
    /// <summary>
    /// Método para cerrar la  referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_CerrarReferencia(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_CerrarReferencia", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objReferencia.iIdSubReferencia.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    objReferencia.sMensaje = "Referencia cerrada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }
    /// <summary>
    /// Método para guardar el referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_AgregarFacturaRefNoFacturable(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_AgregarFacturaRefNoFacturable", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objReferencia.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@iIdFactura", SqlDbType.Int, objReferencia.iIdFactura.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    objReferencia.sMensaje = "Factura agregada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Función usada para marcar una referencia como tarificada
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_MarcarComoTarificada(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_MarcarSubreferenciaTarificada", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@sIdSubReferencia", SqlDbType.Int, objReferencia.iIdSubReferencia.ToString());
                //Se ejecuta el SP
                string[] sResOut = objConexion.ejecutarProcOUTPUT_INT("@sResOut");
                if (sResOut[1] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    objReferencia.sMensaje = "Factura tarificada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }


    /// <summary>
    /// Método para obtener impuestos
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_verificarDatosTablaImporte(Referencia objReferencia)
    {
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //sQuery para validar embalajes
            string sQuery = " select count(iIdSubreferencia) from v_ListaDetallePCCFolioTransitorio where iIdSubreferencia = " + objReferencia.iIdSubReferencia;
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

            if (sRes[0] == "1")
            {
                //Se retorna el sResultado 
                objReferencia.iResultado = int.Parse(sRes[0]);
                objReferencia.iImpuesto = int.Parse(sRes[1]);

                string sQuery1 = " select '$' + CONVERT(VARCHAR(100), convert(money, SUM(impor)), 1)  from v_ListaDetallePCCFolioTransitorio where iIdSubreferencia = " + objReferencia.iIdSubReferencia;
                string[] sRes1 = objConexion.ejecutarConsultaRegistroSimple(sQuery1);

                if (sRes1[0] == "1")
                {

                    objReferencia.sTotalImpuesto = sRes1[1];

                }
                else
                {
                    //Se retorna el sResultado 
                    objReferencia.sTotalImpuesto = sRes1[1];
                }

            }
            else
            {
                //Se retorna el sResultado 
                objReferencia.iResultado = int.Parse(sRes[0]);
            }
        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = ex.Message;
        }
    }

    /// <summary>
    /// Método para obtener impuestos
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_verificarDatosTablaAnticipo(Referencia objReferencia)
    {
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //sQuery para validar embalajes
            string sQuery = " select count(idSubReferencia) from v_ListaAnticiposFolioTransitorio where idSubReferencia = " + objReferencia.iIdSubReferencia;
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

            if (sRes[0] == "1")
            {
                //Se retorna el sResultado 
                objReferencia.iResultado = int.Parse(sRes[0]);
                objReferencia.iAnticipo = int.Parse(sRes[1]);

            }
            else
            {
                //Se retorna el sResultado 
                objReferencia.iResultado = int.Parse(sRes[0]);
            }
        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = ex.Message;
        }
    }

    /// <summary>
    /// metodo para obtener el numero de solicitudes de transferencia que tiene una subreferencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_mostrarTablaCorresponsalias(Referencia objReferencia)
    {

        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = @"SELECT COUNT(tf.idFactura) FROM tSubReferencia ts
                        INNER JOIN tFactura tf ON ts.idSubReferencia=tf.idSubReferencia
                        INNER JOIN (SELECT idCliente FROM tCliente WHERE
					                        rfc in(SELECT rfc FROM tComitente)) tc ON tc.idCliente=tf.idCliente
                        INNER JOIN (SELECT idProveedor FROM tProveedor WHERE honorarios=1) tp ON tp.idProveedor=tf.idProveedor
                        WHERE ts.idTipoReferencia=47 AND ts.idSubReferencia=" + objReferencia.iIdSubReferencia;

        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objReferencia.iResultado = int.Parse(sRes[1]);
    }

    /// <summary>
    /// Método para obtener informacion de subreferencia
    /// </summary>
    /// <param name="objOrdenPagoCompra"></param>
    public List<Referencia> fn_ObtenerInformacionSubreferenciaAdicional(Referencia objr)
    {
        //inicia lista
        List<Referencia> lstDatosSudAdicional = new List<Referencia>();
        //agrega un objeto null en la primer posición 
        lstDatosSudAdicional.Add(null);

        try
        {
            //instancia conexión
            Conexion objConexion = new Conexion();
            //arreglo de columnas a consultar
            string[] aColumnas = new string[] {
                "sTipoOperacion", "sCVEPedimento", "sAduana", "sFechaETA", "sFechaPago", "sImpPagados", "sImpuestosSubcidiados","sGastosDirectosCliente", "sPesoBruto", "sPesoNeto","sVolumen",
                "sDescripcionMercancia", "sClaseMercancia","sMarcaMercancia", "sObservaciones", "sValorAduana","sBulto", "sImpAfianzados", "sPartidaPresupuestal",
                "sBarco", "sNumeroViaje","sEmbarqueHouse", "sEmbarqueMaster", "sTransportista","sOficina", "sNumeroPlantaDestino" };
            //variable squery
            string sQuery = " select " +
                             " (select tipoOperacion from cTipoOperacion cTO where cTO.idTipoOperacion = tSR.idTipoOperacion) as sTipoOperacion, " +
                             " (select cve from cClavesPedimento cCP where cCP.idCvePedimento = tP.idCveDocumento) AS sCVEPedimento, " +
                             " (select aduana + ' - ' + denominacion from cAduana cA where cA.idAduana = tSR.idAduana) AS sAduana, " +
                             " CONVERT(VARCHAR(10),fechaETA,103) as sFechaETA,  CONVERT(VARCHAR(10),fechaPago,103) as sFechaPago, impPagados as sImpPagados, impuestosSubcidiados as sImpuestosSubcidiados, " +
                             " GastosDirectosCliente as sGastosDirectosCliente, pesoBruto as sPesoBruto, pesoNeto as sPesoNeto, volumen as sVolumen,  " +
                             " descripcionMercancia as sDescripcionMercancia, claseMercancia as sClaseMercancia, marcaMercancia as sMarcaMercancia,  " +
                             " observaciones as sObservaciones, valorAduana as sValorAduana, bulto as sBulto, impAfianzados as sImpAfianzados, partidaPresupuestal as sPartidaPresupuestal, " +
                             "  barco as sBarco , numeroViaje as sNumeroViaje,embarqueHouse as sEmbarqueHouse,embarqueMaster as sEmbarqueMaster, transportista as sTransportista, " +
                             " (select nombreOficina from tOficina tOf where tOf.idOficina = tSRA.idOficina) as sOficina,numeroPlantaDestino as sNumeroPlantaDestino, idReferencia as sIdReferencia" +
                             " from tSubReferencia tSR " +
                             " INNER JOIN tPedimento tP on tP.idAduana=tSR.idAduana and tP.idPatente=tSR.idPatente and tP.pedimento = tSR.pedimento " +
                             " left JOIN tsubreferenciaAdicional tSRA on  tSRA.idSubreferencia = tSR.idSubReferencia " +
                             " WHERE tSR.idSubReferencia =" + objr.iIdSubReferencia;
            //llena lista
            string sRespuesta = objConexion.ejecutaRecuperaObjetoLista<Referencia>(sQuery, aColumnas, lstDatosSudAdicional);
            //valida si la consulta se realiza correctamente
            if (sRespuesta == "1")
            {
                //asigna respuesta
                objr.iResultado = 1;
                objr.sMensaje = "Consultado correctamente";
                //agrega objeto a la lista 
                lstDatosSudAdicional[0] = objr;
            }
            else
            {
                //asigna respuesta
                objr.iResultado = 0;
                objr.sMensaje = sRespuesta;
                //agrega objeto a la lista 
                lstDatosSudAdicional[0] = objr;
            }
        }
        catch (Exception ex)
        {
            //atrapa la excepcion
            objr.iResultado = 0;
            objr.sMensaje = ex.Message.ToString();
            //agrega objeto a la lista 
            lstDatosSudAdicional[0] = objr;
        }
        return lstDatosSudAdicional;
    }
    /// <summary>
    /// Método para guardar el referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_AgregarNotaCreditoReferencia(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_AgregarNotaCreditoReferencia", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objReferencia.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@arrIdNotaCredito", SqlDbType.VarChar, objReferencia.arrIdNotaCredito + ",");
                objConexion.agregarParametroSP("@iTotal", SqlDbType.Int, objReferencia.iTotal.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    objReferencia.sMensaje = "Notas Credito agregadas con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para obtener datos factura 
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_verificarDatosTablaFactura(Referencia objReferencia)
    {
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //sQuery para validar embalajes
            string sQuery = " select count(iIdSubReferencia) from v_ListaFacturasReferencia where iIdSubReferencia = " + objReferencia.iIdSubReferencia;
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

            if (sRes[0] == "1")
            {
                //Se retorna el sResultado 
                objReferencia.iResultado = int.Parse(sRes[0]);
                objReferencia.iFactura = int.Parse(sRes[1]);

                //Se valida si el tipo de referencia se trata de un corresponsal
                sQuery = " select count(*) from tSubReferencia where idTipoReferencia=(select idTipoReferencia from cTipoReferencia where nomTipoReferencia='Corresponsal') and idSubReferencia= " + objReferencia.iIdSubReferencia;
                sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

                //Si no es de corresponsal continua flujo normal
                if (sRes[1] == "0")
                {

                    //se crea la consulta para el monto total de las facturas
                    string sQuery1 = " select '$' + CONVERT(VARCHAR(100), convert(money, SUM(fMontoTotal)), 1)  from v_ListaFacturasReferencia where sMoneda = 'MXN' and iIdSubreferencia = " + objReferencia.iIdSubReferencia;
                    string[] sRes1 = objConexion.ejecutarConsultaRegistroSimple(sQuery1);

                    if (sRes1[0] == "1")
                    {

                        objReferencia.sTotalFactura = sRes1[1];
                        if (objReferencia.sTotalFactura == "") objReferencia.sTotalFactura = "$0";
                        // se checa moneda en Dolares
                        sQuery1 = " select '$' + CONVERT(VARCHAR(100), convert(money, SUM(ISNULL(fMontoTotal,0))), 1)  from v_ListaFacturasReferencia where sMoneda = 'USD' and iIdSubreferencia = " + objReferencia.iIdSubReferencia;
                        sRes1 = objConexion.ejecutarConsultaRegistroSimple(sQuery1);

                        if (sRes1[0] == "1")
                        {
                            //Se retorna el sResultado 
                            objReferencia.sTotalFacturaUSD = sRes1[1];
                            if (objReferencia.sTotalFacturaUSD == "") objReferencia.sTotalFacturaUSD = "$0";
                        }
                    }
                    else
                    {
                        //Se retorna el sResultado 
                        objReferencia.sTotalFactura = sRes1[1];
                    }
                    objReferencia.sTotalCuentaGastos = "";
                }
                else ///Si es corresponsal se realiza lo siguiente
                {
                    //Se envia al metodo para obtener el total de las facturas pago corresponsal
                    string sResult = fn_CalcularMontoFacturasCorresponsal(objReferencia.iIdSubReferencia);
                    //Se obtienen los valores del corresponsal y se genera un arreglo con los datos
                    string[] sValores = sResult.Split('|');
                    //Se recorren los valores y se asignan a su correspondiente
                    objReferencia.sTotalFactura = "$" + sValores[0];
                    objReferencia.sTotalFacturaUSD = "$" + sValores[1];
                    objReferencia.sTotalCuentaGastos = "$" + sValores[2];

                }
            }
            else
            {
                //Se retorna el sResultado 
                objReferencia.iResultado = int.Parse(sRes[0]);
            }
        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "Ocurrio un error inesperado, favor de comunicarse con el administrador";
        }
    }

    /// <summary>
    /// Método para obtener datos factura 
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_verificarDatosTablaCorresponsal(Referencia objReferencia)
    {
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //sQuery para validar embalajes
            string sQuery = " select count(iIdSubReferencia) from v_ListaCorresponsal where iIdSubReferencia = " + objReferencia.iIdSubReferencia;
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

            if (sRes[0] == "1")
            {
                //Se retorna el sResultado 
                objReferencia.iResultado = int.Parse(sRes[0]);
                objReferencia.iFactura = int.Parse(sRes[1]);

                //Se valida si el tipo de referencia se trata de un corresponsal
                sQuery = " select count(1) from tSubReferencia where idTipoReferencia=(select idTipoReferencia from cTipoReferencia where nomTipoReferencia='Corresponsal') and idSubReferencia= " + objReferencia.iIdSubReferencia;
                sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

                //Si no es de corresponsal continua flujo normal
                if (sRes[1] == "1")
                {
                    //se crea la consulta para el monto total de las facturas
                    string sQuery1 = " select '$' + CONVERT(VARCHAR(100), convert(money, SUM(fMontoTotal)), 1)  from v_ListaCorresponsal where sMoneda = 'MXN' and iIdSubreferencia = " + objReferencia.iIdSubReferencia;
                    string[] sRes1 = objConexion.ejecutarConsultaRegistroSimple(sQuery1);

                    if (sRes1[0] == "1")
                    {
                        objReferencia.sTotalFactura = sRes1[1];
                        if (objReferencia.sTotalFactura == "") objReferencia.sTotalFactura = "$0";
                        // se checa moneda en Dolares
                        sQuery1 = " select '$' + CONVERT(VARCHAR(100), convert(money, SUM(ISNULL(fMontoTotal,0))), 1)  from v_ListaCorresponsal where sMoneda = 'USD' and iIdSubreferencia = " + objReferencia.iIdSubReferencia;
                        sRes1 = objConexion.ejecutarConsultaRegistroSimple(sQuery1);

                        if (sRes1[0] == "1")
                        {
                            //Se retorna el sResultado 
                            objReferencia.sTotalFacturaUSD = sRes1[1];
                            if (objReferencia.sTotalFacturaUSD == "") objReferencia.sTotalFacturaUSD = "$0";
                        }
                    }
                    else
                    {
                        //Se retorna el sResultado 
                        objReferencia.sTotalFactura = sRes1[1];
                        objReferencia.iResultado = 0;
                    }
                }
                else ///Si es corresponsal se realiza lo siguiente
                {
                    //Se coloca el resulta;
                    objReferencia.iResultado = 0;
                }
            }
            else
            {
                //Se retorna el sResultado 
                objReferencia.iResultado = int.Parse(sRes[0]);
            }
        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = "Ocurrio un error inesperado, favor de comunicarse con el administrador";
        }
    }

    /// <summary>
    /// Método para obtener datos Notas Crédito 
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_verificarDatosTablaNotaC(Referencia objReferencia)
    {
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //sQuery para validar embalajes
            string sQuery = " select count(iIdSubReferencia) from v_ListaNotaCreditoReferencia where iIdSubReferencia = " + objReferencia.iIdSubReferencia;
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

            if (sRes[0] == "1")
            {
                //Se retorna el sResultado 
                objReferencia.iResultado = int.Parse(sRes[0]);
                objReferencia.iNotasC = int.Parse(sRes[1]);

            }
            else
            {
                //Se retorna el sResultado 
                objReferencia.iResultado = int.Parse(sRes[0]);
            }
        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = ex.Message;
        }
    }


    /// <summary>
    /// Método para guardar el referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_AgregarGastoTerceroImpuesto_Anticipo(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_AgregarGastoImpuesto_Anticipo", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdAnticipo", SqlDbType.Int, objReferencia.iIdAanticipo.ToString());
                objConexion.agregarParametroSP("@fGastosTerceros", SqlDbType.Float, objReferencia.fGastoTercero.ToString());
                //Se ejecuta el SP
                string[] sResOut = objConexion.ejecutarProcOUTPUT_INT("@sResOut");
                if (sResOut[1] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objReferencia.iResultado = 1;
                    objReferencia.sMensaje = "Gasto de Terceros e Impuesto agregado con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para obtener datos de la referencia
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_ValidarReferenciaTerrestre(Referencia objReferencia)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "iValido" };
        //Se crea la consulta
        string sQuery = "SELECT CASE WHEN(tsubr.idSubReferencia in (select idSubReferencia from tReferenciaInstancia where idInstancia=1)OR tsubr.idAduana in (select idAduana from tComitenteAduana where idComitente=3))" +
        "THEN '1' ELSE '0' END	AS iValido,tsubr.idClienteOperativo iIdClienteOperativo FROM tSubReferencia tsubr WHERE tsubr.idSubReferencia= '" + objReferencia.iIdSubReferencia + "'";
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<Referencia>(sQuery, arrAtributos, objReferencia);
        //Se asigna el resultado
        objReferencia.iResultado = 1;
    }

    /// <summary>
    /// Método para obtener refAdministrativa
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ObtenerRefAdministrativa(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();

        try
        {

            List<string> lstParametros = new List<string>();
            List<string> lstDatos = new List<string>();

            lstParametros.Add("@iIdAduana");
            lstParametros.Add("@iIdTipoReferencia");

            lstDatos.Add(Convert.ToString(objReferencia.iIdAduana.ToString()));
            lstDatos.Add(Convert.ToString(objReferencia.iIdTipoReferencia.ToString()));

            //Se ejecuta el SP
            string sResultado = objConexion.EjecutaStoreProc(lstParametros, lstDatos, "pa_ObtenerRefAdministrativa");

            if (sResultado != "0")
            {
                string srefAdministrativa = "";
                string srefAdministrativa2 = "";

                if (objReferencia.iIdTipoReferencia == 12)
                {
                    srefAdministrativa = sResultado.Substring(0, 5);
                    srefAdministrativa2 = sResultado.Substring(5, 4);
                    srefAdministrativa = srefAdministrativa + 'Z' + srefAdministrativa2;
                    objReferencia.sNoReferenciaAdmin = srefAdministrativa;
                }
                else
                {
                    objReferencia.sNoReferenciaAdmin = sResultado;
                }

                objReferencia.iResultado = 1;
                objReferencia.sMensaje = "Referencia Administrativa consultada con éxito.";

            }
            else
            {
                objReferencia.sNoReferenciaAdmin = "";
                //Se retorna el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = "Eror al consultar Referencia Administrativa.";
            }
        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = ex.Message;
        }
    }

    /// <summary>
    /// metodo para obtener estado de la ultima linea enviada a SS
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ObtenerEstadoReferenciaION(Referencia objReferencia)
    {
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //se crea la consulta para traer los datos de la referencia ION ID
            string sQuery = " select top 1 idReferenciaSS from tReferencia_ION trion where trion.idSubReferencia = " + objReferencia.iIdSubReferencia + " order by FECHA_ION desc ";
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            // revisa si la consulta se hizo correctamente
            if (sRes[0] == "1")
            {
                // checa si el resultado de la consulta no es vacio
                if (sRes[1] != "")
                {
                    // pone id de la referencia ION
                    objReferencia.iIdReferencia = int.Parse(sRes[1]);
                }
                else
                {
                    // pone 0 en id Referencia ION
                    objReferencia.iIdReferencia = 0;
                    objReferencia.sMensaje = "¿Enviar referencia a SUN?";
                }
            }
            else
            {
                // pone error ya que la consulta no se hizo correctamente
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = sRes[1];
                objReferencia.iIdReferencia = 0;
            }
            // revisa si existe elementos para referencia ION
            if (objReferencia.iIdReferencia != 0)
            {
                // obtiene el ION_FLAG, para ver si hubo error
                string sQuery1 = " select top 1 ION_FLAG from tReferencia_ION trion where trion.idSubReferencia = " + objReferencia.iIdSubReferencia + " order by FECHA_ION desc ";
                string[] sRes1 = objConexion.ejecutarConsultaRegistroSimple(sQuery1);
                //Valida el monto de la Referencia
                if (sRes1[0] == "1")
                {
                    // Pone el mensaje correspondiente
                    if (sRes1[1] == "2")
                    {
                        objReferencia.sMensaje = "Rechazada por Sun Systems";


                    }
                    else if (sRes1[1] == "1")
                    {
                        objReferencia.sMensaje = "Enviada con exito";
                    }
                    else
                    {
                        objReferencia.sMensaje = "En proceso de envío, comuniquese con el administrador";
                    }
                }
                else
                {
                    objReferencia.iResultado = 0;
                    objReferencia.sMensaje = sRes[1];
                }
            }
            //Se retorna el sResultado 
        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = ex.Message;
        }
    }
    /// <summary>
    /// Procedimiento para reenviar una referencia y el pedimento
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_EnviarReferenciaION(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_ReenviarReferenciaION", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objReferencia.iIdSubReferencia.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_STRING("@sResOut");
                if (sResOut[0] == "1")
                {
                    if (sResOut[1] == "1")
                    {
                        objReferencia.iResultado = 1;
                        objReferencia.sMensaje = "Referencia reenviada con exito";
                    }
                    else
                    {
                        //Se retorna el mensaje de éxito
                        objReferencia.iResultado = 0;
                        objReferencia.sMensaje = "Ocurrio un error, consulte con el administrador";
                    }
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }
    /// <summary>
    /// funcion para validar el numero de oficinas por aduana segun la referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ValidarAduanaOficina(Referencia objReferencia)
    {
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //se crea la consulta para traer los datos
            string sQuery = " select count(*) from tOficinaAduana tfa " +
                                " where tfa.idAduana in (select ts.idAduana from tSubReferencia ts " +
                                " where ts.idSubReferencia = " + objReferencia.iIdSubReferencia + " ) ";
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            // revisa si la consulta se hizo correctamente
            if (sRes[0] == "1")
            {
                // saca el numero de relaciones
                objReferencia.iResultado = int.Parse(sRes[1]);
                // una aduana no puede tener mas de una oficina, se tiene que revisar
                if (objReferencia.iResultado > 1)
                {
                    objReferencia.sMensaje = "Revisa con el administrador la Aduana-Oficina";
                }
            }
            else
            {
                // pone error ya que la consulta no se hizo correctamente
                objReferencia.iResultado = 2;
                objReferencia.sMensaje = sRes[1];
            }
            // revisa si existe elementos para referencia ION

        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = ex.Message;
        }
    }
    /// <summary>
    /// funcion para relacionar una aduana con oficina segun la referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_RelacionarAduanaOficina(Referencia objReferencia)
    {
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //se crea la consulta para traer los datos
            string sQuery = " select ts.idAduana from tSubReferencia ts " +
                                " where ts.idSubReferencia = " + objReferencia.iIdSubReferencia;
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            // revisa si la consulta se hizo correctamente
            if (sRes[0] == "1")
            {
                if (int.Parse(sRes[1]) > 0)
                {
                    // se instancia clase de oficina
                    Oficina objOficina = new Oficina();
                    // se pasan los parametros
                    objOficina.iIdOficina = iIdOficina;
                    objOficina.iIdAduana = int.Parse(sRes[1]);
                    // se llama a funcion para guardar oficina aduana 
                    objOficina.fn_GuardarAduanaOficina(objOficina);
                    // saca manda mensaje de exito
                    objReferencia.iResultado = 1;
                    objReferencia.sMensaje = "Oficina - Aduana relaciona con exito";


                }
                else
                {
                    objReferencia.sMensaje = "Ocurrio un error, comuniquese con el adminstrador";
                }
            }
            else
            {
                // pone error ya que la consulta no se hizo correctamente
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = sRes[1];
            }
        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = ex.Message;
        }
    }

    public string fn_CalcularMontoFacturasCorresponsal(int iIdSubreferencia)
    {
        //Se instancia la clase
        Conexion objConexion = new Conexion();
        string sResult = "";
        //Se ejecuta el procedimiento que realiza la suma de las facturas
        string sRes1 = objConexion.generarSP("pa_CalcularMontoFacturasCorresponsal", 1);
        //Se valida que exista
        if (sRes1 == "1")
        {
            try
            {
                //Se asignan los parametros
                objConexion.agregarParametroSP("@iIdSubreferencia", SqlDbType.Int, iIdSubreferencia.ToString());
                //Se ejecuta el procedimiento
                string[] sResOut = objConexion.ejecutarProcOUTPUT_STRING("@sResOut");
                //Valida si se ejecuto correctamente
                if (sResOut[0] == "1")
                {
                    //Se asigna el valor 
                    sResult = sResOut[1];
                }
                else
                {
                    //Se coloca valores en 0
                    sResult = "0.00|0.00|0.00";
                }
            }
            catch (Exception ex)
            {
                sResult = "0.00|0.00|0.00";
            }
        }
        return sResult;
    }

    public void fn_ObtenerFacturasAsociadas(Referencia objReferencia)
    {
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //sQuery para validar embalajes
            string sQuery = @" select string_agg(tf.idFactura,',') from tFactura tf 
                        inner join tSubReferencia ts on ts.idSubReferencia=tf.idSubReferencia
                        where (tf.idFormaPago = 1 and tf.idEstatusFactura in (2, 6, 14, 15, 18, 19, 20)) 
                        and ts.idEstatusReferencia in(2,5,10,11) and isnull(tf.honorarios,0)=0
                        and tf.idSubReferencia= " + objReferencia.iIdSubReferencia;
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

            if (sRes[0] == "1" && sRes[1] != "")
            {
                //Se retorna el sResultado 
                objReferencia.sFacturasRelacionadas = sRes[1];
                int val = objReferencia.sFacturasRelacionadas.Split(',').Length;
                objReferencia.iCantFacturasRelacionadas = val;
                objReferencia.iResultado = 1;
            }
            else
            {
                //Se retorna el sResultado 
                objReferencia.sFacturasRelacionadas = "";
                objReferencia.iCantFacturasRelacionadas = 0;
                objReferencia.iResultado = 0;
            }
        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = ex.Message;
        }
    }

    /// <summary>
    /// Método para cambiar las facturas de referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_CambiarReferencia(Referencia objReferencia)
    {
        //Se crea arreglo para obtener cada factura
        string[] arrFacturas = objReferencia.sFacturas.Split(',');
        //Variable para obtener el num factura
        string sNumFactura = "";
        //Arreglo de datos devueltos
        string[] sRespuesta;
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Lista para facturas correctas
        List<string> lstFacturasCorrectas = new List<string>();
        //Se vacia la variable sFacturas para recontruir con facturas aprobadas
        objReferencia.sFacturas = "";
        //Se crea varible para asignar error
        bool bBandera; string sObservacion = "";
        //Se crea tabla cpn desglose de facturas
        DataTable dtFacturas = new DataTable();

        //Se genera la estructura de la tabla
        string[] arrCol = new string[] { "No.Factura", "Estatus", "Observación" };
        //tabla de validacion 
        DataTable dt_validacion = new DataTable();
        //se construye la tabla
        Utilerias objUtilerias = new Utilerias();
        objUtilerias.fn_GeneraDataTable(dtFacturas, arrCol);

        //Se recorren las facturas
        foreach (string sFactura in arrFacturas)
        {
            sNumFactura = "select serie+noFactura from tFactura where idFactura="+sFactura;
            sRespuesta = objConexion.ejecutarConsultaRegistroSimple(sNumFactura);
            sNumFactura = sRespuesta[1];
            //Se validan por separado las facturas para verificar que no tengan Notas de Credito relacionadas
            if (fn_ValidarFacturaNotaCredito(int.Parse(sFactura)))//Si la factura se aprueba se asigna a la variable, de no ser asi no se asigna
            {
                //Se agrega verdadero
                bBandera = true;
                sObservacion = "Factura cambiada correctamente";
                //Se asigna la lista
                lstFacturasCorrectas.Add(sFactura);
            }
            else
            {
                //Si no se agrega con error
                bBandera = false;
                sObservacion = "La factura no se puede cambiar porque esta relacionada a una Nota de Crédito";
            }
            //Se asigna a la tabla el numero de factura y la observacion
            dtFacturas.Rows.Add(sNumFactura, bBandera == true ? "Cambiada</br><span class='fa fa-check fa-green-sm'></span>" : "No cambiada</br><span class='fa fa-times-circle fa-red-sm'", sObservacion);
        }
        dtFacturas.DefaultView.Sort = "No.Factura asc";
        //Se asignan los campos sin filtro
        string[] arrColumnasFiltro = { "No. Factura" };
        string[] arrColumnasSinFiltro = { "Estatus", "Observación" };
        //Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblFacturasNC";
        //Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraTabla(objUtilerias, dtFacturas);
        //Se asigna el contenido de la tabla a la variable
        objReferencia.sDesgloseFacturas = objUtilerias.sContenido;

        //Se valida si la lista de facturas correctas tiene por lo menos una
        if (lstFacturasCorrectas.Count > 0)
        {
            //Se agrega a la variable dichas facturas
            objReferencia.sFacturas = String.Join(",", lstFacturasCorrectas.ToArray());
        }
        else
        {
            //Si no hay facturas correctas no se continua
            //Se guarda el mensaje de error
            objReferencia.iResultado = 1;
            objReferencia.sMensaje = "No se cambio ninguna factura";
            return;
        }
        //*********************************************************SE HACE EL CAMBIO DE LAS FACTURAS CORRECTAS****************************
        
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_CambiarFacturasCredito", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubreferencia", SqlDbType.BigInt, objReferencia.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@iIdNuevaSubreferencia", SqlDbType.BigInt, objReferencia.sIdNuevaSubreferencia.ToString());
                objConexion.agregarParametroSP("@sFacturas", SqlDbType.VarChar, objReferencia.sFacturas.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_STRING("@iResOut");
                if (sResOut[0] == "1")
                {
                    if (sResOut[1] == "1")
                    {
                        //Se guarda el mensaje de error
                        objReferencia.iResultado = 1;
                        objReferencia.sMensaje = "Facturas cambiadas correctamente";
                    }
                    else
                    {
                        //Se guarda el mensaje de error
                        objReferencia.iResultado = 0;
                        objReferencia.sMensaje = "Ocurrió un error inesperado, favor de contactar al administrador";
                    }
                }
                //Se guarda en el log
                fn_GuardarLogCambioFacturas(objReferencia);
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }

    #region fn_GuardarLogCambioFacturas
    /// <summary>
    /// Método para guardar el log del cambio de referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_GuardarLogCambioFacturas(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se crea la query
        string sQuery = "insert into tLogCambioFacturasReferencia(idUsuario,idSubreferenciaA,idSubreferenciaN,fechaCambio,observacion,facturas)" +
                        "values(" + objReferencia.iIdUsuario + "," + objReferencia.iIdSubReferencia + ", " + int.Parse(objReferencia.sIdNuevaSubreferencia) + ", GETDATE(), '" + objReferencia.sMensaje + "','" + objReferencia.sFacturas + "')";

        string sRes = objConexion.ejecutarComando(sQuery);
        if (sRes == "1")
        {
        }
    }
    #endregion

    public bool fn_EsTerrestre(Referencia objReferencia)
    {
        try
        {
            Conexion objConexion = new Conexion();
            string sQuery = @"SELECT TOP 1 CASE WHEN idComitente = 3 THEN 1 ELSE 0 END FROM tusuariocomitente WHERE idusuario=" + objReferencia.iIdUsuario;
            //Se ejecuta la consulta
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

            if (sRes[0] == "1")
            {
                if (sRes[1] == "1")
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
        catch (Exception ex)
        {
            return false;
        }
    }

    #region fn_AgregarSeleccion
    /// <summary>
    /// Método para quitar agregar la factura seleccionada en BD
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_CambiarSeleccion(Referencia objReferencia)
    {
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //sQuery
            string sQuery = "";
            //Se valida la selecion
            if (objReferencia.iTipoSeleccion == 0)//Se elimina la seleccion
            {
                sQuery = "update tFactura set iSeleccion=0 where idFactura=" + objReferencia.iIdFactura;
            }
            else if (objReferencia.iTipoSeleccion == 1)//Se agrega la seleccion
            {
                sQuery = "update tFactura set iSeleccion=1 where idFactura=" + objReferencia.iIdFactura;
            }
            else if (objReferencia.iTipoSeleccion == 2)//Seleccion masiva
            {
                sQuery = "update tFactura set iSeleccion=1 where idFactura in(" + objReferencia.sFacturas + ")";
            }
            else//Deseleccion masiva
            {
                sQuery = "update tFactura set iSeleccion=0 where idFactura in(" + objReferencia.sFacturas + ")";
            }
            //Se ejecuta la actualizacion
            string sRes = objConexion.ejecutarComando(sQuery);

            //Se valida si fue correcto
            if (sRes == "1")
            {
                //Se retorna el sResultado 
                if (objReferencia.iTipoSeleccion == 0)
                {
                    objReferencia.sMensaje = "Factura deseleccionada";
                }
                else if (objReferencia.iTipoSeleccion == 1)
                {
                    objReferencia.sMensaje = "Factura seleccionada";
                }
                else if (objReferencia.iTipoSeleccion == 2)
                {
                    objReferencia.sMensaje = "Facturas seleccionadas";
                }
                else
                {
                    objReferencia.sMensaje = "Facturas deseleccionadas";
                }
                objReferencia.iResultado = 1;
            }
            else
            {
                //Se retorna el sResultado 
                objReferencia.sMensaje = "Ocurrio un error";
                objReferencia.iResultado = 0;
            }
        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = ex.Message;
        }
    }
    #endregion


    #region fn_ConfirmarSalida
    /// <summary>
    /// Método para quitar la seleccion de facturas al salir de la pagina
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ConfirmarSalida(Referencia objReferencia)
    {
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //sQuery 
            string sQuery = "update tFactura set iSeleccion=0 where (idFormaPago = 1 and idEstatusFactura in (2, 6, 14, 15, 18, 19, 20)) and idSubReferencia =" + objReferencia.iIdSubReferencia;
            string sRes = objConexion.ejecutarComando(sQuery);

            if (sRes == "1")
            {
                //Se retorna el sResultado 
                objReferencia.sMensaje = "Facturas deseleccionadas";
                objReferencia.iResultado = 1;
            }
            else
            {
                //Se retorna el sResultado 
                objReferencia.sMensaje = "Ocurrio un error";
                objReferencia.iResultado = 0;
            }
            //Se deseleccionan los impuestos
            sQuery = "update tImpuesto set iSeleccion=0 where idSubreferenciaN=" + objReferencia.iIdSubReferencia;
            sRes = objConexion.ejecutarComando(sQuery);
        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = ex.Message;
        }
    }
    #endregion

    #region Impuestos

    #region fn_ObtenerImpuestos
    /// <summary>
    /// Método para obtener los impuestos asociados a la referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_ObtenerImpuestos(Referencia objReferencia)
    {
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            /*
            string sQuery = @" select STRING_AGG(idImpuesto,',') from tImpuesto ti
                            inner join tPolizaEgreso tp on tp.idPolizaEgreso = ti.idPoliza
                            inner join tSubReferencia ts on ts.idSubReferencia=ti.idSubreferenciaN
                            where tp.idEstatusPoliza = 1 and ts.idEstatusReferencia in(2,5,10,11) and
							ti.idSubreferenciaN =" + objReferencia.iIdSubReferencia;

            */

            string sQuery = @"
                        SELECT STUFF( (select ','+convert(varchar(30),idImpuesto) from tImpuesto ti
                        inner join tPolizaEgreso tp on tp.idPolizaEgreso = ti.idPoliza
                        inner join tSubReferencia ts on ts.idSubReferencia=ti.idSubreferenciaN
                        where tp.idEstatusPoliza = 1 and ts.idEstatusReferencia in(2,5,10,11) and
                        ti.idSubreferenciaN =" + objReferencia.iIdSubReferencia + @"
                        FOR XML PATH ('')),
                        1,1, '')";
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

            if (sRes[0] == "1" && sRes[1] != "")
            {
                //Se retorna el sResultado 
                objReferencia.sImpuestosRelacionados = sRes[1];
                int val = objReferencia.sImpuestosRelacionados.Split(',').Length;
                objReferencia.iCantImpuestosRelacionados = val;
                objReferencia.iResultado = 1;
            }
            else
            {
                //Se retorna el sResultado 
                objReferencia.sImpuestosRelacionados = "";
                objReferencia.iCantImpuestosRelacionados = 0;
                objReferencia.iResultado = 0;
            }
        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objReferencia.sImpuestosRelacionados = "";
            objReferencia.iCantImpuestosRelacionados = 0;
            objReferencia.iResultado = 0;
        }
    }
    #endregion 

    #region fn_CambiarSeleccionImpuestos
    /// <summary>
    /// Método para quitar o agregar impuestos a la seleccion
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_CambiarSeleccionImpuestos(Referencia objReferencia)
    {
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //sQuery
            string sQuery = "";
            //Se valida la selecion
            if (objReferencia.iTipoSeleccion == 0)//Se elimina la seleccion
            {
                sQuery = "update tImpuesto set iSeleccion=0 where idImpuesto=" + objReferencia.iIdImpuesto;
            }
            else if (objReferencia.iTipoSeleccion == 1)//Se agrega la seleccion
            {
                sQuery = "update tImpuesto set iSeleccion=1 where idImpuesto=" + objReferencia.iIdImpuesto;
            }
            else if (objReferencia.iTipoSeleccion == 2)//Seleccion masiva
            {
                sQuery = "update tImpuesto set iSeleccion=1 where idImpuesto in(" + objReferencia.sImpuestos + ")";
            }
            else//Deseleccion masiva
            {
                sQuery = "update tImpuesto set iSeleccion=0 where idImpuesto in(" + objReferencia.sImpuestos + ")";
            }
            //Se ejecuta la actualizacion
            string sRes = objConexion.ejecutarComando(sQuery);

            //Se valida si fue correcto
            if (sRes == "1")
            {
                //Se retorna el sResultado 
                if (objReferencia.iTipoSeleccion == 0)
                {
                    objReferencia.sMensaje = "Impuesto deseleccionado";
                }
                else if (objReferencia.iTipoSeleccion == 1)
                {
                    objReferencia.sMensaje = "Impuesto seleccionado";
                }
                else if (objReferencia.iTipoSeleccion == 2)
                {
                    objReferencia.sMensaje = "Impuestos seleccionados";
                }
                else
                {
                    objReferencia.sMensaje = "Impuestos deseleccionados";
                }
                objReferencia.iResultado = 1;
            }
            else
            {
                //Se retorna el sResultado 
                objReferencia.sMensaje = "Ocurrio un error";
                objReferencia.iResultado = 0;
            }
        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = ex.Message;
        }
    }
    #endregion

    #region fn_CambiarReferenciaImpuestos
    /// <summary>
    /// Método para cambiar los impuestos de referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_CambiarReferenciaImpuestos(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_CambiarImpuestosReferencia", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubreferencia", SqlDbType.BigInt, objReferencia.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@iIdNuevaSubreferencia", SqlDbType.BigInt, objReferencia.sIdNuevaSubreferencia.ToString());
                objConexion.agregarParametroSP("@sImpuestos", SqlDbType.VarChar, objReferencia.sImpuestos.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_STRING("@iResOut");
                if (sResOut[0] == "1")
                {
                    if (sResOut[1] == "1")
                    {
                        //Se guarda el mensaje de error
                        objReferencia.iResultado = 1;
                        objReferencia.sMensaje = "Impuestos cambiados correctamente";
                    }
                    else
                    {
                        //Se guarda el mensaje de error
                        objReferencia.iResultado = 0;
                        objReferencia.sMensaje = "Ocurrió un error inesperado, favor de contactar al administrador";
                    }
                }
                //Se guarda en el log
                fn_GuardarLogCambioImpuestos(objReferencia);
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }
    #endregion

    #region fn_GuardarLogCambioImpuestos
    /// <summary>
    /// Método para guardar el log del cambio de referencia en impuestos
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_GuardarLogCambioImpuestos(Referencia objReferencia)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se crea la query
        string sQuery = "insert into tLogCambioImpuestosReferencia(idUsuario,idSubreferenciaA,idSubreferenciaN,fechaCambio,observacion,impuestos)" +
                        "values(" + objReferencia.iIdUsuario + "," + objReferencia.iIdSubReferencia + ", " + int.Parse(objReferencia.sIdNuevaSubreferencia) + ", GETDATE(), '" + objReferencia.sMensaje + "','" + objReferencia.sImpuestos + "')";

        string sRes = objConexion.ejecutarComando(sQuery);
        if (sRes == "1")
        {
        }
    }
    #endregion

    #endregion

    /// <summary>
    /// Funcion para validar si la Factura tiene relacion a una Nota de credito
    /// </summary>
    /// <param name="iIdFactura"></param>
    /// <returns></returns>
    public bool fn_ValidarFacturaNotaCredito(int iIdFactura)
    {
        //Se instancia la clase
        Conexion objConexion = new Conexion();
        //Se crea la consulta
        string sQuery = @"select count(idServicioNotaCreditoFactura) from tServicioNotaCreditoFactura 
                        where idFactura = " + iIdFactura;
        //Se ejecuta la consulta
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se valida si se ejecuto la consulta correctamente
        if (sRes[0] == "1")
        {
            //Se valida si la factura puede proceder a cambio,eliminacion, o cancelacion
            if (int.Parse(sRes[1]) == 0)
            {
                //Se manda verdadero y procede
                return true;
            }
            else
            {
                //Al no proceder se envia falso
                return false;
            }
        }
        else
        {
            //al haber error se manda falso
            return false;
        }
    }

    #region fn_ObtenerMensajeNC
    /// <summary>
    /// Método para obtener mensaje de confirmacion de la NC
    /// </summary>
    /// <param name="sIdNotaCredito"></param>
    /// <returns></returns>
    public Resultado fn_ObtenerMensajeNC(int sIdNotaCredito)
    {
        //Se instancia la clase
        Resultado objResultado = new Resultado();
        Conexion objConexion = new Conexion();
        //Se crea la consulta
        string sQuery = @"select 
                CASE WHEN ((select count(1) from tServicioNotaCreditoFactura where idServicioNotaCredito 
			                in(select idServicioNotaCredito from tServicioNotaCredito tsnc where tsnc.idNotaCredito=tnc.idNotaCredito))>0)
                THEN 1
                ELSE 
	                CASE WHEN((select count(1) from tNotaCreditoFactura tncf where tncf.idNotaCredito=tnc.idNotaCredito)>0)
	                THEN 1 ELSE 0 END
                END
                from tNotaCredito tnc 
                where tnc.idNotaCredito="+ sIdNotaCredito;
        //Se ejecuta la consulta
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se valida si se ejecuto correctamente
        if (sRes[0] == "1")
        {
            //Se valida si el resultado es valido
            if (sRes[1] == "1")
            {
                //Se asigna resultado
                objResultado.iResultado = 1;
                objResultado.sMensaje = "Existen facturas relacionadas a la nota de crédito las cuales quedarán desvinculadas, ¿Está seguro de continuar..?";
            }
            else
            {
                //Se asigna resultado
                objResultado.iResultado = 2;
                objResultado.sMensaje = "¿Está seguro de desasignar la nota crédito?";
            }
        }
        else
        {
            //Se asigna resultado
            objResultado.iResultado = 0;
            objResultado.sMensaje = "Favor de cerrar el dialog y volver a intentar";
        }
        //Se retorna el resultado
        return objResultado;
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="iIdSubreferencia"></param>
    public Referencia fn_ObtenerHonorariosCorresponsal(int iIdSubreferencia)
    {
        //Se crea la clase a retornar
        Referencia objReferencia = new Referencia();
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //sQuery para validar embalajes
            string sQuery = @" select string_agg(tf.idFactura,',') from tFactura tf 
                        inner join tSubReferencia ts on ts.idSubReferencia=tf.idSubReferencia
                        where (tf.idFormaPago = 1 and tf.idEstatusFactura in (2, 6, 14, 15, 18, 19, 20)) 
                        and ts.idEstatusReferencia in(2,5,10,11) and isnull(tf.honorarios,0)=1
                        and tf.idSubReferencia= " + iIdSubreferencia;
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

            if (sRes[0] == "1" && sRes[1] != "")
            {
                //Se retorna el sResultado 
                objReferencia.sHonorariosRelacionados = sRes[1];
                int val = objReferencia.sHonorariosRelacionados.Split(',').Length;
                objReferencia.iCantHonorariosRelacionados = val;
                objReferencia.iResultado = 1;
            }
            else
            {
                //Se retorna el sResultado 
                objReferencia.sHonorariosRelacionados = "";
                objReferencia.iCantHonorariosRelacionados = 0;
                objReferencia.iResultado = 0;
            }
        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objReferencia.iResultado = 0;
            objReferencia.sMensaje = ex.Message;
        }
        return objReferencia;
    }

    /// <summary>
    /// Metodo para cambiar las facturas de honorarios de refrencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_CambiarReferenciaHonorario(Referencia objReferencia)
    {
        //Se instancia la clase
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_CambiarFacturasCredito", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubreferencia", SqlDbType.BigInt, objReferencia.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@iIdNuevaSubreferencia", SqlDbType.BigInt, objReferencia.sIdNuevaSubreferencia.ToString());
                objConexion.agregarParametroSP("@sFacturas", SqlDbType.VarChar, objReferencia.sFacturas.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_STRING("@iResOut");
                if (sResOut[0] == "1")
                {
                    if (sResOut[1] == "1")
                    {
                        //Se guarda el mensaje de error
                        objReferencia.iResultado = 1;
                        objReferencia.sMensaje = "Facturas cambiadas correctamente";
                    }
                    else
                    {
                        //Se guarda el mensaje de error
                        objReferencia.iResultado = 0;
                        objReferencia.sMensaje = "Ocurrió un error inesperado, favor de contactar al administrador";
                    }
                }
                //Se guarda en el log
                fn_GuardarLogCambioFacturas(objReferencia);
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = ex.Message;
            }
        }
    }

    public void fn_comparobar_solicitudes_cerrarDG(Referencia objReferencia)
    {
        Conexion objconexion = new Conexion();
        List<string> list;
        string Query = @"SELECT idSolicitudTransferencia FROM tSolicitudTransferencia WHERE idSubreferencia='" + objReferencia.iIdSubReferencia + "' and  idEstatusSolicitudTrans not in (5,8,9,10,11,12)";
        list = objconexion.ejecutarConsultaRegistroMultiples(Query);
        if (list[0] == "1")
        {
            if (list.Count > 1)
            {
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = "Existen Solicitudes sin Recuperar, Regreso Parcial o Cobrado";


            }
            else
            {
                objReferencia.iResultado = 1;
                objReferencia.sMensaje = "La referencia se modifico a Cerrada/DG";
            }
        }
        else
        {
            objReferencia.iResultado = 2;
            objReferencia.sMensaje = list[0].ToString();

        }


    }
    public void fn_comparobar_solicitudes_cierreautomatico_cerrarDG(Referencia objReferencia)
    {
        Conexion objconexion = new Conexion();
        List<string> list;
        string Query = @"SELECT idSolicitudTransferencia FROM tSolicitudTransferencia WHERE idSubreferencia='" + objReferencia.iIdSubReferencia + "' and  idEstatusSolicitudTrans not in (5,9,10,11)";
        list = objconexion.ejecutarConsultaRegistroMultiples(Query);
        if (list[0] == "1")
        {
            if (list.Count > 1)
            {
                objReferencia.iResultado = 0;
                objReferencia.sMensaje = "Existen Solicitudes sin Recuperar, Regreso Parcial o Cobrado";


            }
            else
            {
                objReferencia.iResultado = 1;
                objReferencia.sMensaje = "La referencia se modifico a Cerrada/DG";
            }
        }
        else
        {
            objReferencia.iResultado = 2;
            objReferencia.sMensaje = list[0].ToString();

        }


    }
}