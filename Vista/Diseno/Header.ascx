<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="Vista_Prueba_Header" %>
<!-----/////INICIO CONTENIDO GENERAL///////////////////------>
<div id="header_gen_s" class="container-fluid">

    <!----//////////INICIO FORM GROUP DE PERFIL Y LOGO-->
    <div class="head_content form-group">
        <!----INICIO RENGLON GENERAL --->
        <div class="row">

            <!----LOGO--->
            <div class="col-xs-4">
                <%--<img src="../../Styles/Imagenes/Logos Sistemas-04.png" style="height: 60px; position: absolute" />--%>
                <%--<img src="../../Styles/Imagenes/LogotipoAltaResolucion.png" style="height: 60px;position:absolute;margin-top:-15px;" alt="logo">--%>
                <img src="../../Styles/Imagenes/LogotipoNADSI.png" style="height: 60px; position: absolute; margin-top: -15px;" alt="logo">
            </div>
            <!----///FIN LOGO---->

            <!----DATOS USUARIOS -->
            <div class="col-xs-8 input-group text-right">
                <div class="btn-group">
                    <a id="btnNotificaciones"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="position: relative; width: 16px; height: 16px;" runat="server" class="fa fa-bell fa-green-sm" visible="false"><span id="iGastos" style="position: absolute; top: -6px; right: -2px; padding: 1px 2px 1px 2px; background-color: red; color: white; font-weight: bold; font-size: 0.55em; box-shadow: 1px 1px 1px gray; border-radius: 30px;" runat="server" class="label" ></span></a>
                    <span>&nbsp;&nbsp;&nbsp;</span>
                    <!--INICIO DROPDOWN NOTIFICACIONES -->
                    <ul class="dropdown-menu dropdown-menu-right btn-sm" style="cursor: pointer">
                            <li id="hliGastosNotificaciones" runat="server">
                                <a  onclick="javascript:fn_MostrarNotificacionesGastos()"><strong style="color: red;">( <span id="hspGastosNotificaciones" runat="server">0</span> )</strong> Gastos Fijos</a>
                            </li>
                            <li id="hliOrdenesNotificaciones" runat="server">
                                <a  onclick="javascript:fn_MostrarNotificacionesOrdenes()"><strong style="color: red;">( <span id="hspOrdenesNotificaciones" runat="server">0</span> )</strong> Ordenes de Venta</a>
                            </li>
                            <li id="hliOVDia" runat="server">
                                <a  onclick="javascript:fn_MostrarNotificacionesOrdenesDia()"><strong style="color: red;">( <span id="hspOrdenesVentaDia" runat="server">0</span> )</strong>Ordenes del día procesadas hoy.</a>
                            </li>
                            <li id="hliFacturasNotificaciones" runat="server">
                                <a  onclick="javascript:fn_MostrarNotificacionesFacturas()"><strong style="color: red;">( <span id="hspFacturasTerceros" runat="server">0</span> )</strong> Facturas procesándose</a>
                            </li>
                            <li id="hliOrdenesPasandoNotificaciones" runat="server">
                                <a  onclick="javascript:fn_MostrarNotificacionesOrdenesPasando()"><strong style="color: red;">( <span id="hspOrdenesVentaProcesandoce" runat="server">0</span> )</strong> Ordenes de Venta procesándose</a>
                            </li>
                        </ul>
                </div>
                <!-- FIN DROPDOWN NOTIFICACIONES -->

                <!--///INICIO CONTENDIDO DE PERFIL --->
                <div class="btn-group">

                    <!---INICIO BOTON CON DATOS DE USUARIO --->
                    <button type="button" class="btn btn_gray btn-sm dat_user shadow2">
                        <span class="glyphicon glyphicon-user icon_green"></span>
                        <asp:Label runat="server" ID="alblNombreUsuario">
                            José Alberto Daniel Francisco Peréz Dominguéz</asp:Label>
                    </button>
                    <!---FIN BOTON CON DATOS DE USUARIO --->

                    <!---INICIO BOTON OPCIONES USUARIO-->
                    <button type="button" class="btn btn_gray btn-sm dropdown-toggle dat_user_toogle shadow2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <span class="caret"></span><span class="sr-only">Toggle Dropdown</span>
                    </button>

                    <ul class="dropdown-menu dropdown-menu-right btn-sm">
                        <li>
                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">
                                    Ver Perfil
                            </asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">
                                    Ayuda
                            </asp:LinkButton>
                        </li>
                        <li role="separator" class="sep_line divider" style="margin: 0px 0px;"></li>
                        <li>
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                                    Cerrar Sesión
                            </asp:LinkButton>
                        </li>
                    </ul>
                    <!---FIN BOTON OPCIONES USUARIO-->
                </div>
            </div>
            <!--///FIN CONTENDIDO DE PERFIL --->

            <!---///FIN DATOS USUARIO --->

        </div>
        <!----FIN RENGLON GENERAL --->

    </div>
    <!----//////////FIN FORM GROUP DE PERFIL Y LOGO-->

    <div class="row tnav">
        <!--////tnav////-->
        <nav class="navbar navbar-default">
            <%--inicia boton hamburguesa--%>
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar" aria-expanded="true" aria-controls="navbar">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <%--fin boton hamburguesa--%>
            <%--inicia construccion menu--%>
            <div class="container-fluid navbar-inner-sm" id="hdvMenu" runat="server">
            </div>
            <%--fin construccion menu--%>
            <!--/.container-fluid -->
        </nav>
    </div>
    <!--////tnav////-->
    <!-----/////FIN CONTENIDO GENERAL///////////////////------>
</div>


