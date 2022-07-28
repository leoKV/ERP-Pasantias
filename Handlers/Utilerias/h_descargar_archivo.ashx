<%@ WebHandler Language="C#" Class="h_descargar_archivo" %>

using System;
using System.Web;

public class h_descargar_archivo : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        //Resultado
        string sRes = "";
        try
        {
            context.Response.ContentType = "text/plain";
            //Se declaran variables que se van a utilizar
            string sRuta = context.Request["sRuta"];
            //Nombre del archivo
            string sNombre = context.Request["sNombreArchivo"];
            //Se obtiene las variables
            System.IO.FileInfo toDownload = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(sRuta));
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + sNombre);
            HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.WriteFile(sRuta);
            HttpContext.Current.Response.End();
            //Se retorna correcto
            sRes = "1";
        }
        catch (Exception e) {
            sRes = e.Message;
        }
        //Do the operation as required
        System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string returnRes = javaScriptSerializer.Serialize(sRes);
        context.Response.ContentType = "text/html";
        context.Response.Write(returnRes);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}