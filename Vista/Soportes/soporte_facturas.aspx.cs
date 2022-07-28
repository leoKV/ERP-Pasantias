using ExcelUpload;
using ExcelUpload.Abstract;
using IronXL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Activities;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Excel;
using ExcelDataReader;
using System.Data;
using ExcelReaderFactory = Excel.ExcelReaderFactory;

public partial class Vista_Soportes_soporte_facturas : System.Web.UI.Page
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
                string[] datos = { "Inicio", "Soporte", "Soporte Facturas" };
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
                    fn_GeneraListaFactura();
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
    public void fn_GeneraListaFactura()
    {

        ///Se instancia la clase
        Utilerias objUtilerias = new Utilerias();
        ///Se asignan los campos con filtro
        string[] arrColumnasFiltro = { "UUID", "No. Factura", "Cliente", "Proveedor", "Fecha Factura", "Monto", "Referencia Administrativa", "Estatus", "Forma Pago", "Fecha Entrada" };

        ///Se asignan los campos sin filtro
        string[] arrColumnasSinFiltro = { "Opciones de cambio" };
        ///Se pasan los parámetros
        objUtilerias.arrColumnasFiltro = arrColumnasFiltro;
        objUtilerias.arrColumnasSinFiltro = arrColumnasSinFiltro;
        objUtilerias.sNombre = "htblFacturas";
        ///Se ejecuta el método para generar estructura de tabla
        objUtilerias.fn_GeneraEstructuraTabla(objUtilerias);
        ///Se asigna el contenido a HTML
        hdvFac.InnerHtml = objUtilerias.sContenido;
    }

    #region autocompletes

    [WebMethod]
    public static List<AutoComplete> fn_GeneraAutocompleteEstatusFactura(string sCadena)
    {
        // Instancia conexión.
        Conexion oConexion = new Conexion();
        List<AutoComplete> lstResultados = new List<AutoComplete>();
        List<string> lstEstatusFactura = new List<string>();
        AutoComplete oAutoComplete;
        string sQuery = "";
        sCadena = sCadena.ToLower();

        sQuery = @"select idEstatusFactura, nomEstatusFactura
                    from cEstatusFactura where idEstatusFactura NOT IN (3,13) AND nomEstatusFactura like '%" + sCadena + @"%'";
        //}
        // Obtiene los estaus de referencia.
        lstEstatusFactura = oConexion.ejecutarConsultaRegistroMultiples(sQuery);
        // Elimina elemento de respuesta por default.
        lstEstatusFactura.RemoveAt(0);
        // Verifica que se encontraron datos.
        if (lstEstatusFactura != null && lstEstatusFactura.Count > 0)
        {
            // Agrega resultados a Modelo Autocomplete.
            for (int i = 0; i < lstEstatusFactura.Count; i += 2)
            {
                oAutoComplete = new AutoComplete();
                oAutoComplete.ID = lstEstatusFactura[i];
                oAutoComplete.nombre = lstEstatusFactura[i + 1];
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

    [WebMethod]
    public static Soporte_Facturas fn_AjustarReenvioFacturas(string UUID, string sUsuario)
    {
        Soporte_Facturas objSRF = new Soporte_Facturas();
        // Asignar
        Security objSecurity = new Security(UUID);
        objSRF.UUID = objSecurity.DesEncriptar();

        objSRF.sUsuario = int.Parse(sUsuario);

        objSRF.fn_AjustarReenvioFacturas(objSRF);

        // Enviar
        return objSRF;
    }

    public static HttpServerUtility GetServer(HttpServerUtility server)
    {
        return server;
    }
    [WebMethod]
    public static Soporte_Facturas fn_VolcadoDeDatos(string file,string sUsuario)
    {
        Soporte_Facturas objFile = new Soporte_Facturas();
        var sPath = HttpContext.Current.Server.MapPath("~//Documentos//Soporte_Facturas//");
        var path = Path.Combine(sPath, file);
        using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                reader.IsFirstRowAsColumnNames = true;
                DataSet result = reader.AsDataSet();
                var column = result.Tables[0].Columns[0].ToString();
                if (column == "UUID")
                {
                    var a = result.Tables[0].Rows.Count;
                    List<string> values = new List<string>();
                    for (int i = 0; i < a; i++)
                    {
                        var uuid = result.Tables[0].Rows[i].ItemArray[0].ToString();
                        values.Add(uuid);
                    }
                    objFile.valuesUUID = values;
                    objFile.leng = a;
                    objFile.sUsuario = int.Parse(sUsuario);
                    objFile.iResultado = 1;
                }
                else
                {
                    objFile.iResultado = 0;
                    objFile.sMensaje = "El archivo no cuenta con la información o estructura requerida";
                }
            }
        }
        objFile.fn_volcadoDatos(objFile);
        return objFile;
    }
}//End of the page
