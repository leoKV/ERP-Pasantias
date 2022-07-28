<%@ WebHandler Language="C#" Class="h_mostrarPdf" %>

using System;
using System.Web;
using System.IO;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class h_mostrarPdf : IHttpHandler
{
    /// <summary>
    /// Handler para mostrar archivos que esten guardados en servidor, pasandolo como parametros
    /// form_data.append("iId", -> id del documento)
    /// form_data.append("sIdentificador", -> nombre de la columna del identificador)
    /// form_data.append("sTabla", -> tabla donde se encuentra la direccion del archivo)
    /// form_data.append("sCampo", -> campo con la ruta del archivo)
    /// form_data.append("sArchivo", -> nombre para el archivo)
    /// form_data.append("sTipoArchivo", '-> extencion del archivo [pdf,exl,etc.])
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        //Contingencia
        bool contingencia = true;
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        //Se instancia objeto factura
        // Factura objFactura = new Factura();
        //Se crea arreglo con atributos
        string[] arrRespuesta = new string[2];
        string sRutaTipo;
        string sQuery = "";
        string sDirectorio = "";
        string sRuta = "";

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
        int iIdObjeto = 0;
        //Se obtiene el id
        if (esEntero(context.Request["iId"].ToString()))
        {
            iIdObjeto = int.Parse(context.Request["iId"].ToString());
        }
        else
        {
            //objeto para desencriptar el idSubReferencia
            Security secId = new Security(context.Request["iId"].ToString());
            iIdObjeto = int.Parse(secId.DesEncriptar());
        }

        //Se crea la consulta
        sQuery = "SELECT " + sDirectorio + " + ta." + context.Request["sCampo"].ToString() + " AS sRuta FROM " + context.Request["sTabla"].ToString() + " ta WHERE ta." + context.Request["sIdentificador"].ToString() + "=" + iIdObjeto;
        arrRespuesta = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        sRutaTipo = arrRespuesta[1];

        string sRutaArchivo = HttpContext.Current.Server.MapPath("~//Documentos//" + context.Request["sArchivo"].ToString() + "." + context.Request["sTipoArchivo"].ToString());
        sRuta = "../../Documentos/" + context.Request["sArchivo"].ToString() + "." + context.Request["sTipoArchivo"].ToString();

        if (!System.IO.File.Exists(sRutaArchivo))
        {
            //Se crea el archivo Tipo vacío
            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                Document myDocument = new Document();
                PdfWriter myTipoWriter = PdfWriter.GetInstance(myDocument, myMemoryStream);
                myDocument.Open();
                myDocument.Add(new Paragraph("Hello World!"));
                myDocument.Close();
                byte[] content = myMemoryStream.ToArray();
                // Write out Tipo from memory stream.
                using (FileStream fs = File.Create(sRutaArchivo))
                {
                    fs.Write(content, 0, (int)content.Length);
                }
            }
        }

        FtpWebRequest request = (FtpWebRequest)System.Net.WebRequest.Create(sRutaTipo);
        request.Credentials = new NetworkCredential("ClientAdmin", "q6QQhDVheXLk8TheW");
        request.Method = WebRequestMethods.Ftp.DownloadFile;
        request.UsePassive = true;
        
        try
        {
            //Your code
            using (MemoryStream stream = new MemoryStream())
            {
                ((FtpWebResponse)request.GetResponse()).GetResponseStream().CopyTo(stream);
                System.IO.File.WriteAllBytes(sRutaArchivo, stream.ToArray());
            }
        }
        catch (WebException e)
        {
            string status = ((FtpWebResponse)e.Response).StatusDescription;
		//Se crea el archivo Tipo vacío
            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                Document myDocument = new Document();
                PdfWriter myTipoWriter = PdfWriter.GetInstance(myDocument, myMemoryStream);
                myDocument.Open();
                myDocument.Add(new Paragraph(status +" "+ sRutaTipo));
                myDocument.Close();
                byte[] content = myMemoryStream.ToArray();
                // Write out Tipo from memory stream.
                using (FileStream fs = File.Create(sRutaArchivo))
                {
                    fs.Write(content, 0, (int)content.Length);
                }
            }
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
    public static bool esEntero(string sString)
    {
        try
        {
            int.Parse(sString);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

}