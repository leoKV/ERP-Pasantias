using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using Org.BouncyCastle.Asn1.Ocsp;

/// <summary>
/// Descripción breve de BitacoraAcceso
/// </summary>
public class BitacoraAcceso
{   
    //atributos de la clase
    public int iIdBitacora { get; set; }
    public int iIdUsuario { get; set; }
    public string sNombreHost { get; set; }
    public string sIpLocal { get; set; }
    public string sIpPublica { get; set; }
    public DateTime dFechaLogin { get; set; }
    public DateTime dfechaLogout { get; set; }
    public string sResGenerarSP { get; set; }
    public int iResultado { get; set; }
    public string sMensaje { get; set; }


    public void fn_NuevoRegistroBitacora(BitacoraAcceso objBitacoraAcceso) {
        //Se instancia la clase conexión
        Conexion objConexion = new Conexion();
        //Se verifica que exista el sp
        string sRes = objConexion.generarSP("pa_GuardarBitacoraAcceso", 0);
        //Se declara un arreglo donde se almacena el resultado de ejecutar el sp [0]=1) correcto 0) incorrecto, [1]=valor de retorno
        string[] sResOut = new string[2];
        // se comprara el resultado devuelto de objConexion.generarSP
        if (sRes == "1")
        {
            try {
                objConexion.agregarParametroSP("@iIdRegistroBitacora", SqlDbType.Int, objBitacoraAcceso.iIdBitacora.ToString());
                objConexion.agregarParametroSP("@iIdUsuaio", SqlDbType.Int, objBitacoraAcceso.iIdUsuario.ToString());
                objConexion.agregarParametroSP("@sNombreHost", SqlDbType.VarChar, objBitacoraAcceso.sNombreHost.ToString());
                objConexion.agregarParametroSP("@sIpLocal", SqlDbType.VarChar, objBitacoraAcceso.sIpLocal.ToString());
                objConexion.agregarParametroSP("@sIpPublica", SqlDbType.VarChar, objBitacoraAcceso.sIpPublica.ToString());
                objConexion.agregarParametroSP("@dFechaLogin", SqlDbType.DateTime, objBitacoraAcceso.dFechaLogin.ToString());
                objConexion.agregarParametroSP("@dFechaLogout", SqlDbType.DateTime, objBitacoraAcceso.dfechaLogout.ToString());
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objBitacoraAcceso.iResultado = 1;
                    objBitacoraAcceso.sMensaje = "Registro guardado con éxito.";

                    objBitacoraAcceso.iIdBitacora = Convert.ToInt32((sResOut[1].ToString()));
                    //objBitacoraAcceso.iIdBitacora = int.Parse(sResOut[1].ToString());

                }
                else
                {
                    //Se retorna el mensaje de error
                    objBitacoraAcceso.iResultado = 0;
                    objBitacoraAcceso.sMensaje = sResOut[0];
                }
                               
            }catch(Exception ex){
                //Se retorna el mensaje de error
                objBitacoraAcceso.iResultado = 0;
                objBitacoraAcceso.sMensaje = sResOut[0];
            }
        }
    }

    //Metodo para conseguir la ip Publica
    #region Metodo para conseguir la ip Publica
    public string rsRecuperaIPPublica()
    {
        //Nuevo codigo
        var context = System.Web.HttpContext.Current;
        string sDireccionIP = String.Empty;

        if (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            sDireccionIP = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        else if (!String.IsNullOrWhiteSpace(context.Request.UserHostAddress))
            sDireccionIP = context.Request.UserHostAddress;

        if (sDireccionIP == "::1")
            sDireccionIP = "127.0.0.1";

        return sDireccionIP;
        //Nuevo codigo

        
    }
    #endregion
    //****

    //Metodo para conseguir la ip Local
    #region Metodo para conseguir la ip Local
    public string rsRecuperarIPLocal()
    {
        string sIpLocal = "";
        string Localip4 = "";
        string Localip6 = "";
        try
        {
            // se declara la variable            
            // se asigna nombre de host a la variable sHost
            string sHost = Dns.GetHostName();
            //se obtiene el arreglo de ip acorde al host
            IPAddress[] sIp = Dns.GetHostAddresses(sHost);
            
            //Se recorre el arrelo en busca del indice correspondiente a ipV4
            foreach (IPAddress ip4 in sIp.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork))
            {
                Localip4 = ip4.ToString();
            }
            //Se recorre el arrelo en busca del indice correspondiente a ipV6            
            foreach (IPAddress ip6 in sIp.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6))
            {
                
                Localip6 = ip6.ToString();
            }

            if (Localip4 != "")
            {
                sIpLocal = Localip4;
            }
            else {
                sIpLocal = Localip6;
            }
            
            //retornamos la cadena
            return sIpLocal;
        }
        catch (Exception ex)
        {
            return sIpLocal = "0.0.0.0" + ex.Message;
        }


    }
    #endregion
    //****

    //Metodo para conseguir el nombre del Host
    #region Metodo para obtener el nombre de host
    public string rsRecuperarNombreHost()
    {
        string sNombreHost;
        try
        {
            // se asigna nombre de host a la variable sHost
            sNombreHost = Dns.GetHostName();
            //retornamos la cadena
            return sNombreHost;
        }
        catch (Exception)
        {
            return sNombreHost = "Sin Nombre";
        }

    }
    #endregion
    //****


    public BitacoraAcceso()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}