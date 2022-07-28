$(document).ready(function () {
    //Función para asignar evento a combobox roles
    $("#hslcRoles").change(function () {
        //Función para obtener accesos del rol
        fn_ObtenerAccesosRol();
    });

    ///Función para deplegar/ocultar las opciones del treeview
    $('.hasSub').click(function () {
        $(this).parent().toggleClass('subactivated');
        $(this).parent().children('ul:first').toggle();
        ///Se despliegan las opciones del treeview
        if ($(this).find('i').hasClass('fa-angle-down')) {
            $(this).find('i').removeClass('fa-angle-down').addClass('fa-angle-right');
        } else {
            ///Se ocultan las opciones del treeview
            $(this).find('i').removeClass('fa-angle-right').addClass('fa-angle-down');
        }
    });
    
    //Botón para desplegar o contrer lista opciones accesos
    $('#hbtnExpandir').click(function () {
        ///Se despliegan las opciones del treeview
        if ($("#hbtnExpandir > span").hasClass('fa-plus')) {
            //Se cambia el icono
            $(".hasSub").find('i').removeClass('fa-angle-right').addClass('fa-angle-down');
            //Se despliegan las opciones
            $(".hasSub").parent().children("ul").show();
            $(".hasSub").parent().addClass('subactivated');
            //Se muestran opciones del treeview
            $("#hbtnExpandir > span").removeClass('fa-plus').addClass('fa-minus');
            $("#hbtnExpandir > label").html("Contraer");
        } else {
            //Se cambia el icono
            $(".hasSub").find('i').removeClass('fa-angle-down').addClass('fa-angle-right');
            //Se despliegan las opciones
            $(".hasSub").parent().children("ul").hide();
            $(".hasSub").parent().removeClass('subactivated');
            ///Se ocultan las opciones del treeview
            $("#hbtnExpandir > span").removeClass('fa-minus').addClass('fa-plus');
            $("#hbtnExpandir > label").html("Expandir");
        }
    });
    
});

//Función para obtener accesos del rol
function fn_ObtenerAccesosRol() {
    //Se obtiene id del rol
    var iIdRol = $("#hslcRoles option:selected").val();
    var sIdMenu;
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "accesos.aspx/fn_ObtenerAccesosRol",
        type: "POST",
        dataType: "JSON",
        data: " { iIdRol:" + iIdRol + " } ",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $.each(data.d.arrMenuTipoAcceso, function (key, value) {
                if (key % 2 == 0)
                    sIdMenu = value;
                else
                    $("#" + sIdMenu + "_" + value).prop("checked", true);
            });
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

//Función para cambiar el acceso al menú
function fn_CambiarTipoAccesoMenu(iIdMenu, iIdTipoAcceso) {
    //Se obtiene el rol
    var iIdRol = $("#hslcRoles").val();
    //se verifica que este seleccionado un rol
    if (iIdRol != "") {
        //Se bloquea la pantalla
        $.bloquearPantalla("Cargando...");
        $.ajax({
            url: "accesos.aspx/fn_CambiarTipoAccesoMenu",
            type: "POST",
            dataType: "JSON",
            data: " { iIdRol:" + iIdRol + ", iIdMenu:" + iIdMenu + ", iIdTipoAcceso:" + iIdTipoAcceso + " } ",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                //Se desbloquea la pantalla
                $.desbloquearPantalla();
                //Se verifica resultado
                if (data.d.iResultado == 1) {
                    //Se muestra notificación de éxito
                    $.notificacionMsj(1, data.d.sMensaje);
                } else {
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
    } else {
        //Se desplega combobox
        $("#hslcRoles").selectpicker("toggle");
        //Se muestra notificación de error
        $.notificacionMsj(2, "Debe seleccionar un rol.");
    }
}