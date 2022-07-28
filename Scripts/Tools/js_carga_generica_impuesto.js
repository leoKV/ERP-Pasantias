$(document).ready(() => {
    $("#hbtnCargar").on("click", () => {
        $("#hInput").click();
    })
    $("#hInput").on("change", () => {
        fn_CargarArchivo();
    })
});

function fn_CargarArchivo() {
    var hInput = $("#hInput")[0];
    if (hInput.files.length == 1) {
        $("#hlblNombreArchivo").html(hInput.files[0].name);
        // Obtiene el archivo
        var file_xls = $("#hInput")[0].files[0];
        // Parte el nombre del xlsx obteniendo el nombre sin extensión.
        var archivoXLS = $.trim(file_xls.name.substr(0, file_xls.name.lastIndexOf('.')).replace(/[{()}]/g, '')).replace(/\s/g, "_");
        var resXLS = "";
        // Se almacena el xlsx (fisico) en sistema
        resXLS = fn_CargaArchivo($("#hInput"), ".xlsx", "Tools/" + archivoXLS);
        // Guardar file name en un span
        $("#hspnFileName").html(file_xls.name.replace(/[{()}]/g, '').replace(/\s/g, "_"));
        if (resXLS == "1") {
            // El archivo esta cargado, llamamos al metodo para procesarlo
            fn_ProcesarArchivo(file_xls.name.replace(/[{()}]/g, '').replace(/\s/g, "_"));
        } else {
            // Se muestra notificación de error
            $.notificacionMsj(2, "Ocurrió un error al cargar el archivo");
        }
    }
}

function fn_ModificarProgreso(porcentaje) {
    $("#hdvProgreso").attr("style", "width: " + porcentaje + "%;");
    $("#hdvProgreso").html(porcentaje + "%");
}

function fn_ProgresoFallido(porcentaje) {
    $("#hdvProgreso").html("ERROR");
}

function fn_ProcesarArchivo(nombre) {
    fn_ModificarProgreso(10);

    var sDatos = "{sFileName: '" + nombre + "'}";

    $.ajax({
        url: "CargaGenericaImpuesto.aspx/fn_CargaMasiva",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            // Se muestra el progreso
            fn_ModificarProgreso(20);
            fn_LeerProgreso();
        }, error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            // Se pone en error el progreso
            fn_ProgresoFallido();
            //Se muestra notificación de error
            $.notificacionMsj(3, err.Message);
        }
    });
}

function dosDecimales(n) {
    let t = n.toString();
    let regex = /(\d*.\d{0,2})/;
    return t.match(regex)[0];
}

function fn_LeerProgreso() {
    var sDatos = "{}";

    $.ajax({
        url: "CargaGenericaImpuesto.aspx/fn_LeerProgreso",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            // Se muestra el progreso
            fn_ModificarProgreso(20 + Math.round(data.d.fProgreso));
            if (!data.d.fCompletado) {
                setTimeout(fn_LeerProgreso, 5000);
            } else {
                fn_ModificarProgreso(100);
                if (data.d.lstErrores == null || data.d.lstErrores.length != 0) {
                    for (var index in data.d.lstErrores) {
                        $("#hdvErrores").html($("#hdvErrores").html() + "<br>" + data.d.lstErrores[index]);
                    }


                } else {
                    $("#hdvErrores").html($("#hdvErrores").html() + "Carga completada exitosamente");
                }
                $("#hdvErrores").removeClass("hide");
            }
        }, error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            // Se pone en error el progreso
            fn_ProgresoFallido();
            //Se muestra notificación de error
            $.notificacionMsj(3, err.Message);
        }
    });
}