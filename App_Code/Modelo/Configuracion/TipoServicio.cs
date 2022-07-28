using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

/// <summary>
/// Clase para los tipos de servicios
/// </summary>
public class TipoServicio
{
    //Constructir
    public TipoServicio() { 
    
    }
    //Atributos
    public int iIdAduana { get; set; }
    public int iIdServicio { get; set; }
    public int iIdClienteProveedor { get; set; }
    public int iIdClienteProveedorAduana { get; set; }
    public int iIdEstado { get; set; }
    public int iResultado { get; set; }
    public string sMensaje { get; set; }
    public int idConfiguracion { get; set; }
    public int idCuentaContable { get; set; }
    public string sCuentaContable { get; set; }

    /// <summary>
    /// Método para guardar tipo servicio oficina 
    /// </summary>
    /// <param name="objTipoServicio"></param>
    public void fn_GuardarTipoServicioOficina(TipoServicio objTipoServicio)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarTipoServicioAduana", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objTipoServicio.iIdAduana.ToString());
                objConexion.agregarParametroSP("@iIdServicio", SqlDbType.Int, objTipoServicio.iIdServicio.ToString());
                objConexion.agregarParametroSP("@iIdEstado", SqlDbType.Int, objTipoServicio.iIdEstado.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objTipoServicio.iResultado = 1;
                    objTipoServicio.sMensaje = "Servicio guardado con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTipoServicio.iResultado = 0;
                    objTipoServicio.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objTipoServicio.iResultado = 0;
                objTipoServicio.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para guardar tipo servicio clientes
    /// </summary>
    /// <param name="objTipoServicio"></param>
    public void fn_GuardarServicioClienteProveedor(TipoServicio objTipoServicio)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarServicioClienteProveedor", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdClienteProveedorAduana", SqlDbType.Int, objTipoServicio.iIdClienteProveedorAduana.ToString());
                objConexion.agregarParametroSP("@iIdServicio", SqlDbType.Int, objTipoServicio.iIdServicio.ToString());
                objConexion.agregarParametroSP("@iIdClienteProveedor", SqlDbType.Int, objTipoServicio.iIdClienteProveedor.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objTipoServicio.iResultado = 1;
                    objTipoServicio.sMensaje = "Se guardó con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTipoServicio.iResultado = 0;
                    objTipoServicio.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objTipoServicio.iResultado = 0;
                objTipoServicio.sMensaje = ex.Message;
            }
        }
    }
    /// <summary>
    /// Método para guardar tipo servicio clientes
    /// </summary>
    /// <param name="objTipoServicio"></param>
    public void fn_QuitarServicioClienteProveedor(TipoServicio objTipoServicio)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_QuitarServicioClienteProveedor", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdClienteProveedorAduana", SqlDbType.Int, objTipoServicio.iIdClienteProveedorAduana.ToString());
                objConexion.agregarParametroSP("@iIdServicio", SqlDbType.Int, objTipoServicio.iIdServicio.ToString());
                objConexion.agregarParametroSP("@iIdClienteProveedor", SqlDbType.Int, objTipoServicio.iIdClienteProveedor.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objTipoServicio.iResultado = 1;
                    objTipoServicio.sMensaje = "Se quitó con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTipoServicio.iResultado = 0;
                    objTipoServicio.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objTipoServicio.iResultado = 0;
                objTipoServicio.sMensaje = ex.Message;
            }
        }
    }

    public void fn_GuardarCC(TipoServicio objTipoServicio)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarCuentaContablePropios", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@sCuentaContable", SqlDbType.VarChar, objTipoServicio.sCuentaContable.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_STRING("@iResOut");
                if (sResOut[0] == "1")
                {
                    if (sResOut[1] == "1")
                    {
                        //Se retorna el mensaje de éxito
                        objTipoServicio.iResultado = 1;
                        objTipoServicio.sMensaje = "Se agregó correctamente la cuenta contable";
                    }
                    else
                    {
                        //Se retorna el mensaje de éxito
                        objTipoServicio.iResultado = 0;
                        objTipoServicio.sMensaje = sResOut[1];
                    }
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTipoServicio.iResultado = 0;
                    objTipoServicio.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objTipoServicio.iResultado = 0;
                objTipoServicio.sMensaje = ex.Message;
            }
        }
    }

    public void fn_CambiarCuentaContable(TipoServicio objTipoServicio)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_CambiarCuentaContablePropios", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdConfiguracion", SqlDbType.Int, objTipoServicio.idConfiguracion.ToString());
                objConexion.agregarParametroSP("@iIdCuentaContable", SqlDbType.Int, objTipoServicio.idCuentaContable.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_STRING("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objTipoServicio.iResultado = 1;
                    objTipoServicio.sMensaje = "Se actualizó correctamente la cuenta contable";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objTipoServicio.iResultado = 0;
                    objTipoServicio.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objTipoServicio.iResultado = 0;
                objTipoServicio.sMensaje = ex.Message;
            }
        }
    }
}
