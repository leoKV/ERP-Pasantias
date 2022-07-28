using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Vista_Soportes_soporte_ST : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Instancia a la clase SessionTimeOut
        SessionTimeOut obj_Session = new SessionTimeOut();
        //se manda llamar al metodo de validar sesion
        bool iSessionE = obj_Session.IsSessionTimedOut();
        //Constante que almacena id de menú
        int iMENU = 6;
        //Encriptar id menú
        Security secIdMenu = new Security(iMENU.ToString());
        //se valida la sesion
        if (!iSessionE)
        {
            //Inicio TRY
            try
        {
            //Se declaran los breadCrumbs
            string[] datos = { "Inicio", "Soporte", "Soporte Solicitud Transferencia" };
            string[] url = { "", "", "" };
            breadCrum.migajas(datos, url);
            ///recupera el id de Usuario
            string sIdUsuario = Session["iIdUsuario"].ToString();
            //Desencripta el id usuario
            Security secIdUsuario = new Security(sIdUsuario);
            int iIdUsuario = int.Parse(secIdUsuario.DesEncriptar());
            hlblIdUsuario.InnerHtml = iIdUsuario.ToString();
                ///INSTANCIA A CLASE PERMISOS (Se pasa el id del usuario y el identificador del menú)
                Permisos objPermisos = new Permisos(sIdUsuario, iMENU);
                ///variable para almacenar el tipo de acceso
                objPermisos.getValidaAction(objPermisos);
                ///metodo que permite verificar si el usuario tiene rol tesoreria
                int iRol = fn_ObtenerRol(sIdUsuario);
                //se almacena variable tipo rol en label 
                hlbliRol.InnerText = iRol.ToString();

                //Se almacena variable tipo acceso en label hidden
                hlblTipoAcceso.Value = objPermisos.sIdTipoAcceso;
                if (objPermisos.iIdTipoAcceso == 1 || objPermisos.iIdTipoAcceso == 2)
                {
                    //    //Se verifica si solo se tiene permisos de consulta y se ocultan campos
                    if (objPermisos.iIdTipoAcceso == 2)
                    {
                        //Se verifica rol 
                        if (iRol == 1)
                        { //rol Administrador TI 



                        }

                    }

                    //Se obtiene el identificaor tipo de listado.
                    // 0-Terceros, 1-Facturación, 2-Historico.
                    if (Request.QueryString["iListado"] != null)
            {
                hlbliListado.InnerHtml = Request.QueryString["iListado"];
            }
            else
            {
                // se especifica el proceso por defecto.
                hlbliListado.InnerHtml = "0";
            }
                    ///Método para generar estructura de lista de ST
                    fn_GeneraListaST();
            //Método para generar estructura de lista de servicios
            //fn_GeneraListaServicios();
            //Método para generar combo de servicios
            //fn_GenerarComboServicios();
            //Método para generar estructura de lista de colores
            //fn_GeneraListaColores();
            // genera lista de guias
            //fn_GeneraListaGuias();
            //Método para generar estructura de la tabla de Iconos
            //fn_GeneraListaIconos();

                }
                ///INICIO NO TIENE ACCESO
                else
                {
                    ///HAY ERROR EN EJECUCION, REDIRECCIONA A INICIO
                    Response.Redirect("../Inicio/Inicio.aspx?sIdMenu=" + secIdMenu.Encriptar());
                }///FIN NO TIENE ACCESO
            }
            //Inicio Catch
            catch
        {
                //si ocurre algun error se redirecciona al usuario al Inicio 
                Response.Redirect("../Inicio/Inicio.aspx?sIdMenu=" + secIdMenu.Encriptar());
            }
        }
        else
        {
            //si se termino el tiempo de sesion se redirecciona al Login y cierra sesión
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../../Login.aspx");
        }

    }

    /// <summary>
    /// Método para generar estructura de lista de las solicitudes de transferencia
    /// </summary>
    public void fn_GeneraListaST()
    {

        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "Solicitud de transferencia", "Referencia administrativa", "Referencia operativa", "Aduana", "Cliente", "Proveedor", "Estatus", "Fecha", "Tipo Solicitud", "Monto Total" };
        ///Se asignan los campos sin filtro
        string[] arrColumnasSinFiltro = { "Opciones de cambio" };
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblSolicitudTransferencia";
        ///Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
        hdvST.InnerHtml = objUtilerias.sContenido;
    }


    /// <summary>
    /// Método para generar la estructura de la tabla de un saldo aplicado
    /// </summary>
    public void fn_GenerarTablaSaldoAplicado()
    {
        //Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        //Se crean las columnas
        string[] arrColumnasSinFiltro = { "No.Solicitud", "Monto Total", "Documento Saldo Favor", "Saldo Aplicado", "Fecha", "Monto Pagado", "Opciones" };
        //Se pasan los parametros
        objUtilerias.sNombre = "htblSaldoAplicado";
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        //Se ejecuta el metodo
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        //Se asigna el contenido
        hdvSaldoAplicado.InnerHtml = objUtilerias.sContenido;
    }

    [WebMethod]
    public static List<AutoComplete> fn_GeneraAutocompleteEstatusST(string sCadena)
    {
        // Instancia conexión.
        Conexion oConexion = new Conexion();
        List<AutoComplete> lstResultados = new List<AutoComplete>();
        List<string> lstEstatusST = new List<string>();
        AutoComplete oAutoComplete;
        string sQuery = "";
        sCadena = sCadena.ToLower();

        sQuery = @"select idEstatusSolicitudTrans, nomEstatusSolTransferencia
                    from cEstatusSolicitudTrans where idEstatusSolicitudTrans != 5 AND nomEstatusSolTransferencia like '%" + sCadena + @"%'";
        //}
        // Obtiene los estaus de referencia.
        lstEstatusST = oConexion.ejecutarConsultaRegistroMultiples(sQuery);
        // Elimina elemento de respuesta por default.
        lstEstatusST.RemoveAt(0);
        // Verifica que se encontraron datos.
        if (lstEstatusST != null && lstEstatusST.Count > 0)
        {
            // Agrega resultados a Modelo Autocomplete.
            for (int i = 0; i < lstEstatusST.Count; i += 2)
            {
                oAutoComplete = new AutoComplete();
                oAutoComplete.ID = lstEstatusST[i];
                oAutoComplete.nombre = lstEstatusST[i + 1];
                lstResultados.Add(oAutoComplete);
            }
        }
        else
        {
            // Indica que no se han encontrado coincidencias.
            lstResultados.Add(new AutoComplete { ID = "", nombre = "No se encontraron resultados" });
        }
        return lstResultados;
    }

    [WebMethod]
    public static List<AutoComplete> fn_GeneraAutocompleteProvST(string sCadena)
    {
        // Instancia conexión.
        Conexion oConexion = new Conexion();
        List<AutoComplete> lstResultados = new List<AutoComplete>();
        List<string> lstProvST = new List<string>();
        AutoComplete oAutoComplete;
        string sQuery = "";
        sCadena = sCadena.ToLower();

        sQuery = @"select idProveedor, rfc+' '+nomProveedor sRFCNom
                    from tProveedor where idEstatus = 1 AND rfc+' '+nomProveedor like '%" + sCadena + @"%'";
        // Obtiene los proveedores que esten activos

        lstProvST = oConexion.ejecutarConsultaRegistroMultiples(sQuery);
        // Elimina elemento de respuesta por default.
        lstProvST.RemoveAt(0);
        // Verifica que se encontraron datos.
        if (lstProvST != null && lstProvST.Count > 0)
        {
            // Agrega resultados a Modelo Autocomplete.
            for (int i = 0; i < lstProvST.Count; i += 2)
            {
                oAutoComplete = new AutoComplete();
                oAutoComplete.ID = lstProvST[i];
                oAutoComplete.nombre = lstProvST[i + 1];
                lstResultados.Add(oAutoComplete);
            }
        }
        else
        {
            // Indica que no se han encontrado coincidencias.
            lstResultados.Add(new AutoComplete { ID = "", nombre = "No se encontraron resultados" });
        }
        return lstResultados;
    }

    private int fn_ObtenerRol(string sIdUsuario)
    {
        //objeto para desencriptar el idSubReferencia
        Security secIdUsuario = new Security(sIdUsuario);
        //se obtiene el id del usuario 
        int iIdUsuario = int.Parse(secIdUsuario.DesEncriptar());
        //variable para retornar
        int iRol = 0;

        //Se Instancia la clase
        Conexion objConexion = new Conexion();
        //Se crea la consulta para verificar si el menu tiene acceso a rol tesoreria
        string sQuery = "select count(idRolMenu) from tRolMenu where idRol = 6 and idMenu = 11";
        //Se obtiene el resultado
        string[] sRespuesta = objConexion.ejecutarConsultaRegistroSimple(sQuery);

        //se verifica si existe relación de rol con menu
        if (sRespuesta[1].ToString() != "0")
        { //si existe

            //Se crea la consulta para verificar si el usuario tiene el rol de tesoreria
            string sQuery1 = " select count(idRolUsuarioComitente) from tUsuarioComitenteRol where idRol = 6 and idUsuarioComitente " +
                             " in (select idUsuarioComitente from tUsuarioComitente WHERE idUsuario = " + iIdUsuario + ") ";
            //Se obtiene el resultado
            string[] sRespuesta1 = objConexion.ejecutarConsultaRegistroSimple(sQuery1);
            //se verifica si se ejuto correctamente 
            if (sRespuesta1[1].ToString() != "0")
            { //se regresa un 1 indicando que si tiene acceso 

                iRol = 1;

            }
        }

        //Se retorna la variable
        return iRol;
    }



    /// <summary>
    /// Elimina una ST
    /// </summary>
    /// <param name="iIdST"></param>
    /// <returns></returns>
    [WebMethod]
    public static Soporte_Solicitud_Transferencia fn_EliminarST(string sIdST, string sIdUsuario)
    {
        Soporte_Solicitud_Transferencia objST = new Soporte_Solicitud_Transferencia();
        // Asignar
        Security objSecurity = new Security(sIdST);
        objST.iIdST = int.Parse(objSecurity.DesEncriptar());
        //Extraemos al usuario
        //Security objSecurityUser = new Security(sIdUsuario);
        objST.iIdUsuario = int.Parse(sIdUsuario);

        objST.fn_EliminarST(objST);

        // Enviar
        return objST;
    }


    [WebMethod]
    public static Soporte_Solicitud_Transferencia fn_CambiarEstatusST(string sIdUsuario, string sIdSTEstatus, int sIdEstatus)
    {

        Soporte_Solicitud_Transferencia objST = new Soporte_Solicitud_Transferencia();
        // Asignar
        Security objSecurity = new Security(sIdSTEstatus);
        objST.iIdST = int.Parse(objSecurity.DesEncriptar());
        //Extraemos al usuario
        //Security objSecurityUser = new Security(sIdUsuario);
        objST.iIdUsuario = int.Parse(sIdUsuario);

        objST.iIdEstatusNuevo = sIdEstatus;

        //Se ejecuta el método
        objST.fn_CambiarEstatusST(objST);
        //Se retorna el objeto
        return objST;
    }


    /// <summary>
    /// Método para obtener los datos del cliente
    /// </summary>
    /// <param name="siIdST">Identificador encriptado de la ST</param>
    /// <returns>objCliente</returns>
    [WebMethod]
    public static Soporte_Solicitud_Transferencia fn_CambiarProvST(string sIdUsuario, string sIdSTProv, int sIdProvST)
    {

        Soporte_Solicitud_Transferencia objST = new Soporte_Solicitud_Transferencia();
        // Asignar
        Security objSecurity = new Security(sIdSTProv);
        objST.iIdST = int.Parse(objSecurity.DesEncriptar());
        //Extraemos al usuario
        //Security objSecurityUser = new Security(sIdUsuario);
        objST.iIdUsuario = int.Parse(sIdUsuario);

        objST.iIdProvNuevo = sIdProvST;

        //Se ejecuta el método
        objST.fn_CambiarProvST(objST);
        //Se retorna el objeto
        return objST;
    }

    /// <summary>
    /// Elimina una ST
    /// </summary>
    /// <param name="iIdST"></param>
    /// <returns></returns>
    [WebMethod]
    public static Soporte_Solicitud_Transferencia fn_EliminarSaldoST(string sIdST, string sIdUsuario, string sIdSaldo)
    {
        Soporte_Solicitud_Transferencia objST = new Soporte_Solicitud_Transferencia();
        // Asignar
        Security objSecurity = new Security(sIdST);
        objST.iIdST = int.Parse(objSecurity.DesEncriptar());

        //Extraemos el idSaldo
        Security objSecuritySaldo = new Security(sIdSaldo);
        objST.iIdsIdSaldo = int.Parse(objSecuritySaldo.DesEncriptar());


        //Extraemos al usuario
        //Security objSecurityUser = new Security(sIdUsuario);
        objST.iIdUsuario = int.Parse(sIdUsuario);

        objST.fn_EliminarSaldoST(objST);

        // Enviar
        return objST;
    }


    /// <summary>
    /// Método para obtener los datos del cliente
    /// </summary>
    /// <param name="siIdST">Identificador encriptado de la ST</param>
    /// <returns>objCliente</returns>
    [WebMethod]
    public static Soporte_Solicitud_Transferencia fn_ObtenerDatosProvST(string siIdST)
    {

        Soporte_Solicitud_Transferencia objST = new Soporte_Solicitud_Transferencia();
        // Asignar
        Security objSecurity = new Security(siIdST);
        objST.iIdST = int.Parse(objSecurity.DesEncriptar());
        objST.fn_ObtenerDatosProvST(objST);

        //Se retorna el objeto
        return objST;
    }


}//End of the page







