using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Clase util para formatear valores a tipo moneda
/// </summary>
public class Moneda
{
    private String[] UNIDADES =
    {
        "Cero ",
        "Un ",
        "Dos ",
        "Tres ",
        "Cuatro ",
        "Cinco ",
        "Seis ",
        "Siete ",
        "Ocho ",
        "Nueve "
    };

    private String[] DECENAS =
    {
        "Diez ",
        "Once ",
        "Doce ",
        "Trece ",
        "Cartorce ",
        "Quince ",
        "Dieciseis ",
        "Diecisiete ",
        "Dieciocho ",
        "Diecinueve ",
        "Veinte ",
        "Treinta ",
        "Cuarenta ",
        "Cincuenta ",
        "Sesenta ",
        "Setenta ",
        "Ochenta ",
        "Noventa "
    };

    private String[] CENTENAS =
    {
        "",
        "Ciento ",
        "Doscientos ",
        "Trecientos ",
        "Cuatrocientos ",
        "Quinientos ",
        "Seiscientos ",
        "Setecientos ",
        "Ochocientos ",
        "Novecientos "
    };

    /// <summary>
    /// Dar formato a un valor para que sea moneda
    /// </summary>
    /// <param name="sValor"></param>
    /// <returns></returns>
    public string fn_FormatearMoneda(string sValor)
    {
        return string.Format("{0:###,###,###,##0.00}", Decimal.Parse(sValor));
    }

    /// <summary>
    /// Metodo privado para obtener un texto sin decoración
    /// </summary>
    /// <param name="iNumero"></param>
    /// <returns></returns>
    private string fn_ConvertirATextoUndecor(int iNumero)
    {
        int iParteEntera = iNumero;
        string sParteEntera = "";

        // Se verifica si la parte entera es 0
        if (iParteEntera == 0)
        {
            sParteEntera = "Cero ";
        }
        else if (iParteEntera > 999999) // Si esta en los millones
        {
            sParteEntera = fn_Millones(iParteEntera);
        }
        else if (iParteEntera > 999) // Si esta en los miles
        {
            sParteEntera = fn_Miles(iParteEntera);
        }
        else if (iParteEntera > 99) // Si esta en las centenas
        {
            sParteEntera = fn_Centenas(iParteEntera);
        }
        else if (iParteEntera > 9) // Si esta en las decenas
        {
            sParteEntera = fn_Decenas(iParteEntera);
        }
        else // Unidades
        {
            sParteEntera = fn_Unidades(iParteEntera);
        }

        // Devuelve el resultado
        return sParteEntera;
    }

    /// <summary>
    /// Metodo para obtener una cantidad en texto
    /// </summary>
    /// <param name="dNumero">Numero a convertir</param>
    /// <param name="sMoneda">Moneda a la cual se quiere convertir USD o MXN</param>
    /// <returns></returns>
    public String fn_ConvertirATexto(decimal dNumero, string sMoneda, string sIdioma)
    {
        if (sIdioma == "Ingles")
        {
            return fn_ConvertirATextoIngles(dNumero, sMoneda);
        }

        string sMonedaNombre = "";

        // Obtiene el valor absoluto
        if (dNumero < 0)
            dNumero *= -1;

        // Se convierte el numero en string
        string sNumero = dNumero.ToString();

        // Se cambian las comas por puntos
        sNumero = sNumero.Replace(",", ".");

        // Se separa el numero en entero y decimal
        string sParteEntera = sNumero.Split('.')[0];
        string sParteDecimal = "00";
        if (sNumero.Split('.').Length > 1)
        {
            sParteDecimal = sNumero.Split('.')[1];
            if (sParteDecimal.Length == 1)
                sParteDecimal = "0" + sParteDecimal;
            if (sParteDecimal.Length > 2)
                sParteDecimal = sParteDecimal.Substring(0, 2);
        }

        // convierte la parte entera en numero
        long iParteEntera = long.Parse(sParteEntera);

        // Se verifica si la parte entera es 0
        if (iParteEntera == 0)
        {
            sParteEntera = "Cero ";
        }else if(iParteEntera > 999999) // Si esta en los millones
        {
            sParteEntera = fn_Millones(iParteEntera);
        }else if(iParteEntera > 999) // Si esta en los miles
        {
            sParteEntera = fn_Miles(iParteEntera);
        } else if(iParteEntera > 99) // Si esta en las centenas
        {
            sParteEntera = fn_Centenas(iParteEntera);
        }else if(iParteEntera > 9) // Si esta en las decenas
        {
            sParteEntera = fn_Decenas(iParteEntera);
        }
        else // Unidades
        {
            sParteEntera = fn_Unidades(iParteEntera);
        }

        // Verificar moneda
        if (sMoneda == "USD")
        {
            sMoneda = "US$";
            sMonedaNombre = "DÓLARES ";
        }
        else
        {
            sMoneda = "M.N.";
            sMonedaNombre = "PESOS ";
        }

        // Devuelve el resultado
        return sParteEntera + sMonedaNombre + sParteDecimal + "/100 " + sMoneda;
    }
    
    /// <summary>
    /// Obtener la parte de unidades
    /// </summary>
    /// <param name="iNumero"></param>
    /// <returns></returns>
    private string fn_Unidades(long iNumero)
    {
        return UNIDADES[iNumero];
    }

    /// <summary>
    /// Obtener la parte de decenas
    /// </summary>
    /// <param name="iNumero"></param>
    /// <returns></returns>
    private string fn_Decenas(long iNumero)
    {
        if (iNumero == 0)
            return "";
        // Si el numero es mayor o igual a 20
        if(iNumero >= 20)
        {
            // Si el numero tiene unidades
            if(iNumero % 10 != 0)
            {
                return DECENAS[(int)(iNumero / 10) + 8] + "y " + fn_Unidades(iNumero % 10);
            }
            else
            {
                return DECENAS[(int)(iNumero / 10) + 8];
            }
        }
        else
        {
            if(iNumero >= 10)
            {
                return DECENAS[iNumero - 10];
            }
            else
            {
                return UNIDADES[iNumero];
            }
        }
    }

    /// <summary>
    /// Obtener parte de centenas
    /// </summary>
    /// <param name="iNumero"></param>
    /// <returns></returns>
    private string fn_Centenas(long iNumero)
    {
        // En caso de que sea el numero 100
        if (iNumero == 100) return " Cien ";
        return CENTENAS[(int)(iNumero / 100)] + fn_Decenas(iNumero % 100);
    }

    /// <summary>
    /// Obtener parte de miles
    /// </summary>
    /// <param name="iNumero"></param>
    /// <returns></returns>
    private string fn_Miles(long iNumero)
    {
        return fn_ConvertirATextoUndecor((int)(iNumero / 1000)) + "Mil " + fn_Centenas(iNumero % 1000);
    }

    /// <summary>
    /// Obtener parte de los millones
    /// </summary>
    /// <param name="iNumero"></param>
    /// <returns></returns>
    private string fn_Millones(long iNumero)
    {
        string sSalida = ""; 
        // Si hay mas de un millon
        if(iNumero >= 2000000)
        {
            sSalida = fn_ConvertirATextoUndecor((int)(iNumero / 1000000)) + "Millones ";
        }
        else
        {
            sSalida = "Un millon ";
        }
        return sSalida + fn_Miles(iNumero % 1000000);
    }

    #region CONVERITR MONEDA A TEXTO IDIOMA INGLES

    private String[] UNIDADESINGLES =
    {
        "Zero ",
        "One ",
        "Two ",
        "Theee ",
        "Four ",
        "Five ",
        "Six ",
        "Seven ",
        "Eight ",
        "Nine "
    };

    private String[] DECENASINGLES =
    {
        "Ten ",
        "Eleven ",
        "Twelve ",
        "Thirteen ",
        "Fourteen ",
        "Fifteen ",
        "Sixteen ",
        "Seventeen ",
        "Eighteen ",
        "Nineteen ",
        "Twenty ",
        "Thirty ",
        "Forty ",
        "Fifty ",
        "Sixty ",
        "Seventy ",
        "Eighty ",
        "Ninety "
    };

    private String[] CENTENASINGLES =
    {
        "",
        "Hundred ",
        "Two Hundred ",
        "Three hundred",
        "Four hundred ",
        "Five hundred ",
        "Six hundred",
        "Seven hundred",
        "Eight hundred ",
        "Nine hundred"
    };

    /// <summary>
    /// Metodo para obtener una cantidad en texto
    /// </summary>
    /// <param name="dNumero">Numero a convertir</param>
    /// <param name="sMoneda">Moneda a la cual se quiere convertir USD o MXN</param>
    /// <returns></returns>
    public String fn_ConvertirATextoIngles(decimal dNumero, string sMoneda)
    {
        string sMonedaNombre = "";
        // Obtiene el valor absoluto
        if (dNumero < 0)
            dNumero *= -1;

        // Se convierte el numero en string
        string sNumero = dNumero.ToString();

        // Se cambian las comas por puntos
        sNumero = sNumero.Replace(",", ".");

        // Se separa el numero en entero y decimal
        string sParteEntera = sNumero.Split('.')[0];
        string sParteDecimal = "00";
        if (sNumero.Split('.').Length > 1)
        {
            sParteDecimal = sNumero.Split('.')[1];
            if (sParteDecimal.Length == 1)
                sParteDecimal = "0" + sParteDecimal;
            if (sParteDecimal.Length > 2)
                sParteDecimal = sParteDecimal.Substring(0, 2);
        }

        // convierte la parte entera en numero
        long iParteEntera = long.Parse(sParteEntera);

        // Se verifica si la parte entera es 0
        if (iParteEntera == 0)
        {
            sParteEntera = "Zero ";
        }
        else if (iParteEntera > 999999) // Si esta en los millones
        {
            sParteEntera = fn_MillonesIngles(iParteEntera);
        }
        else if (iParteEntera > 999) // Si esta en los miles
        {
            sParteEntera = fn_MilesIngles(iParteEntera);
        }
        else if (iParteEntera > 99) // Si esta en las centenas
        {
            sParteEntera = fn_CentenasIngles(iParteEntera);
        }
        else if (iParteEntera > 9) // Si esta en las decenas
        {
            sParteEntera = fn_DecenasIngles(iParteEntera);
        }
        else // Unidades
        {
            sParteEntera = fn_UnidadesIngles(iParteEntera);
        }

        // Verificar moneda
        if (sMoneda == "USD")
        {
            sMoneda = "US$";
            sMonedaNombre = "DOLLARS ";
        }
        else
        {
            sMoneda = "M.N.";
            sMonedaNombre = "PESOS ";
        }

        // Devuelve el resultado
        return sParteEntera + sMonedaNombre + sParteDecimal + "/100 " + sMoneda;
    }

    /// <summary>
    /// Obtener la parte de unidades
    /// </summary>
    /// <param name="iNumero"></param>
    /// <returns></returns>
    private string fn_UnidadesIngles(long iNumero)
    {
        return UNIDADESINGLES[iNumero];
    }

    /// <summary>
    /// Obtener la parte de decenas
    /// </summary>
    /// <param name="iNumero"></param>
    /// <returns></returns>
    private string fn_DecenasIngles(long iNumero)
    {
        if (iNumero == 0)
            return "";
        // Si el numero es mayor o igual a 20
        if (iNumero >= 20)
        {
            // Si el numero tiene unidades
            if (iNumero % 10 != 0)
            {//and
                return DECENASINGLES[(int)(iNumero / 10) + 8] + " " + fn_UnidadesIngles(iNumero % 10);
            }
            else
            {
                return DECENASINGLES[(int)(iNumero / 10) + 8];
            }
        }
        else
        {
            if (iNumero >= 10)
            {
                return DECENASINGLES[iNumero - 10];
            }
            else
            {
                return UNIDADESINGLES[iNumero];
            }
        }
    }

    /// <summary>
    /// Obtener parte de centenas
    /// </summary>
    /// <param name="iNumero"></param>
    /// <returns></returns>
    private string fn_CentenasIngles(long iNumero)
    {
        // En caso de que sea el numero 100
        if (iNumero == 100) return " Hundred ";
        return CENTENASINGLES[(int)(iNumero / 100)] + fn_DecenasIngles(iNumero % 100);
    }

    /// <summary>
    /// Obtener parte de miles
    /// </summary>
    /// <param name="iNumero"></param>
    /// <returns></returns>
    private string fn_MilesIngles(long iNumero)
    {
        return fn_ConvertirATextoUndecorIngles((int)(iNumero / 1000)) + "Thousand " + fn_CentenasIngles(iNumero % 1000);
    }

    /// <summary>
    /// Obtener parte de los millones
    /// </summary>
    /// <param name="iNumero"></param>
    /// <returns></returns>
    private string fn_MillonesIngles(long iNumero)
    {
        string sSalida = "";
        // Si hay mas de un millon
        if (iNumero >= 2000000)
        {
            sSalida = fn_ConvertirATextoUndecorIngles((int)(iNumero / 1000000)) + "Millions ";
        }
        else
        {
            sSalida = "One million ";
        }
        return sSalida + fn_MilesIngles(iNumero % 1000000);
    }

    /// <summary>
    /// Metodo privado para obtener un texto sin decoración
    /// </summary>
    /// <param name="iNumero"></param>
    /// <returns></returns>
    private string fn_ConvertirATextoUndecorIngles(int iNumero)
    {
        int iParteEntera = iNumero;
        string sParteEntera = "";

        // Se verifica si la parte entera es 0
        if (iParteEntera == 0)
        {
            sParteEntera = "Zero ";
        }
        else if (iParteEntera > 999999) // Si esta en los millones
        {
            sParteEntera = fn_MillonesIngles(iParteEntera);
        }
        else if (iParteEntera > 999) // Si esta en los miles
        {
            sParteEntera = fn_MilesIngles(iParteEntera);
        }
        else if (iParteEntera > 99) // Si esta en las centenas
        {
            sParteEntera = fn_CentenasIngles(iParteEntera);
        }
        else if (iParteEntera > 9) // Si esta en las decenas
        {
            sParteEntera = fn_DecenasIngles(iParteEntera);
        }
        else // Unidades
        {
            sParteEntera = fn_UnidadesIngles(iParteEntera);
        }

        // Devuelve el resultado
        return sParteEntera;
    }

    #endregion
}