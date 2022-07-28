<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Diseno/MasterPage.master" AutoEventWireup="true" CodeFile="soporte_referencias.aspx.cs" Inherits="Vista_Soportes_Referencia_soporte_referencias" %>
<%@ Register Src="~/Vista/Diseno/BrearCrumbs.ascx" TagName="breadCrumbs" TagPrefix="bread" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!-- HEADER -->
    <script src="../../Scripts/Soportes/Referencia/js_soporte_referencia"></script>
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
    <label class="hidden" id="hlblIdServicioReferencia"></label>
    <label class="hidden" id="hlblIdUsuario" runat="server"></label>
    <label class="hidden" id="hlbliRol" runat="server"></label>
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
                <button type="button" id="hbtnColores" class="btn btn-info btn-sm input-sm"><span class="fa fa-question"></span> Acrónimos</button>
            </div> 
            <div class="col-xs-6 col-xs-offset-6 col-sm-2 col-sm-offset-10 col-md-2 col-md-offset-10 panel-body text-center">
                <button type="button" id="hbtnAbrirDialogoFR" class="btn btn-greenS btn-sm">Filtrar Referencias</button>
            </div> 
         </div>
         <div class="row">
             <div class="col-xs-6 col-xs-offset-6 col-sm-2 col-sm-offset-10 col-md-2 col-md-offset-10 panel-body text-center">
                <button type="button" id="hbtnCerrarR" class="btn btn-primary btn-sm input-sm hidden"><span class="fa fa-window-close"></span> Cerrar Referencias</button>
             </div>
         </div>
      </div>

     <!--fin boton para colores dialog-->

    <!-- Inicio contenedor lista referencias -->
    <div class="container-fluid">
        <!-- Inicio lista referencias -->
        <div class="row">
            <div id="hdvReferencias" class="table-responsive" runat="server"></div>
        </div>
        <!-- Inicio lista referencias -->
    </div>
    <!-- Fin contenedor lista referencias -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="dialog" runat="Server">
    <!-- DIALOGS -->

    <!-- Inicio dialog colores -->
    <div class="modal fade" id="dialogColores" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-md" role="document">
            <div class="modal-content">
                <!-- Inicio encabezado dialog -->
                <div class="modal-header">
                    <h5 class="modal-title"><b>Descripcion Colores</b></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <!-- Fin encabezado dialog -->
                <!-- Inicio contenido dialog -->
                <div class="modal-body">
                    <!-- Inicio lista servicios -->
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12 table-responsive" id="hdvColores" runat="server"></div>
                        </div>
                    </div>
                    <!-- Fin lista servicios -->
                </div>
                <!-- Fin contenido dialog -->
            </div>
        </div>
    </div>
    <!-- fin dialog colores--->

    <!-- Inicio dialog Modificar estatusReferencia -->
    <div class="modal fade" id="dialogModificarEstatus" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <!-- Inicio encabezado dialog -->
                <div class="modal-header">
                    <h5 class="modal-title"><b>Modificar Estatus Referencia</b></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <!-- Fin encabezado dialog -->
                <form id="hfrmModificarEstatus" method="post" action="">
                <!-- Inicio contenido dialog -->
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12 form-group">
                                <label class="form-label">Motivo de modificacion:</label>
                                <input type="text" id="htxtMotivo" name="htxtMotivo" class="input-sm form-control" />
                                <label class="hidden" id="hblNumReferencia" runat="server"></label>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Fin contenido dialog -->
                <!-- Inicio footer dialog -->
                <div class="modal-footer">
                    <button class="btn btn-greenS btn-sm" id="btnConfirmarEnvio" >Aceptar</button>
                    <button class="btn btn-redS btn-sm">Cancelar</button>
                </div>
                <!-- Fin footer dialog -->
                </form>
            </div>
        </div>
    </div>
    <!-- Fin dialog cancelar servicio -->


        <!-- Inicio dialog colores -->
    <div class="modal fade" id="dialogGuias" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-md" role="document">
            <div class="modal-content">
                <!-- Inicio encabezado dialog -->
                <div class="modal-header">
                    <h5 class="modal-title"><b>Guias de la Referencia</b></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <!-- Fin encabezado dialog -->
                <!-- Inicio contenido dialog -->
                <div class="modal-body">
                    <div class="container-fluid fondo-gris-1" style="margin: 10px;">
                        <div class="row">
                            <!-- Inicio número de referencia -->
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <label class="form-label">Referencia Administrativa: </label><br /> <label id="hlblRefAdministrativa"></label>
                            </div>
                            <!-- Fin número de referencia -->
                            <!-- Inicio tipo operación -->
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <label class="form-label">Referencia Operativa: </label><br /> <label id="hlblRefOperativa"></label>
                            </div>
                            <!-- Fin tipo operación -->
                        </div>
                    </div>
                    <!-- Inicio lista servicios -->
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12 table-responsive" id="hdvGuias" runat="server"></div>
                        </div>
                    </div>
                    <!-- Fin lista servicios -->
                </div>
                <!-- Fin contenido dialog -->
            </div>
        </div>
    </div>
    <!-- fin dialog colores--->
    <!-- Inicio dialog iconos -->
        <div class="modal fade" id="dialogIconos" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <!-- Inicio encabezado dialog -->
                    <div class="modal-header">
                        <h5 class="modal-title"><b>Descripcion de Iconos</b></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <!-- Fin encabezado dialog -->
                    <!-- Inicio contenido dialog -->
                    <div class="modal-body">
                        <!-- Inicio lista iconos -->
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-lg-12 table-responsive" id="hdvIconos" runat="server"></div>
                            </div>
                        </div>
                        <!-- Fin lista iconos -->
                    </div>
                    <!-- Fin contenido dialog -->
                </div>
            </div>
        </div>
        <!-- fin dialog iconos--->
    <!-- Inicio dialog  -->
        <div class="modal fade" id="dlogFiltrarReferencias" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <!-- Inicio encabezado dialog -->
                    <div class="modal-header">
                        <h5 class="modal-title"><b>Filtrar Referencias</b></h5>
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
                                        <label class="form-label">Estatus Referencia</label>
                                        <input type="text" id="htxtAutocompleteEstatus" name="htxtAutocompleteEstatus" class="input-sm form-control" />
                                        <label id="hlblIdEstatus" class="form-label hidden"></label>
                                    </div>
                                    <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-5">
                                        <label class="form-label">Referencia Operativa</label>
                                        <input type="text" id="htxtReferenciaOperativa" name="htxtReferenciaOperativa" class="input-sm form-control" />
                                    </div>
                                    <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">      
                                        <br />
                                        <button type="button" class="btn btn-greenS btn-sm" title="Genera Reporte" id="hbtnFiltrarReferencia"><span class="glyphicon glyphicon-floppy-disk"></span>&nbsp;Filtrar Referencia</button>
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

</asp:Content>
