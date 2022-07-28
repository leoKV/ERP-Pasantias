using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

/// <summary>
/// Descripción breve de Usuario
/// </summar
public class Usuario
{
    public Usuario() { }

    //Se declaran los atributos de oficina
    public int iIdAccion { get; set; }
    public int iIdUsuarioAccion { get; set; }
    public string sIdUsuarioAccion { get; set; }
    public int iIdUsuario { get; set; }
    public string sIdUsuario { get; set; }
    public string sNombreUsuario { get; set; }
    public string sApellidoPaterno { get; set; }
    public string sApellidoMaterno { get; set; }
    public string sUsuario { get; set; }
    public string sContraseña { get; set; }
    public int iIdEstatus { get; set; }
    public string sCorreoElectronico { get; set; }
    public int iIdComitente { get; set; }
    public int iIdAduana { get; set; }
    public int iIdRol { get; set; }
    public int iResultado { get; set; }
    public string sMensaje { get; set; }
    public int iIdTipoUsuario { get; set; }

    /// <summary>
    /// Método para validar que la oficina no exista
    /// </summary>
    /// ///<param name="objUsuario"></param>
    public void fn_validar_usuario_existe(Usuario objUsuario)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string andAccion =  (objUsuario.iIdAccion == 0 ? " " : " and idUsuario != "+objUsuario.iIdUsuario+"");
        string sQuery = "SELECT COUNT(*) FROM tUsuarios WHERE usuario='" + objUsuario.sUsuario + "'" + andAccion;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objUsuario.iResultado = int.Parse(sRes[1]);
        //Se retorna mensaje en caso de que exista la oficina
        objUsuario.sMensaje = "El usuario ya existe.";
    }


    /// <summary>
    /// Método para guardar usuario
    /// </summary>
    /// ///<param name="objUsuario"></param>
    public void fn_guardar_usuario(Usuario objUsuario)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarUsuario", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdUsuarioAccion", SqlDbType.Int, objUsuario.iIdUsuarioAccion.ToString());
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, objUsuario.iIdUsuario.ToString());
                objConexion.agregarParametroSP("@sNombreUsuario", SqlDbType.VarChar, objUsuario.sNombreUsuario);
                objConexion.agregarParametroSP("@sApellidoPaterno", SqlDbType.VarChar, objUsuario.sApellidoPaterno);
                objConexion.agregarParametroSP("@sApellidoMaterno", SqlDbType.VarChar, objUsuario.sApellidoMaterno);
                objConexion.agregarParametroSP("@sUsuario", SqlDbType.VarChar, objUsuario.sUsuario);
                objConexion.agregarParametroSP("@sContraseña", SqlDbType.VarChar, objUsuario.sContraseña);
                objConexion.agregarParametroSP("@iIdEstatus", SqlDbType.Int, objUsuario.iIdEstatus.ToString());
                objConexion.agregarParametroSP("@sCorreoElectronico", SqlDbType.VarChar, objUsuario.sCorreoElectronico);
                objConexion.agregarParametroSP("@iIdTipoUsuario", SqlDbType.Int, objUsuario.iIdTipoUsuario.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objUsuario.iResultado = 1;
                    objUsuario.sMensaje = "Usuario guardado con éxito.";
                    objUsuario.iIdUsuario = int.Parse(sResOut[1]);
                }
                else
                {
                    //Se retorna el mensaje de error
                    objUsuario.iResultado = 0;
                    objUsuario.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objUsuario.iResultado = 0;
                objUsuario.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para obtener datos del usuario
    /// </summary>
    /// ///<param name="objUsuario"></param>
    public void fn_obtener_datos_usuario(Usuario objUsuario)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sNombreUsuario", "sApellidoPaterno", "sApellidoMaterno", "sUsuario", "sContraseña", "iIdEstatus", "sCorreoElectronico", "iIdTipoUsuario" };
        //Se crea la consulta
        string sQuery = "select nomUsuario as sNombreUsuario, apPaterno as sApellidoPaterno, apMaterno as sApellidoMaterno, usuario as sUsuario, [dbo].[fn_DecodeB64](contrasenia) as sContraseña, idEstatus as iIdEstatus, correoElectronico as sCorreoElectronico, idTipoUsuario as iIdTipoUsuario from tUsuarios WHERE idUsuario=" + objUsuario.iIdUsuario;
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<Usuario>(sQuery, arrAtributos, objUsuario);
        //Se asigna el resultado
        objUsuario.iResultado = 1;
    }

    /// <summary>
    /// Método para actualizar estado usuario
    /// </summary>
    /// ///<param name="objUsuario"></param>
    public void fn_cambia_estado_usuario(Usuario objUsuario)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_CambiaEstadoUsuario", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdUsuarioAccion", SqlDbType.Int, objUsuario.iIdUsuarioAccion.ToString());
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, objUsuario.iIdUsuario.ToString());
                objConexion.agregarParametroSP("@iIdEstatus", SqlDbType.Int, objUsuario.iIdEstatus.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objUsuario.iResultado = 1;
                    objUsuario.sMensaje = "Usuario actualizado con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objUsuario.iResultado = 0;
                    objUsuario.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objUsuario.iResultado = 0;
                objUsuario.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para asignar usuario a oficina
    /// </summary>
    /// ///<param name="objUsuario"></param>
    public void fn_asignar_usuario_comitente(Usuario objUsuario)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_AsignarUsuarioComitente", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdUsuarioAccion", SqlDbType.Int, objUsuario.iIdUsuarioAccion.ToString());
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, objUsuario.iIdUsuario.ToString());
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objUsuario.iIdComitente.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objUsuario.iResultado = 1;
                    objUsuario.sMensaje = "Comitente asignada con éxito.";
                    objUsuario.iIdUsuario = int.Parse(sResOut[1]);
                }
                else
                {
                    //Se retorna el mensaje de error
                    objUsuario.iResultado = 0;
                    objUsuario.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objUsuario.iResultado = 0;
                objUsuario.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para asignar usuario a comitente
    /// </summary>
    /// ///<param name="objUsuario"></param>
    public void fn_quita_usuario_comitente(Usuario objUsuario)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_QuitaUsuarioComitente", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdUsuarioAccion", SqlDbType.Int, objUsuario.iIdUsuarioAccion.ToString());
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, objUsuario.iIdUsuario.ToString());
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objUsuario.iIdComitente.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objUsuario.iResultado = 1;
                    objUsuario.sMensaje = "Comitente eliminada con éxito.";
                    objUsuario.iIdUsuario = int.Parse(sResOut[1]);
                }
                else
                {
                    //Se retorna el mensaje de error
                    objUsuario.iResultado = 0;
                    objUsuario.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objUsuario.iResultado = 0;
                objUsuario.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para asignar aduana a oficina
    /// </summary>
    /// ///<param name="objUsuario"></param>
    public void fn_guardar_aduana_comitente(Usuario objUsuario)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarAduanaComitenteUsuario", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdUsuarioAccion", SqlDbType.Int, objUsuario.iIdUsuarioAccion.ToString());
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, objUsuario.iIdUsuario.ToString());
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objUsuario.iIdComitente.ToString());
                objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objUsuario.iIdAduana.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objUsuario.iResultado = 1;
                    objUsuario.sMensaje = "Aduana guardada con éxito.";
                    objUsuario.iIdUsuario = int.Parse(sResOut[1]);
                }
                else
                {
                    //Se retorna el mensaje de error
                    objUsuario.iResultado = 0;
                    objUsuario.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objUsuario.iResultado = 0;
                objUsuario.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para quitar aduana a comitente
    /// </summary>
    /// ///<param name="objUsuario"></param>
    public void fn_quitar_aduana_comitente(Usuario objUsuario)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_QuitarAduanaComitente", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdUsuarioAccion", SqlDbType.Int, objUsuario.iIdUsuarioAccion.ToString());
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, objUsuario.iIdUsuario.ToString());
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objUsuario.iIdComitente.ToString());
                objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objUsuario.iIdAduana.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objUsuario.iResultado = 1;
                    objUsuario.sMensaje = "Aduana eliminada con éxito.";
                    objUsuario.iIdUsuario = int.Parse(sResOut[1]);
                }
                else
                {
                    //Se retorna el mensaje de error
                    objUsuario.iResultado = 0;
                    objUsuario.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objUsuario.iResultado = 0;
                objUsuario.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para asignar rol a comitente
    /// </summary>
    /// ///<param name="objUsuario"></param>
    public void fn_guardar_rol_comitente(Usuario objUsuario)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarRolComitente", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdUsuarioAccion", SqlDbType.Int, objUsuario.iIdUsuarioAccion.ToString());
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, objUsuario.iIdUsuario.ToString());
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objUsuario.iIdComitente.ToString());
                objConexion.agregarParametroSP("@iIdRol", SqlDbType.Int, objUsuario.iIdRol.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objUsuario.iResultado = 1;
                    objUsuario.sMensaje = "Rol guardado con éxito.";
                    objUsuario.iIdUsuario = int.Parse(sResOut[1]);
                }
                else
                {
                    //Se retorna el mensaje de error
                    objUsuario.iResultado = 0;
                    objUsuario.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objUsuario.iResultado = 0;
                objUsuario.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para quitar rol a comitente
    /// </summary>
    /// ///<param name="objUsuario"></param>
    public void fn_quitar_rol_comitente(Usuario objUsuario)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_QuitarRolComitente", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdUsuarioAccion", SqlDbType.Int, objUsuario.iIdUsuarioAccion.ToString());
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, objUsuario.iIdUsuario.ToString());
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objUsuario.iIdComitente.ToString());
                objConexion.agregarParametroSP("@iIdRol", SqlDbType.Int, objUsuario.iIdRol.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objUsuario.iResultado = 1;
                    objUsuario.sMensaje = "Rol eliminado con éxito.";
                    objUsuario.iIdUsuario = int.Parse(sResOut[1]);
                }
                else
                {
                    //Se retorna el mensaje de error
                    objUsuario.iResultado = 0;
                    objUsuario.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objUsuario.iResultado = 0;
                objUsuario.sMensaje = ex.Message;
            }
        }
    }
    
}
