<%@ WebHandler Language="C#" Class="h_generarPdf" %>

using System;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Collections.Generic;
using iTextSharp.text.html.simpleparser;
using System.Data;


public class h_generarPdf : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        //obtiene nombre archivo 
        string sNombreArchivo = context.Request["sNombreArchivo"].ToString();
        string sCarpeta = context.Request["sCarpeta"].ToString();
        string sMensaje = context.Request["sMensaje"].ToString();
        //decodifica html
        sMensaje = sMensaje.Replace("!com", "\"").Replace("!mqa", "<").Replace("!mqc", ">"); ;
        
        //Se obtiene nombre archivo
        DateTime Hoy = DateTime.Now;
        string nombreArchivo = sNombreArchivo.Trim().Replace(" ", "") +"_" + Hoy.Year + Hoy.Month + Hoy.Day + Hoy.Hour + Hoy.Minute + Hoy.Second + ".pdf";
        //string rutaImagen = HttpContext.Current.Server.MapPath("../../Styles/Imagenes/LogotipoAltaResolucion.png");
        string rutaArchivo = HttpContext.Current.Server.MapPath("../../Documentos/" + sCarpeta + "/" + nombreArchivo);
        string rutaVerArchivo = "../../Documentos/" + sCarpeta + "/" + nombreArchivo;
        //--- Comenzar a contruir documento en PDF. 
        Document document = new Document(PageSize.A4, 50, 50, 50, 50);
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write, FileShare.None));
        
        //Se crea objeto
        Reporte objReporte = new Reporte();
        
        
        ///INICIO TRY D:/
        try
        {   
            //Se crea la estructura del PDF 
            objReporte.sMensaje = sMensaje;
            
            //obj_recibo.sMensaje += obj_recibo.sContenidoIncrementables;
            List<IElement> lstTable1;
            ///Base de color para fuente
            var FontColour = new BaseColor(29, 102, 136);
            ///Nuevo parrafo de footer
            Paragraph footer = new Paragraph("" + sNombreArchivo + "\n\n©NG Customs " + Hoy.Year + ". ©Todos los derechos reservados.", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, iTextSharp.text.Font.NORMAL, FontColour));
            ///Se alinea a la derecha
            footer.Alignment = Element.ALIGN_RIGHT;
            ///variable tipo tabla para almacenar el footer
            PdfPTable footerTbl = new PdfPTable(1);
            ///Se asigna tamaño y alineación
            footerTbl.TotalWidth = 700F;
            footerTbl.HorizontalAlignment = Element.ALIGN_RIGHT;
            ///Se agrega el footer a una celda
            PdfPCell cell = new PdfPCell(footer);
            cell.Border = 0;
            cell.PaddingLeft = 10;
            cell.PaddingBottom = 10;
            footerTbl.AddCell(cell);
            ///Se abre el documento
            document.Open();

            ///Se escribe el footer en el documento
            footerTbl.WriteSelectedRows(0, -1, 50, 30, writer.DirectContent);
            lstTable1 = HTMLWorker.ParseToList(new StringReader(objReporte.sMensaje), null);
            document.Add((IElement)lstTable1[0]);
            document.Add(new Paragraph("\n"));
            ///Se escribe el footer en el documento
            if (objReporte.sMensaje.Length > 1500)
                footerTbl.WriteSelectedRows(0, -1, 50, 30, writer.DirectContent);
            //document.Add(new Paragraph("\n"));

            document.Close();
            document.Dispose();
  
            objReporte.iResultado = 1;
            objReporte.sRutaPDF = rutaArchivo;
            objReporte.sRutaVerPDF = rutaVerArchivo;
            objReporte.sNombreArchivo = nombreArchivo;
        }///FIN TRY
        ///INICIO CATCH
        catch (Exception ex)
        {
            objReporte.iResultado = 0;
            objReporte.sMensaje = "Error: " + ex.Message;
            document.Close();
            document.Dispose();
        }///FIN CATCH
        //Do the operation as required
        System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string returnRes = javaScriptSerializer.Serialize(objReporte);
        context.Response.ContentType = "text/html";
        context.Response.Write(returnRes);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}