using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
/// Descripción breve de ArchivosXML
/// </summary>
public class ArchivoXML
{
    public string sRutaProyecto { get; set; }
    public string sCarpeta { get; set; }
    public string sCarpetaCont { get; set; }
    public string sRutaXml { get; set; }
    public string sNomArchivoXml { get; set; }
    public string sNomArchivoPdf { get; set; }
    public int iIdSubReferencia { get; set; }
    public string sRemitente { get; set; }
    public int iResultado { get; set; }
    public string sMensaje { get; set; }
    public int iResultadoArchivo { get; set; }

    public ArchivoXML()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /// <summary>
    /// Método para cambiar el nombre de un atributo en el xml a minusculas
    /// </summary>
    /// <param name="xDoc"></param>
    /// <param name="sNombre"></param>
    public void fn_CambiarAtributoLower(XmlDocument xDoc, string sNombre)
    {
        try
        {
            //cambia el nombre del atributo a minusculas
            xDoc.LoadXml(xDoc.InnerXml.Replace(sNombre, sNombre.ToLower()));
        }
        catch (Exception ex)
        {
            //si ocurre una excepcion solo se controla que no se detenga la aplicación
        }
    }

    /// <summary>
    /// Método para cambiar el nombre de un atributo en el xml
    /// </summary>
    /// <param name="xDoc"></param>
    /// <param name="sNomAnterior"></param>
    /// <param name="sNomNuevo"></param>
    public void fn_CambiaNombreAtributo(XmlDocument xDoc, string sNomAnterior, string sNomNuevo)
    {
        try
        {
            //reemplaza el nombre del atributo
            xDoc.LoadXml(xDoc.InnerXml.Replace(sNomAnterior, sNomNuevo));
        }
        catch (Exception ex)
        {
            //si ocurre una excepcion solo se controla que no se detenga la aplicación
        }
    }
    /// <summary>
    /// Método que valida que exista archivo xml y pdf
    /// </summary>
    /// <param name="objFacturaXML"></param>
    public void fn_ValidaNombreArchivo(ArchivoXML objArchivo)
    {
        try
        {
            //si el nombre de los xml no esta en nulo
            if (objArchivo.sNomArchivoXml != null && objArchivo.sNomArchivoPdf != null)
            {
                //el nombre de los archivos es valido
                objArchivo.iResultado = 1;
                objArchivo.sMensaje = "El nombre de los archivos coincide.";
            }
            else
            {
                //valida cual nombre de archivo esta vacío
                if (objArchivo.sNomArchivoXml != null)
                {
                    //mensaje error en el nombre del pdf
                    objArchivo.sMensaje = "No se cargo el archivo pdf para el documento xml: " + objArchivo.sNomArchivoXml + "";
                }
                else
                {
                    //mensaje error en el nombre del xml
                    objArchivo.sMensaje = "No se cargo el archivo xml para el documento pdf: " + objArchivo.sNomArchivoPdf + "";
                }
                //respuesta de error
                objArchivo.iResultado = 0;
            }
        }
        catch (Exception ex)
        {
            //respuesta de error
            objArchivo.iResultado = 0;
            objArchivo.sMensaje = "Excepción al validar nombre de archivos: " + ex.Message.ToString();
        }
    }

}