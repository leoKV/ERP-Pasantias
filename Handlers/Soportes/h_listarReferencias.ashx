<%@ WebHandler Language="C#" Class="h_listarReferencias" %>

using System;
using System.Web;
using System.Collections.Generic;

public class h_listarReferencias : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        #region Variables
        ///Se obtienen variables generales
        context.Response.ContentType = "text/plain";
        int sEcho = Int32.Parse(context.Request["draw"]);//1
        int sDisplayLength = Int32.Parse(context.Request["length"]);
        int sDisplayStart = Int32.Parse(context.Request["start"]);
        string sVista = context.Request["sVista"];
        string sWhere;
        string sIdUsuario = context.Request["sIdUsuario"]; //cambio

        // Se consultan las aduanas asignadas al usuario.
        Conexion objConexion = new Conexion();

        //string sConsulta = " select 'iIdAduana in (' + SUBSTRING ((SELECT STUFF ((SELECT convert(varchar(max),idAduana) + ',' from tUsuarioComitenteAduana tuca " +
        //                   " where idUsuarioComitente in (select idUsuarioComitente from tUsuarioComitente where idUsuario =" + sIdUsuario + ") " +
        //                   " FOR xml path('')), 1, 0, '')), 1, Len((SELECT STUFF ((SELECT convert(varchar(max),idAduana) + ',' from tUsuarioComitenteAduana tuca " +
        //                   " where idUsuarioComitente in (select idUsuarioComitente from tUsuarioComitente where idUsuario =" + sIdUsuario + ") " +
        //                   " FOR xml path('')), 1, 0, ''))) - 1 ) + ')'";

        //string[] mensaje = objConexion.ejecutarConsultaRegistroSimple(sConsulta);

        //string sAduanas ="";

        // Validar si sitene acceso a la aduana que esta consultando.
        string sConsulta = "select idAduana " +
                    " from tUsuarioComitenteAduana tuca " +
                    " inner join tUsuarioComitente tuc on tuca.idUsuarioComitente = tuc.idUsuarioComitente" +
                    " where idUsuario = " + sIdUsuario +
                    " group by idAduana";
        List<string> lstAduanas = new List<string>();
        lstAduanas = objConexion.ejecutarConsultaRegistroMultiples(sConsulta);
        string sAduanas = "";

        if (lstAduanas != null)
        {
            if (lstAduanas.Count > 0)
            {
                sAduanas = " iIdAduana in (" + String.Join(",", lstAduanas) + ") ";
            }
        }

        try
        {
            sWhere = context.Request["sWhere"].Replace("\0", "");

            if (sAduanas != "")
            {
                if (!String.IsNullOrEmpty(sWhere) && !String.IsNullOrWhiteSpace(sWhere))
                {
                    sAduanas = " and " + sAduanas;
                }
                sWhere = sWhere + sAduanas;
            }
        }
        catch
        {
            sWhere = "";
        }

        string sOrderBy;
        try
        {
            sOrderBy = context.Request["sOrderBy"].Replace("\0", "");
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
            " * FROM ( SELECT ( SELECT COUNT(" + arrColumnas[0] + ") FROM " + sVista + " {1} ) AS TotalDisplayRows, " +
            "(SELECT COUNT(" + arrColumnas[0] + ") FROM " + sVista + " ) AS TotalRows, " +
            " * " +
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
                if (i == arrColumnas.Length - 1)
                {
                    string sDetalle = rdrBrowsers[arrColumnas[10]].ToString();
                    string sIdSubReferenciaCod = sDetalle.Substring(6, sDetalle.Length - 6);
                    string sAgregar = rdrBrowsers[arrColumnas[i]].ToString().Substring(0, 1);

                    if (sAgregar == "1")
                    {
                        json.Append("\"" + arrColumnas[i] + "\":\"" + "<a href='alta_subreferencia.aspx?sIdSubReferencia=" + sIdSubReferenciaCod + "&iType=1'><div class='text-center'><span class='fa fa-plus-circle fa-green-sm'></span></div></a>" + " \"");
                    }
                    else
                    {
                        json.Append("\"" + arrColumnas[i] + "\":\"" + "<div></div>" + " \"");
                    }
                }
                else if (arrColumnas[i] == "sNumReferenciaAdmin")
                {

                    string sColor = rdrBrowsers[arrColumnas[11]].ToString();
                    string sColorPrueba = sColor.Substring(2, sColor.Length - 2);

                    json.Append("\"" + arrColumnas[i] + "\":\"<div class='" + sColor.Substring(2, sColor.Length - 2) + "'>" + rdrBrowsers[arrColumnas[i]].ToString() + "</div>\",");
                }
                //else if (arrColumnas[i] == "sRefNoFacturable")
                //{
                //    string sIdSubReferenciaCod = "SELECT sIdSubReferencia FROM v_ListaReferenciasNADSI WHERE " + rdrBrowsers[arrColumnas[2]] + " = sNumReferenciaAdmin";
                //    cmd.CommandText = sIdSubReferenciaCod;
                //    json.Append("\"" + arrColumnas[i] + "\":\"<div class='text-center'><span class='fa fa-plus fa-green-sm col-lg-6' onclick='fn_RelacionarRefNoFacturable(\"" + sIdSubReferenciaCod + "\" , 1)'></span></div> \",");
                //}
                //else if (arrColumnas[i] == "sCerrar")
                //{
                //    string sIdSubReferencia = "SELECT idSubReferencia FROM tSubReferencia tsubr INNER JOIN v_ListaReferenciasNADSI v ON tsubr.refAdministrativa=v.sNumReferenciaAdmin WHERE (SELECT refAdministrativa FROM tSubReferencia WHERE refAdministrativa = " + rdrBrowsers[arrColumnas[2]] + ") = sNumReferenciaAdmin";
                //    cmd.CommandText = sIdSubReferencia;
                //    json.Append("\"" + arrColumnas[i] + "\":\"<div class='text-center'><span class='fa fa-file-text-o fa-green-sm' onclick='fn_MostrarFacturas(" + sIdSubReferencia + ")'></span></div> \",");
                //}
                //else if (arrColumnas[i] == "sFacturas")
                //{
                //    string sIdSubReferencia = "SELECT idSubReferencia FROM tSubReferencia tsubr INNER JOIN v_ListaReferenciasNADSI v ON tsubr.refAdministrativa=v.sNumReferenciaAdmin WHERE (SELECT refAdministrativa FROM tSubReferencia WHERE refAdministrativa = " + rdrBrowsers[arrColumnas[2]] + ") = sNumReferenciaAdmin";
                //    cmd.CommandText = sIdSubReferencia;
                //    json.Append("\"" + arrColumnas[i] + "\":\"<div class='text-center'><span class='fa fa-times fa-yellow-sm' onclick='fn_ValidadorCerrarReferencia(" + sIdSubReferencia + ")'></span></div> \",");
                //}
                else if (arrColumnas[i] == "sFechaEntrada")
                {
                    string sFechaI;
                    if (rdrBrowsers[arrColumnas[i]].ToString() != null && rdrBrowsers[arrColumnas[i]].ToString().Length > 9)
                    {
                        sFechaI = rdrBrowsers[arrColumnas[i]].ToString().Substring(0, 10);
                    }
                    else
                    {
                        sFechaI = "00/00/0000";
                    }

                    json.Append("\"" + arrColumnas[i] + "\":\"" + "<div>" + sFechaI + "</div>" + " \",");
                }
                //else if (arrColumnas[i] == "sGuiaMaster")
                //{

                //    string sIcono = rdrBrowsers[arrColumnas[i]].ToString();
                //    if (sIcono == "glyphicon glyphicon-map-marker fa-green-sm col-lg-6")
                //    {
                //                string sDetalle = rdrBrowsers[arrColumnas[11]].ToString();
                //                string sIdSubReferenciaCod = sDetalle.Substring(6, sDetalle.Length - 6);

                //               Security secIdSubreferencia;
                //                secIdSubreferencia = new Security(sIdSubReferenciaCod);
                //                //Se pasan los parametros
                //                string sIdSubreferencia = secIdSubreferencia.DesEncriptar();

                //                json.Append("\"" + arrColumnas[i] + "\":\"" + "<div class='text-center'><span class='" + sIcono + "' onclick='fn_MostrarGuias( " + sIdSubreferencia + " )'  ></span></div></a>" + " \",");
                //       // json.Append("\"" + arrColumnas[i] + "\":\"" + "<div>1</div>" + " \",");
                //    }
                //    else
                //    {
                //        json.Append("\"" + arrColumnas[i] + "\":\"" + "<div>" + rdrBrowsers[arrColumnas[i]] + "</div>" + " \",");
                //    }

                //}
                else if (arrColumnas[i] == "sDetalleServiciosEditar")
                {
                    string sDetalle = rdrBrowsers[arrColumnas[i]].ToString();
                    string sDetalle2 = sDetalle.Substring(6, sDetalle.Length - 6);
                    string sIdSubReferencia;
                    //if (sDetalle2.Length > 10)
                    //{
                    //    sIdSubReferencia = sDetalle2.Substring(11, sDetalle2.Length - 11);
                    //}
                    //else
                    //{
                    sIdSubReferencia = sDetalle2;
                    //}

                    string sIdSubReferenciaCod = sDetalle.Substring(6, sDetalle.Length - 6);
                    switch (sDetalle.Substring(0, 5))
                    {
                        case "1,1,1":

                            json.Append("\"" + arrColumnas[i] + "\":\"<div class='text-center'>");
                            json.Append("<a href='detalle_referencia.aspx?sIdSubReferencia=" + sIdSubReferenciaCod + "&iType=1'><div class=''text-center''><span class='fa fa-eye fa-green-sm col-lg-4'></span></div></a>");
                            json.Append("<span class='fa fa-cogs fa-blue-sm col-lg-4' onclick='javascript:fn_ObtenerRelacionServicios(&quot " + sIdSubReferencia + "&quot );'></span>");
                            json.Append("</div><a href='alta_referencia.aspx?sIdSubReferencia=" + sIdSubReferenciaCod + "&iType=1'><div class='text-center'><span class='fa fa-pencil fa-green-sm col-lg-4'></span></div></a> \",");
                            break;
                        case "1,0,1":

                            json.Append("\"" + arrColumnas[i] + "\":\"<div class='text-center'>");
                            json.Append("<a href='detalle_referencia.aspx?sIdSubReferencia=" + sIdSubReferenciaCod + "&iType=1'><div class=''text-center''><span class='fa fa-eye fa-green-sm col-lg-4'></span></div></a>");
                            json.Append("<span class='fa fa-cogs fa-yellow-sm col-lg-4' onclick='fn_ObtenerRelacionServicios( " + sIdSubReferencia + " )'></span>");
                            json.Append("</div><a href='alta_referencia.aspx?sIdSubReferencia=" + sIdSubReferenciaCod + "&iType=1'><div class='text-center'><span class='fa fa-pencil fa-green-sm col-lg-4'></span></div></a> \",");
                            break;
                        case "0,1,1":

                            json.Append("\"" + arrColumnas[i] + "\":\"<div class='text-center'>");
                            json.Append("<span class='fa fa-eye fa-yellow-sm col-lg-4' onclick='fn_Advertencia(&quot" + sIdSubReferenciaCod + "&quot , 1)'></span>");
                            json.Append("<span class='fa fa-cogs fa-blue-sm col-lg-4' onclick='javascript:fn_ObtenerRelacionServicios(&quot " + sIdSubReferencia + "&quot);'></span>");
                            json.Append("</div><a href='alta_referencia.aspx?sIdSubReferencia=" + sIdSubReferenciaCod + "&iType=1'><div class='text-center'><span class='fa fa-pencil fa-green-sm col-lg-4'></span></div></a> \",");
                            break;
                        case "0,0,1":

                            json.Append("\"" + arrColumnas[i] + "\":\"<div class='text-center'>");
                            json.Append("<span class='fa fa-eye fa-yellow-sm col-lg-4' onclick='fn_Advertencia(&quot" + sIdSubReferenciaCod + "&quot , 1)'></span>");
                            json.Append("<span class='fa fa-cogs fa-yellow-sm col-lg-4' onclick='fn_ObtenerRelacionServicios( " + sIdSubReferencia + " )'></span>");
                            json.Append("</div><a href='alta_referencia.aspx?sIdSubReferencia=" + sIdSubReferenciaCod + "&iType=1'><div class='text-center'><span class='fa fa-pencil fa-green-sm col-lg-4'></span></div></a> \",");
                            break;
                        case "1,1,0":

                            json.Append("\"" + arrColumnas[i] + "\":\"<div class='text-center'>");
                            json.Append("<a href='detalle_referencia.aspx?sIdSubReferencia=" + sIdSubReferenciaCod + "&iType=1'><div class='text-center'><span class='fa fa-eye fa-green-sm col-lg-4'></span></div></a>");
                            json.Append("<span class='fa fa-cogs fa-blue-sm col-lg-4' onclick='javascript:fn_ObtenerRelacionServicios(&quot " + sIdSubReferencia + "&quot);'></span>");
                            json.Append("</div><a><div class='text-center'><span class='fa fa-pencil fa-yellow-sm col-lg-4'></span></div></a> \",");
                            break;
                        case "1,0,0":

                            json.Append("\"" + arrColumnas[i] + "\":\"<div class='text-center'>");
                            json.Append("<a href='detalle_referencia.aspx?sIdSubReferencia=" + sIdSubReferenciaCod + "&iType=1'><div class='text-center'><span class='fa fa-eye fa-green-sm col-lg-4'></span></div></a>");
                            json.Append("<span class='fa fa-cogs fa-yellow-sm col-lg-4' onclick='fn_ObtenerRelacionServicios(" + sIdSubReferencia + ")'></span>");
                            json.Append("</div><a><div class='text-center'><span class='fa fa-pencil fa-yellow-sm col-lg-4'></span></div></a> \",");
                            break;
                        case "0,1,0":

                            json.Append("\"" + arrColumnas[i] + "\":\"<div class='text-center'>");
                            json.Append("<span class='fa fa-eye fa-yellow-sm col-lg-4' onclick='javascript:fn_Advertencia(&quot " + sIdSubReferenciaCod + "&quot , 1)'></span>");
                            json.Append("<span class='fa fa-cogs fa-blue-sm col-lg-4' onclick='javascript:fn_ObtenerRelacionServicios(&quot " + sIdSubReferencia + "&quot);'></span>");
                            json.Append("</div><a><div class='text-center'><span class='fa fa-pencil fa-yellow-sm col-lg-4'></span></div></a> \",");
                            break;
                        case "0,0,0":

                            json.Append("\"" + arrColumnas[i] + "\":\"<div class='text-center'>");
                            json.Append("<span class='fa fa-eye fa-yellow-sm col-lg-4' onclick='fn_Advertencia(&quot" + sIdSubReferenciaCod + "&quot , 1)'></span>");
                            json.Append("<span class='fa fa-cogs fa-yellow-sm col-lg-4' onclick='fn_ObtenerRelacionServicios(" + sIdSubReferencia + ")'></span>");
                            json.Append("</div><a><div class='text-center'><span class='fa fa-pencil fa-yellow-sm col-lg-4'></span></div></a> \",");
                            break;
                    }
                }
                else if (arrColumnas[i] == "sAgregarSubReferencia")
                {
                    string sIdSubReferenciaCod = "SELECT sIdSubReferencia FROM v_ListaReferenciasNADSI WHERE " + rdrBrowsers[arrColumnas[2]] + " = sNumReferenciaAdmin";
                    cmd.CommandText = sIdSubReferenciaCod;
                    string sAgregar = rdrBrowsers[arrColumnas[i]].ToString();

                    if (sAgregar == "1")
                    {
                        json.Append("\"" + arrColumnas[i] + "\":\"");
                        json.Append("a href='alta_subreferencia.aspx?sIdSubReferencia='" + sIdSubReferenciaCod + "'&iType=1'><div class='text-center'><span class='fa fa-plus-circle fa-green-sm'></span></div></a>");
                    }
                }
                else
                {
                    json.Append("\"" + arrColumnas[i] + "\":\"" + "<div>" + rdrBrowsers[arrColumnas[i]] + "</div>" + " \",");
                }
            }
            //*********************************************************FIN CAMBIOS*************************************************************            
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