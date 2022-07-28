<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Diseno/MasterPage.master" AutoEventWireup="true" CodeFile="soporte_facturas.aspx.cs" Inherits="Vista_Soportes_soporte_facturas" %>
<%@ Register Src="~/Vista/Diseno/BrearCrumbs.ascx" TagName="breadCrumbs" TagPrefix="bread" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
            <!-- HEADER -->
    <script src="../../Scripts/Soportes/js_soporte_factura.js"></script>
    <script src="../../Scripts/Sistema/jquery-ui.js" type="text/javascript"></script>
    <link href="../../Styles/Sistema/Bootsrap/css/fileinput.css" rel="stylesheet">
    <script src="../../Styles/Sistema/Bootsrap/js/fileinput.min.js"></script>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="contend" Runat="Server">
      <!-- CONTENIDO -->
    <!-- Inicio breadcrums -->
    <bread:breadCrumbs ID="breadCrum" runat="server" />
    <!-- Fin breadcrums -->
    <!-- Inicio identificadores -->

    <!--<label class="hidden" id="hlblIdSubReferencia"></label>-->
    <%--<label class="hidden" id="hlblIdServicioReferencia"></label>--%>
    <label class="hidden" id="hlblIdUsuario" runat="server"></label>
    <label class="hidden" id="hlbliRol" runat="server"></label>
    <label class="hidden" id="hlblGenidRef" runat="server"></label>
    <asp:HiddenField ID="hlblTipoAcceso" runat="server" />
    <label class="hidden" id="hlbliListado" runat="server"></label>

    <!-- Fin identificadores -->
    <!-- Inicio form default -->
    <form id="hfrmDefault"></form>
    <!-- Fin form default -->
    <!-- Encabezado referencia -->
    <div class="container-fluid">
        <div class="row panel panel-default" id="hpnlEncabezado" runat="server">
            
        </div>
        <!-- Fin bóton para regresar a listado de Comitentes -->
    </div>
    <!--inicio boton  para colores dialog-->
     <div class="container-fluid">
        <div class="row">
            <div class="col-xs-6 col-xs-offset-6 col-sm-2 col-sm-offset-10 col-md-2 col-md-offset-10 panel-body text-center">
                <button type="button" id="hbtnColores" class="btn btn-info btn-sm input-sm"><span class="fa fa-question"></span>    Acrónimos   </button>
            </div> 
        
            <div class="col-xs-6 col-xs-offset-6 col-sm-2 col-sm-offset-10 col-md-2 col-md-offset-10 panel-body text-center">
                <button type="button" id="hbtnAbrirFiltroFacturas" class="btn btn-greenS btn-sm">Filtrar Facturas</button>
            </div> 
            
            <div class="col-xs-6 col-xs-offset-6 col-sm-2 col-sm-offset-10 col-md-2 col-md-offset-10 panel-body text-center">
                <button type="button" id="hbtnAbrirVolcadoDatos" class="btn btn-greenS btn-sm" onclick="javascript:fn_AbrirVolcadoDatos();">Carga Masiva</button>
            </div> 
         </div>

       
         <div class="row">
             <div class="col-xs-6 col-xs-offset-6 col-sm-2 col-sm-offset-10 col-md-2 col-md-offset-10 panel-body text-center">
                <button type="button" id="hbtnCerrarFiltroFacturas" class="btn btn-primary btn-sm input-sm hidden"><span class="fa fa-window-close"></span> Cerrar Facturas</button>
             </div>
         </div>
      </div>

     <!--fin boton para colores dialog-->
    <!-- Inicio contenedor lista ST -->
    <div class="container-fluid">
        <!-- Inicio lista AT -->
        <div class="row">
            <div id="hdvFac" class="table-responsive" runat="server"></div>
        </div>
        <!-- Inicio lista ST -->
    </div>
    <!-- Fin contenedor lista ST -->


</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="dialog" Runat="Server">

    <!-- Inicio dialog  -->
        <div class="modal fade" id="dlogFiltrarFactura" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <!-- Inicio encabezado dialog -->
                    <div class="modal-header">
                        <h5 class="modal-title"><b>Filtrar Factura</b></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <!-- Fin encabezado dialog -->
                    <!-- Inicio contenido dialog -->
                    <div class="modal-body">
                        <div class="container-fluid">
                            <div id="divFiltros" class="row">
                                <div class="container-fluid fondo-gris-1">
                                    <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-5">
                                        <label class="form-label">Estatus Factura</label>
                                        <input type="text" id="htxtAutocompleteEstatusFacturaM" name="htxtAutocompleteEstatusFacturaM" class="input-sm form-control" />
                                        <label id="hlblIdEstatusFacturaM" class="form-label hidden"></label>
                                    </div>
                                    <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-5">
                                        <label class="form-label">UUID:</label>
                                        <input type="text" id="htxtFiltroFactura" name="htxtFiltroFactura" class="input-sm form-control" />
                                    </div>
                                    <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">      
                                        <br />
                                        <button type="button" class="btn btn-greenS btn-sm" title="Filtrar Factura" id="hbtnFiltrarFactura"><span class="glyphicon glyphicon-floppy-disk"></span>&nbsp;Filtrar Factura</button>
                                    </div>
                                </div>
                        </div>
                        </div>
                    </div>
                    <!-- Fin contenido dialog -->
                </div>
            </div>
        </div>
        <!-- fin dialog iconos--->







      <!-- Inicio dialog Ajustar Factura fn_SoporteReenvioFactura_1 -->
    <div class="modal fade" id="dialogAjustarSRF" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-md" role="document">
            <div class="modal-content">
                <!-- Inicio encabezado dialog -->
                <div class="modal-header">
                    <h5 class="modal-title"><b>Confirmar</b></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <!-- Fin encabezado dialog -->
                <!-- Inicio contenido dialog -->
                <div class="modal-body">
                   
                    <label>Esta acción ajustara el reenvio de factura ¿Está seguro de querer continuar?</label>
                   
                </div>
                <!-- Fin contenido dialog -->
                <!-- Inicio footer dialog -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal" id="hbtnCancelar">Cancelar</button>
                    <button type="button" class="btn btn-greenS btn-sm" id="hbtnAjustarSRF">Aceptar</button>
                </div>
                <!-- Fin footer dialog -->
            </div>
        </div>
    </div>

   

          <!-- Inicio dialog  hbtnAbrirVolcadoDatos -->
    <div class="modal fade" id="dialogVolcadoDatos" tabindex="-1" role="dialog" aria-hidden="true" method="POST">
        <div class="modal-dialog modal-md" role="document">
            <div class="modal-content">
                <!-- Inicio encabezado dialog -->
                <div class="modal-header">
                    <h5 class="modal-title"><b>Carga de Datos</b></h5>
                    <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <!-- Fin encabezado dialog -->
                <!-- Inicio contenido dialog -->


                <div class="modal-body">
                   <label class="form-label" for="hfileCargarArchivo">Cargar archivo XLSX</label>                  
                   <input type="file" id="hfileCargarArchivo" name="hfileCargarArchivo" class="form-control" accept=".xlsx,.xls" onchange = "return fn_validacionFormato()"/>
                </div>
                <!-- Fin contenido dialog -->
                <!-- Inicio footer dialog -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal" id="hbtnCancelarDatos">Cancelar</button>
                    <button type="submit" class="btn btn-greenS btn-sm" id="hbtnCargarDatos"><span class="glyphicon glyphicon-open-file"></span>&nbsp;Cargar</button>
                </div>
                <!-- Fin footer dialog -->
            </div>
        </div>
    </div>


</asp:Content>

