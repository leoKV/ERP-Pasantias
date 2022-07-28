using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;


/// <summary>
/// Descripción breve de Soporte
/// </summary>
public class Soporte
{
    public Soporte() {    }
    public int iIdUsuario { get; set; }
    public int iIdSubReferencia { get; set; }
    public int iIdEstatus { get; set; }

    public string sMensaje { get; set; }
    public int iResultado { get; set; }
    /// <summary>
    /// Método para cambiar estatus del servicio referencia
    /// </summary>
    /// <param name="objReferencia"></param>
    public void fn_CambiarEstatusReferencia(Soporte objSoporte)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("dbo.pa_CambiarEstatusReferencia", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdSubReferencia", SqlDbType.Int, objSoporte.iIdSubReferencia.ToString());
                objConexion.agregarParametroSP("@sEstatus", SqlDbType.Int, objSoporte.iIdEstatus.ToString());
                objConexion.agregarParametroSP("@sIdUsuario", SqlDbType.Int, objSoporte.iIdUsuario.ToString());
                //objConexion.agregarParametroSP("@sMotivo", SqlDbType.VarChar, objReferencia.sMotivo);
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito 
                    objSoporte.sMensaje = "Referencia actualizada correctamente.";

                }
                else
                {
                    //Se retorna el mensaje de error
                    objSoporte.iResultado = 0;
                    objSoporte.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objSoporte.iResultado = 0;
                objSoporte.sMensaje = ex.Message;
            }
        }
    }


    

}