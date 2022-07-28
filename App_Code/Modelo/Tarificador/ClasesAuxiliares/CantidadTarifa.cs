using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Clase para obtener la cantidad de una subreferencia
/// </summary>
public class CantidadTarifa
{
    /// <summary>
    /// Método para obtener la cantidad dependiendo del tipo de tarifa
    /// </summary>
    /// <param name="iIdTipoTarifa"></param>
    /// <param name="iIdSubReferencia"></param>
    /// <returns></returns>
    public decimal fn_ObtenerCantidad(int iIdTipoTarifa, int iIdSubReferencia, int iIdTarifaReferencia)
    {
        //Verifica si la tarifa es por peso, en cuyo caso ignora todo y obtiene la cantidad por peso
        //Query obtener la regla
        string sQuery = "SELECT ctrt.idTipoRegla " +
                "FROM cTipoReglaTarifa ctrt " +
                "INNER JOIN " +
                "tReglaTarifa trt  " +
                "ON ctrt.idTipoRegla = trt.idTipoRegla " +
                "INNER JOIN " +
                "tTarifaReferencia ttr " +
                "ON ttr.idConfTarifa = trt.idConfTarifa " +
                "WHERE ttr.idTarifaReferencia = " + iIdTarifaReferencia;
        //Instancia clase de conexion
        Conexion oConexion = new Conexion();
        //Se obtiene el resultado
        string sIdRegla = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        //If para saber si es por peso
        if(sIdRegla == "4")
            return fn_ObtenerCantidadPorPeso(iIdSubReferencia);
        // Switch por tipo tarifa
        switch (iIdTipoTarifa)
        {
                //Pedimento
            case 6:  return fn_ObtenerCantidadPorPedimento("F4, F5", iIdSubReferencia);
            case 33: return fn_ObtenerCantidadPorPedimento("", iIdSubReferencia);
            case 34: return fn_ObtenerCantidadPorPedimento("", iIdSubReferencia);
            case 35: return fn_ObtenerCantidadPorPedimento("", iIdSubReferencia);
            case 36: return fn_ObtenerCantidadPorPedimento("A1", iIdSubReferencia);
            case 37: return fn_ObtenerCantidadPorPedimento("A3", iIdSubReferencia);
            case 38: return fn_ObtenerCantidadPorPedimento("AD", iIdSubReferencia);
            case 39: return fn_ObtenerCantidadPorPedimento("AF", iIdSubReferencia);
            case 40: return fn_ObtenerCantidadPorPedimento("F4", iIdSubReferencia);
            case 41: return fn_ObtenerCantidadPorPedimento("IN", iIdSubReferencia);
            case 42: return fn_ObtenerCantidadPorPedimento("R1", iIdSubReferencia);
            case 43: return fn_ObtenerCantidadPorPedimento("RT", iIdSubReferencia);
            case 44: return fn_ObtenerCantidadPorPedimento("V1", iIdSubReferencia);
            case 45: return fn_ObtenerCantidadPorPedimento("V4", iIdSubReferencia);
            case 46: return fn_ObtenerCantidadPorPedimento("CT", iIdSubReferencia);
            case 47: return fn_ObtenerCantidadPorPedimento("V1, V2, V5, V6, V7, V9, VD", iIdSubReferencia);
            case 52: return fn_ObtenerCantidadPorPedimento("R1", iIdSubReferencia);
            case 55: return fn_ObtenerCantidadPorPedimento("A3, RE", iIdSubReferencia);
                //Guia
            case 25: return fn_ObtenerCantidadPorGuia(iIdSubReferencia);
                //Facturas
            case 19:
            case 20: return fn_ObtenerCantidadPorFacturas(iIdSubReferencia);
                //Peso neto
            case 48:
            case 49: return fn_ObtenerCantidadPorPeso(iIdSubReferencia);
                //idReferencia
            case 54: return fn_ObtenerCantidadPorReferencia(iIdSubReferencia);
                //No se tiene
            default: return -1;
        }
    }

    /// <summary>
    /// Método para obtener la cantidad de guias de una subreferencia
    /// </summary>
    /// <param name="iIdSubReferencia">ID de la subreferencia</param>
    /// <returns>Cantidad de guias</returns>
    private decimal fn_ObtenerCantidadPorGuia(int iIdSubReferencia)
    {
        //Query para obtener la cantidad
        string sQuery = "SELECT COUNT(*) " +
                        "FROM tGuiaReferencia " +
                        "WHERE idSubReferencia = " + iIdSubReferencia;
        //Se instancia la conexion
        Conexion oConexion = new Conexion();
        //Se hace la consulta
        string sCantidad = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        //Si no existe la cantidad se asigna a 0
        if (String.IsNullOrEmpty(sCantidad))
            sCantidad = "0";
        //Retorna el resultado
        return decimal.Parse(sCantidad);
    }

    /// <summary>
    /// Método para obtener cantidad de facturas de una subreferencia
    /// </summary>
    /// <param name="iIdSubReferencia">ID de la subreferencia</param>
    /// <returns>Cantidad de facturas</returns>
    private decimal fn_ObtenerCantidadPorFacturas(int iIdSubReferencia)
    {
        //Query para obtener la cantidad
        string sQuery = "SELECT cantFacturasProveedor FROM tPedimento WHERE idSubReferencia = " + iIdSubReferencia;
        //Se instancia la conexion
        Conexion oConexion = new Conexion();
        //Se hace la consulta
        string sCantidad = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        //Si no existe la cantidad se asigna a 0
        if (String.IsNullOrEmpty(sCantidad))
            sCantidad = "0";
        //Retorna el resultado
        return decimal.Parse(sCantidad);
    }

    /// <summary>
    /// Método para obtener el peso de una subreferencia
    /// </summary>
    /// <param name="iIdSubReferencia">ID de la subreferencia</param>
    /// <returns>Peso</returns>
    private decimal fn_ObtenerCantidadPorPeso(int iIdSubReferencia)
    {
        //Query para obtener la cantidad
        string sQuery = "SELECT pesoNeto FROM tPedimento WHERE idSubReferencia = " + iIdSubReferencia;
        //Se instancia la conexion
        Conexion oConexion = new Conexion();
        //Se hace la consulta
        string sCantidad = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        //Si no existe la cantidad se asigna a 0
        if (String.IsNullOrEmpty(sCantidad))
            sCantidad = "0";
        //Retorna el resultado
        return decimal.Parse(sCantidad);
    }

    /// <summary>
    /// Método para obtener la cantidad de referencias de una subreferencia
    /// </summary>
    /// <param name="iIdSubReferencia">ID de la subreferencia</param>
    /// <returns>Cantidad de referencias</returns>
    private decimal fn_ObtenerCantidadPorReferencia(int iIdSubReferencia)
    {
        //Query para obtener la cantidad
        string sQuery = "SELECT COUNT(*) " +
                        "FROM tSubReferencia  " +
                        "WHERE idSubReferencia =  " + iIdSubReferencia;
        //Se instancia la conexión
        Conexion oConexion = new Conexion();
        //Se hace la consulta
        string sCantidad = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        //Si no existe la cantidad se asigna a 0
        if (String.IsNullOrEmpty(sCantidad))
            sCantidad = "0";
        //Retorna el resultado
        return decimal.Parse(sCantidad);
    }

    /// <summary>
    /// Métoodo para obtener la cantidad de pedimentos
    /// </summary>
    /// <param name="sWhere">Lista de claves de pedimentos a evaluar</param>
    /// <param name="iIdSubReferencia">ID de la subreferencia</param>
    /// <returns></returns>
    private decimal fn_ObtenerCantidadPorPedimento(string sWhere, int iIdSubReferencia)
    {
        //Se construye el where si trae elementos
        if (!String.IsNullOrEmpty(sWhere))
        {
            sWhere = " AND ccp.cve in (" + sWhere + ")";
        }
        //Query para obtener la cantidad
        string sQuery = "SELECT DISTINCT COUNT(pedimento) " + 
                        "FROM tPedimento tp " + 
                        "LEFT JOIN  " + 
                        "cClavesPedimento ccp " + 
                        "ON tp.idCveDocumento = ccp.idCvePedimento " +
                        "WHERE tp.idSubReferencia = " + iIdSubReferencia + sWhere;
        //Se instancia la conexion
        Conexion oConexion = new Conexion();
        //Se realiza la consulta
        string sCantidad = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        //Si no existe la cantidad se asigna a 0
        if (String.IsNullOrEmpty(sCantidad))
            sCantidad = "0";
        //Retorna el resultado
        return decimal.Parse(sCantidad);
    }
}