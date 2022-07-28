<%@ WebHandler Language="C#" Class="h_descargarFacturaTimbradorZIP" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class h_descargarFacturaTimbradorZIP : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        Factura objFactura = new Factura();
        //Se crea la lista
        List<Factura> lstFactura = new List<Factura>();
        string sPath = context.Server.MapPath("../../Documentos/FacturaTimbrador/FacturaZIP/FacturaZIP/");
        string sRutaZIP = "../../Documentos/FacturaTimbrador/FacturaZIP/FacturasZIP.zip";
        string sWhere = context.Request["sWhere"];
        string sQuery = "";
        string sIdFacturaTimbrador = "";

        //Contingencia
        bool contingencia = true;

        if (!Directory.Exists(sPath))
        {
            Directory.CreateDirectory(sPath);
        }

        string sDirectorio = "";
	 if (contingencia)
        {
            sQuery = @"select '''ftp://'+tcftp.direccionFtp+'//''' from tDirectorioFtp tdftp
                    
		inner join tCredencialesFtp tcftp on tdftp.idCredencialesFtp=tcftp.idCredencialesFtp
                    
		where tdftp.nombreFTP='transferenciaArchivo'";

        }
        else
        {
        	sQuery = @"select '''ftp://'+tcftp.direccionFtp+'//'+tdftp.directorio+'//''' from tDirectorioFtp tdftp 
                   inner join tCredencialesFtp tcftp on tdftp.idCredencialesFtp=tcftp.idCredencialesFtp
                   where tdftp.nombreFTP='transferenciaArchivo'";
	}

        sDirectorio = objConexion.ejecutarConsultaRegistroSimple(sQuery)[1];

        objFactura.sRuta = sRutaZIP;

        //Comprueba que el id de factura no venga encriptado
        if (context.Request["sIdFacturaTimbrador"] != null)
        {
            Security secIdFactura = new Security(context.Request["sIdFacturaTimbrador"]);
            sIdFacturaTimbrador = secIdFactura.DesEncriptar();
        }

        //Atributos
        string[] arrAtributos = { "sNoFactura", "sNombrePDF", "sRutaPDF", "sNombreXML", "sRutaXML","sSerie" };

        //Se valida que no venga vacia
        if (sIdFacturaTimbrador != "")
        {
            //prueba local 
            sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, " + sDirectorio + " + taf.rutaPDF AS sRutaPDF, taf.nomXML AS sNombreXML, " + sDirectorio + " + taf.rutaXML AS sRutaXML, tf.noSerie as sSerie FROM tFacturaTimbrador tf inner join tArchivosFacturaTimbrador taf ON tf.idFacturaTimbrador=taf.idFacturaTimbrador WHERE tf.idFacturaTimbrador = " + sIdFacturaTimbrador;
            //prueba servidor
            //sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, 'ftp://127.0.0.1//nsi//' + taf.rutaPDF AS sRutaPDF, taf.nomXML AS sNombreXML, 'ftp://127.0.0.1//nsi//' + taf.rutaXML AS sRutaXML FROM tFacturaTimbrador tf inner join tArchivosFacturaTimbrador taf ON tf.idFacturaTimbrador=taf.idFacturaTimbrador WHERE tf.idFacturaTimbrador="+ sIdFacturaTimbrador;
        }

        objConexion.ejecutaRecuperaObjetoLista<Factura>(sQuery, arrAtributos, lstFactura);
        //Ciclo que crea los archivos .pdf y .xml de facturas
        for (int i = 0; i < lstFactura.Count; i++)
        {
            // C R E A C I Ó N  A R C H I V O  P D F  I N I C I O
            string sRutaArchivo = context.Server.MapPath("../../Documentos/FacturaTimbrador/FacturaZIP/FacturaZIP/" + lstFactura[i].sSerie+lstFactura[i].sNoFactura + ".pdf");
            string sRuta = "../../Documentos/FacturaZIP/FacturaZIP/" + lstFactura[i].sSerie+lstFactura[i].sNoFactura + ".pdf";

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
            FtpWebRequest request = (FtpWebRequest)System.Net.WebRequest.Create(lstFactura[i].sRutaPDF);
            request.Credentials = new NetworkCredential("ClientAdmin", "q6QQhDVheXLk8TheW");
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UsePassive = true;

            using (MemoryStream stream = new MemoryStream())
            {
                ((FtpWebResponse)request.GetResponse()).GetResponseStream().CopyTo(stream);
                System.IO.File.WriteAllBytes(sRutaArchivo, stream.ToArray());
            }

            // C R E A C I Ó N  A R C H I V O  P D F  F I N

            if (lstFactura[i].sRutaXML != null)
            {
                // C R E A C I Ó N  A R C H I V O  X M L  I N I C I O

                sRutaArchivo = context.Server.MapPath("../../Documentos/FacturaTimbrador/FacturaZIP/FacturaZIP/" + lstFactura[i].sSerie+lstFactura[i].sNoFactura + ".xml");
                sRuta = "../../Documentos/FacturaTimbrador/FacturaZIP/FacturaZIP/" + lstFactura[i].sSerie+lstFactura[i].sNoFactura + ".xml";

                if (!System.IO.File.Exists(sRutaArchivo))
                {

                    using (StreamWriter obj_docTXT = new StreamWriter(sRutaArchivo))
                    {
                        ///SE AGREGA LINEA
                        obj_docTXT.WriteLine("Hello World!");
                    }
                }
                request = (FtpWebRequest)System.Net.WebRequest.Create(lstFactura[i].sRutaXML);
                request.Credentials = new NetworkCredential("ClientAdmin", "q6QQhDVheXLk8TheW");
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.UsePassive = true;

                using (MemoryStream stream = new MemoryStream())
                {
                    ((FtpWebResponse)request.GetResponse()).GetResponseStream().CopyTo(stream);
                    System.IO.File.WriteAllBytes(sRutaArchivo, stream.ToArray());
                }

            }
            // C R E A C I Ó N  A R C H I V O  X M L  F I N
        }

        objFactura.sRuta = sRutaZIP;
        if (!File.Exists(sRutaZIP))
        {
            objFactura.sMensaje = "Se ha generado correctamente el ZIP";
            objFactura.iResultado = 1;
        }
        else
        {
            objFactura.sMensaje = "Ocurrió un error al generar el ZIP";
            objFactura.iResultado = 2;
        }
        //Do the operation as required
        //Devuelve la ruta como respuesta a la llamada ajax
        System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string returnRes = javaScriptSerializer.Serialize(objFactura);
        context.Response.ContentType = "text/html";
        context.Response.Write(returnRes);

        //Creación del ZIP
        ArchivoZip objArchivoZIP = new ArchivoZip();
        objArchivoZIP.fn_ComprimirCarpetaZIP(sPath, "FacturasZIP", "FacturaTimbrador/FacturaZIP");

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
