using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public class ExpresionRegular
{
    public ExpresionRegular()
    {

    }

    public bool bError { get; set; }
    public string sMensaje { get; set; }

    /// <summary>
    /// Método que valida el formato de un texto con un tamaño mínimo y máximo
    /// </summary>
    /// <param name="sAValidar"></param>
    /// <param name="sExpresion"></param>
    /// <param name="iMin"></param>
    /// <param name="iMax"></param>
    /// <returns></returns>
    public void fn_ValidaExpercionRegular(ExpresionRegular objExpresionRegular, string sAValidar, string sExpresion, int iMin, int iMax, string sMensaje)
    {
        try
        {
            //valida si puede ir vacio
            if (sAValidar.Length == 0 && iMin == 0)
            {
                objExpresionRegular.bError = true;
                objExpresionRegular.sMensaje = sMensaje;
            }
            else {
                //valida el tamaño de la cadena
                if (sAValidar.Length >= iMin && sAValidar.Length <= iMax)
                {
                    //valida que la cadena tenga el formato correcto
                    Regex rgdate = new Regex(sExpresion);
                    if (rgdate.IsMatch(sAValidar))
                    {
                        objExpresionRegular.bError = true;
                        objExpresionRegular.sMensaje = sMensaje;
                    }
                    else
                    {
                        objExpresionRegular.bError = false;
                        objExpresionRegular.sMensaje = "Error con el formato: " + sMensaje;
                    }
                }
                else
                {
                    //si el tamaño de la cadena no corresponde
                    objExpresionRegular.bError = false;
                    objExpresionRegular.sMensaje = "Error por tamaño de la cadena: " + sMensaje;
                }
            }
        }
        catch (Exception ex)
        {
            objExpresionRegular.bError = false;
            objExpresionRegular.sMensaje = "Excepción no controlada: " + ex.Message.ToString();
        }
    }

    /// <summary>
    /// Metodo para validar número flotante
    /// </summary>
    /// <param name="objExpresionRegular"></param>
    /// <param name="sAValidar"></param>
    /// <param name="sMensaje"></param>
    public void fn_ValidaValorFlotante(ExpresionRegular objExpresionRegular, string sAValidar, string sMensaje)
    {
        try
        {
            float.Parse(sAValidar);
            objExpresionRegular.bError = true;
            objExpresionRegular.sMensaje = sMensaje;
        }
        catch (Exception ex)
        {
            objExpresionRegular.bError = false;
            objExpresionRegular.sMensaje = sMensaje;
        }
    }


    /// <summary>
    /// Método que valida el formato de un texto
    /// </summary>
    /// <param name="sAValidar"></param>
    /// <param name="sExpresion"></param>
    /// <returns></returns>
    public void fn_ValidaExpercionRegular(ExpresionRegular objExpresionRegular, string sAValidar, string sExpresion, string sMensaje)
    {
        try
        {
            //valida que la cadena tenga el formato correcto
            Regex rgdate = new Regex(sExpresion);
            if (rgdate.IsMatch(sAValidar))
            {
                objExpresionRegular.bError = true;
                objExpresionRegular.sMensaje = sMensaje;
            }
            else
            {
                objExpresionRegular.bError = false;
                objExpresionRegular.sMensaje = "Error con el formato: " + sMensaje;
            }
        }
        catch (Exception ex)
        {
            objExpresionRegular.bError = false;
            objExpresionRegular.sMensaje = "Excepción no controlada: " + ex.Message.ToString();
        }
    }


}
