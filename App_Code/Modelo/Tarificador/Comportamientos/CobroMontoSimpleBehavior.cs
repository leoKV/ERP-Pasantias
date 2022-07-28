using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CobroMontoSimpleBehavior
/// </summary>
public class CobroMontoSimpleBehavior : ICobroBehavior
{
	public CobroMontoSimpleBehavior()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    /// <summary>
    /// Obtener el sub total
    /// </summary>
    /// <param name="objTarifa"></param>
    public void fn_ObtenerSubTotal(Tarifa objTarifa)
    {
        //Variable que será devuelta
        decimal dResultado = 0;
        // Instancia conexion 
        Conexion objConexion = new Conexion();
        // Query para obtener el monto 
        string sQuery = "SELECT trt.valor " +
                        "FROM  " +
	                    "    tReglaTarifa trt " +
                        "WHERE trt.idConfTarifa = " + objTarifa.iIdConfTarifa;
        // Se ejecuta la query
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Verifica que se halla recuperado la información correctamente
        if (arrResultado[0] == "1")
        {
            // Multiplicar la cantidad por el monto
            dResultado = objTarifa.dCantidad * decimal.Parse(arrResultado[1]);
        }
        else // Error
        {
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Error al obtener el monto";
        }
        objTarifa.dSubTotal = dResultado;
    }
}