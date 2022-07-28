<%@ WebHandler Language="C#" Class="h_descargarNotaCreditoZIP" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class h_descargarNotaCreditoZIP : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        NotaCredito objNotaCredito = new NotaCredito();
        //Se crea la lista
        List<NotaCredito> lstNotaCredito = new List<NotaCredito>();
        string sPath = context.Server.MapPath("../../Documentos/NotaCredito/NotaCreditoZIP");
        string sRutaZIP = "../../Documentos/NotaCredito/NotaCredito.zip";
        string sWhere = context.Request["sWhere"];
        string isNotaCredito = context.Request["isNotaCredito"];
        string sQuery = "";

        string sDirectorio = "";
        sQuery = @"select '''ftp://'+tcftp.direccionFtp+'//'+tdftp.directorio+'//''' from tDirectorioFtp tdftp 
                   inner join tCredencialesFtp tcftp on tdftp.idCredencialesFtp=tcftp.idCredencialesFtp
                   where tdftp.nombreFTP='transferenciaArchivo'";

        sDirectorio = objConexion.ejecutarConsultaRegistroSimple(sQuery)[1];

        string[] arrAtributos = { "sNoNotaCredito", "sNombrePDF", "sRutaPDF", "sNombreXML", "sRutaXML" };

        //prueba local 
        sQuery = "SELECT tnc.folio AS sNoNotaCredito, tanc.nomPDF AS sNombrePDF," +
            "" + sDirectorio + " + tanc.rutaPDF AS sRutaPDF, tanc.nomXML AS sNombreXML," +
            "" + sDirectorio + " + tanc.rutaXML AS sRutaXML" +
            " FROM tNotaCredito tnc" +
            "   inner join tArchivosNotaCredito tanc" +
            " ON tnc.idNotaCredito=tanc.idNotaCredito " +
            " WHERE tnc.idNotaCredito " +
            " in (select v.idNotaCredito from v_ListaNotaCredito v ) " + context.Request["sWhere"] + ")";
        //prueba servidor
        //sQuery = "SELECT tnc.folio AS sNoNotaCredito, tanc.nomPDF AS sNombrePDF," +
        //    "'ftp://127.0.0.1//nsi//' + tanc.rutaPDF AS sRutaPDF, tanc.nomXML AS sNombreXML," +
        //    "'ftp://127.0.0.1//nsi//' + tanc.rutaXML AS sRutaXML" +
        //    " FROM tNotaCredito tnc" +
        //    "   inner join tArchivosNotaCredito tanc" +
        //    " ON tnc.idNotaCredito=tanc.idNotaCredito " +
        //    " WHERE tnc.idNotaCredito " +
        //    " in (select v.idNotaCredito from v_ListaNotaCredito v ) " + context.Request["sWhere"] + ")";

        objConexion.ejecutaRecuperaObjetoLista<NotaCredito>(sQuery, arrAtributos, lstNotaCredito);
        //Ciclo que crea los archivos .pdf y .xml
        for (int i = 0; i < lstNotaCredito.Count; i++)
        {
            // C R E A C I Ó N  A R C H I V O  P D F  I N I C I O
            string sRutaArchivo = context.Server.MapPath("../../Documentos/NotaCredito/NotaCreditoZIP/" + lstNotaCredito[i].sNoNotaCredito + ".pdf");
            string sRuta = "../../Documentos/NotaCredito/NotaCreditoZIP/" + lstNotaCredito[i].sNoNotaCredito + ".pdf";

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

            FtpWebRequest request = (FtpWebRequest)System.Net.WebRequest.Create(lstNotaCredito[i].sRutaPDF);
            request.Credentials = new NetworkCredential("ClientAdmin", "q6QQhDVheXLk8TheW");
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UsePassive = true;

            using (MemoryStream stream = new MemoryStream())
            {
                ((FtpWebResponse)request.GetResponse()).GetResponseStream().CopyTo(stream);
                System.IO.File.WriteAllBytes(sRutaArchivo, stream.ToArray());
            }
            // C R E A C I Ó N  A R C H I V O  P D F  F I N


            // C R E A C I Ó N  A R C H I V O  X M L  I N I C I O

            sRutaArchivo = context.Server.MapPath("../../Documentos/NotaCredito/NotaCreditoZIP/" + lstNotaCredito[i].sNoNotaCredito + ".xml");
            sRuta = "../../Documentos/NotaCredito/NotaCreditoZIP/" + lstNotaCredito[i].sNoNotaCredito + ".xml";

            if (!System.IO.File.Exists(sRutaArchivo))
            {

                using (StreamWriter obj_docTXT = new StreamWriter(sRutaArchivo))
                {
                    ///SE AGREGA LINEA
                    obj_docTXT.WriteLine("Hello World!");
                }
            }

            request = (FtpWebRequest)System.Net.WebRequest.Create(lstNotaCredito[i].sRutaXML);
            request.Credentials = new NetworkCredential("ClientAdmin", "q6QQhDVheXLk8TheW");
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UsePassive = true;

            using (MemoryStream stream = new MemoryStream())
            {
                ((FtpWebResponse)request.GetResponse()).GetResponseStream().CopyTo(stream);
                System.IO.File.WriteAllBytes(sRutaArchivo, stream.ToArray());
            }

            // C R E A C I Ó N  A R C H I V O  X M L  F I N
        }
        objNotaCredito.sRuta = sRutaZIP;
        if (!File.Exists(sRutaZIP))
        {
            objNotaCredito.sMensaje = "Se ha generado correctamente el ZIP";
            objNotaCredito.iResultado = 1;
        }
        else
        {
            objNotaCredito.sMensaje = "Ocurrió un error al generar el ZIP";
            objNotaCredito.iResultado = 2;
        }
        //Do the operation as required
        //Devuelve la ruta como respuesta a la llamada ajax
        System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string returnRes = javaScriptSerializer.Serialize(objNotaCredito);
        context.Response.ContentType = "text/html";
        context.Response.Write(returnRes);

        //Creación del ZIP
        ArchivoZip objArchivoZIP = new ArchivoZip();
        objArchivoZIP.fn_ComprimirCarpetaZIP(sPath, "sFechaInicio", "NotaCredito");

        ////Eliminación de todos los archivos de la carpeta
        string[] ficherosCarpeta = Directory.GetFiles(sPath);
        foreach (string ficheroActual in ficherosCarpeta)
            File.Delete(ficheroActual);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}