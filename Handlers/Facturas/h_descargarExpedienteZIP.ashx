<%@ WebHandler Language="C#" Class="h_descargarExpedienteZIP" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class h_descargarExpedienteZIP : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        List<string> lstFacturasNEncontradas = new List<string>();
        //Se instancia la conexión
        Conexion objConexion = new Conexion();
        Factura objFactura = new Factura();
        //NotaCredito objNotaCredito = new NotaCredito();
        //Se crea la lista
        List<Factura> lstFactura = new List<Factura>();
        List<Factura> lstFacturaTimbrador = new List<Factura>();

        string sPath = context.Server.MapPath("../../Documentos/Expediente/ExpedienteZIP/");
        string sRutaZIP = "../../Documentos/Expediente/ExpedienteZIP.zip";
        string sWhere = context.Request["sWhere"];
        string sQuery = "";
        string sVista = context.Request["sVista"];
        string isNotaCredito = context.Request["isNotaCredito"];

        string sIdCliente = "";
        string sNombreCliente = "";
        string sFechaInicio = "";
        string sFechaFin = "";
        bool bContingencia = true;

        string sDirectorio = "";

        if (bContingencia)
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

        // Obtiene el Cliente.
        if (context.Request["sIdCliente"] != null)
        {
            Security oSecCliente = new Security(context.Request["sIdCliente"]);
            sIdCliente = oSecCliente.DesEncriptar();
        }

        // Define el periodo.
        // Obtiene el Cliente.
        if (context.Request["sFechaInicio"] != null)
        {
            sFechaInicio = context.Request["sFechaInicio"];
        }
        // Obtiene el Cliente.
        if (context.Request["sFechaFin"] != null)
        {
            sFechaFin = context.Request["sFechaFin"];
        }

        string[] aAtributosF = { "sNoFactura", "sNombrePDF", "sRutaPDF", "sNombreXML", "sRutaXML" };
        string[] aAtributosFT = { "sNoFactura", "sNombrePDF", "sRutaPDF", "sNombreXML", "sRutaXML", "sSerie" };

        List<Expediente> lstExpediente = new List<Expediente>();
        string[] aAtributos = new string[] { "sFolioPedimento", "iIdAduana", "iIdPatente", "sPedimento" };

        string sRutaArchivoGeneral = context.Server.MapPath("../../Documentos/Expediente/ExpedienteZIP/");
        string sRutaGeneral = "../../Documentos/Expediente/ExpedienteZIP/";
        string sRutaArchivoPedimentoTerceros = "";
        string sRutaPedimentoTerceros = "";
        string sRutaArchivoPedimentoGastos = "";
        string sRutaPedimentoGastos = "";

        // Obtiene el Nombre del Cliente.
        sQuery = "select nomCliente from tCliente where idCliente = " + sIdCliente;
        string[] aObtenerNombreCliente = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        sNombreCliente = aObtenerNombreCliente[1];

        string sCondicionCliente = "";
        if (sIdCliente != "0")
        {
            sCondicionCliente = " and idClienteOperativo = " + sIdCliente + " ";
        }
        sQuery = @"select ca.aduana + '-' + cp.patente + '-' + ts.pedimento as sFolioPedimento, 
                        ts.idAduana as iIdAduana, ts.idPatente as iIdPatente, 
                        ts.pedimento as sPedimento,
                        FORMAt(tp.fechaPago,'yyyy-MM-dd') as sFechaPago
                        from tSubReferencia ts 
                        inner join cAduana ca on ts.idAduana = ca.idAduana
                        inner join cPatente cp on ts.idPatente = cp.idPatente
                        left join tPedimento tp on ts.idSubReferencia = tp.idSubReferencia
							                        or (ts.idAduana = tp.idAduana
							                        and ts.idPatente = tp.idPatente
							                        and ts.pedimento = tp.pedimento
							                        and ts.idCvePedimento = tp.idCveDocumento)
                        where tp.fechaPago between '" + sFechaInicio + "' and '" + sFechaFin + @"'
                        " + sCondicionCliente + @"
                        group by ca.aduana, cp.patente, ts.pedimento,ts.idAduana, ts.idPatente,tp.fechaPago";
        objConexion.ejecutaRecuperaObjetoLista<Expediente>(sQuery, aAtributos, lstExpediente);

        if (!Directory.Exists(sRutaArchivoGeneral))
        {
            Directory.CreateDirectory(sRutaArchivoGeneral);
        }
        else
        {
            Directory.Delete(sRutaArchivoGeneral, true);
            Directory.CreateDirectory(sRutaArchivoGeneral);
        }

        List<string> lstIdFacturaTimbrador = new List<string>();

        foreach (Expediente oExpediente in lstExpediente)
        {
            sQuery = @"SELECT tf.idFacturaTimbrador
                            FROM tFacturaTimbrador tf 
                            inner join tOrdenVenta tov on tf.folioOVNADSI = tov.folioOrdenVenta
                            inner join tFolioTransitorioSubReferencia tfts on tov.idFolioTransitorio = tfts.idFolioTransitorio
                            inner join tSubReferencia ts on tfts.idSubReferencia = ts.idSubReferencia
                            left join tPedimento tp on ts.idSubReferencia = tp.idSubReferencia
			                            or (ts.idAduana = tp.idAduana
			                            and ts.idPatente = tp.idPatente
			                            and ts.pedimento = tp.pedimento
			                            and ts.idCvePedimento = tp.idCveDocumento)
                            where tp.fechaPago between '" + sFechaInicio + "' and '" + sFechaFin + @"'
                            and ts.idAduana = " + oExpediente.iIdAduana + @" and ts.idPatente = " + oExpediente.iIdPatente + @" and ts.pedimento = '" + oExpediente.sPedimento + @"'
                            and tf.idFacturaTimbrador not in (select idFacturaTimbrador from tArchivosFacturaTimbrador)
                            group by tf.idFacturaTimbrador";
            lstIdFacturaTimbrador = new List<string>();
            lstIdFacturaTimbrador = objConexion.ejecutarConsultaRegistroMultiples(sQuery);
            lstIdFacturaTimbrador.RemoveAt(0);
            foreach (string sIdFacturaTimbrador in lstIdFacturaTimbrador)
            {
                Factura oFactura = new Factura();
                oFactura.iIdFactura = int.Parse(sIdFacturaTimbrador);
                objFactura.fn_ValidarFacturaTimbrador(oFactura);
            }

            lstFactura = new List<Factura>();
            lstFacturaTimbrador = new List<Factura>();

            sRutaArchivoPedimentoTerceros = sRutaArchivoGeneral + oExpediente.sFolioPedimento + "/Terceros/";
            sRutaPedimentoTerceros = sRutaGeneral + oExpediente.sFolioPedimento + "/Terceros/";
            sRutaArchivoPedimentoGastos = sRutaArchivoGeneral + oExpediente.sFolioPedimento + "/Cuenta Gastos/";
            sRutaPedimentoGastos = sRutaGeneral + oExpediente.sFolioPedimento + "/Cuenta Gastos/";

            sQuery = @"select tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, " + sDirectorio + " + taf.rutaPDF AS sRutaPDF, taf.nomXML AS sNombreXML, " + sDirectorio + @" + taf.rutaXML AS sRutaXML
                from tSubReferencia ts 
                inner join tFactura tf on ts.idSubReferencia = tf.idSubReferencia
                inner join tArchivosFactura taf on tf.idFactura=taf.idFactura
                left join tPedimento tp on ts.idSubReferencia = tp.idSubReferencia
							or (ts.idAduana = tp.idAduana
							and ts.idPatente = tp.idPatente
							and ts.pedimento = tp.pedimento
							and ts.idCvePedimento = tp.idCveDocumento)
                where tp.fechaPago between '" + sFechaInicio + "' and '" + sFechaFin + @"'
                and ts.idAduana = " + oExpediente.iIdAduana + @" and ts.idPatente = " + oExpediente.iIdPatente + @" and ts.pedimento = '" + oExpediente.sPedimento + @"'
                group by tf.noFactura, taf.nomPDF, taf.rutaPDF, taf.nomXML, taf.rutaXML";

            objConexion.ejecutaRecuperaObjetoLista<Factura>(sQuery, aAtributosF, lstFactura);

            sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, " + sDirectorio + " + taf.rutaPDF AS sRutaPDF, taf.nomXML AS sNombreXML, " + sDirectorio + @" + taf.rutaXML AS sRutaXML, tf.noSerie as sSerie 
                FROM tFacturaTimbrador tf 
                inner join tArchivosFacturaTimbrador taf ON tf.idFacturaTimbrador=taf.idFacturaTimbrador 
                inner join tOrdenVenta tov on tf.folioOVNADSI = tov.folioOrdenVenta
                inner join tFolioTransitorioSubReferencia tfts on tov.idFolioTransitorio = tfts.idFolioTransitorio
                inner join tSubReferencia ts on tfts.idSubReferencia = ts.idSubReferencia
                left join tPedimento tp on ts.idSubReferencia = tp.idSubReferencia
							or (ts.idAduana = tp.idAduana
							and ts.idPatente = tp.idPatente
							and ts.pedimento = tp.pedimento
							and ts.idCvePedimento = tp.idCveDocumento)
                where tp.fechaPago  between '" + sFechaInicio + "' and '" + sFechaFin + @"'
                and ts.idAduana = " + oExpediente.iIdAduana + @" and ts.idPatente = " + oExpediente.iIdPatente + @" and ts.pedimento = '" + oExpediente.sPedimento + @"'
                group by  tf.noFactura, taf.nomPDF,  taf.rutaPDF, taf.nomXML, taf.rutaXML, tf.noSerie";

            objConexion.ejecutaRecuperaObjetoLista<Factura>(sQuery, aAtributosFT, lstFacturaTimbrador);

            //sQuery = @"select tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, " + sDirectorio + " + taf.rutaPDF AS sRutaPDF, taf.nomXML AS sNombreXML, " + sDirectorio + @" + taf.rutaXML AS sRutaXML
            //    from tSubReferencia ts 
            //    inner join tFactura tf on ts.idSubReferencia = tf.idSubReferencia
            //    inner join tArchivosFactura taf on tf.idFactura=taf.idFactura
            //    where tf.idCliente = " + sIdCliente + @" and tf.fechaFactura between '" + sFechaInicio + "' and '" + sFechaFin + @"'
            //    and ts.idAduana = " + oExpediente.iIdAduana + @" and ts.idPatente = " + oExpediente.iIdPatente + @" and ts.pedimento = '" + oExpediente.sPedimento + @"'";

            //objConexion.ejecutaRecuperaObjetoLista<Factura>(sQuery, aAtributosF, lstFactura);

            //sQuery = "SELECT tf.noFactura AS sNoFactura, taf.nomPDF AS sNombrePDF, " + sDirectorio + " + taf.rutaPDF AS sRutaPDF, taf.nomXML AS sNombreXML, " + sDirectorio + @" + taf.rutaXML AS sRutaXML, tf.noSerie as sSerie 
            //    FROM tFacturaTimbrador tf 
            //    inner join tArchivosFacturaTimbrador taf ON tf.idFacturaTimbrador=taf.idFacturaTimbrador 
            //    inner join tOrdenVenta tov on tf.folioOVNADSI = tov.folioOrdenVenta
            //    inner join tFolioTransitorioSubReferencia tfts on tov.idFolioTransitorio = tfts.idFolioTransitorio
            //    inner join tSubReferencia ts on tfts.idSubReferencia = ts.idSubReferencia
            //    where tf.nombreCliente like '%" + sNombreCliente + "%' and tf.fechaFactura between '" + sFechaInicio + "' and '" + sFechaFin + @"'
            //    and ts.idAduana = " + oExpediente.iIdAduana + @" and ts.idPatente = " + oExpediente.iIdPatente + @" and ts.pedimento = '" + oExpediente.sPedimento + @"'";

            //objConexion.ejecutaRecuperaObjetoLista<Factura>(sQuery, aAtributosFT, lstFacturaTimbrador);

            #region Proceso Facturas
            if (lstFactura.Count > 0)
            {
                if (!Directory.Exists(sRutaArchivoPedimentoTerceros))
                {
                    Directory.CreateDirectory(sRutaArchivoPedimentoTerceros);
                }
                else
                {
                    Directory.Delete(sRutaArchivoPedimentoTerceros, true);
                    Directory.CreateDirectory(sRutaArchivoPedimentoTerceros);
                }
            }

            if (lstFacturaTimbrador.Count > 0)
            {
                if (!Directory.Exists(sRutaArchivoPedimentoGastos))
                {
                    Directory.CreateDirectory(sRutaArchivoPedimentoGastos);
                }
                else
                {
                    Directory.Delete(sRutaArchivoPedimentoGastos, true);
                    Directory.CreateDirectory(sRutaArchivoPedimentoGastos);
                }
            }

            //Ciclo que crea los archivos .pdf y .xml
            for (int i = 0; i < lstFactura.Count; i++)
            {
                lstFactura[i].sNoFactura = lstFactura[i].sNoFactura.Replace('/', '-').Replace('\\', '-');
                // C R E A C I Ó N  A R C H I V O  P D F  I N I C I O
                string sRutaArchivo = sRutaArchivoPedimentoTerceros + lstFactura[i].sNoFactura + ".pdf";
                string sRuta = sRutaPedimentoTerceros + lstFactura[i].sNoFactura + ".pdf";

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
                    string sError = lstFactura[i].sNoFactura + "|" + oExpediente.sFolioPedimento + "|" + ex.Message;
                    lstFacturasNEncontradas.Add(sError);
                }
                // C R E A C I Ó N  A R C H I V O  P D F  F I N

                if (lstFactura[i].sRutaXML != null)
                {
                    // C R E A C I Ó N  A R C H I V O  X M L  I N I C I O

                    sRutaArchivo = sRutaArchivoPedimentoTerceros + lstFactura[i].sNoFactura + ".xml";
                    sRuta = sRutaPedimentoTerceros + lstFactura[i].sNoFactura + ".xml";

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
                        string sError = lstFactura[i].sNoFactura + "|" + oExpediente.sFolioPedimento + "|" + ex.Message;
                        lstFacturasNEncontradas.Add(sError);
                    }
                }
                // C R E A C I Ó N  A R C H I V O  X M L  F I N
            }

            //GuiaFacturasWs Timbrador
            //Ciclo que crea los archivos .pdf y .xml de facturas
            for (int i = 0; i < lstFacturaTimbrador.Count; i++)
            {
                lstFacturaTimbrador[i].sNoFactura = lstFacturaTimbrador[i].sNoFactura.Replace('/', '-').Replace('\\', '-');
                lstFacturaTimbrador[i].sSerie = lstFacturaTimbrador[i].sSerie.Replace('/', '-').Replace('\\', '-');
                // C R E A C I Ó N  A R C H I V O  P D F  I N I C I O
                string sRutaArchivo = sRutaArchivoPedimentoGastos + lstFacturaTimbrador[i].sSerie + lstFacturaTimbrador[i].sNoFactura + ".pdf";
                string sRuta = sRutaPedimentoGastos + lstFacturaTimbrador[i].sSerie + lstFacturaTimbrador[i].sNoFactura + ".pdf";

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
                FtpWebRequest request = (FtpWebRequest)System.Net.WebRequest.Create(lstFacturaTimbrador[i].sRutaPDF);
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
                    string sError = lstFacturaTimbrador[i].sNoFactura + "|" + oExpediente.sFolioPedimento + "|" + ex.Message;
                    lstFacturasNEncontradas.Add(sError);
                }

                // C R E A C I Ó N  A R C H I V O  P D F  F I N

                if (lstFacturaTimbrador[i].sRutaXML != null)
                {
                    // C R E A C I Ó N  A R C H I V O  X M L  I N I C I O

                    sRutaArchivo = sRutaArchivoPedimentoGastos + lstFacturaTimbrador[i].sSerie + lstFacturaTimbrador[i].sNoFactura + ".xml";
                    sRuta = sRutaPedimentoGastos + lstFacturaTimbrador[i].sSerie + lstFacturaTimbrador[i].sNoFactura + ".xml";

                    if (!System.IO.File.Exists(sRutaArchivo))
                    {

                        using (StreamWriter obj_docTXT = new StreamWriter(sRutaArchivo))
                        {
                            ///SE AGREGA LINEA
                            obj_docTXT.WriteLine("Hello World!");
                        }
                    }
                    request = (FtpWebRequest)System.Net.WebRequest.Create(lstFacturaTimbrador[i].sRutaXML);
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
                        string sError = lstFacturaTimbrador[i].sNoFactura + "|" + oExpediente.sFolioPedimento + "|" + ex.Message;
                        lstFacturasNEncontradas.Add(sError);
                    }
                }
                // C R E A C I Ó N  A R C H I V O  X M L  F I N
            }
            #endregion
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
        objArchivoZIP.fn_ComprimirCarpetaZIP(sPath, "ExpedienteZIP", "Expediente");

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
