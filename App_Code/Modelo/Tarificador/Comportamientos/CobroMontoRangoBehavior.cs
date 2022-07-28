using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Descripción breve de CobroMontoRangoBehavior
/// </summary>
public class CobroMontoRangoBehavior : ICobroBehavior
{
    public CobroMontoRangoBehavior()
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
        string sQuery = "SELECT trat.valorMinimo, trat.valorMaximo, trat.valor " +
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
        // Declarar variable para cuando este fuera de rango
        int iEncontrado = -1;
        foreach (DataRow objRow in dtbRangos.Rows)
        {
            //Se compara si la cantidad es mayor o igual que el rango menor
            if (dResultado >= decimal.Parse(objRow[0].ToString()))
            {
                //Se compara si la cantidad es menor o igual que el rango mayor
                if (dResultado <= decimal.Parse(objRow[1].ToString()))
                {
                    // Se asigna el row como seleccionado y se rompe el ciclo
                    objRowRango = objRow;
                    // Se asigna la variable de que ya se encontró 
                    iEncontrado = 1;
                    break;
                }
            }
            else if (dResultado < decimal.Parse(objRow[0].ToString()))
            {
                // Se asigna el fuera de rango
                iEncontrado = 0;
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
                // Se calcula el subtotal
                objTarifa.dSubTotal = decimal.Parse(objRowRango[2].ToString());
            }
            else if (iEncontrado == 0)
            { // Si el rango es menor
                // Se asigna el cobro minimo por el subtotal 
                objTarifa.dSubTotal = decimal.Parse(arrResultado[1].ToString());
            }
            else
            { // Si el rango es mayor
                // Multiplica por el maximo rango configurado
                objRowRango = dtbRangos.Rows[dtbRangos.Rows.Count - 1];
                objTarifa.dSubTotal = decimal.Parse(objRowRango[2].ToString());
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