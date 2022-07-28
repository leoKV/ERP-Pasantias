<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="Vista_Diseno_Footer" %>
<!-- inicio dialog Confirmar información -->
<div class="modal fade" id="dialogNotificaciones" tabindex="-1" role="dialog" aria-hidden="true">
    <div id="divmodalnotificaciones" class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <!-- Inicio encabezado dialog -->
            <div class="modal-header">
                <h5 class="modal-title"><b>Notificación Gastos Fijos</b></h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <!-- Fin encabezado dialog -->
            <!-- Inicio contenido dialog -->
            <div class="modal-body" style="height: auto; overflow-y: auto">
                <%--<div class="row">--%>
                <div id="hdvGastoFijoNotificaciones" class="table-responsive" runat="server">

                </div>
                <%--</div>--%>
            </div>
            <!-- Fin contenido dialog -->
            <!-- Inicio footer dialog -->
            <div class="modal-footer">
                <button type="button" class="btn btn-sm input-sm" id="hbtnCerrarNotificaciones">Cerrar</button>
            </div>
            <!-- Fin footer dialog -->
        </div>
    </div>
</div>
<!-- fin dialog Confirmar información -->
<!-- inicio dialog Confirmar información -->
<div class="modal fade" id="dialogOrdenesNotificaciones" tabindex="-1" role="dialog" aria-hidden="true">
    <div id="divmodalordenes" class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <!-- Inicio encabezado dialog -->
            <div class="modal-header">
                <h5 class="modal-title"><b>Notificación Ordenes de venta</b></h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <!-- Fin encabezado dialog -->
            <!-- Inicio contenido dialog -->
            <div class="modal-body" style="height: auto; overflow-y: auto">
                <%--<div class="row">--%>
                <div id="hdvOrdenesNotificaciones" class="table-responsive" runat="server">

                </div>
                <%--</div>--%>
            </div>
            <!-- Fin contenido dialog -->
            <!-- Inicio footer dialog -->
            <div class="modal-footer">
                <button type="button" class="btn btn-sm input-sm" data-dismiss="modal">Cerrar</button>
            </div>
            <!-- Fin footer dialog -->
        </div>
    </div>
</div>
<!-- fin dialog Confirmar información -->

<!-- inicio dialog Confirmar información -->
<div class="modal fade" id="dialogFacturasIntegrandoTerceros" tabindex="-1" role="dialog" aria-hidden="true">
    <div id="divmodalfacturaintegrandoterceros" class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <!-- Inicio encabezado dialog -->
            <div class="modal-header">
                <h5 class="modal-title"><b>Integracion de Facturas</b></h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <!-- Fin encabezado dialog -->
            <!-- Inicio contenido dialog -->
            <div class="modal-body" style="height: auto; overflow-y: auto">
                <%--<div class="row">--%>
                <div id="hdvFacturasTercerosIntegrandoce" class="table-responsive" runat="server">

                </div>
                <%--</div>--%>
            </div>
            <!-- Fin contenido dialog -->
            <!-- Inicio footer dialog -->
            <div class="modal-footer">
                <button type="button" class="btn btn-sm input-sm" data-dismiss="modal">Cerrar</button>
            </div>
            <!-- Fin footer dialog -->
        </div>
    </div>
</div>
<!-- fin dialog Confirmar información -->

<!-- inicio dialog Confirmar información -->
<div class="modal fade" id="dialogOVIntegrando" tabindex="-1" role="dialog" aria-hidden="true">
    <div id="divmodalOVintegrando" class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <!-- Inicio encabezado dialog -->
            <div class="modal-header">
                <h5 class="modal-title"><b>Integracion de Ordenes Venta</b></h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <!-- Fin encabezado dialog -->
            <!-- Inicio contenido dialog -->
            <div class="modal-body" style="height: auto; overflow-y: auto">
                <%--<div class="row">--%>
                <div id="hdvOVIntegrandoce" class="table-responsive" runat="server">

                </div>
                <%--</div>--%>
            </div>
            <!-- Fin contenido dialog -->
            <!-- Inicio footer dialog -->
            <div class="modal-footer">
                <button type="button" class="btn btn-sm input-sm" data-dismiss="modal">Cerrar</button>
            </div>
            <!-- Fin footer dialog -->
        </div>
    </div>
</div>
<!-- fin dialog Confirmar información -->


<!-- inicio dialog Ordenes Venta día -->
<div class="modal fade" id="dialogOVDia" tabindex="-1" role="dialog" aria-hidden="true">
    <div id="divmodalOVDia" class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <!-- Inicio encabezado dialog -->
            <div class="modal-header">
                <h5 class="modal-title"><b>Ordenes de Venta del día</b></h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <!-- Fin encabezado dialog -->
            <!-- Inicio contenido dialog -->
            <div class="modal-body" style="height: auto; overflow-y: auto">
                <%--<div class="row">--%>
                <div id="hdvOVDia" class="table-responsive" runat="server">

                </div>
                <%--</div>--%>
            </div>
            <!-- Fin contenido dialog -->
            <!-- Inicio footer dialog -->
            <div class="modal-footer">
                <button type="button" class="btn btn-sm input-sm" data-dismiss="modal">Cerrar</button>
            </div>
            <!-- Fin footer dialog -->
        </div>
    </div>
</div>
<!-- fin dialog Confirmar información -->








<!----INICIO FOOTER----->
<div class="container-fluid">
    <div class="row" style="background: #1d6688;">
        <!--LEFT footer-->
        <div class="col-xs-6 footer_left">
            <p>NAD Global <%: DateTime.Now.Year %> . ©Todos los derechos reservados.</p>
        </div>
        <!--RIGHT footer-->
        <div class="col-xs-6 text-right text-xs-right footer_right">
            <img alt="" class="" src="../../Styles/Imagenes/img-logonad.png" />
        </div>
    </div>
</div>
<!-----FIN FOOTER----->
