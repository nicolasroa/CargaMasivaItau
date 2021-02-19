<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Operaciones.Master" AutoEventWireup="true"
    CodeBehind="RecuperarContraseña.aspx.cs" Inherits="WebSite.OperacionesLogin.RecuperarContraseña" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorMasterPopup" runat="server">
    <script src="../Styles/Controles/jquery-ui-1.8.23.custom.css" type="text/css" />
    <script src="../Styles/Master/EstiloMaster.css" type="text/css" />
    <script src="../Styles/Paginas/Formulario.css" type="text/css" />
    <script src="../Styles/Paginas/Estilo.css" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" src="../Scripts/Validacion.js"></script>
    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp;
            </td>
            <td align="center" height="300" width="700">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3">
                            <img src="../Img/Login/sup.png" />
                        </td>
                    </tr>
                    <tr id="rowLogin1" runat="server">
                        <td style="width: 17px; height: 107px;" background="../img/Login/iz.png" align="left">
                            <img src="../Img/Login/iz.png" />
                        </td>
                        <td align="center" style="width: 422px; height: 107px;">
                            <anthem:Panel ID="pnlLogin" runat="server" AutoUpdateAfterCallBack="true">
                                <table class="tdFormulario01" style="width: 100%;">
                                    <tr>
                                        <td>
                                            <table style="width: 100%; height: 77px;">
                                                <tr>
                                                    <td align="left" class="txt-01">
                                                        Usuario:
                                                    </td>
                                                    <td align="left">
                                                        <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtUsuario" runat="server" Width="150px"></anthem:TextBox>
                                                        <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                                                            EnabledDuringCallBack="False" ID="btnValidarUsuario" runat="server" CssClass="myButton"
                                                            Text="Validar" OnClick="btnValidarUsuario_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Pregunta de Seguridad:
                                                    </td>
                                                    <td align="left">
                                                        <anthem:Label ID="lbPregunta" runat="server" CssClass="tdInfoData" AutoUpdateAfterCallBack="true"></anthem:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Respuesta:
                                                    </td>
                                                    <td align="left">
                                                        <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtRespuesta" runat="server" Width="200px"></anthem:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                                                EnabledDuringCallBack="False" ID="btnRecuperarClave" runat="server" CssClass="myButton"
                                                Text="Recuperar" OnClick="btnRecuperarClave_Click" TextDuringCallBack="Recuperano Contraseña, Favor Espere..." />
                                            &nbsp;<br />
                                        </td>
                                    </tr>
                                </table>
                            </anthem:Panel>
                        </td>
                        <td background="../img/Login/derc.png" style="width: 35px; height: 107px;" align="right">
                            <img src="../Img/Login/derc.png" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 17px; height: 18px;" background="../img/Login/iz.png" align="left">
                        </td>
                        <td align="center" style="height: 18px;">
                            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                                EnabledDuringCallBack="False" ID="btnCancelar" runat="server" CssClass="myButton"
                                OnClick="btnCancelar_Click" Text="Cancelar" AutoUpdateAfterCallBack="True" />
                        </td>
                        <td background="../img/Login/derc.png" style="width: 35px; height: 18px;" align="right">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <img src="../Img/Login/Base.png" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
                <anthem:HiddenField ID="hfUsuarioId" runat="server" AutoUpdateAfterCallBack="true" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
