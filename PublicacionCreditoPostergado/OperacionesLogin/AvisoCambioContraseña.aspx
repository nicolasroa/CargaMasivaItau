<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Operaciones.Master" AutoEventWireup="true"
    CodeBehind="AvisoCambioContraseña.aspx.cs" Inherits="WebSite.OperacionesLogin.AvisoCambioContraseña" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorMasterPopup" runat="server">
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
                    <tr>
                        <td style="width: 17px;" background="../img/Login/iz.png" align="left">
                        </td>
                        <td align="center" width="400">
                            <anthem:Label AutoUpdateAfterCallBack="true" ID="lblMensaje" runat="server" CssClass="tdInfo"></anthem:Label>
                        </td>
                        <td background="../img/Login/derc.png" style="width: 35px;" align="right">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 17px;" background="../img/Login/iz.png" align="left">
                            &nbsp;
                        </td>
                        <td align="center">
                            <anthem:Panel ID="pnlConfirmacion" runat="server" AutoUpdateAfterCallBack="true">
                                <asp:Button ID="btnSi" runat="server" CssClass="myButton" OnClick="btnSi_Click"
                                    Text="SI" />
                                &nbsp;<asp:Button ID="btnNo" runat="server" CssClass="myButton" OnClick="btnNo_Click"
                                    Text="NO" />
                            </anthem:Panel>
                        </td>
                        <td background="../img/Login/derc.png" style="width: 35px;" align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 17px; height: 172px;" background="../img/Login/iz.png" align="left">
                            <img src="../Img/Login/iz.png" />
                        </td>
                        <td align="center" style="width: 422px; height: 172px;">
                            <anthem:Panel ID="pnlOperacion" runat="server" Visible="False" AutoUpdateAfterCallBack="true">
                                <table class="tdFormulario01" style="width: 100%;">
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="left" class="txt-01">
                                                        Usuario:
                                                    </td>
                                                    <td align="left">
                                                        <anthem:Label AutoUpdateAfterCallBack="true" ID="lblUsuario" runat="server"></anthem:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="txt-01">
                                                        Contraseña Actual:
                                                    </td>
                                                    <td align="left">
                                                        <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtClaveActual" runat="server"
                                                            TextMode="Password"></anthem:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Nueva Contraseña:
                                                    </td>
                                                    <td align="left">
                                                        <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtClaveNueva" 
                                                            runat="server" TextMode="Password"></anthem:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Repetir Nueva Contraseña:
                                                    </td>
                                                    <td align="left">
                                                        <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtClaveNueva2" runat="server"
                                                            TextMode="Password"></anthem:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnCambiarClave" runat="server" CssClass="myButton" Text="Cambiar Contraseña"
                                                OnClick="btnCambiarClave_Click" />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </anthem:Panel>
                        </td>
                        <td background="../img/Login/derc.png" style="width: 35px; height: 172px;" align="right">
                            <img src="../Img/Login/derc.png" />
                        </td>
                    </tr>
                    <tr>
                        <td background="../img/Login/iz.png" align="left">
                            &nbsp;
                        </td>
                        <td align="center">
                            <anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnCancelar" runat="server" CssClass="myButton" Text="Cancelar"
                                OnClick="btnCancelar_Click" />
                        </td>
                        <td background="../img/Login/derc.png" align="right">
                            &nbsp;
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
