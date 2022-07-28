using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Aduana
/// </summary>
public class Aduana
{
	public Aduana()	{ }

    //Se declaran los atributos de aduana
    public int iIdAduana { get; set; }
    public string sIdAduana { get; set; }
    public string sAduana { get; set; }
    public string sDenominacion { get; set; }
    public int iIdEstatus { get; set; }
    public int iIdCliente { get; set; }
    public int iIdProveedor { get; set; }
    //Se declaran atributos generales
    public int iResultado { get; set; }
    public string sMensaje { get; set; }
    public string sContenido { get; set; }
    public int iIdUsuario { get; set; }

    /// <summary>
    /// Método para validar que la aduana no exista
    /// </summary>
    /// <param name="objAduana"></param>
    public void fn_ValidarAduanaExiste(Aduana objAduana)
    {
        if (objAduana.iIdAduana == 0)
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //sQuery
            string sQuery = "SELECT COUNT(*) FROM cAduana ca WHERE ca.aduana='" + objAduana.sAduana + "'";
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            //Se retorna el iResultado 
            objAduana.iResultado = int.Parse(sRes[1]);
            //Se retorna mensaje en caso de que exista la aduana
            objAduana.sMensaje = "La aduana ya existe.";
        }
        else
        {
            //Se retorna el iResultado 
            objAduana.iResultado = 0;
        }
    }

    /// <summary>
    /// Método para guardar aduana
    /// </summary>
    /// <param name="objAduana"></param>
    public void fn_GuardarAduana(Aduana objAduana)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarAduana", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objAduana.iIdAduana.ToString());
                objConexion.agregarParametroSP("@sAduana", SqlDbType.VarChar, objAduana.sAduana);
                objConexion.agregarParametroSP("@sDenominacion", SqlDbType.VarChar, objAduana.sDenominacion);
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, objAduana.iIdUsuario.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objAduana.iResultado = 1;
                    objAduana.sMensaje = "Aduana guardada con éxito.";
                    objAduana.iIdAduana = int.Parse(sResOut[1]);
                }
                else
                {
                    //Se retorna el mensaje de error
                    objAduana.iResultado = 0;
                    objAduana.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objAduana.iResultado = 0;
                objAduana.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para eliminar aduana
    /// </summary>
    /// <param name="objAduana"></param>
    public void fn_EliminarAduana(Aduana objAduana)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_EliminarAduana", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objAduana.iIdAduana.ToString());
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, objAduana.iIdUsuario.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objAduana.iResultado = 1;
                    objAduana.sMensaje = "Aduana eliminada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objAduana.iResultado = 0;
                    objAduana.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objAduana.iResultado = 0;
                objAduana.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para validar que la cliente aduana no exista
    /// </summary>
    /// <param name="objAduana"></param>
    public void fn_ValidarClienteAduanaExiste(Aduana objAduana)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "SELECT COUNT(*) FROM tClienteAduana tca WHERE tca.idCliente=" + objAduana.iIdCliente + " and tca.idAduana=" + objAduana.iIdAduana;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objAduana.iResultado = int.Parse(sRes[1]);
        //Se retorna mensaje en caso de que el ciente ya este asignado
        objAduana.sMensaje = "El cliente ya está asignado a la aduana.";
    }

    /// <summary>
    /// Método para guardar el cliente de la aduana
    /// </summary>
    /// <param name="objAduana"></param>
    public void fn_GuardarClienteAduana(Aduana objAduana)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarClienteAduana", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objAduana.iIdAduana.ToString());
                objConexion.agregarParametroSP("@iIdCliente", SqlDbType.Int, objAduana.iIdCliente.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objAduana.iResultado = 1;
                    objAduana.sMensaje = "Cliente guardado con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objAduana.iResultado = 0;
                    objAduana.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objAduana.iResultado = 0;
                objAduana.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para validar que la proveedor aduana no exista
    /// </summary>
    /// <param name="objAduana"></param>
    public void fn_ValidarProveedorAduanaExiste(Aduana objAduana)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "SELECT COUNT(*) FROM tProveedorAduana tpa WHERE tpa.idProveedor=" + objAduana.iIdProveedor + " and tpa.idAduana=" + objAduana.iIdAduana;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objAduana.iResultado = int.Parse(sRes[1]);
        //Se retorna mensaje en caso de que el proveedor ya este asignado
        objAduana.sMensaje = "El proveedor ya está asignado a la aduana.";
    }

    /// <summary>
    /// Método para guardar el proveedor de la aduana
    /// </summary>
    /// <param name="objAduana"></param>
    public void fn_GuardarProveedorAduana(Aduana objAduana)
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
                objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objAduana.iIdAduana.ToString());
                objConexion.agregarParametroSP("@iIdProveedor", SqlDbType.Int, objAduana.iIdProveedor.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objAduana.iResultado = 1;
                    objAduana.sMensaje = "Proveedor guardado con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objAduana.iResultado = 0;
                    objAduana.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objAduana.iResultado = 0;
                objAduana.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para obtener datos de la aduana
    /// </summary>
    /// <param name="objAduana"></param>
    public void fn_ObtenerDatosAduana(Aduana objAduana)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sAduana", "sDenominacion" };
        //Se crea la consulta
        string sQuery = "SELECT ca.aduana AS sAduana, ca.denominacion AS sDenominacion FROM cAduana ca WHERE ca.idAduana=" + objAduana.iIdAduana;
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<Aduana>(sQuery, arrAtributos, objAduana);
        //Se asigna el resultado
        objAduana.iResultado = 1;
    }

    /// <summary>
    /// Método para eliminar cliente de la aduana
    /// </summary>
    /// <param name="objAduana"></param>
    public void fn_EliminarClienteAduana(Aduana objAduana)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery
        string sQuery = "DELETE FROM tClienteAduana WHERE idCliente=" + objAduana.iIdCliente + " and idAduana=" + objAduana.iIdAduana;
        string sRes = objConexion.ejecutarComando(sQuery);
        //Se verifica el resultado
        if (sRes == "1")
        {
            //Se retorna el sResultado 
            objAduana.iResultado = 1;
            //Se retorna mensaje de éxito
            objAduana.sMensaje = "Cliente eliminado con éxito.";
        }
        else
        {
            //Se retorna el sResultado 
            objAduana.iResultado = 0;
            //Se retorna mensaje de error
            objAduana.sMensaje = sRes;
        }
    }

    /// <summary>
    /// Método para eliminar proveedor de la aduana
    /// </summary>
    /// <param name="objAduana"></param>
    public void fn_EliminarProveedorAduana(Aduana objAduana)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery
        string sQuery = "DELETE FROM tProveedorAduana WHERE idProveedor=" + objAduana.iIdProveedor + " and idAduana=" + objAduana.iIdAduana;
        string sRes = objConexion.ejecutarComando(sQuery);
        //Se verifica el resultado
        if (sRes == "1")
        {
            //Se retorna el sResultado 
            objAduana.iResultado = 1;
            //Se retorna mensaje de éxito
            objAduana.sMensaje = "Proveedor eliminado con éxito.";
        }
        else
        {
            //Se retorna el sResultado 
            objAduana.iResultado = 0;
            //Se retorna mensaje de error
            objAduana.sMensaje = sRes;
        }
    }

}