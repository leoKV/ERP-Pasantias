using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Descripción breve de CobroMontoPorcientoBehavior
/// </summary>
public class CobroMontoPorcientoBehavior : ICobroBehavior
{
	public CobroMontoPorcientoBehavior()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public void fn_ObtenerSubTotal(Tarifa objTarifa)
    {
        // Variable que será devuelta
        decimal dResultado = 0;
        // Comprobar que se tiene la información para tarificar
        objTarifa.fn_ComprobarInformacionTarificar(objTarifa);
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
        // Obtener rangos
        string sQuery = "SELECT trat.valorMinimo, trat.valorMaximo, trat.valor, trat.iva " +
                        "FROM tRangoTarifa trat  " +
                        "     JOIN " +
                        "     tReglaTarifa tret " +
                        "     ON trat.idReglaTarifa = tret.idReglaTarifa " +
                        "WHERE tret.idConfTarifa = " + objTarifa.iIdConfTarifa;
        // Instancias conexion
        Conexion objConexion = new Conexion();
        // Ejecutar la query
        DataTable dtbRangos = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
        // Declarar row para almacenar el rango 
        DataRow objRowRango = dtbRangos.NewRow();
        // Verificar en que rango cae
        int iEncontrado = -1;
        foreach (DataRow objRow in dtbRangos.Rows)
        {
            //Se compara si el resultado es mayor o igual que el rango menor
            if (dResultado >= decimal.Parse(objRow[0].ToString()))
            {
                //Se compara si el resultado es menor o igual que el rango mayor
                if (dResultado <= decimal.Parse(objRow[1].ToString()))
                {
                    // Se asigna el row como seleccionado y se rompe el ciclo
                    objRowRango = objRow;
                    // Se asigna la variable de que ya se encontró 
                    iEncontrado = 1;
                    break;
                }
                else if (dResultado < decimal.Parse(objRow[0].ToString()))
                {
                    // Se asogma eñ fuera de rango
                    iEncontrado = 0;
                }
            }
        }
        // Query para obtener el cobro minimo
        sQuery = "SELECT cobroMinimo " +
                 "FROM tReglaTarifa " +
                 "WHERE idConfTarifa = " + objTarifa.iIdConfTarifa;
        // Consultar
        string[] arrResultado = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        // Verifica resultado de la query
        if (arrResultado[0] == "1")
        {
            // Si se encontró el rango
            if (iEncontrado == 1)
            {
                // Se verifica que tipo de cobro se va a realizar
                if (objRowRango[3].ToString().Trim() == "0") // Monto
                {
                    objTarifa.dSubTotal = decimal.Parse(objRowRango[2].ToString()) * objTarifa.dCantidad;
                }
                else // Porcentaje
                {
                    // Se calcula el subtotal
                    objTarifa.dSubTotal = dResultado * objTarifa.dCantidad * decimal.Parse(objRowRango[3].ToString()) / 100;
                }
            }
            else if (iEncontrado == 0)
            {
                // Se asigna el cobro minimo por el subtotal 
                objTarifa.dSubTotal = decimal.Parse(arrResultado[1].ToString()) * objTarifa.dCantidad;
            }
            else
            {
                // Multiplica por el maximo rango configurado
                objRowRango = dtbRangos.Rows[dtbRangos.Rows.Count - 1];
                // Se verifica que tipo de cobro se va a realizar
                if (objRowRango[3].ToString().Trim() == "0") // Monto
                {
                    objTarifa.dSubTotal = decimal.Parse(objRowRango[2].ToString()) * objTarifa.dCantidad;
                }
                else // Porcentaje
                {
                    // Se calcula el subtotal
                    objTarifa.dSubTotal = dResultado * objTarifa.dCantidad * decimal.Parse(objRowRango[3].ToString()) / 100;
                }
            }

            // Se compara el subtotal con el cobro minimo
            if (objTarifa.dSubTotal < decimal.Parse(arrResultado[1].ToString()))
            {
                // Se asigna el cobro por el subtotal 
                objTarifa.dSubTotal = decimal.Parse(arrResultado[1].ToString());
            }
        }
        else
        {
            objTarifa.iResultado = 0;
            objTarifa.sMensaje = "Ocurrió un error al realizar el calculo de la tarifa";
        }
    }
}