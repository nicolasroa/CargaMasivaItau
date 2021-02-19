<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Inicio2.aspx.cs"
    Inherits="Documental.Inicio2" %>

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
    <link href="Css/Blitzer/jquery-ui.css" rel="stylesheet" type="text/css" />
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
        }
        .style2
        {
            width: 582px;
        }
        .style3
        {
            height: 84px;
        }
        .style4
        {
            width: 275px;
        }
    </style>
</head>
<body onload="MostrarFormularioPortal(); MostrarFormularioPortalRep();">
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
            $("#txtFechaEmision").datepicker();
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
            <td class="style3">
                &nbsp;
            </td>
            <td class="tdLogo" valign="top">
                <table class="EncabezadoTitulo" style="width: 100%;">
                    <tr>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td>
                            Rut Cliente:
                        </td>
                        <td align="left">
                            <anthem:Label ID="lblNroIdentidad" runat="server"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td>
                            Cliente:
                        </td>
                        <td align="left">
                            <anthem:Label ID="lblNombre" runat="server"></anthem:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" colspan="3">
                            N° Solicitud del Credito:&nbsp;<anthem:Label ID="lblNumSolicitud" runat="server"></anthem:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="style3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <div id="TabPrincipal">
                    <ul>
                        <li><a href="#tabs1">Mis Documentos</a></li>
                        <li><a href="#tabs2">Repositorio Identidad</a></li>
                        <li><a href="#tabs3">Historial</a></li>
                    </ul>
                    <div id="tabs1">
                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="35%" valign="top">
                                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="tdTitulo01" align="left">
                                                <img alt="_blank" src="Img/icons/check2.png" />
                                                <anthem:Label ID="lblDocumentosSolicitud" runat="server">Mis Documentos</anthem:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:GridView ID="gvGrupoDocumentosSolicitud" runat="server" ShowHeader="false"
                                                    AutoUpdateAfterCallBack="true" AutoGenerateColumns="False" Width="100%">
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
                                    </table>
                                </td>
                                <td valign="top">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                            <td class="tdTitulo01" align="left">
                                                <img alt="_blank" src="Img/icons/folder.png" />
                                                <anthem:LinkButton ID="lnkFormularioEditar" runat="server" Text="Subir Documento"
                                                    CssClass="tdTitulo01" OnClientClick="MostrarFormularioPortal();"></anthem:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divFormularioEditar" class="" runat="server">
                                                    <table cellpadding="0" cellspacing="0" class="tdFormulario01">
                                                        <tr>
                                                            <td width="150px">
                                                                Grupo
                                                            </td>
                                                            <td class="style4">
                                                                :
                                                                <anthem:DropDownList ID="ddlGrupoDocumento" runat="server" CssClass="Combo" AutoUpdateAfterCallBack="true"
                                                                    AutoCallBack="true" OnSelectedIndexChanged="ddlGrupoDocumento_SelectedIndexChanged"
                                                                    onchange="clearFileUpload();">
                                                                </anthem:DropDownList>
                                                            </td>
                                                            <td width="100px">
                                                                Fecha emisión
                                                            </td>
                                                            <td width="150px">
                                                                :
                                                                <anthem:TextBox ID="txtFechaEmision" runat="server" Columns="10" CssClass="CuadroTexto"
                                                                    AutoUpdateAfterCallBack="true" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Tipo de documento
                                                            </td>
                                                            <td colspan="3">
                                                                :
                                                                <anthem:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="Combo" AutoUpdateAfterCallBack="true"
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
                                                                :
                                                                <anthem:FileUpload ID="fluArchivo" runat="server" CssClass="SubirArchivo" AutoUpdateAfterCallBack="true" />
                                                                <%--<asp:FileUpload ID="fluArchivo" runat="server" Width="500px" CssClass="SubirArchivo" />--%>
                                                                <%--<input type="file" id="fluArchivo" class="SubirArchivo" style="border: solid 1px red"/>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Observaciones
                                                            </td>
                                                            <td colspan="3">
                                                                :<anthem:TextBox ID="txtObservacion" runat="server" AutoUpdateAfterCallBack="true"
                                                                    Width="400px"></anthem:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" style="text-align: center">
                                                                <anthem:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar"
                                                                    Visible="false" TextDuringCallBack="Guardando..." CssClass="myButton" AutoUpdateAfterCallBack="true" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<anthem:Button ID="btnCancelar" runat="server" Text="Cancelar"
                                                                    TextDuringCallBack="Cancelando..." CssClass="myButton" OnClientClick="MostrarFormularioPortal();"
                                                                    OnClick="btnCancelar_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="tdTitulo02">
                                                            Repositorio de Documentos de la Solicitud
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="tableDocumento">
                                                                <tr>
                                                                    <td style="width: 150px; text-align: right">
                                                                        Grupo de Documento
                                                                    </td>
                                                                    <td>
                                                                        <anthem:DropDownList ID="ddlGrupoDocumentoSolicitudBus" runat="server" AutoUpdateAfterCallBack="true"
                                                                            CssClass="Combo" EnableCallBack="true" AutoCallBack="true" OnSelectedIndexChanged="ddlGrupoDocumentoSolicitudBus_SelectedIndexChanged" />
                                                                    </td>
                                                                    <td style="width: 130px; text-align: right">
                                                                        Tipo de documento
                                                                    </td>
                                                                    <td>
                                                                        <anthem:DropDownList ID="ddlTipoDocumentoSolicitudBus" runat="server" CssClass="Combo"
                                                                            AutoUpdateAfterCallBack="true" EnableCallBack="true" AutoCallBack="true" OnSelectedIndexChanged="ddlTipoDocumentoSolicitudBus_SelectedIndexChanged" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="500px" valign="top">
                                                            <anthem:GridView ID="gvwDocumentosSolicitud" runat="server" AutoGenerateColumns="False"
                                                                AllowPaging="True" AllowSorting="True" AutoUpdateAfterCallBack="True" Width="100%"
                                                                OnRowDataBound="gvwDocumentosSolicitud_RowDataBound" OnPageIndexChanging="gvwDocumentosSolicitud_PageIndexChanging"
                                                                CellPadding="0" UpdateAfterCallBack="True">
                                                                <RowStyle CssClass="GridItem" />
                                                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                                <HeaderStyle CssClass="GridHeader" />
                                                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-Width="20px" ItemStyle-Width="20px">
                                                                        <ItemTemplate>
                                                                            <anthem:Image ID="imgEstadoAlertaDoc" runat="server" Height="16px" Width="16px" />
                                                                            <anthem:HiddenField ID="hdnEstadoAlertaDoc" runat="server" Value='<%# Eval("EstadoAlertaDoc") %>' />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                                        <ItemStyle Width="20px"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nombre" HeaderStyle-Width="170px" ItemStyle-Width="150px">
                                                                        <ItemTemplate>
                                                                            <anthem:LinkButton runat="server" ID="lnkNombreArchivo" Text='<%# (Eval("NombreArchivo").ToString().Length >= 18) ? Eval("NombreArchivo").ToString().Substring(0, 17) + "..." : Eval("NombreArchivo")%>'
                                                                                OnClick="lnkNombreArchivo_Click" EnableCallBack="false" AutoUpdateAfterCallBack="false" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="170px"></HeaderStyle>
                                                                        <ItemStyle Width="150px"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-Width="20px" ItemStyle-Width="20px">
                                                                        <ItemTemplate>
                                                                            <anthem:Image ID="imgTipoDocumento" runat="server" Height="16px" Width="16px" />
                                                                            <anthem:HiddenField ID="hdnTipoDocumento" runat="server" Value='<%# Eval("ExtensionArchivo") %>' />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                                        <ItemStyle Width="20px"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Fecha" DataField="FechaEmision" HeaderStyle-Width="70px"
                                                                        ItemStyle-Width="70px" DataFormatString="{0:dd-MM-yyyy}">
                                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                                        <ItemStyle Width="70px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Grupo" HeaderStyle-Width="180px" ItemStyle-Width="180px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGrupo" runat="server" Text='<%# (Eval("GrupoDocumento").ToString().Length >= 20) ? Eval("GrupoDocumento").ToString().Substring(0, 19) + "..." : Eval("GrupoDocumento")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="180px"></HeaderStyle>
                                                                        <ItemStyle Width="180px"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tipo" HeaderStyle-Width="180px" ItemStyle-Width="180px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTipo" runat="server" Text='<%# (Eval("TipoDocumento").ToString().Length >= 20) ? Eval("TipoDocumento").ToString().Substring(0, 19) + "..." : Eval("TipoDocumento")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="180px"></HeaderStyle>
                                                                        <ItemStyle Width="180px"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                            <anthem:ImageButton ID="btnDescargar" alt="Descargar" runat="server" ImageUrl="~/Img/DocDownload.png"
                                                                                EnableCallBack="false" AutoUpdateAfterCallBack="false" Height="20px" Width="20px"
                                                                                OnClick="btnDescargar_Click" />
                                                                            <anthem:ImageButton ID="btnEditarDocumento" alt="Editar" runat="server" ImageUrl="~/Img/DocEdit.png"
                                                                                Height="20px" Width="20px" OnClientClick="MostrarFormularioPortalEditar();" OnClick="btnEditarDocumento_Click" />
                                                                            <anthem:ImageButton ID="btnEliminarDocumento" alt="Eliminar" runat="server" ImageUrl="~/Img/DocDelete.png"
                                                                                Height="20px" Width="20px" OnClientClick="if (!confirm('¿Seguro que desea eliminar el documento seleccionado?')) return false;"
                                                                                OnClick="btnEliminarDocumento_Click" />
                                                                            <anthem:HiddenField ID="IdSolDoc" runat="server" Value='<%# Eval("Id")%>' />
                                                                            <anthem:HiddenField ID="hdnPermisoDescargar" runat="server" Value='<%# Eval("PermisoDescargar")%>' />
                                                                            <anthem:HiddenField ID="hdnPermisoVer" runat="server" Value='<%# Eval("PermisoVer")%>' />
                                                                            <anthem:HiddenField ID="hdnPermisoModificar" runat="server" Value='<%# Eval("PermisoModificar")%>' />
                                                                            <anthem:HiddenField ID="hdnPermisoEliminar" runat="server" Value='<%# Eval("PermisoEliminar")%>' />
                                                                            <anthem:HiddenField ID="hdnPermisoSubir" runat="server" Value='<%# Eval("PermisoSubir")%>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </anthem:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="Div1">
                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="35%" valign="top">
                                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="tdTitulo01" align="left">
                                                <img alt="_blank" src="Img/icons/check2.png" />
                                                <anthem:Label ID="Label1" runat="server">Mis Documentos</anthem:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                <anthem:GridView ID="gvwRepositorio" runat="server" ShowHeader="false" AutoUpdateAfterCallBack="true"
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="35px">
                                            <ItemTemplate>
                                                <img alt="_blank" src="Img/FolderPro.png" width="27" height="25" />
                                            </ItemTemplate>
                                            <ItemStyle Width="35px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="150px">
                                            <ItemTemplate>
                                                <anthem:LinkButton runat="server" ID="lnkBanRepDocNombreArchivo" Text='<%# Eval("GrupoDocumento")%>'
                                                    CommandArgument="GrupoDocumentoSolicitud" AutoUpdateAfterCallBack="false" OnClick="lnkBanRepDocNombreArchivo_Click" />
                                            </ItemTemplate>
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="15px">
                                            <ItemTemplate>
                                                (<anthem:Label ID="lblGrupoCantidad0" runat="server" 
                                                    Text='<%# Bind("Cantidad") %>' />)
                                            </ItemTemplate>
                                            <ItemStyle Width="15px"></ItemStyle>
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
                                    </table>
                                </td>
                                <td valign="top">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                            <td class="tdTitulo01" align="left">
                                                <img alt="_blank" src="Img/icons/folder.png" />
                                                <anthem:LinkButton ID="lnkSubirDocumentoRep" runat="server" Text="Subir Documento"
                                                    CssClass="tdTitulo01" OnClientClick="MostrarFormularioPortalRep();"></anthem:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="div2" class="" runat="server">
                                    <table cellpadding="0" cellspacing="0" class="tableContent">
                                        <tr>
                                            <td width="150px">
                                                Grupo
                                            </td>
                                            <td width="300px">
                                                :
                                                <anthem:DropDownList ID="ddlGrupoDocumentoRep" runat="server" CssClass="Combo" AutoUpdateAfterCallBack="true"
                                                    AutoCallBack="true" OnSelectedIndexChanged="ddlGrupoDocumentoRep_SelectedIndexChanged"
                                                    onchange="clearFileUploadRep();">
                                                </anthem:DropDownList>
                                            </td>
                                            <td width="100px">
                                                Fecha emisión
                                            </td>
                                            <td width="150px">
                                                :
                                                <anthem:TextBox ID="txtFechaEmisionRep" runat="server" Columns="10" CssClass="CuadroTexto"
                                                    AutoUpdateAfterCallBack="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Tipo de documento
                                            </td>
                                            <td colspan="3">
                                                :
                                                <anthem:DropDownList ID="ddlTipoDocumentoRep" runat="server" CssClass="Combo" AutoUpdateAfterCallBack="true"
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
                                                :
                                                <anthem:FileUpload ID="fluArchivoRep" runat="server" Width="500px" CssClass="SubirArchivo"
                                                    AutoUpdateAfterCallBack="true" />
                                                <%--<asp:FileUpload ID="fluArchivo" runat="server" Width="500px" CssClass="SubirArchivo" />--%>
                                                <%--<input type="file" id="fluArchivo" class="SubirArchivo" style="border: solid 1px red"/>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Observaciones
                                            </td>
                                            <td colspan="3">
                                                :
                                                <%--<anthem:TextBox ID="txtObservacionRep" runat="server" Rows="5" TextMode="MultiLine"
                                                    CssClass="CuadroTextoMultifilas" AutoUpdateAfterCallBack="true" />--%>
                                                <anthem:TextBox ID="txtObservacionRep1" runat="server" Rows="1" CssClass="CuadroTextoMultifilas"
                                                    AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center">
                                                <anthem:Button ID="btnGuardarRep" runat="server" Text="Guardar" Visible="false" TextDuringCallBack="Guardando..."
                                                    CssClass="myButton" AutoUpdateAfterCallBack="true" OnClick="btnGuardarRep_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<anthem:Button ID="btnCancelarRep" runat="server" Text="Cancelar"
                                                    TextDuringCallBack="Cancelando..." CssClass="myButton" OnClick="btnCancelarRep_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="tdTitulo02">
                                                            Repositorio de Documentos de la Solicitud
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td height="500px" valign="top">
                                                            <anthem:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                                AllowSorting="True" AutoUpdateAfterCallBack="True" Width="100%" OnRowDataBound="gvwDocumentosSolicitud_RowDataBound"
                                                                OnPageIndexChanging="gvwDocumentosSolicitud_PageIndexChanging" CellPadding="0"
                                                                UpdateAfterCallBack="True">
                                                                <RowStyle CssClass="GridItem" />
                                                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                                <HeaderStyle CssClass="GridHeader" />
                                                                <AlternatingRowStyle CssClass="GridAtlItem" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-Width="20px" ItemStyle-Width="20px">
                                                                        <ItemTemplate>
                                                                            <anthem:Image ID="imgEstadoAlertaDoc" runat="server" Height="16px" Width="16px" />
                                                                            <anthem:HiddenField ID="hdnEstadoAlertaDoc" runat="server" Value='<%# Eval("EstadoAlertaDoc") %>' />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                                        <ItemStyle Width="20px"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nombre" HeaderStyle-Width="170px" ItemStyle-Width="150px">
                                                                        <ItemTemplate>
                                                                            <anthem:LinkButton runat="server" ID="lnkNombreArchivo" Text='<%# (Eval("NombreArchivo").ToString().Length >= 18) ? Eval("NombreArchivo").ToString().Substring(0, 17) + "..." : Eval("NombreArchivo")%>'
                                                                                OnClick="lnkNombreArchivo_Click" EnableCallBack="false" AutoUpdateAfterCallBack="false" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="170px"></HeaderStyle>
                                                                        <ItemStyle Width="150px"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-Width="20px" ItemStyle-Width="20px">
                                                                        <ItemTemplate>
                                                                            <anthem:Image ID="imgTipoDocumento" runat="server" Height="16px" Width="16px" />
                                                                            <anthem:HiddenField ID="hdnTipoDocumento" runat="server" Value='<%# Eval("ExtensionArchivo") %>' />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                                        <ItemStyle Width="20px"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Fecha" DataField="FechaEmision" HeaderStyle-Width="70px"
                                                                        ItemStyle-Width="70px" DataFormatString="{0:dd-MM-yyyy}">
                                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                                        <ItemStyle Width="70px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Grupo" HeaderStyle-Width="180px" ItemStyle-Width="180px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGrupo" runat="server" Text='<%# (Eval("GrupoDocumento").ToString().Length >= 20) ? Eval("GrupoDocumento").ToString().Substring(0, 19) + "..." : Eval("GrupoDocumento")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="180px"></HeaderStyle>
                                                                        <ItemStyle Width="180px"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tipo" HeaderStyle-Width="180px" ItemStyle-Width="180px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTipo" runat="server" Text='<%# (Eval("TipoDocumento").ToString().Length >= 20) ? Eval("TipoDocumento").ToString().Substring(0, 19) + "..." : Eval("TipoDocumento")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="180px"></HeaderStyle>
                                                                        <ItemStyle Width="180px"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-Width="80px">
                                                                        <ItemTemplate>
                                                                            <anthem:ImageButton ID="btnDescargar" alt="Descargar" runat="server" ImageUrl="~/Img/DocDownload.png"
                                                                                EnableCallBack="false" AutoUpdateAfterCallBack="false" Height="20px" Width="20px"
                                                                                OnClick="btnDescargar_Click" />
                                                                            <anthem:ImageButton ID="btnEditarDocumento" alt="Editar" runat="server" ImageUrl="~/Img/DocEdit.png"
                                                                                Height="20px" Width="20px" OnClientClick="MostrarFormularioPortalEditar();" OnClick="btnEditarDocumento_Click" />
                                                                            <anthem:ImageButton ID="btnEliminarDocumento" alt="Eliminar" runat="server" ImageUrl="~/Img/DocDelete.png"
                                                                                Height="20px" Width="20px" OnClientClick="if (!confirm('¿Seguro que desea eliminar el documento seleccionado?')) return false;"
                                                                                OnClick="btnEliminarDocumento_Click" />
                                                                            <anthem:HiddenField ID="IdSolDoc" runat="server" Value='<%# Eval("Id")%>' />
                                                                            <anthem:HiddenField ID="hdnPermisoDescargar" runat="server" Value='<%# Eval("PermisoDescargar")%>' />
                                                                            <anthem:HiddenField ID="hdnPermisoVer" runat="server" Value='<%# Eval("PermisoVer")%>' />
                                                                            <anthem:HiddenField ID="hdnPermisoModificar" runat="server" Value='<%# Eval("PermisoModificar")%>' />
                                                                            <anthem:HiddenField ID="hdnPermisoEliminar" runat="server" Value='<%# Eval("PermisoEliminar")%>' />
                                                                            <anthem:HiddenField ID="hdnPermisoSubir" runat="server" Value='<%# Eval("PermisoSubir")%>' />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </anthem:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    &nbsp;<div class="container">
        <div class="ContenidoDocumentoEncabezado">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="tdLogo" height="100px">
                        <table cellpadding="0" cellspacing="0" border="0" class="EncabezadoTitulo">
                            <tr>
                                <td style="width: 500px">
                                    &nbsp;
                                </td>
                                <td style="width: 160px; font-size: 13px" clientidmode="AutoID">
                                    N° Identidad:
                                </td>
                                <td align="right" style="text-align: right">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="font-size: 13px;">
                                    Nombre:
                                </td>
                                <td align="right" style="text-align: right">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
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
                                    &nbsp;
                                </td>
                                <td colspan="2" style="text-align: right">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <%--  <div id="Contenido" class="ContenidoDocumento">--%>
        <div id="tabs" class="TabsPrincipal">
            <div id="tabs2">
                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2">
                            <div id="Line3" class="lineTab">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="BandejaGrupoDoc">
                                <div class="encabezado">
                                    <h1 style="color: #EAECF4">
                                        <img alt="_blank" src="Img/icons/check.png" width="17" height="18" />
                                        Repositorio de Documentos
                                    </h1>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div id="dvRepositoriodoc" class="MantencionGrupoDoc">
                                <div class="EncabezadoFormularioEditar">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <img alt="_blank" src="Img/FolderPro.png" width="27" height="25" />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="divFormularioEditarRep" class="divtableContent" runat="server">
                                </div>
                                <div class="PanelDocumentosRepositorio">
                                    <div class="PanelFiltroDocumentos">
                                        <h2>
                                            Repositorio de Documentos de la Persona</h2>
                                    </div>
                                    <div style="width: 730px; z-index: 0; _position: relative" class="MarcoGrilla">
                                        <anthem:GridView ID="gvwRepositorioDocumento" runat="server" AutoGenerateColumns="False"
                                            AllowPaging="True" AllowSorting="True" AutoUpdateAfterCallBack="True" Width="730px"
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
                                                    <HeaderStyle Width="10px"></HeaderStyle>
                                                    <ItemStyle Width="10px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nombre">
                                                    <ItemTemplate>
                                                        <anthem:LinkButton runat="server" ID="lnkNombreArchivoRepDoc" Text='<%# (Eval("NombreArchivo").ToString().Length >= 18) ? Eval("NombreArchivo").ToString().Substring(0, 17) + "..." : Eval("NombreArchivo")%>'
                                                            OnClick="lnkNombreArchivoRepDoc_Click" EnableCallBack="false" AutoUpdateAfterCallBack="false" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="150px"></HeaderStyle>
                                                    <ItemStyle Width="150px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="10px" ItemStyle-Width="10px">
                                                    <ItemTemplate>
                                                        <anthem:Image ID="imgTipoDocumentoRepDoc" runat="server" Height="16px" Width="16px" />
                                                        <anthem:HiddenField ID="hdnTipoDocumentoRepDoc" runat="server" Value='<%# Eval("ExtensionArchivo") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="10px"></HeaderStyle>
                                                    <ItemStyle Width="10px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Fecha" DataField="FechaEmision" DataFormatString="{0:dd-MM-yyyy}">
                                                    <HeaderStyle Width="70px"></HeaderStyle>
                                                    <ItemStyle Width="70px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Grupo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGrupoRepDoc" runat="server" Text='<%# (Eval("GrupoDocumento").ToString().Length >= 20) ? Eval("GrupoDocumento").ToString().Substring(0, 19) + "..." : Eval("GrupoDocumento")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="140px"></HeaderStyle>
                                                    <ItemStyle Width="140px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTipoRepDoc" runat="server" Text='<%# (Eval("TipoDocumento").ToString().Length >= 20) ? Eval("TipoDocumento").ToString().Substring(0, 19) + "..." : Eval("TipoDocumento")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="140px"></HeaderStyle>
                                                    <ItemStyle Width="140px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="90px">
                                                    <ItemTemplate>
                                                        <anthem:ImageButton ID="btnAsignarRepDoc" alt="Asignar a Solicitud" runat="server"
                                                            ImageUrl="~/Img/Asignar.png" Height="20px" Width="20px" OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea continuar con la Operación?',this);"
                                                            OnClick="btnAsignarRepDoc_Click" />
                                                        <anthem:ImageButton ID="btnDescargarRepDoc" alt="Descargar" runat="server" ImageUrl="~/Img/DocDownload.png"
                                                            EnableCallBack="false" AutoUpdateAfterCallBack="false" Height="20px" Width="20px"
                                                            OnClick="btnDescargarRepDoc_Click" />
                                                        <anthem:ImageButton ID="btnEditarRepDoc" alt="Editar" runat="server" ImageUrl="~/Img/DocEdit.png"
                                                            Height="20px" Width="20px" OnClientClick="MostrarFormularioPortalEditarRep();"
                                                            OnClick="btnEditarRepDoc_Click" />
                                                        <anthem:ImageButton ID="btnEliminarRepDoc" alt="Eliminar" runat="server" ImageUrl="~/Img/DocDelete.png"
                                                            Height="20px" Width="20px" OnClientClick=" if (!confirm('¿Seguro que desea eliminar el documento seleccionado?')) return false;"
                                                            OnClick="btnEliminarRepDoc_Click" />
                                                        <anthem:HiddenField ID="IdRepDoc" runat="server" Value='<%# Eval("Id")%>' />
                                                        <anthem:HiddenField ID="Rut" runat="server" Value='<%# Eval("Rut")%>' />
                                                        <anthem:HiddenField ID="hdnPermisoDescargarRep" runat="server" Value='<%# Eval("PermisoDescargar")%>' />
                                                        <anthem:HiddenField ID="hdnPermisoVerRep" runat="server" Value='<%# Eval("PermisoVer")%>' />
                                                        <anthem:HiddenField ID="hdnPermisoModificarRep" runat="server" Value='<%# Eval("PermisoModificar")%>' />
                                                        <anthem:HiddenField ID="hdnPermisoEliminarRep" runat="server" Value='<%# Eval("PermisoEliminar")%>' />
                                                        <anthem:HiddenField ID="hdnPermisoSubirRep" runat="server" Value='<%# Eval("PermisoSubir")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="90px"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </anthem:GridView>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="tabs4" style="padding: 0px 0px 0px 0px; border: 0px; width: 950px;">
                <table width="950px" cellpadding="0" cellspacing="0" border="0px" style="padding: 0px 0px 0px 0px;">
                    <tr>
                        <td colspan="2">
                            <div id="line1" class="lineTab">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div id="tabs22" style="padding: 0px 0px 0px 0px; border: 0px; width: 950px;">
                <table width="950px" cellpadding="0" cellspacing="0" border="0px" style="padding: 0px 0px 0px 0px;">
                    <tr>
                        <td colspan="2">
                            <div id="Line2" class="lineTab">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="PanelDocumentosBitacora">
                                <div class="PanelFiltroDocumentosBitacora">
                                    <h2>
                                        Bitácora de Documentos</h2>
                                </div>
                                <div style="width: 950; height: 470px; z-index: 0; _position: relative" class="MarcoGrilla">
                                    <anthem:GridView ID="gvwBitacora" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvwBitacora_RowDataBound"
                                        CellPadding="4" EmptyDataText="Sin Movimientos">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Fecha" DataField="FechaHoraEvento">
                                                <HeaderStyle Width="80px" />
                                                <ItemStyle Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario">
                                                <HeaderStyle Width="150px" />
                                                <ItemStyle Width="150px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Descripción Evento">
                                                <ItemTemplate>
                                                    <%# (Eval("DescripcionEvento").ToString().Length >= 80) ? Eval("DescripcionEvento").ToString().Substring(0, 80) + "..." : Eval("DescripcionEvento")%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="420px" />
                                                <ItemStyle Width="420px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre Archivo">
                                                <ItemTemplate>
                                                    <%# (Eval("NombreArchivo").ToString().Length >= 20) ? Eval("NombreArchivo").ToString().Substring(0, 19) + "..." : Eval("NombreArchivo")%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="140px" />
                                                <ItemStyle Width="140px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo Documento">
                                                <ItemTemplate>
                                                    <%# (Eval("TipoDocumento").ToString().Length >= 20) ? Eval("TipoDocumento").ToString().Substring(0, 19) + "..." : Eval("TipoDocumento")%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="140px" />
                                                <ItemStyle Width="140px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </anthem:GridView>
                                </div>
                                <div class="BotonExportarExcel">
                                    <anthem:Button ID="btnExportar" runat="server" Text="Exportar Historial" AutoUpdateAfterCallBack="true"
                                        CssClass="myButton" EnableCallBack="false" OnClick="btnExportar_Click" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <%--</div>--%>
        <anthem:HiddenField ID="hfId" runat="server" AutoUpdateAfterCallBack="true" />
        <anthem:HiddenField ID="hfIdRep" runat="server" AutoUpdateAfterCallBack="true" />
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
