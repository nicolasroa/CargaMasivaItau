<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestionMailWF.aspx.cs"
    Inherits="Documental.WebApp.EnvioMailWf.GestionMailWF" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>Gestión de Envio de Notificaciones WF</title>
    <link href="../Css/EstiloItau/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Css/EstiloItau/jquery-ui.structure.css" rel="stylesheet" type="text/css" />
    <link href="../Css/EstiloItau/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../Css/layout-default.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Estilo.css" rel="stylesheet" type="text/css" />
    <link href="../Css/jquery.dropdown.css" rel="stylesheet" type="text/css" />
    <link href="../Css/liteaccordion.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Menu.css" rel="stylesheet" type="text/css" />
    <link href="../Css/tipped.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Portal.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <AjaxAsp:ToolkitScriptManager ID="ToolkitScriptManager1" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Js/jquery-1.11.2.js" />
            <asp:ScriptReference Path="~/Js/jquery-ui.js" />
            <asp:ScriptReference Path="~/Js/noty/jquery.noty.packaged.min.js" />
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
        </Scripts>
    </AjaxAsp:ToolkitScriptManager>
    <table style="width: 100%;">
        <tr>
            <td class="tdTitulo01">
                Gestión de Envio de Notificaciones WF
            </td>
        </tr>
        <tr>
            <td class="tdTitulo03">
                <anthem:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Img/icons/document_new.png" />
                <anthem:LinkButton CssClass="link" ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Nuevo Registro</anthem:LinkButton>
            </td>
        </tr>
    </table>
    <div id="accordion">
        <h2 id="hBusqueda">
            <a id="lnkBusqueda" class="tdFormulario01" href="#">Búsqueda</a>
        </h2>
        <div>
            <table style="width: 100%;" class="tdFormulario01">
                <tr>
                    <td class="tdFormulario01">
                        <table class="tdFormulario01" width="100%">
                            <tr>
                                <td>
                                    Flujo:
                                </td>
                                <td>
                                    <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlFlujo" TabIndex="4" runat="server"
                                        OnSelectedIndexChanged="ddlFlujo_SelectedIndexChanged">
                                    </anthem:DropDownList>
                                </td>
                                <td>
                                    Decision del Evento:
                                </td>
                                <td>
                                    <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlDecision" TabIndex="4"
                                        runat="server">
                                    </anthem:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Evento:
                                </td>
                                <td>
                                    <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlEvento" TabIndex="4" runat="server"
                                        OnSelectedIndexChanged="ddlEvento_SelectedIndexChanged">
                                    </anthem:DropDownList>
                                </td>
                                <td>
                                    Estado:
                                </td>
                                <td>
                                    <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlEstado" TabIndex="4" runat="server">
                                    </anthem:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdFormulario01" align="center">
                        <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                            EnabledDuringCallBack="False" ID="btnBuscar" CssClass="myButton" runat="server"
                            Text="Buscar" TextDuringCallBack="Buscando..." />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitulo02">
                        Resultado de la Búsqueda:
                        <anthem:Label ID="lblContador" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdGrid" align="center">
                        <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                            PageSize="10" AllowPaging="True" AllowSorting="True" AutoUpdateAfterCallBack="true">
                            <RowStyle CssClass="GridItem" />
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="GridHeader" />
                            <AlternatingRowStyle CssClass="GridAtlItem" />
                            <Columns>
                                <asp:BoundField DataField="Rut" HeaderText="Rut" />
                                <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
                                <asp:BoundField DataField="DescripcionEstado" HeaderText="Estado" />
                                <asp:BoundField DataField="Mail" HeaderText="Mail" />
                                <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:ImageButton ID="btnModificar" runat="server" ImageUrl="~/Img/Controles/GridView/EditarIC.gif"
                                            ToolTip="Modificar" TextDuringCallBack="Buscando Registro..." />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </anthem:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <h2 id="hFormulario" style="display: none;">
            <a id="lnkFormulario" class="tdFormulario01" href="#">Formulario</a></h2>
        <div>
            <table class="tdFormulario01" style="width: 100%;">
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    Código:
                                </td>
                                <td>
                                    <anthem:Label ID="lblFormCodigo" runat="server" AutoUpdateAfterCallBack="True" UpdateAfterCallBack="True"></anthem:Label>
                                </td>
                                <td>
                                    Asunto:
                                </td>
                                <td>
                                    <anthem:TextBox ID="txtFormAsunto" runat="server" TabIndex="7" Width="435px" AutoUpdateAfterCallBack="true"
                                        MaxLength="500"></anthem:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Flujo:
                                </td>
                                <td>
                                    <anthem:DropDownList ID="ddlFormFlujo" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true"
                                        OnSelectedIndexChanged="ddlFormFlujo_SelectedIndexChanged">
                                    </anthem:DropDownList>
                                </td>
                                <td>
                                    Evento de Despacho:
                                </td>
                                <td>
                                    <anthem:DropDownList ID="ddlFormEvento" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true"
                                        OnSelectedIndexChanged="ddlFormEvento_SelectedIndexChanged">
                                    </anthem:DropDownList>
                                    NOTA: Se Generará al Avanzar este Evento
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Destinatario:
                                </td>
                                <td>
                                    <anthem:DropDownList ID="ddlFormDestinatario" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true">
                                    </anthem:DropDownList>
                                </td>
                                <td>
                                    Decisión de Evento:
                                </td>
                                <td>
                                    <anthem:DropDownList ID="ddlFormDecision" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true">
                                    </anthem:DropDownList>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <anthem:CheckBox ID="chkFormImagenEncabezado" runat="server" Text="Imagen Encabezado" />
                                </td>
                                <td>
                                    <anthem:FileUpload ID="fulFormEncabezado" runat="server" AutoUpdateAfterCallBack="true" />
                                </td>
                                <td>
                                    <anthem:CheckBox ID="chkFormImagenPieFirma" runat="server" Text="Imagen Pie de Firma" />
                                </td>
                                <td>
                                    <anthem:FileUpload ID="fulPieFirma" runat="server" AutoUpdateAfterCallBack="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <anthem:CheckBox ID="chkFormSoloUnEnvio" runat="server" Text="Solo un Envío" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Estado:
                                </td>
                                <td>
                                    <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlFormEstado" TabIndex="4"
                                        runat="server">
                                    </anthem:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td class="tdTitulo03" width="25%">
                                    Variables Disponibles
                                </td>
                                <td class="tdTitulo03" style="text-align: center">
                                    Texto
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <anthem:ListBox ID="lstFormVariablesWf" runat="server" AutoUpdateAfterCallBack="True"
                                        Height="300px" AutoCallBack="True" 
                                        onselectedindexchanged="lstFormVariablesWf_SelectedIndexChanged" Width="100%">
                                        <Items>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem Value="3"></asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem Value="5"></asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                        </Items>
                                    </anthem:ListBox>
                                </td>
                                <td height="300" valign="top">
                                    <anthem:TextBox ID="txtFormTexto" runat="server" AutoUpdateAfterCallBack="True" Height="300px"
                                        TextMode="MultiLine" Width="100%"></anthem:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                            EnabledDuringCallBack="False" ID="btnGuardar" runat="server" CssClass="myButton"
                            Text="Guardar" TextDuringCallBack="Buscando..." EnableCallBack="False" OnClick="btnGuardar_Click" />
                        &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                            EnabledDuringCallBack="False" ID="btnMailPrueba" runat="server" CssClass="myButton"
                            Text="Mail de Prueba" TextDuringCallBack="Enviando Mail..." />
                        &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                            EnabledDuringCallBack="False" ID="btnVistaPrevia" runat="server" CssClass="myButton"
                            Text="Vista Previa" TextDuringCallBack="Generando..." />
                        &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                            EnabledDuringCallBack="False" ID="btnCancelar" runat="server" Text="Cancelar"
                            CssClass="myButton" TextDuringCallBack="Cancelando..." OnClick="btnCancelar_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <anthem:HiddenField ID="hfId" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
