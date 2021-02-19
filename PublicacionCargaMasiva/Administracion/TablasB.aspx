<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Maestra.Master" AutoEventWireup="true"
    CodeBehind="TablasB.aspx.cs" Inherits="WebSite.Administracion.TablasB" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/jscript">

        function Inicio() {
            //$('#<%=gvBusqueda.ClientID %>').Scrollable({
            //  ScrollHeight: 400,
            //Width: 0
            //});
        }
    </script>
    <table style="width: 100%;">
        <tr>
            <td class="tdTitulo01">
                Maestros Generales
            </td>
        </tr>
        <tr>
            <td class="tdTitulo03">
                <anthem:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Img/icons/document_new.png" />
                <anthem:LinkButton CssClass="link" ID="LinkButton1" runat="server" OnClick="btnNuevoRegistro_Click">Nuevo Registro</anthem:LinkButton>
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
                                    Nombre:
                                </td>
                                <td>
                                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtNombre" runat="server" Width="200px"></anthem:TextBox>
                                </td>
                                <td>
                                    Código:
                                </td>
                                <td>
                                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtCodigo" runat="server"></anthem:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Nombre Tabla Padre:
                                </td>
                                <td>
                                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtNombreTablaPadre" runat="server"
                                        Width="200px"></anthem:TextBox>
                                </td>
                                <td>
                                    Estado:
                                </td>
                                <td>
                                    <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlEstado" runat="server">
                                    </anthem:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdFormulario01" align="center">
                        <anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnBuscar" CssClass="myButton" runat="server" Text="Buscar" OnClick="btnBuscar_Click"
                            TextDuringCallBack="Buscando..." />
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
                            PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBusqueda_PageIndexChanging"
                            AutoUpdateAfterCallBack="true">
                            <RowStyle CssClass="GridItem" />
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="GridHeader" />
                            <AlternatingRowStyle CssClass="GridAtlItem" />
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="NombreTablaPadre" HeaderText="Tabla Padre" />
                                <asp:BoundField DataField="NombreEstado" HeaderText="Estado" FooterStyle-Width="20%">
                                    <FooterStyle Width="20%"></FooterStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:ImageButton ID="btnModificar" runat="server" ImageUrl="~/Img/Controles/GridView/EditarIC.gif"
                                            ToolTip="Modificar" OnClick="btnModificar_Click" TextDuringCallBack="Buscando Registro..." />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ver Hijos" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:ImageButton ID="btnVerHijos" runat="server" ImageUrl="~/Img/Controles/GridView/DetalleIC.gif"
                                            ToolTip="Ver Hijos" OnClick="btnVerHijos_Click" />
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
                                    Nombre:
                                </td>
                                <td>
                                    <anthem:TextBox ID="txtFormNombre" runat="server" Width="200px" AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                </td>
                                <td>
                                    Nombre Tabla Padre:
                                </td>
                                <td>
                                    <anthem:TextBox ID="txtFormNombrePadre" runat="server" Width="200px" Enabled="False"
                                        AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Código:
                                </td>
                                <td>
                                    <anthem:TextBox ID="txtFormCodigo" runat="server" AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                </td>
                                <td>
                                    Estado:
                                </td>
                                <td>
                                    <anthem:DropDownList ID="ddlFormEstado" runat="server" AutoUpdateAfterCallBack="true">
                                    </anthem:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Concepto:
                                </td>
                                <td>
                                    <anthem:TextBox ID="txtFormConcepto" runat="server" AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                </td>
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
                        <anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnGuardar" runat="server" CssClass="myButton"
                            Text="Guardar" OnClick="btnGuardar_Click" TextDuringCallBack="Guardando..." />
                        &nbsp;<anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnCancelar" runat="server" Text="Cancelar" CssClass="myButton"
                            OnClick="btnCancelar_Click" TextDuringCallBack="Cancelando..." />
                    </td>
                </tr>
                <tr>
                    <td>
                        <anthem:HiddenField ID="hfId" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />
                        <anthem:HiddenField ID="hfTablaPadreId" runat="server" Visible="False" AutoUpdateAfterCallBack="true" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
