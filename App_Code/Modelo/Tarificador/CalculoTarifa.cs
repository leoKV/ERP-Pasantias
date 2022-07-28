using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Clase utilizada para realizar los calculos de una 
/// tarifa de una referencia
/// </summary>
public class CalculoTarifa
{
    #region Propiedades
    private List<TarifaReferencia> lstIdentificadoresTarifa;
    private List<ICalculoTarifaCosto> lstObjetosCalculo;
    public int iIdSubReferencia { get; set; }
    public int iIdConfTarifa { get; set; }
    public decimal dUnidades { get; set; }
    public decimal dMontoMinimo { get; set; }
    public decimal dIva { get; set; }
    public decimal dRetencion { get; set; }

    //En esta variable se almacena el resultado de la aplicacion de las tarifas
    public string sHTML { get; set; } 

    #endregion

    #region Constructor
    /// <summary>
    /// Se debe incluir el id de configuracion tarifa
    /// </summary>
    /// <param name="iIdConfTarifa">Id de la subreferencia</param>
    public CalculoTarifa(int iIdSubReferencia, List<decimal> lstCantidades)
    {
        this.iIdSubReferencia = iIdSubReferencia;
        lstObjetosCalculo = new List<ICalculoTarifaCosto>();
        fn_ObtenerTarifas(lstCantidades);
    }
    #endregion

    #region Metodos
    
    /// <summary>
    /// Metodo para obtener las tarifas de una subreferencia
    /// </summary>
    /// <param name="lstCantidades">Lista de cantidad (Nulo para obtenerlas de BD)</param>
    private void fn_ObtenerTarifas(List<decimal> lstCantidades)
    {
        //Query usada para traer los id de las tarifas
        string sQuery = "SELECT " +
                            "ttr.idTarifaReferencia AS iIdTarifaReferencia, " +
                            "trt.idTipoRegla AS iIdTipoRegla,  " +
                            "tct.idTipoTarifa AS iIdTipoTarifa " +
                        "FROM  " +
	                        "tTarifaReferencia ttr " +
	                        "JOIN " +
	                        "tReglaTarifa trt " +
                            "ON ttr.idConfTarifa = trt.idConfTarifa " +
                            "JOIN " +
                            "tConfiguracionTarifa tct " +
                            "ON ttr.idConfTarifa = tct.idConfTarifa " +
                            "JOIN " +
                            "tReferencia tr " + 
                            "ON tr.idReferencia = ttr.idReferencia " +
                        "WHERE ttr.idReferencia = " + iIdSubReferencia + 
                        " AND ttr.idEstatus = 1  AND tr.idTipoOperacion = tct.idTipoOperacion";
        //Se instancia objeto de conexion
        Conexion oConexion = new Conexion();
        // Se instancia la lista
        lstIdentificadoresTarifa = new List<TarifaReferencia>();
        // Se crea arreglo de nombres
        string[] arrColumnas = new string[] { "iIdTarifaReferencia", "iIdTipoRegla", "iIdTipoTarifa" };
        //Se recupera la lista
        oConexion.ejecutaRecuperaObjetoLista<TarifaReferencia>(sQuery, arrColumnas, lstIdentificadoresTarifa);
        //Por cada objeto de la lista se manda a llamar a un metodo para obtener 
        //el calculo de costo
        int i = 0;
        foreach (TarifaReferencia item in lstIdentificadoresTarifa)
        {
            var oCalculo = fn_ObtenerObjetoCalculoCosto(item.iIdTipoRegla);
            if (lstCantidades != null)
            {
                oCalculo.fn_AsignarCantidad(lstCantidades[i]);
            }
            oCalculo.fn_Inicializar(item.iIdTarifaReferencia, item.iIdTipoTarifa, iIdSubReferencia);
            lstObjetosCalculo.Add(oCalculo);
            i++;
        }
    }

    /// <summary>
    /// Método para obtener el objeto calculo costo dependiendo del tipo de regla
    /// </summary>
    /// <param name="iTipoRegla">ID del tipo de regla</param>
    /// <returns></returns>
    private ICalculoTarifaCosto fn_ObtenerObjetoCalculoCosto(int iTipoRegla)
    {
        switch (iTipoRegla)
        {
            case 1:
            case 4:
                return new CalculoCostoRangos();
            case 2:
            case 3:
                return new CalculoCostoUnicoValor();
            default:
                return null;
        }
    }

    /// <summary>
    /// Método para generar el HTML del cobro de la tarifa
    /// </summary>
    public void fn_RealizarCalculo()
    {
        for (int i = 0; i < lstIdentificadoresTarifa.Count(); i++ )
        {
            TarifaReferencia oTarifa = lstIdentificadoresTarifa[i];
            ICalculoTarifaCosto oCalculo = lstObjetosCalculo[i];
            sHTML += "<tr>";
                sHTML += "<td>"; // Servicio
                    sHTML += fn_ObtenerServicio(oTarifa.iIdTarifaReferencia);
                sHTML += "</td>";
                sHTML += "<td>"; // Cantidad
                    sHTML += oCalculo.fn_ObtenerCantidadHtml();
                sHTML += "</td>";
                sHTML += "<td>$"; // Fijo
                    sHTML += oCalculo.fn_ObtenerPrecioPorUnidad();
                sHTML += "</td>";
                sHTML += "<td>$"; // Subtotal
                     sHTML += oCalculo.fn_ObtenerSubTotal();
                sHTML += "</td>";
                sHTML += "<td>"; // IVA
                    sHTML += "$" + oCalculo.fn_ObtenerIVAValor();    
                    sHTML += " (" + oCalculo.fn_ObtenerIVA();
                sHTML += "%)</td>";
                sHTML += "<td>"; // Retención
                    sHTML += "$" + oCalculo.fn_ObtenerRetencionValor();
                    sHTML += " (" + oCalculo.fn_ObtenerRetencion();
                sHTML += "%)</td>";
                sHTML += "<td>"; // Total
                    sHTML += oCalculo.fn_ObtenerTotalHtml();
                sHTML += "</td>";
            sHTML += "</tr>";
        }
    }

    /// <summary>
    /// Metodo para obtener el nombre del servicio
    /// </summary>
    /// <param name="iIdTarifaReferencia">ID de la tarifa-referencia</param>
    /// <returns>Nombre del servicio</returns>
    private string fn_ObtenerServicio(int iIdTarifaReferencia)
    {
        //Query para obtener el servicio
        string sQuery = "SELECT cs.descripcion " +
                        "FROM tTarifaReferencia ttr " +
	                    "     JOIN " +
	                    "     tConfiguracionTarifa tct " +
	                    "     ON ttr.idConfTarifa = tct.idConfTarifa " +
	                    "     JOIN cServicio cs " +
                        "     ON tct.idServicio = cs.idServicio " +
                        "WHERE ttr.idTarifaReferencia = " + iIdTarifaReferencia; 
        //Se instancia el objeto de conexion
        Conexion oConexion = new Conexion();
        //Se recupera el nombre y se retorna
        return oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
    }

    /// <summary>
    /// Método para obtener el nombre del tipo de tarifa
    /// </summary>
    /// <param name="iIdTarifaReferencia">ID de la tarifa-referencia</param>
    /// <returns>Nombre del tipo de tarifa</returns>
    private string fn_ObtenertipoTarifa(int iIdTarifaReferencia)
    {
        //Query
        string sQuery = "SELECT ctt.nombre " +
	                    "FROM cTipoTarifa ctt " +
		                "     JOIN  " +
		                "     tConfiguracionTarifa tct " +
		                "     ON tct.idTipoTarifa = ctt.idTipoTarifa " +
		                "     JOIN " +
		                "     tTarifaReferencia ttr " +
                        "     ON ttr.idConfTarifa = tct.idConfTarifa " +
	                    "WHERE ttr.idTarifaReferencia = " + iIdTarifaReferencia;
        //Instancia objeto conexion
        Conexion oConexion = new Conexion();
        //Retorna el resultado de la consulta
        return oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
    }

    /// <summary>
    /// Método para obtener el tipo de material de una tarifa
    /// </summary>
    /// <param name="iIdTarifaReferencia">ID de la tarifa-referencia</param>
    /// <returns>Nombre del material</returns>
    private string fn_ObtenerTipoMaterialTarifa(int iIdTarifaReferencia)
    {
        //Query
        string sQuery = "SELECT ctm.nombre " +
	                    "FROM cTipoMaterial ctm " +
		                "     JOIN  " +
		                "     tConfiguracionTarifa tct " +
		                "     ON tct.idTipoMaterial = ctm.idTipoMaterial " +
		                "     JOIN " +
		                "     tTarifaReferencia ttr " +
                        "     ON ttr.idConfTarifa = tct.idConfTarifa " +
	                    "WHERE ttr.idTarifaReferencia = " + iIdTarifaReferencia;
        //Instancia clase conexion
        Conexion oConexion = new Conexion();
        //Se retorna el nombre obtenido
        return oConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
    }

    #endregion
}