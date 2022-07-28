using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de EtiquetaXML
/// </summary>
public class EtiquetaXML
{
    public string sEtiqueta { get; set; }
    public string sAtributo { get; set; }
    public string sTag { get; set; }
    public int  iNivel { get; set; }
	public string sValor { get; set; }
	public string sEtiquetaPadre { get; set; }
	public string bRepetido { get; set; }
	public EtiquetaXML()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}