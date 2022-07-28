using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Clase para realizar los calculos cuya implementación no difiere
/// entre los distintos tipos de calculo
/// </summary>
public class CalculoCostoGeneral
{
    #region Metodos
    /// <summary>
    /// Método para obtener el tipo de cobro de una asociacion tarifa-referencia
    /// </summary>
    /// <param name="iIdTarifaReferencia">ID de la tarifa-referencia</param>
    /// <returns></returns>
    public int fn_ObtenerTipoCobro(int iIdTarifaReferencia)
    {
        //Qyery para obtener el tipo de cobro
        string sQuery = "SELECT " +
                        "    CASE WHEN idFormula IS NULL THEN 0 ELSE 1 END AS Porciento " +
                        "FROM tReglaTarifa " +
                        "WHERE idConfTarifa = (SELECT idConfTarifa " +
                                              "FROM tTarifaReferencia " +
                                              "WHERE idTarifaReferencia = " + iIdTarifaReferencia + ")";
        //Instancia de la clase conexion
        Conexion oConexion = new Conexion();
        //Se obtiene el resultado de la consulta
        string sTipoCobro = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        //Se retorna el tipo de cobro
        return int.Parse(sTipoCobro);
    }

    /// <summary>
    /// Método para obtener la cantidad para realizar calculos de tarificador
    /// </summary>
    /// <param name="dCantidad">Valor actual en la clase particular</param>
    /// <param name="iIdTipoTarifa">ID del tipo de tarifa</param>
    /// <param name="iIdSubReferencia">ID de la subreferencia</param>
    /// <returns></returns>
    public decimal fn_ObtenerCantidad(decimal dCantidad, int iIdTipoTarifa, int iIdSubReferencia, int iIdTarifaReferencia)
    {
        //Si ya se obtuvo la cantidad se retorna
        if (dCantidad != -2)
            return dCantidad;
        //Instancia clase de cantidad tarifa
        CantidadTarifa oCantidadTarifa = new CantidadTarifa();
        //Obtiene la cantidad
        dCantidad = oCantidadTarifa.fn_ObtenerCantidad(iIdTipoTarifa, iIdSubReferencia, iIdTarifaReferencia);
        return dCantidad;
    }

    /// <summary>
    /// Método para obtener la cantidad en un Input HTML
    /// </summary>
    /// <param name="iIdTarifaReferencia">ID de la tarifa-referencia</param>
    /// <param name="dCantidad">Valor de la cantidad a poner en el input</param>
    /// <returns></returns>
    public string fn_ObtenerCantidadHtml(int iIdTarifaReferencia, decimal dCantidad)
    {
        //Se da formato
        string sResultado = "<input type='text'  id='" + iIdTarifaReferencia + "' value='" +
                (dCantidad == -1 ? "0" : dCantidad.ToString())
                + "' class='input-sm form-control inpCantidad'/>";
        //Se retorna el resultado
        return sResultado;
    }

    /// <summary>
    /// Método para obtener el iva de una tarifa-referencia
    /// </summary>
    /// <param name="dIva">Valor actual en la instancia particular</param>
    /// <param name="iIdTarifaReferencia">ID de la tarifa-referencia</param>
    /// <returns></returns>
    public decimal fn_ObtenerIVA(decimal dIva, int iIdTarifaReferencia)
    {
        //Si ya se obtuvo el iva solo se retorna
        if (dIva != -1)
            return dIva;
        //Query para obtener el iva
        string sQuery = "SELECT ci.iva " +
                        "FROM tTarifaReferencia ttr " +
                        "     JOIN " +
                        "     tReglaTarifa trt " +
                        "     ON ttr.idConfTarifa = trt.idConfTarifa " +
                        "     JOIN cIva ci " +
                        "     ON trt.idIva = ci.idIva " +
                        "WHERE ttr.idTarifaReferencia = " + iIdTarifaReferencia;
        //Se instancia objeto de conexion
        Conexion oConexion = new Conexion();
        //Se obtiene el iva
        dIva = decimal.Parse(oConexion.ejecutarConsultaRegistroSimple(sQuery)[1]);
        //Retorna el valor
        return dIva;
    }

    /// <summary>
    /// Método para obtener la retención de una tarifa-referencia
    /// </summary>
    /// <param name="dRetencion">Valor actual en la instancia particular</param>
    /// <param name="iIdTarifaReferencia">ID de la tarifa-referencia</param>
    /// <returns></returns>
    public decimal fn_ObtenerRetencion(decimal dRetencion, int iIdTarifaReferencia)
    {
        //Si ya se obtuvo la retención se retorna
        if (dRetencion != -1)
            return dRetencion;
        //Query parqa obtener la retención
        string sQuery = "SELECT cr.retencion " +
                        "FROM tTarifaReferencia ttr " +
                        "     JOIN " +
                        "     tReglaTarifa trt " +
                        "     ON ttr.idConfTarifa = trt.idConfTarifa " +
                        "     JOIN cRetencion cr " +
                        "     ON trt.idRetencion = cr.idRetencion " +
                        "WHERE ttr.idTarifaReferencia = " + iIdTarifaReferencia;
        //Se instancia objeto de conexion
        Conexion oConexion = new Conexion();
        //Se obtiene la retención
        dRetencion = decimal.Parse(oConexion.ejecutarConsultaRegistroSimple(sQuery)[1]);
        //Retornaa el valor
        return dRetencion;
    }

    /// <summary>
    /// Método para obtener el cobro minimo de una tarifa-referencia
    /// </summary>
    /// <param name="dCobroMinimo">Valor actual en la instancia particular</param>
    /// <param name="iIdTarifaReferencia">ID de la tarifa-referencia</param>
    /// <returns></returns>
    public decimal fn_ObtenerCobroMinimo(decimal dCobroMinimo, int iIdTarifaReferencia)
    {
        //Si ya se obtuvo el cobro minimo se retorna
        if (dCobroMinimo != -1)
            return dCobroMinimo;
        //Query para obtener el cobro minimo
        string sQuery = "SELECT trt.cobroMinimo " +
                        "FROM tTarifaReferencia ttr " +
                        "     JOIN " +
                        "     tReglaTarifa trt " +
                        "     ON ttr.idConfTarifa = trt.idConfTarifa " +
                        "WHERE ttr.idTarifaReferencia = " + iIdTarifaReferencia;
        //Se instancia objeto conexión
        Conexion oConexion = new Conexion();
        //Se recupera el valor
        dCobroMinimo = decimal.Parse(oConexion.ejecutarConsultaRegistroSimple(sQuery)[1]);
        //Retorna el valor
        return dCobroMinimo;
    }

    /// <summary>
    /// Método para obtener la moneda de una tarifa-referencia
    /// </summary>
    /// <param name="sMoneda">Valor actual en la instancia particular</param>
    /// <param name="iIdTarifaReferencia">ID de la tarifa-referencia</param>
    /// <returns></returns>
    public string fn_ObtenerMoneda(string sMoneda, int iIdTarifaReferencia)
    {
        //Si ya se obtuvo la moneda se retorna
        if (!String.IsNullOrEmpty(sMoneda))
            return sMoneda;
        //Query para obtener la moneda
        string sQuery = "SELECT cm.cveMoneda " +
                        "FROM tTarifaReferencia ttr " +
                        "     JOIN " +
                        "     tConfiguracionTarifa tct " +
                        "     ON ttr.idConfTarifa = tct.idConfTarifa " +
                        "     JOIN " +
                        "     cMoneda cm " +
                        "     ON tct.idMoneda = cm.idMoneda " +
                        "WHERE ttr.idTarifaReferencia = " + iIdTarifaReferencia;
        //Instancia objeto conexion
        Conexion oConexion = new Conexion();
        //Se recupera el valor 
        sMoneda = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        //Se retorna el valor
        return sMoneda;
    }

    /// <summary>
    /// Metodo para obtener el total de la tarifa en HTML
    /// </summary>
    /// <param name="dCantidad">Valor de la cantidad</param>
    /// <param name="dResultado">Referencia a una variable donde almacenar el resultado</param>
    /// <param name="dTotalNoImpuestos">Referencia a una variable donde almacenar el total sin impuestos</param>
    /// <param name="oBaseCalculo">Objeto de base de calculo de la instancia actual</param>
    /// <param name="oCalculoCostoParticular">Instancia particular</param>
    /// <returns></returns>
    public string fn_ObtenerTotalHtml(decimal dCantidad, 
                                     ref decimal dResultado,
                                     ref decimal dTotalNoImpuestos,
                                     BaseCalculo oBaseCalculo,
                                     ICalculoTarifaCosto oCalculoCostoParticular)
    {
        //Si no se obtuvo una cantidad valida se retorna en 0
        if (dCantidad == -1 || dCantidad == 0)
            return "(Ingresar cantidad para calcular)";
        //Variable para almacenar el resultado
        dResultado = dCantidad;
        //Se comprueba si tiene objeto base calculo
        if (oBaseCalculo != null)
        {
            if (oBaseCalculo.bMultiplicar)
                //Se multiplica la cantidad por el fijo
                dResultado *= oCalculoCostoParticular.fn_ObtenerPrecioPorUnidad();
            else
                dResultado = oCalculoCostoParticular.fn_ObtenerPrecioPorUnidad();
        }
        else
        {
            //Se multiplica la cantidad por el fijo
            dResultado *= oCalculoCostoParticular.fn_ObtenerPrecioPorUnidad();
        }
        //Variable para saber si entro al cobro minimo
        bool bCobroMinimo = false;
        //Se verifica si se llega al cobro minimo
        if (dResultado < oCalculoCostoParticular.fn_ObtenerCobroMinimo())
        {
            dResultado = oCalculoCostoParticular.fn_ObtenerCobroMinimo();
            bCobroMinimo = true;
        }
        //decimal para almacenar resultado base
        dTotalNoImpuestos = dResultado;
        //Si el iva no es 0 se le agrega
        if (oCalculoCostoParticular.fn_ObtenerIVA() != 0)
        {
            dResultado += oCalculoCostoParticular.fn_ObtenerIVAValor();
        }
        //Si la retención no es 0 se le agrega
        if (oCalculoCostoParticular.fn_ObtenerRetencion() != 0)
        {
            dResultado += oCalculoCostoParticular.fn_ObtenerRetencionValor();
        }
        //Se retorna el resultado
        return "$" + dResultado + " " + oCalculoCostoParticular.fn_ObtenerMoneda() + " " + (bCobroMinimo ? "(Cobro mínimo)" : "");
    }

    /// <summary>
    /// Método para obtener el valor del iva de una tarifa-referencia
    /// </summary>
    /// <param name="dCantidad">Valor de la cantidad en la instancia particular</param>
    /// <param name="dTotalNoImpuestos">Referencia a variable para almacenar el total sin impuestos</param>
    /// <param name="oBaseCalculo">Instancia de base de calculo de la instancia particular</param>
    /// <param name="oCalculoCostoParticular">Instancia particular</param>
    /// <returns></returns>
    public decimal fn_ObtenerIVAValor(decimal dCantidad, 
                                      ref decimal dTotalNoImpuestos,
                                      BaseCalculo oBaseCalculo,
                                      ICalculoTarifaCosto oCalculoCostoParticular)
    {
        //Si no se obtuvo una cantidad valida se retorna en 0
        if (dCantidad == -1)
            return 0;
        //Variable para almacenar el resultado
        decimal dResultado = dCantidad;
        //Se comprueba si tiene objeto base calculo
        if (oBaseCalculo != null)
        {
            if (oBaseCalculo.bMultiplicar)
                //Se multiplica la cantidad por el fijo
                dResultado *= oCalculoCostoParticular.fn_ObtenerPrecioPorUnidad();
            else
                dResultado = oCalculoCostoParticular.fn_ObtenerPrecioPorUnidad();
        }
        else
        {
            //Se multiplica la cantidad por el fijo
            dResultado *= oCalculoCostoParticular.fn_ObtenerPrecioPorUnidad();
        }
        //Se verifica si se llega al cobro minimo
        if (dResultado < oCalculoCostoParticular.fn_ObtenerCobroMinimo())
            dResultado = oCalculoCostoParticular.fn_ObtenerCobroMinimo();
        //decimal para almacenar resultado base
        dTotalNoImpuestos = dResultado;
        return dTotalNoImpuestos * oCalculoCostoParticular.fn_ObtenerIVA() / 100;
    }

    /// <summary>
    /// Método para obtener el valor de la retención de una tarifa-referencia
    /// </summary>
    /// <param name="dCantidad">Valor de la cantidad en la instancia particular</param>
    /// <param name="dTotalNoImpuestos">Referencia a variable para almacenar el total sin impuestos</param>
    /// <param name="oBaseCalculo">Instancia de base de calculo de la instancia particular</param>
    /// <param name="oCalculoCostoParticular">Instancia particular</param>
    /// <returns></returns>
    public decimal fn_ObtenerRetencionValor(decimal dCantidad, 
                                            ref decimal dTotalNoImpuestos,
                                            BaseCalculo oBaseCalculo,
                                            ICalculoTarifaCosto oCalculoCostoParticular)
    {
        //Si no se obtuvo una cantidad valida se retorna en 0
        if (dCantidad == -1)
            return 0;
        //Variable para almacenar el resultado
        decimal dResultado = dCantidad;
        //Se comprueba si tiene objeto base calculo
        if (oBaseCalculo != null)
        {
            if (oBaseCalculo.bMultiplicar)
                //Se multiplica la cantidad por el fijo
                dResultado *= oCalculoCostoParticular.fn_ObtenerPrecioPorUnidad();
            else
                dResultado = oCalculoCostoParticular.fn_ObtenerPrecioPorUnidad();
        }
        else
        {
            //Se multiplica la cantidad por el fijo
            dResultado *= oCalculoCostoParticular.fn_ObtenerPrecioPorUnidad();
        }
        //Se verifica si se llega al cobro minimo
        if (dResultado < oCalculoCostoParticular.fn_ObtenerCobroMinimo())
            dResultado = oCalculoCostoParticular.fn_ObtenerCobroMinimo();
        //decimal para almacenar resultado base
        dTotalNoImpuestos = dResultado;
        //Retorna el valor
        return dTotalNoImpuestos * oCalculoCostoParticular.fn_ObtenerRetencion() / 100;
    }

    /// <summary>
    /// Método para obtener el subtotal de una tarifa-referencia
    /// </summary>
    /// <param name="dCantidad">Valor de la cantidad en la instancia particular</param>
    /// <param name="oBaseCalculo">Instancia de base de calculo de la instancia particular</param>
    /// <param name="oCalculoCostoParticular">Instancia particular</param>
    /// <returns></returns>
    public decimal fn_ObtenerSubTotal(decimal dCantidad,
                                      BaseCalculo oBaseCalculo,
                                      ICalculoTarifaCosto oCalculoCostoParticular)
    {
        //Si no se obtuvo una cantidad valida se retorna en 0
        if (dCantidad == -1)
            return 0;
        //Variable para almacenar el resultado
        decimal dResultado = dCantidad;
        //Se comprueba si tiene objeto base calculo
        if (oBaseCalculo != null)
        {
            if (oBaseCalculo.bMultiplicar)
                //Se multiplica la cantidad por el fijo
                dResultado *= oCalculoCostoParticular.fn_ObtenerPrecioPorUnidad();
            else
                dResultado = oCalculoCostoParticular.fn_ObtenerPrecioPorUnidad();
        }
        else
        {
            //Se multiplica la cantidad por el fijo
            dResultado *= oCalculoCostoParticular.fn_ObtenerPrecioPorUnidad();
        }
        //Se verifica si se llega al cobro minimo
        if (dResultado < oCalculoCostoParticular.fn_ObtenerCobroMinimo())
            dResultado = oCalculoCostoParticular.fn_ObtenerCobroMinimo();
        //Se retorna el resultado
        return dResultado;
    }
    #endregion
}