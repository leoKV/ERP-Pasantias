using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Soporte_Solicitud_Transferencia
/// </summary>
public class Soporte_Solicitud_Transferencia
{
	public Soporte_Solicitud_Transferencia(){	}
    public int iIdUsuario { get; set; }
    public int iIdST { get; set; }
    public int iIdEstatus { get; set; }
    public int iIdEstatusNuevo { get; set; }

    public int iIdProvNuevo { get; set; }

    public int iIdsIdSaldo { get; set; }

    public string sNomUsuario { get; set; }

    public string sUsuario { get; set; }

    public string sDepartamento { get; set; }

    public string sSolicitud { get; set; }

    public string sEstatusN { get; set; }

    public string sProvN { get; set; }

    public string sMensaje { get; set; }
    public string sProveedor { get; set; }
    public int iResultado { get; set; }

    /// <summary>
    /// Metodo utilizado para eliminar una ST,
    /// este metodo hace uso del procedimiento pa_VolverTarificar
    /// </summary>
    /// <param name="objOrdenVenta"></param>
    public void fn_EliminarST(Soporte_Solicitud_Transferencia objST)
    {
        //Instanciamos clase de conexión
        Conexion oConexion = new Conexion();
        //Generamos el SP
        string sRes = oConexion.generarSP("pa_Soporte_EliminarST", 0);
        //Creamos arreglo para guardar resultado del store
        string[] sResOut;
        //Si el procedimiento se generó correctamente
        if (sRes == "1")
        {
            //Agregamos parametros al sp
            oConexion.agregarParametroSP("@iIdST", System.Data.SqlDbType.Int, objST.iIdST.ToString());
            //oConexion.agregarParametroSP("@iIdUsuario", System.Data.SqlDbType.Int, objST.iIdUsuario.ToString());

            //Ejecutamos el procedimiento
            sResOut = oConexion.ejecutarProcOUTPUT_INT("@sResOut");
            if (sResOut[0] != "0")
            {
                //Se retorna el mensaje de éxito
                //lA POSICIOS [1] contiene la sub referencia
                objST.fn_ObtenerDatosElminacionST(objST);
                
                //Guardar en LOG 
                ConexionLog objConexionLog = new ConexionLog();

                //Inicia query
                string query = "insert into tLogPrueSTEliminadas (idOperador,Operador,NombreOperador,Departamento,SolicitudTransferencia,ReferenciaOpe,Fecha) " +
                "values(" + objST.iIdUsuario + ",'" + objST.sUsuario + "','" + objST.sNomUsuario + "','" + objST.sDepartamento + "','" + objST.sSolicitud + "','"+ sResOut[1].ToString() + "' ,getdate())";
                //Ejecuta query
                objConexionLog.ejecutarConsultaRegistroSimple(query);
                //retorna resultado


                objST.iResultado = 1;
                objST.sMensaje = "Eliminacion Exitosa";
            }
            else
            {
                //Se retorna mensaje de error
                objST.iResultado = 0;
                objST.sMensaje = "Eliminacion Fallida Favor de contactar con el equipo de Soporte";
            }
        }

    }

    /// <summary>
    /// Método para cambiar estatus del servicio referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_CambiarEstatusST(Soporte_Solicitud_Transferencia objST)
    {

        //Instanciamos clase de conexión
        Conexion oConexion = new Conexion();
        //Generamos el SP
        string sRes = oConexion.generarSP("pa_Soporte_ModificarEstatusST", 0);
        //Creamos arreglo para guardar resultado del store
        string[] sResOut;

        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                oConexion.agregarParametroSP("@iIdST", System.Data.SqlDbType.Int, objST.iIdST.ToString());
                oConexion.agregarParametroSP("@iIdEstatusNuevo", System.Data.SqlDbType.Int, objST.iIdEstatusNuevo.ToString());
                //objConexion.agregarParametroSP("@sIdUsuario", SqlDbType.Int, objSoporte.iIdUsuario.ToString());
                //Se ejecuta el SP
                sResOut = oConexion.ejecutarProcOUTPUT_INT("@sResOut");


                if (sResOut[0] != "0")
                {
                    objST.fn_ObtenerDatosCambioEstatusST(objST);

                    //Guardar en LOG 
                    ConexionLog objConexionLog = new ConexionLog();

                    //Inicia query
                    string query = "insert into LogPrueEstatusST (Operador,NombreOperador,Departamento,SolicitudTransferencia,EstatusA,EstatusN,Fecha) " +
                    "values('" + objST.sUsuario + "','" + objST.sNomUsuario + "','" + objST.sDepartamento + "','" + objST.sSolicitud + "','" + sResOut[1].ToString() + "','"+ objST.sEstatusN+ "',getdate())";
                    //Ejecuta query
                    objConexionLog.ejecutarConsultaRegistroSimple(query);
                    //Se retorna el mensaje de éxito 
                    objST.iResultado = 1;
                    objST.sMensaje = "Estatus Solicitud Transferencia actualizado Correctamente.";

                }
                else
                {
                    //Se retorna el mensaje de error
                    objST.iResultado = 0;
                    objST.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objST.iResultado = 0;
                objST.sMensaje = ex.Message;
            }
        }
    }


    /// <summary>
    /// Método para cambiar estatus del servicio referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_CambiarProvST(Soporte_Solicitud_Transferencia objST)
    {

        //Instanciamos clase de conexión
        Conexion oConexion = new Conexion();
        //Generamos el SP
        string sRes = oConexion.generarSP("pa_Soporte_ModificarProvST", 0);
        //Creamos arreglo para guardar resultado del store
        string[] sResOut;

        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                oConexion.agregarParametroSP("@iIdST", System.Data.SqlDbType.Int, objST.iIdST.ToString());
                oConexion.agregarParametroSP("@iIdProvNuevo", System.Data.SqlDbType.Int, objST.iIdProvNuevo.ToString());
                //Se ejecuta el SP
                sResOut = oConexion.ejecutarProcOUTPUT_INT("@sResOut");


                if (sResOut[0] != "0")
                {
                   


                    //Se retorna el mensaje de éxito 
                    objST.iResultado = 1;
                    objST.sMensaje = "Razón social actualizada correctamente.";

                }
                else
                {
                    //Se retorna el mensaje de error
                    objST.iResultado = 0;
                    objST.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objST.iResultado = 0;
                objST.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Metodo utilizado para eliminar una ST,
    /// este metodo hace uso del procedimiento pa_VolverTarificar
    /// </summary>
    /// <param name="objOrdenVenta"></param>
    public void fn_EliminarSaldoST(Soporte_Solicitud_Transferencia objST)
    {
        //Instanciamos clase de conexión
        Conexion oConexion = new Conexion();
        //Generamos el SP
        string sRes = oConexion.generarSP("pa_Soporte_EliminarSaldoST", 0);
        //Creamos arreglo para guardar resultado del store
        string[] sResOut;
        //Si el procedimiento se generó correctamente
        if (sRes == "1")
        {
            //Agregamos parametros al sp
            oConexion.agregarParametroSP("@iIdST", System.Data.SqlDbType.Int, objST.iIdST.ToString());
            oConexion.agregarParametroSP("@iIdSaldo", System.Data.SqlDbType.Int, objST.iIdsIdSaldo.ToString());
            //oConexion.agregarParametroSP("@iIdUsuario", System.Data.SqlDbType.Int, objST.iIdUsuario.ToString());

            //Ejecutamos el procedimiento
            sResOut = oConexion.ejecutarProcOUTPUT_INT("@sResOut");
            if (sResOut[0] != "0")
            {
                //Se retorna el mensaje de éxito
                //lA POSICIOS [1] contiene la sub referencia
                objST.iResultado = 1;
                objST.sMensaje = "Eliminacion de Saldo Exitosa";
            }
            else
            {
                //Se retorna mensaje de error
                objST.iResultado = 0;
                objST.sMensaje = "Eliminacion Fallida Favor de contactar con el equipo de Soporte";
            }
        }

    }




    /// <summary>
    /// Método para obtener datos del proveedor/Razón social del cliente
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_ObtenerDatosProvST(Soporte_Solicitud_Transferencia objST)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sProveedor"};
        //Se crea la consulta
        string sQuery = @"SELECT 
                            ISNULL((SELECT 'RFC: '+tp.rfc+ ' Razón Social: '+ tp.nomProveedor  FROM tProveedor tp WHERE tp.idProveedor = tst.idProveedor),'0') sProveedor
                            FROM tSolicitudTransferencia tst
                            WHERE tst.idSolicitudTransferencia = " + objST.iIdST;
        //Se ejecuta el método para obtener datos
        string sRes = objConexion.ejecutaRecuperaObjeto<Soporte_Solicitud_Transferencia>(sQuery, arrAtributos, objST);
        //valida consulta
        if (sRes != "0")
        {
            //Se asigna el resultado éxito
            objST.iResultado = 1;
            objST.sMensaje = "Datos consultados correctamente";
        }
        else
        {
            //Se asigna el resultado erro
            objST.iResultado = 0;
            objST.sMensaje = "Error al consulta los datos: " + sRes;
        }

    }

    /// <summary>
    /// Método para obtener datos para el log de eliminaciones
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_ObtenerDatosElminacionST(Soporte_Solicitud_Transferencia objST)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sSolicitud" };
        //Se crea la consulta
        string sQuery = @"SELECT 
                            ISNULL(noSolicitudTransferencia,'N/A') sProveedor
                            FROM tSolicitudTransferencia tst
                            WHERE tst.idSolicitudTransferencia = " + objST.iIdST;
        //Se ejecuta el método para obtener datos
        string sRes = objConexion.ejecutaRecuperaObjeto<Soporte_Solicitud_Transferencia>(sQuery, arrAtributos, objST);

        objST.fn_ObtenerDatosUsuario(objST);
    }

    /// <summary>
    /// Método para obtener datos para el log de modificaciones
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_ObtenerDatosCambioEstatusST(Soporte_Solicitud_Transferencia objST)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sSolicitud", "sEstatusN " };
        //Se crea la consulta
        string sQuery = @"SELECT 
                            ISNULL(noSolicitudTransferencia,'N/A') sSolicitud,(SELECT nomEstatusSolTransferencia FROM cEstatusSolicitudTrans WHERE idEstatusSolicitudTrans = tst.idEstatusSolicitudTrans) sEstatusN
                            FROM tSolicitudTransferencia tst
                            WHERE tst.idSolicitudTransferencia = " + objST.iIdST;
        //Se ejecuta el método para obtener datos
        string sRes = objConexion.ejecutaRecuperaObjeto<Soporte_Solicitud_Transferencia>(sQuery, arrAtributos, objST);

        objST.fn_ObtenerDatosUsuario(objST);
    }


    /// <summary>
    /// Método para obtener datos para el log de modificaciones
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_ObtenerDatosCambioProvST(Soporte_Solicitud_Transferencia objST)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sSolicitud", "sProvN" };
        //Se crea la consulta
        string sQuery = @"SELECT 
                            ISNULL(noSolicitudTransferencia,'N/A') sSolicitud,(SELECT rfc+ ' ' + nomProveedor  FROM tProveedor WHERE idProveedor = tst.idProveedor) sProvN
                            FROM tSolicitudTransferencia tst
                            WHERE tst.idSolicitudTransferencia = " + objST.iIdST;
        //Se ejecuta el método para obtener datos
        string sRes = objConexion.ejecutaRecuperaObjeto<Soporte_Solicitud_Transferencia>(sQuery, arrAtributos, objST);

        objST.fn_ObtenerDatosUsuario(objST);
    }



    /// <summary>
    /// Método para obtener datos para el log de eliminacion de saldos
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_ObtenerDatosElimSaldosST(Soporte_Solicitud_Transferencia objST)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sSolicitud", "sEstatusN " };
        //Se crea la consulta
        string sQuery = @"SELECT 
                            ISNULL(noSolicitudTransferencia,'N/A') sSolicitud,(SELECT nomEstatusSolTransferencia FROM cEstatusSolicitudTrans WHERE idEstatusSolicitudTrans = tst.idEstatusSolicitudTrans) sEstatusN
                            FROM tSolicitudTransferencia tst
                            WHERE tst.idSolicitudTransferencia = " + objST.iIdST;
        //Se ejecuta el método para obtener datos
        string sRes = objConexion.ejecutaRecuperaObjeto<Soporte_Solicitud_Transferencia>(sQuery, arrAtributos, objST);

        objST.fn_ObtenerDatosUsuario(objST);
    }




    /// <summary>
    /// Método para obtener datos para el log de eliminaciones sólo del usuario
    /// </summary>
    /// <param name="objComitente"></param>
    public void fn_ObtenerDatosUsuario(Soporte_Solicitud_Transferencia objST)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();

        //Arreglo para atributos de usuario
        string[] arrAtributos = { "sNomUsuario", "sUsuario", "sDepartamento" };

        //Se crea la consulta
        string sQuery = @"
                    SELECT TOP 1 ISNULL(tu.nomUsuario + N' '+ tu.apPaterno + N' ' + tu.apMaterno,'S/D') sNomUsuario, ISNULL(tu.usuario,'S/D') sUsuario,
                    (SELECT TOP 1 cr.nomRol FROM cRol cr WHERE cr.idRol = tcr.idRol) sDepartamento
                    FROM tUsuarios tu
                    INNER JOIN tUsuarioComitenteRol tcr ON tu.idUsuario = tcr.idUsuarioComitente
                    WHERE tu.idUsuario = " + objST.iIdST;

        //Se ejecuta el método para obtener datos
        string sRes = objConexion.ejecutaRecuperaObjeto<Soporte_Solicitud_Transferencia>(sQuery, arrAtributos, objST);

    }


}