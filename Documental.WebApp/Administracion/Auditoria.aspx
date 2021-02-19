<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Maestra.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="Auditoria.aspx.cs" Inherits="WebSite.Administracion.Auditoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../Scripts/Validaciones/Validacion.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/jscript">

        function Inicio() {



            $("#<%= txtFechaInicio.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 3,
                constrainInput: true, //La entrada debe cumplir con el formato
                onClose: function (selectedDate) {
                    $("#<%= txtFechaTermino.ClientID %>").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#<%= txtFechaTermino.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 3,
                onClose: function (selectedDate) {
                    $("#<%= txtFechaInicio.ClientID %>").datepicker("option", "maxDate", selectedDate);
                }
            });
        }


        
    </script>
    <table style="width: 100%;">
        <tr>
            <td class="tdTitulo01">
                Auditoria
            </td>
        </tr>
        <tr>
            <td class="tdTitulo02">
                Parametros de Búsqueda
            </td>
        </tr>
        <tr>
            <td class="tdTitulo03">
                Seleccionar Tablas a Consultar
            </td>
        </tr>
        <tr>
            <td class="tdFormulario01">
                <asp:CheckBoxList ID="chklstTablasCriticas" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="tdFormulario01">
                <table style="width: 100%; height: 54px;">
                    <tr>
                        <td>
                            Usuario:
                        </td>
                        <td>
                            <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlUsuarios" runat="server">
                            </anthem:DropDownList>
                        </td>
                        <td>
                            Fecha Inicio:
                        </td>
                        <td>
                            <anthem:TextBox runat="server" ID="txtFechaInicio" AutoUpdateAfterCallBack="true"
                                onblur="esFechaValida(this);"></anthem:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Movimiento:
                        </td>
                        <td>
                            <anthem:DropDownList AutoUpdateAfterCallBack="true" ID="ddlMovimiento" runat="server">
                                <asp:ListItem> -- Todos --</asp:ListItem>
                                <asp:ListItem Value="I">Creación de Registros</asp:ListItem>
                                <asp:ListItem Value="U">Modificación de Registros</asp:ListItem>
                                <asp:ListItem Value="D">Eliminación de Registros</asp:ListItem>
                            </anthem:DropDownList>
                        </td>
                        <td>
                            Fecha Termino:
                        </td>
                        <td>
                            <anthem:TextBox runat="server" ID="txtFechaTermino" AutoUpdateAfterCallBack="true"
                                onblur="esFechaValida(this);"></anthem:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <anthem:Button OnMouseDown="javascript:Espera(this, 'Buscando Registros');" EnabledDuringCallBack="True"
                    PostCallBackFunction="setTimeout($.unblockUI, 0);" ID="btnBuscar" CssClass="myButton"
                    runat="server" Text="Buscar" OnClick="btnBuscar_Click" TextDuringCallBack="Buscando..." />
            </td>
        </tr>
        <tr>
            <td class="tdTitulo02">
                Resultado de la Búsqueda:
                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblContador" runat="server"></anthem:Label>
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" value="Exportar" data-dropdown="#Exportar" class="ButtonExportar" />
                <div id="Exportar" class="dropdown dropdown-tip">
                    <ul class="dropdown-menu">
                        <li>
                            <anthem:ImageButton ID="Image2" runat="server" ImageUrl="~/Img/icons/xls.png" EnableCallBack="False"
                                title="Exportar a Excel" OnClick="lnkExpotarExcel_Click" />
                        </li>
                        <li class="dropdown-divider"></li>
                        <li>
                            <anthem:ImageButton ID="Image1" runat="server" ImageUrl="~/Img/icons/pdf.png" EnableCallBack="False"
                                title="Exportar a Pdf" OnClick="lnkExpotarPdf_Click" />
                        </li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr class="tdFormulario01">
            <td class="tdGrid" align="center" colspan="1" rowspan="1">
                <anthem:GridView runat="server" ID="gvBusqueda" AutoGenerateColumns="false" Width="100%" 
                    PageSize="10" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvBusqueda_PageIndexChanging"
                    AutoUpdateAfterCallBack="true">
                    <RowStyle CssClass="GridItem" />
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="GridAtlItem" />
                    <Columns>
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo de Operación" />
                        <asp:BoundField DataField="NombreTabla" HeaderText="Tabla Afectada" />
                        <asp:BoundField DataField="NombreCampo" HeaderText="Campo Afectado" />
                        <asp:BoundField DataField="DatoAnterior" HeaderText="Dato Anterior" />
                        <asp:BoundField DataField="DatoNuevo" HeaderText="Dato Nuevo" />
                        <asp:BoundField DataField="NombreCompleto" HeaderText="Responsable" />
                        <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha de la Operación"
                            DataFormatString="{0:g}" />
                    </Columns>
                </anthem:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
