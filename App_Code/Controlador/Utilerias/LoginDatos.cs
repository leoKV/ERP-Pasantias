using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.Data;

/// <summary>
/// Descripción breve de Login
/// </summary>
public class LoginDatos
{

    //variable y metodos de Usuario
    #region variable y metodos de Usuario
    //Tipo de acceso
    public int gsiTipoAcceso { set; get; }
    //***
    //iIdSubmenu
    public int gsiIdSubMenu { get; set; }
    //**
    //sIP
    public string gsiIP { get; set; }
    //Id Encriptada
    public int gsiIdUsuario { set; get; }
    //*************
    //Oficina
    public int gsiIdComitente { set; get; }
    //******************
    //Id Ip
    public int gsiIdIp { set; get; }
    //******************

    //Ip
    public string gssIp { set; get; }
    //******************

    //Descripcion
    public string gssDescripcion { set; get; }
    //******************

    //estado
    public int gsiEstado { set; get; }
    //******************
    //Dia
    public int gsiIdDia { set; get; }
    //******************

    //Hora fin
    public string gssHoraFin { set; get; }
    //******************
    //Hora Inicio
    public string gssHoraInicio { set; get; }
    //******************

    //Ultimo Cambio de Cpntraseña
    public string gssUltimoCambioContraseña { set; get; }

    //Cliente
    public int gsiIdCliente { set; get; }
    //******************

    //roles Usuario
    public List<string> lstRolesUsuario { set; get; }
    //*****
    /*Rol Usuario*/
    public int gsiIdRol { set; get; }
    /**/

    //Id Encriptada
    public string gssIdUsuarioEnc { set; get; }
    //*************
    //Nombre Usuario
    public string gssNombre { set; get; }
    //**************
    //Apellido Paterno
    public string gssApePat { set; get; }
    //**************
    //Apellido Materno
    public string gssApeMat { set; get; }
    //**************
    //Usuario
    public string gssUsuario { set; get; }
    //**************
    //Correo
    public string gssCorreo { set; get; }
    //**************
    //Estatus
    public int gsiEstaus { set; get; }
    //**************
    //Contraseña
    public string gssPassword { set; get; }
    //**************
    //Usuario Global
    public string gssUsuarioGlobal { set; get; }
    //**************
    //Usuario Trafico
    public string gssUsuarioTrafico { set; get; }
    //**************
    //Configuracion IP
    public int gsiConfigIP { set; get; }
    //**************
    //Configuracion Horario
    public int gsiConfigHorario { set; get; }
    //**************
    //Accion
    public int gsiAccion { set; get; }
    //**************
    /*Tipo de Usuario*/
    public int gsiTipoUsuario { set; get; }
    /***********/
    #endregion
    //*******

    //Variables y metodos resultado
    #region Variables y metodos resultado
    /// <summary>
    /// Variables get y set para saber el resultado de la accion
    /// </summary>
    public string gssContenido { set; get; }
    public int gsiResultado { set; get; }
    public string gssMensaje { set; get; }
    #endregion
    //*************



    //Metodo para Validar Usuario
    #region Metodo para validar Usuario
    //Metodo para Validar Usuario
    public void vValidaUsuario(LoginDatos obj_Login){
        //se declara variable de srespuesta
        string sRespuestaMensaje = "";
        //1 = exito,2=alerta,3=error,4=Cambio de Contraseña
        int iRespuestaResultado = 1;
        //***
        #region try
        //Inicio Try
        try
        {
            #region Instancias
            //Instancias 
            //Clase Conexion
            Conexion obj_conexion = new Conexion();
            //********
            #endregion
            #region Variables
            //declaracion de Variables
            //query para Recuperar Datos del Usuario
            string sQuery = "select idUsuario,nomUsuario,apPaterno,apMaterno,usuario,idEstatus,contrasenia from tUsuarios where usuario = '" + this.gssUsuario + "'";
            //Lista para recuperar Datos del Usuario
            DataTable lstDatosUsuario;
            //*******************
            #endregion
            #region se ejecuta la query recuperar Datos
            //se ejecuta la query y se recuperan los Datos
            lstDatosUsuario = obj_conexion.ejecutarConsultaRegistroMultiplesData(sQuery);
            //*****************
            #endregion
            //Se valida si la query se ejecuto correctamente
            /*if (lstDatosUsuario.Rows.Count>0)
            {*/
                #region Validacion de Usuario Existe
                //se valida si Hubo Datos encontrados 
                //si hay Datos existe el usuario
                if (lstDatosUsuario.Rows.Count > 0)
                {
                    //se recùpera la id del usuario
                    obj_Login.gsiIdUsuario = Int32.Parse(lstDatosUsuario.Rows[0]["idUsuario"].ToString());
                    #region Validacion de Usuario Baja Activo
                    if (lstDatosUsuario.Rows[0]["idEstatus"].ToString() != "2")
                    {
                        #region Validacion Contraseña
                        //se manda llamar al metodo de validar Contraseña
                        vValidaContraseña(obj_Login);
                        //*********************************************
                        //se valida la respuesta
                        if (obj_Login.gsiResultado == 1)
                        {
                            #region Validacion Cambio de Contraseña
                            //se valida si el usuario cambio Contraseña
                            if (lstDatosUsuario.Rows[0]["idEstatus"].ToString() != "3")
                            {
                                #region Validacion de Contraseña Expirada
                                //se manda llamar al metodo que valida expiracion de contraseña
                                //obj_Login.vValidaPasswordExpira(obj_Login);
                                //***************
                                if (obj_Login.gsiResultado == 1)
                                {
                                    //se manda mensaje de Acceso conseguido
                                    iRespuestaResultado = 1;
                                    sRespuestaMensaje = "Acceso Correcto. Que pase un buen día.";
                                    //se agrega el mensaje  y resultado Obtenidos
                                    obj_Login.gsiResultado = iRespuestaResultado;
                                    obj_Login.gssMensaje = sRespuestaMensaje;
                                    //*****************
                                    //SE ASIGNAN LOS DATOS DEL USUARIO A SU METODO CORRESPONDIENTE
                                    obj_Login.gsiIdUsuario = Int32.Parse(lstDatosUsuario.Rows[0]["idUsuario"].ToString());
                                    obj_Login.gssNombre = lstDatosUsuario.Rows[0]["nomUsuario"].ToString();
                                    obj_Login.gssApePat = lstDatosUsuario.Rows[0]["apPaterno"].ToString();
                                    obj_Login.gssApeMat = lstDatosUsuario.Rows[0]["apMaterno"].ToString();
                                    obj_Login.gssUsuario = lstDatosUsuario.Rows[0]["usuario"].ToString();
                                    obj_Login.gssPassword = lstDatosUsuario.Rows[0]["contrasenia"].ToString();
                                    //**************************
                                }
                                #endregion
                            }
                            //si lo tiene Cambio de contraseña
                            else
                            {
                                #region Cambio de Contraseña
                                //se manda mensaje de cambio de Contraseña
                                iRespuestaResultado = 4;
                                sRespuestaMensaje = "Cambio de Contraseña Obligatorio. Motivo = <b>Haz realizado un cambio de contraseña anteriormente</b>";
                                //se agrega Contenido
                                obj_Login.gssContenido = "<label>Haz solicitado anteriormente un cambio de contraseña, por tal motivo es necesario que realices  un cambio de la misma<label>";
                                //se agrega el mensaje  y resultado Obtenidos
                                obj_Login.gsiResultado = iRespuestaResultado;
                                obj_Login.gssMensaje = sRespuestaMensaje;
                                //*****************

                                //SE ASIGNAN LOS DATOS DEL USUARIO A SU METODO CORRESPONDIENTE
                                obj_Login.gsiIdUsuario = Int32.Parse(lstDatosUsuario.Rows[0]["iIdUsuario"].ToString());
                                obj_Login.gssNombre = lstDatosUsuario.Rows[0]["sNombreUsuario"].ToString();
                                obj_Login.gssApePat = lstDatosUsuario.Rows[0]["sApPaterno"].ToString();
                                obj_Login.gssApeMat = lstDatosUsuario.Rows[0]["sApMaterno"].ToString();
                                obj_Login.gssUsuario = lstDatosUsuario.Rows[0]["sUsuario"].ToString();
                                obj_Login.gssCorreo = lstDatosUsuario.Rows[0]["sCorreo"].ToString();
                                obj_Login.gssPassword = lstDatosUsuario.Rows[0]["sPassUsuario"].ToString();
                                obj_Login.gssUsuarioGlobal = lstDatosUsuario.Rows[0]["sUsuarioGlobal"].ToString();
                                obj_Login.gssUsuarioTrafico = lstDatosUsuario.Rows[0]["sUsuarioTrafico"].ToString();
                                obj_Login.gssUltimoCambioContraseña = lstDatosUsuario.Rows[0]["dUltimoCambioPass"].ToString();
                                obj_Login.gsiTipoUsuario = Int32.Parse(lstDatosUsuario.Rows[0]["iIdTipoUsuario"].ToString());
                                obj_Login.gsiAccion = 2;
                                //**************************
                                #endregion
                            }
                            //*******
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                    // si es 2 es baja
                    else
                    {
                        iRespuestaResultado = 2;
                        sRespuestaMensaje = "Lo sentimos el usuario ingresado <b>esta dado de baja</b>";
                        //se agrega el mensaje  y resultado Obtenidos
                        obj_Login.gsiResultado = iRespuestaResultado;
                        obj_Login.gssMensaje = sRespuestaMensaje;
                        //*****************
                    }
                    //************
                }
                //****************
                #endregion
                //si no hay Datos no existe el usuario
                else
                {
                    iRespuestaResultado = 2;
                    sRespuestaMensaje = "Lo sentimos el usuario Ingresado <b>No existe</b>";
                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gsiResultado = iRespuestaResultado;
                    obj_Login.gssMensaje = sRespuestaMensaje;
                    //*****************
                }
                //************
            /*}
            else
            {
                iRespuestaResultado = 3;
                sRespuestaMensaje = "Lo sentimos hubo un error al recuperar Datos del Usuario";
                //se agrega el mensaje  y resultado Obtenidos
                obj_Login.gsiResultado = iRespuestaResultado;
                obj_Login.gssMensaje = sRespuestaMensaje;
                //*****************
            }*/
            //**********
        }
        #endregion

        #region CATCH
        //Try 
        //Inicio Catch
        catch (Exception ex)
        {
            //se agrega resultado error y mensaje de la exepcion
            iRespuestaResultado = 3;
            sRespuestaMensaje = "Lo sentimos, a sucedido un error de tipo <b>EXEPCION</b> al momento de validar Usuario comunicate con el departamento de TI, Error:" + ex.Message + "'";
            //se agrega el mensaje  y resultado Obtenidos
            obj_Login.gsiResultado = iRespuestaResultado;
            obj_Login.gssMensaje = sRespuestaMensaje;
            //*****************
        }
        //Fin Catch
        #endregion
    }
    #endregion
    //*************



    //Metodo para Validar Contraseña del Usuario
    #region Metodo para Validar Contraseña del Usuario
    public void vValidaContraseña(LoginDatos obj_Login)
    {
        //se declara variable de srespuesta
        string sRespuestaMensaje = "";
        //1 = exito,2=alerta,3=error
        int iRespuestaResultado = 1;
        //***
        //Inicio TRY
        try
        {
            //Instancias
            //Clase conexion
            Conexion obj_conexion = new Conexion();
            //Clase Security
            Security obj_secPassword;
            //*************
            //Query para recuperar contraseña de usuario
            string sQuery = "select contrasenia from tUsuarios where idUsuario = " + obj_Login.gsiIdUsuario + "";
            //**********
            //se ejecuta la query y se recuperan en un arreglo 
            string[] sPasswordUsuario = obj_conexion.ejecutarConsultaRegistroSimple(sQuery);
            //*****************************************
            //se valida si la query se ejecuto correctamente
            if (sPasswordUsuario[0] == "1")
            {
                //se desencripta la contraseña
                //obj_secPassword = new Security(sPasswordUsuario[1]);
                obj_secPassword = new Security(sPasswordUsuario[1]);
                //***********
                string pass = obj_secPassword.DesEncriptar();
                //se valida la contraseña
                if (obj_Login.gssPassword == pass)
                {
                    //se agregan respuesta y mensaje
                    //se agrega resultado error y mensaje de la exepcion
                    iRespuestaResultado = 1;
                    sRespuestaMensaje = "Usuario validado con éxito <b> Acceso concedido</b>";
                    //********
                }
                else
                {
                    //se agregan respuesta y mensaje
                    //se agrega resultado error y mensaje de la exepcion
                    iRespuestaResultado = 2;
                    sRespuestaMensaje = "<b>La contraseña ingresada no es la correcta</b>";
                    //********
                }
            }
            else
            {
                //se agregan respuesta y mensaje
                //se agrega resultado error y mensaje de la exepcion
                iRespuestaResultado = 2;
                sRespuestaMensaje = "Lo sentimos, ha sucedido un error  al momento de <b>recuperar contraseña</b>";
                //********
            }
            //**********
            
        }
        //*
        //*Incio CATCH
        catch (Exception ex)
        {
            //se agregan respuesta y mensaje
            //se agrega resultado error y mensaje de la exepcion
            iRespuestaResultado = 3;
            sRespuestaMensaje = "Lo sentimos, ha sucedido un error de tipo <b>EXEPCIÓN</b> al momento de validar contraseña comunícate con el departamento de TI, Error:" + ex.Message + "'";
            //********
        }
        //*
        //se agrega el mensaje  y resultado Obtenidos
        obj_Login.gsiResultado = iRespuestaResultado;
        obj_Login.gssMensaje = sRespuestaMensaje;
        //*****************
    }
    #endregion
    //**********


    //Metodo para validar Horario del Usuario
    #region Metodo para validar Horario del Usuario
    public void vValidaHorario(LoginDatos obj_Login)
    {
        //Inicio Try
        try
        {
            //Instancias 
            //Clase Conexion
            Conexion obj_conexion = new Conexion();

            //query para recuperar resultado de la validacion
            string sQuery = "Exec pa_ValidaHorarioIP "+obj_Login.gsiIdUsuario+",' ',1";

            //se ejecuta la query y se recupera el resultado
            string[] sRespuesta = obj_conexion.ejecutarConsultaRegistroSimple(sQuery);

            //se valida si la query se ejecuto correctamente
            //si se ejecuto correctamente
            if (sRespuesta[0] == "1")
            {
                //se valida la respuesta 
                //si es 1 el usuario tiene la configuracion correcta
                if (sRespuesta[1] == "1")
                {
                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gsiResultado = 1;
                    obj_Login.gssMensaje = "Acceso correcto, Disfrute su dia";
                    //*****************
                }
                //si es 2 el usuario no tiene configuracion para ese dia
                else if (sRespuesta[1] == "2")
                {
                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gsiResultado = 2;
                    obj_Login.gssMensaje = "Lo sentimos no tienes Acceso, debido a que no tienes agendado este dia y/u hora a tu horario de sesión";
                    //*****************
                }
                //si es 3 el usuario no tiene configuraciones programadas
                else if (sRespuesta[1] == "3")
                {
                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gsiResultado = 2;
                    obj_Login.gssMensaje = "Lo sentimos no tienes Acceso, debido a que no tienes dias agendados a tu horario de sesión";
                    //*****************
                }
                //si no es ninguna de las anteriores es error
                else
                {
                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gsiResultado = 3;
                    obj_Login.gssMensaje = "Lo sentimos, ha sucedido un error de  al momento de validar tu horario de sesión, Error:" + sRespuesta[1] + "'";
                    //*****************
                }
            }
            //***
            //si hay error
            else
            {
                //se agrega el mensaje  y resultado Obtenidos
                obj_Login.gsiResultado = 3;
                obj_Login.gssMensaje = "Lo sentimos, ha sucedido un error de  al momento de validar tu horario de sesión, Error:" + sRespuesta[0] + "'";
                //*****************
            }
            //********
        }
        //****
        //Inicio Catch
        catch (Exception ex)
        {
            //se agrega el mensaje  y resultado Obtenidos
            obj_Login.gsiResultado = 3;
            obj_Login.gssMensaje = "Lo sentimos, ha sucedido un error de tipo <b>EXEPCIÓN</b> al momento de validar tu horario de sesión comunícate con el departamento de TI, Error:" + ex.Message + "'";
            //*****************
        }
        //***
    }
    #endregion
    //********

    //Metodo para validar Contraseña Expiro del Usuario
    #region Metodo para validar Contraseña Expirada
    public void vValidaPasswordExpira(LoginDatos obj_Login)
    {
        //Inicio Try
        try
        {
            //Instancias 
            //Clase Conexion
            Conexion obj_conexion = new Conexion();

            //query para recuperar resultado de la validacion
            string sQuery = "Exec pa_ValidaHorarioIP " + obj_Login.gsiIdUsuario + ",' ',3";

            //se ejecuta la query y se recupera el resultado
            string[] sRespuesta = obj_conexion.ejecutarConsultaRegistroSimple(sQuery);

            //se valida si la query se ejecuto correctamente
            //si se ejecuto correctamente
            if (sRespuesta[0] == "1")
            {
                //se valida la respuesta 
                //si es 1 el usuario tiene la configuracion correcta
                if (sRespuesta[1] == "1")
                {
                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gsiResultado = 1;
                    obj_Login.gssMensaje = "Acceso correcto, Disfrute su día";
                    //*****************
                }
                //si es 2 el usuario se expiro Su Contraseña
                else if (sRespuesta[1] == "2")
                {
                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gssContenido = "<label>Tu contraseña ha expirado, por tal motivo es necesario que realizes un cambío de la misma<label>";
                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gsiResultado = 4;
                    obj_Login.gssMensaje = "Cambio de Contraseña Obligatorio. Motivo = <b>Contraseña Expirada</b>";
                    obj_Login.gsiAccion = 3;
                    //*****************
                    //*****************
                }
                //si no es ninguna de las anteriores es error
                else
                {
                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gsiResultado = 3;
                    obj_Login.gssMensaje = "Lo sentimos, ha sucedido un error de  al momento de validar la expiración de contraseña , Error:" + sRespuesta[1] + "'";
                    //*****************
                }
            }
            //***
            //si hay error
            else
            {
                //se agrega el mensaje  y resultado Obtenidos
                obj_Login.gsiResultado = 3;
                obj_Login.gssMensaje = "Lo sentimos, ha sucedido un error de  al momento de validar la expiraciòn de contraseña, Error:" + sRespuesta[0] + "'";
                //*****************
            }
            //********
        }
        //****
        //Inicio Catch
        catch (Exception ex)
        {
            //se agrega el mensaje  y resultado Obtenidos
            obj_Login.gsiResultado = 3;
            obj_Login.gssMensaje = "Lo sentimos, ha sucedido un error de tipo <b>EXEPCIÓN</b> al momento de validar la expiraciòn de tu contraseña comunícate con el departamento de TI, Error:" + ex.Message + "'";
            //*****************
        }
        //***
    }
    #endregion
    //********

    //Metodo para validar IP del Usuario
    #region Metodo para validar IP del Usuario
    public void vValidaIP(LoginDatos obj_Login)
    {
        //Inicio Try
        try
        {
            //Instancias 
            //Clase Conexion
            Conexion obj_conexion = new Conexion();

            //query para recuperar resultado de la validacion
            string sQuery = "Exec pa_ValidaHorarioIP " + obj_Login.gsiIdUsuario + ",'"+obj_Login.gssIp+"',2";

            //se ejecuta la query y se recupera el resultado
            string[] sRespuesta = obj_conexion.ejecutarConsultaRegistroSimple(sQuery);

            //se valida si la query se ejecuto correctamente
            //si se ejecuto correctamente
            if (sRespuesta[0] == "1")
            {
                //se valida la respuesta 
                //si es 1 el usuario tiene la configuracion correcta
                if (sRespuesta[1] == "1")
                {
                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gsiResultado = 1;
                    obj_Login.gssMensaje = "Acceso correcto, Disfrute su dia";
                    //*****************
                }
                //si es 2 el usuario no tiene configuracion para ese dia
                else if (sRespuesta[1] == "2")
                {
                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gsiResultado = 2;
                    obj_Login.gssMensaje = "Lo sentimos no tienes Acceso, debido a que no tienes asignada esta IP a tu inicio de sesión ";
                    //*****************
                }
                //si es 3 el usuario no tiene configuraciones programadas
                else if (sRespuesta[1] == "3")
                {
                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gsiResultado = 2;
                    obj_Login.gssMensaje = "Lo sentimos no tienes Acceso, debido a que no tienes IP asignadas a tu inicio de sesión";
                    //*****************
                }
                //si no es ninguna de las anteriores es error
                else
                {
                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gsiResultado = 3;
                    obj_Login.gssMensaje = "Lo sentimos, ha sucedido un error de  al momento de validar tu horario de sesión, Error:" + sRespuesta[1] + "'";
                    //*****************
                }
            }
            //***
            //si hay error
            else
            {
                //se agrega el mensaje  y resultado Obtenidos
                obj_Login.gsiResultado = 3;
                obj_Login.gssMensaje = "Lo sentimos, ha sucedido un error de  al momento de validar tu horario de sesión, Error:" + sRespuesta[0] + "'";
                //*****************
            }
            //********
        }
        //****
        //Inicio Catch
        catch (Exception ex)
        {
            //se agrega el mensaje  y resultado Obtenidos
            obj_Login.gsiResultado = 3;
            obj_Login.gssMensaje = "Lo sentimos, ha sucedido un error de tipo <b>EXEPCIÓN</b> al momento de validar tu horario de sesión comunícate con el departamento de TI, Error:" + ex.Message + "'";
            //*****************
        }
        //***
    }
    #endregion
    //********
        
    //Metodo para realizar cambio de Contraseña
    #region Metodo para realizar cambio de Contraseña
    public void vCambiaPassword(LoginDatos obj_Login)
    {

        Conexion objConexion = new Conexion();
        Security obj_sePassword = new Security(obj_Login.gssPassword);
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("dbo.sp_GuardaCambioPassword", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros al SP desde el objeto
                objConexion.agregarParametroSP("@sUsuario", SqlDbType.VarChar, obj_Login.gssUsuario.ToString());
                objConexion.agregarParametroSP("@sPassword", SqlDbType.VarChar, obj_sePassword.Encriptar().ToString());
                //objConexion.agregarParametroSP("@sAccion", SqlDbType.Int, obj_Login.gsiAccion);


                sRes = objConexion.ejecutarProc();

                //Instancias 
                //clase Conexion
                //Conexion obj_Conexion = new Conexion();
                //clase Security

                //***************

                //se crea query para ejecutar el procedimiento de cambio de contraseña
                //string sQuery = "exec pa_GuardaCambioPassword '"+"','"++"',"++"";
                //*****

                //se ejecuta la query y se recupera el valor
                //string[] sRespuesta = obj_Conexion.ejecutarConsultaRegistroSimple(sQuery);
                //*****************

                //se valida que la query se ejecuto correctamente
                if (sRes == "1")
                {
                    if (sRes == "1")
                    {
                        //se agrega el mensaje  y resultado Obtenidos
                        obj_Login.gsiResultado = 1;
                        obj_Login.gssMensaje = "Se ha realizado el cambio de Contraseña";
                        //*****************
                    }
                    else if (sRes == "2")
                    {
                        //se agrega el mensaje  y resultado Obtenidos
                        obj_Login.gsiResultado = 2;
                        obj_Login.gssMensaje = "Lo sentimos el usuario Ingresado no existe o esta dado de baja";
                        //*****************
                    }
                    else
                    {
                        //se agrega el mensaje  y resultado Obtenidos
                        obj_Login.gsiResultado = 3;
                        obj_Login.gssMensaje = "Lo sentimos ha ocurrido un error al momento realizar el cambio de Contraseña, error:";// + sRespuesta[1];
                        //*****************
                    }
                }
                else
                {
                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gsiResultado = 3;
                    obj_Login.gssMensaje = "Lo sentimos ha ocurrido un error al realizar el cambio de Contraseña, error:";// +sRespuesta[0];
                    //*****************
                }


            }
            //**********
            //Inicio CATCH
            catch (Exception ex)
            {
                //se agrega el mensaje  y resultado Obtenidos
                obj_Login.gsiResultado = 3;
                obj_Login.gssMensaje = "Lo sentimos ha ocurrido un error al realizar el cambio de Contraseña, error:" + ex.Message;
                //*****************
            }
            //**********
        }
    }
    //
    #endregion
    //***************
    
    //Metodo para crear  el correo
    #region Metodo para enviar el correo con la nueva contraseña

    /// <summary>
    /// Metodo para enviar el correo con la nueva contraseña y actualizar
    /// </summary>
    /// <param name="usuario">Usuario a cambiar la contraseña</param>
    public void vGeneraCorreo(LoginDatos obj_Login)
    {

        //inicio try
        try
        {
            /*Instancia a la clase Conexion*/
            Conexion con = new Conexion();
            /********************************/


            // se crea nueva contraseña mandando llamar al metodo
            string sNuevaCon = "";
            //******************************************************************

            //se agrega la nueva contraseña al metodoset
            sNuevaCon = obj_Login.gssPassword;
            //************

            //variable de query para recuperar el correo
            string sRecuperaCorreo = "";
            /******************************************/

            /*query para recuperar el correo del usuario*/
            //sRecuperaCorreo = "select sCorreo from tb_Usuarios where sUsuario = '" + obj_Login.gssUsuario + "'";
            /*********************************************************************/


            /*se ejecuta la query de recuperar el correo*/
            string[] sCorreoRec = new string[1];
            sCorreoRec[0] = "antonio.aparicio@nadglobal.com";
            /**********************************************************************/

            /*Variables de cuerpo del email y el asunto*/
            string bodyMail; //variable para cuerpo de correo
            string subject_ = "Solicitud de Cambio de Contraseña"; ///Asunto del correo
            /********************************************************************************************/

            /*Encabezado del mail*/
            bodyMail = "<hr/>" +
                            "<span style='color:#1B6079;font-size: 0.9em;font-weight: bold;'>SOP</span>" +
                            "<br>" +
                            "<span style='color:#888C8D;'>Supervisión de Operaciones y Productividad</span>" +
                            "<div style='width: 100%;margin: 10px auto;background-color:#1D6688;color: #FFFFFF;font-weight: bold;" +
                            "text-align: center;padding: 1px 0px !important;position: relative;font-size: 0.8em;text-transform: uppercase;clear: both;'>" +
                                "Cambio de Contraseña Usuario SOP" +
                            "</div>";
            /******************************************************************************************************************************************************/

            /*Cuerpo del mensaje*/
            bodyMail += "<table style='width:100%;'>" +
                            "<tr style='border-right:2px solid #000'>" +
                                "<td style='color:#225069'>Usuario:</td>" +
                                "<td style='font-style: italic;font-size: 0.9em;'>" + obj_Login.gssUsuario + "</td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td style='color:#225069'>Nueva Contraseña:</td>" +
                                "<td style='font-style: italic;font-size: 0.9em;'>" + sNuevaCon + "</td>" +
                            "</tr>" +
                        "</table>";
            /***********************************************************************************************************/

            /*Pie del mensaje con lista de pasos para terminar el proceso de cambio de contraseña*/
            bodyMail += "<br/><br/><div style='font-style: italic;font-size: 0.9em;'>Cambio de Contraseña</div>";
            bodyMail += "<br/><div style='font-style: italic;font-size: 0.9em;'>Haz solicitado un cambio de contraseña. Para poder completar este proceso, favor de seguir los siguientes pasos:" +
                            "<ol>" +
                                "<li>Abre el navegador de internet e ingresa la dirección del sistema.</li>" +
                                "<li>Ingresa tu usuario y la contraseña que se te asigno y envio a tu correo.</li>" +
                                "<li>Una vez que se ingresaron tus datos, deberas hacer clic en el boton 'ENTRAR'.</li>" +
                                "<li>El sistema pedirá un cambio de contraseña, siguiendo un formato establecido:" +
                                    "<ul>" +
                                        "<li>Minimo 8 digitos</li>" +
                                        "<li>Minimo una letra minuscula</li>" +
                                        "<li>Minimo una letra mayuscula</li>" +
                                        "<li>Minimo un nùmero</li>" +
                                        "<li>Minimo un caracter especial</li>"
                                    + "</ul>"
                                + "</li>" +
                                "<li>Listo tu solicitud ha sido realizada.</li>" +
                            "</ol>" +
                            "<label style='color:#1D6688'><b>(Para cualquier aclaración comunicate con el administrador)</b></label></div>";
            /***********************************************************************************************************************************************************/
            bool a = true;
            //se valida si se ejecuto correcto el envio
            //if (sCorreoRec[0] == "1" && sCorreoRec[1] != "")
            if(a)
            {
                ///Se hace llamada a Clase Email para mandar notificacion
                try
                {


                    ///Se envio correo
                    Email correo = new Email(subject_);
                    correo.fn_IniciarConfiguracionCorreo(true, System.Net.Mail.MailPriority.High);
                    ///se agregar a DataTable lista de registros "correos"
                    ///Agrega correo de usuario
                    string prueba = "antonio.aparicio@nadglobal.com";
                    correo._AddTo(prueba);
                    /// 
                    /*Se agrega el cuerpo al mail*/
                    correo._AddBody(bodyMail);
                    /*****************************/
                    /*Se envia el email*/
                    correo.sendMail();
                    /*********************/
                    /*************************************************************************************************************************************************************************************************/
                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gsiResultado = 1;
                    obj_Login.gssMensaje = "Se ha realizado el envio de correo";
                    //*****************

                }
                catch (Exception ex)
                {
                    ///SI OCURRE ERROR RETORNA ERROR 
                    ///se declara la vaiable del error
                    string body = "Error al realizar el envió de correo: " + ex.Message;
                    /************************************************************************************/

                    /*se envia correo al administrador*/
                    Email correo = new Email("Error Enviar correo Cambio de contraseña");
                    correo.fn_IniciarConfiguracionCorreo(true,System.Net.Mail.MailPriority.Normal);
                    correo._AddTo(/*"it.querataro@nadglobal.com"*/"vguerrero@lcamx.com");
                    correo._AddBody(body);
                    correo.sendMail();
                    /*****************************************************************************************************************/

                    //se agrega el mensaje  y resultado Obtenidos
                    obj_Login.gsiResultado = 3;
                    obj_Login.gssMensaje = "Error al realizar el envió de correo: " + ex.Message;
                    //*****************
                }
            }
            else
            {
                //se agrega el mensaje  y resultado Obtenidos
                obj_Login.gsiResultado = 3;
                obj_Login.gssMensaje = "Error al realizar el envió de correo";
                //*****************
            }
            //********
        }
        catch (Exception ex)
        {
            //se agrega el mensaje  y resultado Obtenidos
            obj_Login.gsiResultado = 3;
            obj_Login.gssMensaje = "Error al realizar el envió de correo: " + ex.Message;
            //*****************
        }
    }
    #endregion
    //******

    public LoginDatos()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}