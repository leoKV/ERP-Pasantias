<%@ WebHandler Language="C#" Class="h_carga_archivo" %>

using System;
using System.Web;
using System.Text.RegularExpressions;

public class h_carga_archivo : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        //Se instancia la clase
        CargaArchivo obj_cargaArchivo = new CargaArchivo();
        HttpPostedFile archivo = context.Request.Files["file"];
        string extencion = context.Request["extencion"].ToString();
        string sNombreArchivo = context.Request["nombre"].ToString();
        string carpeta = Regex.Replace(Regex.Replace(context.Request["carpeta"].ToString(), "[()]", ""), "[ ]", "_");
        //Se declara la variable tipo string "ruta"
        string ruta = "";
        string ext = System.IO.Path.GetExtension(archivo.FileName);
        try
        {
            if (ext.ToLower() == extencion.ToString())
            {
                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("../../Documentos/" + carpeta + "/")))
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("../../Documentos/" + carpeta + "/"));
                //sNombreArchivo = Regex.Replace(Regex.Replace(archivo.FileName.ToString(), "[()]", ""), "[ ]", "_");
                string sExtensionArchivo = Regex.Replace(Regex.Replace(archivo.FileName.ToString(), "[()]", ""), "[ ]", "_");
                /** int iTamArchivo = carpeta.Length;
                 int iAparicion = carpeta.LastIndexOf("/");
                 sNombreArchivo = carpeta.Substring(iAparicion, (iTamArchivo-iAparicion));**/
                if (sNombreArchivo.Length > 0)
                {
                    ruta = HttpContext.Current.Server.MapPath("../../Documentos/" + carpeta + "/" + sNombreArchivo + System.IO.Path.GetExtension(archivo.FileName.ToString()).ToLower().Replace(' ','_'));
                }
                else {
                    ruta = HttpContext.Current.Server.MapPath("../../Documentos/" + carpeta + "/" + System.IO.Path.GetFileNameWithoutExtension(archivo.FileName.ToString().Replace(' ','_')) + System.IO.Path.GetExtension(archivo.FileName.ToString().Replace(' ','_')).ToLower());
                }
                //elimina el archivo si ya existe
                if(System.IO.File.Exists(ruta))
                    System.IO.File.Delete(ruta);

                //se guarda el archivo 
                archivo.SaveAs(ruta);

                //se le dan todos los permisos al archivo cargado 
                System.IO.File.SetAttributes(ruta, System.IO.FileAttributes.Normal);

                obj_cargaArchivo.iResultado = 1;
                obj_cargaArchivo.sMensaje = "Archivo cargado correctamente.";
            }
            else
            {
                obj_cargaArchivo.iResultado = 0;
                obj_cargaArchivo.sMensaje = "La extención del archivo no es correcta.";
            }
        }
        catch (Exception e)
        {
            //Se retorna el error
            obj_cargaArchivo.iResultado = 0;
            obj_cargaArchivo.sMensaje = e.Message;
        }
        finally
        {

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