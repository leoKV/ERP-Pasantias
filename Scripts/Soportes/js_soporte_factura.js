
$(document).ready(function () {
    //Se llenan datos de tabla referencias
    fn_AgregarFiltrosFactura();
    fn_LlenarTablaFac();


    $("#btnCambiarEstatus").click(function () {
        fn_ValidarCambioEstatus();
    });



});

//Función para llenar la tabla de referencias
function fn_LlenarTablaFac() {
    var sIdUsuario = $("#hlblIdUsuario").html();
    //Se crea arreglo de columnas
    var sColumnas = "sUUIDColores,sNoFactura,sCliente,sProveedor,sFechaFactura,sMonto,sRefAdministrativa,sEstatus,sFormaPago,sFechaEntrada,sCambio";

    var arrDatos = {
        "sVista": "v_ListaSoporteFactura", "sColumnas": sColumnas, "sWhere": "",
        "sOrderBy": "sFechaFactura",
        "sIdUsuario": sIdUsuario

    }

    fn_LlenarTablaFacturas("htblFacturas", arrDatos, sColumnas);


}

//Función para llenar lista
function fn_LlenarTablaFacturas(sIdTable, arrDatos, sColumnas) {
    //Se crea variable arreglo
    var arrColumnasTab = [];
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
        "jQueryUI": true,
        "processing": true,
        "deferRender": true,
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

//Función para llenar la tabla de colores
function fn_LlenarTablaColores() {
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    //Se crea arreglo de columnas
    var sColumnas = "sColores,sDescripcion";
    //Se crea arreglo de datos
    var arrDatos = { "sVista": "v_ListaColoresReferencia", "sColumnas": sColumnas, "sWhere": "", "sOrderBy": "" }
    //Se llama función para llenar tabla
    fn_LlenarTabla("htblColores", arrDatos, sColumnas);
    //Se abre dialog servicios
    $("#dialogColores").modal("show");
    //Se desbloquea la pantalla
    $.desbloquearPantalla();
}


//Funcion para el filtrado de los datos
function fn_AgregarFiltrosFactura() {
    // Busqueda Referencías.
    $("#divFiltros").show();
    $("#hdvFac").hide();

    //var sOpcion = 1;
    fn_GeneraAutocompleteEstatusFacturaFiltro();
    //fn_GeneraAutocompleteTipoReferencia();

    $("#hbtnFiltrarFactura").unbind("click");
    $("#hbtnFiltrarFactura").click(function () {
        fn_GenerarListadoFiltros();
    });



    $("#hbtnAbrirFiltroFacturas").unbind("click");

    $("#hbtnAbrirFiltroFacturas").click(function () {
        $("#dlogFiltrarFactura").modal("show");
    });

    $("#dlogFiltrarFactura").modal("show");

}



// Generar Autocomplete Estatus Modal Busqueda
function fn_GeneraAutocompleteEstatusST(sOpcion) {

    $("#htxtCambioEstatus").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "soporte_ST.aspx/fn_GeneraAutocompleteEstatusST",
                data: "{ 'sCadena': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.nombre,
                            id: item.ID,
                            value: item.nombre
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        open: function () {
            setTimeout(function () {
                $('.ui-autocomplete').css('z-index', 99999999999999);
            }, 0);
        },
        minLength: 3,
        select: function (event, ui, ID) {
            $("#htxtCambioEstatus").val(ui.item.nombre);
            $("#hlblIdCambioEstatus").val(ui.item.id);
        }
    });

}

//Generar Autocomplete Estatus Tabla
function fn_GeneraAutocompleteEstatusFacturaFiltro() {

    $("#htxtAutocompleteEstatusFacturaM").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "soporte_facturas.aspx/fn_GeneraAutocompleteEstatusFactura",
                data: "{ 'sCadena': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.nombre,
                            id: item.ID,
                            value: item.nombre
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        open: function () {
            setTimeout(function () {
                $('.ui-autocomplete').css('z-index', 99999999999999);
            }, 0);
        },
        minLength: 3,
        select: function (event, ui, ID) {
            $("#htxtAutocompleteEstatusFacturaM").val(ui.item.nombre);
            $("#hlblIdEstatusFacturaM").val(ui.item.id);
        }
    });
}


function fn_ValidarCambioEstatus() {
    var sNuevoEstatus = $("#hlblIdCambioEstatus").val();

    if (sNuevoEstatus != null && sNuevoEstatus != undefined && sNuevoEstatus != "") {
        var sEstatus = $("#htxtIdEstatus").val();


        if (sNuevoEstatus != sEstatus) {
            CambiarEstatusST();

        } else {
            $("#htxtCambioEstatus").val("");
            $("#hlblIdCambioEstatus").val("");
            $.notificacionMsj(2, "Aún no se ha seleccionado un estatus distinto al actual.");

        }
    }

    else {
        $("#htxtCambioEstatus").val("");
        $("#hlblIdCambioEstatus").val("");
        $.notificacionMsj(2, " Debe de seleccionar uno de los estatus sugeridos");
    }


    //alert(sEstatus);
    //alert(sNuevoEstatus);

}



//Función para llenar la tabla de referencias con el filtrado.
function fn_GenerarListadoFiltros() {

    var sEstatus = $("#hlblIdEstatusFacturaM").val();
    var sUUID = $("#htxtFiltroFactura").val();
    var sWhere = "";
    var bFiltrar = false;


    if (sEstatus != '' && sEstatus != undefined && sEstatus != null && sEstatus != 0) {
        bFiltrar = true;
        if (sWhere == '') {
            sWhere = "sIEstatusFactura = " + sEstatus;
        } else {
            sWhere += " and sIEstatusFactura = " + sEstatus;
        }
    }

    if (sUUID != '' && sUUID != undefined && sUUID != null && sUUID != 0) {
        bFiltrar = true;
        if (sWhere == '') {
            sWhere = "sUUID like '" + sUUID + "'";
        } else {
            sWhere += " and sUUID like '" + sUUID + "'";
        }
    }

    if (bFiltrar) {
        var sIdUsuario = $("#hlblIdUsuario").html();
        //Se crea arreglo de columnas
        var sColumnas = "sUUIDColores,sNoFactura,sCliente,sProveedor,sFechaFactura,sMonto,sRefAdministrativa,sEstatus,sFormaPago,sFechaEntrada,sCambio";

        var arrDatos = {
            "sVista": "v_ListaSoporteFactura", "sColumnas": sColumnas, "sWhere": sWhere,
            "sOrderBy": "",
            "sIdUsuario": sIdUsuario
        }

        //Se llama función para llenar tabla 
        fn_LlenarTablaFacturas("htblFacturas", arrDatos, sColumnas);



        $("#hdvFac").show();
        $("#dlogFiltrarFactura").modal("hide")
    } else {
        $.desbloquearPantalla();
        $.notificacionMsj(2, "Por favor selecciona al menos un dato para filtrar las solicitudes.");
    }

}




function fn_SeleccionCliente() {
    var opcion = $('input[name=rdbCambioCliente]:checked').val();
    if (opcion != undefined && opcion != '') {
        switch (opcion) {
            //Se escoge opcion Cliente Contable
            case "1":
                $("#lblCambioCO").hide();  // Escondemos
                $("#htxtCambioCO").hide();  // Escondemos
                $("#htxtCambioCO").val(''); //Borramos Valores
                $("#htxtIdCambioCO").val(''); //Borramos Valores

                $("#lblCambioCC").show();  // Mostramos
                $("#htxtCambioCC").show();  // Mostramos
                break;
            //Se escoge opcion Cliente Operativo
            case "2":
                $("#lblCambioCC").hide();  // Escondemos
                $("#htxtCambioCC").hide();  // Escondemos
                $("#htxtCambioCC").val(''); //Borramos Valores
                $("#htxtIdCambioCC").val(''); //Borramos Valores
                $("#lblCambioCO").show();  // Mostramos
                $("#htxtCambioCO").show();  // Mostramos
                break;
            //Se escoge opcion Ambos
            case "3":
                $("#lblCambioCC").show();  // Mostramos
                $("#htxtCambioCC").show();  // Mostramos
                $("#lblCambioCO").show();  // Mostramos
                $("#htxtCambioCO").show();  // Mostramos
                break;

            //No se escoge opcion dejamos por default la número uno
            default:
                $("#lblCambioCO").hide();  // Escondemos
                $("#htxtCambioCO").hide();  // Escondemos
                $("#htxtCambioCO").val(''); //Borramos Valores
                $("#htxtIdCambioCO").val(''); //Borramos Valores

                $("#lblCambioCC").show();  // Mostramos
                $("#htxtCambioCC").show();  // Mostramos
                break;
        }
    }

}

function fn_Cliente(n) {

    $("#dialogModificarCliente").modal("show");


    $.bloquearPantalla("Cargando...");

    $.ajax({
        url: "soporte_referencia.aspx/fn_Cliente",
        type: "POST",
        dataType: "JSON",
        data: " { sRef: " + n + " } ",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (data) {
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            $("#hlblClienteCC").html(data.d.sClienteConta);
            $("#hlblClienteOC").html(data.d.sClienteOpe);
            fn_GeneraAutocompleteClientC();
            fn_GeneraAutocompleteClientO();
            $("#hlblGenidRef").html(n);
        }, error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            //Se muestra notificación de error
            $.notificacionMsj(3, err.Message);
        }
    });

    $("#htxtCambioCO").hide();  // hide
    $("#lblCambioCO").hide();  // hide


}


function fn_GeneraAutocompleteProvST() {
    $("#htxtCambioProvST").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "soporte_ST.aspx/fn_GeneraAutocompleteProvST",
                data: "{ 'sCadena': '" + request.term + "' }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.nombre,
                            id: item.ID,
                            value: item.nombre
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        open: function () {
            setTimeout(function () {
                $('.ui-autocomplete').css('z-index', 99999999999999);
            }, 0);
        },
        minLength: 3,
        select: function (event, ui, ID) {
            $("#htxtCambioProvST").val(ui.item.nombre);
            $("#lblIdCambioProvST").val(ui.item.id);
        }
    });
}

function fn_GeneraAutocompleteClientO() {
    $("#htxtCambioCO").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "soporte_referencia.aspx/fn_GeneraAutocompleteClientes",
                data: "{ 'sCadena': '" + request.term + "' }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.nombre,
                            id: item.ID,
                            value: item.nombre
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        open: function () {
            setTimeout(function () {
                $('.ui-autocomplete').css('z-index', 99999999999999);
            }, 0);
        },
        minLength: 3,
        select: function (event, ui, ID) {
            $("#htxtCambioCO").val(ui.item.nombre);
            $("#htxtIdCambioCO").val(ui.item.id);
        }
    });
}



function fn_ValidarCliente() {
    var opcion = $('input[name=rdbCambioCliente]:checked').val();
    var idCC = 0;
    var idCO = 0;
    if (opcion != undefined && opcion != '') {
        switch (opcion) {
            //Se escoge opcion Cliente Contable
            case "1":
                idCC = $("#htxtIdCambioCC").val();

                if (idCC != 0 && idCC != undefined) {

                    fn_GuardarCliente(1, idCC, idCO);

                } else {
                    $.notificacionMsj(2, "Debe de seleccionar una opcion valida para el cliente Contable");
                }
                break;
            //Se escoge opcion Cliente Operativo
            case "2":
                idCO = $("#htxtIdCambioCO").val();

                if (idCO != 0 && idCO != undefined) {
                    fn_GuardarCliente(2, idCC, idCO);
                } else {
                    $.notificacionMsj(2, "Debe de seleccionar una opcion valida para el cliente Operativo");
                }

                break;
            //Se escoge opcion Ambos
            case "3":
                idCC = $("#htxtIdCambioCC").val();
                idCO = $("#htxtIdCambioCO").val();

                if ((idCC != 0 && idCC != undefined) && (idCO != 0 && idCO != undefined)) {
                    fn_GuardarCliente(3, idCC, idCO);
                } else {
                    $.notificacionMsj(2, "Debe de seleccionar una opcion valida para ambos clientes");
                }
                break;

            //No se escoge opcion dejamos por default la número uno
            default:
                $.notificacionMsj(2, "Debe de seleccionar una opcion de cliente para cambiar");
                break;
        }
    }

    else {
        $.notificacionMsj(2, "Debe de seleccionar una opcion de cliente para cambiar");
    }




}


function fn_GuardarCliente(opcion, idCC, idCO) {
    //Se bloquea la pantalla
    var sIdUsuario = $("#hlblIdUsuario").html();
    var sidRef = $("#hlblGenidRef").html();

    $.bloquearPantalla("Cargando...");
    //Se crea variable para almacenar datos


    var sDatos = "{sIdUsuario: '" + sIdUsuario + "', sIdRef: " + sidRef + " , sOpcion:" + opcion + ", sIdClienteC: " + idCC + ", sIdClienteO: " + idCO + " } ";
    $.ajax({
        url: "soporte_referencia.aspx/fn_GuardarClienteReferencia",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Se cierra el dialog

            $("#dialogModificarEstatus").modal("hide");
            //Se recarga lista servicios
            fn_GenerarListadoFiltros();
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



function fn_Estatus(idSTEstatus, idEstatus) {
    $("#htxtIdSTEstatus").val(idSTEstatus);
    $("#htxtIdEstatus").val(idEstatus);
    fn_GeneraAutocompleteEstatusST();
    $("#dialogModificarEstatus").modal("show");

}


function CambiarEstatusST() {

    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");

    //Se obtiene datos para la actualizacion.
    var sIdUsuario = $("#hlblIdUsuario").html();

    var sIdSTEstatus = $("#htxtIdSTEstatus").val();

    var sNuevoEstatus = $("#hlblIdCambioEstatus").val();

    //Se crea variable para almacenar datos
    var sDatos = "{sIdUsuario: '" + sIdUsuario + "', sIdSTEstatus:'" + sIdSTEstatus + "', sIdEstatus:'" + sNuevoEstatus + "'} ";
    $.ajax({
        url: "soporte_ST.aspx/fn_CambiarEstatusST",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Se cierra el dialog
            if (data.d.iResultado == 1) {
                $("#htxtIdSTEstatus").val("");
                $("#htxtIdEstatus").val("");
                $("#htxtCambioEstatus").val("");
                $("#hlblIdCambioEstatus").val("");
                //Se recarga lista servicios
                $("#dialogModificarEstatus").modal("hide");
                //Se recarga lista servicios
                fn_GenerarListadoFiltros();
                $.notificacionMsj(1, data.d.sMensaje);
            } else {
                // Eliminado
                $.notificacionMsj(2, "Algo salio mal, contacte al equipo de soporte");
                fn_GenerarListadoFiltros();
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

function fn_EliminarSTProceso(sIdST) {
    //Se obtiene datos para la actualizacion.
    var sIdUsuario = $("#hlblIdUsuario").html();

    // Guardar asignacion
    var sDatos = "{sIdST: '" + sIdST + "', sIdUsuario: '" + sIdUsuario + "'}";

    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");

    $.ajax({
        url: "soporte_ST.aspx/fn_EliminarST",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            // Verificar resultado
            if (data.d.iResultado == 1) {
                $("#dialogQuitarST").modal("hide");
                //Se recarga lista servicios
                fn_GenerarListadoFiltros();
                $.notificacionMsj(1, data.d.sMensaje);
            } else {
                // Eliminado
                $.notificacionMsj(2, "Algo salio mal, contacte al equipo de soporte");
                fn_LlenarTablaOrdenes();
            }
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            //$("#dialogQuitarOrden").modal("hide");
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


function fn_EliminarST(iIdST) {
    $("#dialogQuitarST").modal("show");
    $("#hbtnConfirmarST").unbind();
    $("#hbtnConfirmarST").click(function () {
        fn_EliminarSTProceso(iIdST);
    });
}

//Función para obtner datos del cliente
function fn_ObtenerProST(iIdST) {
    //Se crea variable para almacenar datos
    var sDatos = " { siIdST:'" + iIdST + "' } ";
    $.ajax({
        url: "soporte_ST.aspx/fn_ObtenerDatosProvST",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Se desbloquea la pantalla
            if (data.d.iResultado == 1) {
                $("#hlblProvActual").html(data.d.sProveedor);
            } else {
                $("#hlblProvActual").html('');
            }
            $.desbloquearPantalla();
            //llena campo

        }, error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            //Se muestra notificación de error
            $.notificacionMsj(3, err.Message);
        }
    });

}


function CambiarProvST(iIdST) {

    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");

    //Se obtiene datos para la actualizacion.
    var sIdUsuario = $("#hlblIdUsuario").html();

    //var sIdSTEstatus = $("#htxtIdSTEstatus").val();

    var sNuevoProvST = $("#lblIdCambioProvST").val();

    //Se crea variable para almacenar datos
    var sDatos = "{sIdUsuario: '" + sIdUsuario + "', sIdSTProv:'" + iIdST + "', sIdProvST:'" + sNuevoProvST + "'} ";
    $.ajax({
        url: "soporte_ST.aspx/fn_CambiarProvST",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Se cierra el dialog
            if (data.d.iResultado == 1) {
                $("#htxtCambioProvST").val("");
                $("#lblIdCambioProvST").val("");
                $("#hlblProvActual").val("");
                //$("#hlblIdCambioEstatus").val("");
                //Se recarga lista servicios
                $("#dialogModificarProveedorST").modal("hide");
                //Se recarga lista servicios
                fn_GenerarListadoFiltros();
                $.notificacionMsj(1, data.d.sMensaje);
            } else {
                // Eliminado
                $.notificacionMsj(2, "Algo salio mal, contacte al equipo de soporte");
                fn_GenerarListadoFiltros();
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


function fn_ValidarCambioProvST(iIdST) {
    var sNuevoProST = $("#lblIdCambioProvST").val();

    if (sNuevoProST != null && sNuevoProST != undefined && sNuevoProST != "") {
        CambiarProvST(iIdST);
    }
    else {
        $("#htxtCambioProvST").val("");
        $("#lblIdCambioProvST").val("");
        $.notificacionMsj(2, " Debe de seleccionar uno de las razónes sociales sugeridas");
    }


}



function fn_Proveedor(sIdST) {

    $("#dialogModificarProveedorST").modal("show");
    fn_ObtenerProST(sIdST);
    fn_GeneraAutocompleteProvST();


    $("#btnCambiarProvST").unbind();
    $("#btnCambiarProvST").click(function () {
        fn_ValidarCambioProvST(sIdST);
    });
}



function fn_EliminarSaldo(sIdSolicitud) {
    //Se genera la lista de los saldos que se utilizaron
    //Se crea arreglo de columnas
    var sColumnas = "sNoSolicitud,sTotal,sDocumentoSF,sCargo,sFecha,sMontoPagado,sOpcion";

    $("#htxtIdSTSaldo").val(sIdSolicitud);

    //Se crea arreglo de datos
    var arrDatos = { "sVista": "v_ListaSaldosSoporte", "sColumnas": sColumnas, "sWhere": "sIdSolicitud=" + sIdSolicitud, "sOrderBy": "" }
    //Se llama función para llenar tabla
    fn_LlenarTabla("htblSaldoAplicado", arrDatos, sColumnas);
    //abre dialog confirmación
    $('#dialogSaldoAplicado').modal('show');
}

function fn_EliminarSaldoSTProceso(sIdSaldo) {
    //Se obtiene datos para la actualizacion.
    var sIdUsuario = $("#hlblIdUsuario").html();
    var sIdST = $("#htxtIdSTSaldo").val();

    // Guardar asignacion
    var sDatos = "{sIdST: '" + sIdST + "', sIdUsuario: '" + sIdUsuario + "', sIdSaldo: '" + sIdSaldo + "' }";

    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");

    $.ajax({
        url: "soporte_ST.aspx/fn_EliminarSaldoST",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            // Verificar resultado
            if (data.d.iResultado == 1) {
                $("#dialogQuitarST").modal("hide");
                //Se recarga lista servicios
                fn_GenerarListadoFiltros();
                $.notificacionMsj(1, data.d.sMensaje);
            } else {
                // Eliminado
                $.notificacionMsj(2, "Algo salio mal, contacte al equipo de soporte");
                fn_LlenarTablaOrdenes();
            }
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            //$("#dialogQuitarOrden").modal("hide");
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


function fn_EliminarSaldoST(sIdSaldo) {
    $("#dialogQuitarSaldoST").modal("show");
    $("#hbtnConfirmarSaldoST").unbind();
    $("#hbtnConfirmarSaldoST").click(function () {
        fn_EliminarSaldoSTProceso(sIdSaldo);
    });
}

function fn_AgregarReenvio(UUID) {
    $("#dialogAjustarSRF").modal("show");
    $("#hbtnAjustarSRF").unbind();
    $("#hbtnAjustarSRF").click(function () {
        fn_AjustarReenvioFactura(UUID);
    });
}


function fn_AjustarReenvioFactura(UUID) {
    //Se obtiene el Usuario
    var sUsuario = $("#hlblIdUsuario").html();
    //Se hace la asignación de valores
    var sDatos = "{UUID: '" + UUID + "',sUsuario: '" + sUsuario + "'}"
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "soporte_facturas.aspx/fn_AjustarReenvioFacturas",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            // Verificar resultado
            if (data.d.iResultado == 1) {
                $("#dialogAjustarSRF").modal("hide");
                //Se recarga lista servicios
                fn_GenerarListadoFiltros();
                $.notificacionMsj(1, data.d.sMensaje);
            } else {
                // Eliminado
                $.notificacionMsj(2, "Algo salio mal, contacte al equipo de soporte");
                fn_LlenarTablaOrdenes();
            }
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            //$("#dialogQuitarOrden").modal("hide");
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

function fn_AbrirVolcadoDatos() {
    //Se muestra el modal de volcado de datos
    $("#dialogVolcadoDatos").modal("show");
    //Se deshabilita el botón para envio de datos
    $("#hbtnCargarDatos").prop("disabled",true);
}

function fn_validacionFormato() {
    //se obtiene el archivo
    var file = document.getElementById('hfileCargarArchivo');
    //se obtiene el nombre del archivo
    var filePath = file.value;
    var allowedExtensions = /(.xls|.xlsx)$/i;
    //se verifica la extensión del archivo
    if (!allowedExtensions.exec(filePath)) {
        //si no cumple notificamos al usuario
        $.notificacionMsj(2, "Por favor carga un archivo unicamente con extensión .xls|.xlsx");
        //se limpia el valor del input
        file.value = "";
        return false;
    } else {
        //se habilita el botón de carga de datos
            $("#hbtnCargarDatos").prop("disabled", false); 
            $("#hbtnCargarDatos").unbind();
        //Si el usuario cancela la acción
            $("#hbtnCancelarDatos").click(function () {
                //ocultamos el modal
                $("#dialogVolcadoDatos").modal("hide");
                //se limpia el valor del input
                file.value = "";
                return false;
            });

            $("#btnClose").click(function () {
                //Si el usuario cierra el modal se limpia el valor del input
                file.value = "";
            });

            $("#hbtnCargarDatos").click(function () {
                //Se envia el archivo para cargarlo
                fn_CargarArchivoFactura(file);
            });
    }
   
}

function fn_CargarArchivoFactura(file) {
    //Se obtiene el archivo completo
    var file_xlsx = file.files[0];
    //Se obtiene el nombre del archivo
    var archivoXlsx = (file_xlsx.name.substr(0, file_xlsx.name.lastIndexOf('.')).replace(/[{()}]/g, '')).replace(/\s/g, "_");
    //Se guarda el archivo fisico en la carpeta Documentos
    var resxlsx = fn_CargaArchivo($("#hfileCargarArchivo"), ".xlsx", "Soporte_Facturas", archivoXlsx);
    if (resxlsx== "1") {
        //enviamos el nombre del archivo al volcado de datos
        fn_VolcadoDatos((archivoXlsx + '.xlsx'));
    } else {
        //Se muestra notificación de error
        $.notificacionMsj(2, "Ocurrió un error al cargar los archivos");
    }
}

function fn_VolcadoDatos(file) {
    //Se obtiene el Usuario
    var sUsuario = $("#hlblIdUsuario").html();
    //Se hace la asignación de valores
    var sDatos = "{file: '" + file + "',sUsuario: '" + sUsuario + "'}"
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "soporte_facturas.aspx/fn_VolcadoDeDatos",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            // Verificar resultado
            if (data.d.iResultado == 1) {
                //fn_EliminarArchivoDoc(sRuta)
                //Se recarga lista servicios
                fn_GenerarListadoFiltros();
                //Se muestra la notificación en pantalla
                $.notificacionMsj(1, data.d.sMensaje);
            } else {
                //Se muestra la notificación en pantalla
                $.notificacionMsj(2, data.d.sMensaje);
                //Se recarga lista servicios
                fn_GenerarListadoFiltros();
                //Se limpia el valor del input
                document.getElementById('hfileCargarArchivo').value="";
            }
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            $("#dialogVolcadoDatos").modal("hide");
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
