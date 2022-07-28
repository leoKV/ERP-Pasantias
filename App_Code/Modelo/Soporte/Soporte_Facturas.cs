using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Soporte_Reenvio_Factura
/// </summary>
public class Soporte_Facturas
{
    public Soporte_Facturas() { }

    public string UUID { get; set; }

    public List<string>valuesUUID { get; set; }
    public string sNoFactura { get; set; }
    public string sNomUsuario { get; set; }

    public string sUser { get; set; }

    public string sDepartamento { get; set; }

    public int sUsuario { get; set; }

    public string sMensaje { get; set; }
 
    public int iResultado { get; set; }

    public int leng { get; set; }
    public void fn_AjustarReenvioFacturas(Soporte_Facturas objSRF)
    {
        //Instanciamos clase de conexión
        Conexion oConexion = new Conexion();

        //Generamos el SP
        string sRes = oConexion.generarSP("SP_SoporteReenvioFactura_2", 0);
        ////Creamos arreglo para guardar resultado del store
        string[] sResOut;
        //Si el procedimiento se generó correctamente
        if (sRes == "1")
        {
            //Agregamos parametros al sp
            //string sModo = null;
            //oConexion.agregarParametroSP("@ENVAUTO", System.Data.SqlDbType.SmallInt,sModo);
            oConexion.agregarParametroSP("@UUID", System.Data.SqlDbType.VarChar, objSRF.UUID);
            //Ejecutamos el procedimiento
            sResOut = oConexion.ejecutarProcOUTPUT_INT("@sResOut");
            if (sResOut[0] != "0")
            {
                //Se retorna el mensaje de éxito
                //lA POSICIOS [1] contiene la sub referencia
                objSRF.fn_ObtenerDatosReenvioFacturas(objSRF);
                ConexionLog objConexionLog = new ConexionLog();
                string query = "SELECT noFactura FROM tFactura WHERE UUID=" + objSRF.UUID;
                objConexionLog.ejecutarConsultaRegistroSimple(query);
                objSRF.iResultado = 1;
                objSRF.sMensaje = "Ajuste Exitoso";
            }
            else
            {
                //Se retorna mensaje de error
                objSRF.iResultado = 0;
                objSRF.sMensaje = "Ajuste Fallido Favor de contactar con el equipo de Soporte";
            }
        }
    }

    public void fn_ObtenerDatosReenvioFacturas(Soporte_Facturas objSRF)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sNoFactura" };
        //Se crea la consulta
        string sQuery = "SELECT noFactura FROM tFactura WHERE UUID='"+ objSRF.UUID + "'"  ;
        ////Se ejecuta el método para obtener datos
        string sRes = objConexion.ejecutaRecuperaObjeto<Soporte_Facturas>(sQuery, arrAtributos, objSRF);

        objSRF.fn_ObtenerDatosUsuario(objSRF);
    }

    public void fn_ObtenerDatosUsuario(Soporte_Facturas objSRF)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();

        //Arreglo para atributos de usuario
        string[] arrAtributos = { "sNomUsuario", "sUser", "sDepartamento" };

        //Se crea la consulta
        string sQuery = @"
                    SELECT TOP 1 ISNULL(tu.nomUsuario + N' '+ tu.apPaterno + N' ' + tu.apMaterno,'S/D') sNomUsuario, ISNULL(tu.usuario,'S/D') sUsuario,
                    (SELECT TOP 1 cr.nomRol FROM cRol cr WHERE cr.idRol = tcr.idRol) sDepartamento
                    FROM tUsuarios tu
                    INNER JOIN tUsuarioComitenteRol tcr ON tu.idUsuario = tcr.idUsuarioComitente
                    WHERE tu.idUsuario = " + objSRF.sUsuario;

        //Se ejecuta el método para obtener datos
        string sRes = objConexion.ejecutaRecuperaObjeto<Soporte_Facturas>(sQuery, arrAtributos, objSRF);
        
    }


    public void fn_volcadoDatos(Soporte_Facturas objFile)
    {
        if (objFile.iResultado != 0)
        {
            //Instanciamos clase de conexión
            Conexion oConexion = new Conexion();
            for (int i = 0; i < objFile.leng; i++)
            {

                //Generamos el SP
                string sRes = oConexion.generarSP("SP_SoporteReenvioFactura_2", 0);
                ////Creamos arreglo para guardar resultado del store
                string[] sResOut;
                //Si el procedimiento se generó correctamente
                if (sRes == "1")
                {
                    //Agregamos parametros al sp
                    //string sModo = null;
                    //oConexion.agregarParametroSP("@ENVAUTO", System.Data.SqlDbType.SmallInt,sModo);
                    oConexion.agregarParametroSP("@UUID", System.Data.SqlDbType.VarChar, objFile.valuesUUID[i]);
                    //Ejecutamos el procedimiento
                    sResOut = oConexion.ejecutarProcOUTPUT_INT("@sResOut");
                    if (sResOut[0] != "0")
                    {
                        //Se retorna el mensaje de éxito
                        //lA POSICIOS [1] contiene la sub referencia
                        objFile.UUID = objFile.valuesUUID[i];
                        objFile.fn_ObtenerDatosReenvioMasivo(objFile);
                        ConexionLog objConexionLog = new ConexionLog();
                        string query = "SELECT noFactura FROM tFactura WHERE UUID=" + objFile.valuesUUID[i];
                        objConexionLog.ejecutarConsultaRegistroSimple(query);
                        objFile.iResultado = 1;
                        objFile.sMensaje = "Ajuste Masivo Exitoso";
                    }
                    else
                    {
                        //Se retorna mensaje de error
                        objFile.iResultado = 0;
                        objFile.sMensaje = "Ajuste Fallido Favor de contactar con el equipo de Soporte";
                    }
                }
            }

        }
        else
        {
            //Se retorna mensaje de error
            objFile.iResultado = 0;
            objFile.sMensaje = "El archivo que cargo, no cuenta con la estructura deseada";
        }

    }

    public void fn_ObtenerDatosReenvioMasivo(Soporte_Facturas objFile)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sNoFactura" };
        //Se crea la consulta
        string sQuery = "SELECT noFactura FROM tFactura WHERE UUID='" + objFile.UUID + "'";
        ////Se ejecuta el método para obtener datos
        string sRes = objConexion.ejecutaRecuperaObjeto<Soporte_Facturas>(sQuery, arrAtributos, objFile);

        objFile.fn_ObtenerUsuarioMasivo(objFile);
    }

    public void fn_ObtenerUsuarioMasivo(Soporte_Facturas objFile)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();

        //Arreglo para atributos de usuario
        string[] arrAtributos = { "sNomUsuario", "sUser", "sDepartamento" };

        //Se crea la consulta
        string sQuery = @"
                    SELECT TOP 1 ISNULL(tu.nomUsuario + N' '+ tu.apPaterno + N' ' + tu.apMaterno,'S/D') sNomUsuario, ISNULL(tu.usuario,'S/D') sUsuario,
                    (SELECT TOP 1 cr.nomRol FROM cRol cr WHERE cr.idRol = tcr.idRol) sDepartamento
                    FROM tUsuarios tu
                    INNER JOIN tUsuarioComitenteRol tcr ON tu.idUsuario = tcr.idUsuarioComitente
                    WHERE tu.idUsuario = " + objFile.sUsuario;

        //Se ejecuta el método para obtener datos
        string sRes = objConexion.ejecutaRecuperaObjeto<Soporte_Facturas>(sQuery, arrAtributos, objFile);

    }

}