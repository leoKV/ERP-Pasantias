<%@ WebHandler Language="C#" Class="h_carga_archivo" %>

using System;
using System.Web;
using System.Text.RegularExpressions;

public class h_carga_archivo : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        CargaArchivo obj_cargaArchivo = new CargaArchivo();
        HttpPostedFile archivo = context.Request.Files["file"];
        string extencion = context.Request["extencion"].ToString();
        string sNombreArchivo = context.Request["nombre"].ToString();
        string carpeta = context.Request["carpeta"].ToString();

        string sRutaInicial = HttpContext.Current.Server.MapPath("~\\Documentos\\");
        string sRuta = sRutaInicial + carpeta;

        if (!System.IO.Directory.Exists(sRuta))
        {
            System.IO.Directory.CreateDirectory(sRuta);
        }

        sRuta = sRuta + sNombreArchivo + System.IO.Path.GetExtension(archivo.FileName.ToString());
        sNombreArchivo = sNombreArchivo + System.IO.Path.GetExtension(archivo.FileName.ToString());
        archivo.SaveAs(sRuta);

        ServicioFtp objServicioFtp = new ServicioFtp("transferenciaArchivo");
        objServicioFtp.fn_ObtenerServidorFtp(objServicioFtp);

        ArchivoFactura objArchivoFactura = new ArchivoFactura();
        //objArchivoFactura.sCarpetaCont = "SolicitudTransferencia";
        carpeta = carpeta.Substring(0, carpeta.Length - 2);
        objArchivoFactura.sCarpeta = carpeta;
        objArchivoFactura.sNomArchivoPdf = sNombreArchivo;
        objArchivoFactura.sCarpetaCont = carpeta;
        objArchivoFactura.sRutaProyecto = sRutaInicial + carpeta;

        objServicioFtp.fn_MoverArchivoAFtpPDFSolicitud(objServicioFtp, objArchivoFactura, objArchivoFactura.sRutaProyecto);

        if(!objServicioFtp.bError){
            obj_cargaArchivo.iResultado = 1;
            obj_cargaArchivo.sMensaje = "Archivo cargado correctamente.";
        }
        else
        {
            obj_cargaArchivo.iResultado = 0;
            obj_cargaArchivo.sMensaje = "No se pudo cargar el archivo";
        }

        
        //Do the operation as required
        System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string returnRes = javaScriptSerializer.Serialize(obj_cargaArchivo);
        context.Response.ContentType = "text/html";
        context.Response.Write(returnRes);
    }


    public bool IsReusable {
        get {
            return false;
        }
    }
}