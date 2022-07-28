using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vista_monitor_cargas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Instancia a la clase SessionTimeOut
        SessionTimeOut obj_Session = new SessionTimeOut();
        //se manda llamar al metodo de validar sesion
        bool iSessionE = obj_Session.IsSessionTimedOut();

        //se valida la sesion
        if (!iSessionE)
        {
            //Inicio TRY
            try
            {
                //se obtien id de usuario Loggeado
                hlblIdUsuarioAccion.InnerHtml = Session["iIdUsuario"].ToString();
                ///Se declaran los breadCrumbs
                string[] datos = { "Inicio", "Utilerias", "Bitácora de cargas" };
                string[] url = { "", "", "" };
                breadCrum.migajas(datos, url);
                ///Metodo para generar la estructura de la lista de usuarios
                fn_genera_lista_bitacora();
                fn_genera_lista_bitacoraCP();
                fn_genera_lista_bitacoraNC();

            }
            //*******
            //Inicio Catch
            catch
            {
                //si ocurre algun error se redirecciona al usuario al Inicio 
                Response.Redirect("../Inicio/Inicio.aspx");
                //******
            }
            //*******

        }
        //**********
        else
        {
            //si se termino el tiempo de sesion se redirecciona al Login y cierra sesión
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../../Login.aspx");
            //***********
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void fn_genera_lista_bitacora()
    {
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "Estatus", "Folio", "UUID", "Remitente", "Fecha" };
        ///Se asignan los campos sin filtro
        string[] arrColumnasSinFiltro = { "Ver Detalle" };
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblBitacoraCarga";
        ///Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
        hlblBitacora.InnerHtml = objUtilerias.sContenido;
    }

    public void fn_genera_lista_bitacoraCP()
    {
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "Estatus", "Folio", "UUID", "Remitente", "Fecha" };
        ///Se asignan los campos sin filtro
        string[] arrColumnasSinFiltro = { "Ver Detalle" };
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblBitacoraCargaCP";
        ///Se ejecuta el método para generar estrucStura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
        hlblBitacoraCP.InnerHtml = objUtilerias.sContenido;
    }

    public void fn_genera_lista_bitacoraNC()
    {
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "Estatus", "Folio", "UUID", "Remitente", "Fecha" };
        ///Se asignan los campos sin filtro
        string[] arrColumnasSinFiltro = { "Ver Detalle" };
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblBitacoraCargaNC";
        ///Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
        hlblBitacoraNC.InnerHtml = objUtilerias.sContenido;
    }

    [WebMethod]
    public static string fn_LlenarTablaDetalleBitacora(string sIdRegistroBitacora)
    {
        Conexion objConexion = new Conexion();
        string sQuery = @"SELECT html 
                          FROM tLogCargaWS
                          WHERE idLogCargaWS = " + sIdRegistroBitacora;
        string[] sRes = new string[2];
        string sRetorno = "";
        sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        if (sRes[0] == "1")
        {
            sRetorno = sRes[1];
        }
        else
        {
            sRetorno = "Error";
        }
        return sRetorno;
    }

}