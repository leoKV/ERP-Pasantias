$(document).ready(function () {
    //Se llena la tabla
    fn_LlenarTablaBitacoraAccesos();
});

//Función para llenar la tabla de la bitacora
function fn_LlenarTablaBitacoraAccesos() {
    //Se crea arreglo de columnas
    var sColumnas = "sUsuario,sNombrehost,sIpLocal,sIpPublica,sFechaLogin,sFechaLogout,sVerDetalle";
    //Se crea arreglo de datos
    var arrDatos = { "sVista": "v_BitacoraAccesos", "sColumnas": sColumnas, "sWhere": "", "sOrderBy": "sFechaLogin DESC" }
    //Se llama función para llenar tabla
    fn_LlenarTabla("htblBitacoraAccesos", arrDatos, sColumnas);
}

//Función para llenar la tabla detalle de la bitacora (dialog)
function fn_LlenarTablaDetalleBitacora(sIdU,w,x,y,z) {
    //Se crea arreglo de columnas
    var sColumnas = "sUrl,sPaginaVisitada,sFechaVisita";

    //Se genera el codigo completo
    var sIdIp = w + '.' + x + '.' + y + '.' + z


    var sWhere = "sIp = '" + sIdIp + "' AND sUs = " + sIdU;

    //Se crea arreglo de datos
    var arrDatos = { "sVista": "v_DetalleBitacoraAccesos", "sColumnas": sColumnas, "sWhere": sWhere, "sOrderBy": "sFechaVisita DESC" }
    //Se llama función para llenar tabla
    fn_LlenarTabla("htblDetalleBitacora", arrDatos, sColumnas);
    
}