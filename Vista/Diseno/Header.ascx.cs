using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vista_Prueba_Header : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ///INSTNACIA A CLASE DE SESSION
        SessionTimeOut session = new SessionTimeOut();
        ///EJECUTA METODO PARA VERIFICAR SI NO ESTA CADUCADA LA SESSION
        bool sessionE = session.IsSessionTimedOut();
        ///VERIFICA SI NO ESTA CADUDADA SESSION
        if (!sessionE)
        {
            ///INICIO TRY
            try
            {
                ///CONSTRUYE MENU
                //lblMenu.Text = menuUsuario(Session["iIdUsuario"].ToString());
                ///Variable para desencryptar id de usuario
                Security secNomUser = new Security(Session["NombreUsuario"].ToString());
                Conexion objConexion = new Conexion();
                ///MUESTRA NOMBRE DE USUARIO
                alblNombreUsuario.Text = secNomUser.DesEncriptar();
                //Se genera menú
                fn_GenerarMenu(Session["iIdUsuario"].ToString());
                secNomUser = new Security(Session["iIdUsuario"].ToString());
                string sQuery = "select count(*) from tGasto where idResponsable = " + secNomUser.DesEncriptar() + " and GETDATE() >= DATEADD(day,-notificar,fechaCumplimiento) and idEstatus !=3";
                Session["iAlertas"] = objConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
                // Acomodar gastos
                if (Session["iAlertas"].ToString() == "0")
                    hliGastosNotificaciones.Visible = false;
                else
                    hspGastosNotificaciones.InnerHtml = "✔";
                // Agregar alertas ordenes
                //sQuery = "SELECT COUNT(*) " +
                //         "FROM tOrdenVenta tov " +
                //         "WHERE tov.idEstatus = 6 " +
                //         "      AND " +
                //         "      tov.idEncargado = " + secNomUser.DesEncriptar();

                sQuery = @"
                            SELECT COUNT(*)
                         FROM tOrdenVenta tov
                         WHERE tov.idEstatus = 6
                               AND (SELECT COUNT(1) FROM tUsuarioComitenteRol WHERE idUsuarioComitente in (SELECT idUsuarioComitente FROM tUsuarioComitente WHERE idUsuario = "+   secNomUser.DesEncriptar() +") and idRol = 9) >= 1";

                int iOrdenes = int.Parse(objConexion.ejecutarConsultaRegistroSimple(sQuery)[1]);
                // Acomodar ordenes
                if (iOrdenes == 0)
                    hliOrdenesNotificaciones.Visible = false;
                else
                //    hspOrdenesNotificaciones.InnerHtml = iOrdenes.ToString();
                //Session["iAlertas"] = int.Parse(Session["iAlertas"].ToString()) + iOrdenes;
                hspOrdenesNotificaciones.InnerHtml = "✔";
                Session["iAlertas"] = int.Parse(Session["iAlertas"].ToString()) + iOrdenes;
                fn_GenerarFacturasOVProcesandoce();
                

                if (Session["iAlertas"] != null && int.Parse(Session["iAlertas"].ToString()) > 0)
                {
                    btnNotificaciones.Visible = true;
                    iGastos.InnerHtml = "✔";
                }
            }///FIN TRY
             ///INICIO CATCH
            catch (Exception ex)
            {

            }///FIN CATCH

        }/////FIN VERIFICA NO ESTA CADUCADA LA SESSION
        ///INICIO EL SE ESTA CADUCA SESSION
        else
        {
            ///INICIO TRY
            try
            {
                ///LIMPIA VARIABLES
                Session.Clear();
                Session.Abandon();
                ///REDIRECCIONA A LOGIN
                Response.Redirect("../../Login.aspx", false);
            }///FIN TRY
            ///INICIO CATCH
            catch (Exception ex)
            {

            }///FIN CATCH

        }///FIN ELSE ESTA CADUCADA SESSION
    }

    /// <summary>
    /// Método para cerrar session
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            #region Metodo para registrar la hora y fecha de salida
            //Clase DateTime
            DateTime obj_datetime = DateTime.Now;
            //Clase Bitacora Acceso
            BitacoraAcceso obj_bitAcceso = new BitacoraAcceso();
            //Se obtiene la cadena encriptada correspondiente al id del usuaio
            string sIdUsuario = Session["iIdUsuario"].ToString();
            //Se instanacia la clase Security pasando como parametro el id encriptado
            Security secIdUsuario = new Security(sIdUsuario);
            //Se desencripta id del usuario
            int iUsuario = int.Parse(secIdUsuario.DesEncriptar());
            //Se obtiene la variable de Session correspondiente al registro activo y se setea junto con la fecha y hora
            obj_bitAcceso.iIdBitacora = Convert.ToInt32(Session["iIdBitacora"]);
            obj_bitAcceso.iIdUsuario = iUsuario;
            obj_bitAcceso.sNombreHost = obj_bitAcceso.rsRecuperarNombreHost();
            obj_bitAcceso.sIpLocal = obj_bitAcceso.rsRecuperarIPLocal();
            obj_bitAcceso.sIpPublica = obj_bitAcceso.rsRecuperaIPPublica();
            obj_bitAcceso.dFechaLogin = obj_datetime;
            obj_bitAcceso.dfechaLogout = obj_datetime;
            //Se ejecuta el metodo para crear el registro en la bitacora de acceso
            obj_bitAcceso.fn_NuevoRegistroBitacora(obj_bitAcceso);
            #endregion
            if (Session["iIdUsuario"] != null) Session.Remove("iIdUsuario");
            if (Session["iTipoUsuario"] != null) Session.Remove("iTipoUsuario");
            if (Session["NombreUsuario"] != null) Session.Remove("NombreUsuario");

            Session.Abandon();
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Session.RemoveAll();
            Response.Redirect("../../LogIn.aspx", false);
        }
        catch (Exception ex)
        {
            Session.Abandon();
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Session.RemoveAll();
            Response.Redirect("../../LogIn.aspx", false);
        }

    }

    /// <summary>
    /// Método para redireccionar a información de perfil
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

    }


    /// <summary>
    /// Método para redireccionar a información de perfil
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkButton3_Click(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Método para generar el menú
    /// </summary>
    /// <param name="sIdUsuario">Identificador de usuario encriptado</param>
    public void fn_GenerarMenu(string sIdUsuario) { 
        //Se instancia clase inicio
        Inicio objInicio = new Inicio();
        //Se desencripta id del usuario
        Security secIdUsuario = new Security(sIdUsuario);
        //Se pasan los parametros
        objInicio.iIdUsuario = int.Parse(secIdUsuario.DesEncriptar());
        //Se ejecuta método para obtener menú
        objInicio.fn_GenerarMenu(objInicio);
        //Se crea asigna el html del contenido
        hdvMenu.InnerHtml = objInicio.sContenido;

        #region Metodo para guardar la url en los detalles de la bitacora
        //Clase Bitacora Acceso
        DetalleBitacora obj_DetBitacora = new DetalleBitacora();
        //Clase DateTime
        DateTime obj_datetime = DateTime.Now;
        //Set a los campos
        obj_DetBitacora.iIdRegistroBitacora = int.Parse(Session["iIdBitacora"].ToString());
        obj_DetBitacora.sUrl = obj_DetBitacora.fn_ObtenerUrl();
        obj_DetBitacora.sPaginaVisitada = obj_DetBitacora.fn_ObtenerPagina();
        obj_DetBitacora.dFechaVisita = obj_datetime;

        obj_DetBitacora.fn_NuevoDetalleBitacora(obj_DetBitacora);

        #endregion
    }
    /// <summary>
    /// Método para consultar Facturas y Ordenes Venta en proceso
    /// </summary>
    /// <param name="sIdUsuario">Identificador de usuario encriptado</param>
    public void fn_GenerarFacturasOVProcesandoce()
    {
                Conexion objConexion = new Conexion();
        string sQuery = "";
//Facturas 
                sQuery = "SELECT sTotal FROM v_IntegracionFacturasTerceros";
                int iFacturasTerceros = int.Parse(objConexion.ejecutarConsultaRegistroSimple(sQuery)[1]);
                //Acomodar Facturas

                if (iFacturasTerceros == 0)
                    hliFacturasNotificaciones.Visible = false;
                else
                hspFacturasTerceros.InnerHtml = "✔";
                Session["iAlertas"] = int.Parse(Session["iAlertas"].ToString()) + iFacturasTerceros;
                //Ordenes Venta 
                sQuery = "SELECT sContador FROM v_IntegracionOrdenVenta";
                int iOVProcesando = int.Parse(objConexion.ejecutarConsultaRegistroSimple(sQuery)[1]);
                //Acomodar OV Integrandoce

                if (iOVProcesando == 0)
                    hliOrdenesPasandoNotificaciones.Visible = false;
                else
                hspOrdenesVentaProcesandoce.InnerHtml = "✔";
                Session["iAlertas"] = int.Parse(Session["iAlertas"].ToString()) + iOVProcesando;

                hspOrdenesVentaDia.InnerHtml = "✔";



    } 
    
    protected void btnNotificaciones_Click(object sender, EventArgs e)
    {
        //fn_GenerarFacturasOVProcesandoce();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { fn_MostrarNotificaciones(); });", true);
    }


}