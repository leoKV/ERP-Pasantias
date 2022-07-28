using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vista_Diseno_BrearCrumbs : System.Web.UI.UserControl
{
    private string resultado = "";
    public void migajas(string[] datosB, string[] url)
    {
        int total = 0;
        string res = "<ol class='breadcrumb'>";

        //Inicio <strong>│</strong> Contenidos <strong>│</strong> SEGURIDAD EN LA CADENA DE SUMINISTRO

        if (datosB.Length > 0)
        {
            total = datosB.Length;
            for (int i = 0; i < datosB.Length; i++)
            {
                ///VERIFICA SI ES EL ULTIMO REGISTRO PARA ASIGNAR CLASE ACTIVA
                if (i == (total - 1))
                {
                    res += "<li class='Active'>" + datosB[i] + "</li>";
                }///ELSE DE QUE ES UNA SECUENCIA ANTERIOR
                else
                {
                    res += "<li><a href='"+ url[i] + "'>" + datosB[i] + "</a></li>";
                }

            }

        }
        res += "</ol>";

        resultado = res;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cb1.Text = resultado;
    }
}