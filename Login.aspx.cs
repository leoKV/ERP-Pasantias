using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;



public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Regex obj = new Regex("^[0-9]+(.[0-9]+)?(\\|([0-9]+(.[0-9]+)?))*$");
        //Regex obj = new Regex("^[0-9]+(\\|([0-9]+)?)*$");
        Regex obj = new Regex("^[A-Za-z]+(\\/([A-Za-z]+))*$");
        string sParnet = "";
        sParnet = "DSF/ASDF/gfgf";
        bool isValid = obj.IsMatch(sParnet);

    
    }


    #region Metodo para abrir el Dialog del Capctha
    protected void vAbreDialogCambioContraseña(object sender, EventArgs e)
    {
        //manda llamar la función de javaScript de abrir Dialog
        //ClientScript.RegisterStartupScript(this.GetType(), "fn_AbrirModales", "fn_AbrirModales()");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { fn_AbrirModales(); });", true);
        /****************************************************************/
    }
    #endregion

    #region Metodo Para Entrar al sistema
    protected void btnEntrar_Click(object sender, EventArgs e)
    {

        //Instancias 
         //Se instancia clase inicio
        Inicio objInicio = new Inicio();
        //Clase Conexion
        Conexion obj_Conexion = new Conexion();
        //Clase LoginDatos
        LoginDatos obj_Login = new LoginDatos();
        //Clase DateTime
        DateTime obj_datetime = DateTime.Now;
        //Clase de Security
        Security obj_secDatos;
        //Clase Bitacora Acceso
        BitacoraAcceso obj_bitAcceso = new BitacoraAcceso();
        //Variable de tipo de cambio
        float tipoCambio = 1;
        //**
        //Inicio TRY
        try
        {
            //se recuperan valores ingresados por el usuario
            //nombre de usuario
            string sUsuario = htxtUsuario.Value;
            //contraseña
            string sPassword = htxtContrasenia.Value;
            //*********************************
            //se agrega el valor de los campos a los metodos set
            obj_Login.gssUsuario = sUsuario;
            obj_Login.gssPassword = sPassword;
            //*****
            obj_Login.vValidaUsuario(obj_Login);
            //Se verifica respuesta de método vValidaUsuario
            if (obj_Login.gsiResultado == 1)
            {
                //manda llamar la función de javaScript de abrir alerta y redireccionar
                ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(" + obj_Login.gsiResultado + ", \" " + obj_Login.gssMensaje + "\");/*setTimeout(function(){window.location.href='Vista/Inicio/Inicio.aspx';},0)*/}</script>");
                /****************************************************************/
                //se guardan las variables de sesión
                //Id de usuario
                //se encripta
                obj_secDatos = new Security(obj_Login.gsiIdUsuario.ToString());
                Session["iIdUsuario"] = obj_secDatos.Encriptar();
                //
                //Tipo de Usuario
                //se encripta
                obj_secDatos = new Security(obj_Login.gsiTipoUsuario.ToString());
                Session["iTipoUsuario"] = obj_secDatos.Encriptar();
                //Nombre de Usuario
                //se encripta
                string NombreUsuario = obj_Login.gssNombre + " " + obj_Login.gssApePat + " " + obj_Login.gssApeMat;
                obj_secDatos = new Security(NombreUsuario);
                Session["NombreUsuario"] = obj_secDatos.Encriptar();
                //Se desencripta nombre usuario
                string a = Session["NombreUsuario"].ToString();
                obj_secDatos = new Security(a);
                string d = obj_secDatos.DesEncriptar();
                #region Crear un nuevo registo en la bitacora de acceso
                 
                //Se obtiene la cadena encriptada correspondiente al id del usuaio
                string sIdUsuario = Session["iIdUsuario"].ToString();
                //Se instanacia la clase Security pasando como parametro el id encriptado
                Security secIdUsuario = new Security(sIdUsuario);
                //Se desencripta id del usuario
                int iUsuario = int.Parse(secIdUsuario.DesEncriptar());
                //Set a los datos del objeto obj_bitAcceso  
                obj_bitAcceso.iIdBitacora = 0;
                obj_bitAcceso.iIdUsuario = iUsuario;
                obj_bitAcceso.sNombreHost = obj_bitAcceso.rsRecuperarNombreHost();
                obj_bitAcceso.sIpLocal = obj_bitAcceso.rsRecuperarIPLocal();
                obj_bitAcceso.sIpPublica = obj_bitAcceso.rsRecuperaIPPublica();
                obj_bitAcceso.dFechaLogin = obj_datetime;
                obj_bitAcceso.dfechaLogout = obj_datetime;
                //Se ejecuta el metodo para crear el registro en la bitacora de acceso
                obj_bitAcceso.fn_NuevoRegistroBitacora(obj_bitAcceso);
                //Se asigna el idBitacora devuelto por el metodo, a una variable de sesion

                 
                Session["iIdBitacora"] = obj_bitAcceso.iIdBitacora;
                #endregion

        
                //se redirecciona a la pagina de inicio
                Response.Redirect("Vista/Inicio/Inicio.aspx", false);
                /**************************************************/
            }
            else if (obj_Login.gsiResultado == 4)
            {
                //manda llamar la funcion de javaScript de abrir alerta
                ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{/*$.notificacionMsj(" + obj_Login.gsiResultado + ", \" " + obj_Login.gssMensaje + "\");*/" +
                    "fn_abreDialogCambioPass('" + obj_Login.gssContenido + "','Cambio de contraseña','javaScript:alert(" + obj_Login.gsiIdUsuario + ")');$('#txtCambioPassword').val('');$('#atxtRepitePass').val('');}</script>");
                /****************************************************************/
                atxtUsuarioCambioPassword.Text = obj_Login.gssUsuario;
                ahiddenId.Value = obj_Login.gsiAccion.ToString();
                ahiddenNombre.Value = obj_Login.gssUsuario;
            }
            else
            {
                //manda llamar la funcion de javaScript de abrir alerta
                ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(" + obj_Login.gsiResultado + ", \" " + obj_Login.gssMensaje + "\");}</script>");
                /****************************************************************/
            }
        }
        /********/
        /*Inicio CATCH*/
        catch (Exception ex)
        {
            //manda llamar la funcion de javaScript de abrir alerta
            ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{ $.notificacionMsj(3, 'Lo sentimos, ha sucedido un error de tipo <b>EXEPCIÓN</b> comunícate con el departamento de TI, Error:" + ex.Message + "');}</script>");
            /****************************************************************/
        }
        /**********/

    }
    #endregion

    #region  Metodo para elegir nuevo  captcha

    /// <summary>
    /// Metodo para elejir otro captcha
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void vElegirNuevoCaptcha(object sender, EventArgs e)
    {
        //se limpian las cajas de texto y mensaje de captcha
        atxtUsuario.Text = "";
        atxtCaptcha.Text = "";
        //*****************************************************
        //se agrega la nueva imagen en la etiqueta image
        //imageCaptcha.ImageUrl = "Vista/Utilerias/CrearCaptcha.aspx?New=1";
        //******************************************************

        //se habre el dialog donde se encuentra el captcha
        //ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$('#hdvDialogCambioPass').modal('show')}</script>");
        //ClientScript.RegisterStartupScript(this.GetType(), "function", "<script>{fn_AbrirModales();}</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { fn_AbrirModales(); });", true);
        //********************************************************************************************************************************************************************************************
    }


    #endregion

    #region Metodo para hacer el Cambio de contraseña
    public void vCambioPassword(object sender, EventArgs e)
    {

        try
        {
            /*Instancia a la clase conexion*/
            Conexion con = new Conexion();
            /******************************/
            //atxtCaptcha

            //se crea objeto de respuesta
            LoginDatos obj_Login = new LoginDatos();
            //*************

            /*se recupera el usuario y captcha ingresados por el usuario*/
            string sUsuario = atxtUsuario.Text;
            string sCaptcha = atxtCaptcha.Text;
            /***********************************************************/

            /*se recupera el codigo captcha de session encriptado*/
            //string sCaptchaEncry = Session["CaptchaCode"].ToString();
            /****************************************************/

            /*Instancia a la clase security y se manda como parametro el codigo encryptado*/
            //Security obj_SecCaptcha = new Security(sCaptchaEncry);
            /***********************************************************************/

            /*Se recupera el captcha de sesión desencriptado*/
            string sCaptchaDes = htctOc.Text;
            //string prueba = Request.Form(htxtCaptcha.ClientID);
                //[htxtCaptcha.ClientID];
                
                //Form(htxtCaptcha.ClientID);
            /************************************************/

            /*Se valida que el usuario haya introducido la captcha correcta*/
            /*Si es correcta*/
            if (sCaptcha == sCaptchaDes)
            {
                //se settean las variables
                obj_Login.gssUsuario = sUsuario;
                obj_Login.gssPassword = rsNuevaPassword();
                obj_Login.gsiAccion = 1;
                /*Se manda llamar al metodo de Cambiar contraseña*/
                obj_Login.vCambiaPassword(obj_Login);
                /*********************************************************/

                if (obj_Login.gsiResultado == 1)
                {

                    obj_Login.vGeneraCorreo(obj_Login);
                    if (obj_Login.gsiResultado == 1)
                    {
                        //manda llamar la función de javaScript de abrir Dialog
                        ClientScript.RegisterStartupScript(this.GetType(), "LaunchServerSide", "<script>{$.notificacionMsj(1,'Cambio de contraseña exitoso');$('#hdvConfirmacion').modal('toggle');}</script>");
                        /****************************************************************/
                    }
                    else
                    {
                        //manda llamar la función de javaScript de abrir Dialog
                        ClientScript.RegisterStartupScript(this.GetType(), "LaunchServerSide", "<script>{$.notificacionMsj(\"" + obj_Login.gsiResultado + "\",\"" + obj_Login.gssMensaje + "\");fn_AbrirModales();}</script>");
                        /****************************************************************/
                    }
                }
                else
                {
                    //manda llamar la función de javaScript de abrir Dialog
                    ClientScript.RegisterStartupScript(this.GetType(), "LaunchServerSide", "<script>{$.notificacionMsj(\"" + obj_Login.gsiResultado + "\",\"" + obj_Login.gssMensaje + "\");fn_AbrirModales();}</script>");
                    /****************************************************************/
                }
                /*******************************************************************************************************************************************************************************************************/

            }
            //Si el captcha no es correcto
            else
            {
                /*se limpian las cajas de texto*/
                //atxtUsuario.Text = "";
                //atxtCaptcha.Text = "";
                /***********************************************************/

                //manda llamar la funcion de javaScript de abrir Dialog
                ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(2,'El código ingresado no es el correcto intenta nuevamente.');$('#hdvDialogCambioPass').modal('show');}</script>");
                /****************************************************************/
                /**********************************************************************************************************************************************************************************************/
            }

        }
        catch (Exception ex)
        {
            /*se limpian las cajas de texto*/
            //atxtUsuario.Text = "";
            //atxtCaptcha.Text = "";
            /***********************************************************/
            //manda llamar la funcion de javaScript de abrir Dialog
            ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(2,'Error al realizar cambio de contraseña: " + ex.Message + "');$('#hdvDialogCambioPass').modal('show');}</script>");
            /****************************************************************/
        }



    }
    #endregion

    #region Metodo para crear  la nueva contraseña
    /// <summary>
    /// Metodo para cambiar la contraseña 
    /// </summary>
    /// <returns>string nueva contraseña</returns>
    public string rsNuevaPassword()
    {
        //se crea random para realizar contraseña
        Random rRandom = new Random();
        //***********************************
        //se crea variable con caracteres que puedan estar en la contraseña
        string sCaracteres = "_$-#&!¡%012345679ACEFGHKLMNPRSWXZab_$-#&!¡%cdefghijkhlmnopqrstuvwxyz_$-#&!¡%";
        //****************
        //se declara stringbuilder para almacenar la contraseña
        StringBuilder sContrasena = new StringBuilder();
        //***************

        //ciclo para recorrer caracterres y agregarla a la contraseña
        for (int i = 0; i < 15; i++)
        {
            sContrasena.Append(sCaracteres[rRandom.Next(sCaracteres.Length)]);
        }
        //*********

        //se retorna la contraseña
        return sContrasena.ToString();
        //**************

    }
    #endregion

    #region Metodo Para cambiar contraseña
    protected void vCambiaPasswordInicio(object sender, EventArgs e)
    {
        //***********************************************************/
        //Instancias 
        //Clase Conexion
        Conexion obj_Conexion = new Conexion();
        //Clase Login_Datos
        LoginDatos obj_Login = new LoginDatos();
        //Clase de Security
        Security obj_secDatos;
        //Inicio TRY
        try
        {




            //se recuperan Valores Ingresados por el usuario
            //nombre de usuario
            string sUsuario = ahiddenNombre.Value;
            //contraseña
            string sPassword = txtCambioPassword.Text;
            //Accion
            string sAccion = ahiddenId.Value;
            //*********************************

            //se agrega el valor de los campos a los metodos set
            obj_Login.gssUsuario = sUsuario;
            obj_Login.gssPassword = sPassword;
            obj_Login.gsiAccion = Int32.Parse(sAccion);
            //*****
            obj_Login.vCambiaPassword(obj_Login);

            if (obj_Login.gsiResultado == 1)
            {
                obj_Login.vValidaUsuario(obj_Login);
                if (obj_Login.gsiResultado == 1)
                {
                    //manda llamar la funcion de javaScript de abrir alerta y redireccionar
                    ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(" + obj_Login.gsiResultado + ", \" " + obj_Login.gssMensaje + "\");setTimeout(function(){window.location.href='Vista/Inicio/Inicio.aspx';},0)}</script>");
                    /****************************************************************/
                    //se guardan las Variables de sesion
                    //Id de usuario
                    //se encripta
                    obj_secDatos = new Security(obj_Login.gsiIdUsuario.ToString());
                    Session["iIdUsuario"] = obj_secDatos.Encriptar();
                    //
                    //Tipo de Usuario
                    //se encripta
                    obj_secDatos = new Security(obj_Login.gsiTipoUsuario.ToString());
                    Session["iTipoUsuario"] = obj_secDatos.Encriptar();
                    //Nombre de Usuario
                    //se encripta
                    string NombreUsuario = obj_Login.gssNombre + " " + obj_Login.gssApePat + " " + obj_Login.gssApeMat;
                    obj_secDatos = new Security(NombreUsuario);
                    Session["NombreUsuario"] = obj_secDatos.Encriptar();

                    string a = Session["NombreUsuario"].ToString();

                    obj_secDatos = new Security(a);

                    string d = obj_secDatos.DesEncriptar();
                    //se redirecciona a la pagina de inicio
                    //Response.Redirect("Vista/Inicio/Inicio.aspx", false);
                    /**************************************************/
                }
                else if (obj_Login.gsiResultado == 4)
                {

                    //manda llamar la funcion de javaScript de abrir alerta
                    ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(" + obj_Login.gsiResultado + ", \" " + obj_Login.gssMensaje + "\");" +
                        "fn_abreDialogCambioPass('" + obj_Login.gssContenido + "','Cambio de contraseña','javaScript:alert(" + obj_Login.gsiIdUsuario + ")');$('#txtCambioPassword').val('');$('#atxtRepitePass').val('');}</script>");
                    /****************************************************************/

                    atxtUsuarioCambioPassword.Text = obj_Login.gssUsuario;
                    ahiddenId.Value = obj_Login.gsiAccion.ToString();
                }
                else
                {
                    //manda llamar la funcion de javaScript de abrir alerta
                    ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(" + obj_Login.gsiResultado + ", \" " + obj_Login.gssMensaje + "\");}</script>");
                    /****************************************************************/

                }
            }
            else
            {
                //manda llamar la funcion de javaScript de abrir alerta y redireccionar
                ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{$.notificacionMsj(" + obj_Login.gsiResultado + ", \" " + obj_Login.gssMensaje + "\");}</script>");
                /****************************************************************/
            }




        }
        /********/
        /*Inicio CATCH*/
        catch (Exception ex)
        {
            //manda llamar la funcion de javaScript de abrir alerta
            ClientScript.RegisterStartupScript(this.GetType(), "funcion", "<script>{ $.notificacionMsj(3, 'Lo sentimos, ha sucedido un error de tipo <b>EXEPCIÓN</b> comunícate con el departamento de TI, Error:" + ex.Message + "');}</script>");
            /****************************************************************/
        }
        /**********/

    }
    #endregion
    
}