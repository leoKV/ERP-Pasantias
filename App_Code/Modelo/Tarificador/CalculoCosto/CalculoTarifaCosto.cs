using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Interfaz para implementar calculos ya sea por rango o por 
/// valores unicos dependiendo del caso especifico
/// </summary>
public interface ICalculoTarifaCosto
{
    // Metodos que deben ser implementados por las clases concretas

    #region Metodos

    decimal fn_ObtenerPrecioPorUnidad();
    decimal fn_ObtenerCantidad();
    decimal fn_ObtenerIVAValor();
    decimal fn_ObtenerRetencionValor();
    decimal fn_ObtenerSubTotal();
    decimal fn_ObtenerTotal();
    decimal fn_ObtenerIVA();
    decimal fn_ObtenerRetencion();
    decimal fn_ObtenerCobroMinimo();
    int fn_ObtenerTipoCobro();
    string fn_ObtenerCantidadHtml();
    string fn_ObtenerMoneda();
    string fn_ObtenerTotalHtml();
    void fn_Inicializar(int iIdTarifaReferencia, int iIdTipoTarifa, int iIdSubReferencia);
    void fn_AsignarCantidad(decimal dCantidad);
    
    #endregion

    
}