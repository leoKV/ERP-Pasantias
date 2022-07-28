<%@ WebHandler Language="C#" Class="h_mostrarPdf" %>

using System;
using System.Web;
using System.IO;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class h_mostrarPdf : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se instancia objeto factura
        Factura objFactura = new Factura();
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sNoFactura", "sNombrePDF", "sRutaPDF" };
        //Se crea la consulta
        string sQuery = "";
        string sDirectorio = "";
        sQuery = @"select '''ftp://'+tcftp.direccionFtp+'//'+tdftp.directorio+'//''' from tDirectorioFtp tdftp 
                   inner join tCredencialesFtp tcftp on tdftp.idCredencialesFtp=tcftp.idCredencialesFtp
                   where tdftp.nombreFTP='transferenciaArchivo'";

        sDirectorio = objConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        //Usar para pruebas en local
        sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, " + sDirectorio + " + taf.rutaPDF AS sRutaPDF  FROM tFactura tf inner join tArchivosFactura taf ON tf.idFactura=taf.idFactura WHERE tf.idFactura=" + context.Request["iIdFactura"].ToString();
        //Usar para aplicacion en servidor
        //string sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, 'ftp://127.0.0.1//nsi//' + taf.rutaPDF AS sRutaPDF  FROM tFactura tf inner join tArchivosFactura taf ON tf.idFactura=taf.idFactura WHERE tf.idFactura=" + context.Request["iIdFactura"].ToString();
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<Factura>(sQuery, arrAtributos, objFactura);

        //string sRutaArchivo = context.Server.MapPath("../../Documentos/Factura/TemporalPDF.pdf");
        string sRutaArchivo = HttpContext.Current.Server.MapPath("~//Documentos//Factura//" + objFactura.sNombrePDF);
        string sRuta = "../../Documentos/Factura/" + objFactura.sNombrePDF;

        if (!System.IO.File.Exists(sRutaArchivo))
        {
            //Se crea el archivo PDF vacío
            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                Document myDocument = new Document();
                PdfWriter myPDFWriter = PdfWriter.GetInstance(myDocument, myMemoryStream);
                myDocument.Open();
                myDocument.Add(new Paragraph("0"));
                myDocument.Close();
                byte[] content = myMemoryStream.ToArray();
                // Write out PDF from memory stream. 
                using (FileStream fs = File.Create(sRutaArchivo))
                {
                    fs.Write(content, 0, (int)content.Length);
                }
            }
        }

        FtpWebRequest request = (FtpWebRequest)System.Net.WebRequest.Create(objFactura.sRutaPDF); ;
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