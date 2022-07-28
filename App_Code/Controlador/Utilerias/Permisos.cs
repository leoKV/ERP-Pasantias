using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Permisos
/// </summary>
public class Permisos
{
    /// <summary>
    /// Declara variables
    /// </summary>
    public int iResultado { set; get; }
    public string sMensaje { set; get; }
    public string sContenido { set; get; }
    public int iIdTipoAcceso { set; get; }
    public string sIdTipoAcceso { set; get; }
    /// <summary>
    /// VARIABLE PARA ID DE USUARIO
    /// </summary>
    private string sIdUsuario { set; get; }
    private int iIdMenu { set; get; }

    /// <summary>
    /// Declara constantes
    /// </summary>
    private int iEXITO = 1; ///RETORNA EXITO
    private int iALERTA = 2; ///RETORNA ALERTA
    private int iERROR=3; ///RETORNA ERROR

    private int iMODIFICA=1; ///ACCION PARA MODIFICAR REGISTROS
    private int iCOUNSULTA=2; ///ACCION PARA CONSULTAR REGISTROS
    private int iSIN_ACCESO=3; ///SIN ACCESO A SISTEMA

    /// <summary>
    /// CONSTRUCTOR
    /// </summary>
    /// <param name="iIdUser"></param>
    public Permisos(string iIdUser, int iIdAcceso) { this.sIdUsuario = iIdUser; this.iIdMenu = iIdAcceso; }

    /// <summary>
    /// Método para recuperar acceso de Usuario
    /// </summary>
    /// <param name="objPermisos"></param>
    /// <returns></returns>
    public void getValidaAction(Permisos objPermisos)
    {
        ///INICIO TRY
        try
        {
            ///clase securiry para desencryptar id de usuario
            Security secIdUser = new Security(sIdUsuario);

            ///instancia a clase conexion
            Conexion conexion = new Conexion();
            ///QUERY PARA RECUPERAR TIPO DE ACCESO
            string sQuery = "SELECT TOP 1 idTipoAccesoMenu FROM tRolMenu trm WHERE trm.idMenu=" + iIdMenu + " and trm.idRol in (SELECT DISTINCT tuor.idRol FROM tUsuarioComitenteRol tuor inner join tUsuarioComitente tuo ON tuor.idUsuarioComitente=tuo.idUsuarioComitente WHERE tuo.idUsuario=" + secIdUser.DesEncriptar() + ") ORDER BY idTipoAccesoMenu ASC";
            ///VARIABLE PARA ALMACENAR RESULTADO
            string[] resResultado = conexion.ejecutarConsultaRegistroSimple(sQuery);
            ///verifica si se ejecuta con éxito
            if (resResultado[0].Equals("1"))
            {
                ///VERIFICA QUE TENGA RESULTADO
                if (!resResultado[1].Equals(""))
                {
                    objPermisos.iResultado = iEXITO;///retorna resultado el resultado
                    objPermisos.sMensaje = "Acceso recuperado con éxito.";///retorna mensaje

                    ///RETORNA EL ACCESO
                    objPermisos.iIdTipoAcceso = int.Parse(resResultado[1]);
                }///FIN VERIFICA RESULTADO
                 ///INICIO NO TIENE RESULTADO
                else {
                    objPermisos.iResultado = iERROR;///retorna resultado de error
                    objPermisos.sMensaje = "No se recuperó resultado.";

                    ///RETORNA SIN ACCESO
                    objPermisos.iIdTipoAcceso = iSIN_ACCESO;
                }///FIN NO TIENE RESULTADO
            }///fin verifica se ejecuta con exito
             ///inicio else error consulta
            else {
                objPermisos.iResultado = iERROR;///retorna resultado de error
                objPermisos.sMensaje = "Error recupear acceso: " + resResultado[0].ToString();///retorna mensaje
                ///RETORNA SIN ACCESO
                objPermisos.iIdTipoAcceso = iSIN_ACCESO;
            }///fin else error consulta

        }///FIN TRY
         ///INICIO CATCH
        catch (Exception ex)
        {
            objPermisos.iResultado = iERROR;///retorna resultado de error
            objPermisos.sMensaje = "Error general: "+ex.Message;///retorna mensaje
            ///RETORNA SIN ACCESO
            objPermisos.iIdTipoAcceso = iSIN_ACCESO;
        }///FIN CATCH
        //Se encripta el tipo de acceso 
        Security secTipoAcceso = new Security(objPermisos.iIdTipoAcceso.ToString());
        objPermisos.sIdTipoAcceso = secTipoAcceso.Encriptar();
    }


}