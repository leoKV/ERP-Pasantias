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
        //Se desencripta el id de la solicitud
        Security secIdSolicitud = new Security(context.Request["sIdSolicitud"].ToString());
        int iIdSolicitud = int.Parse(secIdSolicitud.DesEncriptar());
        int iTipo = int.Parse(context.Request["iTipo"].ToString());
        //Se obtiene nombre archivo
        DateTime Hoy = DateTime.Now;
        string nombreArchivo = "SolicitudTransferencia_" +iIdSolicitud+ ".pdf";
        string rutaImagen = HttpContext.Current.Server.MapPath("../../Styles/Imagenes/LogotipoAltaResolucion.png");
        string rutaArchivo = HttpContext.Current.Server.MapPath("../../Documentos/SolicitudTransferencia/" + nombreArchivo);
        //--- Comenzar a contruir documento en PDF. 
        Document document = new Document(PageSize.A4, 50, 50, 50, 50);
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write, FileShare.None));
        //Se crea objeto
        SolicitudTransferencia objSolicitudTransferencia = new SolicitudTransferencia();
        ///INICIO TRY D:/
        try
        {
            //Se pasan los parametros
            objSolicitudTransferencia.iIdSolicitudTransferencia = int.Parse(secIdSolicitud.DesEncriptar());
            //Se instancia la conexión
            Conexion objConexion = new Conexion();
            //Se crea arreglo con atributos
            string[] arrAtributos = { "fMontoTotal", "sFechaAlta", "sHoraAlta", "sNombreSolicitante", "sFirmaOperaciones", "sEncargadoTesoreria","sNumeroSolicitud","sCargoA","sComisionista" };
            //Se crea la consulta
            string sQuery = "SELECT tst.montoTotal AS fMontoTotal, CONVERT(VARCHAR(10),fechaAlta,103) AS sFechaAlta, (SUBSTRING(CONVERT(VARCHAR(30),fechaAlta,100),14,12)) AS sHoraAlta, nombreSolicitante AS sNombreSolicitante, firmaOperaciones AS sFirmaOperaciones, encargadoTesoreria AS sEncargadoTesoreria," +
                " noSolicitudTransferencia as sNumeroSolicitud,CargoA as sCargoA,case when(Comisionista=1)then 'NAD GLOBAL' else 'CLIENTE' end as sComisionista FROM tSolicitudTransferencia tst WHERE tst.idSolicitudTransferencia=" + objSolicitudTransferencia.iIdSolicitudTransferencia;
            //Se ejecuta el método para obtener datos
            objConexion.ejecutaRecuperaObjeto<SolicitudTransferencia>(sQuery, arrAtributos, objSolicitudTransferencia);
            //Se ejecuta el método
            objSolicitudTransferencia.fn_ObtenerDatosSolicitud(objSolicitudTransferencia);
            //Se crea la estructura del PDF 
            objSolicitudTransferencia.sMensaje +=
            "<table width='100%' cellspacing='0' cellpadding='0' style='font-family: Garamond; font-size:6px;' border='0'>" +
                "<tbody>" +
                "<tr>" +
                    "<td colspan='5' border='0.5'><img src='" + rutaImagen + "' alt='Encabezado PDF' width='100%' /></td>" +
                    "<td border='0.5' colspan='15' style='text-align: center;'>" +
                        "<table rules=cols><tr border='0'>" +
                        "<td border='0' colspan='5'></td>" +
                            "<td border='0' colspan='10' style='border-top:1px;border-right:1px solid;'> <label style='font-size:20px !important;'><b style='font-size:20px !important;'>NAD GLOBAL</b></label></td>" +
                            "<td border='0' colspan='4'>" +
                                "<table>" +
                                    "<tr border='0'><td></td></tr>"+
                                    "<tr border='0.5'><td colspan='3'><label style='font-size:30px !important;'><b style='font-size:7px !important;'>"+objSolicitudTransferencia.sNumeroSolicitud+"</b></label></td><td border='0' colspan='1'></td></tr>" +
                                    "<tr border='0'><td></td></tr>"+
                                "</table>"+
                            "</td>" +
                            "<td border='0' colspan='1'></td>" +
                        "</tr>" +
                        "<tr border='0'  style='border-rigth:1px;'><td colspan='20'><label>SOLICITUD DE PAGO PARA FLETES, DESCONSOLIDACION, MANIOBRAS, ALMACENAJE, OTROS GASTOS A LÍNEAS AÉREAS Y CONSOLIDADORAS DE CARGA</label></td><tr>" +
                    "</table></td>" +
                "</tr>" +
                "<tr border='0.5'>" +
                    "<td colspan='1' style='text-align: center;'>No</td>" +
                    "<td colspan='2' style='text-align: center;'>Cliente Primario</td>" +
                    "<td colspan='2' style='text-align: center;'>Cliente Secundario</td>" +
                    "<td colspan='2' style='text-align: center;'>Concepto a pagar</td>" +
                    "<td colspan='1' style='text-align: center;'>Forma de pago</td>" +
                    "<td colspan='1' style='text-align: center;'>Total</td>" +
                    "<td colspan='3' style='text-align: center;'>Beneficiario</td>" +
                    "<td colspan='2' style='text-align: center;'>Control</td>" +
                    "<td colspan='2' style='text-align: center;'>Guía House</td>" +
                    "<td colspan='2' style='text-align: center;'>Guía Master</td>" +
                    "<td colspan='2' style='text-align: center;'>Observaciones</td>" +
                "</tr>";

            sQuery = "";
            //Se valida el tipo de solicitud y se crea una query diferente
            if (iTipo == 3)
            {
                sQuery = @"SELECT ROW_NUMBER() OVER(ORDER BY tst.idSolicitudTransferencia ASC) AS iNumero, 
                        (SELECT tc.nomCliente FROM tCliente tc WHERE tc.idCliente=tst.idClientePrimario) AS sClientePrimario, 
                        (SELECT tc.nomCliente FROM tCliente tc WHERE tc.idCliente=tst.idClienteSecundario) AS sClienteSecundario, 
                        'Pago de Multa' AS sServicio,
                        'TRANSFERENCIA' AS sFormaPago, tst.montoTotal AS sMontoServicio, 
                        (SELECT tp.nomProveedor FROM tProveedor tp WHERE tp.idProveedor=tst.idProveedor) AS sBeneficiario, 
                        (SELECT cb.nomBanco FROM cBanco cb WHERE cb.idBanco=tst.idBanco) AS sBanco, 
                        tst.noCuentaBancaria AS sCuenta, 
                        (SELECT tpcb.cuentaClabe FROM tProveedorCuentasBancarias tpcb WHERE tpcb.numCuenta=tst.noCuentaBancaria and tpcb.idProveedor = tst.idProveedor) AS sClabe, 
                        (SELECT tsubr.refAdministrativa FROM tSubReferencia tsubr WHERE tsubr.idSubReferencia=tst.idSubReferencia) AS sControl, 
                        tst.noGuiaHouse AS sGuiaHouse, 
                        tst.noGuiaMaster AS sGuiaMaster, 
                        tst.observaciones AS sObservaciones 
                        FROM tSolicitudTransferencia tst where tst.idSolicitudTransferencia="+objSolicitudTransferencia.iIdSolicitudTransferencia;
            }
            else
            {
                //Consulta para obtener datos 
                sQuery = "SELECT ROW_NUMBER() OVER(ORDER BY tsst.idServicioSolTransferencia ASC) AS iNumero, " +
                         "(SELECT tc.nomCliente FROM tCliente tc WHERE tc.idCliente=tst.idClientePrimario) AS sClientePrimario, " +
                          "(SELECT tc.nomCliente FROM tCliente tc WHERE tc.idCliente=tst.idClienteSecundario) AS sClienteSecundario, " +
                         "case when(tst.idTipoSolicitudTrans=3)then 'Pago de Multa' else (SELECT cs.descripcion FROM cServicio cs WHERE cs.idServicio=tsst.idServicio) end AS sServicio, " +
                         "'TRANSFERENCIA' AS sFormaPago, tsst.total AS sMontoServicio, " +
                         "(SELECT tp.nomProveedor FROM tProveedor tp WHERE tp.idProveedor=tst.idProveedor) AS sBeneficiario, " +
                         "(SELECT cb.nomBanco FROM cBanco cb WHERE cb.idBanco=tst.idBanco) AS sBanco, " +
                         "tst.noCuentaBancaria AS sCuenta, " +
                         "(SELECT tpcb.cuentaClabe FROM tProveedorCuentasBancarias tpcb WHERE tpcb.numCuenta=tst.noCuentaBancaria and tpcb.idProveedor = tst.idProveedor) AS sClabe, " +
                         "(SELECT tsubr.refAdministrativa FROM tSubReferencia tsubr WHERE tsubr.idSubReferencia=tst.idSubReferencia) AS sControl, " +
                         "tst.noGuiaHouse AS sGuiaHouse, " +
                         "tst.noGuiaMaster AS sGuiaMaster, " +
                         "tst.observaciones AS sObservaciones " +
                         "FROM tServicioSolicitudTransferencia tsst inner join tSolicitudTransferencia tst ON tsst.idSolicitudTransferencia=tst.idSolicitudTransferencia WHERE tst.idSolicitudTransferencia=" + objSolicitudTransferencia.iIdSolicitudTransferencia;
            }
            //Se crea la variable para almacenar datos
            DataSet dsDatos;
            //Se ejecuta la consulta para obtener los datos
            dsDatos = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "concepto");
            //Se verifica que se tengan datos
            if (dsDatos.Tables["concepto"].Rows.Count > 0)
            {
                foreach (DataRow registro in dsDatos.Tables["concepto"].Rows)
                {
                    objSolicitudTransferencia.sMensaje +=
                        "<tr border='0.5'>" +
                            "<td colspan='1' style='text-align: center;'>" + registro["iNumero"] + "</td>" +
                            "<td colspan='2' style='text-align: center;'>" + registro["sClientePrimario"] + "</td>" +
                             "<td colspan='2' style='text-align: center;'>" + registro["sClienteSecundario"] + "</td>" +
                            "<td colspan='2' style='text-align: center;'>" + registro["sServicio"] + "</td>" +
                            "<td colspan='1' style='text-align: center;'>" + registro["sFormaPago"] + "</td>" +
                            "<td colspan='1' style='text-align: center;'>$" + registro["sMontoServicio"] + "</td>" +
                            "<td colspan='3' style='text-align: center;'>" + registro["sBeneficiario"] + "<br />Banco: " + registro["sBanco"] + "<br />Cuenta: " + registro["sCuenta"] + "<br /> Clabe: " + registro["sClabe"] + "</td>" +
                            "<td colspan='2' style='text-align: center;'>" + registro["sControl"] + "</td>" +
                            "<td colspan='2' style='text-align: center;'>" + registro["sGuiaHouse"] + "</td>" +
                            "<td colspan='2' style='text-align: center;'>" + registro["sGuiaMaster"] + "</td>" +
                            "<td colspan='2' style='text-align: center;'>" + registro["sObservaciones"] + "</td>" +
                        "</tr>";
                }
            }
            else
            {
                objSolicitudTransferencia.sMensaje +=
                    "<tr border='0.5'>" +
                        "<td colspan='20' style='text-align: center;'>No existen servicios relacionados a la solicitud</td>" +
                    "</tr>";
            }

            objSolicitudTransferencia.sMensaje +=
                "<tr border='0.5'>" +
                    "<td colspan='8' style='text-align: center;'>Monto Solicitud Transferencia: </td>" +
                    "<td colspan='2' style='text-align: center;'>$" + objSolicitudTransferencia.fMontoTotal + "</td>" +
                    "<td colspan='10' style='text-align: center;'></td>" +
                "</tr>";

            //Se valida si el tipo de solicitud es pago multa
            if (iTipo == 3)
            {
                objSolicitudTransferencia.sMensaje +=
                "<tr border='0.5'>" +
                    "<td colspan='2' style='text-align: center;'>Cargo a: </td>" +
                    "<td colspan='8' style='text-align: center;'>"+objSolicitudTransferencia.sCargoA+"</td>" +
                    "<td colspan='2' style='text-align: center;'>Comisionista: </td>" +
                    "<td colspan='8' style='text-align: center;'>"+objSolicitudTransferencia.sComisionista+"</td>" +
                "</tr>";
            }

            objSolicitudTransferencia.sMensaje +=
            "<tr border='0.5'>" +
                "<td colspan='4' style='text-align: left; padding-left:2px;'>Fecha: " + objSolicitudTransferencia.sFechaAlta + "</td>" +
                "<td colspan='2' rowspan='2' style='text-align: center;'>PAF06-03</td>" +
                "<td colspan='7' style='text-align: left; padding-left:2px;'>Solicitado por " + objSolicitudTransferencia.sNombreSolicitante + "</td>" +
                "<td colspan='7' style='text-align: left; padding-left:2px;'>Firma de operaciones " + objSolicitudTransferencia.sFirmaOperaciones + "</td>" +
            "</tr>" +
            "<tr border='0.5'>" +
                "<td colspan='4' style='text-align: left; padding-left:2px;'>Hora: " + objSolicitudTransferencia.sHoraAlta + "</td>" +
                "<td colspan='14' style='text-align: left; padding-left:2px;'>Recibido por " + objSolicitudTransferencia.sEncargadoTesoreria + "</td>" +
            "</tr>" +
            "</tbody>" +
            "</table>";
            //obj_recibo.sMensaje += obj_recibo.sContenidoIncrementables;
            List<IElement> lstTable1;
            ///Base de color para fuente
            var FontColour = new BaseColor(29, 102, 136);
            ///Nuevo parrafo de footer
            Paragraph footer = new Paragraph("Solicitud de transferencia\n\n©NAD Global 2016. ©Todos los derechos reservados.", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, iTextSharp.text.Font.NORMAL, FontColour));
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
            lstTable1 = HTMLWorker.ParseToList(new StringReader(objSolicitudTransferencia.sMensaje), null);
            document.Add((IElement)lstTable1[0]);
            document.Add(new Paragraph("\n"));
            ///Se escribe el footer en el documento
            if (objSolicitudTransferencia.sMensaje.Length > 1500)
                footerTbl.WriteSelectedRows(0, -1, 50, 30, writer.DirectContent);
            //document.Add(new Paragraph("\n"));

            document.Close();
            document.Dispose();


            string rutaProyecto = HttpContext.Current.Server.MapPath("~\\Documentos\\");

            string a = rutaProyecto + "SolicitudTransferencia" + "\\" + nombreArchivo;
            //instancia clase para mover archivos a direccion ftp
            ServicioFtp objServicioFtp = new ServicioFtp("transferenciaArchivo");
            //obtine configuracion del servidor ftp
            objServicioFtp.fn_ObtenerServidorFtp(objServicioFtp);

            //se crea una instancia para guardar la información del pdf
            ArchivoFactura objArchivoFactura = new ArchivoFactura();
            objArchivoFactura.sCarpetaCont = "SolicitudTransferencia";
            objArchivoFactura.sCarpeta = "SolicitudDeTransferencia";
            objArchivoFactura.sNomArchivoPdf = nombreArchivo;
            //objArchivoFactura.iIdSubReferencia = iIdSubReferenciaCuerpo;
            objArchivoFactura.sRutaProyecto = rutaProyecto + "SolicitudTransferencia";
            try
            {
                //Se pasan los parametros

                objSolicitudTransferencia.sRutaPDF = objArchivoFactura.sCarpeta + "\\" + objArchivoFactura.sNomArchivoPdf;
                objSolicitudTransferencia.sNombreArchivo = objArchivoFactura.sNomArchivoPdf;
                //Se ejecuta el método para guardar 
                objSolicitudTransferencia.fn_GuardarDreccionPdfSolicitud(objSolicitudTransferencia);


                //transfiere archivo al servidor ftp
                objServicioFtp.fn_MoverArchivoAFtpPDFSolicitud(objServicioFtp, objArchivoFactura, objArchivoFactura.sRutaProyecto);

            }
            catch (Exception ex)
            {
                objServicioFtp.sMensaje = ex.Message.ToString();

            }
            //slimina el archivo
            if (System.IO.File.Exists(rutaProyecto + "SolicitudTransferencia" + "\\" + nombreArchivo))
                System.IO.File.Delete(rutaProyecto + "SolicitudTransferencia" + "\\" + nombreArchivo);
            //Se retorna el objeto

        }///FIN TRY
         ///INICIO CATCH
        catch (Exception ex)
        {
            objSolicitudTransferencia.iResultado = 0;
            objSolicitudTransferencia.sMensaje = "Error: " + ex.Message;

            document.Close();
            document.Dispose();
        }///FIN CATCH
        //Do the operation as required
        System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string returnRes = javaScriptSerializer.Serialize(objSolicitudTransferencia);
        context.Response.ContentType = "text/html";
        context.Response.Write(returnRes);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}