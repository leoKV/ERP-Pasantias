<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <title>NAD Smart Invoice</title>
    <!--Estilos-->
    <link href="Styles/Sistema/login.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Sistema/styles.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Sistema/Bootsrap/css/bootstrap.css" rel="stylesheet" />
    <link href="Styles/Sistema/Bootsrap/FontAwesome/css/font-awesome.min.css" rel="stylesheet" />
    <!--*************-->
    <!--Scripts-->

        <!---****************** INICIO CSS **********************--->
    <script src="Scripts/Sistema/jquery-1.10.2.js"></script>
    <script src="Scripts/Sistema/bootstrapValidator.js"></script>
    <script src="Scripts/Sistema/jsblockUI.js"></script>
    <script src="Scripts/Sistema/jsNotificaciones.js"></script>
    <script src="Scripts/Sistema/js_login.js" type="text/javascript"></script>
    <script src="Styles/Sistema/Bootsrap/js/bootstrap.min.js"></script>
    <script src="Scripts/Sistema/jquery.dataTables.min.js"></script>
    <script src="Scripts/Sistema/notify-master/bootstrap-notify.min.js"></script>
    <script src="Scripts/Sistema/js_general.js?v=4"></script>
    <script src="Scripts/Sistema/bootstrap-select.js"></script>
    <script src="Scripts/Sistema/bootstrap-datepicker.js"></script>
    <!---****************** FIN CSS **********************--->
    <!---****************** INICIO JQUERY **********************--->
    <link href="Styles/Sistema/bootstrap-select.css" rel="stylesheet" />
    <link href="Styles/Sistema/jquery.dataTables_themeroller.css" rel="stylesheet" />
    <link href="Styles/Sistema/datepicker.css" rel="stylesheet" />
    <link href="Styles/Sistema/estiloGeneral.css" rel="stylesheet" />
    <link href="Styles/Sistema/fileinput.css" rel="stylesheet" />
    <link href="Styles/Sistema/styles.css" rel="stylesheet" />
    <link href="Styles/Sistema/animate.min.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Sistema/jquery-ui-1.10.4.custom.css" rel="stylesheet" />
    <link href="Styles/Sistema/bootstrap-checkbox.css" rel="stylesheet" />
    <link href="Styles/Sistema/Bootsrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Styles/Sistema/Bootsrap/FontAwesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="Styles/Sistema/cssTreeView.css" rel="stylesheet" />
    <link href="Styles/Sistema/headSistema.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xls/0.7.6/xls.core.min.js"></script>
    <script src="Scripts/Sistema/ex2excel/src/jquery.table2excel.js"></script>
    <!---****************** FIN JQUERY **********************--->


    <script src="Scripts/Sistema/bootstrapValidator.min.js" type="text/javascript"></script>



</head>

<body>
    <!-----INICIO FORMULARIO --->
    <form id="frm1" runat="server">
        <!--Script Manager para panel de Captcha-->
        <asp:ScriptManager ID="aSManagerCaptcha" runat="server">
        </asp:ScriptManager>
        <!--*******************-->
        <!----INICIO HEADER---->
        <div class="container-fluid">
            <div class="row header_content">
                <div class="header_login">
                    <!----LOGO--->
                    <div class="col-xs-4">
                        <%--<img src="Styles/Imagenes/LogotipoAltaResolucion.png" style="height: 60px;position:absolute;margin-top:-15px;" alt="logo" />--%>
                        <img src="Styles/Imagenes/LogotipoNADSI.png" style="height: 60px;position:absolute;margin-top:-15px;" alt="logo"/>
                    </div>
                    <!----LOGO--->
                </div>
            </div>
        </div>
        <!------FIN HEADER---->
        <!-------INICIO CONTENIDO---->
        <div class="container-fluid">
            <!---INI CONTENIDO LOGIN--->
            <div class="row content_login img_login">
                <div class="col-xs-10 col-sm-6 col-md-5 col-lg-3 data_login shadow_login">
                    <!------datos de usuario-->
                    <div class="row form-group col-xs-12 m-3">
                        <div>
                            <span class="glyphicon glyphicon-user icon_green"></span>
                            <label for="txtLogin">
                                Usuario:
                            </label>
                        </div>
                        <div id="hdvTxtLogin">
                            <input type="text" id="htxtUsuario" name="htxtUsuario" runat="server" class="form-control input-sm" placeholder="Usuario" oninput="javascript:fn_ValidaForm(this.value, 'hsmLogin', 'Favor de ingresar usuario', /^[A-ZÑa-zñáéíóúÁÉÍÓÚ._-]+$/, 'hdvTxtLogin', 'hspIconoLogin','btnEntrar','hsmRespuestaLogin','hsmRespuestaPassword')" />
                            <span id="hspIconoLogin"></span>
                            <small id="hsmLogin"></small>
                            <small id="hsmRespuestaLogin" class="hidden">0</small>
                        </div>
                    </div>
                    <br />
                    <!------datos de pass-->
                    <div class="form-group row col-xs-12 ">
                        <div>
                            <span class="glyphicon glyphicon-lock icon_green" aria-hidden="true"></span>
                            <label>
                                Contraseña:
                            </label>
                        </div>
                        <div id="hdvTxtPassword">
                            <input type="password" id="htxtContrasenia" name="htxtContrasenia" runat="server" class="form-control input-sm" placeholder="Contraseña" oninput="javascript:fn_ValidaForm(this.value, 'hsmPassword', 'Favor de Ingresar la contraseña', '', 'hdvTxtPassword', 'hspIconoPassword','btnEntrar','hsmRespuestaPassword','hsmRespuestaLogin')" />
                            <span id="hspIconoPassword"></span>
                            <small id="hsmPassword"></small>
                            <small id="hsmRespuestaPassword" class="hidden">0</small>
                        </div>
                    </div>
                    <!------Boton para entrar-->
                    <div class="form-group row col-xs-12 text-right">
                        <asp:Button ID="btnEntrar" runat="server"
                            class="shadow btn btn-sm btn-greenS input-sm"
                            Text="Entrar" OnClick="btnEntrar_Click" disabled />
                    </div>
                    <!------Opción olvidaste tu contraseña-->
                    <div class="form-group row col-xs-12">
                        <!--Recuperar contraseña-->
                        <asp:LinkButton class="txt-verde bold" ID="alkOlvidastePassword" runat="server" OnClick="vAbreDialogCambioContraseña">¿Has olvidado tu contraseña?</asp:LinkButton>
                        <!--***********************************************************-->
                    </div>
                </div>
            </div>
            <!---FIN CONTENIDO LOGIN--->
        </div>
        <!---------FIN  CONTENIDO---->
        <!-------INICIO AREA DIALOG---->
        <!--Dialog Cambio de Contraseña Captcha-->
        <div class="modal fade" id="hdvDialogCambioPass" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <!--Inicio Header-->
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Cambio de contraseña</h4>
                    </div>
                    <!--*****************-->
                    <!--Inicio Body-->
                    <div class="modal-body">
                        <table class="table table-bordered  table-responsive">
                            <tr>
                                <th>
                                    <label>Nombre de Usuario:</label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <div class="form-group">
                                        <div class="col-lg-12">
                                            <div id="hdvTxtUsuario">
                                                <asp:TextBox runat="server" ID="atxtUsuario" name="atxtUsuario" placeholder="Nombre de Usuario" CssClass="form-control input-sm" oninput="javascript:fn_ValidaForm(this.value, 'hsmUsuario', 'Favor de ingresar usuario', /^[A-ZÑa-zñáéíóúÁÉÍÓÚ._-]+$/, 'hdvTxtUsuario', 'hspIconoUsuario','abtnCambioPassword','hsmRespuestaUsuario','hsmRespuestaCaptcha')"></asp:TextBox>
                                                <span id="hspIconoUsuario"></span>
                                                <small id="hsmUsuario"></small>
                                                <small id="hsmRespuestaUsuario" class="hidden">0</small>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>Captcha:                          
                                    <div class="form-group">
                                        <div class="col-lg-12">
                                            <div id="hdvTxtCaptcha">
                                                <asp:TextBox runat="server" ID="htxtCaptcha" name="htxtCaptcha" placeholder="Captcha" CssClass="form-control input-sm" readonly Enabled></asp:TextBox>
                                             <%--   <span id="hspIconoUsuario"></span>
                                                <small id="hsmUsuario"></small>
                                                <small id="hsmRespuestaUsuario" class="hidden">0</small>--%>
                                            </div>
                                        </div>
                                    </div>

                                     <div class="form-group">
                                        <div class="col-lg-12">
                                            <div id="hdvtxtOc">
                                                <asp:TextBox runat="server" ID="htctOc" name="htctOc" hidden></asp:TextBox>
                                             <%--   <span id="hspIconoUsuario"></span>
                                                <small id="hsmUsuario"></small>
                                                <small id="hsmRespuestaUsuario" class="hidden">0</small>--%>
                                            </div>
                                        </div>
                                    </div>                                   
                                </th>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <div class="form-group">
                                        <div class="col-lg-12">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <%--<asp:Image ID="imageCaptcha" ImageUrl="~/Vista/Utilerias/CrearCaptcha.aspx?New=1" runat="server" Style="width: 100%; max-width: 400px;" />--%>
                                                    <asp:LinkButton ID="btnReload" runat="server" CssClass="btn btn-sm shadow" OnClick="vElegirNuevoCaptcha">
                                                    <span aria-hidden="true" class="glyphicon glyphicon-glyphicon glyphicon-refresh" style="font-size:2em;color:#90C63D"></span>
                                                    </asp:LinkButton>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnReload" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </td>
                            </tr>


                            <tr>
                                
                            </tr>

                            <tr>
                                <td>
                                    <div>
                                        <div class="form-group">
                                            <div class="col-lg-12">
                                                <div id="hdvTxtCaptcha" runat="server">
                                                    <asp:TextBox ID="atxtCaptcha" name="atxtCaptcha" placeHolder="Ingresa Captcha" runat="server" CssClass="form-control input-sm" oninput="javascript:fn_ValidaForm(this.value, 'hsmCatpcha', 'Favor de ingresar codigo capctha','', 'hdvTxtCaptcha', 'hspIconoCaptcha','abtnCambioPassword','hsmRespuestaCaptcha','hsmRespuestaUsuario')"></asp:TextBox>
                                                    <span id="hspIconoCaptcha" runat="server"></span>
                                                    <small id="hsmCatpcha" runat="server"></small>
                                                    <small id="hsmRespuestaCaptcha" class="hidden">0</small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>

                        </table>
                        <!--Inicio Footer-->
                        <div class="modal-footer">
                            <div class="form-group">
                                <div class="col-lg-9 col-lg-offset-3">
                                    <asp:Button disabled ID="abtnCambioPassword" runat="server" CssClass="btn btn-sm btn-greenS input-sm shadow" Text="Enviar" OnClick="vCambioPassword" />
                                </div>
                            </div>
                        </div>
                        <!--Fin Footer-->
                    </div>
                    <!--Fin Body-->
                </div>
            </div>
        </div>
        <!--***********-->
        <!--Dialog Cambio de Contraseña Inicio Sesion-->
        <div class="modal fade" id="hdvCambioPassword" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <!--Inicio Header-->
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 id="hhTitulo" class="modal-title">Cambio de contraseña</h4>
                    </div>
                    <!--*****************-->
                    <!--Inicio Body-->
                    <div class="modal-body">
                        <label><b id="hdvMensajeCambioPass"></b></label>
                        <table class="table table-bordered  table-responsive">
                            <tr>
                                <th>
                                    <label>Usuario:</label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <div class="form-group">
                                        <div class="col-lg-12">
                                            <div id="">
                                                <asp:TextBox ID="atxtUsuarioCambioPassword" placeHolder="" runat="server"
                                                    CssClass="form-control input-sm" disabled></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="ahiddenId" />
                                                <asp:HiddenField runat="server" ID="ahiddenNombre" />
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <label>Nueva contraseña:</label>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <div class="form-group">
                                        <div class="col-lg-12">
                                            <div id="hdvTxtCambioPassword" runat="server">
                                                <asp:TextBox ID="txtCambioPassword" name="atxtCaptcha" placeHolder="Nueva Contraseña" runat="server"
                                                    CssClass="form-control input-sm" TextMode="Password"
                                                    oninput="javascript:fn_ValidaForm(this.value, 'hsmCambioPass', 'Favor de ingresar contraseña',/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&_+.\s-])([A-Za-z\d$@$!%*?&_+.\s-]|[^ ]){8,80}$/, 'hdvTxtCambioPassword', 'hspCambioPass','abtnCambioPassSistema','hsmRespuestaCambioPass','hsmRespuestaUsuario')"></asp:TextBox>
                                                <span id="hspCambioPass" runat="server"></span>
                                                <small id="hsmCambioPass" runat="server"></small>
                                                <small id="hsmRespuestaCambioPass" class="hidden">0</small>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>Repite Contraseña:
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        <div class="form-group">
                                            <div class="col-lg-12">

                                                <div id="hdvTxtRepitePass">
                                                    <asp:TextBox runat="server" ID="atxtRepitePass" TextMode="Password" placeholder="Repite Contraseña" CssClass="form-control input-sm" oninput="javascript:fn_ValidaPassword(this.value,'hdvTxtRepitePass')"></asp:TextBox>
                                                    <span id="hspRepitePass"></span>
                                                    <i id="hsmTxtRepitePass"></i>
                                                    <small id="hsmRespuestaRepitePass" class="hidden">0</small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <!--Inicio Footer-->
                        <div class="modal-footer">
                            <div class="form-group">
                                <div class="col-lg-9 col-lg-offset-3">
                                    <asp:Button disabled ID="abtnCambioPassSistema" runat="server" CssClass="btn btn-sm btn-greenS shadow" Text="Enviar" OnClick="vCambiaPasswordInicio" />
                                </div>
                            </div>
                        </div>
                        <!--Fin Footer-->
                    </div>
                    <!--Fin Body-->
                </div>
            </div>
        </div>
        <!--*********************************************-->
        <!--Dialog de confirmacion de cambio de contraseña-->
        <div class="modal fade" id="hdvConfirmacion" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <!--Inicio Header-->
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Cambio de contraseña exitoso</h4>
                    </div>
                    <!--*****************-->
                    <!--Inicio Body-->
                    <div class="modal-body">
                        <p style="text-align: justify; color: rgb(31, 117, 148);"><span class="glyphicon glyphicon-ok-sign icon_green"></span><b>Su contraseña se ha cambiado correctamente. En estos momentos, se ha enviado un email con una contraseña temporal, la cual deberá utilizar la próxima vez que inicie sesión en el sistema.</b></p>
                        <!--Inicio Footer-->
                        <div class="modal-footer">
                            <div class="form-group">
                                <div class="col-lg-9 col-lg-offset-3">
                                    <span class="btn btn-sm btn-primary shadow" onclick="javascript:$('#hdvConfirmacion').modal('toggle')">Aceptar</span>
                                </div>
                            </div>
                        </div>
                        <!--Fin Footer-->
                    </div>
                    <!--Fin Body-->
                </div>
            </div>
        </div>
        <!---->
        <!-------FIN AREA DIALOG---->
        <!----INICIO FOOTER----->
        <div class="container-fluid">
            <div class="row" style="background: #1d6688;">
                <!--LEFT footer-->
                <div class="col-xs-6 footer_left">
                    <p>NAD Global <%: DateTime.Now.Year %>. ©Todos los derechos reservados.</p>
                </div>
                <!--RIGHT footer-->
                <div class="col-xs-6 text-right text-xs-right footer_right">
                    <img alt="" class="" src="Styles/Imagenes/img-logonad.png" />
                </div>
            </div>
        </div>
        <!-----FIN FOOTER----->
    </form>
</body>
</html>
