using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;

public class Reporte
{
    public Reporte()
    {

    }

    public string sMensaje { get; set; }
    public string sRutaPDF { get; set; }
    public string sRutaVerPDF { get; set; }
    List<string> lstColumnasExcel = new List<string>();
    public string sRuta { get; set; }
    public int iResultado { get; set; }
    public string sNombreArchivo { get; set; }
    public string sFechaInicio { get; set; }
    public string sFechaFin { get; set; }
    public int iIdUsuario { get; set; }
    public int iTipo { get; set; }
    public string sEstatus { get; set; }
    public string sAduana { get; set; }
    public int iIdClienteContable { get; set; }
    public List<Referencia> lstReferencia { get; set; }


}