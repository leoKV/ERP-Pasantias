using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vista_Tools_CargaGenerica : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Instancia a la clase SessionTimeOut
        SessionTimeOut obj_Session = new SessionTimeOut();
        //se manda llamar al metodo de validar sesion
        bool iSessionE = obj_Session.IsSessionTimedOut();
        //Constante que almacena id de menú
        int iMENU = 11;
        //Encriptar id menú
        Security secIdMenu = new Security(iMENU.ToString());
        //se valida la sesion
        if (!iSessionE)
        {
            //Inicio TRY
            try
            {
                //Se declaran los breadCrumbs
                string[] datos = { "Inicio", "Tools", "Carga másiva" };
                string[] url = { "", "", "" };
                breadCrum.migajas(datos, url);

                // Desencriptar usuario
                string sIdUsuario = Session["iIdUsuario"].ToString();
                Security objSecurity = new Security(sIdUsuario);

                ///INSTANCIA A CLASE PERMISOS (Se pasa el id del usuario y el identificador del menú)
                Permisos objPermisos = new Permisos(sIdUsuario, iMENU);
                ///variable para almacenar el tipo de acceso
                objPermisos.getValidaAction(objPermisos);

                //valida los permisos del usuario en la pantalla
                if (objPermisos.iIdTipoAcceso == 1 || objPermisos.iIdTipoAcceso == 2)
                {
                    //Se verifica si solo se tiene permisos de consulta y se ocultan campos
                    if (objPermisos.iIdTipoAcceso == 2)
                    {
                        
                    }
                }
                ///INICIO NO TIENE ACCESO
                else
                {
                    ///HAY ERROR EN EJECUCION, REDIRECCIONA A INICIO
                    Response.Redirect("../Inicio/Inicio.aspx?sIdMenu=" + secIdMenu.Encriptar());
                }///FIN NO TIENE ACCESO
            }
            //Inicio Catch
            catch
            {
                //si ocurre algun error se redirecciona al usuario al Inicio 
                Response.Redirect("../Inicio/Inicio.aspx?sIdMenu=" + secIdMenu.Encriptar());
            }
        }
        else
        {
            //si se termino el tiempo de sesion se redirecciona al Login y cierra sesión
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../../Login.aspx");
        }
    }

    [WebMethod]
    public static void fn_CargaMasiva(string sFileName)
    {
        HttpContext.Current.Session.Add("fProgresoCarga", (float) 0.0);
        HttpContext.Current.Session.Add("fCompletado", (bool)false);
        Tools objTools = new Tools();
        objTools.sFileName = sFileName;
        Thread objThread = new Thread(new ParameterizedThreadStart(objTools.fn_ProcesaArchivo));
        objThread.Start(new object[] { HttpContext.Current.Session, HttpContext.Current });
    }

    [WebMethod(EnableSession=true)]
    public static Tools fn_LeerProgreso()
    {
        Tools objTools = new Tools();
        objTools.fProgreso = (float) HttpContext.Current.Session["fProgresoCarga"];
        
        if ((bool)HttpContext.Current.Session["fCompletado"])
        {
            objTools.fProgreso = 80f;
            objTools.lstErrores = (List<string>)HttpContext.Current.Session["lstErrores"];
            objTools.fCompletado = true;
            HttpContext.Current.Session.Remove("fCompletado");
            HttpContext.Current.Session.Remove("fProgresoCarga");            
            HttpContext.Current.Session.Remove("lstErrores");
        } else
        {
            objTools.fCompletado = false;
        }
        return objTools;
    }
}