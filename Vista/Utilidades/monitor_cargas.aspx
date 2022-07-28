<%@ Page Title="Monitor" Language="C#" MasterPageFile="~/Vista/Diseno/MasterPage.master" AutoEventWireup="true" CodeFile="monitor_cargas.aspx.cs" Inherits="Vista_monitor_cargas" %>

<%@ Register Src="~/Vista/Diseno/BrearCrumbs.ascx" TagName="breadCrumbs" TagPrefix="bread" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../Scripts/Utilidades/js_monitor_cargas.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contend" Runat="Server">
    <!-- CONTENIDO -->
    <!-- Inicio identificadores -->
    <label class="hidden" id="hlblIdUsuario" runat="server"></label>
    <label class="hidden" id="hlblIdUsuarioAccion" runat="server"></label>
    <!-- Fin identificadores -->
    <!-- Inicio breadcrums -->
    <bread:breadCrumbs ID="breadCrum" runat="server" />
    <!-- Fin breadcrums -->
    <!-- Inicio form default -->
    <form id="hfrmDefault"></form>
    <!-- Fin form default -->

      <!--TABS-->
    <div>
        <div class="panel with-nav-tabs panel-default">
            <!--Inicio Tabs-->
            <div class="panel-heading heading-Azul" style="padding: 5px 15px;">
                <ul class="nav nav-tabs tab-Azul" id="hlstTabs">
                    <!--Gasto Fijo-->
                    <li class="active lstTabC" id="hliFactura"><a id="haFactura" data-toggle="tab" href="#hdvFactura">Facturas</a></li>
                    <!--Fin Gasto Fijo -->
                    <!--Órdenes de Compra-->
                    <li id="hliComplementoP" class="lstTab"><a id="haComplementoP" data-toggle="tab" href="#hdvComplementoP">Complementos de Pago</a></li>
                    <!--Fin Órdenes de Compra-->
                    <!--Órdenes de Compra-->
                    <li id="hliNotaCredito" class="lstTab"><a id="haNotaCredito" data-toggle="tab" href="#hdvNotaCredito">Notas de crédito</a></li>
                    <!--Fin Órdenes de Compra-->
                </ul>
            </div>
            <!--***************************************-->
            <!--Inicio Contenidos de Tabs-->
            <!--DIV General-->
            <div class="panel-body">
                <div id="hdvContenedorGeneral" class="tab-content">
                    <!--Inicio Contenido de TAB Gasto Fijo-->
                    <div id="hdvFactura" class="tab-pane fade active in">
                        <div class="row">
                            <!-- Inicio contenedor lista gasto fijo-->
                            <div class="container-fluid">
                                <!-- Inicio lista gasto fijo-->
                                <div class="row">
                                    <div id="hlblBitacora" class="table-responsive" runat="server">
                                    </div>
                                </div>
                                <!-- Inicio lista gasto fijo-->
                            </div>
                        </div>
                    </div>
                    <!--Fin de contenido TAB Gasto Fijo-->
                    <!--Inicio contenido  de TAB Órdenes de Compra-->
                    <div id="hdvComplementoP" class="container-fluid tab-pane fade">
                        <div class="row">
                            <!-- Inicio contenedor lista gasto fijo-->
                            <div class="container-fluid">
                                <!-- Inicio lista órdenes de compra-->
                                <div class="row">
                                    <div id="hlblBitacoraCP" class="table-responsive" runat="server">
                                    </div>
                                </div>
                                <!-- Inicio lista órdenes de compra-->
                            </div>
                        </div>
                    </div>
                    <!--Fin contenido  de TAB Ordenes de Compra-->
                    <!--Inicio contenido  de TAB Factura directa-->
                    <div id="hdvNotaCredito" class="container-fluid tab-pane fade">
                        <div class="row">
                            <!-- Inicio contenedor lista Factura directa--->
                            <div class="container-fluid">
                                <!-- Inicio lista Factura directa--->
                                <div class="row">
                                    <div id="hlblBitacoraNC" class="table-responsive" runat="server">
                                    </div>
                                </div>
                                <!-- Inicio lista Factura directa--->
                            </div>
                        </div>
                    </div>
                    <!--Fin contenido  de TAB Factura directa--->
                </div>
            </div>
        </div>
    </div>

<%--    <!-- Inicio lista usuario -->
    <div class="container-fluid">
        <div class="row">
            <div class="table table-responsive" id="hlblBitacora" runat="server"></div>
        </div>
    </div>--%>
    <!-- Inicio lista usuario -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="dialog" Runat="Server">
    <!-- Inicio dialog detalle bitacora -->
    <div class="modal fade" id="dialogDetalleBitacora" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <!-- Inicio encabezado dialog -->
                <div class="modal-header">
                    <h5 class="modal-title"><b>Detalle</b></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <!-- Fin encabezado dialog -->
                <!-- Inicio contenido dialog -->
                <div class="modal-body">
                    <!-- Inicio lista detalles -->
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12" id="hdvTablaDetalles" runat="server"></div>
                        </div>
                    </div>
                    <!-- Fin lista detalles -->
                </div>
                <!-- Fin contenido dialog -->
            </div>
        </div>
    </div>
    <!-- Fin dialog detalle bitacora -->
</asp:Content>