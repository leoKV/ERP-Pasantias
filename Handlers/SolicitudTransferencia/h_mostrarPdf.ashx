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
        //Se instancia objeto Solicitud
        SolicitudTransferencia objSolicitud = new SolicitudTransferencia();

        //Se crea arreglo con atributos
        string[] arrAtributos = { "iIdSolicitud", "sNombreArchivo", "sRutaPDF" };
        string sQuery = "";
        string sDirectorio = "";
        sQuery = @"select '''ftp://'+tcftp.direccionFtp+'//'+tdftp.directorio+'//''' from tDirectorioFtp tdftp 
                   inner join tCredencialesFtp tcftp on tdftp.idCredencialesFtp=tcftp.idCredencialesFtp
                   where tdftp.nombreFTP='transferenciaArchivo'";

        sDirectorio = objConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
        //Se crea la consulta

        if (context.Request["iIdTipoObjSolicitud"].ToString() == "1")
        {
            //Usar para pruebas en local
            sQuery = "SELECT tstp.idSolicitudTransferencia AS iIdSolicitud, tstp.nombrePdf AS sNombreArchivo, " + sDirectorio + " + tstp.rutaPDF AS sRutaPDF  FROM tSolicitudTransferenciaPDF tstp where tstp.idSolicitudTransferenciaPDF=" + context.Request["iIdPdfSolicitud"].ToString();
            //Usar para aplicacion en servidor
            //sQuery = "SELECT tstp.idSolicitudTransferencia AS iIdSolicitud, tstp.nombrePdf AS sNombreArchivo, 'ftp://127.0.0.1//nsi//' + tstp.rutaPDF AS sRutaPDF  FROM tSolicitudTransferenciaPDF tstp where tstp.idSolicitudTransferenciaPDF=" + context.Request["iIdPdfSolicitud"].ToString();

        }
        else
        {
            Security secIdSolicitud = new Security(context.Request["iIdSolicitud"].ToString());
            string iIdSolicitud = secIdSolicitud.DesEncriptar();
            //Usar para pruebas en local
            sQuery = "SELECT tst.idSolicitudTransferencia AS iIdSolicitud, tst.nomSolicitudPDF AS sNombreArchivo, " + sDirectorio + " + tst.rutaSolicitudPDF AS sRutaPDF  FROM tSolicitudTransferencia tst where tst.idSolicitudTransferencia=" + iIdSolicitud;
            //Usar para aplicacion en servidor
            //sQuery = "SELECT tst.idSolicitudTransferencia AS iIdSolicitud, tst.nomSolicitudPDF AS sNombreArchivo, 'ftp://127.0.0.1//nsi//' + tst.rutaSolicitudPDF AS sRutaPDF  FROM tSolicitudTransferencia tst where tst.idSolicitudTransferencia=" + iIdSolicitud;

        }
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<SolicitudTransferencia>(sQuery, arrAtributos, objSolicitud);

        string sRutaArchivo = context.Server.MapPath("../../Documentos/Factura/TemporalPDF.pdf");
        string sRuta = "../../Documentos/Factura/TemporalPDF.pdf";

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

        FtpWebRequest request = (FtpWebRequest)System.Net.WebRequest.Create(objSolicitud.sRutaPDF); ;
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