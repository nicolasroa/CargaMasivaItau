<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Documental.OperacionesLogin.Login" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<%@ Register Namespace="Telerik.WebControls" TagPrefix="WebControls" %>
<%@ Register Assembly="RadMenu.Net2" Namespace="Telerik.WebControls" TagPrefix="radM" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/Blitzer/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Css/layout-default.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Estilo.css" rel="stylesheet" type="text/css" />
    <link href="../Css/jquery.dropdown.css" rel="stylesheet" type="text/css" />
    <link href="../Css/liteaccordion.css" rel="stylesheet" type="text/css" />
    <link href="../Css/tipped.css" rel="stylesheet" type="text/css" />
    <link href="../Js/noty/buttons.css" rel="stylesheet" type="text/css" />
    <link href="../Js/noty/animate.css" rel="stylesheet" type="text/css" />
    <link href="../Js/noty/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <AjaxAsp:ToolkitScriptManager ID="ToolkitScriptManager1" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Js/jquery-1.11.2.js" />
                <asp:ScriptReference Path="~/Js/jquery-ui.js" />
                <asp:ScriptReference Path="~/Js/Controles.js" />
                <asp:ScriptReference Path="~/Js/Efectos.js" />
                <asp:ScriptReference Path="~/Js/Validacion.js" />
                <asp:ScriptReference Path="~/Js/excanvas.js" />
                <asp:ScriptReference Path="~/Js/tipped.js" />
                <asp:ScriptReference Path="~/Js/jquery.layout.js" />
                <asp:ScriptReference Path="~/Js/Menu.js" />
                <asp:ScriptReference Path="~/Js/funciones.js" />
                
                <asp:ScriptReference Path="~/Js/jquery.dropdown.js" />
                <asp:ScriptReference Path="~/Js/liteaccordion.jquery.js" />
                <asp:ScriptReference Path="~/Js/noty/packaged/jquery.noty.packaged.min.js" />
                <asp:ScriptReference Path="~/Js/noty/layouts/bottomCenter.js" />


            </Scripts>
        </AjaxAsp:ToolkitScriptManager>
        <div id="divLogin">
            <anthem:Panel ID="pnlLogin" runat="server" AutoUpdateAfterCallBack="true">
                <table class="tdFormulario01" style="width: 100%;">

                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td align="center">
                                        <anthem:TextBox ID="txtUsuario" runat="server" AutoUpdateAfterCallBack="true" CssClass="tdLblDatosLoginUser" placeholder="Nombre de Usuario" Width="200px"></anthem:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <anthem:TextBox ID="txtClave" runat="server" AutoUpdateAfterCallBack="true" CssClass="tdLblDatosLoginClave" placeholder="Contraseña" TextMode="Password" Width="200px"></anthem:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <anthem:Button ID="btnValidar" runat="server" AutoUpdateAfterCallBack="true" CssClass="myButton2" Text="Validar" OnClick="btnValidar_Click" />
                            &nbsp;<anthem:Button ID="btnCancelarLogin" runat="server" AutoUpdateAfterCallBack="true" CssClass="myButton2" OnClick="btnValidar_Click" Text="Cancelar" />
                            <br />
                            <asp:LinkButton ID="lkbtnRecuperarContraseña" runat="server" CssClass="link-01" OnClick="lkbtnRecuperarContraseña_Click">Olvidé Mi Contraseña</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </anthem:Panel>
        </div>
        <div style="display: none">


            <div id="divModalPopup1" title="VentanaModal">
            </div>
            <div id="divModalPopup2" title="VentanaModal">
            </div>
            <div id="divModalPopup3" title="VentanaModal">
            </div>
            <div id="divMensajes">
                <div>
                    <label id="lbltextoMensaje">
                    </label>
                </div>
            </div>
            <div id="divConfirmacion">
                <label id="lbltextoConfirmacion">
                </label>
            </div>

            <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                EnabledDuringCallBack="False" ID="btnCerrarPopup" runat="server" Text="btnCerrar"
                CssClass="CssCerrarPopup" />
            <anthem:HiddenField ID="hdnIsPostBack" runat="server" AutoUpdateAfterCallBack="true" />
        </div>


       


    </form>
</body>
</html>
