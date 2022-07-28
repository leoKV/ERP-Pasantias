<%@ WebHandler Language="C#" Class="h_descargarFacturaZIP" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;



public class h_descargarFacturaZIP : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        Factura objFactura = new Factura();
        NotaCredito objNotaCredito = new NotaCredito();
        //Se crea la lista
        List<Factura> lstFactura = new List<Factura>();
        List<NotaCredito> lstNotaCredito = new List<NotaCredito>();
        string sPath = context.Server.MapPath("../../Documentos/Factura/FacturaZIP/FacturaZIP");
        string sRutaZIP = "../../Documentos/Factura/FacturaZIP/Facturas.zip";
        string sWhere = context.Request["sWhere"];
        string sQuery = "";
        string sIdSubReferencia = "";
        string sIdGasto = "";
        string sIdFactura = "";
        string sVista = context.Request["sVista"];
        string isNotaCredito = context.Request["isNotaCredito"];
        string sQueryNC = "";
        int iIdNotaCredito = 0;
        bool bEncontroTodosLosArchivos = true;
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

        //Comprueba que el id de la nota de credito no venga encriptado
        if (context.Request["sIdNotaCredito"] != null && context.Request["sIdNotaCredito"] != "")
        {
            Security secIdNotaCredito = new Security(context.Request["sIdNotaCredito"]);
            iIdNotaCredito = Int32.Parse(secIdNotaCredito.DesEncriptar());
        }

        //Comprueba que el id de factura no venga encriptado
        if (context.Request["sIdFactura"] != null)
        {
            Security secIdFactura = new Security(context.Request["sIdFactura"]);
            sIdFactura = secIdFactura.DesEncriptar();
        }

        //Si existe subreferencia la desencripta en caso de ser necesario
        if (context.Request["sIdSubReferencia"] != null)
        {
            //Compara que no se encuentre encriptada la subreferencia
            if (context.Request["iEncriptado"] == null)
            {
                //Desencripta la subreferencia
                Security secIdReferencia = new Security(context.Request["sIdSubReferencia"]);
                sIdSubReferencia = secIdReferencia.DesEncriptar();
            }
            else
            {
                sIdSubReferencia = context.Request["sIdSubReferencia"];
            }
        }
        else
        {
            sIdSubReferencia = null;
        }
        //Si existe subreferencia la desencripta en caso de ser necesario
        if (context.Request["sIdGasto"] != null)
        {

            //Desencripta la subreferencia
            Security secIdReferencia = new Security(context.Request["sIdGasto"]);
            sIdGasto = secIdReferencia.DesEncriptar();
        }
        else
        {
            sIdGasto = null;
        }

        //obtiene la query para saber si se recuperara tambien para las notas de credito


        string[] arrAtributos = { "sNoFactura", "sNombrePDF", "sRutaPDF", "sNombreXML", "sRutaXML" };

        string[] arrAtributosNC = { "sNoNotaCredito", "sNombrePDF", "sRutaPDF", "sNombreXML", "sRutaXML" };


        if (isNotaCredito == "1")
        {
            //prueba local
            sQueryNC = "SELECT tnc.folio AS sNoNotaCredito, tanc.nomPDF AS sNombrePDF," +
            "" + sDirectorio + " + tanc.rutaPDF AS sRutaPDF, tanc.nomXML AS sNombreXML," +
            "" + sDirectorio + " + tanc.rutaXML AS sRutaXML" +
            " FROM tNotaCredito tnc" +
            "   inner join tArchivosNotaCredito tanc" +
            " ON tnc.idNotaCredito=tanc.idNotaCredito " +
            " WHERE tnc.idNotaCredito " +
            " in (" + iIdNotaCredito + ") ";
            //prueba servidor
            //sQueryNC = "SELECT tnc.folio AS sNoNotaCredito, tanc.nomPDF AS sNombrePDF," +
            //"'ftp://127.0.0.1//nsi//' + tanc.rutaPDF AS sRutaPDF, tanc.nomXML AS sNombreXML," +
            //"'ftp://127.0.0.1//nsi//' + tanc.rutaXML AS sRutaXML" +
            //"FROM tFactura tf " +
            //"INNER JOIN tNotaCreditoFactura tncf	ON tf.idFactura=tncf.idFactura " +
            //"INNER JOIN tNotaCredito tnc ON tnc.idNotaCredito=tncf.idNotaCredito  " +
            //"inner join tArchivosNotaCredito tanc" +
            //" ON tnc.idNotaCredito=tanc.idNotaCredito " +
            //" WHERE tnc.idNotaCredito " +
            //" in (" + iIdNotaCredito + ") " + context.Request["sWhere"] + "";

            //prueba local 
            sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, " +
                "" + sDirectorio + "' + taf.rutaPDF AS sRutaPDF, taf.nomXML AS sNombreXML, " +
                "" + sDirectorio + " + taf.rutaXML AS sRutaXML FROM tFactura tf " +
                " inner join tArchivosFactura taf ON tf.idFactura=taf.idFactura " +
                " inner join tNotaCreditoFactura tncf  on tncf.idFactura=tf.idFactura " +
                " inner join tNotaCredito tnc on tnc.idNotaCredito=tncf.idNotaCredito " +
                " WHERE tf.idFactura in (select tf.idFactura from tFactura tf " +
                " inner join tNotaCreditoFactura tncf on tncf.idFactura=tf.idFactura " +
                " inner join tNotaCredito tnc on tnc.idNotaCredito=tncf.idNotaCredito " +
                " inner join v_ListaIntegracionFacturas v on v.sNoFactura=tf.noFactura " + context.Request["sWhere"] + ") and tnc.idNotaCredito in (" + iIdNotaCredito + ")";

            //prueba servidor
            //sQuery = "SELECT tf.noFactura AS sNoFactura, " +
            //    "taf.nomPDF AS sNombrePDF, 'ftp://127.0.0.1//nsi//' + taf.rutaPDF AS sRutaPDF," +
            //    " taf.nomXML AS sNombreXML, 'ftp://127.0.0.1//nsi//' + taf.rutaXML AS sRutaXML " +
            //    "FROM tFactura tf " +
            //    "INNER JOIN tNotaCreditoFactura tncf	ON tf.idFactura=tncf.idFactura " +
            //    "INNER JOIN tNotaCredito tnc ON tnc.idNotaCredito=tncf.idNotaCredito  " +
            //    "inner join tArchivosFactura taf ON tf.idFactura=taf.idFactura " +
            //     " WHERE tnc.idNotaCredito " +
            //    " in (" + iIdNotaCredito + ") " + context.Request["sWhere"] + "";
        }
        else if (sIdFactura != "")
        {
            //prueba local 
            sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, " + sDirectorio + " + taf.rutaPDF AS sRutaPDF, taf.nomXML AS sNombreXML, " + sDirectorio + " + taf.rutaXML AS sRutaXML FROM tFactura tf inner join tArchivosFactura taf ON tf.idFactura=taf.idFactura WHERE tf.idFactura = " + sIdFactura;
            //prueba servidor
            //sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, 'ftp://127.0.0.1//nsi//' + taf.rutaPDF AS sRutaPDF, taf.nomXML AS sNombreXML, 'ftp://127.0.0.1//nsi//' + taf.rutaXML AS sRutaXML FROM tFactura tf inner join tArchivosFactura taf ON tf.idFactura=taf.idFactura WHERE tf.idFactura in (select idFactura from v_ListaIntegracionFacturas " + context.Request["sWhere"] + ") and idSubReferencia = " + sIdSubReferencia;
        }
        else if (sIdGasto != null && sIdGasto != "")
        {
            //prueba local 
            sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, " + sDirectorio + " + taf.rutaPDF AS sRutaPDF, taf.nomXML AS sNombreXML, " + sDirectorio + " + taf.rutaXML AS sRutaXML FROM tFactura tf inner join tArchivosFactura taf ON tf.idFactura=taf.idFactura WHERE tf.idFactura in (select v.idFactura from " + sVista + " v" + context.Request["sWhere"] + ")";
            //prueba servidor
            //sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, 'ftp://127.0.0.1//nsi//' + taf.rutaPDF AS sRutaPDF, taf.nomXML AS sNombreXML, 'ftp://127.0.0.1//nsi//' + taf.rutaXML AS sRutaXML FROM tFactura tf inner join tArchivosFactura taf ON tf.idFactura=taf.idFactura WHERE tf.idFactura in (select idFactura from v_ListaIntegracionFacturas " + context.Request["sWhere"] + ") and idSubReferencia = " + sIdSubReferencia;
        }
        else if (sIdSubReferencia != null && sIdSubReferencia != "")
        {
            //prueba local 
            sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, " + sDirectorio + " + taf.rutaPDF AS sRutaPDF, taf.nomXML AS sNombreXML, " + sDirectorio + " + taf.rutaXML AS sRutaXML FROM tFactura tf inner join tArchivosFactura taf ON tf.idFactura=taf.idFactura WHERE tf.idFactura in (select v.idFactura from " + sVista + " v" + context.Request["sWhere"] + ") and idSubReferencia = " + sIdSubReferencia;
            //prueba servidor
            //sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, 'ftp://127.0.0.1//nsi//' + taf.rutaPDF AS sRutaPDF, taf.nomXML AS sNombreXML, 'ftp://127.0.0.1//nsi//' + taf.rutaXML AS sRutaXML FROM tFactura tf inner join tArchivosFactura taf ON tf.idFactura=taf.idFactura WHERE tf.idFactura in (select idFactura from v_ListaIntegracionFacturas " + context.Request["sWhere"] + ") and idSubReferencia = " + sIdSubReferencia;
        }
        else
        {
            //prueba local 
            sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, " + sDirectorio + " + taf.rutaPDF AS sRutaPDF, taf.nomXML AS sNombreXML, " + sDirectorio + " + taf.rutaXML AS sRutaXML FROM tFactura tf inner join tArchivosFactura taf ON tf.idFactura=taf.idFactura WHERE tf.idFactura in (select v.idFactura from " + sVista + " v" + context.Request["sWhere"] + ")";
            //prueba servidor
            //sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, 'ftp://127.0.0.1//nsi//' + taf.rutaPDF AS sRutaPDF, taf.nomXML AS sNombreXML, 'ftp://127.0.0.1//nsi//' + taf.rutaXML AS sRutaXML FROM tFactura tf inner join tArchivosFactura taf ON tf.idFactura=taf.idFactura WHERE tf.idFactura in (select idFactura from v_ListaIntegracionFacturas " + context.Request["sWhere"] + ")";
        }

        if (isNotaCredito == "1")
        {

            objConexion.ejecutaRecuperaObjetoLista<Factura>(sQuery, arrAtributos, lstFactura);
            //Ciclo que crea los archivos .pdf y .xml de facturas
            for (int i = 0; i < lstFactura.Count; i++)
            {
                // C R E A C I Ó N  A R C H I V O  P D F  I N I C I O
                string sRutaArchivo = context.Server.MapPath("../../Documentos/Factura/FacturaZIP/FacturaZIP/" + lstFactura[i].sNoFactura + ".pdf");
                string sRuta = "../../Documentos/FacturaZIP/FacturaZIP/" + lstFactura[i].sNoFactura + ".pdf";

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

                    sRutaArchivo = context.Server.MapPath("../../Documentos/Factura/FacturaZIP/FacturaZIP/" + lstFactura[i].sNoFactura + ".xml");
                    sRuta = "../../Documentos/Factura/FacturaZIP/FacturaZIP/" + lstFactura[i].sNoFactura + ".xml";

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
            ///generar nota credito archivos

            objConexion.ejecutaRecuperaObjetoLista<NotaCredito>(sQueryNC, arrAtributosNC, lstNotaCredito);
            //Ciclo que crea los archivos .pdf y .xml de notas de credito
            for (int i = 0; i < lstNotaCredito.Count; i++)
            {
                // C R E A C I Ó N  A R C H I V O  P D F  I N I C I O
                string sRutaArchivo = context.Server.MapPath("../../Documentos/Factura/FacturaZIP/FacturaZIP/NotaCredito-" + lstNotaCredito[i].sNoNotaCredito + ".pdf");
                string sRuta = "../../Documentos/Factura/FacturaZIP/FacturaZIP/NotaCredito-" + lstNotaCredito[i].sNoNotaCredito + ".pdf";

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

                if (lstNotaCredito[i].sRutaXML != null)
                {
                    // C R E A C I Ó N  A R C H I V O  X M L  I N I C I O
                    sRutaArchivo = context.Server.MapPath("../../Documentos/Factura/FacturaZIP/FacturaZIP/NotaCredito-" + lstNotaCredito[i].sNoNotaCredito + ".xml");
                    sRuta = "../../Documentos/Factura/FacturaZIP/FacturaZIP/NotaCredito-" + lstNotaCredito[i].sNoNotaCredito + ".xml";

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
            objArchivoZIP.fn_ComprimirCarpetaZIP(sPath, "Facturas", "Factura/FacturaZIP");

            ////Eliminación de todos los archivos de la carpeta
            string[] ficherosCarpeta = Directory.GetFiles(sPath);
            foreach (string ficheroActual in ficherosCarpeta)
                File.Delete(ficheroActual);


        }
        else
        {

            objConexion.ejecutaRecuperaObjetoLista<Factura>(sQuery, arrAtributos, lstFactura);
            //Ciclo que crea los archivos .pdf y .xml
            for (int i = 0; i < lstFactura.Count; i++)
            {
                // C R E A C I Ó N  A R C H I V O  P D F  I N I C I O
                string sRutaArchivo = context.Server.MapPath("../../Documentos/Factura/FacturaZIP/FacturaZIP/" + lstFactura[i].sNoFactura + ".pdf");
                string sRuta = "../../Documentos/FacturaZIP/FacturaZIP/" + lstFactura[i].sNoFactura + ".pdf";

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
                try
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        ((FtpWebResponse)request.GetResponse()).GetResponseStream().CopyTo(stream);
                        System.IO.File.WriteAllBytes(sRutaArchivo, stream.ToArray());
                    }

                }
                catch (Exception ex)
                {
                    bEncontroTodosLosArchivos = false;
                    string sError = lstFactura[i].sNoFactura + "|" + ex.Message + "|";
                    System.IO.File.Delete(sRutaArchivo);
                }
                // C R E A C I Ó N  A R C H I V O  P D F  F I N

                if (lstFactura[i].sRutaXML != null)
                {
                    // C R E A C I Ó N  A R C H I V O  X M L  I N I C I O

                    sRutaArchivo = context.Server.MapPath("../../Documentos/Factura/FacturaZIP/FacturaZIP/" + lstFactura[i].sNoFactura + ".xml");
                    sRuta = "../../Documentos/Factura/FacturaZIP/FacturaZIP/" + lstFactura[i].sNoFactura + ".xml";

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
                    try
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            ((FtpWebResponse)request.GetResponse()).GetResponseStream().CopyTo(stream);
                            System.IO.File.WriteAllBytes(sRutaArchivo, stream.ToArray());
                        }

                    }
                    catch (Exception ex)
                    {
                        bEncontroTodosLosArchivos = false;
                        string sError = lstFactura[i].sNoFactura + "|" + ex.Message + "|";
                        System.IO.File.Delete(sRutaArchivo);
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

            //if (bEncontroTodosLosArchivos)
            //{
            //    objFactura.sMensaje = "Se ha generado el ZIP, pero no se han ubicado todos los archivos.";
            //    objFactura.iResultado = 1;
            //}
            //Do the operation as required
            //Devuelve la ruta como respuesta a la llamada ajax
            System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string returnRes = javaScriptSerializer.Serialize(objFactura);
            context.Response.ContentType = "text/html";
            context.Response.Write(returnRes);

            //Creación del ZIP
            ArchivoZip objArchivoZIP = new ArchivoZip();
            objArchivoZIP.fn_ComprimirCarpetaZIP(sPath, "Facturas", "Factura/FacturaZIP");

            ////Eliminación de todos los archivos de la carpeta
            string[] ficherosCarpeta = Directory.GetFiles(sPath);
            foreach (string ficheroActual in ficherosCarpeta)
                File.Delete(ficheroActual);
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
