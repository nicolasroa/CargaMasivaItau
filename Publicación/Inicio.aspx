<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Inicio.aspx.cs"
    Inherits="Documental.Inicio" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<%@ Register Namespace="Telerik.WebControls" TagPrefix="WebControls" %>
<%@ Register Assembly="RadMenu.Net2" Namespace="Telerik.WebControls" TagPrefix="radM" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>Documental</title>
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
</head>
<body onload="MostrarFormularioPortal(); MostrarFormularioPortalRep();" class="tdBody">
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
    <script type='text/javascript'>


        $(function () {
            $("#TabPrincipal").tabs();
        });

        function MostrarFormularioPortal() {
            if (ObtenerControl('divFormularioEditar').style.display == "none") {
                ObtenerControl('divFormularioEditar').style.display = "block";
            }
            else {
                getElement('divFormularioEditar').style.display = "none";
            }
        }

        function MostrarFormularioPortalEditar() {
            ObtenerControl('divFormularioEditar').style.display = "block";
        }

        function MostrarFormularioPortalRep() {
            if (ObtenerControl('divFormularioEditarRep').style.display == "none") {
                ObtenerControl('divFormularioEditarRep').style.display = "block";
            }
            else {
                getElement('divFormularioEditarRep').style.display = "none";
            }
        }

        function MostrarFormularioPortalEditarRep() {
            ObtenerControl('divFormularioEditarRep').style.display = "block";
        }

        function ObtenerControl(id) {

            if (document.getElementById) {
                return document.getElementById(id);
            }

            else if (document.all) {
                return window.document.all[id];
            }

            else if (document.layers) {
                return window.document.layers[id];
            }
        }


        function getjquerydate() {
            $("#<%= txtFechaEmision.ClientID %>").datepicker({

        });
    };

    function getjquerydateRep() {
        $("#txtFechaEmisionRep").datepicker();
    };

    function clearFileUpload() {
        var fu = document.getElementById("<%= fluArchivo.ClientID %>");
        if (fu != null) {
            try {
                fu.setAttribute("type", "input");
                fu.setAttribute("type", "file");
            }
            catch (ex) {
                fu.outerHTML = fu.outerHTML;
            }
        }
    }

    function clearFileUploadRep() {
        var fuRep = document.getElementById("<%= fluArchivoRep.ClientID %>");
        if (fuRep != null) {
            try {
                fuRep.setAttribute("type", "input");
                fuRep.setAttribute("type", "file");
            }
            catch (ex) {
                fuRep.outerHTML = fuRep.outerHTML;
            }
        }
    }

    </script>
    <table style="width: 100%;">
        <tr>
            <td>
            </td>
            <td class="tdLogo" valign="top">
                <table class="EncabezadoTitulo" style="width: 100%;">
                    <tr>
                        <td align="left" rowspan="4" style="padding-left: 30px; font-size: medium;" width="650">
                            <anthem:Image ID="imgLogoItau" runat="server" ImageUrl="~/Img/logo-itau.png" />
                            <br />
                            Sistema Documental
                        </td>
                        <td>
                            Rut Cliente:
                        </td>
                        <td align="left">
                            <anthem:Label ID="lblNroIdentidad" runat="server"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cliente:
                        </td>
                        <td align="left">
                            <anthem:Label ID="lblNombre" runat="server"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Proceso:</td>
                        <td align="left">
                            <anthem:Label ID="lblProceso" runat="server"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            N° Solicitud del Credito:
                        </td>
                        <td align="left">
                            <anthem:Label ID="lblNumSolicitud" runat="server"></anthem:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <%-- INICIO TAB PRINCIPAL--%>
                <div id="TabPrincipal" style="background-color: #FFFFFF; border-style: groove">
                    <ul>
                        <li><a href="#tabs1">Repositorio Por Solicitud</a></li>
                        <li><a id="tabParticipantes" runat="server" href="#tabs2">Repositorio Por Participantes</a></li>
                        <li><a href="#tabs3">Historial</a></li>
                    </ul>
                    <%--INICIO TAB DOCUMENTOS POR SOLICITUD--%>
                    <div id="tabs1">
                        <table style="width: 100%; height: 500px;">
                            <tr>
                                <td width="25%" valign="top">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="tdTitulo01">
                                                <img alt="_blank" src="Img/icons/check2.png" />
                                                <anthem:Label ID="lblDocumentosSolicitud" runat="server">Mis Documentos</anthem:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdFormulario01">
                                                <anthem:GridView ID="gvGrupoDocumentosSolicitud" runat="server" ShowHeader="false"
                                                    AutoUpdateAfterCallBack="true" AutoGenerateColumns="False" Width="100%">
                                                    <RowStyle CssClass="GridItem" />
                                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <AlternatingRowStyle CssClass="GridAtlItem" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <img alt="_blank" src="Img/icons/folder.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <anthem:LinkButton runat="server" ID="lnkNombreArchivoBanSol" Text='<%# Eval("GrupoDocumento")%>'
                                                                    CommandArgument="GrupoDocumentoSolicitud" OnClick="lnkNombreArchivoBanSol_Click"
                                                                    AutoUpdateAfterCallBack="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                (<anthem:Label ID="lblGrupoCantidad" runat="server" Text='<%# Bind("Cantidad") %>'
                                                                    AutoUpdateAfterCallBack="true" />)
                                                            </ItemTemplate>
                                                            <ItemStyle Font-Bold="True"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAgrupacionValidada" runat="server" AutoPostBack="True" />
                                                <asp:Label ID="lblIdGrupoDocumento" runat="server" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                </anthem:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdFormulario02">
                                                <asp:Image ID="imgVigenteSolicitud" runat="server" 
                                                    ImageUrl="~/Img/IconGreen.png" />
                                                &nbsp;Vigente
                                                <asp:Image ID="imgPorVencerSolicitud" runat="server" 
                                                    ImageUrl="~/Img/IconYellow.png" />
                                                &nbsp;Por Vencer
                                                <asp:Image ID="imgVencidoSolicitud" runat="server" 
                                                    ImageUrl="~/Img/IconRed.png" />
                                                &nbsp;Vencido</td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="tdTitulo01" align="left" valign="center">
                                                <img alt="_blank" src="Img/icons/folder.png" />
                                                <anthem:LinkButton ID="lnkFormularioEditar" runat="server" Text="Subir Documento"
                                                    CssClass="tdTitulo01" OnClientClick="MostrarFormularioPortal();" ForeColor="White"></anthem:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <div id="divFormularioEditar" runat="server">
                                                    <table class="tdFormulario01" width="100%">
                                                        <tr>
                                                            <td>
                                                                Grupo
                                                            </td>
                                                            <td>
                                                                <anthem:DropDownList ID="ddlGrupoDocumento" runat="server" AutoUpdateAfterCallBack="true"
                                                                    AutoCallBack="true" OnSelectedIndexChanged="ddlGrupoDocumento_SelectedIndexChanged"
                                                                    onchange="clearFileUpload();">
                                                                </anthem:DropDownList>
                                                            </td>
                                                            <td>
                                                                Fecha emisión
                                                            </td>
                                                            <td>
                                                                <anthem:TextBox ID="txtFechaEmision" runat="server" AutoUpdateAfterCallBack="true" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Tipo de documento
                                                            </td>
                                                            <td colspan="3">
                                                                <anthem:DropDownList ID="ddlTipoDocumento" runat="server" AutoUpdateAfterCallBack="true"
                                                                    AutoCallBack="true" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged"
                                                                    onchange="clearFileUpload();">
                                                                </anthem:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Archivo
                                                            </td>
                                                            <td colspan="3">
                                                                <anthem:FileUpload ID="fluArchivo" runat="server" AutoUpdateAfterCallBack="true" />
                                                                <%--<asp:TemplateField ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAgrupacionValidada" runat="server" AutoPostBack="True" />
                                                <asp:Label ID="lblIdGrupoDocumento" runat="server" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%><%--<asp:TemplateField ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAgrupacionValidada" runat="server" AutoPostBack="True" />
                                                <asp:Label ID="lblIdGrupoDocumento" runat="server" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Observaciones
                                                            </td>
                                                            <td colspan="3">
                                                                <anthem:TextBox ID="txtObservacion" runat="server" AutoUpdateAfterCallBack="true"
                                                                    Width="300px"></anthem:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" style="text-align: center">
                                                                <anthem:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar"
                                                                    Visible="false" TextDuringCallBack="Guardando..." CssClass="myButton2" AutoUpdateAfterCallBack="true" />
                                                                <anthem:Button ID="btnCancelar" runat="server" Text="Cancelar" TextDuringCallBack="Cancelando..."
                                                                    CssClass="myButton2" OnClientClick="MostrarFormularioPortal();" OnClick="btnCancelar_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdTitulo02">
                                                Búsqueda de Documentos
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table class="tdFormulario01" width="100%">
                                                    <tr>
                                                        <td>
                                                            Grupo de Documento
                                                        </td>
                                                        <td>
                                                            <anthem:DropDownList ID="ddlGrupoDocumentoSolicitudBus" runat="server" AutoUpdateAfterCallBack="true"
                                                                EnableCallBack="true" AutoCallBack="true" OnSelectedIndexChanged="ddlGrupoDocumentoSolicitudBus_SelectedIndexChanged" />
                                                        </td>
                                                        <td>
                                                            Tipo de documento
                                                        </td>
                                                        <td>
                                                            <anthem:DropDownList ID="ddlTipoDocumentoSolicitudBus" runat="server" AutoUpdateAfterCallBack="true"
                                                                EnableCallBack="true" AutoCallBack="true" OnSelectedIndexChanged="ddlTipoDocumentoSolicitudBus_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:GridView ID="gvwDocumentosSolicitud" runat="server" AutoGenerateColumns="False"
                                                    AllowPaging="True" AllowSorting="True" AutoUpdateAfterCallBack="True" Width="100%"
                                                    OnRowDataBound="gvwDocumentosSolicitud_RowDataBound" OnPageIndexChanging="gvwDocumentosSolicitud_PageIndexChanging"
                                                    CellPadding="0" UpdateAfterCallBack="True">
                                                    <RowStyle CssClass="GridItem" />
                                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <AlternatingRowStyle CssClass="GridAtlItem" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <anthem:Image ID="imgEstadoAlertaDoc" runat="server" Height="16px" Width="16px" />
                                                                <anthem:HiddenField ID="hdnEstadoAlertaDoc" runat="server" Value='<%# Eval("EstadoAlertaDoc") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nombre" HeaderStyle-Width="170px" ItemStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <anthem:LinkButton runat="server" ID="lnkNombreArchivo" Text='<%# (Eval("NombreArchivo").ToString().Length >= 18) ? Eval("NombreArchivo").ToString().Substring(0, 17) + "..." : Eval("NombreArchivo")%>'
                                                                    OnClick="lnkNombreArchivo_Click" EnableCallBack="false" AutoUpdateAfterCallBack="false"
                                                                    ToolTip='<%# (Eval("NombreArchivo").ToString())%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <anthem:Image ID="imgTipoDocumento" runat="server" Height="16px" Width="16px" />
                                                                <anthem:HiddenField ID="hdnTipoDocumento" runat="server" Value='<%# Eval("ExtensionArchivo") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Fecha" DataField="FechaEmision" DataFormatString="{0:dd-MM-yyyy}">
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Grupo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGrupo" runat="server" Text='<%# (Eval("GrupoDocumento").ToString().Length >= 20) ? Eval("GrupoDocumento").ToString().Substring(0, 19) + "..." : Eval("GrupoDocumento")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTipo" runat="server" Text='<%# (Eval("TipoDocumento").ToString().Length >= 20) ? Eval("TipoDocumento").ToString().Substring(0, 19) + "..." : Eval("TipoDocumento")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <anthem:ImageButton ID="btnDescargar" alt="Descargar" runat="server" ImageUrl="~/Img/DocDownload.png"
                                                                    EnableCallBack="false" AutoUpdateAfterCallBack="false" Height="20px" Width="20px"
                                                                    OnClick="btnDescargar_Click" />
                                                                <anthem:ImageButton ID="btnEditarDocumento" alt="Editar" runat="server" ImageUrl="~/Img/DocEdit.png"
                                                                    Height="20px" Width="20px" OnClientClick="MostrarFormularioPortalEditar();" OnClick="btnEditarDocumento_Click" />
                                                                <anthem:ImageButton ID="btnEliminarDocumento" alt="Eliminar" runat="server" ImageUrl="~/Img/DocDelete.png"
                                                                    Height="20px" Width="20px" OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea Eliminar el Registro?',this);"
                                                                    OnClick="btnEliminarDocumento_Click" />
                                                                <anthem:HiddenField ID="IdSolDoc" runat="server" Value='<%# Eval("Id")%>' />
                                                                <anthem:HiddenField ID="hdnPermisoDescargar" runat="server" Value='<%# Eval("PermisoDescargar")%>' />
                                                                <anthem:HiddenField ID="hdnPermisoVer" runat="server" Value='<%# Eval("PermisoVer")%>' />
                                                                <anthem:HiddenField ID="hdnPermisoModificar" runat="server" Value='<%# Eval("PermisoModificar")%>' />
                                                                <anthem:HiddenField ID="hdnPermisoEliminar" runat="server" Value='<%# Eval("PermisoEliminar")%>' />
                                                                <anthem:HiddenField ID="hdnPermisoSubir" runat="server" Value='<%# Eval("PermisoSubir")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </anthem:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--<asp:TemplateField ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAgrupacionValidada" runat="server" AutoPostBack="True" />
                                                <asp:Label ID="lblIdGrupoDocumento" runat="server" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%><%--<asp:TemplateField ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAgrupacionValidada" runat="server" AutoPostBack="True" />
                                                <asp:Label ID="lblIdGrupoDocumento" runat="server" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                    <div id="tabs2">
                        <table style="width: 100%; height: 500px;">
                            <tr>
                                <td width="25%" valign="top">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="tdTitulo01">
                                                <img alt="_blank" src="Img/icons/check2.png" />
                                                <anthem:Label ID="lblDocumentosPersona" runat="server">Mis Documentos</anthem:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdFormulario01">
                                                <anthem:GridView ID="gvwRepositorio" runat="server" ShowHeader="false" AutoUpdateAfterCallBack="true"
                                                    AutoGenerateColumns="False" Width="100%">
                                                    <RowStyle CssClass="GridItem" />
                                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <AlternatingRowStyle CssClass="GridAtlItem" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <img alt="_blank" src="Img/icons/folder.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <anthem:LinkButton runat="server" ID="lnkBanRepDocNombreArchivo" Text='<%# Eval("GrupoDocumento")%>'
                                                                    CommandArgument="GrupoDocumentoSolicitud" AutoUpdateAfterCallBack="false" OnClick="lnkBanRepDocNombreArchivo_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                (<anthem:Label ID="lblGrupoCantidad0" runat="server" Text='<%# Bind("Cantidad") %>' />)
                                                            </ItemTemplate>
                                                            <ItemStyle Font-Bold="True"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAgrupacionValidada" runat="server" AutoPostBack="True" />
                                                <asp:Label ID="lblIdGrupoDocumento" runat="server" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                </anthem:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdFormulario02">
                                                <asp:Image ID="imgVigentePersona" runat="server" 
                                                    ImageUrl="~/Img/IconGreen.png" />
                                                &nbsp;Vigente
                                                <asp:Image ID="imgPorVencerPersona" runat="server" 
                                                    ImageUrl="~/Img/IconYellow.png" />
                                                &nbsp;Por Vencer
                                                <asp:Image ID="imgVencidoPersona" runat="server" 
                                                    ImageUrl="~/Img/IconRed.png" />
                                                &nbsp;Vencido</td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="tdTitulo01" valign="center">
                                                <img alt="_blank" src="Img/icons/folder.png" />
                                                <anthem:LinkButton ID="lnkSubirDocumentoRep" runat="server" Text="Subir Documento"
                                                    CssClass="tdTitulo01" OnClientClick="MostrarFormularioPortalRep();" ForeColor="White"></anthem:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <div id="divFormularioEditarRep" runat="server">
                                                    <table class="tdFormulario01" width="100%">
                                                        <tr>
                                                            <td>
                                                                Participante
                                                            </td>
                                                            <td>
                                                                <anthem:DropDownList ID="ddlParticipantesRep" runat="server" AutoUpdateAfterCallBack="true"
                                                                    AutoCallBack="true" onchange="clearFileUploadRep();">
                                                                </anthem:DropDownList>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Grupo
                                                            </td>
                                                            <td>
                                                                <anthem:DropDownList ID="ddlGrupoDocumentoRep" runat="server" AutoUpdateAfterCallBack="true"
                                                                    AutoCallBack="true" OnSelectedIndexChanged="ddlGrupoDocumentoRep_SelectedIndexChanged"
                                                                    onchange="clearFileUploadRep();">
                                                                </anthem:DropDownList>
                                                            </td>
                                                            <td>
                                                                Fecha emisión
                                                            </td>
                                                            <td>
                                                                <anthem:TextBox ID="txtFechaEmisionRep" runat="server" AutoUpdateAfterCallBack="true" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Tipo de documento
                                                            </td>
                                                            <td colspan="3">
                                                                <anthem:DropDownList ID="ddlTipoDocumentoRep" runat="server" AutoUpdateAfterCallBack="true"
                                                                    AutoCallBack="true" OnSelectedIndexChanged="ddlTipoDocumentoRep_SelectedIndexChanged"
                                                                    onchange="clearFileUploadRep();">
                                                                </anthem:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Archivo
                                                            </td>
                                                            <td colspan="3">
                                                                <anthem:FileUpload ID="fluArchivoRep" runat="server" AutoUpdateAfterCallBack="true" />
                                                                <%--<asp:TemplateField ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAgrupacionValidada" runat="server" AutoPostBack="True" />
                                                <asp:Label ID="lblIdGrupoDocumento" runat="server" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%><%--<asp:TemplateField ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAgrupacionValidada" runat="server" AutoPostBack="True" />
                                                <asp:Label ID="lblIdGrupoDocumento" runat="server" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Observaciones
                                                            </td>
                                                            <td colspan="3">
                                                                <anthem:TextBox ID="txtObservacionRep" runat="server" Width="300px" AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" style="text-align: center">
                                                                <anthem:Button ID="btnGuardarRep" runat="server" Text="Guardar" Visible="false" TextDuringCallBack="Guardando..."
                                                                    CssClass="myButton2" AutoUpdateAfterCallBack="true" OnClick="btnGuardarRep_Click" />
                                                                &nbsp;<anthem:Button ID="btnCancelarRep" runat="server" Text="Cancelar" TextDuringCallBack="Cancelando..."
                                                                    CssClass="myButton2" OnClick="btnCancelarRep_Click" OnClientClick="MostrarFormularioPortalRep();" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdTitulo02">
                                                Búsqueda de Documentos
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table class="tdFormulario01" width="100%">
                                                    <tr>
                                                        <td>
                                                            Grupo de Documento
                                                        </td>
                                                        <td>
                                                            <anthem:DropDownList ID="ddlGrupoDocumentoRepFil" runat="server" AutoUpdateAfterCallBack="true"
                                                                EnableCallBack="true" AutoCallBack="true" OnSelectedIndexChanged="ddlGrupoDocumentoRepFil_SelectedIndexChanged" />
                                                        </td>
                                                        <td>
                                                            Tipo de documento
                                                        </td>
                                                        <td>
                                                            <anthem:DropDownList ID="ddlTipoDocumentoRepFil" runat="server" AutoUpdateAfterCallBack="true"
                                                                EnableCallBack="true" AutoCallBack="true" OnSelectedIndexChanged="ddlTipoDocumentoSolicitudBus_SelectedIndexChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:GridView ID="gvwRepositorioDocumento" runat="server" AutoGenerateColumns="False"
                                                    AllowPaging="True" AllowSorting="True" AutoUpdateAfterCallBack="True" Width="100%"
                                                    OnRowDataBound="gvwRepositorioDocumento_RowDataBound" OnPageIndexChanging="gvwRepositorioDocumento_PageIndexChanging"
                                                    CellPadding="4" UpdateAfterCallBack="True">
                                                    <RowStyle CssClass="GridItem" />
                                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <AlternatingRowStyle CssClass="GridAtlItem" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <anthem:Image ID="imgEstadoAlertaRepDoc" runat="server" Height="16px" Width="16px" />
                                                                <anthem:HiddenField ID="hdnEstadoAlertaRepDoc" runat="server" Value='<%# Eval("EstadoAlertaDoc") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Participante" DataField="NombreParticipante" />
                                                        <asp:TemplateField HeaderText="Nombre">
                                                            <ItemTemplate>
                                                                <anthem:LinkButton runat="server" ID="lnkNombreArchivoRepDoc" Text='<%# (Eval("NombreArchivo").ToString().Length >= 18) ? Eval("NombreArchivo").ToString().Substring(0, 17) + "..." : Eval("NombreArchivo")%>'
                                                                    OnClick="lnkNombreArchivoRepDoc_Click" EnableCallBack="false" AutoUpdateAfterCallBack="false"
                                                                    ToolTip='<%# (Eval("NombreArchivo").ToString())%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <anthem:Image ID="imgTipoDocumentoRepDoc" runat="server" Height="16px" Width="16px" />
                                                                <anthem:HiddenField ID="hdnTipoDocumentoRepDoc" runat="server" Value='<%# Eval("ExtensionArchivo") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Fecha" DataField="FechaEmision" DataFormatString="{0:dd-MM-yyyy}" />
                                                        <asp:TemplateField HeaderText="Grupo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGrupoRepDoc" runat="server" Text='<%# (Eval("GrupoDocumento").ToString().Length >= 20) ? Eval("GrupoDocumento").ToString().Substring(0, 19) + "..." : Eval("GrupoDocumento")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTipoRepDoc" runat="server" Text='<%# (Eval("TipoDocumento").ToString().Length >= 20) ? Eval("TipoDocumento").ToString().Substring(0, 19) + "..." : Eval("TipoDocumento")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <anthem:ImageButton ID="btnAsignarRepDoc" alt="Asignar a Solicitud" runat="server"
                                                                    ImageUrl="~/Img/Asignar.png" Height="20px" Width="20px" OnMouseDown="javascript:MensajeConfirmacion('Esta Acción Asignará el Documento a la Solicitud  ¿Seguro que desea continuar con la Operación?',this);"
                                                                    OnClick="btnAsignarRepDoc_Click" />
                                                                <anthem:ImageButton ID="btnDescargarRepDoc" alt="Descargar" runat="server" ImageUrl="~/Img/DocDownload.png"
                                                                    EnableCallBack="false" AutoUpdateAfterCallBack="false" Height="20px" Width="20px"
                                                                    OnClick="btnDescargarRepDoc_Click" />
                                                                <anthem:ImageButton ID="btnEditarRepDoc" alt="Editar" runat="server" ImageUrl="~/Img/DocEdit.png"
                                                                    Height="20px" Width="20px" OnClientClick="MostrarFormularioPortalEditarRep();"
                                                                    OnClick="btnEditarRepDoc_Click" />
                                                                <anthem:ImageButton ID="btnEliminarRepDoc" alt="Eliminar" runat="server" ImageUrl="~/Img/DocDelete.png"
                                                                    Height="20px" Width="20px" OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea Eliminar el Registro?',this);"
                                                                    OnClick="btnEliminarRepDoc_Click" />
                                                                <anthem:HiddenField ID="IdRepDoc" runat="server" Value='<%# Eval("Id")%>' />
                                                                <anthem:HiddenField ID="Rut" runat="server" Value='<%# Eval("Rut")%>' />
                                                                <anthem:HiddenField ID="hdnPermisoDescargarRep" runat="server" Value='<%# Eval("PermisoDescargar")%>' />
                                                                <anthem:HiddenField ID="hdnPermisoVerRep" runat="server" Value='<%# Eval("PermisoVer")%>' />
                                                                <anthem:HiddenField ID="hdnPermisoModificarRep" runat="server" Value='<%# Eval("PermisoModificar")%>' />
                                                                <anthem:HiddenField ID="hdnPermisoEliminarRep" runat="server" Value='<%# Eval("PermisoEliminar")%>' />
                                                                <anthem:HiddenField ID="hdnPermisoSubirRep" runat="server" Value='<%# Eval("PermisoSubir")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </anthem:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--<asp:TemplateField ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAgrupacionValidada" runat="server" AutoPostBack="True" />
                                                <asp:Label ID="lblIdGrupoDocumento" runat="server" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%><%--<asp:TemplateField ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAgrupacionValidada" runat="server" AutoPostBack="True" />
                                                <asp:Label ID="lblIdGrupoDocumento" runat="server" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                    <div id="tabs3">
                        <table style="width: 100%;">
                            <tr>
                                <td class="tdTitulo02">
                                    Bitacora
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" height="450px">
                                    <anthem:GridView ID="gvwBitacora" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvwBitacora_RowDataBound"
                                        CellPadding="0" EmptyDataText="Sin Movimientos" Width="100%">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Fecha" DataField="FechaHoraEvento"></asp:BoundField>
                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Descripción Evento">
                                                <ItemTemplate>
                                                    <%# (Eval("DescripcionEvento").ToString().Length >= 80) ? Eval("DescripcionEvento").ToString().Substring(0, 80) + "..." : Eval("DescripcionEvento")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre Archivo">
                                                <ItemTemplate>
                                                    <%# (Eval("NombreArchivo").ToString().Length >= 30) ? Eval("NombreArchivo").ToString().Substring(0, 29) + "..." : Eval("NombreArchivo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo Documento">
                                                <ItemTemplate>
                                                    <%# (Eval("TipoDocumento").ToString().Length >= 30) ? Eval("TipoDocumento").ToString().Substring(0, 29) + "..." : Eval("TipoDocumento")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </anthem:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <anthem:Button ID="btnExportar" runat="server" Text="Exportar" AutoUpdateAfterCallBack="true"
                                        CssClass="myButton2" EnableCallBack="false" OnClick="btnExportar_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--<asp:FileUpload ID="fluArchivo" runat="server" Width="500px" CssClass="SubirArchivo" />--%>
                </div>
                <%--<asp:TemplateField ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAgrupacionValidada" runat="server" AutoPostBack="True" />
                                                <asp:Label ID="lblIdGrupoDocumento" runat="server" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <div class="footer">
                    <img src="Img/asicom.jpg" width="150" height="24" alt="Asicom" longdesc="http://www.asicom.cl" />
                </div>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <anthem:HiddenField ID="hfId" runat="server" AutoUpdateAfterCallBack="true" />
    <anthem:HiddenField ID="hfIdRep" runat="server" AutoUpdateAfterCallBack="true" />
    <anthem:HiddenField ID="hfIdArchivo" runat="server" AutoUpdateAfterCallBack="true" />
    <anthem:HiddenField ID="hfIdArchivoRep" runat="server" AutoUpdateAfterCallBack="true" />
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
