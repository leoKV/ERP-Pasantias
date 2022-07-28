
//Inicio Document Ready
$(document).ready(function () {
    //se desbloquea la pantalla
    $.desbloquearPantalla();
    //**
    $('#htxtUsuario').val("");
    $('#htxtContrasenia').val("");
    sessionStorage.removeItem('sWhere');
});

//Funcion Para Abrir Dialog de Cambio obligatorio de Contraseña
function fn_abreDialogCambioPass(sMensaje,sTitulo,sOnclik) {
    //se agrega Mensaje
    $("#hdvMensajeCambioPass").html(sMensaje);
    //*********
    //se agrega titulo
    $("#hhTitulo > b").html(sTitulo);
    //********************
    //se agrega formato a boton
    $("#abtnEnviaCambio").attr("onclick", sOnclik);
    //$("#hbtnBotonConfig > b").html("Aceptar");
    //************
    //se abre dialog
    $("#hdvCambioPassword").modal("toggle");
    //****
}
//***************

//FUNCION PARA VALIDAR CAMPOS
function fn_ValidaForm(sContenido, sIdSmallError, sMensajeVacio, sExpresion, idInput, sIcono, sidButton, sRespuesta, sRespuestaConse) {
    //se valida los campos vacios
    if (sContenido.trim() == "") {
        $("#" + idInput).attr("class", "has-error");
        $("#" + sIdSmallError).html(sMensajeVacio);
        $("#" + sIdSmallError).attr("style", "color: #a94442;");
        $("#" + sIcono).attr("class", "glyphicon glyphicon-remove form-control-feedback");
        $("#" + sRespuesta).html("0");
        if ($("#" + sRespuestaConse).html() != "0" && $("#" + sRespuesta).html() != "0") {
            $("#" + sidButton).prop("disabled", false);
        } else {
            $("#" + sidButton).prop("disabled", true);
        }
    } else if (sExpresion != "") {
        var expre = new RegExp(sExpresion);
        if (expre.exec(sContenido)) {
            $("#" + idInput).attr("class", "has-success");
            $("#" + sIdSmallError).html("");
            $("#" + sIcono).attr("class", "glyphicon glyphicon-ok form-control-feedback");
            $("#" + sRespuesta).html("1");

            if ($("#" + sRespuestaConse).html() != "0" && $("#" + sRespuesta).html() != "0") {
                $("#" + sidButton).prop("disabled", false);
            } else {
                $("#" + sidButton).prop("disabled", true);
            }
        } else {
            $("#" + idInput).attr("class", "has-error");
            $("#" + sIdSmallError).html("Formato Invalido");
            $("#" + sIdSmallError).attr("style", "color: #a94442;");
            $("#" + sIcono).attr("class", "glyphicon glyphicon-remove form-control-feedback");
            $("#" + sRespuesta).html("0");
            if ($("#" + sRespuestaConse).html() != "0" && $("#" + sRespuesta).html() != "0") {
                $("#" + sidButton).prop("disabled", false);
            } else {
                $("#" + sidButton).prop("disabled", true);
            }
        }
    }
    else {
        $("#" + idInput).attr("class", "has-success");
        $("#" + sIdSmallError).html("");
        $("#" + sIdSmallError).attr("style", "color: #a94442;");
        $("#" + sIcono).attr("class", "glyphicon glyphicon-ok form-control-feedback");
        $("#" + sRespuesta).html("1");
        if ($("#" + sRespuestaConse).html() != "0" && $("#" + sRespuesta).html() != "0") {
            $("#" + sidButton).prop("disabled", false);
        } else {
            $("#" + sidButton).prop("disabled", true);
        }
    }
}
//**************

//funcion para validar Contraseña de Cambio
function fn_ValidaPassword(sContenido,sIdInput) {
    var sPassword = $("#txtCambioPassword").val();

    if (sContenido != sPassword) {
        $("#" + sIdInput).attr("class", "has-error");
        $("#" + sIdInput + " > i").html("Las contraseñas no concuerdan o esta vacia");
        $("#" + sIdInput + " > i").attr("style", "color: #a94442;");
        $("#" + sIdInput +" > span").attr("class", "glyphicon glyphicon-remove form-control-feedback");
        $("#" + sIdInput + " > small").html("0");


        if ($("#" + sIdInput + " > small").html() != "0" && $("#hsmRespuestaCambioPass").html() != "0") {
            $("#abtnCambioPassSistema").prop("disabled", false);
        } else {
            $("#abtnCambioPassSistema").prop("disabled", true);
        }


    } else {
        $("#" + sIdInput).attr("class", "has-success");
        $("#" + sIdInput + " > i").html("");
        $("#" + sIdInput + " > i").attr("style", "color: #a94442;");
        $("#" + sIdInput + " > span").attr("class", "glyphicon glyphicon-ok form-control-feedback");
        $("#" + sIdInput + " > small").html("1");

        if ($("#" + sIdInput + " > small").html() != "0" && $("#hsmRespuestaCambioPass").html() != "0") {
            $("#abtnCambioPassSistema").prop("disabled", false);
        } else {
            $("#abtnCambioPassSistema").prop("disabled", true);
        }
    }
}
//*****************


function fn_AbrirModales() {
    event.preventDefault();

    Captcha();
    $("#hdvDialogCambioPass").modal("show");

}


function Captcha() {
    var alpha = new Array('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z','1','2','3','4','5','6','7','8','9');
    var i;
    for (i = 0; i < 6; i++) {
        var a = alpha[Math.floor(Math.random() * alpha.length)];
        var b = alpha[Math.floor(Math.random() * alpha.length)];
        var c = alpha[Math.floor(Math.random() * alpha.length)];
        var d = alpha[Math.floor(Math.random() * alpha.length)];
        var e = alpha[Math.floor(Math.random() * alpha.length)];
        var f = alpha[Math.floor(Math.random() * alpha.length)];
        var g = alpha[Math.floor(Math.random() * alpha.length)];
    }
    var code = a + '' + b + '' + '' + c + '' + d + '' + e + '' + f + '' + g;
    $("#htctOc").val(code);
    $("#htxtCaptcha").val(code);
    
}