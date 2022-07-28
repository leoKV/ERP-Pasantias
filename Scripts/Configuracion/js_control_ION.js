
var datosRefI;
$(document).ready(function () {
    //se oculta div
    $("#hdvClienteSecundarioAduana").hide();



    //$("#btnCargarCSF_PDF").on("click", fn_CargarArchivo);
    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);
    drawChart();
});

function fn_MostrarModal() {
    $("#dialogCargaPDF").modal("show");
}


//Función para generar autocomplete de Estado 
function fn_GenerarAutocompleteCiudad() {
    var sIdUsuario = $("#hlblIdUsuario").html();
    $("#htxtCiudad").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "alta_cliente.aspx/fn_GenerarAutocompleteCiudad",
                data: "{ 'term': '" + request.term + "', sIdUsuario: '" + sIdUsuario + "' }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    $("#hlblIdCiudad").html("");
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
        minLength: 3,
        select: function (event, ui, ID) {
            var id = ui.item.id;
            $("#htxtCiudad").val(ui.item.nombre);
            $("#hlblIdCiudad").html(id);

        }
    });
}

//Función para llenar la tabla cliente secundario
function fn_LlenarTablaClienteSecundario() {
    //Se crea arreglo de columnas
    var sColumnas = "sNomCliente,sRfc,sNomTipoCarga,sEliminar";
    //Se crea arreglo de datos
    var arrDatos = { "sVista": "v_ListaClienteSecundario", "sColumnas": sColumnas, "sWhere": "idCliente = " + fn_Decode($.trim($("#hlblIdCliente").html())) + "", "sOrderBy": "" }
    //Se llama función para llenar tabla
    fn_LlenarTabla("htblClienteSecundario", arrDatos, sColumnas);
}



function drawChart() {
    var dates;
    //$.ajax({
    //    url: "control_ION.aspx/obtenerDatosReferenciaION",
    //    type: "POST",
    //    //dataType: "JSON",
    //    //data: sDatos,
    //    contentType: "application/json; charset=utf-8",
    //    success: function (data) {
    //        //actualiza combo
    //        //fn_GenerarComboClienteSecundario();
    //        //alert(data.d);

    //        datosRefI = data.d;
          
    //        //alert();
    //        ////actualiza tabla
    //        //fn_LlenarTablaClienteSecundario()
    //        ////Cierra dialog
    //        //$("#dialogQuitar").modal("toggle");
    //        ////Se desbloquea la pantalla
    //        ////$.desbloquearPantalla();
    //        ////Se verifica el resultado
    //        //if (data.d.iResultado == 1)
    //        //    //Se muestra notificación de error
    //        //    $.notificacionMsj(1, data.d.sMensaje);
    //        //else
    //        //    //Se muestra notificación de error
    //        //    $.notificacionMsj(2, data.d.sMensaje);
    //    }, error: function (xhr, status, error) {
    //        //var err = eval("(" + xhr.responseText + ")");
    //        //Se desbloquea la pantalla
    //        //$.desbloquearPantalla();
    //        //Se muestra notificación de error
    //        //$.notificacionMsj(3, err.Message);
    //        alert('Errors');
    //    }
    //});

    //if (datosRefI === undefined) {
        
    //} else {
        //alert(datosRefI);



        //var data = google.visualization.DataTable();
        var data = new google.visualization.arrayToDataTable('<%=obtenerDatosReferenciaION%>');
        //  //data.addRows(datosRefI);

    //    //var datosR = google.visualization.arrayToDataTable(data);
    //        var datosR = google.visualization.arrayToDataTable([


    //    ['Referencias', 'Estatus ION_FLAG'],
    //    ['ION FLAG 1', 11],
    //    ['ION FLAG 2', 2],
    //    ['ION FLAG 3', 2],
    //    ['ION FLAG 4', 2],
    //    ['ION FLAG 0', 7]

    //    //['Referencias', 'Estatus ION_FLAG'], ['ION FLAG 1', 11], ['ION FLAG 2', 2], ['ION FLAG 3', 2], ['ION FLAG 4', 2], ['ION FLAG 0', 7]
    //    //dates.split('C');
    //]);
        var options = {
            title: 'Grafica',
            backgroundColor: { fill: 'transparent' }
        };

        var chart = new google.visualization.PieChart(document.getElementById('piechart'));

        chart.draw(data, options);
    //}
    //alert(length(dates));
    //for (i = 0; i < dates.length; i++) {
    //    for (O = 0; O < dates.length; O++) {
    //        alert(dates[i][O]);
    //    }
    //}
    //var tabla = new Array();

    //var datosR = google.visualization.arrayToDataTable(dates);
    //var datosR = google.visualization.arrayToDataTable([

  
    //    ['Referencias', 'Estatus ION_FLAG'],
    //    ['ION FLAG 1', 11],
    //    ['ION FLAG 2', 2],
    //    ['ION FLAG 3', 2],
    //    ['ION FLAG 4', 2],
    //    ['ION FLAG 0', 7]
       
    //    //['Referencias', 'Estatus ION_FLAG'], ['ION FLAG 1', 11], ['ION FLAG 2', 2], ['ION FLAG 3', 2], ['ION FLAG 4', 2], ['ION FLAG 0', 7]
    //    //dates.split('C');
    //]);
    //var data = google.visualization.arrayToDataTable(<%=obtenerDatosReferenciaION%>);


  
}
