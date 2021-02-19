<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Maestra.Master" AutoEventWireup="true"
    CodeBehind="AsignacionMenu.aspx.cs" Inherits="WebSite.Administracion.AsignacionMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     
    <script type="text/jscript">

        function Inicio() {

        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 55px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="tdTitulo01">
                Asignación de Menu
            </td>
        </tr>
        <tr>
            <td class="tdFormulario01">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            Rol:
                        </td>
                        <td>
                            <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlRol" runat="server">
                            </anthem:DropDownList>
                        </td>
                        <td>
                            Sección:
                        </td>
                        <td>
                            <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlMenu" runat="server">
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
                Asignación de Mantenedores
            </td>
        </tr>
        <tr>
            <td class="tdFormulario01">
                <anthem:GridView runat="server" ID="gvMantenedores" AutoGenerateColumns="false" Width="100%"
                    AutoUpdateAfterCallBack="true">
                    <RowStyle CssClass="GridItem" />
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="GridAtlItem" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <anthem:ImageButton ID="imgBotonExpandir" runat="server" ImageUrl="~/Img/Controles/GridView/plus.gif"
                                    CommandName="Expandir" OnClick="imgBotonExpandir_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAcceso" runat="server" Text=""></asp:CheckBox>
                            </ItemTemplate>
                            <HeaderTemplate>
                                <anthem:Label ID="lblTituloAcceso" runat="server">Acceso</anthem:Label>
                                <br></br>
                                <asp:CheckBox ID="chkAccesoTodos" Text="SeleccionarTodos" runat="server" OnCheckedChanged="chkAccesoTodos_CheckedChanged"
                                    AutoPostBack="true"></asp:CheckBox>
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCrear" runat="server" Text=""></asp:CheckBox>
                            </ItemTemplate>
                            <HeaderTemplate>
                                <anthem:Label ID="lblTituloCrear" runat="server">Permiso para Crear</anthem:Label>
                                <br></br>
                                <asp:CheckBox ID="chkCrearTodos" Text="Seleccionar Todos" runat="server" OnCheckedChanged="chkCrearTodos_CheckedChanged"
                                    AutoPostBack="true"></asp:CheckBox>
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkModificar" runat="server" Text=""></asp:CheckBox>
                            </ItemTemplate>
                            <HeaderTemplate>
                                <anthem:Label ID="lblTituloModificar" runat="server">Permiso para Modificar</anthem:Label>
                                <br></br>
                                <asp:CheckBox ID="chkModificarTodos" Text="Seleccionar Todos" runat="server" OnCheckedChanged="chkModificarTodos_CheckedChanged"
                                    AutoPostBack="true"></asp:CheckBox>
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Permiso para Eliminar">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEliminar" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <HeaderTemplate>
                                <anthem:Label ID="lblTituloEliminar" runat="server">Permiso para Eliminar</anthem:Label>
                                <br></br>
                                <asp:CheckBox ID="chkEliminarTodos" Text="Seleccionar Todos" runat="server" OnCheckedChanged="chkEliminarTodos_CheckedChanged"
                                    AutoPostBack="true"></asp:CheckBox>
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:PlaceHolder ID="Operacionales" runat="server" Visible="false">
                                    <%  Response.Write("</td></tr>");
                                        Response.Write("<tr><td width='9'>&nbsp;</td>");
                                        Response.Write("<td colspan='8'>");%>
                                    <anthem:GridView ID="gvHijosOperacionales" runat="server" AutoGenerateColumns="False"
                                        AutoUpdateAfterCallBack="true" CssClass="Grid" PageSize="18" UpdateAfterCallBack="True"
                                        Width="60%" TotalRecordString="" UseCoolPager="False" Height="16px" ShowHeader="True">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="Descripcion" HeaderText="Operacionales de Nivel 3" />
                                            <asp:TemplateField HeaderText="Asignar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAcceso" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </anthem:GridView>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="Mantenedores" runat="server" Visible="false">
                                    <%  Response.Write("</td></tr>");
                                        Response.Write("<tr><td width='9'>&nbsp;</td>");
                                        Response.Write("<td colspan='8'>");%>
                                    <anthem:GridView ID="gvHijosMantenedores" runat="server" AutoGenerateColumns="False"
                                        AutoUpdateAfterCallBack="true" CssClass="Grid" PageSize="18" UpdateAfterCallBack="True"
                                        Width="60%" TotalRecordString="" UseCoolPager="False" Height="16px" ShowHeader="True">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="Descripcion" HeaderText="Mantenedores de Nivel 3" />
                                            <asp:TemplateField HeaderText="Asignar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAcceso" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Permiso Para Crear">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCrear" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Permiso Para Modificar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkModificar" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Permiso Para Eliminar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEliminar" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </anthem:GridView>
                                </asp:PlaceHolder>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </anthem:GridView>
            </td>
        </tr>
        <tr>
            <td class="tdTitulo02">
                &nbsp; Asignacion de Operacionales
            </td>
        </tr>
        <tr>
            <td class="tdFormulario01">
                <anthem:GridView ID="gvOperacionales" runat="server" AutoGenerateColumns="false"
                    Width="100%" AutoUpdateAfterCallBack="true">
                    <RowStyle CssClass="GridItem" />
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="GridAtlItem" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <anthem:ImageButton ID="imgBotonExpandir" runat="server" ImageUrl="~/Img/Controles/GridView/plus.gif"
                                    CommandName="Expandir" OnClick="imgBotonExpandirOperacionales_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Descripcion" HeaderText="Descrición" />
                        <asp:TemplateField HeaderText="Acceso">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAcceso" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <HeaderTemplate>
                                <anthem:Label ID="lblTituloAccesoOp" runat="server">Acceso</anthem:Label>
                                <br></br>
                                <asp:CheckBox ID="chkAcceso2Todos" Text="Seleccionar Todos" runat="server" OnCheckedChanged="chkAcceso2Todos_CheckedChanged"
                                    AutoPostBack="true" />
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <anthem:PlaceHolder ID="Operacionales" runat="server" Visible="false" AutoUpdateAfterCallBack="true">
                                    <%  Response.Write("</td></tr>");
                                        Response.Write("<tr><td width='9'>&nbsp;</td>");
                                        Response.Write("<td colspan='8'>");%>
                                    <anthem:GridView ID="gvHijosOperacionales" runat="server" AutoGenerateColumns="False"
                                        AutoUpdateAfterCallBack="true" CssClass="Grid" PageSize="18" UpdateAfterCallBack="True"
                                        Width="60%" TotalRecordString="" UseCoolPager="False" Height="16px" ShowHeader="True">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="SubGridHeader" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="Descripcion" HeaderText="Operacionales de Nivel 3" />
                                            <asp:TemplateField HeaderText="Asignar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAcceso" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </anthem:GridView>
                                </anthem:PlaceHolder>
                                <anthem:PlaceHolder ID="Mantenedores" runat="server" Visible="false" AutoUpdateAfterCallBack="true">
                                    <%  Response.Write("</td></tr>");
                                        Response.Write("<tr><td width='9'>&nbsp;</td>");
                                        Response.Write("<td colspan='8'>");%>
                                    <anthem:GridView ID="gvHijosMantenedores" runat="server" AutoGenerateColumns="False"
                                        AutoUpdateAfterCallBack="true" CssClass="Grid" PageSize="18" UpdateAfterCallBack="True"
                                        Width="60%" TotalRecordString="" UseCoolPager="False" Height="16px" ShowHeader="True">
                                        <RowStyle CssClass="GridItem" />
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                        <HeaderStyle CssClass="SubGridHeader" />
                                        <AlternatingRowStyle CssClass="GridAtlItem" />
                                        <Columns>
                                            <asp:BoundField DataField="Descripcion" HeaderText="Mantenedores de Nivel 3" />
                                            <asp:TemplateField HeaderText="Asignar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAcceso" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Permiso Para Crear">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCrear" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Permiso Para Modificar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkModificar" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Permiso Para Eliminar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEliminar" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </anthem:GridView>
                                </anthem:PlaceHolder>
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
                    OnClick="btnProcesar_Click" Text="Procesar Asignación" TextDuringCallBack="Procesando..."
                    OnMouseDown="javascript:MensajeConfirmacion('¿Seguro que desea continuar con la Operación?',this);" />
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
