using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Descripción breve de Oficina
/// </summary>
public class Oficina
{
	public Oficina()
	{
	}

    // se declaran los atributos

    public int iIdOficina { get; set; }
    public string sIdOficina { get; set; }
    public string sOficina { get; set; }
    public string sDireccion { get; set; }
    public string sCodigoPostal { get; set; }
    public string sCorreoElectronico { get; set; }
    public int iIdAduana { get; set; }
    public int iIdInstancia { get; set; }
    public string sCorreoNG { get; set; }
    public string sCorreoCliente { get; set; }


    //Se declaran atributos generales
    public int iResultado { get; set; }
    public string sMensaje { get; set; }
    public string sContenido { get; set; }
    public int iIdUsuario { get; set; }


    /// <summary>
    /// Método para validar que la oficina no exista
    /// </summary>
    /// <param name="objOficina"></param>
    public void fn_ValidarOficinaExiste(Oficina objOficina)
    {
        if (objOficina.iIdOficina == 0)
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //sQuery
            string sQuery = "SELECT COUNT(*) FROM tOficina ofi WHERE ofi.nombreOficina='" + objOficina.sOficina + "'";
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            //Se retorna el iResultado 
            objOficina.iResultado = int.Parse(sRes[1]);
            //Se retorna mensaje en caso de que exista la aduana
            objOficina.sMensaje = "La Oficina ya existe.";
        }
        else
        {
            //Se retorna el iResultado 
            objOficina.iResultado = 0;
        }
    }

    /// <summary>
    /// Método para guardar Oficina
    /// </summary>
    /// <param name="objOficina"></param>
    public void fn_GuardarOficina(Oficina objOficina)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarOficina", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdOficina", SqlDbType.Int, objOficina.iIdOficina.ToString());
                objConexion.agregarParametroSP("@sOficina", SqlDbType.VarChar, objOficina.sOficina);
                objConexion.agregarParametroSP("@sDireccion", SqlDbType.VarChar, objOficina.sDireccion);
                objConexion.agregarParametroSP("@iIdInstancia",SqlDbType.Int,objOficina.iIdInstancia.ToString());
                objConexion.agregarParametroSP("@sCodigoPostal", SqlDbType.VarChar, objOficina.sCodigoPostal.ToString());
                objConexion.agregarParametroSP("@sCorreoElectronico",SqlDbType.VarChar, objOficina.sCorreoElectronico.ToString());
                objConexion.agregarParametroSP("@sCorreoNG", SqlDbType.VarChar, objOficina.sCorreoNG.ToString());
                objConexion.agregarParametroSP("@sCorreoCliente", SqlDbType.VarChar, objOficina.sCorreoCliente.ToString());

               

                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objOficina.iResultado = 1;
                    objOficina.sMensaje = "Oficina guardada con éxito.";
                    objOficina.iIdOficina = int.Parse(sResOut[1]);
                }
                else
                {
                    //Se retorna el mensaje de error
                    objOficina.iResultado = 0;
                    objOficina.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objOficina.iResultado = 0;
                objOficina.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para eliminar aduana
    /// </summary>
    /// <param name="objOficina"></param>
    public void fn_EliminarOficina(Oficina objOficina)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_EliminarOficina", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdOficina", SqlDbType.Int, objOficina.iIdOficina.ToString());
               // objConexion.agregarParametroSP("@iIdInstancia", SqlDbType.Int, objOficina.iIdInstancia.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objOficina.iResultado = 1;
                    objOficina.sMensaje = "Oficina eliminada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objOficina.iResultado = 0;
                    objOficina.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objOficina.iResultado = 0;
                objOficina.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para validar que la Instancia aduana no exista
    /// </summary>
    /// <param name="objOficina"></param>
    public void fn_ValidarInstanciaOficinaExiste(Oficina objOficina)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "SELECT COUNT(*) FROM tInstanciaOficina tca WHERE tca.idInstancia=" + objOficina.iIdInstancia + " and tca.idOficina=" + objOficina.iIdOficina;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objOficina.iResultado = int.Parse(sRes[1]);
        //Se retorna mensaje en caso de que el ciente ya este asignado
        objOficina.sMensaje = "La Instancia ya está asignado a la Oficina.";
    }

    
    /// <summary>
    /// Método para validar que la proveedor aduana no exista
    /// </summary>
    /// <param name="objOficina"></param>
    public void fn_ValidarAduanaOficinaExiste(Oficina objOficina)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "SELECT COUNT(*) FROM tOficinaAduana tpa WHERE tpa.idAduana=" + objOficina.iIdAduana + " and tpa.idOficina=" + objOficina.iIdOficina;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objOficina.iResultado = int.Parse(sRes[1]);
        //Se retorna mensaje en caso de que el proveedor ya este asignado
        objOficina.sMensaje = "El proveedor ya está asignado a la aduana.";
    }

    /// <summary>
    /// Método para guardar el proveedor de la aduana
    /// </summary>
    /// <param name="objOficina"></param>
    
    
    public void fn_GuardarAduanaOficina(Oficina objOficina)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarAduanaOficina", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objOficina.iIdAduana.ToString());
                objConexion.agregarParametroSP("@iIdOficina", SqlDbType.Int, objOficina.iIdOficina.ToString());
                
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objOficina.iResultado = 1;
                    objOficina.sMensaje = "Aduana guardado con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objOficina.iResultado = 0;
                    objOficina.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objOficina.iResultado = 0;
                objOficina.sMensaje = ex.Message;
            }
        }
    }
   
    /// <summary>
    /// Método para obtener datos de la aduana
    /// </summary>
    /// <param name="objOficina"></param>
    
    public void fn_ObtenerDatosOficina(Oficina objOficina)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sOficina", "sDireccion","sCodigoPostal", "sCorreoElectronico","iIdInstancia", "sCorreoNG", "sCorreoCliente" };
        //Se crea la consulta
        string sQuery = "SELECT ofi.nombreOficina as sOficina, ofi.direccion as sDireccion,ofi.codigoPostal as sCodigoPostal,"+
                            " ofi.correoElectronico as sCorreoElectronico,insOfi.idInstancia as iIdInstancia, insOfi.correoNG as sCorreoNG,"+
                            " insOfi.correoCliente as sCorreoCliente from tOficina ofi inner join tInstanciaOficina insOfi on ofi.idOficina = insOfi.idOficina"+
	                            " WHERE ofi.idOficina =" + objOficina.iIdOficina;
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<Oficina>(sQuery, arrAtributos, objOficina);
        //Se asigna el resultado
        objOficina.iResultado = 1;
    }
    
   

    /// <summary>
    /// Método para eliminar proveedor de la aduana
    /// </summary>
    /// <param name="objOficina"></param>
    public void fn_EliminarAduanaOficina(Oficina objOficina)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery
        string sQuery = "DELETE FROM tOficinaAduana  WHERE idAduana=" + objOficina.iIdAduana + " and idOficina=" + objOficina.iIdOficina;
        string sRes = objConexion.ejecutarComando(sQuery);
        //Se verifica el resultado
        if (sRes == "1")
        {
            //Se retorna el sResultado 
            objOficina.iResultado = 1;
            //Se retorna mensaje de éxito
            objOficina.sMensaje = "Aduana eliminado con éxito.";
        }
        else
        {
            //Se retorna el sResultado 
            objOficina.iResultado = 0;
            //Se retorna mensaje de error
            objOficina.sMensaje = sRes;
        }
    }
}