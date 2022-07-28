using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Interface usada para poder encapsular los algoritmos de cobro
/// de forma independiente del cliente (Clase Tarifa)
/// </summary>
public interface ICobroBehavior
{
    /// <summary>
    /// Metodo usado para obtener el subtotal basandose en la cantidad y
    /// la configuración realizada en la base de datos
    /// </summary>
    /// <param name="iCantidad">Cantidad para realizar el calculo</param>
    /// <param name="iIdSubReferencia">Subreferencia a la cual esta aplicando</param>
    /// <returns></returns>
    void fn_ObtenerSubTotal(Tarifa objTarifa);
}