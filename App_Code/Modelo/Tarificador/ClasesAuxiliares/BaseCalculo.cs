using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Clase usada para obtener la base de calculo para una tarifa
/// </summary>
public class BaseCalculo
{
    #region Propiedades de la clase
    public decimal dValorAduana { get; set; }
    public decimal dImpuestosPagados { get; set; }
    public decimal dImpuestosAfianzados { get; set; }
    public decimal dGastosComprobados { get; set; }
    public decimal dPartidaPresupuestal { get; set; }
    public decimal dGastosDirectos { get; set; }
    public decimal dImpuestosSubsidiados { get; set; }
    public bool bMultiplicar { get; set; }
    #endregion

    #region Metodos
    /// <summary>
    /// Metodo para obtener el valor base de calculo de la tarifa
    /// </summary>
    /// <param name="sIdSubReferencia">Id del registro tTarifaReferencia de la cual se quiera obtener el valor</param>
    /// <param name="dPorcentaje">Porcentaje a aplicar</param>
    /// <param name="iIdTarifaReferencia">Id de la subreferencia a la cual se le esta aplicando el calculo</param>
    /// <returns></returns>
    public decimal fn_ObtenerValorBaseCalculo(int iIdTarifaReferencia, string sIdSubReferencia, decimal dPorcentaje)
    {
        //Query para obtener las bases de calculo de una referencia
        string sQuery = "SELECT tft.formula " +
                          "FROM tTarifaReferencia ttr " +
                          "INNER JOIN " +
                          "tReglaTarifa trt " +
                          "ON ttr.idConfTarifa = trt.idConfTarifa " +
                          "INNER JOIN " +
                          "tFormulaTarifa tft " +
                          "ON trt.idFormula = tft.idFormula " +
                          "WHERE ttr.idTarifaReferencia = " + iIdTarifaReferencia;
        //Se instancia objetod decionex
        Conexion oConexion = new Conexion();
        // se recupera un arreglo
        string[] arrBases = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1].Split(',');
        //Código para el caso de que sea por peso y no tenga 
        // Variable que almacenara el resultado
        decimal dResultado = 0;
        //Recorremos el arreglo de formulas
        foreach (string item in arrBases)
        {
            switch (item.Trim())
            {
                case "1":
                    fn_ObtenerValorAduana(sIdSubReferencia);
                    dResultado += dValorAduana * dPorcentaje / 100;
                    break;
                case "2":
                    fn_ObtenerImpuestosPagados(sIdSubReferencia);
                    dResultado += dImpuestosPagados * dPorcentaje / 100;
                    break;
                case "3":
                    fn_ObntenerImpuestosAfianzados(sIdSubReferencia);
                    dResultado += dImpuestosAfianzados * dPorcentaje / 100;
                    break;
                case "4":
                    fn_ObtenerImpuestosSubsidiados(sIdSubReferencia);
                    dResultado += dImpuestosSubsidiados * dPorcentaje / 100;
                    break;
                case "5":
                    fn_ObtenerGastosComprobados(sIdSubReferencia);
                    dResultado += dGastosComprobados * dPorcentaje / 100;
                    break;
                case "6":
                    fn_ObtenerPartidaPresupuestal(sIdSubReferencia);
                    dResultado += dPartidaPresupuestal * dPorcentaje / 100;
                    break;
                case "7":
                    fn_ObtenerGatosDirectos(sIdSubReferencia);
                    dResultado += dGastosDirectos * dPorcentaje / 100;
                    break;
                default:
                    bMultiplicar = true;
                    break;
            }
        }
        //Se retorna el resultado
        return dResultado;
    }

    /// <summary>
    /// Método para obtener el valor de aduana a una subreferencia
    /// </summary>
    /// <param name="sIdSubReferencia">ID de la subreferencia</param>
    private void fn_ObtenerValorAduana(string sIdSubReferencia)
    {
        //Consulta para obtener la informacion base
        string sQuery = "SELECT ISNULL(valorAduana, 0) FROM tPedimento WHERE idSubReferencia = " + sIdSubReferencia;
        //Se instancia objeto conexion
        Conexion oConexion = new Conexion();
        //se recupera el valor de la base de datos
        string sValorAduana = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        dValorAduana = decimal.Parse(sValorAduana);
    }

    /// <summary>
    /// Método para obtener el valor de impuestos pagados de una subreferencia
    /// </summary>
    /// <param name="sIdSubReferencia">ID de la subreferencia</param>
    private void fn_ObtenerImpuestosPagados(string sIdSubReferencia)
    {
        //Consulta para obtener la informacion base
        string sQuery = "SELECT ISNULL(impPagados, 0) FROM tPedimento WHERE idSubReferencia = " + sIdSubReferencia;
        //Se instancia objeto conexion
        Conexion oConexion = new Conexion();
        //se recupera el valor de la base de datos
        string sImpuestosPagados = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        dImpuestosPagados = decimal.Parse(sImpuestosPagados);
    }

    /// <summary>
    /// Método para obtener el valor de impuestos afianzados de una subreferencia
    /// </summary>
    /// <param name="sIdSubReferencia">ID de la subreferencia</param>
    private void fn_ObntenerImpuestosAfianzados(string sIdSubReferencia)
    {
        //Consulta para obtener la informacion base
        string sQuery = "SELECT ISNULL(impAfianzados, 9) FROM tPedimento WHERE idSubReferencia = " + sIdSubReferencia;
        //Se instancia objeto conexion
        Conexion oConexion = new Conexion();
        //se recupera el valor de la base de datos
        string sImpuestosAfianzados = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        dImpuestosAfianzados = decimal.Parse(sImpuestosAfianzados);
    }

    /// <summary>
    /// Método para obtener el valor de impuestos subsidiados de una subreferencia
    /// </summary>
    /// <param name="sIdSubReferencia">ID de la subreferencia</param>
    private void fn_ObtenerImpuestosSubsidiados(string sIdSubReferencia)
    {
        //Consulta para obtener la informacion base
        string sQuery = "SELECT ISNULL(impuestosSubsidiados, 0) FROM tPedimento WHERE idSubReferencia = " + sIdSubReferencia;
        //Se instancia objeto conexion
        Conexion oConexion = new Conexion();
        //se recupera el valor de la base de datos
        string sImpuestosSubsidiados = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        dImpuestosSubsidiados = decimal.Parse(sImpuestosSubsidiados);
    }

    /// <summary>
    /// Método para obtener el valor de impuestos comprobados de una subreferencia
    /// </summary>
    /// <param name="sIdSubReferencia">ID de la subreferencia</param>
    private void fn_ObtenerGastosComprobados(string sIdSubReferencia)
    {
        //Consulta para obtener la informacion base
        string sQuery = "SELECT ISNULL(SUM(tsf.total),0) " +
                        "FROM tServicioFactura tsf " +
			            "        INNER JOIN " +
		                "     tFactura tf " +
                        "        ON tsf.idFactura = tf.idFactura " +
		                "    WHERE idSubReferencia = " + sIdSubReferencia;
        //Se instancia objeto conexion
        Conexion oConexion = new Conexion();
        //se recupera el valor de la base de datos
        string sGastosComprobados= oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
    }

    /// <summary>
    /// Método para obtener el valor de la partida presupuestal de una subreferencia
    /// </summary>
    /// <param name="sIdSubReferencia">ID de la subreferencia</param>
    private void fn_ObtenerPartidaPresupuestal(string sIdSubReferencia)
    {
        //Consulta para obtener la informacion base
        string sQuery = "SELECT ISNULL(partidaPresupuestal, 0) FROM tPedimento WHERE idSubReferencia = " + sIdSubReferencia;
        //Se instancia objeto conexion
        Conexion oConexion = new Conexion();
        //se recupera el valor de la base de datos
        string sPartidaPresupuestal = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
    }

    /// <summary>
    /// Método para obtener los gastos directos de una subreferencia
    /// </summary>
    /// <param name="sIdSubReferencia">ID de la subreferencia</param>
    private void fn_ObtenerGatosDirectos(string sIdSubReferencia)
    {
        //Consulta para obtener la informacion base
        string sQuery = "SELECT ISNULL(partidaPresupuestal, 0) FROM tPedimento WHERE idSubReferencia = " + sIdSubReferencia;
        //Se instancia objeto conexion
        Conexion oConexion = new Conexion();
        //se recupera el valor de la base de datos
        string sGastosDirectos = oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        dGastosDirectos = decimal.Parse(sGastosDirectos);
    }
    #endregion
}