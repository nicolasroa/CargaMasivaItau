﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargaNominaPostergados.aspx.cs"
    EnableEventValidation="false" Inherits="Documental.WebApp.CargaNominaPostergados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="Css/EstiloItau/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="Css/EstiloItau/jquery-ui.structure.css" rel="stylesheet" type="text/css" />
    <link href="Css/EstiloItau/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="Css/layout-default.css" rel="stylesheet" type="text/css" />
    <link href="Css/Estilo.css" rel="stylesheet" type="text/css" />
    <link href="Css/jquery.dropdown.css" rel="stylesheet" type="text/css" />
    <link href="Css/liteaccordion.css" rel="stylesheet" type="text/css" />
    <link href="Css/Menu.css" rel="stylesheet" type="text/css" />
    <link href="Css/tipped.css" rel="stylesheet" type="text/css" />
    <link href="Css/Portal.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 288px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
    <script>
        $(function () {
            $("#TabPrincipal").tabs();
        });
    </script>
    <table style="width: 100%;">
        <tr>
            <td class="tdLogo" valign="top">
                <table class="EncabezadoTitulo" style="width: 100%;">
                    <tr>
                        <td align="left" style="padding-left: 30px; font-size: medium;" width="350">
                            <anthem:Image ID="imgLogoItau" runat="server" ImageUrl="~/Img/logo-itau.png" />
                            <br />
                            Carga Masiva de Créditos Postegados
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="TabPrincipal" style="background-color: #FFFFFF; border-style: groove">
                    <ul>
                        <li><a href="#tabs1">Carga</a></li>
                        <!--<li><a href="#tabs2">Historial de Cargas</a></li>-->
                    </ul>
                    <%--INICIO TAB DOCUMENTOS POR SOLICITUD--%>
                    <div id="tabs1">
                        <table style="width: 100%;">
                            <tr>
                                <td class="tdTitulo01">
                                    Seleccion de Archivo
                                </td>
                            </tr>
                            <tr>
                                <td class="tdFormulario01">
                                    <anthem:FileUpload ID="fupCargaInterface" runat="server" AutoUpdateAfterCallBack="true"
                                        Width="464px" />
                                    <anthem:Button ID="btnValidarNomina" runat="server"
                                        Text="Validar Nómina" OnClick="btnValidarNomina_Click" 
                                        EnabledDuringCallBack="False" TextDuringCallBack="Cargando Nómina...." />
                                &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="tdTitulo02">
                                    Datos de la Nómina Cargada</td>
                            </tr>
                            <tr>
                                <td class="tdFormulario01">
                                    <table style="width:100%;">
                                        <tr>
                                            <td class="style1">
                                                Cantidad Total de Registros</td>
                                            <td class="tdInfoData">
                                                <anthem:Label ID="lblTotal" runat="server" AutoUpdateAfterCallBack="True" 
                                                    UpdateAfterCallBack="True"></anthem:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                Cantidad de Registros Aptos para Procesar</td>
                                            <td class="tdInfoData">
                                                <anthem:Label ID="lblAprobados" runat="server" AutoUpdateAfterCallBack="True" 
                                                    UpdateAfterCallBack="True"></anthem:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                Cantidad de Registros Rechazados</td>
                                            <td class="tdInfoData">
                                                <anthem:Label ID="lblRechazados" runat="server" AutoUpdateAfterCallBack="True" 
                                                    UpdateAfterCallBack="True"></anthem:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdFormulario01" align="center" style="text-align: center;">
                                    <anthem:Button ID="btnProcesar" runat="server"
                                        Text="Procesar Registros Validados" OnClick="btnProcesar_Click" 
                                        OnMouseDown="javascript:MensajeConfirmacion('Solo se postergaran los registros validados ¿Seguro que desea continuar?',this);" 
                                        Width="300px" EnabledDuringCallBack="False" TextDuringCallBack="Procesando..." />
                                    &nbsp;<anthem:Button ID="btnLimpiar" runat="server"
                                        Text="Limpiar" onclick="btnLimpiar_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdTitulo01">
                                    Datos de la Nómina
                                </td>
                            </tr>
                            <tr class="tdFormulario01">
                                <td>
                                    <div style="width: 100%; height: 200px; overflow-y: scroll;">
                                    <anthem:GridView ID="gvPostergados" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                        AutoUpdateAfterCallBack="True" Width="100%" CellPadding="0" UpdateAfterCallBack="True">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Validación">
                                                <ItemTemplate>
                                                    <anthem:CheckBox ID="chkValido" runat="server" Enabled="False" AutoUpdateAfterCallBack="True"
                                                        Checked='<%# Eval("Validado")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Rut" HeaderText="Rut" />
                                            <asp:BoundField DataField="FechaAprobacion" HeaderText="Fecha de Aprobación" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="NroOperacion" HeaderText="Nro. Operación" />
                                            <asp:BoundField DataField="Plazo" HeaderText="Plazo" />
                                            <asp:BoundField DataField="Gracia" HeaderText="Gracia" />
                                            <asp:BoundField DataField="Tasa" HeaderText="Tasa" DataFormatString="{0:F4}" />
                                            <asp:BoundField DataField="NroDividendosAPostergar" HeaderText="Nro Dividendos A Postergar" />
                                            <asp:BoundField DataField="FechaEscritura" HeaderText="Fecha Escritura" />
                                            <asp:BoundField DataField="MensajeError" HeaderText="Observaciones" />
                                        </Columns>
                                    </anthem:GridView>
                                </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdTitulo01">
                                    Solicitudes Creadas
                                </td>
                            </tr>
                            <tr class="tdFormulario01">
                                <td>
                                    <div style="width: 100%; height: 200px; overflow-y: scroll;">
                                    <anthem:GridView ID="gvPostergadosCreadas" runat="server" AutoGenerateColumns="False"
                                        AllowSorting="True" AutoUpdateAfterCallBack="True" Width="100%"
                                        CellPadding="0" UpdateAfterCallBack="True">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Validación">
                                                <ItemTemplate>
                                                    <anthem:CheckBox ID="chkValido0" runat="server" Enabled="False" AutoUpdateAfterCallBack="True"
                                                        Checked='<%# Eval("Validado")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Rut" HeaderText="Rut" />
                                            <asp:BoundField DataField="FechaAprobacion" HeaderText="Fecha de Aprobación" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="NroOperacion" HeaderText="Nro. Operación" />
                                            <asp:BoundField DataField="Plazo" HeaderText="Plazo" />
                                            <asp:BoundField DataField="Gracia" HeaderText="Gracia" />
                                            <asp:BoundField DataField="Tasa" HeaderText="Tasa" DataFormatString="{0:F4}" />
                                            <asp:BoundField DataField="NroDividendosAPostergar" HeaderText="Nro Dividendos A Postergar" />
                                            <asp:BoundField DataField="FechaEscritura" HeaderText="Fecha Escritura" />
                                            <asp:BoundField DataField="MensajeError" HeaderText="Observaciones" />
                                        </Columns>
                                    </anthem:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <anthem:Button ID="btnExportar" runat="server" Text="Exportar" 
                                        AutoUpdateAfterCallBack="true" EnableCallBack="false" 
                                        OnClick="btnExportar_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!--div id="tabs2">
                        <table style="width: 100%;">
                            <tr>
                                <td class="tdTitulo01">
                                    Últimas 100 Cargas Realizadas
                                </td>
                            </tr>
                            <tr class="tdFormulario01">
                                <td>
                                    <div style="width: 100%; height: 200px; overflow-y: scroll;">
                                    <anthem:GridView ID="gvLogCarga" runat="server" AutoGenerateColumns="False"
                                        AllowSorting="True" AutoUpdateAfterCallBack="True" Width="100%" CellPadding="0"
                                        UpdateAfterCallBack="True">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="NumeroSolicitud" HeaderText="Número de Solicitud Creada" />
                                            <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                                            <asp:BoundField DataField="UsuarioResponsable" HeaderText="Responsable de la Carga" />
                                            <asp:BoundField DataField="FechaAprobacion" HeaderText="Fecha de Aprobación" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="FechaCarga" HeaderText="Fecha de Carga" />
                                            <asp:BoundField DataField="MontoAprobado" HeaderText="Monto Aprobado" DataFormatString="{0:F4}" />
                                            
                                            <asp:BoundField DataField="CanalVenta" HeaderText="Canal de Venta" />
                                        </Columns>
                                    </anthem:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div-->
                </div>
            </td>
        </tr>
    </table>
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
