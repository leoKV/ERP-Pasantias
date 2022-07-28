<%@ WebHandler Language="C#" Class="h_mostrarXML" %>
//REALIZADO POR SAÚL
using System;
using System.Web;
using System.IO;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class h_mostrarXML : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se instancia objeto factura
        ComplementoPago objComplementoPago = new ComplementoPago();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sNoComplementoPago", "sNombreXML", "sRutaXML" };
        string sQuery = "";
        string sDirectorio = "";
        sQuery = @"select '''ftp://'+tcftp.direccionFtp+'//'+tdftp.directorio+'//''' from tDirectorioFtp tdftp 
                   inner join tCredencialesFtp tcftp on tdftp.idCredencialesFtp=tcftp.idCredencialesFtp
                   where tdftp.nombreFTP='transferenciaArchivo'";

        sDirectorio = objConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        //Se crea la consulta
        //Usar para pruebas en local
        sQuery = "select tc.noComplementoPago AS sNoComplementoPago,tacp.nomXML AS sNombreXML," + sDirectorio + "+tacp.rutaXML AS sRutaXML from tComplementoPago tc inner join tArchivosComplementoPago tacp on tc.idComplementoPago=tacp.idComplementoPago WHERE tacp.idComplementoPago=" + context.Request["iIdComplementoPago"].ToString();
        //Usar para aplicacion en servidor
        //string sQuery = "select tc.noComplementoPago AS sNoComplementoPago,tacp.nomXML AS sNombreXML,'ftp://127.0.0.1//nsi//'+tacp.rutaXML AS sRutaXML from tComplementoPago tc inner join tArchivosComplementoPago tacp on tc.idComplementoPago=tacp.idComplementoPago WHERE tacp.idComplementoPago=" + context.Request["iIdComplementoPago"].ToString();
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<ComplementoPago>(sQuery, arrAtributos, objComplementoPago);

        string sRutaArchivo = context.Server.MapPath("../../Documentos/ComplementoPago/" + objComplementoPago.sNoComplementoPago + ".xml");
        string sRuta = "../../Documentos/ComplementoPago/" + objComplementoPago.sNoComplementoPago + ".xml";

        if (!System.IO.File.Exists(sRutaArchivo))
        {

            using (StreamWriter obj_docTXT = new StreamWriter(sRutaArchivo))
            {
                ///SE AGREGA LINEA
                obj_docTXT.WriteLine("Hello World!");
            }
        }

        FtpWebRequest request = (FtpWebRequest)System.Net.WebRequest.Create(objComplementoPago.sRutaXML); ;
        request.Credentials = new NetworkCredential("ClientAdmin", "q6QQhDVheXLk8TheW");
        request.Method = WebRequestMethods.Ftp.DownloadFile;
        request.UsePassive = false;

        using (MemoryStream stream = new MemoryStream())
        {
            ((FtpWebResponse)request.GetResponse()).GetResponseStream().CopyTo(stream);
            System.IO.File.WriteAllBytes(sRutaArchivo, stream.ToArray());
        }


        //Do the operation as required
        System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string returnRes = javaScriptSerializer.Serialize(sRuta);
        context.Response.ContentType = "text/html";
        context.Response.Write(returnRes);

    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    //FIN REALIZADO POR SAÚL
}