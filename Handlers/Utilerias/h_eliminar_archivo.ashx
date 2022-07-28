<%@ WebHandler Language="C#" Class="h_eliminar_archivo" %>

using System;
using System.Web;
using System.IO;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class h_eliminar_archivo : IHttpHandler {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest (HttpContext context) {
        string res = "";
        string objeto = context.Request["sRuta"].ToString();
        
        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(context.Request["sRuta"].ToString())))
        {
            System.IO.File.Delete(HttpContext.Current.Server.MapPath(context.Request["sRuta"].ToString()));
            res = "1";

        }
        else
        {
            res = "0";
        }
        //Do the operation as required
        System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string returnRes = javaScriptSerializer.Serialize(res);
        context.Response.ContentType = "text/html";
        context.Response.Write(returnRes);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}