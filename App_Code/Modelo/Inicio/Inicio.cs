using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Inicio
/// </summary>
public class Inicio
{
	public Inicio() { }

    //Se crean atributos de la clase
    public int iIdMenu { get; set; }
    public int iIdUsuarioPantalla { get; set; }
    public string sQuery { get; set; }
    //Se crean los atributos generales
    public int iResultado { get; set; }
    public string sMensaje { get; set; }
    public string sContenido { get; set; }
    public int iIdUsuario { get; set; }

    /// <summary>
    /// Método para generar el menú
    /// </summary>
    /// <param name="objInicio">Objeto inicio</param>
    public void fn_GenerarMenu(Inicio objInicio) {
        //Consulta para obtener datos 
        string sQuery = "SELECT idMenu, nomMenu, orden, icono FROM cMenu cm WHERE idTipoMenu=1 AND idMenu = 92 and (SELECT COUNT(*) FROM cMenu c WHERE c.idMenuPadre=cm.idMenu and c.idMenu in (SELECT trm.idMenu FROM tRolMenu trm WHERE trm.idRol in (SELECT tucr.idRol FROM tUsuarioComitenteRol tucr WHERE tucr.idUsuarioComitente in (SELECT tuo.idUsuarioComitente FROM tUsuarioComitente tuo WHERE tuo.idUsuario=" + objInicio.iIdUsuario + ")))) != 0";
        //Se crea la variable para almacenar datos
        DataSet dsDatos;
        //Se instancia la clase conexión
        Conexion objConexion = new Conexion();
        //Se ejecuta la consulta para obtener los datos
        dsDatos = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "menu");
        //Se verifica que se tengan datos
        if (dsDatos.Tables["menu"].Rows.Count > 0)
        {
            //Se abre el contenedor
            objInicio.sContenido += "<div id='navbar' class='navbar-collapse collapse' aria-expanded='false' style='height: 0px;'>";
            //Se abre el encabezado del menú
            objInicio.sContenido += "<ul class='nav navbar-nav'>";
            objInicio.sContenido += "<li class=''><a href='../Inicio/Inicio.aspx'>Inicio</a></li>";
            foreach (DataRow registro in dsDatos.Tables["menu"].Rows)
            {
                //Se abre el arbol
                objInicio.sContenido += "<li class=''><a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false'>" + registro["nomMenu"] + "<span class='caret'></span></a>";
                //se construye el submuenu
                fn_ConstruyeSubMenu(registro,objConexion,objInicio);
                //Se cierra el arbol
                objInicio.sContenido += "</li>";
            }
            //Se cierra el arbol
            objInicio.sContenido += "</ul>";
            //Se cierra el contenedor
            objInicio.sContenido += "</div>";
        }
        else
        {
            objInicio.sContenido = "<div><div>";
        }
    }

    /// <summary>
    /// Método recursivo para construir submenu de submenu
    /// </summary>
    /// <param name="registro"></param>
    /// <param name="objConexion"></param>
    /// <param name="objInicio"></param>
    public void fn_ConstruyeSubMenu(DataRow registro, Conexion objConexion, Inicio objInicio)
    {

        //Consulta para obtener datos 
        sQuery = "SELECT cm.idMenu, cm.nomMenu, cm.icono, cm.rutaMenu FROM cMenu cm WHERE cm.idMenuPadre=" + registro["idMenu"] + " and cm.idMenu in (SELECT trm.idMenu FROM tRolMenu trm WHERE trm.idTipoAccesoMenu in (1,2) and trm.idRol in ((SELECT tuor.idRol FROM tUsuarioComitenteRol tuor WHERE tuor.idUsuarioComitente in (SELECT tuo.idUsuarioComitente FROM tUsuarioComitente tuo WHERE tuo.idUsuario=" + objInicio.iIdUsuario + ")))) ORDER BY orden ASC";
        //Se crea la variable para almacenar datos
        DataSet dsDatos1;
        //Se ejecuta la consulta para obtener los embalajes
        dsDatos1 = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "submenu");
        //Se verifica que se tengan embalajes asignados
        if (dsDatos1.Tables["submenu"].Rows.Count > 0)
        {
            objInicio.sContenido += "<ul class='dropdown-menu'>";
            foreach (DataRow registro1 in dsDatos1.Tables["submenu"].Rows)
            {

                string[] sRes = objConexion.ejecutarConsultaRegistroSimple("select COUNT(*) from cMenu where idMenuPadre = "+registro1["idMenu"]+"");
                
                //submenus normales
                if (sRes[1] == "0")
                {
                    //Se abre el submenu
                    objInicio.sContenido += "<li><a href='" + registro1["rutaMenu"] + "'><span class='" + registro1["icono"] + " fa-blue-sm'></span> " + registro1["nomMenu"] + "</a>";

                    //Se cierra el submenu
                    objInicio.sContenido += "</li>";
                }
                else
                { //submenus con submenus 

                    //Se abre el submenu
                    objInicio.sContenido += "<li class='dropdown-submenu'><a href='" + registro1["rutaMenu"] + "'><span class='" + registro1["icono"] + " fa-blue-sm'></span> " + registro1["nomMenu"] + "</a>";

                    fn_ConstruyeSubMenu(registro1, objConexion, objInicio);

                    //Se cierra el submenu
                    objInicio.sContenido += "</li>";

                }
            }
            objInicio.sContenido += "</ul>";
        }
    }

    /// <summary>
    /// Método para validar que la la pantalla no este asignada al usuario
    /// </summary>
    /// <param name="objInicio"></param>
    public void fn_ValidarUsuarioPantallaExiste(Inicio objInicio)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery
        string sQuery = "SELECT COUNT(*) FROM tUsuarioPantallaInicio tupi WHERE tupi.idUsuario=" + objInicio.iIdUsuario + " and tupi.idMenu=" + objInicio.iIdMenu;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el iResultado 
        objInicio.iResultado = int.Parse(sRes[1]);
        //Se retorna mensaje en caso de que exista la pantalla
        objInicio.sMensaje = "La pantalla ya esta asignada al usuario.";
    }

    /// <summary>
    /// Método para guardar la pantalla del usuario
    /// </summary>
    /// <param name="objInicio"></param>
    public void fn_GuardarUsuarioPantalla(Inicio objInicio)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarUsuarioPantalla", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdUsuario", SqlDbType.Int, objInicio.iIdUsuario.ToString());
                objConexion.agregarParametroSP("@iIdMenu", SqlDbType.Int, objInicio.iIdMenu.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objInicio.iResultado = 1;
                    objInicio.sMensaje = "Pantalla asignada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objInicio.iResultado = 0;
                    objInicio.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objInicio.iResultado = 0;
                objInicio.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para eliminar la pantalla del usuario
    /// </summary>
    /// <param name="objInicio"></param>
    public void fn_EliminarUsuarioPantalla(Inicio objInicio)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_EliminarUsuarioPantalla", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdUsuarioPantalla", SqlDbType.Int, objInicio.iIdUsuarioPantalla.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objInicio.iResultado = 1;
                    objInicio.sMensaje = "Pantalla eliminada con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objInicio.iResultado = 0;
                    objInicio.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objInicio.iResultado = 0;
                objInicio.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para obtener pantallas inicio
    /// </summary>
    /// <param name="objInicio">Objeto inicio</param>
    public void fn_ObtenerPantallasUsuario(Inicio objInicio)
    {
        //Consulta para obtener datos 
        string sQuery = "SELECT cm.nomMenu, cm.rutaMenu, cm.icono FROM tUsuarioPantallaInicio tupi inner join cMenu cm ON cm.idMenu=tupi.idMenu WHERE tupi.idUsuario=" + objInicio.iIdUsuario + " and tupi.idUsuario in (SELECT tuo.idUsuario FROM tUsuarioComitente tuo WHERE tuo.idUsuarioComitente in (SELECT tuor.idUsuarioComitente FROM tUsuarioComitenteRol tuor WHERE tuor.idRol in (SELECT trm.idRol FROM tRolMenu trm WHERE trm.idTipoAccesoMenu in (1,2) and trm.idMenu=cm.idMenu))) ORDER BY cm.idMenuPadre ASC,cm.orden ASC";
        //Se crea la variable para almacenar datos
        DataSet dsDatos;
        //Se instancia la clase conexión
        Conexion objConexion = new Conexion();
        //Se ejecuta la consulta para obtener los datos
        dsDatos = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "pantalla");
        //Se verifica que se tengan datos
        if (dsDatos.Tables["pantalla"].Rows.Count > 0)
        {
            //Se abre el contenedor
            objInicio.sContenido += "<div class='container-fluid'>";
            //Se abre la fila
            objInicio.sContenido += "<div class='row'>";
            foreach (DataRow registro in dsDatos.Tables["pantalla"].Rows)
            {
                objInicio.sContenido += "<a href=" + registro["rutaMenu"] + ">";
                objInicio.sContenido += "<div class='col-lg-2 col-md-3 col-sm-6 col-xs-6 text-center fondo-gris-1' style='margin:15px;'>";
                objInicio.sContenido += "<span class='" + registro["icono"] + " fa-green-xlg'></span><br />";
                objInicio.sContenido += "<label class='form-label'>" + registro["nomMenu"] + "</label>";
                objInicio.sContenido += "</div>";
                objInicio.sContenido += "</a>";
            }
            //Se cierra la fila
            objInicio.sContenido += "</div>";
            //Se cierra el contenedor
            objInicio.sContenido += "</div>";
        }
        else
        {
            objInicio.sContenido = "<div><div>";
        }
    }
    
    /// <summary>
    /// Método para obtener contenido alerta sin acceso menú
    /// </summary>
    /// <param name="objInicio"></param>
    public void fn_AlertaSinAccesoMenu(Inicio objInicio)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery
        string sQuery = "SELECT cm.nomMenu FROM cMenu cm WHERE cm.idMenu=" + objInicio.iIdMenu;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna nombre de menú
        objInicio.sMensaje = sRes[1];
    }

}