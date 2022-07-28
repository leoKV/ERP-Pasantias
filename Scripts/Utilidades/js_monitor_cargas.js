    $(document).ready(function () {
    //Se llena la tabla
    fn_LlenarTablaBitacoraCargas();
    fn_LlenarTablaBitacoraCargasCP();
    fn_LlenarTablaBitacoraCargasNC();
});

//Función para llenar la tabla de la bitacora
function fn_LlenarTablaBitacoraCargas() {
    //Se crea arreglo de columnas
    var sColumnas = "sEstatus,sNoFactura,sUuid,sRemitente,sFecha,sDetalle";
    //Se crea arreglo de datos
    var arrDatos = { "sVista": "v_BitacoraCarga", "sColumnas": sColumnas, "sWhere": "sTipo='FACTURA'", "sOrderBy": "sFecha DESC" }
    //Se llama función para llenar tabla
    fn_LlenarTabla("htblBitacoraCarga", arrDatos, sColumnas);
}

//Función para llenar la tabla de la bitacora
function fn_LlenarTablaBitacoraCargasCP() {
    //Se crea arreglo de columnas
    var sColumnas = "sEstatus,sNoFactura,sUuid,sRemitente,sFecha,sDetalle";
    //Se crea arreglo de datos
    var arrDatos = { "sVista": "v_BitacoraCarga", "sColumnas": sColumnas, "sWhere": "sTipo='COMPLEMENTO DE PAGO'", "sOrderBy": "sFecha DESC" }
    //Se llama función para llenar tabla
    fn_LlenarTabla("htblBitacoraCargaCP", arrDatos, sColumnas);
}

//Función para llenar la tabla de la bitacora
function fn_LlenarTablaBitacoraCargasNC() {
    //Se crea arreglo de columnas
    var sColumnas = "sEstatus,sNoFactura,sUuid,sRemitente,sFecha,sDetalle";
    //Se crea arreglo de datos
    var arrDatos = { "sVista": "v_BitacoraCarga", "sColumnas": sColumnas, "sWhere": "sTipo='NOTA DE CRÉDITO'", "sOrderBy": "sFecha DESC" }
    //Se llama función para llenar tabla
    fn_LlenarTabla("htblBitacoraCargaNC", arrDatos, sColumnas);
}

//Función para llenar la tabla detalle de la bitacora (dialog)
function fn_LlenarTablaDetalleBitacora(sIdRegistroBitacora) {
    //Se bloquea la pantalla
    $.bloquearPantalla("Cargando...");
    $.ajax({
        url: "monitor_cargas.aspx/fn_LlenarTablaDetalleBitacora",
        type: "POST",
        dataType: "JSON",
        data: " { sIdRegistroBitacora:'" + sIdRegistroBitacora + "'} ",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Se desbloquea la pantalla
            $.desbloquearPantalla();
            //Se verifica el resultado
            if (data.d != "Error")
                $("#hdvTablaDetalles").html(data.d)
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