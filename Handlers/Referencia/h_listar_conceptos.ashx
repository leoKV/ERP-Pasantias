<%@ WebHandler Language="C#" Class="h_listar_conceptos" %>

using System;
using System.Web;

public class h_listar_conceptos : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
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
        string sIdFactura;
        string sWhere;
        int iTipo;
        try
        {
            sWhere = context.Request["sWhere"].Replace("\0", "");
            sIdFactura = context.Request["sIdFactura"].Replace("\0", "");
        }
        catch
        {
            sWhere = "";
            sIdFactura = "";
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
        try
        {
            iTipo = int.Parse(context.Request["sTipo"]);
        }
        catch
        {
            iTipo = 0;
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
            " * FROM ( SELECT ( SELECT COUNT(*) FROM " + sVista + " {1} ) AS TotalDisplayRows, " +
            "(SELECT COUNT(*) FROM " + sVista + " ) AS TotalRows, " +
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
        // se instancia clase de conexion
        Conexion oConexion = new Conexion();
        Security objSecurity = new Security(context.Session["iIdUsuario"].ToString());
        string sIdUsuario = objSecurity.DesEncriptar();
        System.Data.DataTable dataOpciones = new System.Data.DataTable();
        string sProvServicioConcepto = "";
        string sConjuntoServicio;
        string[] arrServicios;
        string[] arrValores;
        //string sConsulta;
        string sDesServicio;
        string sIdServicio;
        string sValidaServicio = "";
        string sServicioA;
        //string sValidadorProvAduana;
        string sSeleccionar;
        string sIdFactServ;
        string sConsultaSer = "";
        System.Collections.Generic.List<Servicio> lServicio = new System.Collections.Generic.List<Servicio>();
        // se obtienen servicios
        if (iTipo > 3) {
            sConsultaSer = "select cs.idServicio iIdServicio, cs.cveSunServicio + ' - ' + replace(cs.descripcion,'\"','&quot') sDescripcion FROM cServicio cs where cveSunServicio like 'PRG%' and idTipoServicio is not null " +
                           "AND idServicio in (select idServicio from tServComitenteCuentaContable where idComitente in (select idComitente from tUsuarioComitente where idUsuario = " + sIdUsuario + "))";
        } else if (iTipo==3)
        {
            //Se agregan solo aquellos configurados para honorarios
            sConsultaSer = "select idServicio iIdServicio, cveSunServicio + ' - ' + cs.descripcion sDescripcion  from cServicio cs where cs.idTipoServicio=4";
        }
        else {
            sConsultaSer = "select idServicio iIdServicio, cveSunServicio + ' - ' + cs.descripcion sDescripcion  from cServicio cs where cveSunServicio like 'CGT%'";
        }
        String[] sAtributosSer = { "iIdServicio", "sDescripcion" };
        oConexion.ejecutaRecuperaObjetoLista(sConsultaSer, sAtributosSer, lServicio);

        // se traen arreglo con los servicios

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

                // SE CONSTRUYE EL SELECT 
                if (arrColumnas[i] == "sServicio")
                {
                    //Se obtienen los datos a trabajar
                    sConjuntoServicio = rdrBrowsers[arrColumnas[i]].ToString();
                    string sCadena = "";
                    arrValores = sConjuntoServicio.Split('|');
                    sProvServicioConcepto = arrValores[0];
                    sIdServicio = arrValores[1];
                    sSeleccionar = arrValores[2];
                    sValidaServicio = arrValores[3];
                    sIdFactServ = arrValores[5];
                    // se recorren la lista de servicios para construir combo
                    foreach (Servicio oServicio in lServicio)
                    {

                        if (oServicio.iIdServicio == int.Parse(sIdServicio))
                        {
                            // = arrValores[4];

                            if (sSeleccionar == "1")
                            {
                                sSeleccionar = "selected= 'true'";
                            }
                            else
                            {
                                sSeleccionar = "";
                            }

                            sCadena += "<option value='" + oServicio.iIdServicio + "'" + sSeleccionar + " >" + oServicio.sDescripcion + "</option>";
                        }
                        else
                        {
                            sCadena += "<option value='" + oServicio.iIdServicio + "' >" + oServicio.sDescripcion + "</option>";
                        }
                    }
                    // se crea el select
                    json.Append("\"" + arrColumnas[i] + "\":\"" + "<div class=''form-group text-center''>");
                    json.Append("<select ");
                    if (sValidaServicio == "1")
                    {
                        if (sIdUsuario != "1")
                            json.Append(" disabled ");
                    }
                    json.Append(" id='hslcServicio-" + sProvServicioConcepto + "'");
                    json.Append(" data-width='100%' data-idS='hselect' data-live-search='true' title='' class='selectpicker' dataInf=" + sIdFactServ+"' ");
                    json.Append("onchange='javascript:fn_AsignarServicio(" + sProvServicioConcepto + ","+sIdFactServ+")'>");
                    json.Append(" <option value='' aria-selected='false'></option> ");
                    json.Append(sCadena);
                    json.Append("</select><div>");
                    json.Append("</div>" + " \",");


                }
                else if (i == arrColumnas.Length - 1)
                {
                    json.Append("\"" + arrColumnas[i] + "\":\"" + "<div>" + rdrBrowsers[arrColumnas[i]].ToString() + "</div>" + " \"");
                }
                else
                {
                    json.Append("\"" + arrColumnas[i] + "\":\"" + "<div>" + rdrBrowsers[arrColumnas[i]] + "</div>" + " \",");
                }
                // COMIENZA CREACION DE CADA CAMPO DE LA TABLA
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