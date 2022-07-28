<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Diseno/MasterPage.master" AutoEventWireup="true" CodeFile="soporte_ST.aspx.cs" Inherits="Vista_Soportes_soporte_ST" %>
<%@ Register Src="~/Vista/Diseno/BrearCrumbs.ascx" TagName="breadCrumbs" TagPrefix="bread" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <!-- HEADER -->
    <script src="../../Scripts/Soportes/js_solicitud_transferencia.js"></script>
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
                <button type="button" id="hbtnColores" class="btn btn-info btn-sm input-sm"><span class="fa fa-question"></span> Acrónimos</button>
            </div> 
            <div class="col-xs-6 col-xs-offset-6 col-sm-2 col-sm-offset-10 col-md-2 col-md-offset-10 panel-body text-center">
                <button type="button" id="hbtnAbrirST" class="btn btn-greenS btn-sm">Filtrar Solicitud Transferencia</button>
            </div> 
         </div>
         <div class="row">
             <div class="col-xs-6 col-xs-offset-6 col-sm-2 col-sm-offset-10 col-md-2 col-md-offset-10 panel-body text-center">
                <button type="button" id="hbtnCerrarST" class="btn btn-primary btn-sm input-sm hidden"><span class="fa fa-window-close"></span> Cerrar Solicitud Transferencia</button>
             </div>
         </div>
      </div>

     <!--fin boton para colores dialog-->
    <!-- Inicio contenedor lista ST -->
    <div class="container-fluid">
        <!-- Inicio lista AT -->
        <div class="row">
            <div id="hdvST" class="table-responsive" runat="server"></div>
        </div>
        <!-- Inicio lista ST -->
    </div>
    <!-- Fin contenedor lista ST -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="dialog" runat="Server">

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

        <!-- Inicio dialog colores -->
    <div class="modal fade" id="dialogGuias" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-md" role="document">
            <div class="modal-content">
                <!-- Inicio encabezado dialog -->
                <div class="modal-header">
                    <h5 class="modal-title"><b>Solicitud de Transferencia</b></h5>
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
                                <label class="form-label"> </label><br /> <label id="hlblRefAdministrativa"></label>
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
        <div class="modal fade" id="dlogFiltrarST" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <!-- Inicio encabezado dialog -->
                    <div class="modal-header">
                        <h5 class="modal-title"><b>Filtrar Solicitud Transferencia </b></h5>
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
                                        <label class="form-label">Estatus ST</label>
                                        <input type="text" id="htxtAutocompleteEstatus" name="htxtAutocompleteEstatus" class="input-sm form-control" />
                                        <label id="hlblIdEstatus" class="form-label hidden"></label>
                                    </div>
                                    <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-5">
                                        <label class="form-label">Solicitud Transferencia</label>
                                        <input type="text" id="htxtFiltroST" name="htxtFiltroST" class="input-sm form-control" />
                                    </div>
                                    <div class="form-group col-xs-12 col-sm-12 col-md-6 col-lg-3">      
                                        <br />
                                        <button type="button" class="btn btn-greenS btn-sm" title="Filtrar Solcitud" id="hbtnFiltrarST"><span class="glyphicon glyphicon-floppy-disk"></span>&nbsp;Filtrar Solicitud Transferencia</button>
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



    <!-- Inicio dialog Modificar fn_ -->
    <div class="modal fade" id="dialogModificarProveedorST" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <!-- Inicio encabezado dialog -->
                <div class="modal-header">
                    <h5 class="modal-title"><b>Modificar Razón Social</b></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                
                <!-- Inicio contenido dialog -->
                <div class="modal-body">
                    <div class="container-fluid">
                        
                        <div class="row">
                            
                            <label id ="hlblProvActual"></label>
                            <br>
                            <%--<label id ="hlblClienteOC"></label>--%>
                        </div>

                        <div class="row">
                            <label for="htxtCambioProvST" id="lblCambioProvST">Razón Social</label>
                            <input id="htxtCambioProvST" type="text" name="htxtCambioProvST" class="input-sm form-control"/>
                            <label id="lblIdCambioProvST" class="form-label hidden"></label>
                            <br><br>
                        </div>

                    </div>
                </div>
                <!-- Fin contenido dialog -->
                <!-- Inicio footer dialog -->
                <div class="modal-footer">
                    <button class="btn btn-greenS btn-sm" id="btnCambiarProvST" >Guardar Cambio.</button>
                    <button class="btn btn-redS btn-sm">Cancelar</button>
                </div>
                <!-- Fin footer dialog -->
                
                <!-- </form> -->
            </div>
        </div>
    </div>
    <!-- Fin dialog cancelar servicio -->

        <!-- Inicio dialog Modificar fn_ -->
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
                <!-- <form id="hfrmEstatusReferencia" method="post" action=""> -->
                
                <!-- Inicio contenido dialog -->
                <div class="modal-body">
                    <div class="container-fluid">
                        
                        <div class="row">
                            
                           <input id="htxtIdEstatus" class="input-sm hidden" />
                            <input id="htxtIdSTEstatus" class="input-sm hidden" />

                        </div>

                        <div class="row">
                            <label for="htxtCambioEstatus" id="lblCambioEstatus">Estatus Nuevo:</label>
                            <input id="htxtCambioEstatus" type="text" name="htxtCambioEstatus" class="input-sm form-control"/>
                            <label id="hlblIdCambioEstatus" class="form-label hidden"></label>
                            
                        </div>

                    </div>
                </div>
                <!-- Fin contenido dialog -->
                <!-- Inicio footer dialog -->
                <div class="modal-footer">
                    <button class="btn btn-greenS btn-sm" id="btnCambiarEstatus" >Guardar Cambio.</button>
                    <button class="btn btn-redS btn-sm">Cancelar</button>
                </div>
                <!-- Fin footer dialog -->
                
                <!-- </form> -->
            </div>
        </div>
    </div>
    <!-- Fin dialog estatus-->

     <!-- Inicio dialog Eliminar fn_EliminarST -->
    <div class="modal fade" id="dialogQuitarST" tabindex="-1" role="dialog" aria-hidden="true">
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
                    <label>Esta accion eliminara la Solicitud de Transferencia ¿Está seguro de querer continuar?</label>
                </div>
                <!-- Fin contenido dialog -->
                <!-- Inicio footer dialog -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal" id="hbtnCancelar">Cancelar</button>
                    <button type="button" class="btn btn-greenS btn-sm" id="hbtnConfirmarST">Aceptar</button>

                </div>
                <!-- Fin footer dialog -->
            </div>
        </div>
    </div>

     <!-- Inicio dialog Eliminar Saldo a Favor ST fn_EliminarSaldoST -->
    <div class="modal fade" id="dialogQuitarSaldoST" tabindex="-1" role="dialog" aria-hidden="true">
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
                    <label>Esta accion eliminara el saldo a favor de la solicitud de transferencia ¿Está seguro de querer continuar?</label>
                </div>
                <!-- Fin contenido dialog -->
                <!-- Inicio footer dialog -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal" id="hbtnCancelarSaldoST">Cancelar</button>
                    <button type="button" class="btn btn-greenS btn-sm" id="hbtnConfirmarSaldoST">Aceptar</button>

                </div>
                <!-- Fin footer dialog -->
            </div>
        </div>
    </div>
    <!-- Inicio dialog Saldo aplicado -->
    <div class="modal fade" id="dialogSaldoAplicado" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document" style="width: 90% !important;">
            <div class="modal-content">
                <!-- Inicio encabezado dialog -->
                <div class="modal-header">
                    <h5 class="modal-title"><b>Detalle Saldo a Favor aplicado</b></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <!-- Fin encabezado dialog -->
                <!-- Inicio contenido dialog -->
                <div class="modal-body" style="height: 450px; overflow-y:auto">
                    <div class="container-fluid">
                        <input id="htxtIdSTSaldo" class="input-sm hidden" />
                    </div>
                    <!-- Lista de facturas -->
                    <div id="hdvSaldoAplicado" class="table-responsive" runat="server"></div>
                </div>
                <!-- Fin contenido dialog -->
            </div>
        </div>
    </div>
    <!-- Fin dialog Saldo aplicado  -->

</asp:Content>


