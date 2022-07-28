using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Descripción breve de Acceso
/// </summary>
public class Acceso
{
    public Acceso() { }

    //Se declaran los atributos de accesos
    public int iIdRol { get; set; }
    public int iIdMenu { get; set; }
    public int iIdTipoAcceso { get; set; }
    public int[,] arrMenuTipoAcceso { get; set; }
    //Se declaran atributos generales
    public int iResultado { get; set; }
    public string sMensaje { get; set; }
    public string sContenido { get; set; }
    public int iIdUsuario { get; set; }
    public string sIdTipoAcceso { get; set; }

    /// <summary>
    /// Método para obtener la lista de los menús del sistema
    /// </summary>
    /// <param name="objAcceso"></param>
    public void fn_ObtenerListaMenus(Acceso objAcceso)
    {
        //Consulta para obtener datos 
        string sQuery = "SELECT idMenu, nomMenu, orden, icono FROM cMenu WHERE idTipoMenu=1";
        //Se crea la variable para almacenar datos
        DataSet dsDatos;
        //Se crea variable para deshabilitar radios
        string sDisabled;
        //Se desencripta el id de tipo acceso
        Security secIdTipoAcceso = new Security(objAcceso.sIdTipoAcceso);
        //Se verifica el tipo de acceso
        if (secIdTipoAcceso.DesEncriptar() == "1")
            sDisabled = "";
        else
            sDisabled = "disabled";
        //Se instancia la clase conexión
        Conexion objConexion = new Conexion();
        //Se ejecuta la consulta para obtener los embalajes
        dsDatos = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "menu");
        //Se verifica que se tengan embalajes asignados
        if (dsDatos.Tables["menu"].Rows.Count > 0)
        {
            foreach (DataRow registro in dsDatos.Tables["menu"].Rows)
            {
                //Se abre el contenedor
                objAcceso.sContenido += "<div class='col-lg-4 col-lg-md-4 col-sm-6 col-xs-12'>";
                //Se abre el encabezado del menú
                objAcceso.sContenido += "<div class='text-center'>";
                objAcceso.sContenido += "<span class='" + registro["icono"] + " fa-blue-md'></span> <label style='color:#6E6E6E;font-size:25px;'> " + registro["nomMenu"] + "</label>";
                objAcceso.sContenido += "</div>";
                //Se cierra el encabezado del menú
                //Consulta para obtener datos 
                sQuery = "SELECT idMenu, nomMenu, icono FROM cMenu WHERE idMenuPadre=" + registro["idMenu"] + " ORDER BY orden ASC";
                //Se crea la variable para almacenar datos
                DataSet dsDatos1;
                //Se ejecuta la consulta para obtener los embalajes
                dsDatos1 = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "submenu");
                //Se verifica que se tengan embalajes asignados
                if (dsDatos1.Tables["submenu"].Rows.Count > 0)
                {
                    //Se abre el arbol
                    objAcceso.sContenido += "<ul class='fordtreeview list-group'>";
                    foreach (DataRow registro1 in dsDatos1.Tables["submenu"].Rows)
                    { 
                        //Se crea el submenu
                        objAcceso.sContenido += "<li class='list-group-item'>" +
                                                    "<span class='hasSub'>" +
                                                        "<i class='fa fa-angle-right'></i> <span style='color:#6E6E6E;min-width:18px !important;' class='" + registro1["icono"] + "'></span> <label class='form-label'> " + registro1["nomMenu"] + "</label>" +
                                                    "</span>" +
                                                    "<ul>" +
                                                        "<span class='contenedorRojo'><input type='radio' class='estiloRadioAnimadoRojo' name='" + registro1["idMenu"] + "' id='" + registro1["idMenu"] + "_3' value='3' onchange='javascript:fn_CambiarTipoAccesoMenu(this.name,this.value)' checked " + sDisabled + "> <label for='" + registro1["idMenu"] + "_3'>Sin acceso</label></span>" +
                                                        "<span class='contenedorAzul'><input type='radio' class='estiloRadioAnimadoAzul' name='" + registro1["idMenu"] + "' id='" + registro1["idMenu"] + "_2' value='2' onchange='javascript:fn_CambiarTipoAccesoMenu(this.name,this.value)' " + sDisabled + "> <label for='" + registro1["idMenu"] + "_2'>Consulta</label></span>" +
                                                        "<span class='contenedorVerde'><input type='radio' class='estiloRadioAnimadoVerde' name='" + registro1["idMenu"] + "' id='" + registro1["idMenu"] + "_1' value='1' onchange='javascript:fn_CambiarTipoAccesoMenu(this.name,this.value)' " + sDisabled + "> <label for='" + registro1["idMenu"] + "_1'>Modificar</label></span>" +
                                                    "</ul>" +
                                                "</li>";
                    }
                    //Se cierra el arbol
                    objAcceso.sContenido += "</ul>";
                }
                //Se cierra el contenedor
                objAcceso.sContenido += "</div>";
            }
        }
        else
        {
            objAcceso.sContenido = "<div><div>";
        }
    }
    
    /// <summary>
    /// Método para cambiar el tipo acceso del menú
    /// </summary>
    /// <param name="objAcceso"></param>
    public void fn_CambiarTipoAccesoMenu(Acceso objAcceso)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_CambiarTipoAccesoMenu", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdRol", SqlDbType.Int, objAcceso.iIdRol.ToString());
                objConexion.agregarParametroSP("@iIdMenu", SqlDbType.Int, objAcceso.iIdMenu.ToString());
                objConexion.agregarParametroSP("@iIdTipoAcceso", SqlDbType.Int, objAcceso.iIdTipoAcceso.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objAcceso.iResultado = 1;
                    objAcceso.sMensaje = "Tipo de acceso actualizado con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objAcceso.iResultado = 0;
                    objAcceso.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objAcceso.iResultado = 0;
                objAcceso.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para cambiar obtener los accesos por rol
    /// </summary>
    /// <param name="objAcceso"></param>
    public void fn_ObtenerAccesosRol(Acceso objAcceso)
    {
        //Consulta para obtener datos
        string sQuery = "SELECT cm.idMenu, ISNULL((SELECT trm.idTipoAccesoMenu FROM tRolMenu trm WHERE trm.idMenu=cm.idMenu and trm.idRol=" + objAcceso.iIdRol + "),3) AS idTipoAccesoMenu FROM cMenu cm";
        //Se crea la variable para almacenar datos
        DataSet dsDatos;
        //Se instancia la clase conexión
        Conexion objConexion = new Conexion();
        //Se crea variable contador
        int iCont = 0;
        //Se ejecuta la consulta para obtener los datos
        dsDatos = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "acceso");
        //Se verifica que se tengan datos
        if (dsDatos.Tables["acceso"].Rows.Count > 0)
        {
            //Se da dimension al array
            objAcceso.arrMenuTipoAcceso = new int[dsDatos.Tables["acceso"].Rows.Count, 2];
            //Se recorren los registros
            foreach (DataRow registro in dsDatos.Tables["acceso"].Rows)
            {
                //Se obtienen los daots
                objAcceso.arrMenuTipoAcceso[iCont, 0] = int.Parse(registro["idMenu"].ToString());
                objAcceso.arrMenuTipoAcceso[iCont, 1] = int.Parse(registro["idTipoAccesoMenu"].ToString());
                //Se incrementa el contador
                iCont++;
            }
        }
    }
    
}