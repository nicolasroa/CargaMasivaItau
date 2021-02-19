<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Maestra.Master" AutoEventWireup="true"
    CodeBehind="UsuarioB.aspx.cs" Inherits="WebSite.Administracion.UsuarioB" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="tdTitulo01">
                Usuarios</td>
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
                        <table class="tdFormulario01" width="100%">
                            <tr>
                                <td>
                                    Nombre:
                                </td>
                                <td>
                                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtNombre" runat="server" TabIndex="1"
                                        Width="200px"></anthem:TextBox>
                                </td>
                                <td>
                                    Apellido:
                                </td>
                                <td>
                                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtApellido" runat="server" TabIndex="2"
                                        Width="200px"></anthem:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Rut:
                                </td>
                                <td>
                                    <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtRut" runat="server" MaxLength="13"
                                        TabIndex="3" onchange="validaRut(this);"></anthem:TextBox>
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
                        <anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnBuscar" CssClass="myButton" runat="server" Text="Buscar"
                            OnClick="btnBuscar_Click" TextDuringCallBack="Buscando..." />
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
                                <asp:BoundField DataField="Rut" HeaderText="Rut" />
                                <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
                                <asp:BoundField DataField="DescripcionEstado" HeaderText="Estado" />
                                <asp:BoundField DataField="Mail" HeaderText="Mail" />
                                <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <anthem:ImageButton ID="btnModificar" runat="server" ImageUrl="~/Img/Controles/GridView/EditarIC.gif"
                                            ToolTip="Modificar" OnClick="btnModificar_Click" TextDuringCallBack="Buscando Registro..." />
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
                                <td class="style10">
                                    Nombre de Usuario:
                                </td>
                                <td class="style6">
                                    <anthem:TextBox ID="txtFormNombreUsuario" runat="server" TabIndex="7" Width="435px"
                                        AutoUpdateAfterCallBack="true" MaxLength="30"></anthem:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style10">
                                    Nombre:
                                </td>
                                <td class="style6">
                                    <anthem:TextBox ID="txtFormNombre" runat="server" TabIndex="7" Width="435px" MaxLength="30"
                                        AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                </td>
                                <td class="tdTitulo03">
                                    Roles del Usuario
                                </td>
                            </tr>
                            <tr>
                                <td class="style10">
                                    Apellido:
                                </td>
                                <td class="style6">
                                    <anthem:TextBox ID="txtFormApellido" runat="server" TabIndex="8" Width="435px" MaxLength="30"
                                        AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                </td>
                                <td class="style9" rowspan="6">
                                    <anthem:ListBox ID="lstbFormRoles" runat="server" TabIndex="12" Rows="7" SelectionMode="Multiple"
                                        AutoUpdateAfterCallBack="true">
                                    </anthem:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style10">
                                    Mail:
                                </td>
                                <td class="style6">
                                    <anthem:TextBox ID="txtFormMail" runat="server" Width="250px" TabIndex="9" onblur="validarEmail(this);"
                                        AutoUpdateAfterCallBack="true" MaxLength="50"></anthem:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style10">
                                    Rut:
                                </td>
                                <td class="style6">
                                    <anthem:TextBox ID="txtFormRut" runat="server" MaxLength="13" TabIndex="10" onchange="validaRut(this);"
                                        onKeyPress="return SoloRut(event);" AutoUpdateAfterCallBack="true"></anthem:TextBox>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="style10">
                                    <anthem:CheckBox ID="chkAdministracionAplicaciones" runat="server" 
                                        AutoUpdateAfterCallBack="True" Text="Administrador de Aplicaciones" />
                                </td>
                                <td class="style6">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style10">
                                    Estado:
                                </td>
                                <td class="style6">
                                    <anthem:DropDownList ID="ddlFormEstado" TabIndex="11" runat="server" AutoUpdateAfterCallBack="true">
                                    </anthem:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnGuardar" runat="server" CssClass="myButton" Text="Guardar"
                            OnClick="btnGuardar_Click" TextDuringCallBack="Buscando..." />
                        &nbsp;<anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnCancelar" runat="server" Text="Cancelar" CssClass="myButton"
                            OnClick="btnCancelar_Click" TextDuringCallBack="Cancelando..." />
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
