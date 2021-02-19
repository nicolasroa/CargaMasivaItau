<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Maestra.Master" AutoEventWireup="true" CodeBehind="MenuB.aspx.cs" Inherits="WebSite.Administracion.MenuB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="tdTitulo01">Menú
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
                                <td>Descripción:
                                </td>
                                <td>
                                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtDescripcion" runat="server"
                                        Width="200px"></anthem:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">Menú Padre:
                                </td>
                                <td>
                                    <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlMenuPadre" runat="server">
                                    </anthem:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdFormulario01" style="text-align: center;">
                        <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                            EnabledDuringCallBack="False" ID="btnBuscar" CssClass="myButton" runat="server"
                            Text="Buscar" OnClick="btnBuscar_Click" TextDuringCallBack="Buscando..." />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitulo02">Resultado de la Búsqueda:
                        <anthem:Label ID="lblContador" runat="server" AutoUpdateAfterCallBack="true"></anthem:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdGrid" align="center">
                        <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%"
                            AutoUpdateAfterCallBack="true" PageSize="10" AllowPaging="True" AllowSorting="True"
                            OnPageIndexChanging="gvBusqueda_PageIndexChanging">
                            <RowStyle CssClass="GridItem" />
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="GridHeader" />
                            <AlternatingRowStyle CssClass="GridAtlItem" />
                            <Columns>
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                <asp:BoundField DataField="DescripcionMenuPadre" HeaderText="Menú Padre Padre" />
                                <asp:BoundField DataField="DescripcionVisible" HeaderText="Estado" />
                                <asp:BoundField DataField="DescripcionMantenedor" HeaderText="Tipo" />
                                <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:ImageButton ID="btnModificar" runat="server" ImageUrl="~/Img/Controles/GridView/EditarIC.gif"
                                            ToolTip="Modificar" OnClick="btnModificar_Click" TextDuringCallBack="Buscado Registro..." />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Configurar Controles" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:ImageButton ID="btnVerControles" runat="server" ImageUrl="~/Img/Controles/GridView/ConfigEditar16.png"
                                            ToolTip="Ver Controles" OnClick="btnVerControles_Click" />
                                        &nbsp;&nbsp;
                                        <anthem:ImageButton ID="btnAsignarControles" runat="server" ImageUrl="~/Img/Controles/GridView/ConfigOriginal16.png"
                                            ToolTip="Asignar Controles" OnClick="btnAsignarControles_Click" />
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
                                <td>Descripción
                                </td>
                                <td>
                                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtFormDescripcion" runat="server"
                                        Width="200px"></anthem:TextBox>
                                </td>
                                <td>URL:
                                </td>
                                <td>
                                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtFormUrl" runat="server" Width="500px"></anthem:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Nivel:
                                </td>
                                <td>
                                    <anthem:DropDownList ID="ddlFormNivel" runat="server" AutoCallBack="true" OnSelectedIndexChanged="ddlFormNivel_SelectedIndexChanged"
                                        AutoUpdateAfterCallBack="True">
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                    </anthem:DropDownList>
                                </td>
                                <td>Orden:
                                </td>
                                <td>
                                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtFormOrden" onKeyPress="return SoloNumeros(event,this.value,0,this);" runat="server" Width="50px"></anthem:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Menú Padre:
                                </td>
                                <td>
                                    <anthem:DropDownList ID="ddlFormMenuPadre" runat="server" Enabled="False" AutoUpdateAfterCallBack="true">
                                    </anthem:DropDownList>
                                </td>
                                <td>
                                    <anthem:CheckBox AutoUpdateAfterCallBack="true" ID="chkFormVisible" runat="server"
                                        Text="Visible" />
                                </td>
                                <td>
                                    <anthem:RadioButtonList ID="rblFormTipo" runat="server" RepeatDirection="Horizontal"
                                        AutoUpdateAfterCallBack="true" RepeatLayout="Flow">
                                        <asp:ListItem Value="true">Mantenedor</asp:ListItem>
                                        <asp:ListItem Value="false">Operacional</asp:ListItem>
                                    </anthem:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <anthem:CheckBox ID="chkAdministracion" runat="server" AutoUpdateAfterCallBack="True"
                                        Text="Administración" />
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                            EnabledDuringCallBack="False" ID="btnGuardar" runat="server" CssClass="myButton"
                            Text="Guardar" TextDuringCallBack="Guardando..." OnClick="btnGuardar_Click" />
                        &nbsp;<anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                            EnabledDuringCallBack="False" ID="btnCancelar" runat="server" Text="Cancelar"
                            CssClass="myButton" OnClick="btnCancelar_Click" TextDuringCallBack="Cancelando..." />
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
</asp:Content>
