<%@ WebHandler Language="C#" Class="h_descargarComplementoPagoZIP" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;


public class h_descargarComplementoPagoZIP : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        ComplementoPago objComplementoPago = new ComplementoPago();
        //Se crea la lista
        List<ComplementoPago> lstComplementoPago = new List<ComplementoPago>();
        string sPath = context.Server.MapPath("../../Documentos/ComplementoPago/ComplementoPagoZIP");
        string sRutaZIP = "../../Documentos/ComplementoPago/ComplementoPago.zip";
        string sWhere = context.Request["sWhere"];
        string sQuery = "";

        string sDirectorio = "";
        sQuery = @"select '''ftp://'+tcftp.direccionFtp+'//''' from tDirectorioFtp tdftp
                    
		inner join tCredencialesFtp tcftp on tdftp.idCredencialesFtp=tcftp.idCredencialesFtp
                    
		where tdftp.nombreFTP='transferenciaArchivo'";
        //sQuery = @"select '''ftp://'+tcftp.direccionFtp+'//'+tdftp.directorio+'//''' from tDirectorioFtp tdftp 
        //           inner join tCredencialesFtp tcftp on tdftp.idCredencialesFtp=tcftp.idCredencialesFtp
        //           where tdftp.nombreFTP='transferenciaArchivo'";

        sDirectorio = objConexion.ejecutarConsultaRegistroSimple(sQuery)[1];

        string[] arrAtributos = { "sNoComplementoPago", "sNombrePDF", "sRutaPDF", "sNombreXML", "sRutaXML" };

        //prueba local 
        sQuery = "SELECT (case when tcop.noComplementoPago='' then 'CP' else tcop.noComplementoPago END )+'_'+tcop.UUID AS sNoComplementoPago, tacp.nomPDF AS sNombrePDF, " + sDirectorio + " + tacp.rutaPDF AS sRutaPDF, tacp.nomXML AS sNombreXML, " + sDirectorio + " + tacp.rutaXML AS sRutaXML FROM tComplementoPago tcop inner join tArchivosComplementoPago tacp ON tcop.idComplementoPago=tacp.idComplementoPago WHERE tcop.idComplementoPago in (select v.idComplementoPago from v_ListaComplementosPago v " + context.Request["sWhere"] + ")";
        //prueba servidor
        //sQuery = "SELECT tcop.noComplementoPago AS sNoComplementoPago, tacp.nomPDF AS sNombrePDF, 'ftp://127.0.0.1//nsi//' + tacp.rutaPDF AS sRutaPDF, tacp.nomXML AS sNombreXML, 'ftp://127.0.0.1//nsi//' + tacp.rutaXML AS sRutaXML FROM tComplementoPago tcop inner join tArchivosComplementoPago tacp ON tcop.idComplementoPago=tacp.idComplementoPago WHERE tcop.idComplementoPago in (select v.idComplementoPago from v_ListaComplementosPago v " + context.Request["sWhere"] + ")";

        objConexion.ejecutaRecuperaObjetoLista<ComplementoPago>(sQuery, arrAtributos, lstComplementoPago);
        //Ciclo que crea los archivos .pdf y .xml
        for (int i = 0; i < lstComplementoPago.Count; i++)
        {
            // C R E A C I Ó N  A R C H I V O  P D F  I N I C I O
            string sRutaArchivo = context.Server.MapPath("../../Documentos/ComplementoPago/ComplementoPagoZIP/" + lstComplementoPago[i].sNoComplementoPago + ".pdf");
            string sRuta = "../../Documentos/ComplementoPago/ComplementoPagoZIP/" + lstComplementoPago[i].sNoComplementoPago + ".pdf";

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

            FtpWebRequest request = (FtpWebRequest)System.Net.WebRequest.Create(lstComplementoPago[i].sRutaPDF);
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

            sRutaArchivo = context.Server.MapPath("../../Documentos/ComplementoPago/ComplementoPagoZIP/" + lstComplementoPago[i].sNoComplementoPago + ".xml");
            sRuta = "../../Documentos/ComplementoPago/ComplementoPagoZIP/" + lstComplementoPago[i].sNoComplementoPago + ".xml";

            if (!System.IO.File.Exists(sRutaArchivo))
            {

                using (StreamWriter obj_docTXT = new StreamWriter(sRutaArchivo))
                {
                    ///SE AGREGA LINEA
                    obj_docTXT.WriteLine("Hello World!");
                }
            }

            request = (FtpWebRequest)System.Net.WebRequest.Create(lstComplementoPago[i].sRutaXML);
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
        objComplementoPago.sRuta = sRutaZIP;
        if (!File.Exists(sRutaZIP))
        {
            objComplementoPago.sMensaje = "Se ha generado correctamente el ZIP";
            objComplementoPago.iResultado = 1;
        }
        else
        {
            objComplementoPago.sMensaje = "Ocurrió un error al generar el ZIP";
            objComplementoPago.iResultado = 2;
        }
        //Do the operation as required
        //Devuelve la ruta como respuesta a la llamada ajax
        System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string returnRes = javaScriptSerializer.Serialize(objComplementoPago);
        context.Response.ContentType = "text/html";
        context.Response.Write(returnRes);

        //Creación del ZIP
        ArchivoZip objArchivoZIP = new ArchivoZip();
        objArchivoZIP.fn_ComprimirCarpetaZIP(sPath, "ComplementoPago", "ComplementoPago");

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