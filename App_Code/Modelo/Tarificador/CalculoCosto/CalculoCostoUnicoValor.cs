using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Clase usada para calcular el precio por unidad cuando es un costo unico
/// sin rangos
/// </summary>
public class CalculoCostoUnicoValor : ICalculoTarifaCosto
{
    #region Propiedades

    public BaseCalculo oBaseCalculo { get; set; }
    private CalculoCostoGeneral oCalculoGeneral;
    public int iIdSubReferencia { get; set; }
    public int iIdConfTarifa { get; set; }
    public int iIdTarifaReferencia { get; set; }
    public int iIdTipoTarifa { get; set; }
    public decimal dCantidad { get; set; }
    public decimal dIva { get; set; }
    public decimal dRetencion { get; set; }
    public decimal dPrecio { get; set; }
    public decimal dCobroMinimo { get; set; }
    public decimal dTotalNoImpuestos { get; set; }
    public decimal dResultado { get; set; }
    public string sMoneda { get; set; }
    
    
    #endregion

    #region Constructor
    /// <summary>
    /// Constructor de la clase
    /// </summary>
    public CalculoCostoUnicoValor()
    {
        // Se instancian las variables en negativo para indicar que
        // no se han consultado
        dCantidad = -2;
        dIva = -1;
        dRetencion = -1;
        dPrecio = -1;
        dCobroMinimo = -1;
        oCalculoGeneral = new CalculoCostoGeneral();
    }
    #endregion

    #region Metodos

    public decimal fn_ObtenerPrecioPorUnidad()
    {
        //Query para seleccionar el monto a aplicar
        string sQuery = "SELECT trt.valor " +
                        "FROM tReglaTarifa trt " +
		                "     INNER JOIN " +
		                "     tTarifaReferencia ttr " +
                        "     ON ttr.idConfTarifa = trt.idConfTarifa " +
	                    "WHERE ttr.idTarifaReferencia = " + iIdTarifaReferencia;
        //Se instancia conexion con la base de datos
        Conexion oConexion = new Conexion();
        //Se obtiene el resultado de la consulta
        string sPrecio = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        //Se retorna el resultado
        return decimal.Parse(sPrecio);
    }

    /// <summary>
    /// Metodo para inicializar las variables y poder comenzar a usar otros metodos
    /// </summary>
    /// <param name="iIdTarifaReferencia">ID de la tarifa-referencia</param>
    /// <param name="iIdTipoTarifa">ID del tipo de tarifa</param>
    /// <param name="iIdSubReferencia">ID de la subreferencia</param>
    public void fn_Inicializar(int iIdTarifaReferencia, int iIdTipoTarifa, int iIdSubReferencia)
    {
        this.iIdTarifaReferencia = iIdTarifaReferencia;
        this.iIdTipoTarifa = iIdTipoTarifa;
        this.iIdSubReferencia = iIdSubReferencia;
        fn_ObtenerCantidad();
    }

    /// <summary>
    /// Método para obtener la cantidad en base al calculo general
    /// </summary>
    /// <returns>Cantidad a tarificar</returns>
    public decimal fn_ObtenerCantidad()
    {
        //Obtiene la cantidad
        dCantidad = oCalculoGeneral.fn_ObtenerCantidad(dCantidad, iIdTipoTarifa, iIdSubReferencia, iIdTarifaReferencia);
        return dCantidad;
    }

    /// <summary>
    /// Método para obtener la cantidad en formato de HTML dentro de un input
    /// </summary>
    /// <returns>Retorna el código HTML generado a partir del calculo general</returns>
    public string fn_ObtenerCantidadHtml()
    {
        return oCalculoGeneral.fn_ObtenerCantidadHtml(iIdTarifaReferencia, dCantidad);
    }

    /// <summary>
    /// Método para obtener el iva a aplicar
    /// </summary>
    /// <returns>El valor del iva obtenido a partir del calculo general</returns>
    public decimal fn_ObtenerIVA()
    {
        dIva = oCalculoGeneral.fn_ObtenerIVA(dIva, iIdTarifaReferencia);
        return dIva;
    }

    /// <summary>
    /// Método par aobtener la retención a aplicar
    /// </summary>
    /// <returns>El valor de la retención obtenido a partir del calculo general</returns>
    public decimal fn_ObtenerRetencion()
    {
        dRetencion = oCalculoGeneral.fn_ObtenerRetencion(dRetencion, iIdTarifaReferencia);
        return dRetencion;
    }

    /// <summary>
    /// Método para obtener el cobro minimo a aplicar
    /// </summary>
    /// <returns>Cobro minimo obtenido a partir del calculo general</returns>
    public decimal fn_ObtenerCobroMinimo()
    {
        dCobroMinimo = oCalculoGeneral.fn_ObtenerCobroMinimo(dCobroMinimo, iIdTarifaReferencia);
        return dCobroMinimo;
    }


    /// <summary>
    /// Método para obtener la moneda
    /// </summary>
    /// <returns>Moneda obtenida a partir del calculo general</returns>
    public string fn_ObtenerMoneda()
    {
        //Se recupera el valor 
        sMoneda = oCalculoGeneral.fn_ObtenerMoneda(sMoneda, iIdTarifaReferencia);
        //Se retorna el valor
        return sMoneda;
    }


    /// <summary>
    /// Método para obtener el total formateado en HTML
    /// </summary>
    /// <returns>Total en HTML obtenido a partir del calculo general</returns>
    public string fn_ObtenerTotalHtml()
    {
        decimal dResultadoTmp = dResultado;
        decimal dTotalNoImpuestosTmp = dTotalNoImpuestos;
        string sResultado = oCalculoGeneral.fn_ObtenerTotalHtml(dCantidad,
                                                      ref dResultadoTmp,
                                                      ref dTotalNoImpuestosTmp,
                                                      oBaseCalculo,
                                                      this);
        dResultado = dResultadoTmp;
        dTotalNoImpuestos = dTotalNoImpuestosTmp;
        return sResultado;
    }

    /// <summary>
    /// Método para obtener el tipo de cobro
    /// </summary>
    /// <returns>Valor de tipo de cobro obtenido a partir del calculo general</returns>
    public int fn_ObtenerTipoCobro()
    {
        return oCalculoGeneral.fn_ObtenerTipoCobro(iIdTarifaReferencia);
    }

    /// <summary>
    /// Método para obtener el valor del iva aplicado al subtotal
    /// </summary>
    /// <returns>Valor del iva obtenido a partir del calculo general</returns>
    public decimal fn_ObtenerIVAValor()
    {
        decimal dTotalNoImpuestosTmp = dTotalNoImpuestos;
        decimal dResultado = oCalculoGeneral.fn_ObtenerIVAValor(dCantidad,
                                                                ref dTotalNoImpuestosTmp,
                                                                oBaseCalculo,
                                                                this);
        dTotalNoImpuestos = dTotalNoImpuestosTmp;
        return dResultado;
    }

    /// <summary>
    /// Método para obtener la retención aplicada al subtotal
    /// </summary>
    /// <returns>Valor de la retención obtenido a partir del calculo general</returns>
    public decimal fn_ObtenerRetencionValor()
    {
        decimal dTotalNoImpuestosTmp = dTotalNoImpuestos;
        decimal dResultado = oCalculoGeneral.fn_ObtenerRetencionValor(dCantidad,
                                                                      ref dTotalNoImpuestosTmp,
                                                                      oBaseCalculo,
                                                                      this);
        dTotalNoImpuestos = dTotalNoImpuestosTmp;
        //Retorna el valor
        return dResultado;
    }


    /// <summary>
    /// Método para obtener el subtotal
    /// </summary>
    /// <returns>Valor del subtotal obtenido a apartir del calculo general</returns>
    public decimal fn_ObtenerSubTotal()
    {
        return oCalculoGeneral.fn_ObtenerSubTotal(dCantidad, oBaseCalculo, this);
    }


    /// <summary>
    /// Método para asignar la cantidad en caso de estarse re-calculando
    /// </summary>
    /// <param name="dCantidad"></param>
    public void fn_AsignarCantidad(decimal dCantidad)
    {
        this.dCantidad = dCantidad;
    }

    /// <summary>
    /// Metodo para obtener el total 
    /// </summary>
    /// <returns>Valor del total</returns>
    public decimal fn_ObtenerTotal()
    {
        return dResultado;
    }

    #endregion
}