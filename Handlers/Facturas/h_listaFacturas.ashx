<%@ WebHandler Language="C#" Class="h_listaFacturas" %>

using System;
using System.Web;

public class h_listaFacturas : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        #region Variables
        ///Se obtienen variables generales
        context.Response.ContentType = "text/plain";
        int sEcho = Int32.Parse(context.Request["draw"]);
        int sDisplayLength = Int32.Parse(context.Request["length"]);
        int sDisplayStart = Int32.Parse(context.Request["start"]);
        string sVista = context.Request["sVista"];



        string sWhere;
        try
        {
            sWhere = context.Request["sWhere"];
            sWhere = String.IsNullOrEmpty(sWhere) ? "" : sWhere.Replace("\0", "");
        }
        catch
        {
            sWhere = "";
        }
        string sOrderBy;
        try
        {
            sOrderBy = context.Request["sOrderBy"];
            sOrderBy = String.IsNullOrEmpty(sOrderBy) ? "" : sOrderBy.Replace("\0", "");
            if (sOrderBy != "")
                sOrderBy = sOrderBy + ",";
        }
        catch
        {
            sOrderBy = "";
        }
        //Se obtiene arreglo de columnas
        string[] sSeparador = { "," };
        string sColumnas = context.Request["sColumnas"];
        string[] arrColumnas = sColumnas.Split(sSeparador, StringSplitOptions.RemoveEmptyEntries);
        string search = context.Request["search[value]"];
        #endregion
        #region Filtro
        /*** FILTRO ***/
        System.Text.StringBuilder where = new System.Text.StringBuilder();
        string sWhereClause = string.Empty;
        for (int i = 0; i < arrColumnas.Length; i++)
        {
            if (where.ToString().Equals(""))
            {
                where.Append(" WHERE ");
            }
            string sText = context.Request["columns[" + i + "][search][value]"];
            if (!sText.Equals(""))
            {
                where.Append("  " + arrColumnas[i] + " like '%" + sText + "%' AND");
            }
            sWhereClause = where.ToString().Substring(0, (where.ToString().Length - 3));
        }
        if (sWhereClause.Length < 5)
            sWhereClause = "";
        if (sWhereClause == "")
        {
            if (sWhere != "")
                sWhereClause += " WHERE " + sWhere;
        }
        else
        {
            if (sWhere != "")
                sWhereClause += " AND " + sWhere;
        }
        #endregion
        #region Ordenar
        /*** ORDENAR ***/
        System.Text.StringBuilder orderBy = new System.Text.StringBuilder();
        string sOrderByClause = string.Empty;
        int iNumOrden = -0;
        //Check which column is to be sorted by in which direction
        for (int i = 0; i < arrColumnas.Length; i++)
        {
            if (context.Request.Params["order[" + i + "][column]"] != null)
            {
                iNumOrden = int.Parse(context.Request.Params["order[" + i + "][column]"].ToString());
                orderBy.Append(context.Request.Params["order[" + i + "][column]"]);
                orderBy.Append(" ");
                orderBy.Append(context.Request.Params["order[" + i + "][dir]"]);
            }
        }
        sOrderByClause = orderBy.ToString();
        //Replace the number corresponding the column position by the corresponding name of the column in the database
        if (!String.IsNullOrEmpty(sOrderByClause))
        {
            for (int i = 0; i < arrColumnas.Length; i++)
            {
                if (i == iNumOrden)
                {
                    sOrderByClause = sOrderByClause.Replace(i.ToString(), ", " + arrColumnas[(i)] + "");
                }

            }
            //Eliminate the first comma of the variable "order"
            sOrderByClause = sOrderByClause.Remove(0, 1);
        }
        sOrderByClause = "ORDER BY " + sOrderBy + sOrderByClause;
        #endregion
        #region Query
        /*** QUERY ***/
        string sQuery = "SELECT * FROM ( SELECT ROW_NUMBER() OVER ({0}) AS RowNumber, " +
            " * FROM ( SELECT ( SELECT COUNT(1) FROM " + sVista + " {1} ) AS TotalDisplayRows, " +
            "(SELECT COUNT(1) FROM " + sVista + " ) AS TotalRows, " +
            "* " +
            "FROM " + sVista + " {1}) RawResults ) " +
            "Results WHERE RowNumber BETWEEN {2} AND {3}";
        string sDisplayLengthMod = sDisplayLength == -1 ? "TotalDisplayRows" : (sDisplayLength + sDisplayStart).ToString();
        sQuery = String.Format(sQuery, sOrderByClause, sWhereClause, sDisplayStart + 1, sDisplayLengthMod);
        //Get result rows from DB
        System.Data.SqlClient.SqlConnection conn = new
        System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DB_NADSIConnectionString"].ConnectionString);
        conn.Open();
        System.Data.SqlClient.SqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = sQuery;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandTimeout = 120;
        System.Data.IDataReader rdrBrowsers = cmd.ExecuteReader();
        #endregion
        #region Json
        /*** JSON ***/
        System.Text.StringBuilder json = new System.Text.StringBuilder();
        string sOutputJson = string.Empty;
        int iTotalDisplayRecords = 0;
        int iTotalRecords = 0;
        while (rdrBrowsers.Read())
        {
            if (iTotalRecords == 0)
                iTotalRecords = Int32.Parse(rdrBrowsers["TotalRows"].ToString());
            if (iTotalDisplayRecords == 0)
                iTotalDisplayRecords = Int32.Parse(rdrBrowsers["TotalDisplayRows"].ToString());
            json.Append("{");
            ///Ciclo para recorrer los registros de las tablas
            ///
            for (int i = 0; i < arrColumnas.Length; i++)
            {
                string sContenidoCelda = "";
                //sUUID,sNoFactura,sCliente,sProveedor,sFechaFactura,sMonto,sReferencia,sEstatus,sFormaPago,sFechaEntrada,sPDF,sNumDiarioCXP,sEnviar
                //sUUID,sNoFactura,sCliente,sProveedor,sFechaFactura,sMonto,sReferencia,sEstatus,sFormaPago,sFechaEntrada,sPDF,sVer,sCancelar,sNumDiarioCXP,sEnviar";
                if (arrColumnas[i] == "sUUID")
                {
                    sContenidoCelda = "<div style='cursor: pointer;' class='" + rdrBrowsers["sColorFactura"] + "'>" + rdrBrowsers["sUUID"] + "</div>";
                }
                if (arrColumnas[i] == "sNoFactura")
                {
                    sContenidoCelda = rdrBrowsers[arrColumnas[i]].ToString();
                }
                if (arrColumnas[i] == "sCliente")
                {
                    sContenidoCelda = rdrBrowsers[arrColumnas[i]].ToString();
                }
                if (arrColumnas[i] == "sProveedor")
                {
                    sContenidoCelda = rdrBrowsers[arrColumnas[i]].ToString();
                }
                if (arrColumnas[i] == "sFechaFactura")
                {
                    sContenidoCelda = rdrBrowsers[arrColumnas[i]].ToString();
                }
                if (arrColumnas[i] == "sMonto")
                {
                    sContenidoCelda = rdrBrowsers[arrColumnas[i]].ToString();
                }
                if (arrColumnas[i] == "sReferencia")
                {
                    sContenidoCelda = "<div class='" + rdrBrowsers["sColorReferencia"] + "'>" + rdrBrowsers["sReferencia"] + "</div>";
                }
                if (arrColumnas[i] == "sEstatus")
                {
                    sContenidoCelda = rdrBrowsers[arrColumnas[i]].ToString();
                }
                if (arrColumnas[i] == "sFormaPago")
                {
                    sContenidoCelda = rdrBrowsers[arrColumnas[i]].ToString();
                }
                if (arrColumnas[i] == "sUsoCFDI")
                {
                    sContenidoCelda = rdrBrowsers[arrColumnas[i]].ToString();
                }
                if (arrColumnas[i] == "sFechaEntrada")
                {
                    sContenidoCelda = rdrBrowsers[arrColumnas[i]].ToString();
                }
                if (arrColumnas[i] == "sPDF")
                {
                    sContenidoCelda = "<div class='text-center'><span class='fa fa-file-pdf-o fa-red-sm' onclick='javascript:fn_ObtenerDatosPDF(" + rdrBrowsers["iIdFactura"] + ")'></span></div>";
                }
                if (arrColumnas[i] == "sVer")
                {
                    sContenidoCelda = "<div class='text-center'><a href='detalle_integracion_factura.aspx?sIdFactura=" + rdrBrowsers["sIdFactura"] + "&sIdSubReferencia=" +
                    rdrBrowsers["sIdSubReferencia"] + "&iType=1'><span class='fa fa-eye fa-green-sm'></span></a></div>";
                    //sContenidoCelda = "<div class='text-center'><div onclick=\\\"javascript: window.open('detalle_integracion_factura.aspx?sIdFactura=" + rdrBrowsers["sIdFactura"] + "&sIdSubReferencia=" +
                    //rdrBrowsers["sIdSubReferencia"] + "&iType=1');\\\"><span class='fa fa-eye fa-green-sm'></span></div></div>";
                }
                if (arrColumnas[i] == "sCancelar")
                {
                    sContenidoCelda = "<div class='text-center'><span class='fa fa-times fa-yellow-sm' onclick=\\\"javascript:fn_ValidaCancelarFactura(" + rdrBrowsers["iIdFactura"] + "," +
                    rdrBrowsers["iIdSubReferencia"] + ", '" + rdrBrowsers["sUUID"] + "')\\\"></span></div>";
                }
                if (arrColumnas[i] == "sNumDiarioCXP")
                {
                    sContenidoCelda = rdrBrowsers["sFolioFDC"].ToString() +"-"+ rdrBrowsers["sNumDiarioCXP"].ToString();
                    if (rdrBrowsers["iIdEstatusFactura"].ToString() == "22")
                    {
                        if (Int32.Parse(rdrBrowsers["iNoFRechazoSS"].ToString()) > 0)
                        {
                            sContenidoCelda += " (RECHAZADA SS)" + rdrBrowsers["sErrorFRechazoSS"];

                        }
                        else
                        {
                            sContenidoCelda += " (AÚN NO SE ENVIADO A SS)";
                        }

                    }
                }
                if (arrColumnas[i] == "sEnviar")
                {

                    if (rdrBrowsers["iIdEstatusFactura"].ToString() == "15" || rdrBrowsers["iIdEstatusFactura"].ToString() == "22")
                    {
                        if (rdrBrowsers["sMoneda"].ToString() == "MXN")
                        {
                            sContenidoCelda = "<div class='text-center'><span class='fa fa-paper-plane fa-green-sm' onclick=\\\"javascript:fn_ValidarMontoCuentaGastos(" +
                                    rdrBrowsers["iIdFactura"] + ",1);fn_ObtenerDatosDetalle('" + rdrBrowsers["sIdFactura"] + "','" +
                                    rdrBrowsers["sIdSubReferencia"] + "',1)\\\"></span></div>";

                        }
                        else
                        {
                            sContenidoCelda = "<div class='text-center'><span class='fa fa-paper-plane fa-green-sm' onclick='javascript:fn_ValidarMontoCuentaGastos(" +
                                    rdrBrowsers["iIdFactura"] + ",2)'></span></div>";
                        }
                    }
                    else
                    {
                        if (rdrBrowsers["iIdEstatusFactura"].ToString() == "6")
                        {
                            sContenidoCelda = "<div class='text-center'><span class='fa fa-paper-plane fa-blue-sm'></span></div>";
                        }
                        else
                        {
                            sContenidoCelda = "";
                        }
                    }
                }
                if (arrColumnas[i] == "iPagar")
                {
                    if (rdrBrowsers["iPagado"].ToString() == "0")
                    {
                        sContenidoCelda = "<div class='text-center'><span class='fa fa-square-o fa-green-sm' onclick='javascript:fn_PagarFacturas(" +
                                rdrBrowsers["iIdFactura"] + ",1)'></span></div>";
                    }
                    else
                    {
                        sContenidoCelda = "<div class='text-center'><span class='fa fa-check-square-o fa-green-sm' onclick='javascript:fn_PagarFacturas(" +
                                rdrBrowsers["iIdFactura"] + ",0)'></span></div>";
                    }
                }

                if (i == arrColumnas.Length - 1)
                {
                    json.Append("\"" + arrColumnas[i] + "\":\"" + "<div>" + sContenidoCelda + "</div>" + " \"");
                }
                else
                {
                    json.Append("\"" + arrColumnas[i] + "\":\"" + "<div>" + sContenidoCelda + "</div>" + " \",");
                }
            }
            json.Append("},");
        }
        sOutputJson = json.ToString();
        if (!sOutputJson.Equals(""))
        {
            sOutputJson = sOutputJson.Remove(sOutputJson.Length - 1);
        }
        System.Text.StringBuilder response = new System.Text.StringBuilder();
        sOutputJson = sOutputJson.Replace(System.Environment.NewLine, "");
        sOutputJson = sOutputJson.Replace("\u0009", "");
        response.Append("{");
        response.Append("\"draw\": ");
        response.Append(sEcho);
        response.Append(",");
        response.Append("\"recordsTotal\": ");
        response.Append(iTotalRecords);
        response.Append(",");
        response.Append("\"recordsFiltered\": ");
        response.Append(iTotalDisplayRecords);
        response.Append(",");
        response.Append("\"data\": [");
        response.Append(sOutputJson);
        response.Append("]}");
        sOutputJson = response.ToString();
        #endregion
        #region Resultado
        /*** RESULTADO ***/
        context.Response.Clear();
        context.Response.ClearHeaders();
        context.Response.ClearContent();
        context.Response.Write(sOutputJson);
        context.Response.Flush();
        context.Response.End();
        #endregion
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}