<%@ WebHandler Language="C#" Class="h_mostrarPdf" %>
//REALIZADO POR SAÚL
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
        ComplementoPago objComplementoPago = new ComplementoPago();
        string sQuery = "";
        string sDirectorio = "";
        sQuery = @"select '''ftp://'+tcftp.direccionFtp+'//'+tdftp.directorio+'//''' from tDirectorioFtp tdftp 
                   inner join tCredencialesFtp tcftp on tdftp.idCredencialesFtp=tcftp.idCredencialesFtp
                   where tdftp.nombreFTP='transferenciaArchivo'";

        sDirectorio = objConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        //Se crea arreglo con atributos
        string[] arrAtributos = { "sNoComplementoPago", "sNombrePDF", "sRutaPDF" };
        //Se crea la consulta
        //Usar para pruebas en local
        sQuery = "select tc.noComplementoPago AS sNoComplementoPago,tacp.nomPDF AS sNombrePDF," + sDirectorio + "+tacp.rutaPDF AS sRutaPDF from tComplementoPago tc inner join tArchivosComplementoPago tacp on tc.idComplementoPago=tacp.idComplementoPago WHERE tacp.idComplementoPago=" + context.Request["iIdComplementoPago"].ToString();
        //Usar para aplicacion en servidor
        //string sQuery = "select tc.noComplementoPago AS sNoComplementoPago,tacp.nomPDF AS sNombrePDF,'ftp://127.0.0.1//nsi//'+tacp.rutaPDF AS sRutaPDF from tComplementoPago tc inner join tArchivosComplementoPago tacp on tc.idComplementoPago=tacp.idComplementoPago WHERE tacp.idComplementoPago=" + context.Request["iIdComplementoPago"].ToString();
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<ComplementoPago>(sQuery, arrAtributos, objComplementoPago);

        string sRutaArchivo = context.Server.MapPath("../../Documentos/ComplementoPago/" + objComplementoPago.sNoComplementoPago + ".pdf");
        string sRuta = "../../Documentos/ComplementoPago/" + objComplementoPago.sNoComplementoPago + ".pdf";

        if (!System.IO.File.Exists(sRutaArchivo))
        {
            //Se crea el archivo PDF vacío
            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                Document myDocument = new Document();
                PdfWriter myPDFWriter = PdfWriter.GetInstance(myDocument, myMemoryStream);
                myDocument.Open();
                myDocument.Add(new Paragraph("Hello World!"));
                myDocument.Close();
                byte[] content = myMemoryStream.ToArray();
                // Write out PDF from memory stream. 
                using (FileStream fs = File.Create(sRutaArchivo))
                {
                    fs.Write(content, 0, (int)content.Length);
                }
            }
        }

        FtpWebRequest request = (FtpWebRequest)System.Net.WebRequest.Create(objComplementoPago.sRutaPDF); ;
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