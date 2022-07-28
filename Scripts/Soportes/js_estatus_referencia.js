

$(document).ready(function () {
    //Se llenan datos de tabla referencias
    fn_AgregarFiltrosReferencias();
    fn_LlenarTablaReferencias();
    ////Se crea el validador de modificacion del estatus
    fn_ValidadorEnvio();


    //Botón para llamar a la tabla colores
    $("#hbtnColores").click(function () {
        fn_LlenarTablaColores();
    });


    //Cambio para checked de clientes opciones
    $("input[name=rdbCambioCliente]").change(function () {

        fn_SeleccionCliente();
    });


    $("#btnCambiarCliente").click(function () {
        fn_ValidarCliente();
    });

    $("#btnCambiarEstatus").click(function () {
        fn_ValidarCambio();
    });

    

});

//Función para llenar la tabla de referencias
function fn_LlenarTablaReferencias() {
    $.bloquearPantalla("Cargando...");

    var sIdUsuario = $("#hlblIdUsuario").html();
    //Se crea arreglo de columnas
    var sColumnas = "sRefAdministrativa,sRefOperativa,sEstatusReferencia,sCambio";

    var arrDatos = {
        "sVista": "v_ListaReferenciasSoporte", "sColumnas": sColumnas, "sWhere": "",//(sTipoReferencia <> 'No Facturable' OR sTipoReferencia IS NULL)",
        "sOrderBy": "sRefOperativa",
        "sIdUsuario": sIdUsuario
    }
        //Se llama función para llenar tabla
    fn_LlenarTablaReferencia("htblReferencias", arrDatos, sColumnas);
    $.desbloquearPantalla();
}

//Función para llenar lista
function fn_LlenarTablaReferencia(sIdTable, arrDatos, sColumnas) {
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

//Función para limpiar campos servicio referencia
function fn_LimpiarCamposServicioReferencia() {
    //Se reinicia el validador
    $("#hfrmServicio").data('bootstrapValidator').resetForm();
    //Se limpian combos
    $("#hfrmServicio .selectpicker").val("");
    $("#hfrmServicio .selectpicker").selectpicker("render");
}



function fn_AgregarFiltrosReferencias() {
    //var iListado = $("#hlbliListado").html();
    // Historico / Busqueda Referencias.
    //if (iListado == 2) {
        // Busqueda Referencías.
        $("#divFiltros").show();
        $("#hdvReferencias").hide();
        var sOpcion = 1;
        fn_GeneraAutocompleteEstatusReferenciaModal();
        //fn_GeneraAutocompleteTipoReferencia();

        $("#hbtnFiltrarReferencia").unbind("click");
        $("#hbtnFiltrarReferencia").click(function () {
            fn_GenerarListadoFiltros();
        });

        $("#hbtnAbrirDialogoFR").unbind("click");
        $("#hbtnAbrirDialogoFR").click(function () {
            $("#dlogFiltrarReferencias").modal("show");
        });

        $("#dlogFiltrarReferencias").modal("show");

    //} else {
    //    $("#hbtnAbrirDialogoFR").hide();
    //    $("#divFiltros").hide();
    //    $("#hdvReferencias").show();
    //}
}



// Generar Autocomplete Estatus Modal Busqueda
function fn_GeneraAutocompleteEstatusReferencia(sOpcion,sPocision) {

    $("#htxtAutocompleteNuevoEstado" + sPocision).autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "estatus_referencia.aspx/fn_GeneraAutocompleteEstatusReferencia",
                    data: "{ 'sCadena': '" + request.term + "', sOpcion: "+ sOpcion  +"}",
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
                $("#htxtAutocompleteNuevoEstado" + sPocision).val(ui.item.nombre);
                $("#hlblIdEstatusNuevo" + sPocision).val(ui.item.id);
            }
        });
    


}

 //Generar Autocomplete Estatus Tabla
function fn_GeneraAutocompleteEstatusReferenciaModal() {
    
    $("#htxtCambioEstatus").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "soporte_referencia.aspx/fn_GeneraAutocompleteEstatusReferencia",
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
            $("#htxtIdCambioEstatus").val(ui.item.id);
        }
    });
}


function fn_ModificarEstatus() {

    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");

    //Se obtiene datos para la actualizacion.
    var numReferencia = $("#hblNumReferencia").html();
    var sIdUsuario = $("#hlblIdUsuario").html();


    var sIdSubReferencia = $("#hlblIdSubReferencia" + numReferencia).html();
    var sIdEstatus = $("#hlblIdEstatusNuevo" + numReferencia).val();
    var sMotivo = $("#htxtMotivo").val();

    //Se crea variable para almacenar datos
    var sDatos = "{sIdUsuario: '" + sIdUsuario + "', sIdSubReferencia:" + sIdSubReferencia + ", sIdEstatus:'" + sIdEstatus + "', sMotivo:'" + sMotivo + "' } ";
    $.ajax({
        url: "estatus_referencia.aspx/fn_CambiarEstatusReferencia",
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



// Función para generar validador.
function fn_ValidadorEnvio() {
    // Retirar el validador del reporte antes de generarlo.
    $("#hfrmModificarEstatus").bootstrapValidator("destroy");
    // Definir Validador.
    $('#hfrmModificarEstatus').bootstrapValidator({
        message: 'El valor es inválido.',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            // Campo Motivo.
            htxtMotivo: {
                validators: {
                    notEmpty: {
                        message: 'Aun no se ha escrito un motivo de cambio'
                    }
                }
            },
            onError: function () {
                //Se muestra notificación de error
                $.notificacionMsj(2, "Existen campos que no cumplen la validación.");
            }
        }
    }).on('success.form.bv', function (e) {
        // Cuando se aprueban las validaciones se genera el reporte.
        fn_ModificarEstatus();
    });
}


// Función para generar validador.
// Función para generar validador.
function fn_ValidarEstatus(n) {
    var sNuevoEstatus = $("#htxtAutocompleteNuevoEstado"+n).val();

    if (sNuevoEstatus != null && sNuevoEstatus != undefined && sNuevoEstatus != "") {
        var sIdEstatus = $("#hlblIdEstatusNuevo").val();

    
        if (sIdEstatus != 0) {
            $("#hblNumReferencia").html(n);
            $("#hblNumReferencia").val(n);
            $('#dialogModificarEstatus').modal('show');

        } else {
            $("#hlblIdEstatusNuevo").val("");
            $.notificacionMsj(2, "Debe de seleccionar uno de los estatus sugeridos");

        }
    } else {
        $("#hlblIdEstatusNuevo").val("");
        $.notificacionMsj(2, "Aún no se ha seleccionado un estatus nuevo.");
    }
}


function fn_ValidarCambio() {
    var sNuevoEstatus = $("#htxtIdCambioEstatus").val();
    
        //var sEstatus = $("#htxtIdEstatus").val();


    if (sNuevoEstatus != null && sNuevoEstatus != undefined && sNuevoEstatus != "") {
        var sEstatus = $("#htxtIdEstatus").val();


        if (sNuevoEstatus != sEstatus) {
            CambiarEstatus();

        } else {
            $("#htxtIdCambioEstatus").val("");
            $.notificacionMsj(2, "Aún no se ha seleccionado un estatus nuevo.");

        }
    }
   
    else {
        $("#htxtIdCambioEstatus").val("");
        $.notificacionMsj(2, " Debe de seleccionar uno de los estatus sugeridos");
    }


    //alert(sEstatus);
    //alert(sNuevoEstatus);
    
}







//Función para llenar la tabla de referencias con el filtrado.
function fn_GenerarListadoFiltros() {

    var sEstatus = $("#htxtAutocompleteEstatus").val();
    

    //alert(sEstatus);
    var sReferencia = $("#htxtReferenciaOperativa").val();

    var sWhere = "";
    var bFiltrar = false;


    if (sEstatus != '' && sEstatus != undefined && sEstatus != null && sEstatus != 0) {
        bFiltrar = true;
        if (sWhere == '') {
            sWhere = "sIEstatusReferencia = " + sEstatus;
        } else {
            sWhere += " and sIEstatusReferencia = " + sEstatus;
        }
    }

    if (sReferencia != '' && sReferencia != undefined && sReferencia != null && sReferencia != 0) {
        bFiltrar = true;
        if (sWhere == '') {
            sWhere = "sRefOperativa like '" + sReferencia + "'";
        } else {
            sWhere += " and sRefOperativa like '" + sReferencia + "'";
        }
    }

    if (bFiltrar) {
        var sIdUsuario = $("#hlblIdUsuario").html();
        //Se crea arreglo de columnas
        var sColumnas = "sRefAdministrativa,sRefOperativa,sEstatusReferencia,sCambio";
        
        var arrDatos = {
            "sVista": "v_ListaReferenciasSoporte", "sColumnas": sColumnas, "sWhere": sWhere,
            "sOrderBy": "",
            "sIdUsuario": sIdUsuario
        }

        //Se llama función para llenar tabla      
        fn_LlenarTablaReferencia("htblReferencias", arrDatos, sColumnas);
        $("#hdvReferencias").show();
        $("#dlogFiltrarReferencias").modal("hide")
    } else {
        $.desbloquearPantalla();
        $.notificacionMsj(2, "Por favor selecciona al menos un dato para filtrar las referencias.");
    }
    
}


//COSAS NUEVAS






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


function fn_GeneraAutocompleteClientC() {
    $("#htxtCambioCC").autocomplete({
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
            $("#htxtCambioCC").val(ui.item.nombre);
            $("#htxtIdCambioCC").val(ui.item.id);
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

                }else{
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


function fn_GuardarCliente(opcion,idCC,idCO) {
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



function fn_Estatus(idSub, idEstatus) {
    $("#htxtIdReferencia").val(idSub);
    $("#htxtIdEstatus").val(idEstatus);
    
    $("#dialogModificarEstatus").modal("show");

}


function CambiarEstatus() {

    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");

    //Se obtiene datos para la actualizacion.
    var sIdUsuario = $("#hlblIdUsuario").html();

    var sIdSubReferencia = $("#htxtIdReferencia").val();
    var sNuevoEstatus = $("#htxtIdCambioEstatus").val();

    //Se crea variable para almacenar datos
    var sDatos = "{sIdUsuario: '" + sIdUsuario + "', sIdSubReferencia:" + sIdSubReferencia + ", sIdEstatus:'" + sNuevoEstatus + "'} ";
    $.ajax({
        url: "soporte_referencia.aspx/fn_CambiarEstatusRef",
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