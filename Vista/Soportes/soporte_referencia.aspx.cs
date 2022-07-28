using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vista_Soportes_estatus_referencia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Instancia a la clase SessionTimeOut
        SessionTimeOut obj_Session = new SessionTimeOut();
        //se manda llamar al metodo de validar sesion
        bool iSessionE = obj_Session.IsSessionTimedOut();
        //Constante que almacena id de menú
        //int iMENU = 6;
        //Encriptar id menú
        //Security secIdMenu = new Security(iMENU.ToString());
        //se valida la sesion
        //if (!iSessionE)
        //{
            //Inicio TRY
            try
            {
                //Se declaran los breadCrumbs
                string[] datos = { "Inicio", "Soporte", "Soporte Referencias" };
                string[] url = { "", "", "" };
                breadCrum.migajas(datos, url);
                ///recupera el id de Usuario
                string sIdUsuario = Session["iIdUsuario"].ToString();
                //Desencripta el id usuario
                Security secIdUsuario = new Security(sIdUsuario);
                int iIdUsuario = int.Parse(secIdUsuario.DesEncriptar());
                hlblIdUsuario.InnerHtml = iIdUsuario.ToString();
                ///INSTANCIA A CLASE PERMISOS (Se pasa el id del usuario y el identificador del menú)
                //Permisos objPermisos = new Permisos(sIdUsuario, iMENU);
                ///variable para almacenar el tipo de acceso
                //objPermisos.getValidaAction(objPermisos);
                ///metodo que permite verificar si el usuario tiene rol tesoreria
                //int iRol = fn_ObtenerRol(sIdUsuario);
                //se almacena variable tipo rol en label 
                //hlbliRol.InnerText = iRol.ToString();

                //Se almacena variable tipo acceso en label hidden
                //hlblTipoAcceso.Value = objPermisos.sIdTipoAcceso;
                //if (objPermisos.iIdTipoAcceso == 1 || objPermisos.iIdTipoAcceso == 2)
                //{
                //    //Se verifica si solo se tiene permisos de consulta y se ocultan campos
                //    if (objPermisos.iIdTipoAcceso == 2)
                //    {
                //        //Se verifica rol 
                //        if (iRol == 1)
                //        { //rol Administrador TI 

                            

                //        }

                //    }

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
                    ///Método para generar estructura de lista de referencias
                    fn_GeneraListaReferencias();
                    //Método para generar estructura de lista de servicios
                    //fn_GeneraListaServicios();
                    //Método para generar combo de servicios
                    //fn_GenerarComboServicios();
                    //Método para generar estructura de lista de colores
                    fn_GeneraListaColores();
                    // genera lista de guias
                    //fn_GeneraListaGuias();
                    //Método para generar estructura de la tabla de Iconos
                    //fn_GeneraListaIconos();

                //}
                /////INICIO NO TIENE ACCESO
                //else
                //{
                //    ///HAY ERROR EN EJECUCION, REDIRECCIONA A INICIO
                //    Response.Redirect("../Inicio/Inicio.aspx?sIdMenu=" + secIdMenu.Encriptar());
                //}///FIN NO TIENE ACCESO
            }
            //Inicio Catch
            catch
            {
                //si ocurre algun error se redirecciona al usuario al Inicio 
                //Response.Redirect("../Inicio/Inicio.aspx?sIdMenu=" + secIdMenu.Encriptar());
            }
        //}
        //else
        //{
        //    //si se termino el tiempo de sesion se redirecciona al Login y cierra sesión
        //    Session.Clear();
        //    Session.Abandon();
        //    Response.Redirect("../../Login.aspx");
        //}
    }



    /// <summary>
    /// Metodo que devuelve si el usuario tiene rol Administrador TI 
    /// </summary>
    /// <param name="idUsuario"></param>
    /// <returns></returns>
    public static int fn_ObtenerRol(string sIdUsuario)
    {
        //objeto para desencriptar el idSubReferencia
        Security secIdUsuario = new Security(sIdUsuario);
        //se obtiene el id del usuario 
        int iIdUsuario = int.Parse(secIdUsuario.DesEncriptar());
        //variable para retornar
        int iRol = 0;

        //Se Instancia la clase
        Conexion objConexion = new Conexion();
        //Se crea la consulta para verificar si el menu tiene acceso a rol Administrador TI
        string sQuery = "select count(idRolMenu) from tRolMenu where idRol = 7 and idMenu = 6";
        //Se obtiene el resultado
        string[] sRespuesta = objConexion.ejecutarConsultaRegistroSimple(sQuery);

        //se verifica si existe relación de rol con menu
        if (sRespuesta[1].ToString() != "0")
        { //si existe

            //Se crea la consulta para verificar si el usuario tiene el rol de Administrador TI
            string sQuery1 = " select count(idRolUsuarioComitente) from tUsuarioComitenteRol where idRol = 7 and idUsuarioComitente " +
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
    /// Método para generar estructura de lista de recepción de referencias
    /// </summary>
    public void fn_GeneraListaReferencias()
    {

        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "Referencia administrativa", "Referencia operativa", "Estatus" };
        ///Se asignan los campos sin filtro
        string[] arrColumnasSinFiltro = { "Opciones de cambio" };
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblReferencias";
        ///Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
        hdvReferencias.InnerHtml = objUtilerias.sContenido;
    }

    /// <summary>
    /// Método para generar estructura de lista de colores
    /// </summary>
    public void fn_GeneraListaColores()
    {
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "Estatus", "Descripción" };
        ///Se asignan los campos sin filtro
        string[] arrColumnasSinFiltro = { };
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblColores";
        ///Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
        hdvColores.InnerHtml = objUtilerias.sContenido;
    }

    /// <summary>
    /// Método para obtener la relación de servicios
    /// </summary>
    /// <param name="iIdReferencia">Identificador de la referencia</param>
    /// <returns>objOficina</returns>
    [WebMethod]
    public static Referencia fn_ObtenerRelacionServicios(string iIdSubReferencia)
    {
        iIdSubReferencia = iIdSubReferencia.Trim();
        //Se instancia la clase
        Security objSecuruty = new Security(iIdSubReferencia);
        //Se instancia la clase
        Referencia objReferencia = new Referencia();
        //Se pasan los parametros
        objReferencia.iIdSubReferencia = int.Parse(objSecuruty.DesEncriptar());
        //Se ejecura el método
        objReferencia.fn_ObtenerRelacionServicios(objReferencia);
        //Se retorna el objeto
        return objReferencia;
    }

    /// <summary>
    /// Método para generar estructura de lista de servicios
    /// </summary>
    public void fn_GeneraListaServicios()
    {
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "Servicio" };
        ///Se asignan los campos sin filtro
        string[] arrColumnasSinFiltro = { "Aduana", "Cliente", "Estatus", "Cancelar" };
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblServicios";
        ///Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
        //hdvTablaServicios.InnerHtml = objUtilerias.sContenido;
    }

    /// <summary>
    /// Método para generar combo de servicios
    /// </summary>
    public void fn_GenerarComboServicios()
    {
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se pasan los parámetros
        objUtilerias.sNombre = "hslcServicios";
        objUtilerias.sQuery = "SELECT idServicio, CASE WHEN cs.cveSunServicio is null or cs.cveSunServicio = '' THEN cs.descripcion ELSE cs.cveSunServicio + ' - ' + cs.descripcion END FROM cServicio cs";
        ///Se ejecuta el método para generar estructura del combo
        objUtilerias.fn_GeneraCombobox(objUtilerias);
        ///Se asigna el contenido a HTML
        //hdvComboServicios.InnerHtml = objUtilerias.sContenido;
    }

    /// <summary>
    /// Método para validar que no exista el servicio referencia
    /// </summary>
    /// <param name="iIdReferencia">Identificador de la referencia</param>
    /// <param name="iIdServicio">Identificador del servicio</param>
    /// <returns>objOficina</returns>
    [WebMethod]
    public static Referencia fn_ValidarServicioReferernciaExiste(int iIdSubReferencia, int iIdServicio)
    {
        //Se instancia clase referencia
        Referencia objReferencia = new Referencia();
        //Se pasan los parametros
        objReferencia.iIdSubReferencia = iIdSubReferencia;
        objReferencia.iIdServicio = iIdServicio;
        //Se ejecuta el método para validar cliente oficina
        objReferencia.fn_ValidarServicioReferernciaExiste(objReferencia);
        //Se retorna el objeto
        return objReferencia;
    }

    /// <summary>
    /// Método para guardar el servicio de la referencia
    /// </summary>
    /// <param name="iIdReferencia">Identificador de la referencia</param>
    /// <param name="iIdServicio">Identificador del servicio</param>
    /// <returns>objOficina</returns>
    [WebMethod]
    public static Referencia fn_GuardarServicioReferencia(int iIdSubReferencia, int iIdServicio)
    {
        //Se instancia clase referencia
        Referencia objReferencia = new Referencia();
        //Se pasan los parametros
        objReferencia.iIdSubReferencia = iIdSubReferencia;
        objReferencia.iIdServicio = iIdServicio;
        //Se ejecuta el método para guardar 
        objReferencia.fn_GuardarServicioReferencia(objReferencia);
        //Se retorna el objeto
        return objReferencia;
    }

    /// <summary>
    /// Método para crear relación servicio aduana
    /// </summary>
    /// <param name="iIdReferencia">Identificador de la referencia</param>
    /// <param name="iIdServicio">Identificador del servicio</param>
    /// <returns>objOficina</returns>
    [WebMethod]
    public static Referencia fn_RelacionServicioAduana(int iIdSubReferencia, int iIdServicio, int iType)
    {
        //Se instancia clase referencia
        Referencia objReferencia = new Referencia();
        //Se pasan los parametros
        objReferencia.iIdSubReferencia = iIdSubReferencia;
        objReferencia.iIdServicio = iIdServicio;
        objReferencia.iType = iType;
        //Se ejecuta el método para guardar cliente oficina
        objReferencia.fn_RelacionServicioAduana(objReferencia);
        //Se retorna el objeto
        return objReferencia;
    }

    /// <summary>
    /// Método para crear relación servicio cliente
    /// </summary>
    /// <param name="iIdReferencia">Identificador de la referencia</param>
    /// <param name="iIdServicio">Identificador del servicio</param>
    /// <returns>objOficina</returns>
    [WebMethod]
    public static Referencia fn_RelacionServicioCliente(int iIdSubReferencia, int iIdServicio, int iType)
    {
        //Se instancia clase referencia
        Referencia objReferencia = new Referencia();
        //Se pasan los parametros
        objReferencia.iIdSubReferencia = iIdSubReferencia;
        objReferencia.iIdServicio = iIdServicio;
        objReferencia.iType = iType;
        //Se ejecuta el método para guardar cliente oficina
        objReferencia.fn_RelacionServicioCliente(objReferencia);
        //Se retorna el objeto
        return objReferencia;
    }

    /// <summary>
    /// Método para cambiar el estatus del servicio de la referencia
    /// </summary>
    /// <param name="iIdServicioReferencia">Identificador del servicio de la referencia</param>
    /// <returns>objOficina</returns>
    [WebMethod]
    public static Referencia fn_CambiarEstatusServicio(int iIdServicioReferencia, string sMotivo, int iType)
    {
        //Se instancia clase referencia
        Referencia objReferencia = new Referencia();
        //Se pasan los parametros
        objReferencia.iIdServicioReferencia = iIdServicioReferencia;
        objReferencia.sMotivo = sMotivo;
        objReferencia.iType = iType;
        //Se ejecuta el método
        objReferencia.fn_CambiarEstatusServicio(objReferencia);
        //Se retorna el objeto
        return objReferencia;
    }

    /// <summary>
    /// Método para cambiar el estatus del servicio de la referencia
    /// </summary>
    /// <param name="sIdSubReferencia">Identificador  de la referencia</param>
    /// <returns>objOficina</returns>
    [WebMethod]
    public static Referencia fn_CambiarEstatusReferencia(string sIdUsuario, int sIdSubReferencia, string sIdEstatus, string sMotivo)
    {

        //Se instancia clase referencia

        //Security objSecurity = new Security(sIdUsuario);
        //int iIdUsuario = int.Parse(objSecurity.DesEncriptar());

        int iIdUsuario = int.Parse(sIdEstatus);
        Referencia objReferencia = new Referencia();

        //Se pasan los parametros
        objReferencia.iIdReferencia  = sIdSubReferencia;
        objReferencia.sMotivo = sMotivo;
        objReferencia.sEstatus = sIdEstatus;
        //Se ejecuta el método
        objReferencia.fn_CambiarEstatusReferencia(objReferencia, iIdUsuario);
        //Se retorna el objeto
        return objReferencia;
    }



    /// <summary>
    /// Método para estatus de la referencia
    /// </summary>
    /// <param name="sIdSubReferencia">Identificador encriptado de la referencia</param>
    /// <returns>objReferencia</returns>
    [WebMethod]
    public static Referencia fn_ObtenerEstatusReferencia(int iIdSubReferencia)
    {


        //Se instancia clase referencia
        Referencia objReferencia = new Referencia();
        //Se pasan los parametros
        objReferencia.iIdSubReferencia = iIdSubReferencia;

        //Se ejecuta el método para obtener datos de la Comitente
        objReferencia.fn_ObtenerEstatusReferencia(objReferencia);
        //Se retorna el objeto
        return objReferencia;
    }
    /// <summary>
    /// Método para generar estructura de lista de guias
    /// </summary>
    public void fn_GeneraListaGuias()
    {
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "Guia Master", "Guia House" };
        ///Se asignan los campos sin filtro
        string[] arrColumnasSinFiltro = { };
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblGuias";
        ///Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
        hdvGuias.InnerHtml = objUtilerias.sContenido;
    }
    /// <summary>
    /// metodo para obtener datos de la subReferencia
    /// </summary>
    /// <param name="iIdSubReferencia"></param>
    /// <returns></returns>
    [WebMethod]
    public static Referencia fn_DatosSubReferencia(int iIdSubReferencia)
    {
        //Se instancia clase referencia
        Referencia objReferencia = new Referencia();
        //Se pasan los parametros
        objReferencia.iIdSubReferencia = iIdSubReferencia;
        //Se ejecuta el método para guardar cliente oficina
        objReferencia.fn_ObtenerDatosReferencia(objReferencia);
        //Se retorna el objeto
        return objReferencia;
    }

    /// <summary>
    /// Método para generar estructura de lista de iconos
    /// </summary>
    public void fn_GeneraListaIconos()
    {
        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "Icono", "Descripción" };
        ///Se asignan los campos sin filtro
        string[] arrColumnasSinFiltro = { };
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblIconos";
        ///Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
       // hdvIconos.InnerHtml = objUtilerias.sContenido;
    }


    [WebMethod]
    public static int fn_MostrarBotonCierreRefs(string sUsuario)
    {
        Conexion objConexion = new Conexion();
        string sQuery = @"SELECT CASE WHEN " + sUsuario + @" IN (SELECT DISTINCT idusuario 
                            FROM tUsuarioComitente
                            WHERE idUsuarioComitente IN(SELECT idUsuarioComitente

                                                        FROM tUsuarioComitenteRol WHERE idRol = 14
                            )) THEN '1' ELSE '0' END ";
        string[] sRes;

        sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        if (sRes[0] == "1")
        {
            return int.Parse(sRes[1]);
        }
        else
        {
            return 2;
        }
    }

    #region FILTROS REFERENCIA
    [WebMethod]
    public static List<AutoComplete> fn_GeneraAutocompleteAduana(string sCadena)
    {
        // Instancia conexión.
        Conexion oConexion = new Conexion();
        List<AutoComplete> lstResultados = new List<AutoComplete>();
        List<string> lstAduanas = new List<string>();
        AutoComplete oAutoComplete;
        string sQuery = "";
        sCadena = sCadena.ToLower();
        sQuery = @"select ca.idAduana, ca.aduana + '-'  + ca.denominacion
                    from cAduana ca
                    where ca.aduana + '-'  + ca.denominacion like '%" + sCadena + @"%'
                    group by ca.idAduana, ca.aduana, ca.denominacion";

        // Obtiene las aduanas.
        lstAduanas = oConexion.ejecutarConsultaRegistroMultiples(sQuery);
        // Elimina elemento de respuesta por default.
        lstAduanas.RemoveAt(0);
        // Verifica que se encontraron datos.
        if (lstAduanas != null && lstAduanas.Count > 0)
        {
            // Agrega resultados a Modelo Autocomplete.
            for (int i = 0; i < lstAduanas.Count; i += 2)
            {
                oAutoComplete = new AutoComplete();
                oAutoComplete.ID = lstAduanas[i];
                oAutoComplete.nombre = lstAduanas[i + 1];
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
    public static List<AutoComplete> fn_GeneraAutocompleteCliente(string sCadena)
    {
        // Instancia conexión.
        Conexion oConexion = new Conexion();
        List<AutoComplete> lstResultados = new List<AutoComplete>();
        List<string> lstClientes = new List<string>();
        AutoComplete oAutoComplete;
        string sQuery = "";
        sCadena = sCadena.ToLower();

        sQuery = @"select idCliente, nomCliente
                    from tCliente where nomCliente like '%" + sCadena + @"%'";

        // Obtiene los clientes.
        lstClientes = oConexion.ejecutarConsultaRegistroMultiples(sQuery);
        // Elimina elemento de respuesta por default.
        lstClientes.RemoveAt(0);
        // Verifica que se encontraron datos.
        if (lstClientes != null && lstClientes.Count > 0)
        {
            // Agrega resultados a Modelo Autocomplete.
            for (int i = 0; i < lstClientes.Count; i += 2)
            {
                oAutoComplete = new AutoComplete();
                Security oSCliente = new Security(lstClientes[i]);
                oAutoComplete.ID = oSCliente.Encriptar();
                oAutoComplete.nombre = lstClientes[i + 1];
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
    public static List<AutoComplete> fn_GeneraAutocompleteEstatusReferencia(string sCadena)
    {
        // Instancia conexión.
        Conexion oConexion = new Conexion();
        List<AutoComplete> lstResultados = new List<AutoComplete>();
        List<string> lstEstatusReferencia = new List<string>();
        AutoComplete oAutoComplete;
        string sQuery = "";
        sCadena = sCadena.ToLower();

        //if (sOpcion != 0)
        //{

        //    sQuery = @"select idEstatusReferencia, nomEstatusReferencia
        //            from cEstatusReferencia where idEstatusReferencia not in (" + sOpcion + ") AND nomEstatusReferencia like '%" + sCadena + @"%' ";
        //}
        //else
        //{
            sQuery = @"select idEstatusReferencia, nomEstatusReferencia
                    from cEstatusReferencia where nomEstatusReferencia like '%" + sCadena + @"%'";
        //}
        // Obtiene los estaus de referencia.
        lstEstatusReferencia = oConexion.ejecutarConsultaRegistroMultiples(sQuery);
        // Elimina elemento de respuesta por default.
        lstEstatusReferencia.RemoveAt(0);
        // Verifica que se encontraron datos.
        if (lstEstatusReferencia != null && lstEstatusReferencia.Count > 0)
        {
            // Agrega resultados a Modelo Autocomplete.
            for (int i = 0; i < lstEstatusReferencia.Count; i += 2)
            {
                oAutoComplete = new AutoComplete();
                oAutoComplete.ID = lstEstatusReferencia[i];
                oAutoComplete.nombre = lstEstatusReferencia[i + 1];
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
    public static List<AutoComplete> fn_GeneraAutocompleteTipoReferencia(string sCadena)
    {
        // Instancia conexión.
        Conexion oConexion = new Conexion();
        List<AutoComplete> lstResultados = new List<AutoComplete>();
        List<string> lstTipoReferencia = new List<string>();
        AutoComplete oAutoComplete;
        string sQuery = "";
        sCadena = sCadena.ToLower();

        sQuery = @"select idTipoReferencia, nomTipoReferencia
                    from cTipoReferencia where nomTipoReferencia like '%" + sCadena + @"%'";

        // Obtiene las aduanas.
        lstTipoReferencia = oConexion.ejecutarConsultaRegistroMultiples(sQuery);
        // Elimina elemento de respuesta por default.
        lstTipoReferencia.RemoveAt(0);
        // Verifica que se encontraron datos.
        if (lstTipoReferencia != null && lstTipoReferencia.Count > 0)
        {
            // Agrega resultados a Modelo Autocomplete.
            for (int i = 0; i < lstTipoReferencia.Count; i += 2)
            {
                oAutoComplete = new AutoComplete();
                oAutoComplete.ID = lstTipoReferencia[i];
                oAutoComplete.nombre = lstTipoReferencia[i + 1];
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
    #endregion


    #region
    [WebMethod]
    public static Referencia fn_Cliente(int sRef)
    {
        //Se desencripta el id del usuario
        //Security secIdUsuario;
        //secIdUsuario = new Security(sIdUsuario);
        //Se instancia clase usuario
        Referencia objReferencia = new Referencia();

        objReferencia.iIdSubReferencia = sRef;

        //Se pasan los parametros
        //Se ejecuta el método para obtener datos del usuario
        //objReferencia.fn_Cliente(objReferencia);
        //Se retorna el objeto
        return objReferencia;


    }
    #endregion


    [WebMethod]
    public static List<AutoComplete> fn_GeneraAutocompleteClientes(string sCadena)
    {
        // Instancia conexión.
        Conexion oConexion = new Conexion();
        List<AutoComplete> lstResultados = new List<AutoComplete>();
        List<string> lstClientes = new List<string>();
        AutoComplete oAutoComplete;
        string sQuery = "";
        sCadena = sCadena.ToLower();

        sQuery = @"select idCliente, nomCliente +N' RFC: '+ rfc 
                    from tCliente where nomCliente like '%" + sCadena + @"%'";

        // Obtiene los clientes.
        lstClientes = oConexion.ejecutarConsultaRegistroMultiples(sQuery);
        // Elimina elemento de respuesta por default.
        lstClientes.RemoveAt(0);
        // Verifica que se encontraron datos.
        if (lstClientes != null && lstClientes.Count > 0)
        {
            // Agrega resultados a Modelo Autocomplete.
            for (int i = 0; i < lstClientes.Count; i += 2)
            {
                oAutoComplete = new AutoComplete();
                Security oSCliente = new Security(lstClientes[i]);
                oAutoComplete.ID = oSCliente.Encriptar();
                oAutoComplete.nombre = lstClientes[i + 1];
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

    /// <summary>
    /// Método para cambiar el cliente de la referencia
    /// </summary>
    /// <param name="sIdSubReferencia">Identificador  de la referencia</param>
    /// <returns>objOficina</returns>
    [WebMethod]
    public static Referencia fn_GuardarClienteReferencia(string sIdUsuario, int sIdRef,int sOpcion, string idCC, string idCO)
    {

        //Se instancia clase referencia

        Security objSecurity = new Security(sIdUsuario);
        int iIdUsuario = int.Parse(objSecurity.DesEncriptar());



        ///Desencriptamos el cliente contable
        Security objSecurityCC = new Security(idCC);
        int idClienteC = int.Parse(objSecurityCC.DesEncriptar());

        ///Desencriptamos el cliente Operativo
        Security objSecurityCO = new Security(idCO);
        int idClienteO = int.Parse(objSecurityCO.DesEncriptar());

        //int iIdUsuario = int.Parse(sIdEstatus);
        Referencia objReferencia = new Referencia();

        //Se pasan los parametros
        objReferencia.iIdReferencia = sIdRef;
        objReferencia.iIdClienteContable = idClienteC;
        objReferencia.iIdClienteOperativo = idClienteO;


        //Se ejecuta el método
        //objReferencia.fn_CambiarClienteReferencia(objReferencia, iIdUsuario, sOpcion);
        //Se retorna el objeto
        return objReferencia;
    }

    [WebMethod]
    public static Soporte fn_CambiarEstatusRef(string sIdUsuario, int sIdSubReferencia, int sIdEstatus)
    {

        //Se instancia clase referencia

        Security objSecurity = new Security(sIdUsuario);
        int iIdUsuario = 1;

        //int iIdUsuario = int.Parse(sIdEstatus);
        //Referencia objReferencia = new Referencia();

        Soporte objSoporte = new Soporte();

        ////Se pasan los parametros
        //objReferencia.iIdReferencia = sIdSubReferencia;
        ////objReferencia.sMotivo = sMotivo;
        //objReferencia.sEstatus = sIdEstatus;
        objSoporte.iIdUsuario = iIdUsuario;
        objSoporte.iIdSubReferencia = sIdSubReferencia;
        objSoporte.iIdEstatus = sIdEstatus;

        //Se ejecuta el método
        objSoporte.fn_CambiarEstatusReferencia(objSoporte);
        //Se retorna el objeto
        return objSoporte;
    }



}