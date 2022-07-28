<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Diseno/MasterPage.master" AutoEventWireup="true" CodeFile="CargaGenericaImpuesto.aspx.cs" Inherits="Vista_Tools_CargaGenericaImpuesto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%@ Register Src="~/Vista/Diseno/BrearCrumbs.ascx" TagName="breadCrumbs" TagPrefix="bread" %>
    <script src="../../Scripts/Tools/js_Carga_generica_impuesto.js?v=2"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="contend" Runat="Server">
    <bread:breadCrumbs ID="breadCrum" runat="server" />
    <!-- Inicio bóton para regresar a listado de tarifa -->
    <div class="container-fluid">
        <div class="row panel panel-default" id="hpnlEncabezado" runat="server">
            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12 panel-body text-left">
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 panel-body text-right">
                <button type="button" id="hbtnCargar" class="btn btn-success btn-sm input-sm"><span class="fa fa-plus"></span> Carga masiva</button>
            </div>
        </div>
    </div>
    <!-- Fin bóton para regresar a listado de tarifa -->
    <div class="container-fluid">
        <input type="file" id="hInput" class="file-input hide" />
        <div class="row">
            <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                <label class="form-label">Archivo a cargar:</label>

            </div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                <label id="hlblNombreArchivo" class="file-input"></label>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="progress">
                  <div id="hdvProgreso" class="progress-bar" role="progressbar" style="width: 0%;" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100">0%</div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                <div id="hdvErrores" class="text-info hide">
                    Resultado: 
                </div>
            </div>
        </div>
    </div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="dialog" Runat="Server">
</asp:Content>


