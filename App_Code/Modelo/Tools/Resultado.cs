using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Permite regresar una estructura de datos para informar el resultado de la ejecución de un proceso.
/// </summary>
public class Resultado
{
    public int iResultado { set; get; }
    public string sMensaje { set; get; }
    public string sRuta { set; get; }

    public Resultado()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}