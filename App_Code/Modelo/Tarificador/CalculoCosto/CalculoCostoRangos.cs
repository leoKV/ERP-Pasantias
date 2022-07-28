using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Clase usada para calcular el precio por unidad cuando se
/// debe hacer por medio de rangos
/// </summary>
public class CalculoCostoRangos : ICalculoTarifaCosto
{
    #region Propiedades

    private CalculoCostoGeneral oCalculoGeneral;
    public DataTable dtbRangos;
    public BaseCalculo oBaseCalculo { get; set; }
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
    public bool bMultiplicar { get; set; }
    
    #endregion

    #region Constructor
    /// <summary>
    /// Constructor de la clase
    /// </summary>
    public CalculoCostoRangos()
	{
        // Se inicializan variables en negativo para indicar que aun
        // no se obtienen
        dCantidad = -2;
        dIva = -1;
        dRetencion = -1;
        dPrecio = -1;
        dCobroMinimo = -1;
        oCalculoGeneral = new CalculoCostoGeneral();
	}
    #endregion

    #region Metodos

    /// <summary>
    /// Metodo para obtener el precio por unidad,
    /// solo se debe de ejecutar luego de inicializar con el metodo
    /// fn_Inicializar
    /// </summary>
    /// <returns></returns>
    public decimal fn_ObtenerPrecioPorUnidad()
    {
        //Si ya se obtuvo el precio se retorna
        if (dPrecio != -1)
            return dPrecio;
        dPrecio = 0;
        // Por cada uno de los rangos que hay, se busca en cual entra la cantidad
        foreach (DataRow oRow in dtbRangos.Rows)
            if (dCantidad >= decimal.Parse(oRow[0].ToString()) && dCantidad <= decimal.Parse(oRow[1].ToString()))
            {
                // Si se calcula en base a un monto
                if (int.Parse(oRow[3].ToString()) == 1)
                {
                    dPrecio = decimal.Parse(oRow[2].ToString());
                }
                else // Si se calcula a base de un porcentaje
                {
                    //Instancia de la clase Base Calculo
                    oBaseCalculo = new BaseCalculo();
                    //Se obtiene el precio
                    dPrecio = oBaseCalculo.fn_ObtenerValorBaseCalculo(iIdTarifaReferencia, iIdSubReferencia.ToString(), decimal.Parse(oRow[2].ToString()));
                    //Se trae el booleano para saber si sse debe de multiplicar o no
                    bMultiplicar = oBaseCalculo.bMultiplicar;
                }
                break;
            }
        return dPrecio;
    }

    /// <summary>
    /// Metodo para obtener los rangos 
    /// </summary>
    private void fn_ObtenerRangos()
    {
        //Query para obtener los rangos 
        string sQuery = "SELECT valorMinimo, valorMaximo, " +
	                    "    CASE WHEN trat.idTipoRangoTarifa = 1 THEN trat.valor ELSE trat.iva END,  " +
	                    "    trat.idTipoRangoTarifa  " +
                        "FROM tRangoTarifa trat  " +
                        "INNER JOIN tReglaTarifa tret  " +
                        "ON trat.idReglaTarifa = tret.idReglaTarifa " +
                        "WHERE tret.idConfTarifa = (SELECT idConfTarifa  " +
						"                          FROM " +           
						"                          tTarifaReferencia " +
						"                          WHERE idTarifaReferencia = " + iIdTarifaReferencia + " " +
                        "                          AND idEstatus = 1)  " +
						"                          ORDER BY valorMinimo ASC";
        //Se instancia objeto conexion
        Conexion oConexion = new Conexion();
        //Se obtiene y guardan los rangos en dtbRangos
        dtbRangos = oConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
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
        fn_ObtenerRangos();
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