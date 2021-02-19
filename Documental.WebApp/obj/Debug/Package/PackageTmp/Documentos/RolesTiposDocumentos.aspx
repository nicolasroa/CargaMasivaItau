<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Maestra.Master" AutoEventWireup="true" CodeBehind="RolesTiposDocumentos.aspx.cs" Inherits="Documental.WebApp.Documentos.RolesTiposDocumentos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table style="width: 100%;">
        <tr>
            <td class="tdTitulo01">
                Asignación Roles
            </td>
        </tr>
        <tr>
            <td class="tdFormulario01">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            Roles:
                        </td>
                        <td>
                            <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlRoles" runat="server">
                            </anthem:DropDownList>
                        </td>
                        <td>
                            Grupo Documentos:
                        </td>
                        <td>
                            <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlGrupoDocumentos" EnableCallBack="true" AutoCallBack = "true"
                                runat="server" onselectedindexchanged="ddlGrupoDocumentos_SelectedIndexChanged">
                            </anthem:DropDownList>
                        </td>
                        <td>
                            Tipo Documentos:
                        </td>
                        <td>
                            <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlTipoDocumento" runat="server">
                            </anthem:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                    EnabledDuringCallBack="False" ID="btnVerAsignacion" runat="server" CssClass="myButton"
                    Text="Ver Asignación" OnClick="btnVerAsignacion_Click" TextDuringCallBack="Buscando Asignación..." />
            </td>
        </tr>
        <tr>
            <td class="tdTitulo02">
                Asignación Tipo de Documentos
            </td>
        </tr>
        <tr>
            <td class="tdFormulario01">
                <anthem:GridView runat="server" ID="gvTiposDocumentos" AutoGenerateColumns="false" Width="100%"
                    AutoUpdateAfterCallBack="true">
                    <RowStyle CssClass="GridItem" />
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="GridAtlItem" />
                    <Columns>
                        <asp:BoundField DataField="GrupoDocumento" HeaderText="Grupo Documento" />
                        <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Documento" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <anthem:Label ID="lblPermisoListar" runat="server">Listar</anthem:Label>
                                <br>
                                <br></br>
                                <asp:CheckBox ID="chkPermisoListar" runat="server" AutoPostBack="true" 
                                    oncheckedchanged="chkPermisoListar_CheckedChanged" Text="SeleccionarTodos" />                                
                                </br>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkListar" runat="server" 
                                    Checked='<%# Eval("PermisoListar") %>' Text="" ></asp:CheckBox>
                                <asp:HiddenField ID="hdnIdGrupoDocumento" runat="server" Value='<%# Eval("IdGrupoDocumento") %>' />
                                <asp:HiddenField ID="hdnIdTipoDocumento" runat="server" Value='<%# Eval("IdTipoDocumento") %>' />
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <anthem:Label ID="lblPermisoVer" runat="server">Ver</anthem:Label>
                                <br>
                                <br></br>
                                <asp:CheckBox ID="chkPermisoVer" runat="server" AutoPostBack="true" 
                                     oncheckedchanged="chkPermisoVer_CheckedChanged" Text="SeleccionarTodos" />
                                </br>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkVer" runat="server" Checked='<%# Eval("PermisoVer") %>' Text=""></asp:CheckBox>
                            </ItemTemplate>                            
                        </asp:TemplateField>    
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <anthem:Label ID="lblPermisoAbrir" runat="server">Abrir</anthem:Label>
                                <br></br>
                                <asp:CheckBox ID="chkPermisoAbrir" Text="SeleccionarTodos" runat="server" AutoPostBack="true" oncheckedchanged="chkPermisoAbrir_CheckedChanged"></asp:CheckBox>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAbrir" runat="server" Checked='<%# Eval("PermisoAbrir") %>' Text=""></asp:CheckBox>
                            </ItemTemplate>                            
                        </asp:TemplateField> 
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <anthem:Label ID="lblPermisoModificar" runat="server">Modificar</anthem:Label>
                                <br></br>
                                <asp:CheckBox ID="chkPermisoModificar" Text="SeleccionarTodos" runat="server" AutoPostBack="true" oncheckedchanged="chkPermisoModificar_CheckedChanged"></asp:CheckBox>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkModificar" runat="server" Checked='<%# Eval("PermisoModificar") %>' Text=""></asp:CheckBox>
                            </ItemTemplate>                            
                        </asp:TemplateField> 
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <anthem:Label ID="lblPermisoDescargar" runat="server">Descargar</anthem:Label>
                                <br></br>
                                <asp:CheckBox ID="chkPermisoDescargar" Text="SeleccionarTodos" runat="server" AutoPostBack="true" oncheckedchanged="chkPermisoDescargar_CheckedChanged"></asp:CheckBox>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkDescargar" runat="server" Checked='<%# Eval("PermisoDescargar") %>' Text=""></asp:CheckBox>
                            </ItemTemplate>                            
                        </asp:TemplateField> 
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <anthem:Label ID="lblPermisoEliminar" runat="server">Eliminar</anthem:Label>
                                <br></br>
                                <asp:CheckBox ID="chkPermisoEliminar" Text="SeleccionarTodos" runat="server" AutoPostBack="true" oncheckedchanged="chkPermisoEliminar_CheckedChanged"></asp:CheckBox>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEliminar" runat="server" Checked='<%# Eval("PermisoEliminar") %>' Text=""></asp:CheckBox>
                            </ItemTemplate>                            
                        </asp:TemplateField> 
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <anthem:Label ID="lblPermisoSubir" runat="server">Subir</anthem:Label>
                                <br></br>
                                <asp:CheckBox ID="chkPermisoSubir" Text="SeleccionarTodos" runat="server" AutoPostBack="true" oncheckedchanged="chkPermisoSubir_CheckedChanged"></asp:CheckBox>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSubir" runat="server" Checked='<%# Eval("PermisoSubir") %>' Text=""></asp:CheckBox>
                            </ItemTemplate>                            
                        </asp:TemplateField> 
                    </Columns>
                </anthem:GridView>
            </td>
        </tr>        
        <tr>
            <td align="center">
                <anthem:Button PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false"
                    EnabledDuringCallBack="False" ID="btnProcesar" runat="server" CssClass="myButton"
                    Text="Procesar Asignación" TextDuringCallBack="Procesando..."                    
                    OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea continuar con la Operación?',this);" 
                    onclick="btnProcesar_Click" />
                &nbsp;
            </td>
        </tr>
    </table>
    
</asp:Content>