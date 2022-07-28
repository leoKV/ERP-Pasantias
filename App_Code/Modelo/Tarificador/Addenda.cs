using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;



/// <summary>
/// Descripción breve de Addenda
/// </summary>
public class Addenda
{
    //variables
    public int iResultado { get; set; }
    public int iResultadoInsertNotaCredito { get; set; }
    public string sMensaje { get; set; }
    public string sContenido { get; set; }
    public string sNomCampo { get; set; }
    public string sValores { get; set; }
    public string sMostrar { get; set; }
    public string sTipo { get; set; }
    public string folioOrdenVenta { get; set; }
    public int iLongitud { get; set; }
    public int iRecuperaComo { get; set; }
    public int iNivel { get; set; }
    public string sEtiqueta { get; set; }
    public int iIdCampoAddenda { get; set; }
    public int iIdCliente { set; get; }

    public string sColumnas { get; set; }
    public string sDatos { get; set; }
    public string[] sDatosArray { get; set; }
    public int iTotalCampos { set; get; }
    public DataTable dt_Addendas;
    public DataTable dt_Resultado;
    public object objGrupos;

    public List<string> lst_datos { get; set; }
    public List<Addenda> lstAddendas { get; set; }
    public List<Addenda> lstAddendasAddMas { get; set; }
    public List<string> lst_datos1 { get; set; }
    public List<string> lst_datos_columnas { get; set; }
    public string valores { get; set; }
    public string[] sPosiciones { get; set; }
    public string[] arrDatos { get; set; }

    #region Atributos de la addenda para cada cliente
    public string c1 { get; set; }
    public string Grupo { get; set; }
    public string c2 { get; set; }
    public string tipo_mercancia { get; set; }
    public string numped { get; set; }
    public string clave { get; set; }
    public string tipop { get; set; }
    public string fechapago { get; set; }
    public string valor_aduana { get; set; }
    public string rate { get; set; }
    public string importe { get; set; }
    public string limporte { get; set; }
    public string concepto { get; set; }
    public string total { get; set; }
    public string folio { get; set; }
    public string serie { get; set; }
    public string rfc { get; set; }
    public string uuid { get; set; }
    public string fechafactura { get; set; }
    public string division { get; set; }
    public string tipodocumentovwm { get; set; }
    public string tipodocumentofiscal { get; set; }
    public string referenciaproveedor { get; set; }
    public string montolinea { get; set; }
    public string monto { get; set; }
    public string cantidadmaterial { get; set; }
    public string preciounitario { get; set; }
    public string descripcionmaterial { get; set; }
    public string refprov { get; set; }
    public string valfactcomercial { get; set; }
    public string faccomercial { get; set; }
    public string pnumero { get; set; }
    public string pedyy { get; set; }
    public string pedaduana { get; set; }
    public string pedpat { get; set; }
    public string pedfolio { get; set; }
    public string importeva { get; set; }
    public string importegc { get; set; }
    public string importeimppg { get; set; }
    public string importeimpaf { get; set; }
    public string saldoto { get; set; }
    public string tsubtotal { get; set; }
    public string ttotal { get; set; }
    public string gtnumero { get; set; }
    public string terconcepto { get; set; }
    public string terimporte { get; set; }
    public string teriva { get; set; }
    public string terret { get; set; }
    public string tertotal { get; set; }
    public string terrs { get; set; }
    public string terrfc { get; set; }
    public string terclnt { get; set; }
    public string terrfcclnt { get; set; }
    public string ternumfac { get; set; }
    public string terffiscal { get; set; }
    public string terfechafac { get; set; }
    public string tipomoneda { get; set; }
    public string numpedimento { get; set; }
    public string ternumref { get; set; }
    public string currencyisocode { get; set; }
    public string lineitemtype { get; set; }
    public string lineitemnumber { get; set; }
    public string valoraduana { get; set; }
    public string longtext { get; set; }
    public string invoicedquantity { get; set; }
    public string gpamount { get; set; }
    public string npamount { get; set; }
    public string liaireferenceidentification { get; set; }
    public string gaamount { get; set; }
    public string naamount { get; set; }
    public string taamount { get; set; }
    public string baamount { get; set; }
    public string payaamount { get; set; }
    public string mabeuuid { get; set; }
    public string fechapago3ro { get; set; }
    public string descripcionconcepto { get; set; }
    public string line { get; set; }
    public string importetotcon { get; set; }
    public string impuestototcon { get; set; }
    public string totaltotcon { get; set; }
    public string tasarettotcon { get; set; }
    public string retenciontotcon { get; set; }
    public string tasaimptotcon { get; set; }
    public string nomproveedor { get; set; }
    public string rfcproveedor { get; set; }
    public string tftotal { get; set; }
    public string nopedimento { get; set; }
    public string tipooperacion { get; set; }
    public string iva { get; set; }
    public string retencioniva { get; set; }
    public string totalcuentagastos { get; set; }
    public string referenciatrafico { get; set; }
    public string tipocuenta { get; set; }
    public string cimportetotal { get; set; }
    public string civa { get; set; }
    public string aduana { get; set; }
    public string patente { get; set; }
    public string pedimento { get; set; }
    public string honorarios { get; set; }
    public string anticipos { get; set; }
    public string nofactura { get; set; }
    public string fadatos { get; set; }
    public string fatipo { get; set; }
    public string pdescripcion { get; set; }
    public string tipomercancia { get; set; }
    public string cantidad { get; set; }
    public string facimportada { get; set; }
    public string numcove { get; set; }
    #endregion

    //-----
    int fil { get; set; }
    int col { get; set; }
    //-----
    public DataTable dt_datosR_Temp { get; set; }
    public int tamFil { get; set; }
    public int tamCol { get; set; }
    public List<int> lst_PosicionR_BD { get; set; }
    public List<Addenda> lstCamposCliente { get; set; }
    public string[] arrDatosCliente { get; set; }
    public List<string> lst_datosConsultas { get; set; }
    public int[] sArrayPosiciones { get; set; }
    public int idOrdenVenta { get; set; }

    public Addenda()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /// <summary>
    /// Método para generar los campos dependiendo del cliente que se seleccione
    /// </summary>
    /// <param name="obj_Addenda"></param>
    public void fn_generarCampos(Addenda obj_Addenda)
    {
        this.iTotalCampos = 0;
        try
        {
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++ CAMPOS EN COMÚN ++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            Conexion obj_conexion = new Conexion();
            obj_Addenda.lstAddendas = new List<Addenda>();
            obj_Addenda.lstAddendasAddMas = new List<Addenda>();
            obj_Addenda.lstCamposCliente = new List<Addenda>();
            List<string> lstEtiquetas = new List<string>();
            string valor = "";
            System.Reflection.PropertyInfo obj_property;
            sColumnas = "";
            string sCampo = "";
            string[] arrDatos = { "iIdCampoAddenda", "iIdCliente", "sNomCampo", "sValores", "sTipo", "iLongitud", "sMostrar", "iRecuperaComo", "sEtiqueta" };
            string sQuery = "select idCliente from tFolioTransitorio where idFolioTransitorio=(select idFolioTransitorio from tOrdenVenta where idOrdenVenta=" +
                            obj_Addenda.idOrdenVenta + ")";
            obj_Addenda.iIdCliente = int.Parse(obj_conexion.ejecutarConsultaRegistroSimple(sQuery)[1]);
            #region Genera Campos
            //                          1               2             3               4             5             6           7                 8
            //query para consulta **** posicion  |   idCliente  |  campo  |   opciones/null    |   clase   |  mostrar(S/N) |  recuperaComo  | nivel


            //Obtiene todos los campos que no se repiten
            sQuery = "select tcaic.idCampoAddendaION iIdCampoAddenda, tc.idCliente iIdCliente, ccai.campoAddendaION sNomCampo,opcionCampoAddenda sValores, " +
                            "case when CHARINDEX('_',validacion)=0 then validacion else SUBSTRING(validacion,0,CHARINDEX('_',validacion)) end sTipo, " +
                            "case when CHARINDEX('_',validacion)=0 then 500 else SUBSTRING(validacion,CHARINDEX('_',validacion)+1,LEN(validacion)) end iLongitud, " +
                            "tcaic.mostrar sMostrar , tcaic.recuperaComo iRecuperaComo,tcaic.etiqueta sEtiqueta from tCliente tc	inner join tCamposAddendaIONClientes tcaic on tcaic.idCliente=tc.idCliente	 " +
                            "inner join cCamposAddendaION ccai on ccai.idCampoAddendaION=tcaic.idCampoAddendaION " +// pasar idcliente        /////no es necesario recuperar el numero 6 ya que esos valores son calculados por ION
                            " where tc.idCliente =" + obj_Addenda.iIdCliente + " and tcaic.recuperaComo!=6 and etiqueta is null order by idCamposAddendaIONClientes";
            //ejecuta consulta para traer los campos 
            obj_conexion.ejecutaRecuperaObjetoLista(sQuery, arrDatos, lstAddendas);

            //Obtiene todos los campos que se repiten, agrupados por etiqueta
            sQuery = "select tcaic.idCampoAddendaION iIdCampoAddenda, tc.idCliente iIdCliente, ccai.campoAddendaION sNomCampo,opcionCampoAddenda sValores, " +
                            "case when CHARINDEX('_',validacion)=0 then validacion else SUBSTRING(validacion,0,CHARINDEX('_',validacion)) end sTipo, " +
                            "case when CHARINDEX('_',validacion)=0 then 500 else SUBSTRING(validacion,CHARINDEX('_',validacion)+1,LEN(validacion)) end iLongitud, " +
                            "tcaic.mostrar sMostrar , tcaic.recuperaComo iRecuperaComo,tcaic.etiqueta sEtiqueta from tCliente tc	inner join tCamposAddendaIONClientes tcaic on tcaic.idCliente=tc.idCliente	 " +
                            "inner join cCamposAddendaION ccai on ccai.idCampoAddendaION=tcaic.idCampoAddendaION " +// pasar idcliente        /////no es necesario recuperar el numero 6 ya que esos valores son calculados por ION
                            " where tc.idCliente =" + obj_Addenda.iIdCliente + " and tcaic.recuperaComo!=6 and etiqueta is not null order by idCamposAddendaIONClientes";

            obj_conexion.ejecutaRecuperaObjetoLista(sQuery, arrDatos, lstAddendasAddMas);

            sQuery = "select lower(campos) from tCamposAddendaCliente where idCliente = " + obj_Addenda.iIdCliente;
            string sRes = obj_conexion.ejecutarConsultaRegistroSimple(sQuery)[1];
            arrDatosCliente = sRes.Split(',');

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++ RECUPERA LOS CAMPOS ESPECÍFICOS DEL CLIENTE ++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            string sQueryCampos = "EXEC pa_ObtenerCampos_BD_Addenda " + obj_Addenda.iIdCliente + "," + obj_Addenda.idOrdenVenta;//cambiar orden de venta(Pasar el id de la orden de venta, e idcliente)
            //ejecuta consulta para traer los campos 
            //obj_Addenda.lst_datosConsultas = obj_conexion.ejecutarConsultaRegistroMultiples(sQueryCampos);
            //fn_obtenerPosR_BD(lst_datos, lst_datosConsultas);

            obj_conexion.ejecutaRecuperaObjeto(sQueryCampos, arrDatosCliente, obj_Addenda);

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++ NO TENGO NI LA MÍNIMA IDEA DE QUÉ SEA ESTO ++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            //obtiene el tamaño de la matriz en la que se guardaran datos temporalmente
            //fn_obtenerTamanioArray(obj_Addenda);
            //obtiene las posiciones de los valores que pueden ser repetidos mas de una vez para asi insertarlos temporalmente en una matriz
            //fn_obtenerPosR_BD(obj_Addenda);


            //declaracion de variables
            string sTag = "";
            string sTag1 = "";
            string sContent = "";
            int contador = 0;
            int con = 0;

            //////variables para datos que se traen de BD
            string[,] sArray;
            int icont = 0;
            sArray = new string[this.tamFil, this.tamCol];
            int y = 0;

            int iBandera1;//datos obtenidos de tabla tCamposAddendaIONCliente y editable, que no se repiten
            int iBandera2;//datos obtenidos de tabla tCamposAddendaIONCliente o de pa_ObtenerRefAdministrativa que se repiten

            //verifica que se haya hecho bien la consulta
            if (lstAddendas.Count > 0)
            {


                // cliclo para iterar sobre toda la lista de datos obtenidos de las tablas tCamposAddendaIONClientes y cCamposAddendaION
                for (int i = 0; i < lstAddendas.Count; i++)
                {
                    sCampo = lstAddendas[i].sNomCampo.ToLower();
                    int iNivel = lstAddendas[i].iNivel;
                    //variables 
                    string sOpcRecuperados = "";
                    string sComboBoxOption = "";
                    string sTextBloqueado = "";
                    string[] sOpciones;
                    string sComboBox = "";
                    string sText = "";

                    //Se recupera los datos por:
                    //1--fijo--obtiene un valor fijo desde opcionCampoAddenda de la tabla tCamposAddendaIONClientes--generar input no editable 
                    //2--fijo - editable --obtiene un valor fijo desde opcionCampoAddenda de la tabla tCamposAddendaIONClientes, pero da opcion para ser editado por el usuario  --generar input editable y pasarle un valor
                    //3--editable  ---no se obtienen valores, son editables los campos para el usuario--- generar input editable y sin pasarle ningun valor
                    //4--opciones --obtiene valores de opciones desde opcionCampoAddenda de la tabla tCamposAddendaIONClientes--generar combobox
                    //5--BD ---son obtenidos desde el pa_ObtenerCampos_BD_Addenda de Base de Datos--y puede generar combo box,input editable con valor,no editable con valor, o input editable sin valor 
                    //6--son calculados por ION, no es necesario mostrar al usuario

                    //banderas para saber en donde se encuentra, para no generar espacios en el formulario
                    iBandera1 = 0;//cuando cambie la posicion a 1, podra gurdar el valor en un string 
                    iBandera2 = 0;//cuando cambie la posicion a 1, podra gurdar el valor en arreglo y posteriormente iterar sobre el

                    //verifica que los campos no se deban obtener de base de datos
                    if (lstAddendas[i].iRecuperaComo != 6 && lstAddendas[i].iRecuperaComo != 5)
                    {
                        //validar que todo vaya con informacion
                        if (lstAddendas[i].sValores != null)
                        {
                            iBandera1 = 1;
                            //quitar el ultimo "|" si es que tiene  
                            sOpcRecuperados = lstAddendas[i].sValores.TrimEnd('|');
                            //separar la cadena de opciones e inserta en arreglo sOpciones
                            sOpciones = sOpcRecuperados.Split('|');
                            //es combobox
                            if (sOpciones.Length != 1)
                            {
                                sComboBox = fn_generaCombo(sOpciones, lstAddendas[i].sNomCampo, lstAddendas[i].iIdCampoAddenda, 0);
                            }
                            else
                            {
                                //genera el campo de texto bloqueado, con un valor fijo
                                sTextBloqueado = fn_generaTextBox("bloqueado", lstAddendas[i].iLongitud, sOpciones[0], lstAddendas[i].sNomCampo,
                                                                  lstAddendas[i].iIdCampoAddenda, lstAddendas[i].iRecuperaComo, 0);
                            }
                        }
                        else
                        {
                            iBandera1 = 1;
                            //genera el campo de texto
                            sText = fn_generaTextBox(lstAddendas[i].sTipo, lstAddendas[i].iLongitud, "", lstAddendas[i].sNomCampo,
                                                     lstAddendas[i].iIdCampoAddenda, lstAddendas[i].iRecuperaComo, 0);

                        }
                    }//genera los campos obtenidos de base de datos
                    else if (lstAddendas[i].iRecuperaComo == 5)
                    {
                        con++;
                        obj_property = obj_Addenda.GetType().GetProperty(sCampo);
                        valor = (string)(obj_property.GetValue(obj_Addenda, null));
                        if (valor == null)
                        {
                            valor = "";
                        }
                        valor = valor.TrimEnd('|');// quitar ultimo '|' de la cadena
                        if (valor.Contains("|"))// si tiene mas de un elemento, puede generar combobox
                        {
                            //quitar el ultimo "|" si es que tiene  
                            sOpcRecuperados = valor.TrimEnd('|');
                            //separar la cadena de opciones e inserta en arreglo sOpciones
                            sOpciones = sOpcRecuperados.Split('|');
                            sComboBox = fn_generaCombo(sOpciones, lstAddendas[i].sNomCampo, lstAddendas[i].iIdCampoAddenda, 0);
                            iBandera1 = 1;
                        }
                        else
                        {

                            //genera el campo de texto bloqueado, con un valor fijo
                            sTextBloqueado = fn_generaTextBox("bloqueado", lstAddendas[i].iLongitud, valor, lstAddendas[i].sNomCampo, lstAddendas[i].iIdCampoAddenda,
                                                lstAddendas[i].iRecuperaComo, 0);
                            iBandera1 = 1;

                        }

                    }//genera los campos obtenidos de base de datos , por el procedimiento almacenado, y tambien obtenidos de la consulta princial(solamente los datos que se repiten)
                    else if (lstAddendas[i].iNivel >= 1)
                    {
                        con++;
                        obj_property = obj_Addenda.GetType().GetProperty(sCampo);
                        valor = (string)(obj_property.GetValue(obj_Addenda, null));
                        if (valor == null)
                        {
                            valor = "";
                        }
                        valor = valor.TrimEnd('|');// quitar ultimo '|' de la cadena
                        icont++;
                        int r = 0;

                        //verifica para cuando se repiten, pero no son obtenidos de base de datos
                        if (lstAddendas[i].iRecuperaComo != 5)
                        {
                            //verifica para cuando requiere de opciones
                            if (lstAddendas[i].iRecuperaComo == 4)
                            {
                                string[] lstOpciones;
                                //quitar el ultimo '|' a la cadena de texto
                                lstAddendas[i].sValores = lstAddendas[i].sValores.TrimEnd('|');
                                //generar lista con diversos valores , 
                                lstOpciones = lstAddendas[i].sValores.Split('|');
                                string sOpcc = "";
                                //guarda el combo en el array
                                sComboBox =//genera el combobox
                                    fn_generaCombo(lstOpciones, lstAddendas[i].sNomCampo, lstAddendas[i].iIdCampoAddenda, 0);
                                iBandera1 = 1;
                            }//verifica para cuando obtiene datos, y si son fijos o fijos editables
                            else if (lstAddendas[i].iRecuperaComo == 1 || lstAddendas[i].iRecuperaComo == 2)
                            {
                                //quitar el ultimo '|' a la cadena de texto
                                lstAddendas[i].sValores = lstAddendas[i].sValores.TrimEnd('|');
                                //genera el campo de texto bloqueado, con un valor fijo
                                sComboBox = fn_generaTextBox("bloqueado", lstAddendas[i].iLongitud, lstAddendas[i].sValores, lstAddendas[i].sNomCampo,
                                                                        lstAddendas[i].iIdCampoAddenda, lstAddendas[i].iRecuperaComo, 0);
                                iBandera1 = 1;
                            }
                            else//es igual a 3 por lo tanto es editable
                            {
                                //genera el campo de texto
                                sText = fn_generaTextBox(lstAddendas[i].sTipo, lstAddendas[i].iLongitud, lstAddendas[i].sValores,
                                                                        lstAddendas[i].sNomCampo, lstAddendas[i].iIdCampoAddenda, lstAddendas[i].iRecuperaComo,
                                                                        0);
                                iBandera1 = 1;
                            }
                        }
                        else if (lstAddendas[i].iRecuperaComo == 5)//si es obtenido de base de datos y se repite
                        {
                            y++;
                            if (valor != null)
                            {
                                if (valor.Contains('|'))
                                {
                                    string[] lstRows;
                                    valor = valor.TrimEnd('|');
                                    lstRows = valor.Split('|');
                                    //string sOpcc = "";
                                    r++;
                                    string sDato = fn_generaCombo(lstRows, lstAddendas[i].sNomCampo, lstAddendas[i].iIdCampoAddenda, lstAddendas[i].iNivel);

                                    iBandera1 = 1;
                                    sComboBox = sDato;

                                }
                                else
                                {
                                    sText = fn_generaTextBox(lstAddendas[i].sTipo, lstAddendas[i].iLongitud, valor, lstAddendas[i].sNomCampo,
                                                                     lstAddendas[i].iIdCampoAddenda, lstAddendas[i].iRecuperaComo, lstAddendas[i].iNivel);
                                    iBandera1 = 1;
                                }
                            }
                        }
                        //******************************************************************
                    }
                    //da formato en HTML para los campos recuperados por cliente
                    sTag += sText + sComboBox + sTextBloqueado;
                    //aumentar contador
                    contador = (iBandera1 == 1 ? contador + 1 : contador + 0);
                    if (contador == 4)
                    {
                        sTag1 += "<div class='row'>" +
                                    sTag +
                                 "</div>";
                        sTag = "";
                        contador = 0;
                    }
                }
                sTag1 += "<div class='row'>" +
                                    sTag +
                                 "</div>";
                sTag = "";

            }
            for (int i = 0; i < lstAddendasAddMas.Count; i++)
            {
                if (!lstEtiquetas.Contains(lstAddendasAddMas[i].sEtiqueta))
                {
                    lstEtiquetas.Add(lstAddendasAddMas[i].sEtiqueta);
                }
            }

            string sDiv = "";
            string sDivFinal = "";
            int iContador = 0;

            //Ciclo que itera los grupos de campos y los agrupa en un div
            for (int i = 0; i < lstEtiquetas.Count; i++)
            {
                //banderas para saber en donde se encuentra, para no generar espacios en el formulario
                iBandera1 = 0;//cuando cambie la posicion a 1, podra gurdar el valor en un string 
                iBandera2 = 0;//cuando cambie la posicion a 1, podra gurdar el valor en arreglo y posteriormente iterar sobre el
                string sInfo = "";
                sDiv = "<label id='hlbl" + lstEtiquetas[i] + "' class='hidden grupo'></label><div class='row panel panel-default'> <div class='panel-heading'>" +
                        "<label class='form-label panel-title' !important;'=''>" + lstEtiquetas[i] +
                        "<span> <i class='fa fa-plus-circle fa-green-sm' id='hbtn" + lstEtiquetas[i] + "' onclick='fn_agregarMas(this)'></i></span></label>" +
                        "</div><div class='panel-body'>";
                for (int c = 0; c < lstAddendasAddMas.Count; c++)
                {
                    if (lstAddendasAddMas[c].sEtiqueta == lstEtiquetas[i])
                    {
                        sCampo = lstAddendasAddMas[c].sNomCampo.ToLower();
                        //variables 
                        string sOpcRecuperados = "";
                        string sComboBoxOption = "";
                        string sTextBloqueado = "";
                        string[] sOpciones;
                        string sComboBox = "";
                        string sText = "";

                        //Se recupera los datos por:
                        //1--fijo--obtiene un valor fijo desde opcionCampoAddenda de la tabla tCamposAddendaIONClientes--generar input no editable 
                        //2--fijo - editable --obtiene un valor fijo desde opcionCampoAddenda de la tabla tCamposAddendaIONClientes, pero da opcion para ser editado por el usuario  --generar input editable y pasarle un valor
                        //3--editable  ---no se obtienen valores, son editables los campos para el usuario--- generar input editable y sin pasarle ningun valor
                        //4--opciones --obtiene valores de opciones desde opcionCampoAddenda de la tabla tCamposAddendaIONClientes--generar combobox
                        //5--BD ---son obtenidos desde el pa_ObtenerCampos_BD_Addenda de Base de Datos--y puede generar combo box,input editable con valor,no editable con valor, o input editable sin valor 
                        //6--son calculados por ION, no es necesario mostrar al usuario

                        //banderas para saber en donde se encuentra, para no generar espacios en el formulario
                        iBandera1 = 0;//cuando cambie la posicion a 1, podra gurdar el valor en un string 
                        iBandera2 = 0;//cuando cambie la posicion a 1, podra gurdar el valor en arreglo y posteriormente iterar sobre el

                        //verifica que los campos no se deban obtener de base de datos
                        if (lstAddendasAddMas[c].iRecuperaComo != 6 && lstAddendasAddMas[c].iRecuperaComo != 5)
                        {
                            //validar que todo vaya con informacion
                            if (lstAddendasAddMas[c].sValores != null)
                            {
                                iBandera1 = 1;
                                //quitar el ultimo "|" si es que tiene  
                                sOpcRecuperados = lstAddendasAddMas[c].sValores.TrimEnd('|');
                                //separar la cadena de opciones e inserta en arreglo sOpciones
                                sOpciones = sOpcRecuperados.Split('|');
                                //es combobox
                                if (sOpciones.Length > 1)
                                {
                                    if (iContador == 0)
                                    {
                                        sComboBox = fn_generaCombo(sOpciones, lstAddendasAddMas[c].sNomCampo, lstAddendasAddMas[c].iIdCampoAddenda, 1);
                                        iContador++;
                                    }
                                    else
                                    {
                                        sComboBox = fn_generaCombo(sOpciones, lstAddendasAddMas[c].sNomCampo, lstAddendasAddMas[c].iIdCampoAddenda, 2);
                                    }
                                }
                                else if (sOpciones.Length == 1)
                                {
                                    //genera el campo de texto bloqueado, con un valor fijo
                                    sTextBloqueado = fn_generaTextBox("bloqueado", lstAddendasAddMas[c].iLongitud, sOpciones[0], lstAddendasAddMas[c].sNomCampo,
                                                                      lstAddendasAddMas[c].iIdCampoAddenda, lstAddendasAddMas[c].iRecuperaComo, 1);
                                }
                            }
                            else
                            {
                                iBandera1 = 1;
                                //genera el campo de texto
                                sText = fn_generaTextBox(lstAddendasAddMas[c].sTipo, lstAddendasAddMas[c].iLongitud, "", lstAddendasAddMas[c].sNomCampo,
                                                         lstAddendasAddMas[c].iIdCampoAddenda, lstAddendasAddMas[c].iRecuperaComo, 1);

                            }
                        }//genera los campos obtenidos de base de datos
                        else if (lstAddendasAddMas[c].iRecuperaComo == 5)
                        {
                            con++;
                            obj_property = obj_Addenda.GetType().GetProperty(sCampo);
                            valor = (string)(obj_property.GetValue(obj_Addenda, null));
                            if (valor == null)
                            {
                                valor = "";
                            }
                            valor = valor.TrimEnd('|');// quitar ultimo '|' de la cadena
                            if (valor.Contains("|"))// si tiene mas de un elemento, puede generar combobox
                            {
                                //quitar el ultimo "|" si es que tiene  
                                sOpcRecuperados = valor.TrimEnd('|');
                                //separar la cadena de opciones e inserta en arreglo sOpciones
                                sOpciones = sOpcRecuperados.Split('|');
                                if (iContador == 0)
                                {
                                    sComboBox = fn_generaCombo(sOpciones, lstAddendasAddMas[c].sNomCampo, lstAddendasAddMas[c].iIdCampoAddenda, 1);
                                    iContador++;
                                }
                                else
                                {
                                    sComboBox = fn_generaCombo(sOpciones, lstAddendasAddMas[c].sNomCampo, lstAddendasAddMas[c].iIdCampoAddenda, 2);
                                }
                                iBandera1 = 1;
                            }
                            else //if (valor != "")
                            {

                                //genera el campo de texto bloqueado, con un valor fijo
                                sTextBloqueado = fn_generaTextBox("bloqueado", lstAddendasAddMas[c].iLongitud, valor, lstAddendasAddMas[c].sNomCampo, lstAddendasAddMas[c].iIdCampoAddenda,
                                                    lstAddendasAddMas[c].iRecuperaComo, 1);
                                iBandera1 = 1;

                            }

                        }//genera los campos obtenidos de base de datos , por el procedimiento almacenado, y tambien obtenidos de la consulta princial(solamente los datos que se repiten)
                        else //if (lstAddendasAddMas[c].iNivel >= 0)
                        {
                            con++;
                            obj_property = obj_Addenda.GetType().GetProperty(sCampo);
                            valor = (string)(obj_property.GetValue(obj_Addenda, null));
                            if (valor == null)
                            {
                                valor = "";
                            }
                            valor = valor.TrimEnd('|');// quitar ultimo '|' de la cadena
                            icont++;
                            int r = 0;

                            //verifica para cuando se repiten, pero no son obtenidos de base de datos
                            if (lstAddendasAddMas[c].iRecuperaComo != 5)
                            {
                                //verifica para cuando requiere de opciones
                                if (lstAddendasAddMas[c].iRecuperaComo == 4)
                                {
                                    string[] lstOpciones;
                                    //quitar el ultimo '|' a la cadena de texto
                                    lstAddendasAddMas[c].sValores = lstAddendasAddMas[c].sValores.TrimEnd('|');
                                    //generar lista con diversos valores , 
                                    lstOpciones = lstAddendasAddMas[c].sValores.Split('|');
                                    string sOpcc = "";
                                    if (lstOpciones.Length > 0)
                                    {
                                        if (iContador == 0)
                                        {
                                            //guarda el combo en el array
                                            sComboBox =//genera el combobox
                                                fn_generaCombo(lstOpciones, lstAddendasAddMas[c].sNomCampo, lstAddendasAddMas[c].iIdCampoAddenda, 1);
                                            iContador++;
                                        }
                                        else
                                        {
                                            //guarda el combo en el array
                                            sComboBox =//genera el combobox
                                                fn_generaCombo(lstOpciones, lstAddendasAddMas[c].sNomCampo, lstAddendasAddMas[c].iIdCampoAddenda, 2);
                                        }
                                    }
                                    iBandera1 = 1;
                                }//verifica para cuando obtiene datos, y si son fijos o fijos editables
                                else if (lstAddendasAddMas[c].iRecuperaComo == 1 || lstAddendasAddMas[c].iRecuperaComo == 2)
                                {
                                    //quitar el ultimo '|' a la cadena de texto
                                    lstAddendasAddMas[c].sValores = lstAddendasAddMas[c].sValores.TrimEnd('|');
                                    if (lstAddendasAddMas[c].sValores != "")
                                    {
                                        //genera el campo de texto bloqueado, con un valor fijo
                                        sComboBox = fn_generaTextBox("bloqueado", lstAddendasAddMas[c].iLongitud, lstAddendasAddMas[c].sValores, lstAddendasAddMas[c].sNomCampo,
                                                                                lstAddendasAddMas[c].iIdCampoAddenda, lstAddendasAddMas[c].iRecuperaComo, 1);
                                    }
                                    iBandera1 = 1;
                                }
                                else//es igual a 3 por lo tanto es editable
                                {
                                    if (lstAddendasAddMas[c].sValores != "")
                                    {
                                        //genera el campo de texto
                                        sText = fn_generaTextBox(lstAddendasAddMas[c].sTipo, lstAddendasAddMas[c].iLongitud, lstAddendasAddMas[c].sValores,
                                                                                lstAddendasAddMas[c].sNomCampo, lstAddendasAddMas[c].iIdCampoAddenda, lstAddendasAddMas[c].iRecuperaComo,
                                                                                1);
                                    }
                                    iBandera1 = 1;
                                }
                            }
                            else if (lstAddendasAddMas[c].iRecuperaComo == 5)//si es obtenido de base de datos y se repite
                            {
                                y++;
                                if (valor != null)
                                {
                                    if (valor.Contains('|'))
                                    {
                                        string[] lstRows;
                                        valor = valor.TrimEnd('|');
                                        lstRows = valor.Split('|');
                                        //string sOpcc = "";
                                        r++;
                                        string sDato = "";
                                        if (lstRows.Length > 0)
                                        {
                                            if (iContador == 0)
                                            {
                                                sDato = fn_generaCombo(lstRows, lstAddendasAddMas[c].sNomCampo, lstAddendasAddMas[c].iIdCampoAddenda, 1);
                                                iContador++;
                                            }
                                            else
                                            {
                                                sDato = fn_generaCombo(lstRows, lstAddendasAddMas[c].sNomCampo, lstAddendasAddMas[c].iIdCampoAddenda, 2);
                                            }
                                        }

                                        iBandera1 = 1;
                                        sComboBox = sDato;

                                    }
                                    else //if (valor != "")
                                    {
                                        sText = fn_generaTextBox(lstAddendasAddMas[c].sTipo, lstAddendasAddMas[c].iLongitud, valor, lstAddendasAddMas[c].sNomCampo,
                                                                         lstAddendasAddMas[c].iIdCampoAddenda, lstAddendasAddMas[c].iRecuperaComo, 1);
                                        iBandera1 = 1;
                                    }
                                }
                            }
                            //******************************************************************
                        }
                        //da formato en HTML para los campos recuperados por cliente
                        sInfo += sText + sComboBox + sTextBloqueado;
                        ////aumentar contador
                        //contador = (iBandera1 == 1 ? contador + 1 : contador + 0);
                        //if (contador == 4)
                        //{
                        //    sTag1 += "<div class='row'>" +
                        //                sTag +
                        //             "</div>";
                        //    sTag = "";
                        //    contador = 0;
                        //}
                    }
                }
                if (sInfo == "")
                {
                    sDivFinal = "";
                }
                else
                {
                    sDiv += sInfo + "</div></div>";
                    sDivFinal += sDiv;
                }
            }


            //*********************************************************************
            //declaracion de variables
            int co = 0;
            string sTag2 = "";
            string sTag3 = "";
            //ciclo para iterar sobre el array , donde estan los datos que se pueden repetir mas de una vez, en 
            //for (int a = 0; a < this.tamFil; a++)
            //{
            //    for (int e = 0; e < this.tamCol; e++)
            //    {
            //        sTag2 += (sArray[a, e] == null || sArray[a, e] == "" ? "" : sArray[a, e]);
            //        co = (sArray[a, e] == null || sArray[a, e] == "" ? co + 0 : co + 1);
            //        if (co == 4)
            //        {
            //            sTag3 += "<div class='row'>" +
            //                        sTag2 +
            //                     "</div>";
            //            sTag2 = "";
            //            co = 0;
            //        }
            //    }
            //}
            //string sContainerRows = "<div class='container" + (sTag3 != "" || sTag2 != "" ? " div-margin" : "") + "'>" + sTag3 + sTag2 + "</div>";
            //*********************************************************************

            // boton de enviar a ION
            string sButtonEnviar = "<div class='col-lg-3 col-md-3 col-sm-3 col-xs-11 form-group'>" +// style='left: 20px; padding-right: 2px; text-left'
                                        "</br>" +
                                        "<span class='text-right'>" +
                                            "<button type ='button' id='hbtnGenerarTags'  class='btn btn-primary btn-sm input-sm' onclick='javascript:fn_guardarAddenda();'>Enviar a ION</button>" +//
                                        "</span>" +
                                    "</div>";
            //quita la ultima coma de la cadena
            //string sColumnas1 = sColumnas.TrimEnd('|');
            // se le da un formato a las etiquetas y campos que se le mostraran al usuario
            sContent = "<div class='panel panel-body'>" +
                                "<div class='container'>" +
                                    (sTag1 != "" ? "<div class='row'>" + sTag1 + sDivFinal + "</div>" : "<div class='row'>" + sTag + sDivFinal + "</div>") +
                                "</div>" +
                                sButtonEnviar +
                       "</div>";
            //devuelve las columnas de las Addendas, para saber donde insertar
            //this.sColumnas = sColumnas1;
            //Mensaje de exito
            this.iResultado = 1;
            this.sMensaje = "Campos recuperados correctamente";
            this.sContenido = sContent;
            #endregion

        }
        catch (Exception ex)
        {
            //Atrapa excepcion y manda mensaje de excepcion
            obj_Addenda.iResultado = 0;
            obj_Addenda.sMensaje = "Excepción al listar Tags: " + ex.Message;
        }
    }

    /// <summary>
    /// Método para obtener la posicion de los datos obtenidos de la vista en BD (solamente los repetibles)
    /// </summary>
    public void fn_obtenerPosR_BD(Addenda obj_addenda)
    {
        int count = 0;
        obj_addenda.sArrayPosiciones = new int[obj_addenda.tamCol];
        //Addenda obj_Addenda = new Addenda();
        int n = 0;
        for (int i = 1; i < obj_addenda.lstAddendas.Count; i++)
        {
            if (obj_addenda.lstAddendas[i].iRecuperaComo == 5)
            {
                count++;
                if (obj_addenda.lstAddendas[i].iNivel == 1)
                {
                    obj_addenda.sArrayPosiciones[n] = count + 2;
                    n++;
                }
            }
        }
    }

    /// <summary>
    /// Método para obtener el tamaño del arreglo donde se guardaran temporalmente los datos obtenidos de base de datos y que se repiten mas de una vez
    /// </summary>
    /// <param name="obj_Addenda"></param>
    public void fn_obtenerTamanioArray(Addenda obj_addenda)
    {
        // Nuevo datatable temporal 
        DataTable dt_datosR_Temp1 = new DataTable();
        int col = 0;
        int fil = 1;
        //determina el tamaño de las filas que tendra el datatable temporal
        if (lst_datosConsultas.Count > 1)
        {
            fil = (obj_addenda.c1 == "0" ? 1 : (obj_addenda.c1 == "1" ? 1 : (obj_addenda.c1 != "1" ? int.Parse(obj_addenda.c1) : 1)));
        }
        else
        {
            fil = 1;
        }

        //List<string> lst_nameDatosR;
        for (int i = 0; i < obj_addenda.lstAddendas.Count; i++)
        {
            //verifica solamente los datos que se repiten y con ellos se forma el datatable
            if (obj_addenda.lstAddendas[i].iNivel == 1)
            {
                //agrega las columnas del datatable
                dt_datosR_Temp1.Columns.Add(obj_addenda.lstAddendas[i].sNomCampo, typeof(string));
                col++;
            }
        }
        //Establece los datos de la datatable, y del numero de filas que tendra el datatable       
        obj_addenda.dt_datosR_Temp = dt_datosR_Temp1;
        this.tamFil = fil;
        this.tamCol = (col == 0 ? 1 : col);
    }

    /// <summary>
    /// Método para guardar las Addendas en tAddenda_ION 
    /// </summary>
    /// <param name="obj_Addenda"></param>
    public void fn_guardarAddenda(Addenda obj_Addenda, int iIdUsuario)
    {
        try
        {
            Utilerias objUtilerias = new Utilerias();
            obj_Addenda.dt_Addendas = new DataTable();
            #region Se define el arreglo de columnas que tendrá el datatable para pasarlo a BD  
            string[] arrColumnas = { "area","bu","identificadorAcreedor","pedido","sucursal","depto","tipoMercancia","numPed","clave","tipOp","fechaPago","pedOrig",
                "valorDolares","valorAduana","fechaCruce","rate","nomProveedor","facImportada","numCove","monto","lImporte","concepto","iIdentificadorContenedor",
                "tipo","oc","total","folio","serie","rfc","uuid","fechaFactura","vVersion","xmlnsPSV","division","tipoDocumentoVWM","tipoDocumentoFiscal",
                "codigoImpuestoMoneda","tipoCambio","tipoMoneda","correoContacto","codigo","referenciaProveedor","nombreSolicitante","correoSolicitante","tipoArchivo",
                "datosArchivo","codigoImpuestoParte","montoLinea","precioUnitario","unidadMedida","cantidadMaterial","descripcionMaterial","posicion","ordenCompra",
                "notaFactura","nombreReceptor","numPedimento","descripcionConcepto","rfcProveedor","cImporte","importeTotCon","impuestoTotCon","tasaImpTotCon",
                "retencionTotCon","tasaRetTotCon","totalTotCon","numProvBic","refProv","regimen","ocComercial","valFactComercial","facComercial","cEmbarq","numCont",
                "peso","pNumero","pedYy","pedAduana","pedPat","pedFolio","importeVa","importeGc","importeImpPg","importeImpAf","saldoTo","hImporte","hTasa","hIva",
                "hTotal","aaSubtotal","aaTasa","aaIva","aaTotal","cNumero","cClave","cTasa","cIva","cTotal","tSubtotal","tTasaIva","tMontIva","tTasaRet","tRet","tTotal",
                "gtNumero","terConcepto","terImporte","terIva","terRet","terTotal","terRs","terRfc","terClnt","terRfcClnt","terNumprov","terNumfac","terFfiscal","terFechafac",
                "terTcambio","terContenedor","terNumref","terCembarque","terNumCont","terPeso","deliveryDate","documentStatus","documentStructureVersion","contentVersion",
                "tType","entityType","uniqueCreatorIdentification","code","sIText","oiReferenceIdentification","oiType","aIReferenceIdentification","aIType","buyerGln",
                "pdnText","sellerGln","sellerAlternatePartyIdentification","sellerType","icGln","icAlternatePartyIdentification","icType","currencyIsoCode","currencyFunction",
                "rateOfChange","netPaymentTermsType","timePeriod","timePeriodDueValue","lineItemType","lineItemNumber","tiiGtin","atiiType","longText","invoicedQuantity",
                "unitOfMeasure","gpAmount","npAmount","liAiReferenceIdentification","liAIType","taxTypeDescription","titaTaxPercentage","taxAmoun","gaAmount","naAmount",
                "taAmount","baAmount","taxType","taxTaxPercentage","taxAmount","taxCategory","payaAmount","noFactura","rfcAgenteAduanal","mabeUuid","fechaPago3ro","line",
                "folioInt","tfTotal","proveedor","comprador","proceso","noPedido","noPedimento","facturaPedimento","xmlEncoding","cuentaGastosConsolidada","cRfc","rfcAgencia",
                "claveCliente","fechaCuentaGastos","tipoOperacion","baseIva","iva","retencionIva","totalCuentaGastos","cancelaA","referenciaTrafico","tipoCuenta","cTipo",
                "cImporteTotal","cRetencionIva","aduana","patente","pedimento","nombreProveedor","claveProveedor","pImporteSinIva","pIva","pComprobados","pRetencionIva",
                "pFolioComprobanteTercero","xmlns","pagosaTerceros","serviciosComplementarios","honorarios","anticipos","importe","cuentaContableConceptos",
                "totalGastosReembolsables","baseCalculoHonorarios","xmlnsS","audiTipodocumentoFiscal","tipoDocumentoAudi","fCodigoImpuesto","numeroProveedor",
                "sCorreoElectronico","fADatos","fATipo","pCodigoImpuesto","pDescripcion","cantidad","numeroOrdenCompra","xsiSchemaLocation","xmlnsFomadd","fomaddGsdb",
                "fomaddAsn" };
            #endregion
            objUtilerias.fn_GeneraDataTable(obj_Addenda.dt_Addendas, arrColumnas);

            //verifica que no esten vacios las variables de cliente y columnas, para hacer una inserccion correcta
            if (obj_Addenda.iIdCliente != 0 && obj_Addenda.iIdCliente != null && obj_Addenda.sColumnas != "")
            {
                //valida que cliente  exista en base de datos
                obj_Addenda.fn_ValidaCliente(obj_Addenda);
                if (obj_Addenda.iResultado == 1)
                {
                    //Carga los datos al datatable para pasarlos como parámetro
                    obj_Addenda.fn_CargaDatosATabla(obj_Addenda);
                    if (obj_Addenda.iResultado == 1)
                    {
                        //Se guarda la addenda
                        obj_Addenda.fn_GuardarAddenda_BD(obj_Addenda, iIdUsuario);
                    }
                }
                else
                {
                    //Mensaje que valida que existan datos
                    obj_Addenda.iResultado = 0;
                    obj_Addenda.sMensaje = "No se a podido guardar Addenda";
                }
            }
            else
            {
                //Mensaje que valida que existan datos
                obj_Addenda.iResultado = 0;
                obj_Addenda.sMensaje = "No se a podido guardar Addenda";
            }
        }
        catch (Exception ex)
        {
            //Atrapa excepcion
            obj_Addenda.iResultado = 0;
            obj_Addenda.sMensaje = "Excepción al guardar Addenda: " + ex.Message;
        }
    }

    /// <summary>
    /// Método para traer todo el listado de los campos
    /// </summary>
    public List<string> fn_traerListadoCampos()
    {
        List<string> lst_col = new List<string>();
        //traer el listado de todos los campos
        Conexion obj_conexion = new Conexion();
        string sQuery_Columns = "select campoAddendaION from cCamposAddendaION order by idCampoAddendaION asc";//listado de campos en orden

        //ejecuta consulta para traer el istado de los campos 
        lst_col = obj_conexion.ejecutarConsultaRegistroMultiples(sQuery_Columns);
        //quita la primera columna de la tabla
        //lst_col.RemoveAt(0);
        //retorna la lista de columnas de la tabla
        return lst_col;
    }

    /// <summary>
    /// Método para traer todo el listado del tipo de dato de los campos
    /// </summary>
    public List<string> fn_traerListadoCamposTipoDato()
    {
        List<string> lst_TipoDato = new List<string>();
        //traer el listado de todos los campos
        Conexion obj_conexion = new Conexion();
        string sQuery = "select tipoDato from cCamposAddendaION order by idCampoAddendaION asc";//listado de campos en orden

        //ejecuta consulta para traer el istado de los campos 
        lst_TipoDato = obj_conexion.ejecutarConsultaRegistroMultiples(sQuery);
        //quita la primera columna de la tabla
        //lst_col.RemoveAt(0);
        //retorna la lista de columnas de la tabla
        return lst_TipoDato;
    }
    /// <summary>
    /// Función que genera un combo a partir de un arreglo de datos
    /// </summary>
    public string fn_generaCombo(string[] arrOpciones, string sNomCampo, int iIdCampo, int iNivel)
    {
        string sComboBoxOption = "";
        string sComboBox = "";
        for (int j = 0; j < arrOpciones.Length; j++)
        {
            sComboBoxOption += "<option value=" + j + ">" + arrOpciones[j] + "</option>";
        }

        if (iNivel >= 1)
        {
            if (iNivel == 1)
            {
                sComboBox =//genera el combobox
                "<div id='" + sNomCampo + "' class='col-md-3 col-sm-3 col-xs-3'>" +
                    "<div class='form-group'>" +
                        "<label for='" + sNomCampo + "'class='form-label'>" + sNomCampo + ":</label>" +
                        "<select class='form-control success-val linked slc' onchange='fn_CambiaValores(this);' id='htxt" + sNomCampo + "' name='cmb" + sNomCampo + "' data-index-number='" + iIdCampo + "'>" +
                            "<option value = '' aria-selected='false'></option>" +
                            sComboBoxOption +
                        "</select>" +
                    "</div>" +
                "</div>";
            }
            else
            {
                sComboBox =//genera el combobox
                "<div id='" + sNomCampo + "' class='col-md-3 col-sm-3 col-xs-3'>" +
                    "<div class='form-group'>" +
                        "<label for='" + sNomCampo + "'class='form-label'>" + sNomCampo + ":</label>" +
                        "<select class='form-control success-val linked slc' id='htxt" + sNomCampo + "' name='cmb" + sNomCampo + "' data-index-number='" + iIdCampo + "'>" +
                            "<option value = '' aria-selected='false'></option>" +
                            sComboBoxOption +
                        "</select>" +
                    "</div>" +
                "</div>";
            }
        }
        else
        {
            sComboBox =//genera el combobox
                "<div id='" + sNomCampo + "' class='col-md-3 col-sm-3 col-xs-3'>" +
                    "<div class='form-group'>" +
                        "<label for='" + sNomCampo + "'class='form-label'>" + sNomCampo + ":</label>" +
                        "<select class='form-control editable success-val' id='htxt" + sNomCampo + "' name='cmb" + sNomCampo + "' data-index-number='" + iIdCampo + "'>" +
                            "<option value = '' aria-selected='false'></option>" +
                            sComboBoxOption +
                        "</select>" +
                    "</div>" +
                "</div>";
        }

        return sComboBox;
    }

    public string fn_generaTextBox(string sTipo, int iLongitud, string sValor, string sNombre, int iIdCampo, int iRecuperado, int iNivel)
    {
        string sTextBox = "<div id='" + sNombre + "' class='col-md-3 col-sm-3 col-xs-3'>" +
                          "<div class='form-group'>" +
                          "<label class='form-label' !important;'>" + sNombre + "</label>";

        switch (sTipo)
        {
            case "bloqueado":
                if (iNivel >= 1)
                {
                    if (sValor != "")
                    {
                        sTextBox += "<input id = 'htxt" + sNombre + "' onblur='fn_validaLongitud(this)' minlength='1' maxlength='" + iLongitud + "' name='htxt" + sNombre + "' class='form-control input-sm success-val NB ' value='" + sValor + "' data-index-number='" + iIdCampo + "' " + (iRecuperado == 1 ? "disabled" : "") + " />";
                    }
                    else
                    {
                        sTextBox += "<input id = 'htxt" + sNombre + "' onblur='fn_validaLongitud(this)' minlength='1' maxlength='" + iLongitud + "' name='htxt" + sNombre + "' class='form-control input-sm success-val ' value='" + sValor + "' data-index-number='" + iIdCampo + "' " + (iRecuperado == 1 ? "disabled" : "") + " />";
                    }
                }
                else
                {
                    sTextBox += "<input id = 'htxt" + sNombre + "' onblur='fn_validaLongitud(this)' minlength='1' maxlength='" + iLongitud + "' name='htxt" + sNombre + "' class='editable form-control input-sm success-val' value='" + sValor + "' data-index-number='" + iIdCampo + "' " + (iRecuperado == 1 ? "disabled" : "") + " />";
                }
                break;
            case "string":
                if (iNivel >= 1)
                {
                    if (sValor != "")
                    {
                        sTextBox += "<input id = 'htxt" + sNombre + "' onblur='fn_validaLongitud(this)' minlength=" + (iRecuperado == 3 ? "0" : "1") + " maxlength='" + iLongitud + "' value='" + sValor + "' name='htxt" + sNombre + "' class='form-control input-sm NB " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                    }
                    else
                    {
                        sTextBox += "<input id = 'htxt" + sNombre + "' onblur='fn_validaLongitud(this)' minlength=" + (iRecuperado == 3 ? "0" : "1") + " maxlength='" + iLongitud + "' value='" + sValor + "' name='htxt" + sNombre + "' class='form-control input-sm  " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                    }
                }
                else
                {
                    sTextBox += "<input id = 'htxt" + sNombre + "' onblur='fn_validaLongitud(this)' minlength=" + (iRecuperado == 3 ? "0" : "1") + " maxlength='" + iLongitud + "' value='" + sValor + "' name='htxt" + sNombre + "' class='editable form-control input-sm  " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                }
                break;
            case "int":
                if (iNivel >= 1)
                {
                    if (sValor != "")
                    {
                        sTextBox += "<input id = 'htxt" + sNombre + "' onblur='fn_validaLongitud(this)' onkeypress = 'return fn_validaNumeros(this)' minlength=" + (iRecuperado == 3 ? "0" : "1") + " maxlength='" + iLongitud + "' value='" + sValor + "' name='htxt" + sNombre + "' class='form-control input-sm NB " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                    }
                    else
                    {
                        sTextBox += "<input id = 'htxt" + sNombre + "' onblur='fn_validaLongitud(this)' onkeypress = 'return fn_validaNumeros(this)' minlength=" + (iRecuperado == 3 ? "0" : "1") + " maxlength='" + iLongitud + "' value='0' name='htxt" + sNombre + "' class='form-control input-sm NB " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                    }
                }
                else
                {
                    if (sValor == "")
                        sValor = "0";
                    sTextBox += "<input type='number' step='any' id ='htxt" + sNombre + "' onkeypress = 'return fn_ValidaDecimal(event,this)' minlength=" + (iRecuperado == 3 ? "0" : "1") + " maxlength='" + iLongitud + "' value='" + sValor + "' name='htxt" + sNombre + "' class='editable form-control input-sm " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                    //if (sValor == "")
                    //{
                    //    sTextBox += "<input type='number' step='any' id ='htxt" + sNombre + "' onkeypress = 'return fn_ValidaDecimal(event,this)' minlength=" + (iRecuperado == 3 ? "0" : "1") + " maxlength='" + iLongitud + "' value='0' name='htxt" + sNombre + "' class='editable form-control input-sm NB " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                    //}
                    //else
                    //{
                    //    sTextBox += "<input type='number' step='any' id ='htxt" + sNombre + "' onkeypress = 'return fn_ValidaDecimal(event,this)' minlength=" + (iRecuperado == 3 ? "0" : "1") + " maxlength='" + iLongitud + "' value='" + sValor + "' name='htxt" + sNombre + "' class='editable form-control input-sm NB " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                    //}
                }
                break;
            case "decimal":
                if (iNivel >= 1)
                {
                    if (sValor != "")
                    {
                        sTextBox += "<input type='number' step='any' id ='htxt" + sNombre + "' onkeypress = 'return fn_ValidaDecimal(event,this)' minlength=" + (iRecuperado == 3 ? "0" : "1") + " maxlength='" + iLongitud + "' value='" + sValor + "' name='htxt" + sNombre + "' class='form-control input-sm NB " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                    }
                    else
                    {
                        sTextBox += "<input type='number' step='any' id ='htxt" + sNombre + "' onkeypress = 'return fn_ValidaDecimal(event,this)' minlength=" + (iRecuperado == 3 ? "0" : "1") + " maxlength='" + iLongitud + "' value='0.0' name='htxt" + sNombre + "' class='form-control input-sm NB " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                    }
                }
                else
                {
                    if (sValor == "")
                        sValor = "0.0";
                    sTextBox += "<input type='number' step='any' id ='htxt" + sNombre + "' onkeypress = 'return fn_ValidaDecimal(event,this)' minlength=" + (iRecuperado == 3 ? "0" : "1") + " maxlength='" + iLongitud + "' value='" + sValor + "' name='htxt" + sNombre + "' class='editable form-control input-sm NB " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                    //if (sValor == "")
                    //{
                    //    sTextBox += "<input type='number' step='any' id ='htxt" + sNombre + "' onkeypress = 'return fn_ValidaDecimal(event,this)' minlength=" + (iRecuperado == 3 ? "0" : "1") + " maxlength='" + iLongitud + "' value='0.0' name='htxt" + sNombre + "' class='editable form-control input-sm NB " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                    //}
                    //else
                    //{
                    //    sTextBox += "<input type='number' step='any' id ='htxt" + sNombre + "' onkeypress = 'return fn_ValidaDecimal(event,this)' minlength=" + (iRecuperado == 3 ? "0" : "1") + " maxlength='" + iLongitud + "' value='" + sValor + "' name='htxt" + sNombre + "' class='editable form-control input-sm NB " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                    //}
                }
                break;
            case "date":
                if (iNivel >= 1)
                {
                    sTextBox += "<input type='date' id = 'htxt" + sNombre + "' onblur='fn_validaLongitud(this)' minlength=" + (iRecuperado == 3 ? "0" : "1") + " min='2010-01-01' value='" + sValor + "' name='htxt" + sNombre + "' class='form-control input-sm " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                }
                else
                {
                    sTextBox += "<input type='date' id = 'htxt" + sNombre + "' onblur='fn_validaLongitud(this)' minlength=" + (iRecuperado == 3 ? "0" : "1") + " min='2010-01-01' value='" + sValor + "' name='htxt" + sNombre + "' class='editable form-control input-sm " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                }
                break;
            case "email":
                if (iNivel >= 1)
                {
                    if (sValor != "")
                    {
                        sTextBox += "<input type='email' onblur='fn_validarEmail(this)' value='" + sValor + "' id = 'htxt" + sNombre + "' minlength=" + (iRecuperado == 3 ? "0" : "8") + " maxlength='" + iLongitud + "' name='htxt" + sNombre + "' class='form-control input-sm NB " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                    }
                    else
                    {
                        sTextBox += "<input type='email' onblur='fn_validarEmail(this)' value='" + sValor + "' id = 'htxt" + sNombre + "' minlength=" + (iRecuperado == 3 ? "0" : "8") + " maxlength='" + iLongitud + "' name='htxt" + sNombre + "' class='form-control input-sm " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                    }
                }
                else
                {
                    sTextBox += "<input type='email' onblur='fn_validarEmail(this)' value='" + sValor + "' id = 'htxt" + sNombre + "' minlength=" + (iRecuperado == 3 ? "0" : "8") + " maxlength='" + iLongitud + "' name='htxt" + sNombre + "' class='editable form-control input-sm " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                }
                break;
            default:
                if (iNivel >= 1)
                {
                    sTextBox += "<input id = 'htxt" + sNombre + "' onblur='fn_validaLongitud(this)' minlength=" + (iRecuperado == 3 ? "0" : "5") + " maxlength='" + iLongitud + "' value='" + sValor + "' name='htxt" + sNombre + "' class='form-control input-sm " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                }
                else
                {
                    sTextBox += "<input id = 'htxt" + sNombre + "' onblur='fn_validaLongitud(this)' minlength=" + (iRecuperado == 3 ? "0" : "5") + " maxlength='" + iLongitud + "' value='" + sValor + "' name='htxt" + sNombre + "' class='editable form-control input-sm " + sTipo + "' data-index-number='" + iIdCampo + "' placeholder='ingresa el " + sNombre + "'>";
                }
                break;
        }
        sTextBox += "</div>" +
                          "</div>";
        return sTextBox;
    }

    /// <summary>
    /// Método para validar que el cliente de Addendas sea correcto
    /// </summary>
    /// <param name="obj_Addenda"></param>
    public void fn_ValidaCliente(Addenda obj_Addenda)
    {
        try
        {
            Conexion objConexion = new Conexion();
            string sQuery = "select tc.idCliente from tCliente tc " +
                "  inner join tCamposAddendaIONClientes tcaic on tcaic.idCliente=tc.idCliente " +
                "  where tcaic.idCliente = " + (obj_Addenda.iIdCliente != 0 && obj_Addenda.iIdCliente != null ? obj_Addenda.iIdCliente : 0) + "";
            string sCliente = objConexion.ejecutarConsultaRegistroSimple(sQuery)[1];
            //valida que exista el cliente

            if (sCliente != "" && sCliente != null)
            {
                //mensaje correcto
                obj_Addenda.iResultado = 1;
                obj_Addenda.sMensaje = " El cliente existe y si cuenta con Addenda";
            }
            else
            {
                //mensaje de error
                obj_Addenda.iResultado = 0;
                obj_Addenda.sMensaje = "El cliente no se encuentra, o no cuenta con Addenda";
            }
        }
        catch (Exception ex)
        {
            //mensaje de error
            obj_Addenda.iResultado = 0;
            obj_Addenda.sMensaje = "Excepción al validar Cliente: " + ex.Message;
        }
    }

    /// <summary>
    ///  Método para guardar en un log cuando la addenda no se guarda correctamente
    /// </summary>
    /// <param name="obj_Addenda"></param>
    /// <param name="sMensajeError"></param>
    public void fn_GuardarLogAddenda(Addenda obj_Addenda, string sMensajeError, int iIdUsuario)
    {
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //inicia query
            //string sQuery = " INSERT INTO tLogAddenda_ION() " +
            //                " VALUES( ) ";
            string squery1 = " insert into tLogAddenda_ION(idUsuario,fecha, observacion) " +
                " values(" + iIdUsuario + ",GETDATE(),'" + (sMensajeError != "" && sMensajeError != null ? sMensajeError : "") + "'); ";
            //ejecuta query
            objConexion.ejecutarComando(squery1);
            //Mensaje de exito de log de Addenda
            obj_Addenda.iResultado = 1;
            obj_Addenda.sMensaje = "Log de Addenda Generado correctamente";
        }
        catch (Exception ex)
        {
            //Atrapa excepcion
            obj_Addenda.iResultado = 0;
            obj_Addenda.sMensaje = "Excepción al guardar Log de Addenda: " + ex.Message.ToString();
        }
    }

    /// <summary>
    /// Metodo para guardar la Addenda en base de datos
    /// </summary>
    /// <param name="objAddenda"></param>
    /// <param name="iIdUsuario"></param>
    /// <returns></returns>
    public string fn_GuardarAddenda_BD(Addenda objAddenda, int iIdUsuario)
    {
        try
        {
            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //Se verifica existencia del SP
            string sRes = objConexion.generarSP("pa_GuardarAddendas", 0);
            string[] sResOut = new string[2];
            if (sRes == "1")
            {
                try
                {
                    //Se pasan los parametros del SP
                    objConexion.agregarParametroSPTabla("@tAddendas", objAddenda.dt_Addendas);
                    objConexion.agregarParametroSP("@idOrdenVenta", SqlDbType.Int, objAddenda.idOrdenVenta.ToString());
                    objConexion.agregarParametroSP("@idUsuario", SqlDbType.Int, iIdUsuario.ToString());
                    //Se ejecuta el SP
                    sResOut = objConexion.ejecutarProcOUTPUT_STRING("@sResOut");
                    if (sResOut[0] == "1")
                    {
                        //Se retorna el mensaje de éxito
                        objAddenda.iResultado = 1;
                        objAddenda.iResultadoInsertNotaCredito = 1;
                        objAddenda.sMensaje = "Addenda guardada con éxito";
                    }
                    else
                    {
                        //Se retorna el mensaje de error
                        objAddenda.iResultado = 0;
                        objAddenda.iResultadoInsertNotaCredito = 0;
                        objAddenda.sMensaje = "Error al guardar la Addenda: " + sResOut[0];
                    }
                    ////objAddenda.dt_Addendas.Columns.Remove("1");
                    ////objAddenda.fil = (objAddenda.fil == 0 ? 1 : objAddenda.fil);
                    ////for (int i=0;i< objAddenda.fil;i++)
                    ////{
                    ////    this.dt_Resultado.Clear();
                    ////    this.dt_Resultado.ImportRow(objAddenda.dt_Addendas.Rows[i+1]);
                    ////    //DataTablenueva
                    ////    //objAddenda.fil;
                    ////    //objAddenda.col;
                    ////    //for

                    ////    // objAddenda.dt_Addendas.Columns["1"].ColumnName = "idRegistroAddenda_ION";
                    ////    //Se pasan los parametros del SP
                    ////    objConexion.agregarParametroSPTabla("@tAddendas", dt_Resultado);
                    ////    //objConexion.agregarParametroSPTabla("@tAddendas", objAddenda.dt_Addendas);
                    ////    objConexion.agregarParametroSP("@idCliente", SqlDbType.Int, objAddenda.iIdCliente.ToString());
                    ////    objConexion.agregarParametroSP("@idUsuario", SqlDbType.Int, iIdUsuario.ToString());
                    ////    //Se ejecuta el SP
                    ////    sResOut = objConexion.ejecutarProcOUTPUT_STRING("@sResOut");
                    ////    if (sResOut[0] == "1")
                    ////    {
                    ////        //Se retorna el mensaje de éxito
                    ////        objAddenda.iResultado = 1;
                    ////        objAddenda.iResultadoInsertNotaCredito = 1;
                    ////        objAddenda.sMensaje = "Addenda guardada con éxito";
                    ////    }
                    ////    else
                    ////    {
                    ////        //Se retorna el mensaje de error
                    ////        objAddenda.iResultado = 0;
                    ////        objAddenda.iResultadoInsertNotaCredito = 0;
                    ////        objAddenda.sMensaje = "Error al guardar la Addenda: " + sResOut[0];
                    ////    }
                    ////}
                }
                catch (Exception ex)
                {
                    //Se guarda el mensaje de error
                    objAddenda.iResultado = 0;
                    objAddenda.sMensaje = "Excepción al guardar la Addenda: " + ex.Message.ToString();
                }
            }
            return sRes;
        }
        catch (Exception ex)
        {
            objAddenda.iResultado = 0;
            return ex.Message.ToString();
        }
    }


    /// <summary>
    /// Metodo para cargar los datos a un datatable
    /// </summary>
    /// <param name="string[,] matriz"></param>
    public void fn_CargaDatosATabla(Addenda obj_Addenda)
    {
        try
        {
            string[] arrInfo;
            string[] arrD;
            int c = 0;
            foreach (string s in obj_Addenda.arrDatos)//Itera todas las listas que existen dentro de la lista padre
            {
                obj_Addenda.dt_Addendas.Rows.Add(obj_Addenda.dt_Addendas.NewRow());
                arrD = s.TrimEnd('/').Split('/');
                for (int i = 0; i < arrD.Length; i++)
                {
                    arrInfo = arrD[i].Split(':');
                    foreach (DataColumn col in obj_Addenda.dt_Addendas.Columns)//Itera la cantidad de columnas que existen en el datatable
                    {
                        if (col.ColumnName.ToLower() == arrInfo[0].ToLower())//Compara que el nombre de la columna coincida con los campos del cliente
                        {
                            obj_Addenda.dt_Addendas.Rows[c][col.ColumnName] = arrInfo[1];//Agrega el valor correspondiente a ese campo
                            break;
                        }
                        //else//Si no son iguales los nombres
                        //{
                        //    if (string.IsNullOrEmpty(obj_Addenda.dt_Addendas.Rows[c][col.ColumnName].ToString())) // Verifica que no exista un valor
                        //    {
                        //        obj_Addenda.dt_Addendas.Rows[i][col.ColumnName] = null;//Inserta un nulo
                        //    }
                        //}
                    }
                }
                c++;
            }


            //    obj_Addenda.dt_Addendas.Rows.Add(obj_Addenda.dt_Addendas.NewRow());//Por cada lista agrega una nueva fila
            //    foreach (string s in lst)//Itera la lista para obtener cada valor
            //    {
            //        arrInfo = s.Split(':');//Divide la información en el nombre del campo y el valor
            //        foreach (DataColumn col in obj_Addenda.dt_Addendas.Columns)//Itera la cantidad de columnas que existen en el datatable
            //        {
            //            if (col.ColumnName == arrInfo[0])//Compara que el nombre de la columna coincida con los campos del cliente
            //            {
            //                obj_Addenda.dt_Addendas.Rows[c][col.ColumnName] = arrInfo[1];//Agrega el valor correspondiente a ese campo
            //            }
            //            else//Si no son iguales los nombres
            //            {
            //                if (string.IsNullOrEmpty(obj_Addenda.dt_Addendas.Rows[c][col.ColumnName].ToString())) // Verifica que no exista un valor
            //                {
            //                    obj_Addenda.dt_Addendas.Rows[c][col.ColumnName] = null;//Inserta un nulo
            //                }
            //            }
            //        }
            //    }
            //    c++;//Incrementa el contador de la fila
            //}
            obj_Addenda.iResultado = 1;
            obj_Addenda.sMensaje = "Tabla Addenda creada correctamente.";
        }
        catch (Exception ex)
        {
            //error no esperado
            this.iResultado = 0;
            this.sMensaje = "Exception al pasar la información de la Addenda a la tabla: " + ex.Message.ToString();
        }
    }

    static List<List<string>> recorrer(List<Addenda> grupos)
    {
        // Al metodo normal se le manda el arreglo de grupos y crea con este el arreglo de strings
        string[] arrString = new string[grupos.Count];
        string val = "";
        // Por cada grupo crea una linea del arrString
        for (int i = 0; i < grupos.Count(); i++)
        {
            foreach (PropertyInfo p in grupos[i].GetType().GetProperties())
            {
                try
                {
                    if (p.GetValue(grupos[i], null) != null)
                    {
                        val = (string)p.GetValue(grupos[i], null);
                    }
                    else
                    {
                        val = "";
                    }
                }
                catch { val = ""; }
                if (val.TrimEnd('|').Contains('|') && val != null && val != "")
                {
                    // Por cada valor contenido en el grupo agrega el indice a la linea del string que va a arrString
                    for (int j = 0; j < val.TrimEnd('|').Split('|').Length; j++)
                    {
                        arrString[i] += j + ",";
                    }
                    // Quita la ultima coma de la linea
                    arrString[i] = arrString[i].TrimEnd(',');
                    break;
                }
                //else if (val != "")
                //{
                //    arrString[i] += i + ",";
                //    arrString[i] = arrString[i].TrimEnd(',');
                //    break;
                //}
            }
        }
        /* Ejecuta el proceso normal y nos hace return de la lista padre
          Esta lista va a contener combinaciones de indices en la forma:
          listaPadre = {
            listaHija = {"0", "1"}, Siendo cada lista hija una linea final y cada valor un indice que corresponde a un objeto
            listaHija = {"0", "2"}, 
          }

        listaHija[k] corresponde a grupos[k] 
        El valor contenido en listaHija[k] corresponde al indice de grupos[k].campo.split('|')[listaHija[k]]
        con la debida conversion a entero
         */

        List<List<string>> lstPadre = new List<List<string>>();
        foreach (string s in arrString)
        {
            if (s.Contains(","))
            {
                if (lstPadre.Count == 0)
                {
                    foreach (string si in s.Split(','))
                    {
                        List<string> lstHija = new List<string>();
                        lstHija.Add(si);
                        lstPadre.Add(lstHija);
                    }
                }
                else
                {
                    List<List<string>> lstAuxiliar = new List<List<string>>();
                    foreach (List<string> lstHija in lstPadre)
                    {
                        foreach (string si in s.Split(','))
                        {
                            List<string> lstNew = new List<string>(lstHija);
                            lstNew.Add(si);
                            lstAuxiliar.Add(lstNew);
                        }
                    }
                    lstPadre = new List<List<string>>(lstAuxiliar);
                }
            }
            else
            {
                if (lstPadre.Count == 0)
                {
                    List<string> lstHija = new List<string>();
                    lstHija.Add(s);
                    lstPadre.Add(lstHija);
                }
                else
                {
                    foreach (List<string> lstHija in lstPadre)
                    {
                        lstHija.Add(s);
                    }
                }
            }
        }

        return lstPadre;
    }

    /// <summary>
    /// Método que guarda las addendas automáticamente
    /// </summary>
    /// <param name="objAddenda"></param>
    /// <param name="iIdUsuario"></param>
    public void fn_enviarAddenda(Addenda objAddenda, int iIdUsuario)
    {
        try
        {
            Utilerias objUtilerias = new Utilerias();
            objAddenda.dt_Addendas = new DataTable();
            #region Se define el arreglo de columnas que tendrá el datatable para pasarlo a BD  
            string[] arrColumnas = { "area","bu","identificadorAcreedor","pedido","sucursal","depto","tipoMercancia","numPed","clave","tipOp","fechaPago","pedOrig",
                "valorDolares","valorAduana","fechaCruce","rate","nomProveedor","facImportada","numCove","monto","lImporte","concepto","iIdentificadorContenedor",
                "tipo","oc","total","folio","serie","rfc","uuid","fechaFactura","vVersion","xmlnsPSV","division","tipoDocumentoVWM","tipoDocumentoFiscal",
                "codigoImpuestoMoneda","tipoCambio","tipoMoneda","correoContacto","codigo","referenciaProveedor","nombreSolicitante","correoSolicitante","tipoArchivo",
                "datosArchivo","codigoImpuestoParte","montoLinea","precioUnitario","unidadMedida","cantidadMaterial","descripcionMaterial","posicion","ordenCompra",
                "notaFactura","nombreReceptor","numPedimento","descripcionConcepto","rfcProveedor","cImporte","importeTotCon","impuestoTotCon","tasaImpTotCon",
                "retencionTotCon","tasaRetTotCon","totalTotCon","numProvBic","refProv","regimen","ocComercial","valFactComercial","facComercial","cEmbarq","numCont",
                "peso","pNumero","pedYy","pedAduana","pedPat","pedFolio","importeVa","importeGc","importeImpPg","importeImpAf","saldoTo","hImporte","hTasa","hIva",
                "hTotal","aaSubtotal","aaTasa","aaIva","aaTotal","cNumero","cClave","cTasa","cIva","cTotal","tSubtotal","tTasaIva","tMontIva","tTasaRet","tRet","tTotal",
                "gtNumero","terConcepto","terImporte","terIva","terRet","terTotal","terRs","terRfc","terClnt","terRfcClnt","terNumprov","terNumfac","terFfiscal","terFechafac",
                "terTcambio","terContenedor","terNumref","terCembarque","terNumCont","terPeso","deliveryDate","documentStatus","documentStructureVersion","contentVersion",
                "tType","entityType","uniqueCreatorIdentification","code","sIText","oiReferenceIdentification","oiType","aIReferenceIdentification","aIType","buyerGln",
                "pdnText","sellerGln","sellerAlternatePartyIdentification","sellerType","icGln","icAlternatePartyIdentification","icType","currencyIsoCode","currencyFunction",
                "rateOfChange","netPaymentTermsType","timePeriod","timePeriodDueValue","lineItemType","lineItemNumber","tiiGtin","atiiType","longText","invoicedQuantity",
                "unitOfMeasure","gpAmount","npAmount","liAiReferenceIdentification","liAIType","taxTypeDescription","titaTaxPercentage","taxAmoun","gaAmount","naAmount",
                "taAmount","baAmount","taxType","taxTaxPercentage","taxAmount","taxCategory","payaAmount","noFactura","rfcAgenteAduanal","mabeUuid","fechaPago3ro","line",
                "folioInt","tfTotal","proveedor","comprador","proceso","noPedido","noPedimento","facturaPedimento","xmlEncoding","cuentaGastosConsolidada","cRfc","rfcAgencia",
                "claveCliente","fechaCuentaGastos","tipoOperacion","baseIva","iva","retencionIva","totalCuentaGastos","cancelaA","referenciaTrafico","tipoCuenta","cTipo",
                "cImporteTotal","cRetencionIva","aduana","patente","pedimento","nombreProveedor","claveProveedor","pImporteSinIva","pIva","pComprobados","pRetencionIva",
                "pFolioComprobanteTercero","xmlns","pagosaTerceros","serviciosComplementarios","honorarios","anticipos","importe","cuentaContableConceptos",
                "totalGastosReembolsables","baseCalculoHonorarios","xmlnsS","audiTipodocumentoFiscal","tipoDocumentoAudi","fCodigoImpuesto","numeroProveedor",
                "sCorreoElectronico","fADatos","fATipo","pCodigoImpuesto","pDescripcion","cantidad","numeroOrdenCompra","xsiSchemaLocation","xmlnsFomadd","fomaddGsdb",
                "fomaddAsn" };
            #endregion
            objUtilerias.fn_GeneraDataTable(objAddenda.dt_Addendas, arrColumnas);
            if (objAddenda.iIdCliente == 734 || objAddenda.iIdCliente == 642)
            {
                objAddenda.dt_Addendas.Merge(objAddenda.dt_Resultado, true, MissingSchemaAction.Ignore);
            }

            //Se instancia la clase conexión 
            Conexion objConexion = new Conexion();
            //Se verifica existencia del SP
            string sRes = objConexion.generarSP("pa_GuardarAddendas", 0);
            string[] sResOut = new string[2];
            if (sRes == "1")
            {
                try
                {
                    //Se pasan los parametros del SP
                    objConexion.agregarParametroSP("@idOrdenVenta", SqlDbType.Int, objAddenda.idOrdenVenta.ToString());
                    objConexion.agregarParametroSP("@idUsuario", SqlDbType.Int, iIdUsuario.ToString());
                    objConexion.agregarParametroSP("@idCliente", SqlDbType.Int, objAddenda.iIdCliente.ToString());
                    if (objAddenda.iIdCliente == 642 || objAddenda.iIdCliente == 734)
                    {
                        objConexion.agregarParametroSPTabla("@tAddendas", objAddenda.dt_Addendas);
                    }
                    else
                    {
                        objConexion.agregarParametroSPTabla("@tAddendas", null);
                    }
                    //Se ejecuta el SP
                    sResOut = objConexion.ejecutarProcOUTPUT_STRING("@sResOut");
                    if (sResOut[0] == "1")
                    {
                        //Se retorna el mensaje de éxito
                        objAddenda.iResultado = 1;
                        objAddenda.iResultadoInsertNotaCredito = 1;
                        objAddenda.sMensaje = "Addenda guardada con éxito";
                    }
                    else
                    {
                        //Se retorna el mensaje de error
                        objAddenda.iResultado = 0;
                        objAddenda.sMensaje = "Error al guardar la Addenda: " + sResOut[0];
                    }
                }
                catch (Exception ex)
                {
                    //Se guarda el mensaje de error
                    objAddenda.iResultado = 0;
                    objAddenda.sMensaje = "Excepción al guardar la Addenda: " + ex.Message.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            objAddenda.iResultado = 0;
            objAddenda.sMensaje = ex.Message.ToString();
        }
    }

    public void fn_obtenerDT(Addenda objAddenda)
    {
        string sQuery = "";
        objAddenda.dt_Resultado = new DataTable();
        Conexion objConexion = new Conexion();
        Utilerias objUtilerias = new Utilerias();
        string[] arrColumnas;

        if (objAddenda.iIdCliente == 642) //CLIENTE AUDI
        {
            #region Se define el arreglo de columnas que tendrá el datatable para pasarlo a BD
            arrColumnas = new string[] { "vVersion", "xmlnsS", "audiTipodocumentoFiscal", "tipoDocumentoAudi", "fCodigoImpuesto", "tipoMoneda", "numeroProveedor",
                            "correoSolicitante", "sCorreoElectronico", "fADatos", "fATipo", "pCodigoImpuesto", "posicion", "pDescripcion", "cantidad", "unidadMedida",
                            "precioUnitario", "montoLinea", "numeroOrdenCompra","code" };
            #endregion

            sQuery = @"SELECT
	                    '1.0' vVersion,
	                    'http://www.audi.net.mx/Addenda/S' xmlnsS,
	                    'FA' audiTipodocumentoFiscal,
	                    '' tipoDocumentoAudi,
	                    'D2' fCodigoImpuesto,
	                    'MXN' tipoMoneda,
	                    '00X7209000' numeroProveedor,
	                    '' correoSolicitante,
	                    'tania.gomez@nadglobal.com' sCorreoElectronico,
	                    (select dbo.fn_EncodeB64(cadenaXML) from tArchivosFactura where idFactura=tf.idfactura) fADatos,
	                    'XML' fATipo,
	                    case when tdov.porcentajeIva=0.16 or tdov.porcentajeIva=0.08 then 'D2' else 'V0' end pCodigoImpuesto,
	                    '' posicion,
	                    cs.descripcion pDescripcion,
	                    tdov.cantidad cantidad,
	                    'SER' unidadMedida,
	                    tdov.importe precioUnitario,
	                    tdov.importe montoLinea,
	                    '' numeroOrdenCompra,
                        tov.folioOrdenVenta code
                    FROM tOrdenVenta tov
                    INNER JOIN tDetalleOrdenVenta tdov on tov.idOrdenVenta=tdov.idOrdenVenta
                    INNER JOIN cServicio cs on cs.idServicio=tdov.idServicio
                    LEFT JOIN tFactura tf on tf.idFactura=tdov.idFactura
                    WHERE tov.idOrdenVenta=" + objAddenda.idOrdenVenta + @" 
                    ORDER BY tdov.idDetalleOrden";
        }
        else //CLIENTE VW
        {

            #region Se define el arreglo de columnas que tendrá el datatable para pasarlo a BD  
            arrColumnas = new string[] {
                                        "vVersion", "xmlnsPSV", "division", "tipoDocumentoVWM", "tipoDocumentoFiscal", "codigoImpuestoMoneda", "tipoCambio",
                                        "tipoMoneda","nomProveedor","correoContacto","codigo","referenciaProveedor","nombreSolicitante","correoSolicitante",
                                        "tipoArchivo","datosArchivo","codigoImpuestoParte","montoLinea","precioUnitario","unidadMedida","cantidadMaterial",
                                        "descripcionMaterial","posicion","ordenCompra","notaFactura","code"};
            #endregion

            sQuery = @"SELECT
                        '1.0' vVersion,
                        'http://www.vwnovedades.com/volkswagen/kanseilab/shcp/2009/Addenda/PSV' xmlnsPSV,
                        'VW' division,
                        'PSV' tipoDocumentoVWM,
                        'FA' tipoDocumentoFiscal,
                        case when tdov.porcentajeIva=0.16 then '1A' else case when tdov.porcentajeIva = 0.00 then 'V0' else '' end end codigoImpuestoMoneda,
                        tov.tipoCambio tipoCambio,
                        CASE WHEN cveMoneda = 'MXN' THEN 'MXP' ELSE cveMoneda END tipoMoneda,
                        ISNULL((select nomproveedor from tProveedor where idProveedor=(select idProveedor from tFactura where idFactura=tdov.idFactura)),'') nomProveedor,
                        CASE WHEN aduana IN (SELECT ca.denominacion FROM cAduana ca WHERE idAduana IN (14,72,73,74,75,25,118,90,84,86)) 
		                    THEN 'bianca.motta@nadglobal.com;teresa.ricardez@nadglobal.com'
		                    WHEN aduana IN (SELECT ca.denominacion FROM cAduana ca WHERE idAduana IN (9,10,11,12))
		                    THEN 'alejandra.almanza@nadglobal.com;teresa.vilchis@nadglobal.com'
		                    ELSE 'jessica.meza@nadglobal.com' END correoContacto,
                        '6001013662' codigo,
                        (SELECT tsr.refOperativa FROM tSubReferencia tsr WHERE idSubReferencia=(
                        (SELECT MIN(idSubReferencia) FROM tFolioTransitorioSubReferencia WHERE idFolioTransitorio=tov.idFolioTransitorio))) referenciaProveedor,
                        'x' nombreSolicitante,
                        'x' correoSolicitante,
                        'XML' tipoArchivo,
                        (SELECT dbo.fn_EncodeB64(cadenaXML) from tArchivosFactura where idFactura=tdov.idFactura) datosArchivo,
                        case when tdov.porcentajeIva=0.16 then '1A' else case when tdov.porcentajeIva = 0.00 then 'V0' else '' end end codigoImpuestoParte,
                        tdov.importe montoLinea,
	                    tdov.importe/tdov.cantidad precioUnitario,
                        'EA' unidadMedida,
                        tdov.cantidad cantidadMaterial,
                        (select descripcion from cServicio where idServicio=tdov.idServicio) descripcionMaterial,
                        'x' posicion,
                        'x' ordenCompra,
                        '' notaFactura,
                        tov.folioOrdenVenta code
                        FROM tOrdenVenta tov
                        INNER JOIN tDetalleOrdenVenta tdov ON tdov.idOrdenVenta=tov.idOrdenVenta
                        WHERE tov.idOrdenVenta=" + objAddenda.idOrdenVenta;
        }

        objUtilerias.fn_GeneraDataTable(objAddenda.dt_Resultado, arrColumnas);
        objAddenda.dt_Resultado = objConexion.ejecutarConsultaRegistroMultiplesData(sQuery);
    }
    public void fn_ordenaArreglos(Addenda objAddenda)
    {
        // LLamamos al recorrido modificado
        List<List<string>> recorrido = recorrer(objAddenda.lstAddendas);
        string[] lineas = new string[recorrido.Count];
        string val = "";
        string[] arrTest;
        for (int i = 0; i < recorrido.Count(); i++) // Itera por cada linea del recorrido
        {
            for (int j = 0; j < objAddenda.lstAddendas.Count; j++) // Itesra por cada grupo
            {

                foreach (PropertyInfo p in objAddenda.lstAddendas[j].GetType().GetProperties())
                {
                    if (p.GetValue(objAddenda.lstAddendas[j], null) != null)
                    {
                        try { val = (string)p.GetValue(objAddenda.lstAddendas[j], null); } catch { }
                    }
                    else
                    {
                        val = "";
                    }
                    if (val.Contains('|'))
                    {
                        // Accede al objeto y añade los valores a las lineas finales
                        arrTest = val.Split('|');
                        lineas[i] += p.Name + ":" + arrTest[int.Parse(recorrido[i][j])] + "/";
                    }
                }
            }
            foreach (string s in objAddenda.arrDatos)
            {
                lineas[i] += s + "/";
            }
        }


        objAddenda.arrDatos = lineas;


        //try
        //{
        //    string[] arrInfo;
        //    int c = 0;
        //    foreach (Addenda a in objAddenda.lstAddendas)//Itera todas las listas que existen dentro de la lista padre
        //    {
        //        objAddenda.dt_Addendas.Rows.Add(objAddenda.dt_Addendas.NewRow());//Por cada lista agrega una nueva fila
        //        foreach (Addenda s in lst)//Itera la lista para obtener cada valor
        //        {
        //            arrInfo = s.Split(':');//Divide la información en el nombre del campo y el valor
        //            foreach (DataColumn col in objAddenda.dt_Addendas.Columns)//Itera la cantidad de columnas que existen en el datatable
        //            {
        //                if (col.ColumnName == arrInfo[0])//Compara que el nombre de la columna coincida con los campos del cliente
        //                {
        //                    objAddenda.dt_Addendas.Rows[c][col.ColumnName] = arrInfo[1];//Agrega el valor correspondiente a ese campo
        //                }
        //                else//Si no son iguales los nombres
        //                {
        //                    if (string.IsNullOrEmpty(objAddenda.dt_Addendas.Rows[c][col.ColumnName].ToString())) // Verifica que no exista un valor
        //                    {
        //                        objAddenda.dt_Addendas.Rows[c][col.ColumnName] = null;//Inserta un nulo
        //                    }
        //                }
        //            }
        //        }
        //        c++;//Incrementa el contador de la fila
        //    }
        //    obj_Addenda.iResultado = 1;
        //    obj_Addenda.sMensaje = "Tabla Addenda creada correctamente.";
        //}
        //catch (Exception ex)
        //{
        //    //error no esperado
        //    this.iResultado = 0;
        //    this.sMensaje = "Exception al pasar la información de la Addenda a la tabla: " + ex.Message.ToString();
        //}
    }
}






