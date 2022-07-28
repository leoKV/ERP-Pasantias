using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vista_Diseno_Footer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            fn_GeneraListaGastoFijoN();
            fn_GeneraListaOrdenesN();
            fn_GeneraListaIntegracionFacturas();
            fn_GeneraListaIntegracionOV();
            fn_GeneraListaIntegracionOVDia();
        }
        catch (Exception ex) {
        }
 

    }

    public void fn_GeneraListaGastoFijoN()
    {
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "Comitente", "Proveedor", "Monto", "Moneda", "Autoriza", "Día", "Periodicidad", "Repetir cada", "Responsable" };
        string[] arrColumnasSinFiltro = { "Estatus","Detalle"};
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblGastosFijosNotificacion";
        ///Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
        hdvGastoFijoNotificaciones.InnerHtml = objUtilerias.sContenido;
    }

    public void fn_GeneraListaOrdenesN()
    {
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "Folio orden", "Folio SUN", "Folio transitorio", "Tipo orden", "Moneda", "Cuenta gastos", "Oficina", "Solicitado por" };
        string[] arrColumnasSinFiltro = { "Controles" };
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblOrdenesNotificacion";
        ///Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
        hdvOrdenesNotificaciones.InnerHtml = objUtilerias.sContenido;
    }

    public void fn_GeneraListaIntegracionFacturas()
    {
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "Número Factura", "Fecha Factura"};
        string[] arrColumnasSinFiltro = { "Orden de Integracion", "Número de lineas a integrar" };
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblIntegracionFacturasTerceros";
        ///Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
        hdvFacturasTercerosIntegrandoce.InnerHtml = objUtilerias.sContenido;
    }
    public void fn_GeneraListaIntegracionOV()
    {
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "Folio Orden Venta","Fecha Orden Venta" };
        string[] arrColumnasSinFiltro = { "Orden de Integracion", "Número de lineas a integrar" };
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblIntegracionOV";
        ///Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
        hdvOVIntegrandoce.InnerHtml = objUtilerias.sContenido;
    }
    public void fn_GeneraListaIntegracionOVDia()
    {
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "Folio Orden Venta", "OVST" };
        string[] arrColumnasSinFiltro = { "Fecha de procesamiento", "Estatus", "Líneas Totales" };
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblOVDia";
        ///Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
        hdvOVDia.InnerHtml = objUtilerias.sContenido;
    }

}