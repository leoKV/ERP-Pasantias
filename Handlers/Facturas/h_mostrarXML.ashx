<%@ WebHandler Language="C#" Class="h_mostrarXML" %>

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
        Factura objFactura = new Factura();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sNoFactura", "sNombreXML", "sRutaXML" };
        //Se crea la consulta
        string sQuery = "";
        string sDirectorio = "";
        sQuery = @"select '''ftp://'+tcftp.direccionFtp+'//'+tdftp.directorio+'//''' from tDirectorioFtp tdftp 
                   inner join tCredencialesFtp tcftp on tdftp.idCredencialesFtp=tcftp.idCredencialesFtp
                   where tdftp.nombreFTP='transferenciaArchivo'";

        sDirectorio = objConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        //Usar para pruebas en local
        sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomXML AS sNombreXML, " + sDirectorio + " + taf.rutaXML AS sRutaXML  FROM tFactura tf inner join tArchivosFactura taf ON tf.idFactura=taf.idFactura WHERE tf.idFactura=" + context.Request["iIdFacturaXML"].ToString();
        //Usar para aplicacion en servidor
        //string sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomXML AS sNombreXML, 'ftp://127.0.0.1//nsi//' + taf.rutaXML AS sRutaXML  FROM tFactura tf inner join tArchivosFactura taf ON tf.idFactura=taf.idFactura WHERE tf.idFactura=" + context.Request["iIdFacturaXML"].ToString();
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<Factura>(sQuery, arrAtributos, objFactura);

        //string sRutaArchivo = context.Server.MapPath("../../Documentos/Factura/TemporalXML.xml");
        string sRutaArchivo = HttpContext.Current.Server.MapPath("~//Documentos//Factura//" + objFactura.sNombreXML);
        string sRuta = "../../Documentos/Factura/" + objFactura.sNombreXML;

        if (!System.IO.File.Exists(sRutaArchivo))
        {

            using (StreamWriter obj_docTXT = new StreamWriter(sRutaArchivo))
            {
                ///SE AGREGA LINEA
                obj_docTXT.WriteLine("Hello World!");
            }
        }

        FtpWebRequest request = (FtpWebRequest)System.Net.WebRequest.Create(objFactura.sRutaXML); ;
        request.Credentials = new NetworkCredential("ClientAdmin", "q6QQhDVheXLk8TheW");
        request.Method = WebRequestMethods.Ftp.DownloadFile;
        request.UsePassive = true;

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

}