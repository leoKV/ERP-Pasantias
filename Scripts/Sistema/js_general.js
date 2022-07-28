$(document).ready(function () {
    $(".primeraMayuscula").keyup(function () {
        $(this).val($(this).val().capitalize());
    });

    //aplicando esta clase los textos se cambian a mayuscula
    $(".todoMayuscula").keyup(function () {
        $(this).val($(this).val().toUpperCase());
    });

    //aplicando esta clase los textos se cambian a minuscula
    $(".todoMinuscula").keyup(function () {
        $(this).val($(this).val().toLowerCase());
    });

    $("#hbtnCerrarNotificaciones").click(function () {
        $("#dialogNotificaciones").modal("hide");
    });

    //$("#btnNotificaciones").click(function () {
    //    alert('1');
    //});


    

});

var bDibujada = false;


function fn_MostrarNotificacionesGastos() {

    var sColumnas = "sComitente,sProveedor,fMonto,sMoneda,sUsuarioAutoriza,iDia,sPeriodicidad,sRepetirCada,sUsuarioResponsable,sEstatus,sVer";

    var arrDatos = {
        "sVista": "v_ListaGasto", "sColumnas": sColumnas, "sWhere": "idResponsable = " + fn_Decode($("#hlblIdUsuario").html()) + " and GETDATE() >= DATEADD(day,-notificar,fecha) and iTipo = 1"
    }
    fn_LlenarTabla("htblGastosFijosNotificacion", arrDatos, sColumnas);
    $("#dialogNotificaciones").modal("show");
}

function fn_MostrarNotificacionesOrdenes() {

    var sColumnas = "sFolioOrden,sFolioSUN,sFolioTransitorio,sTipoOrden,sMoneda,sCuentaGastos,sOficina,sSolicita,sControles";

    // Verificar si esta codificado o no el idUsuario
    var iIdUsuario = $("#hlblIdUsuario").html();
    if (isNaN(iIdUsuario)) {
        // Decodificar
        iIdUsuario = fn_Decode(iIdUsuario);
    }

    var arrDatos = {
        //"sVista": "v_ListaNotfiOrdenes", "sColumnas": sColumnas, "sWhere": "iIdEncargado = " + iIdUsuario + ""
        "sVista": "v_ListaNotfiOrdenes", "sColumnas": sColumnas, "sWhere": ""
    }
    fn_LlenarTabla("htblOrdenesNotificacion", arrDatos, sColumnas);
    $("#dialogOrdenesNotificaciones").modal("show");
}


function fn_MostrarNotificacionesFacturas() {

    var sColumnas = "sNumFactur,sFechaFactura,sIntegracion,sLineas";

    var arrDatos = {
        "sVista": "v_IntegracionFacturasTereceroLista", "sColumnas": sColumnas, "sOrderBy": "sFecha_ION"
    }

    fn_LlenarTabla("htblIntegracionFacturasTerceros", arrDatos, sColumnas);
    $("#dialogFacturasIntegrandoTerceros").modal("show");
}

function fn_MostrarNotificacionesOrdenesPasando() {

    var sColumnas = "sFolioOrdenVenta,sFechaOrdenVenta,sIntegracion,sLineasTotales";

    var arrDatos = {
        "sVista": "v_IntegracionOrdenVentaLista", "sColumnas": sColumnas, "sOrderBy": "ION_FLAG desc,sFechaOrdenVenta"
    }

    fn_LlenarTabla("htblIntegracionOV", arrDatos, sColumnas);
    $("#dialogOVIntegrando").modal("show");
}

function fn_MostrarNotificacionesOrdenesDia() {

    var sColumnas = "sFolioOrdenVenta,SOVST,sFechaOrdenVenta,sEstatus,sLineasTotales";

    var arrDatos = {
        "sVista": "v_OrdenVentaDiaLista", "sColumnas": sColumnas, "sOrderBy": "sFechaOrdenVenta"
    }

    fn_LlenarTabla("htblOVDia", arrDatos, sColumnas);
    $("#dialogOVDia").modal("show");
}
function fn_VerOrdenNotificacion(sIdOrden) {
    window.location = "/Vista/Tarificador/orden_venta.aspx?sIdOrden=" + sIdOrden;
}

// Aprobar cancelacion de la ov
function fn_AprobarCancelacionOV(sIdOrden) {
    // Datos a enviar
    var sDatos = "{sIdOrdenVenta: '" + sIdOrden + "', iIdUsuario: '" + $("#hlblIdUsuario").html() + "'}";
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "../Tarificador/orden_venta.aspx/fn_AprobarCancelacionOV",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            // Verificar resultado 2 = cancelado
            if (data.d.iResultado == 2) {
                $.notificacionMsj(1, "Orden cancelada correctamente");
                fn_MostrarNotificacionesOrdenes();
            } else {
                $.notificacionMsj(2, "No se pudo cancelar la orden de venta");
            }

            //Se desbloquea la pantalla
            $.desbloquearPantalla();
        }, error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            //Se muestra notificación de error
            $.notificacionMsj(3, err.Message);
            $("#htxtBodyDialog").html(data.d.sMensaje);
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
        }
    });
}

function fn_DescartarCancelacionOV(sIdOrden) {
    // Datos a enviar
    var sDatos = "{sIdOrdenVenta: '" + sIdOrden + "', iIdUsuario: '" + $("#hlblIdUsuario").html() + "'}";
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "../Tarificador/orden_venta.aspx/fn_DescartarCancelacionOV",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            // Verificar resultado
            if (data.d.iResultado == 1) {
                $.notificacionMsj(1, "La orden se ha regresado a su estado anterior");
                fn_MostrarNotificacionesOrdenes();
            } else {
                $.notificacionMsj(2, "No se pudo descartar la cancelacion");
            }
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
        }, error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            //Se muestra notificación de error
            $.notificacionMsj(3, err.Message);
            $("#htxtBodyDialog").html(data.d.sMensaje);
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
        }
    });
}

//funcion que comberte la primer letra de un texto en mayuscula
String.prototype.capitalize = function () {
    return this.toLowerCase().replace(/(^|\s)([a-z])/g,
        function (m, p1, p2) {
            return p1 + p2.toUpperCase();
        });
};


//Función para llenar lista
function fn_LlenarTabla(sIdTable, arrDatos, sColumnas, fnDraw) {
    //Se crea variable arreglo
    var arrColumnasTab = []
    var arrColumnas = [];
    fnDraw = fnDraw != null && fnDraw != undefined ? fnDraw : function () { };
    //Se transforma string en array
    arrColumnas = sColumnas.split(',');
    //Se recorren las columnas para agregarlas a una variable
    for (var i = 0; i < arrColumnas.length; i++) {
        arrColumnasTab.push({ "data": arrColumnas[i] });
    }
    //Se destrye la tabla
    $("#" + sIdTable).DataTable().destroy();
    //Se crea la tabla
    $("#" + sIdTable).DataTable({
        "jQueryUI": true,
        "processing": true,
        "serverSide": true,
        "drawCallback": fnDraw,
        "ajax": {
            //Se pasa la URL del handler
            "url": "../../Handlers/Utilerias/h_listar.ashx",
            //Se pasa arreglo de datos
            "data": arrDatos,
            "type": "POST",
            "error": function (xhr, error, status) {
                //se desbloquea la Pantalla
                $.desbloquearPantalla();
                //Se envía mensaje de error
                $.notificacionMsj(3, "Error al generar listado: " + status);
            }
        },
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
        //Se pasa arreglo de columnas
        "columns": arrColumnasTab
    });
    //Se declara variable tipo DataTable
    var tblTable = $('#' + sIdTable).DataTable();
    //se oculta el Filtro principal del DataTable 
    $("#" + sIdTable + "_filter").hide();
    //se le da funcionalidad a los Fitros en la Cabezera
    tblTable.columns().eq(0).each(function (colIdx) {
        $('input', tblTable.column(colIdx).footer()).on('keydown', function (e) {
            var code = e.which;
            if (e.keyCode == 13 || code == 9) {
                tblTable.column(colIdx).search(this.value).draw();
                return false;
            }
        });
    });
}

// Funcion llenar tabla sin paginación
function fn_LlenarTablaSinPaginar(sIdTable, arrDatos, sColumnas) {
    //Se crea variable arreglo
    var arrColumnasTab = []
    var arrColumnas = [];
    //Se transforma string en array
    arrColumnas = sColumnas.split(',');
    //Se recorren las columnas para agregarlas a una variable
    for (var i = 0; i < arrColumnas.length; i++) {
        arrColumnasTab.push({ "data": arrColumnas[i] });
    }
    //Se destrye la tabla
    $("#" + sIdTable).DataTable().destroy();
    //Se crea la tabla
    $("#" + sIdTable).DataTable({
        "paging": false,
        "jQueryUI": true,
        "processing": true,
        "serverSide": true,
        "drawCallback": function (settings) {

        },
        "ajax": {
            //Se pasa la URL del handler
            "url": "../../Handlers/Utilerias/h_listar.ashx",
            //Se pasa arreglo de datos
            "data": arrDatos,
            "type": "POST",
            "error": function (xhr, error, status) {
                //se desbloquea la Pantalla
                $.desbloquearPantalla();
                //Se envía mensaje de error
                $.notificacionMsj(3, "Error al generar listado: " + status);
            }
        },
        //Se pasa arreglo de columnas
        "columns": arrColumnasTab
    });
    //Se declara variable tipo DataTable
    var tblTable = $('#' + sIdTable).DataTable();
    //se oculta el Filtro principal del DataTable 
    $("#" + sIdTable + "_filter").hide();
    //se le da funcionalidad a los Fitros en la Cabezera
    tblTable.columns().eq(0).each(function (colIdx) {
        $('input', tblTable.column(colIdx).footer()).on('keydown', function (e) {
            var code = e.which;
            if (e.keyCode == 13 || code == 9) {
                tblTable.column(colIdx).search(this.value).draw();
                return false;
            }
        });
    });
}

function fn_CargarTabla(sIdTable) {

    //Se destrye la tabla
    $("#" + sIdTable).DataTable().destroy();
    //Se crea la tabla
    $("#" + sIdTable).DataTable({
        "jQueryUI": true
    });
    //Se declara variable tipo DataTable
    var tblTable = $('#' + sIdTable).DataTable();
    //se oculta el Filtro principal del DataTable 
    $("#" + sIdTable + "_filter").hide();
    //se le da funcionalidad a los Fitros en la Cabezera
    tblTable.columns().eq(0).each(function (colIdx) {
        $('input', tblTable.column(colIdx).footer()).on('keydown', function (e) {
            var code = e.which;
            if (e.keyCode == 13 || code == 9) {
                tblTable.column(colIdx).search(this.value).draw();
                return false;
            }
        });
    });
}


function fn_Encode(sInput) {
    // Create Base64 Object
    var Base64 = { _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=", encode: function (e) { var t = ""; var n, r, i, s, o, u, a; var f = 0; e = Base64._utf8_encode(e); while (f < e.length) { n = e.charCodeAt(f++); r = e.charCodeAt(f++); i = e.charCodeAt(f++); s = n >> 2; o = (n & 3) << 4 | r >> 4; u = (r & 15) << 2 | i >> 6; a = i & 63; if (isNaN(r)) { u = a = 64 } else if (isNaN(i)) { a = 64 } t = t + this._keyStr.charAt(s) + this._keyStr.charAt(o) + this._keyStr.charAt(u) + this._keyStr.charAt(a) } return t }, decode: function (e) { var t = ""; var n, r, i; var s, o, u, a; var f = 0; e = e.replace(/[^A-Za-z0-9+/=]/g, ""); while (f < e.length) { s = this._keyStr.indexOf(e.charAt(f++)); o = this._keyStr.indexOf(e.charAt(f++)); u = this._keyStr.indexOf(e.charAt(f++)); a = this._keyStr.indexOf(e.charAt(f++)); n = s << 2 | o >> 4; r = (o & 15) << 4 | u >> 2; i = (u & 3) << 6 | a; t = t + String.fromCharCode(n); if (u != 64) { t = t + String.fromCharCode(r) } if (a != 64) { t = t + String.fromCharCode(i) } } t = Base64._utf8_decode(t); return t }, _utf8_encode: function (e) { e = e.replace(/rn/g, "n"); var t = ""; for (var n = 0; n < e.length; n++) { var r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r) } else if (r > 127 && r < 2048) { t += String.fromCharCode(r >> 6 | 192); t += String.fromCharCode(r & 63 | 128) } else { t += String.fromCharCode(r >> 12 | 224); t += String.fromCharCode(r >> 6 & 63 | 128); t += String.fromCharCode(r & 63 | 128) } } return t }, _utf8_decode: function (e) { var t = ""; var n = 0; var r = c1 = c2 = 0; while (n < e.length) { r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r); n++ } else if (r > 191 && r < 224) { c2 = e.charCodeAt(n + 1); t += String.fromCharCode((r & 31) << 6 | c2 & 63); n += 2 } else { c2 = e.charCodeAt(n + 1); c3 = e.charCodeAt(n + 2); t += String.fromCharCode((r & 15) << 12 | (c2 & 63) << 6 | c3 & 63); n += 3 } } return t } }
    // Se decodifica cadena
    var encodedString = Base64.encode(sInput);
    //Se retorna resultado
    return encodedString;
}

function fn_Decode(sInput) {
    // Create Base64 Object
    var Base64 = { _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=", encode: function (e) { var t = ""; var n, r, i, s, o, u, a; var f = 0; e = Base64._utf8_encode(e); while (f < e.length) { n = e.charCodeAt(f++); r = e.charCodeAt(f++); i = e.charCodeAt(f++); s = n >> 2; o = (n & 3) << 4 | r >> 4; u = (r & 15) << 2 | i >> 6; a = i & 63; if (isNaN(r)) { u = a = 64 } else if (isNaN(i)) { a = 64 } t = t + this._keyStr.charAt(s) + this._keyStr.charAt(o) + this._keyStr.charAt(u) + this._keyStr.charAt(a) } return t }, decode: function (e) { var t = ""; var n, r, i; var s, o, u, a; var f = 0; e = e.replace(/[^A-Za-z0-9+/=]/g, ""); while (f < e.length) { s = this._keyStr.indexOf(e.charAt(f++)); o = this._keyStr.indexOf(e.charAt(f++)); u = this._keyStr.indexOf(e.charAt(f++)); a = this._keyStr.indexOf(e.charAt(f++)); n = s << 2 | o >> 4; r = (o & 15) << 4 | u >> 2; i = (u & 3) << 6 | a; t = t + String.fromCharCode(n); if (u != 64) { t = t + String.fromCharCode(r) } if (a != 64) { t = t + String.fromCharCode(i) } } t = Base64._utf8_decode(t); return t }, _utf8_encode: function (e) { e = e.replace(/rn/g, "n"); var t = ""; for (var n = 0; n < e.length; n++) { var r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r) } else if (r > 127 && r < 2048) { t += String.fromCharCode(r >> 6 | 192); t += String.fromCharCode(r & 63 | 128) } else { t += String.fromCharCode(r >> 12 | 224); t += String.fromCharCode(r >> 6 & 63 | 128); t += String.fromCharCode(r & 63 | 128) } } return t }, _utf8_decode: function (e) { var t = ""; var n = 0; var r = c1 = c2 = 0; while (n < e.length) { r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r); n++ } else if (r > 191 && r < 224) { c2 = e.charCodeAt(n + 1); t += String.fromCharCode((r & 31) << 6 | c2 & 63); n += 2 } else { c2 = e.charCodeAt(n + 1); c3 = e.charCodeAt(n + 2); t += String.fromCharCode((r & 15) << 12 | (c2 & 63) << 6 | c3 & 63); n += 3 } } return t } }
    // Se decodifica cadena
    var decodedString = Base64.decode(sInput);
    //Se retorna resultado
    return decodedString;
}

//Función para limpiar campos
function fn_LimpiarCampos(idForm) {
    //Se reinicia el validador
    $("#" + idForm).data('bootstrapValidator').resetForm();
    //Se limpian campos
    $("#" + idForm)[0].reset();
    //Se limpian combos
    $("#" + idForm + " .selectpicker").val("");
    $("#" + idForm + " .selectpicker").selectpicker("render");
}

//Función para generar combobox
function fn_GeneraCombo(sDiv, sQuery, sNombre) {
    //Se remplaza comilla simple por simbolo
    sQuery = sQuery.replace("'", "@").replace("'", "@");
    $.ajax({
        url: "../Inicio/Inicio.aspx/fn_GeneraCombobox",
        type: "POST",
        dataType: "JSON",
        data: " { sQuery:'" + sQuery + "', sNombre:'" + sNombre + "' } ",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Se crean los radios de instancia
            $("#" + sDiv).html(data.d);
            $("#" + sDiv + " .selectpicker").selectpicker("render");
        }, error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            //Se muestra notificación de error
            $.notificacionMsj(3, err.Message);
        }
    });
}

//Función para cargar un archivo al la aplicación
//recibe inputFile, extenciónArchivo, carpetaGuardar 
function fn_CargaArchivo(fileInput, extencion, carpeta, nombre) {
    //bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    var res = "";
    nombre = nombre == null || nombre == undefined ? '' : nombre;
    /*************** Guardar archivo ***************/
    if (fileInput != null && fileInput != "" && fileInput != "undefined") {
        var file_data = fileInput.prop("files")[0];
        var form_data = new FormData();
        form_data.append("file", file_data);
        form_data.append("extencion", extencion);
        form_data.append("carpeta", carpeta);
        form_data.append("nombre", nombre);
        $.ajax({
            url: "../../Handlers/Utilerias/h_carga_archivo.ashx",//dirección del manejador que carga el archivo
            dataType: 'JSON',
            cache: false,
            contentType: false,
            async: false, //con esto podemos regresar una respuesta desde el succes
            processData: false,
            data: form_data,
            type: 'POST',
            success: function (data) {
                $.desbloquearPantalla();
                if (data.iResultado == 1) {
                    //$.notificacionMsj(1, data.sMensaje);
                    res = "1";
                } else {
                    $.notificacionMsj(2, data.sMensaje);
                }
            }
        });
    } else {
        $.notificacionMsj(2, "No se cargó el archivo");
        $.desbloquearPantalla();
    }
    return res;
}

function fn_CargaArch(fileInput, extencion, carpeta, nombre) {
    //bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    var res = "";
    nombre = nombre == null || nombre == undefined ? '' : nombre;
    /*************** Guardar archivo ***************/
    if (fileInput != null && fileInput != "" && fileInput != "undefined") {
        var file_data = fileInput;
        var form_data = new FormData();
        form_data.append("file", file_data);
        form_data.append("extencion", extencion);
        form_data.append("carpeta", carpeta);
        form_data.append("nombre", nombre);
        //url: "../../Handlers/Utilerias/h_carga_archivo.ashx",//dirección del manejador que carga el archivo
        //url: "../../App_Code/Controlador/WS/SolicitudTransferenciaWS.cs/fn_GuardaArchivoPPPP",
        //url: "../../Handlers/Utilerias/h_carga_archivo.ashx",
        $.ajax({
            url: "../../Handlers/Utilerias/h_carga_archivo_cliente.ashx",
            type: "POST",
            dataType: "JSON",
            data: form_data,//data: "{ QUEQUE: ''}",
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                //alert(Object.values(data));
                $.desbloquearPantalla();
                if (data.iResultado == 1) {
                    //$.notificacionMsj(1, data.sMensaje);
                    res = "1";
                } else {
                    $.notificacionMsj(2, data.sMensaje);
                }
            }
        });
    } else {
        $.notificacionMsj(2, "No se cargó el archivo");
        $.desbloquearPantalla();
    }
    return res;
}

//Función para quitar opciones al los combos
function fn_QuitarOpcionesCombo(iIdDropDown) {
    //inicia contador para contar los indices del combo
    var cnt = 0;
    //recorre cada opcion del combo
    $("#" + iIdDropDown + " option").each(function () {
        //si el indice del combo es mayor a cero
        if (cnt > 0) {
            //remueve la opción del combo
            $("#" + iIdDropDown + " option").eq(1).remove();
        }
        //aumenta el indice del combo
        cnt = cnt + 1;
    });
    //refresca el combo
    $("#" + iIdDropDown + "").selectpicker('refresh');
    //selecciona opción por default
    $("#" + iIdDropDown + " option[value=]").prop('selected', 'selected').change();
}


//Función para cargar un archivo al la aplicación de un input file multiple
//recibe inputFile, numero de archivo especifico, extenciónArchivo, carpetaGuardar 
function fn_CargaArchivoMultiple(fileInput, iArchivo, extencion, carpeta) {
    //bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    var res = "";

    /*************** Guardar archivo ***************/
    if (fileInput != null && fileInput != "" && fileInput != "undefined") {
        
        var file_data = fileInput.prop("files")[iArchivo];
        var form_data = new FormData();
        var nombre = fileInput.prop("files")[iArchivo]["name"].replace(/[{()}]/g, "").replace(/\s/g, "_");
        nombre = nombre.replace(extencion, "");
        form_data.append("file", file_data);
        form_data.append("extencion", extencion);
        form_data.append("carpeta", carpeta);
        form_data.append("nombre", nombre);
        $.ajax({
            url: "../../Handlers/Utilerias/h_carga_archivo.ashx",//dirección del manejador que carga el archivo
            dataType: 'JSON',
            cache: false,
            contentType: false,
            async: false, //con esto podemos regresar una respuesta desde el succes
            processData: false,
            data: form_data,
            type: 'POST',
            success: function (data) {
                $.desbloquearPantalla();
                if (data.iResultado == 1) {
                    //$.notificacionMsj(1, data.sMensaje);
                    res = "1";
                } else {
                    $.notificacionMsj(2, data.sMensaje);
                }
            }
        });
    } else {
        $.notificacionMsj(2, "No se cargó el archivo");
        $.desbloquearPantalla();
    }
    return res;
}
//Función para mostrar archivo de servidor
///  form_data.append("iId", -> id del documento)
///  form_data.append("sIdentificador", -> nombre de la columna del identificador)
/// form_data.append("sTabla", -> tabla donde se encuentra la direccion del archivo)
/// form_data.append("sCampo", -> campo con la ruta del archivo)
/// form_data.append("sArchivo", -> nombre para el archivo)
/// form_data.append("sTipoArchivo", '-> extencion del archivo [pdf,exl,etc.])
function fn_MostrarArchivoDoc(iId, sIdentificador, sTabla, sCampo, sArchivo, sTipoArchivo) {
    //Se crea una variable de FormData
    //var res ="";
    var form_data = new FormData();
    //Se pasan los parametros al FormData
    form_data.append("iId", iId)
    form_data.append("sIdentificador", sIdentificador)
    form_data.append("sTabla", sTabla)
    form_data.append("sCampo", sCampo)
    form_data.append("sArchivo", sArchivo)
    form_data.append("sTipoArchivo", sTipoArchivo)
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "../../Handlers/Utilerias/h_mostrar_archivo.ashx",
        dataType: 'JSON',
        cache: false,
        contentType: false,
        processData: false,
        data: form_data,
        type: 'POST',
        async: false,
        success: function (data) {
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            //Se abre el PDF
            window.open(data, sTipoArchivo, "width=1000,height=800,scrollbars=NO");
            //res = data;
            setTimeout(function () { fn_EliminarArchivoDoc(data) }, 10000);
        }
    });
    //return res;
}


function fn_GeneraReporte(tabla, btnClick) {
    var oTable;
    oTable = $('#' + tabla).dataTable();
    var oSettings = oTable.fnSettings();
    oSettings._iDisplayLength = -1;
    oTable.fnDraw();
    oTable.on('draw.dt', function () {
        if (btnClick) {
            setTimeout(async function () {
                await $("#" + tabla).table2excel({
                    exclude: ".noExl",
                    name: "Reporte",
                    filename: "Reporte ",
                    fileext: ".xls",
                    preserveColors: true,
                    exclude_inputs: true,
                    exclude_img: true,
                    exclude_links: true
                });
                btnClick = false;
                oSettings._iDisplayLength = 10;
                oTable.fnDraw();
            }, 100);
        }
        });
}


// funcion para eliminar archivo 
function fn_EliminarArchivoDoc(sRuta) {
    //Se crea una variable de FormData
    var form_data = new FormData();
    //var res = "";
    //variable
    form_data.append("sRuta", sRuta)
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "../../Handlers/Utilerias/h_eliminar_archivo.ashx",
        dataType: 'JSON',
        cache: false,
        contentType: false,
        processData: false,
        data: form_data,
        type: 'POST',
        async: false,
        success: function (data) {
            if (data == "1") {
                //res = data;
                $.desbloquearPantalla();
            } else {
                // Mensaje de Error.
                $.notificacionMsj(3, data);
            }
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
        }, error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            //Se muestra notificación de error
            $.notificacionMsj(3, err.Message);
        }
    });
}