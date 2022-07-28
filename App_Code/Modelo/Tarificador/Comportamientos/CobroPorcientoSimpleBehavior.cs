using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CobroPorcientoSimpleBehavior
/// </summary>
public class CobroPorcientoSimpleBehavior : ICobroBehavior
{
	public CobroPorcientoSimpleBehavior()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}


    public void fn_ObtenerSubTotal(Tarifa objTarifa)
    {
        // Variable que será devuelta
        decimal dResultado = 0;
        // Instancia conexion
        Conexion objConexion = new Conexion();
        // Query para obtener el porcentaje y cobro minimo
        string sQuery = "SELECT cobroMinimo, valor " +
                        "FROM tReglaTarifa " +
                        "WHERE idConfTarifa = " + objTarifa.iIdConfTarifa;
        // Se ejecuta la query
        List<string> lstResultado = objConexion.ejecutarConsultaRegistroMultiples(sQuery);
        // Comprobar que se tiene la información para tarificar
        objTarifa.fn_ComprobarInformacionTarificar(objTarifa);
        // Verifica que se halla recuperado la información correctamente
        if (lstResultado[0] == "1")
        {
            // Sumar los valores que se obtuvieron
            dResultado = objTarifa.dValorAduana +
                         objTarifa.dImpuestosPagados +
                         objTarifa.dImpuestosAfianzados +
                         objTarifa.dImpuestosSubsidiados +
                         objTarifa.dGastosComprobados +
                         objTarifa.dImpuestosFinanciadosPorNAD;
            if (objTarifa.fn_RestarImpuestos(objTarifa))
            {
                dResultado = dResultado - objTarifa.dAnticipos;
            }
            // Sacar el porcentaje correspondiente
            dResultado = dResultado * (decimal.Parse(lstResultado[2]) / 100);
            // Si el cobro minimo es mayor el reusltado se iguala a este
            dResultado = objTarifa.dCantidad * (decimal.Parse(lstResultado[1]) > dResultado ? decimal.Parse(lstResultado[1]) : dResultado);
            // Se asigna resultado
            objTarifa.dSubTotal = dResultado;
        }
        else // Error
        {
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Error al obtener el monto";
            objTarifa.dSubTotal = 0;
        }
    }
}