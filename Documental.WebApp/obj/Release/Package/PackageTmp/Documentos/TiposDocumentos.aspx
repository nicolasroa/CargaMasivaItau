<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Maestra.Master" AutoEventWireup="true" CodeBehind="TiposDocumentos.aspx.cs" Inherits="Documental.WebApp.Documentos.TiposDocumentos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table style="width: 100%;">
        <tr>
            <td class="tdTitulo01">
                Maestros Tipos Documentos
            </td>
        </tr>
        <tr>
            <td class="tdTitulo03">
                <anthem:ImageButton ID="imgAgregarRegistro" runat="server" ImageUrl="~/Img/icons/document_new.png" />
                <anthem:LinkButton CssClass="link" ID="btnNuevoRegistro" runat="server" OnClick="btnNuevoRegistro_Click">Nuevo Registro</anthem:LinkButton>
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
                        <table style="width: 100%;">
                            <tr>                                
                                <td>
                                    Codigo Tipo Documento:
                                </td>
                                <td>
                                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtCodigoTipoDocumentoB" runat="server"
                                        Width="200px"></anthem:TextBox>
                                </td>
                                <td>
                                    Tipo Documento:
                                </td>
                                <td>
                                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtTipoDocumentoB" runat="server"></anthem:TextBox>
                                </td>
                                <td>
                                    Grupos Documentos:
                                </td>
                                <td>
                                    <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlGruposDocumentosB" runat="server">
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
                            Text="Buscar" TextDuringCallBack="Buscando..." OnClick="btnBuscar_Click" />
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
                            PageSize="10" AllowPaging="True" AllowSorting="True" 
                            AutoUpdateAfterCallBack="true" onpageindexchanging="gvBusqueda_PageIndexChanging"
                            >
                            <RowStyle CssClass="GridItem" />
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="GridHeader" />
                            <AlternatingRowStyle CssClass="GridAtlItem" />
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" />
                                 <asp:BoundField DataField="GrupoDocumento" HeaderText="Grupo Documento" />
                                <asp:BoundField DataField="CodTipoDocumento" HeaderText="Codigo Tipo Documento" />
                                <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Documento"></asp:BoundField>
                                <asp:TemplateField HeaderText="Habilitado">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkHabilitadoG" runat="server" Checked='<%# Convert.ToBoolean(Eval("Estado_Id")) %>' Enabled="true"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:ImageButton ID="btnModificarTipoDocumento" runat="server" ImageUrl="~/Img/Controles/GridView/EditarIC.gif"
                                            ToolTip="Modificar" TextDuringCallBack="Buscando Registro..." 
                                            onclick="btnModificarTipoDocumento_Click"  />
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
                                    Codigo Tipo Documento:
                                </td>
                                <td>
                                    <anthem:TextBox ID="txtCodigoTipoDocumento" runat="server" Width="200px" AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                </td>
                                <td>
                                    Tipo Documento:
                                </td>
                                <td>
                                    <anthem:TextBox ID="txtTipoDocumentos" runat="server" Width="300px" AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Grupos Documentos:
                                </td>
                                <td>
                                    <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlGrupoDocumento" runat="server">
                                    </anthem:DropDownList>
                                </td>
                                <td>
                                    Habilitado:
                                </td>
                                <td>
                                    <anthem:CheckBox ID="chkHabilitado" runat="server" AutoUpdateAfterCallBack="true">
                                    </anthem:CheckBox>
                                </td>
                               
                            </tr>
                            <tr>
                                <td>
                                    Dias de Caducidad</td>
                                <td>
                                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtDiasCaducidad" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server" Width="50px"></anthem:TextBox></td>
                                <td>
                                    Dias Aviso Caducidad</td>
                                <td>
                                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtDiasAvisoCaducidad" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server" Width="50px"></anthem:TextBox></td>
                               
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                            EnabledDuringCallBack="False" ID="btnGuardar" runat="server" CssClass="myButton"
                            Text="Guardar" TextDuringCallBack="Guardando..." 
                            onclick="btnGuardar_Click"  />
                        &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                            EnabledDuringCallBack="False" ID="btnCancelar" runat="server" Text="Cancelar"
                            CssClass="myButton" TextDuringCallBack="Cancelando..." 
                            onclick="btnCancelar_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <anthem:HiddenField ID="hfId" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />
                        <%--<anthem:HiddenField ID="hfTablaPadreId" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />--%>
                    </td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>
