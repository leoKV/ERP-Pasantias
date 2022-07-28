using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Comitente
/// </summary>
public class Comitente
{
	public Comitente() { }

    //Se declaran los atributos de comitente
    public int iIdComitente { get; set; }
    public string sIdComitente { get; set; }
    public string sNombreComitente { get; set; }
    public string sCalle { get; set; }
    public string sColonia { get; set; }
    public string sNumInterior { get; set; }
    public string sNumExterior { get; set; }
    public string sRFC { get; set; }
    public string sCorreo { get; set; }
    public int iIdInstancia { get; set; }
    public int iIdEstatus { get; set; }
    public int iIdAduana { get; set; }
    public int iIdCliente { get; set; }
    public int iIdProveedor { get; set; }
    //Se declaran atributos generales
    public int iResultado { get; set; }
    public string sMensaje { get; set; }
    public string sContenido { get; set; }
    public int iIdUsuario { get; set; }

    /// <summary>
    /// Método para validar que la Comitente no exista
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_ValidarComitenteExiste(Comitente objComitente) {
        if (objComitente.iIdComitente == 0)
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //sQuery
            string sQuery = "SELECT COUNT(*) FROM tComitente WHERE razonSocial='" + objComitente.sNombreComitente + "'";
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            //Se retorna el iResultado 
            objComitente.iResultado = int.Parse(sRes[1]);
            //Se retorna mensaje en caso de que exista la Comitente
            objComitente.sMensaje = "El comitente ya existe.";
        }
        else {
            //Se retorna el iResultado 
            objComitente.iResultado = 0;
        }
    }

    /// <summary>
    /// Método para guardar Comitente
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_GuardarComitente(Comitente objComitente) {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarComitente", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objComitente.iIdComitente.ToString());
                objConexion.agregarParametroSP("@sNombreComitente", SqlDbType.VarChar, objComitente.sNombreComitente);
                objConexion.agregarParametroSP("@sCalle", SqlDbType.VarChar, objComitente.sCalle);
                objConexion.agregarParametroSP("@sColonia", SqlDbType.VarChar, objComitente.sColonia);
                objConexion.agregarParametroSP("@sNumInterior", SqlDbType.VarChar, objComitente.sNumInterior);
                objConexion.agregarParametroSP("@sNumExterior", SqlDbType.VarChar, objComitente.sNumExterior);
                objConexion.agregarParametroSP("@sRFC", SqlDbType.VarChar, objComitente.sRFC);
                objConexion.agregarParametroSP("@sCorreo", SqlDbType.VarChar, objComitente.sCorreo);
                objConexion.agregarParametroSP("@iIdInstancia", SqlDbType.Int, objComitente.iIdInstancia.ToString());
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, objComitente.iIdUsuario.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objComitente.iResultado = 1;
                    objComitente.sMensaje = "Comitente guardado con éxito.";
                    objComitente.iIdComitente = int.Parse(sResOut[1]);
                }
                else
                {
                    //Se retorna el mensaje de error
                    objComitente.iResultado = 0;
                    objComitente.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objComitente.iResultado = 0;
                objComitente.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para eliminar Comitente
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_EliminarComitente(Comitente objComitente)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_EliminarComitente", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objComitente.iIdComitente.ToString());
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, objComitente.iIdUsuario.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objComitente.iResultado = 1;
                    objComitente.sMensaje = "Comitente eliminada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objComitente.iResultado = 0;
                    objComitente.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objComitente.iResultado = 0;
                objComitente.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para validar que la aduana Comitente no exista
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_ValidarAduanaComitenteExiste(Comitente objComitente)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "SELECT COUNT(*) FROM tComitenteAduana tca WHERE tca.idAduana=" + objComitente.iIdAduana + " and tca.idComitente=" + objComitente.iIdComitente;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objComitente.iResultado = int.Parse(sRes[1]);
        //Se retorna mensaje en caso de que el ciente ya este asignado
        objComitente.sMensaje = "La aduana ya está asignada al comitente.";
    }

    /// <summary>
    /// Método para guardar el aduana de la Comitente
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_GuardarAduanaComitente(Comitente objComitente)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarAduanaComitente", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objComitente.iIdComitente.ToString());
                objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objComitente.iIdAduana.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objComitente.iResultado = 1;
                    objComitente.sMensaje = "Aduana guardada con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objComitente.iResultado = 0;
                    objComitente.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objComitente.iResultado = 0;
                objComitente.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para eliminar aduana de la Comitente
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_EliminarAduanaComitente(Comitente objComitente)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "DELETE FROM tComitenteAduana WHERE idAduana=" + objComitente.iIdAduana + " and idComitente=" + objComitente.iIdComitente;
        string sRes = objConexion.ejecutarComando(sQuery);
        //Se verifica el resultado
        if (sRes == "1")
        {
            sQuery = "DELETE FROM tUsuarioComitenteAduana WHERE idUsuarioComitente in ( select idUsuarioComitente from tUsuarioComitente where idComitente = " + objComitente.iIdComitente + ") and idAduana =" + objComitente.iIdAduana;
            //Se retorna el sResultado 
             sRes = objConexion.ejecutarComando(sQuery);
            //Se verifica el resultado
             if (sRes == "1")
             {
                 objComitente.iResultado = 1;
                 //Se retorna mensaje de éxito
                 objComitente.sMensaje = "Aduana eliminada con éxito.";
             }
             else {
                 //Se retorna el sResultado 
                 objComitente.iResultado = 0;
                 //Se retorna mensaje de error
                 objComitente.sMensaje = sRes;
             }
        }
        else
        {
            //Se retorna el sResultado 
            objComitente.iResultado = 0;
            //Se retorna mensaje de error
            objComitente.sMensaje = sRes;
        }
    }

    /// <summary>
    /// Método para validar que la cliente Comitente no exista
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_ValidarClienteComitenteExiste(Comitente objComitente)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "SELECT COUNT(*) FROM tClienteComitente WHERE idCliente=" + objComitente.iIdCliente + " and idComitente=" + objComitente.iIdComitente;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objComitente.iResultado = int.Parse(sRes[1]);
        //Se retorna mensaje en caso de que el ciente ya este asignado
        objComitente.sMensaje = "El cliente ya está asignado al comitente.";
    }

    /// <summary>
    /// Método para guardar el cliente de la Comitente
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_GuardarClienteComitente(Comitente objComitente)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarClienteComitente", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objComitente.iIdComitente.ToString());
                objConexion.agregarParametroSP("@iIdCliente", SqlDbType.Int, objComitente.iIdCliente.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objComitente.iResultado = 1;
                    objComitente.sMensaje = "Cliente guardado con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objComitente.iResultado = 0;
                    objComitente.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objComitente.iResultado = 0;
                objComitente.sMensaje = ex.Message;
            }
        }
    }
    
    /// <summary>
    /// Método para eliminar cliente de la Comitente
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_EliminarClienteComitente(Comitente objComitente)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "DELETE FROM tClienteComitente WHERE idCliente=" + objComitente.iIdCliente + " and idComitente=" + objComitente.iIdComitente;
        string sRes = objConexion.ejecutarComando(sQuery);
        //Se verifica el resultado
        if (sRes == "1")
        {
            //Se retorna el sResultado 
            objComitente.iResultado = 1;
            //Se retorna mensaje de éxito
            objComitente.sMensaje = "Cliente eliminado con éxito.";
        }
        else {
            //Se retorna el sResultado 
            objComitente.iResultado = 0;
            //Se retorna mensaje de error
            objComitente.sMensaje = sRes;
        }
    }

    /// <summary>
    /// Método para validar que la proveedor Comitente no exista
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_ValidarProveedorComitenteExiste(Comitente objComitente)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "SELECT COUNT(*) FROM tProveedorComitente WHERE idProveedor=" + objComitente.iIdProveedor + " and idComitente=" + objComitente.iIdComitente;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objComitente.iResultado = int.Parse(sRes[1]);
        //Se retorna mensaje en caso de que el proveedor ya este asignado
        objComitente.sMensaje = "El proveedor ya está asignado al comitente.";
    }

    /// <summary>
    /// Método para guardar el proveedor de la Comitente
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_GuardarProveedorComitente(Comitente objComitente)
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
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objComitente.iIdComitente.ToString());
                objConexion.agregarParametroSP("@iIdProveedor", SqlDbType.Int, objComitente.iIdProveedor.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objComitente.iResultado = 1;
                    objComitente.sMensaje = "Proveedor guardado con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objComitente.iResultado = 0;
                    objComitente.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objComitente.iResultado = 0;
                objComitente.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para eliminar proveedor de la Comitente
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_EliminarProveedorComitente(Comitente objComitente)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "DELETE FROM tProveedorComitente WHERE idProveedor=" + objComitente.iIdProveedor + " and idComitente=" + objComitente.iIdComitente;
        string sRes = objConexion.ejecutarComando(sQuery);
        //Se verifica el resultado
        if (sRes == "1")
        {
            //Se retorna el sResultado 
            objComitente.iResultado = 1;
            //Se retorna mensaje de éxito
            objComitente.sMensaje = "Proveedor eliminado con éxito.";
        }
        else
        {
            //Se retorna el sResultado 
            objComitente.iResultado = 0;
            //Se retorna mensaje de error
            objComitente.sMensaje = sRes;
        }
    }
    
    /// <summary>
    /// Método para obtener datos de la Comitente
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_ObtenerDatosComitente(Comitente objComitente)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sNombreComitente", "sCalle", "sColonia", "sNumInterior", "sNumExterior", "sRFC", "sCorreo", "iIdInstancia" };
        //Se crea la consulta
        string sQuery = "SELECT razonSocial AS sNombreComitente, calle AS sCalle, colonia AS sColonia, numInt AS sNumInterior, numExt AS sNumExterior, rfc AS sRFC, correo AS sCorreo, idInstancia AS iIdInstancia FROM tComitente WHERE idComitente=" + objComitente.iIdComitente;
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<Comitente>(sQuery, arrAtributos, objComitente);
        //Se asigna el resultado
        objComitente.iResultado = 1;
    }

    /// <summary>
    /// Método para obtener instancias
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_ObtenerInstancias(Comitente objComitente)
    {
        //Consulta para obtener datos
        string sQuery = "SELECT ci.idInstancia,ci.nomInstancia, (SELECT COUNT(*) FROM tComitente tof WHERE tof.idComitente=" + objComitente.iIdComitente + " and tof.idInstancia=ci.idInstancia) AS iIdInstanciaActual FROM cInstancias ci";
        //Se crea la variable para almacenar datos
        DataSet dsDatos;
        //Se declara variable para almacenar checked
        string sChecked = "";
        //Se instancia la clase conexión
        Conexion objConexion = new Conexion();
        //Se ejecuta la consulta para obtener los embalajes
        dsDatos = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "instancia");
        //Se verifica que se tengan embalajes asignados
        if (dsDatos.Tables["instancia"].Rows.Count > 0)
        {
            //Se crea contenedor radio
            objComitente.sContenido += "<div class='container-fluid'><div class='row'>";
            foreach (DataRow registro in dsDatos.Tables["instancia"].Rows)
            {
                //Se verifica instancia actual
                sChecked = registro["iIdInstanciaActual"].ToString() == "1" ? "checked" : "";
                //Se abre div radio
                objComitente.sContenido += "<div class='col-lg-6'><div class='radio radio-primary'>";
                //Se crea el radio
                objComitente.sContenido += "<input type='radio' name='hrbnInstancias' id='hrbn-" + registro["idInstancia"].ToString() + "' value='" + registro["idInstancia"].ToString() + "' " + sChecked + " onclick='javascript:fn_CambiarInstancia(" + objComitente.iIdComitente + ",this.value);' />" +
                                         "<label for='hrbn-" + registro["idInstancia"].ToString() + "' class='form-label'>" +
                                             registro["nomInstancia"].ToString() +
                                         "</label>";
                //Se cierra div radio
                objComitente.sContenido += "</div></div>";
            }
            //Se cierra contenedor radio
            objComitente.sContenido += "</div></div>";
        }
        else
        {
            objComitente.sContenido = "<div></div>";
        }
    }

    /// <summary>
    /// Método para cambiar instancia
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_CambiarInstancia(Comitente objComitente)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_CambiarInstancia", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objComitente.iIdComitente.ToString());
                objConexion.agregarParametroSP("@iIdInstancia", SqlDbType.Int, objComitente.iIdInstancia.ToString());
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, objComitente.iIdUsuario.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objComitente.iResultado = 1;
                    objComitente.sMensaje = "Instancia actualizada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objComitente.iResultado = 0;
                    objComitente.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objComitente.iResultado = 0;
                objComitente.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para obtener combo cliente.
    /// </summary>
    /// <param name="objSolicitudTransferencia"></param>
    public void fn_ComboCliente(Comitente objComitente)
    {
        //Consulta para obtener datos
        string sQuery = "select Distinct tC.idCliente, tC.nomCliente " +
                        " from cAduana cA inner join tComitenteAduana tCA  " +
                        " on cA.idAduana = tCA.idAduana inner join tClienteComitente tCC " +
                        " on tCA.idComitente = tCC.idComitente inner join tCliente tC " +
                        " on tCC.idCliente = tC.idCliente inner join tClienteAduana tClA " +
                        " on cA.idAduana = tClA.idAduana" +
                        " where tC.idCliente NOT IN" +
                        " (SELECT idCliente " +
                        " FROM tClienteAduana" +
                        " WHERE idAduana = " + objComitente.iIdAduana + ") and cA.idAduana = " + objComitente.iIdAduana;
        //Se crea la variable para almacenar datos
        DataSet dsDatos;
        //Se instancia la clase conexión
        Conexion objConexion = new Conexion();
        //Se ejecuta la consulta para obtener los datos
        dsDatos = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "cliente");
        //Se verifica que se tengan datos
        if (dsDatos.Tables["cliente"].Rows.Count > 0)
        {
            //Se crea option vacío
            objComitente.sContenido = "<option value=''></option>";
            //Se recorren los registros
            foreach (DataRow registro in dsDatos.Tables["cliente"].Rows)
            {
                objComitente.sContenido += "<option value='" + registro[0] + "'>" + registro[1] + "</option>";
            }
        }
    }

    /// <summary>
    /// Método para obtener combo cliente.
    /// </summary>
    /// <param name="objSolicitudTransferencia"></param>
    public void fn_ComboProveedor(Comitente objComitente)
    {
        //Consulta para obtener datos
        string sQuery = "select tP.idProveedor, tP.nomProveedor from cAduana cA inner join tComitenteAduana tCA " +
                        " on cA.idAduana = tCA.idAduana inner join tProveedorComitente tPC" +
                        " on tCA.idComitente = tPC.idComitente inner join tProveedor tP" +
                        " on tPC.idProveedor = tP.idProveedor" +
                        " where cA.idAduana = " + objComitente.iIdAduana.ToString();
        //Se crea la variable para almacenar datos
        DataSet dsDatos;
        //Se instancia la clase conexión
        Conexion objConexion = new Conexion();
        //Se ejecuta la consulta para obtener los datos
        dsDatos = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "proveedor");
        //Se verifica que se tengan datos
        if (dsDatos.Tables["proveedor"].Rows.Count > 0)
        {
            //Se crea option vacío
            objComitente.sContenido = "<option value=''></option>";
            //Se recorren los registros
            foreach (DataRow registro in dsDatos.Tables["proveedor"].Rows)
            {
                objComitente.sContenido += "<option value='" + registro[0] + "'>" + registro[1] + "</option>";
            }
        }
    }
}