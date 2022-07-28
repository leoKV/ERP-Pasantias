using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

public class Proveedor
{
    //constructor
    public Proveedor() { 
    
    }
    //atributos de la clase
    public int iIdProveedor { get; set; }
    public int iIdComitente { get; set; }
    public int iIdCliente { get; set; }
    public int iIdAduana { get; set; }
    public int iIdServicio { get; set; }
    public int iResultado { get; set; }
    public string sMensaje { get; set; }
    public string sNomProveedor { get; set; }
    public string sRfc { get; set; }
    public string sCalle { get; set; }
    public string sNoInterior { get; set; }
    public string sNoExterior { get; set; }
    public string sCodPostal { get; set; }
    public string sNomPais { get; set; }
    public string sNomCiudad { get; set; }


    /// <summary>
    /// Método para guardar proveedor comitente
    /// </summary>
    /// <param name="objProveedor"></param>
    public void fn_GuardarProveedorComitente(Proveedor objProveedor)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarProveedorComitente", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objProveedor.iIdComitente.ToString());
                objConexion.agregarParametroSP("@iIdProveedor", SqlDbType.Int, objProveedor.iIdProveedor.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objProveedor.iResultado = 1;
                    objProveedor.sMensaje = "Comitente guardado con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objProveedor.iResultado = 0;
                    objProveedor.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objProveedor.iResultado = 0;
                objProveedor.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para quitar proveedor comitente
    /// </summary>
    /// <param name="objProveedor"></param>
    public void fn_QuitarProveedorComitente(Proveedor objProveedor)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_QuitarProveedorComitente", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objProveedor.iIdComitente.ToString());
                objConexion.agregarParametroSP("@iIdProveedor", SqlDbType.Int, objProveedor.iIdProveedor.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objProveedor.iResultado = 1;
                    objProveedor.sMensaje = "Comitente eliminado con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objProveedor.iResultado = 0;
                    objProveedor.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objProveedor.iResultado = 0;
                objProveedor.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para guardar proveedor cliente
    /// </summary>
    /// <param name="objProveedor"></param>
    public void fn_GuardarProveedorCliente(Proveedor objProveedor)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarClienteProveedor", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdProveedor", SqlDbType.Int, objProveedor.iIdProveedor.ToString());
                objConexion.agregarParametroSP("@iIdCliente", SqlDbType.Int, objProveedor.iIdCliente.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objProveedor.iResultado = 1;
                    objProveedor.sMensaje = "Cliente guardado con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objProveedor.iResultado = 0;
                    objProveedor.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objProveedor.iResultado = 0;
                objProveedor.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para quitar proveedor cliente
    /// </summary>
    /// <param name="objProveedor"></param>
    public void fn_QuitarProveedorCliente(Proveedor objProveedor)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_QuitarClienteProveedor", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdProveedor", SqlDbType.Int, objProveedor.iIdProveedor.ToString());
                objConexion.agregarParametroSP("@iIdCliente", SqlDbType.Int, objProveedor.iIdCliente.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objProveedor.iResultado = 1;
                    objProveedor.sMensaje = "Cliente eliminado con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objProveedor.iResultado = 0;
                    objProveedor.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objProveedor.iResultado = 0;
                objProveedor.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para guardar proveedor aduana
    /// </summary>
    /// <param name="objProveedor"></param>
    public void fn_GuardarProveedorAduana(Proveedor objProveedor)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarProveedorAduana", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objProveedor.iIdAduana.ToString());
                objConexion.agregarParametroSP("@iIdProveedor", SqlDbType.Int, objProveedor.iIdProveedor.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objProveedor.iResultado = 1;
                    objProveedor.sMensaje = "Aduana guardada con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objProveedor.iResultado = 0;
                    objProveedor.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objProveedor.iResultado = 0;
                objProveedor.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para quitar proveedor cliente
    /// </summary>
    /// <param name="objProveedor"></param>
    public void fn_QuitarProveedorAduana(Proveedor objProveedor)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_QuitarProveedorAduana", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objProveedor.iIdAduana.ToString());
                objConexion.agregarParametroSP("@iIdProveedor", SqlDbType.Int, objProveedor.iIdProveedor.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objProveedor.iResultado = 1;
                    objProveedor.sMensaje = "Aduana eliminada con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objProveedor.iResultado = 0;
                    objProveedor.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objProveedor.iResultado = 0;
                objProveedor.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para guardar proveedor aduana servicio
    /// </summary>
    /// <param name="objProveedor"></param>
    public void fn_GuardarProveedorServicio(Proveedor objProveedor)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarProveedorServicio", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objProveedor.iIdAduana.ToString());
                objConexion.agregarParametroSP("@iIdServicio", SqlDbType.Int, objProveedor.iIdServicio.ToString());
                objConexion.agregarParametroSP("@iIdProveedor", SqlDbType.Int, objProveedor.iIdProveedor.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objProveedor.iResultado = 1;
                    objProveedor.sMensaje = "Servicio guardado con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objProveedor.iResultado = 0;
                    objProveedor.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objProveedor.iResultado = 0;
                objProveedor.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para guardar proveedor aduana servicio
    /// </summary>
    /// <param name="objProveedor"></param>
    public void fn_QuitarProveedorServicio(Proveedor objProveedor)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_QuitarProveedorServicio", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objProveedor.iIdAduana.ToString());
                objConexion.agregarParametroSP("@iIdServicio", SqlDbType.Int, objProveedor.iIdServicio.ToString());
                objConexion.agregarParametroSP("@iIdProveedor", SqlDbType.Int, objProveedor.iIdProveedor.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objProveedor.iResultado = 1;
                    objProveedor.sMensaje = "Servicio eliminado con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objProveedor.iResultado = 0;
                    objProveedor.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objProveedor.iResultado = 0;
                objProveedor.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para guardar proveedor aduana servicio
    /// </summary>
    /// <param name="objProveedor"></param>
    public void fn_QuitarProveedor(Proveedor objProveedor)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        
        try
        {
            string sQuery = "update tProveedor set idEstatus = 2 where idProveedor = " + objProveedor.iIdProveedor + "";
            string msg = objConexion.ejecutarComando(sQuery);
            if (msg == "1")
            {
                //Se retorna el mensaje de éxito
                objProveedor.iResultado = 1;
                objProveedor.sMensaje = "Servicio eliminado con éxito";
            }
            else {
                objProveedor.iResultado = 0;
                objProveedor.sMensaje = msg;
            }
        }
        catch (Exception ex)
        {
            //Se guarda el mensaje de error
            objProveedor.iResultado = 0;
            objProveedor.sMensaje = ex.Message;
        }
    }

    /// <summary>
    /// Método para obtener datos del proveedor
    /// </summary>
    /// <param name="objProveedor"></param>
    public void fn_ObtenerDatosProveedor(Proveedor objProveedor)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sNomProveedor", "sRfc", "sCalle", "sNoInterior", "sNoExterior", 
                                  "sCodPostal","sNomPais","sNomCiudad" };
        //Se crea la consulta
        string sQuery = "select nomProveedor sNomProveedor,rfc sRfc,calle sCalle,noInterior sNoInterior,"+
                        " noExterior sNoExterior,codPostal sCodPostal,c_pa.nomPais sNomPais, c_ci.nomCiudad sNomCiudad"+
                        " from tProveedor t_pr "+
                        " left join cPais c_pa on t_pr.idPais = c_pa.idPais"+
                        " left join cCiudad c_ci on t_pr.idCiudad = c_ci.idCiudad"+
                        " where t_pr.idProveedor = "+objProveedor.iIdProveedor+"";
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<Proveedor>(sQuery, arrAtributos, objProveedor);
        //Se asigna el resultado
        objProveedor.iResultado = 1;
    }

}
