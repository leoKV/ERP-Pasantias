using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de DetalleBitacora
/// </summary>
public class DetalleBitacora
{

    public int iIdRegistroBitacora { get; set; }
    public string sUrl { get; set; }
    public string sPaginaVisitada { get; set; }
    public DateTime dFechaVisita { get; set; }
    public int iResultado { get; set; }
    public string sMensaje { get; set; }

    public void fn_NuevoDetalleBitacora(DetalleBitacora objDetalleBitacora)
    {
        //Se instancia la clase conexión
        Conexion objConexion = new Conexion();
        //Se verifica que exista el sp
        string sRes = objConexion.generarSP("pa_GuardarDetalleBitacora", 0);
        string[] sResOut = new string[2];
        // se comprara el resultado devuelto de objConexion.generarSP
        if (sRes == "1")
        {
            try
            {
                objConexion.agregarParametroSP("@iIdRegistroBitacora", SqlDbType.Int, objDetalleBitacora.iIdRegistroBitacora.ToString());
                objConexion.agregarParametroSP("@sUrl", SqlDbType.VarChar, objDetalleBitacora.sUrl.ToString());
                objConexion.agregarParametroSP("@sPaginaVisitada", SqlDbType.VarChar, objDetalleBitacora.sPaginaVisitada.ToString());
                objConexion.agregarParametroSP("@dFechaVisita", SqlDbType.DateTime, objDetalleBitacora.dFechaVisita.ToString());
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@sResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objDetalleBitacora.iResultado = 1;
                    objDetalleBitacora.sMensaje = "Registro guardado con éxito.";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objDetalleBitacora.iResultado = 0;
                    objDetalleBitacora.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se retorna el mensaje de error
                objDetalleBitacora.iResultado = 0;
                objDetalleBitacora.sMensaje = sResOut[0];
            }
        }
    }


    //Metodo para conseguir la url de la pagina visitada
    #region Metodo para obtener la URL de las paginas
    public string fn_ObtenerPagina()
    {
        try
        {
            // Se asigna a la variable lo contenido en la Url 
            string sCadena = HttpContext.Current.Request.Url.AbsoluteUri;
            // Se dividen las partes de la cadena origina cada que se encuentre un '/'
            // y se almacenan en un arreglo
            string[] sSeparado = sCadena.Split('/');
            // De el arreglo sSeparado se toma el contenido correspondiente al ultimo indice
            // lo cual nos indica el nombre de la pagina visitada
            string sCadenaRecortada = sSeparado[sSeparado.Length - 1];
            //Nuevamente de corta la cadena cuando se encuentre un '.'
            string[] sSeparadoDos = sCadenaRecortada.Split('.');
            //Del arreglo sSeparadoDos se optiene el contenido correspondiente
            string sPaginaVisitada = sSeparadoDos[sSeparadoDos.Length - 2];
            // Se retorna la cadena
            return sPaginaVisitada;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }
    #endregion
    //******

    //Metodo para conseguir la url de la pagina visitada
    #region Metodo para obtener la URL de las paginas
    public string fn_ObtenerUrl()
    {
        try
        {
            // Se asigna a la variable lo contenido en la Url 
            string sCadena = HttpContext.Current.Request.Url.AbsoluteUri;
            // Se asigna la cadena de la url a la variable             
            string sUrl = sCadena;
            // Se retorna la cadena
            return sUrl;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }
    #endregion
    //******

	public DetalleBitacora()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}