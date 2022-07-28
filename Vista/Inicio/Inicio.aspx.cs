using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vista_Inicio_Inicio : System.Web.UI.Page
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
            ///Se declaran los breadCrumbs
            string[] sDatos = { "Inicio" };
            string[] sUrl = { "" };
            breadCrum.migajas(sDatos, sUrl);
            ///recupera el id de Usuario
            string sIdUsuario = Session["iIdUsuario"].ToString();
            //Variable para almacenar id menú
            string sIdMenu;
            //Se verifica si el id menú esta nulo
            if (Request.QueryString["sIdMenu"] == null)
                sIdMenu = "MAA=";
            else 
                sIdMenu =  Request.QueryString["sIdMenu"];
            //Se asigna el id de menú
            hlblIdMenu.InnerHtml = sIdMenu;
            //Se asigna el id del usuario
            hlblIdUsuario.InnerHtml = sIdUsuario;
            //Se genera el combo del menú
            fn_GenerarComboMenus(sIdUsuario);
            //Se genera la lista de pantallas
            fn_GeneraListaPantallas();
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
        }
    }

    /// <summary>
    /// Método para generar combo de menús
    /// </summary>
    public void fn_GenerarComboMenus(string sIdUsuario)
    {
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        //Se desencripta id de usuario
        Security secIdUsuario = new Security(sIdUsuario);
        ///Se pasan los parámetros
        objUtilerias.sNombre = "hslcMenus";
        objUtilerias.sQuery = "SELECT cm.idMenu,cm.nomMenu FROM cMenu cm WHERE cm.idTipoMenu=1 AND idMenu = 92 and (SELECT COUNT(*) FROM cMenu c WHERE c.idMenuPadre=cm.idMenu and c.idMenu in (SELECT trm.idMenu FROM tRolMenu trm WHERE trm.idTipoAccesoMenu in (1,2) and trm.idRol in (SELECT tuor.idRol FROM tUsuarioComitenteRol tuor WHERE tuor.idUsuarioComitente in (SELECT tuo.idUsuarioComitente FROM tUsuarioComitente tuo WHERE tuo.idUsuario=" + secIdUsuario.DesEncriptar() + ")))) <> 0 ORDER BY cm.orden ASC";
        objUtilerias.sQueryHijos = "SELECT cm.idMenu,cm.nomMenu FROM cMenu cm WHERE cm.idTipoMenu=2 and cm.idMenuPadre = 92 and cm.idMenu in (SELECT trm.idMenu FROM tRolMenu trm WHERE trm.idTipoAccesoMenu in (1,2) and trm.idRol in (SELECT tuor.idRol FROM tUsuarioComitenteRol tuor WHERE tuor.idUsuarioComitente in (SELECT tuo.idUsuarioComitente FROM tUsuarioComitente tuo WHERE tuo.idUsuario=" + secIdUsuario.DesEncriptar() + "))) ORDER BY cm.orden ASC";
        ///Se ejecuta el método para generar estructura del combo con grupos
        objUtilerias.fn_GeneraComboboxGroups(objUtilerias);
        ///Se asigna el contenido a HTML
        hdvComboPantallas.InnerHtml = objUtilerias.sContenido;
    }

    /// <summary>
    /// Método para generar estructura de lista de pantallas
    /// </summary>
    public void fn_GeneraListaPantallas()
    {
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "Menú", "Sub menú" };
        ///Se asignan los campos sin filtro
        string[] arrColumnasSinFiltro = { "Eliminar" };
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblPantallas";
        ///Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
        hdvTablaPantallas.InnerHtml = objUtilerias.sContenido;
    }

    /// <summary>
    /// Método para validar que no este la pantalla asiganada al usuario
    /// </summary>
    /// <param name="iIdUsuario">Identificador del usuario</param>
    /// <param name="iIdMenu">Identificador de la pantalla</param>
    /// <returns>objInicio</returns>
    [WebMethod]
    public static Inicio fn_ValidarUsuarioPantallaExiste(string sIdUsuario, int iIdMenu)
    {
        //Se instancia clase inicio
        Inicio objInicio = new Inicio();
        //Se desencripta el id del usuario
        Security secIdUsuario = new Security(sIdUsuario);
        //Se pasan los parametros
        objInicio.iIdUsuario = int.Parse(secIdUsuario.DesEncriptar());
        objInicio.iIdMenu = iIdMenu;
        //Se ejecuta el método para validar pantalla usuario
        objInicio.fn_ValidarUsuarioPantallaExiste(objInicio);
        //Se retorna el objeto
        return objInicio;
    }

    /// <summary>
    /// Método para guardar el la pantalla al usuario
    /// </summary>
    /// <param name="iIdUsuario">Identificador del usuario</param>
    /// <param name="iIdMenu">Identificador de la pantalla</param>
    /// <returns>objInicio</returns>
    [WebMethod]
    public static Inicio fn_GuardarUsuarioPantalla(string sIdUsuario, int iIdMenu)
    {
        //Se instancia clase inicio
        Inicio objInicio = new Inicio();
        //Se desencripta el id del usuario
        Security secIdUsuario = new Security(sIdUsuario);
        //Se pasan los parametros
        objInicio.iIdUsuario = int.Parse(secIdUsuario.DesEncriptar());
        objInicio.iIdMenu = iIdMenu;
        //Se ejecuta el método para guardar la pantalla al usuario
        objInicio.fn_GuardarUsuarioPantalla(objInicio);
        //Se retorna el objeto
        return objInicio;
    }

    /// <summary>
    /// Método para eliminar el la pantalla al usuario
    /// </summary>
    /// <param name="iIdUsuarioPantalla">Identificador del usuario pantalla</param>
    /// <returns>objInicio</returns>
    [WebMethod]
    public static Inicio fn_EliminarUsuarioPantalla(int iIdUsuarioPantalla)
    {
        //Se instancia clase inicio
        Inicio objInicio = new Inicio();
        //Se pasan los parametros
        objInicio.iIdUsuarioPantalla = iIdUsuarioPantalla;
        //Se ejecuta el método para eliminar la pantalla al usuario
        objInicio.fn_EliminarUsuarioPantalla(objInicio);
        //Se retorna el objeto
        return objInicio;
    }
    
    /// <summary>
    /// Método para obtener pantallas del usuario
    /// </summary>
    /// <param name="sIdUsuario">Identificador del usuario pantalla</param>
    /// <returns>objInicio</returns>
    [WebMethod]
    public static Inicio fn_ObtenerPantallasUsuario(string sIdUsuario)
    {
        //Se instancia clase inicio
        Inicio objInicio = new Inicio();
        //Se desencripta id de usuario
        Security secIdUsuario = new Security(sIdUsuario);
        //Se pasan los parametros
        objInicio.iIdUsuario = int.Parse(secIdUsuario.DesEncriptar());
        //Se ejecuta el método para obtener pantallas del usuario
        objInicio.fn_ObtenerPantallasUsuario(objInicio);
        //Se retorna el objeto
        return objInicio;
    }
    
    /// <summary>
    /// Método para obtener la alerta de sin acceso menú
    /// </summary>
    /// <param name="sIdMenu">Identificador del menu</param>
    /// <returns>objInicio</returns>
    [WebMethod]
    public static Inicio fn_AlertaSinAccesoMenu(string sIdMenu)
    {
        //Se instancia clase inicio
        Inicio objInicio = new Inicio();
        //Se desencripta id de usuario
        Security secIdMenu = new Security(sIdMenu);
        //Se pasan los parametros
        objInicio.iIdMenu = int.Parse(secIdMenu.DesEncriptar());
        //Se ejecuta el método para obtener pantallas del usuario
        objInicio.fn_AlertaSinAccesoMenu(objInicio);
        //Se retorna el objeto
        return objInicio;
    }

    /// <summary>
    /// Método para generar combobox
    /// </summary>
    /// <param name="sQuery">Consulta para obtener datos</param>
    /// <returns>objInicio</returns>
    [WebMethod]
    public static string fn_GeneraCombobox(string sQuery, string sNombre)
    {
        //Se remplaza simbolo por comilla simple
        sQuery = sQuery.Replace("@","'");
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se pasan los parámetros
        objUtilerias.sNombre = sNombre;
        objUtilerias.sQuery = sQuery;
        ///Se ejecuta el método para generar estructura del combo
        objUtilerias.fn_GeneraCombobox(objUtilerias);
        ///Se asigna el contenido a HTML
        return objUtilerias.sContenido;
    }


    public static Resultado fn_EnviarCorreo()
    {
        Resultado objResultado = new Resultado();
        Timer objTime = new Timer();

        objResultado.iResultado = 1;
        return objResultado;
    }



    protected void fn_EnviarCorreoAnticipo(object sender, EventArgs e)
    {
        //inicia clase para enviar correos de alerta
        Email objEmail = new Email("Registro Anticipo");
        //Se instancia la clase
        Conexion objConexion = new Conexion();
        //Lista de referencias
        List<string> lstReferencias = new List<string>();
        try
        {
            //Se crea lista de correo a quien se mandara el registro
            string[] lstCorreos = { "victor.carano@nadglobal.com;jesus.guerrero@nadglobal.com" }; //
            string correo = "";
            foreach (string sRes in lstCorreos)
            {
                correo += sRes + ";";
            }
            //Se quita la ultima punto y coma
            correo = correo.Substring(0, correo.Length - 1);
            //inicia la configuración, asigna destinos, cuerpo y envia correo
            objEmail.fn_IniciarConfiguracionCorreo(true, System.Net.Mail.MailPriority.Normal);
            //agrega destino
            objEmail._AddTo(correo);

            //Consulta
            string quequery = "SELECT DISTINCT refAdministrativa FROM tAnticiposClientes_ION WHERE env_FLAG = 0";
            lstReferencias = objConexion.ejecutarConsultaRegistroMultiples(quequery);
            //Se valida que tenga registros
            if (lstReferencias.Count > 1)
            {
                //Se quita el primer registro que no vale
                lstReferencias.RemoveAt(0);
                //Cuerpo correo
                string body = "";

                foreach (string dat in lstReferencias)
                {
                    //string quequery22 = "SELECT * FROM tAnticiposClientes_ION WHERE refAdministrativa = '" + dat + "'";
                    string quequery22 = "SELECT tac.refAdministrativa,tac.monto,ts.idClienteContable,tc.razonSocial,tc.rfcHomologado FROM tAnticiposClientes_ION tac INNER JOIN tSubReferencia ts ON tac.refAdministrativa = ts.refAdministrativa AND ts.refAdministrativa = '" + dat + "' INNER JOIN tCliente tc ON ts.idClienteContable = tc.idCliente";
                    //Se crea el cuerpo correo
                    body = cuerpoCorreo(quequery22);
                    //agraga mensaje
                    objEmail._AddBody(body);
                    //envia correo
                    objEmail.sendMail();
                }

                foreach (string dat in lstReferencias)
                {
                    string quequery33 = "UPDATE tAnticiposClientes_ION SET env_FLAG = 1 WHERE refAdministrativa = '" + dat + "' AND env_FLAG = 0";
                    //Se ejecuta la consulta
                    objConexion.ejecutarComando(quequery33);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("ERROR AL ENVIAR CORREOS" + ex);
        }
    }


    public string cuerpoCorreo(string query)
    {
        //Se instancia la clase
        Conexion objConexion = new Conexion();
        //Lista de registros
        List<string> lstRegistros = new List<string>();
        //Se ejecuta la consulta
        lstRegistros = objConexion.ejecutarConsultaRegistroMultiples(query);
        DataTable dtbRegistros = objConexion.ejecutarConsultaRegistroMultiplesData(query);

        string referencia, razonSoc, rfcHomo, montoInfo;
        string reftxt = "", montxt = "", raztxt = "", rfcHtxt = "", txtF = "";
        float total = 0;
        if (dtbRegistros.Rows.Count > 1)
        {
            var sRes = from fila in dtbRegistros.AsEnumerable()
                       let arrFila = new object[]
                       {
                           fila.Field<object>(0),
                           fila.Field<object>(1),
                           fila.Field<object>(3),
                           fila.Field<object>(4),
                       }
                       select arrFila;
            foreach (object[] item in sRes)
            {
                referencia = item[0].ToString();
                montoInfo = item[1].ToString();
                razonSoc = item[2].ToString();
                rfcHomo = item[3].ToString();
                total = total + (float.Parse(item[1].ToString()));
                reftxt = "<td width='115'>" + referencia + "</td> ";
                montxt = "<td width='115'>" + montoInfo + "</td> ";
                raztxt = razonSoc;
                rfcHtxt = rfcHomo;
                txtF += "<tr><td width='360'></td>" + reftxt + montxt + "</tr>";
            }
            //Se recorre la tabla

        }
        string body = "<html>" +
                                "<body style='font - family:'Century Gothic'' > " +
                                "<table cellpadding='2' width='590'>" +
                                "       <tr>" +
                                "           <td style='color: #00337e;'>ANTICIPO RECIBIDO</td>" +
                                "           <td colspan='2'><b>$" + total + "</b></td>" +
                                "       </tr>" +
                                "       <tr>" +
                                "           <td style='color: #00337e;'>CPT </td>" +
                                "           <td colspan='2'>" + rfcHtxt + "</td>" +
                                "       </tr>" +
                                "       <tr>" +
                                "           <td style='color: #00337e;'>RAZON SOCIAL</td>" +
                                "           <td colspan='2'>" + raztxt + "</td>" +
                                "       </tr>" +
                                "       <tr>" +
                                "           <td style='color: #00337e;'>NUMERO DE DIARIO ANTIC</td>" +
                                "           <td colspan='2'></td>" +
                                "       </tr>" +
                                "   </table>" +
                                "   <table cellpadding='10'>" +
                                "       <tr>" +
                                "           <td width='330' style='color: #00337e;'>REFERENCIA ADMINISTRATIVA E IMPORTES</td>" +
                                "               <td >REFERENCIA</td>" +
                                "               <td >IMPORTES</td>" +
                                "       </tr>" +
                                "   </table>" +
                                "   <table>" +
                                        txtF +
                                "   </table>";
        body += "</body></html>";
        return body;
    }
}