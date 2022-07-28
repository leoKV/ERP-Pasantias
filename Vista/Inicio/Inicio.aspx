<%@ Page Title="Inicio" Language="C#" AutoEventWireup="true" CodeFile="Inicio.aspx.cs" Inherits="Vista_Inicio_Inicio" MasterPageFile="~/Vista/Diseno/MasterPage.master" EnableEventValidation="true" %>

<%@ Register Src="~/Vista/Diseno/BrearCrumbs.ascx" TagName="breadCrumbs" TagPrefix="bread" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../Scripts/Inicio/js_inicio.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contend" Runat="Server">
     <!-- Inicio identificadores -->
    <label class="hidden" id="hlblIdUsuario" runat="server"></label>
    <label class="hidden" id="hlblIdMenu" runat="server"></label>
    <label class="hidden" id="hlblIdUsuarioPantalla" runat="server"></label>
    <!-- Fin identificadores -->
    <!-- Inicio breadcrums -->
    <bread:breadCrumbs ID="breadCrum" runat="server" />
    <!-- Fin breadcrums -->
    <!-- Inicio form default -->
    <form id="hfrmDefault"></form>
    <!-- Fin form default -->

    <asp:ScriptManager ID="hManager" runat="server"/>
            <asp:Timer ID="Timer1" runat="server" Interval="3000000" ontick="fn_EnviarCorreoAnticipo"></asp:Timer>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server"></asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="dialog" Runat="Server">
    <!-- Inicio dialog agregar pantalla frecunente -->
    <div class="modal fade" id="dialogAgregarPantalla" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-md" role="document">
            <div class="modal-content">
                <!-- Inicio encabezado dialog -->
                <div class="modal-header">
                    <h5 class="modal-title"><b>Pantallas frecuentes</b></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <!-- Fin encabezado dialog -->
                <!-- Inicio contenido dialog -->
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <form id="hfrmPantalla" method="post" action="" class="form-group">
                                <div class="col-lg-10">
                                    <div id="hdvComboPantallas" runat="server"></div>
                                </div>
                                <div class="col-lg-2 text-center">
                                    <span class="fa fa-plus-circle fa-green-md" id="hbtnAgregarPantalla"></span>
                                </div>
                            </form>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-12">
                                <div id="hdvTablaPantallas" runat="server"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Fin contenido dialog -->
            </div>
        </div>
    </div>
    <!-- Fin dialog agregar pantalla frecuente -->
    <!-- Inicio dialog eliminar pantalla frecunente -->
    <div class="modal fade" id="dialogEliminarPantallaInicio" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-md" role="document">
            <div class="modal-content">
                <!-- Inicio encabezado dialog -->
                <div class="modal-header">
                    <h5 class="modal-title"><b>Confirmar eliminar pantalla</b></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <!-- Fin encabezado dialog -->
                <!-- Inicio contenido dialog -->
                <div class="modal-body">
                    <label>¿Está seguro de eliminar la pantalla del usuario?</label>
                </div>
                <!-- Fin contenido dialog -->
                <!-- Inicio footer dialog -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-greenS btn-sm input-sm" onclick="javascript:fn_EliminarUsuarioPantalla()">Aceptar</button>
                </div>
                <!-- Fin footer dialog -->
            </div>
        </div>
    </div>
    <!-- Fin dialog eliminar pantalla frecuente -->
    <!-- Inicio dialog alerta sin acceso menú -->
    <div class="modal fade" id="dialogAlertaSinAccesoMenu" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <!-- Inicio encabezado dialog -->
                <div class="modal-header">
                    <h5 class="modal-title"><b>Alerta</b></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <!-- Fin encabezado dialog -->
                <!-- Inicio contenido dialog -->
                <div class="modal-body">
                    <label>Usted no tiene acceso al módulo: <b id="hlblNombreMenu" style="color: #90C63D"></b></label>
                </div>
                <!-- Fin contenido dialog -->
            </div>
        </div>
    </div>
    <!-- Fin dialog alerta sin acceso menú -->
</asp:Content>