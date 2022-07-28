$(document).ready(function () {
    //Función para obtener pantallas de inicio del usuario
    fn_ObtenerPantallasUsuario();
    //Botón para guardar pantalla
    $("#hbtnAgregarPantalla").click(function () {
        //Función para validar formulario pantalla
        $("#hfrmPantalla").data("bootstrapValidator").validate();
    });
    //Función para validar pantalla inicio
    fn_ValidadorPantalla();
    //Función para mstrar alerta SIN ACCESO MENÚ
    $(function () {
        //Se obtiene id de menú encritado
        var sIdMenu = $("#hlblIdMenu").html();
        //Se verifica id de menú diferente a 0
        if (parseInt(fn_Decode(sIdMenu), 10) != 0)
            //Se muestra alerta de sin acceso menú
            fn_AlertaSinAccesoMenu(sIdMenu);
    });
});

//Función para obtener las pantallas del usuario
function fn_ObtenerPantallasUsuario() {
    //Se obtienen los datos
    var sIdUsuario = $.trim($("#hlblIdUsuario").html());
    //Se crea variable para almacenar datos
    var sDatos = " { sIdUsuario:'" + sIdUsuario + "'} ";
    $.ajax({
        url: "Inicio.aspx/fn_ObtenerPantallasUsuario",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Se llena el contenido de pantallas usuario
            $("#hdvInicio").html(data.d.sContenido);
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

//Función para obtener pantallas
function fn_ObtenerListaPantallas() {
    $("#dialogAgregarPantalla").modal("show");
    fn_LlenarTablaPantallas();
}

//Función para llenar la tabla de pantallas
function fn_LlenarTablaPantallas() {
    //Se crea arreglo de columnas
    var sColumnas = "sMenu,sSubMenu,sEliminar";
    //Se crea arreglo de datos
    var arrDatos = { "sVista": "v_ListaUsuarioPantalla", "sColumnas": sColumnas, "sWhere": " idUsuario=" + fn_Decode($("#hlblIdUsuario").html()), "sOrderBy": "" }
    //Se llama función para llenar tabla
    fn_LlenarTabla("htblPantallas", arrDatos, sColumnas);
}

//Función para generar validador pantalla
function fn_ValidadorPantalla() {
    //INICIO VALIDACIONES DATOS
    $("#hfrmPantalla").bootstrapValidator({
        message: 'El valor es inválido.',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            //Se valida campo menu
            hslcMenus: {
                validators: {
                    //Se valida que el campo no este vacío
                    notEmpty: {
                        message: 'Debe seleccionar un menú.'
                    }
                }
            }
        },
        onError: function () {
            //Se muestra notificación de error
            $.notificacionMsj(2, "Existen campos que no cumplen la validación.");
        }
    }).on('success.form.bv', function (e) {
        //Se valida que la pantalla no este asignada al usuario
        fn_ValidarUsuarioPantallaExiste();
    });
    //FIN VALIDACIONES DATOS
}

//Función para validar que la pantalla no este asignada al usuario
function fn_ValidarUsuarioPantallaExiste() {
    //Se obtienen los datos
    var sIdUsuario = $.trim($("#hlblIdUsuario").html());
    var iIdMenu = $.trim($("#hslcMenus option:selected").val());
    //Se crea variable para almacenar datos
    var sDatos = " { sIdUsuario:'" + sIdUsuario + "',iIdMenu:" + iIdMenu + " } ";
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "Inicio.aspx/fn_ValidarUsuarioPantallaExiste",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Se verifica el resultado
            if (data.d.iResultado == 0)
                //Se guarda el cliente
                fn_GuardarUsuarioPantalla(sIdUsuario, iIdMenu);
            else {
                //Se desbloquea la pantalla
                $.desbloquearPantalla();
                //Se muestra notificación de error
                $.notificacionMsj(2, data.d.sMensaje);
            }
        }, error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            //Se muestra notificación de error
            $.notificacionMsj(3, err.Message);
        }
    });
}

//Función para guardar ella pantalla del usuario
function fn_GuardarUsuarioPantalla(sIdUsuario, iIdMenu) {
    //Se crea variable para almacenar datos
    var sDatos = " { sIdUsuario:'" + sIdUsuario + "',iIdMenu:" + iIdMenu + " } ";
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "Inicio.aspx/fn_GuardarUsuarioPantalla",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Se limpian los campos
            fn_LimpiarCampos("hfrmPantalla");
            //Se recargan las pantallas del usuario
            fn_ObtenerPantallasUsuario();
            //Se recarga lista pantallas
            fn_LlenarTablaPantallas();
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            //Se verifica el resultado
            if (data.d.iResultado == 1)
                //Se muestra notificación de error
                $.notificacionMsj(1, data.d.sMensaje);
            else
                //Se muestra notificación de error
                $.notificacionMsj(2, data.d.sMensaje);
        }, error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            //Se muestra notificación de error
            $.notificacionMsj(3, err.Message);
        }
    });
}

//Dialog para confirmar eliminar pantalla inicio
function fn_ConfirmarEliminarUsuarioPantalla(iIdUsuarioPantalla) {
    //Se abre dialog para confirmar eliminar
    $("#dialogEliminarPantallaInicio").modal("show");
    //Se guarda el id del usuario pantalla
    $("#hlblIdUsuarioPantalla").html(iIdUsuarioPantalla);
}

//Función para eliminar la pantalla de inicio
function fn_EliminarUsuarioPantalla() {
    //Se crea variable para almacenar datos
    var sDatos = " { iIdUsuarioPantalla:" + $.trim($("#hlblIdUsuarioPantalla").html()) + " } ";
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "Inicio.aspx/fn_EliminarUsuarioPantalla",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Se recargan las pantallas del usuario
            fn_ObtenerPantallasUsuario();
            //Se recarga lista pantallas
            fn_LlenarTablaPantallas();
            //Se cierra dialog para confirmar eliminar
            $("#dialogEliminarPantallaInicio").modal("hide");
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            //Se verifica el resultado
            if (data.d.iResultado == 1)
                //Se muestra notificación de error
                $.notificacionMsj(1, data.d.sMensaje);
            else
                //Se muestra notificación de error
                $.notificacionMsj(2, data.d.sMensaje);
        }, error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            //Se muestra notificación de error
            $.notificacionMsj(3, err.Message);
        }
    });
}

//Función para mostrar alerta de sin acceso menú
function fn_AlertaSinAccesoMenu(sIdMenu) {
    //Se crea variable para almacenar datos
    var sDatos = " { sIdMenu:'" + sIdMenu + "' } ";
    $.ajax({
        url: "Inicio.aspx/fn_AlertaSinAccesoMenu",
        type: "POST",
        dataType: "JSON",
        data: sDatos,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Se asigna el nombre del menú
            $("#hlblNombreMenu").html(data.d.sMensaje);
            //Se abre el dialog 
            $("#dialogAlertaSinAccesoMenu").modal("show");
        }, error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            //Se muestra notificación de error
            $.notificacionMsj(3, err.Message);
        }
    });
}