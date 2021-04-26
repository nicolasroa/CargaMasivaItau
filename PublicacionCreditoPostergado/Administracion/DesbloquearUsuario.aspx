<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Maestra.Master" AutoEventWireup="true"
    CodeBehind="DesbloquearUsuario.aspx.cs" Inherits="WebSite.Administracion.DesbloquearUsuario" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="tdTitulo01">
                Desbloqueo de Usuarios
            </td>
        </tr>
        <tr>
            <td class="tdTitulo02">
                Usuarios Bloqueados
            </td>
        </tr>
        <tr>
            <td>
                <anthem:DropDownList AutoUpdateAfterCallBack="true"  ID="ddlUsuarios" runat="server">
                </anthem:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnDesbloquea" runat="server" CssClass="myButton" TextDuringCallBack="Desbloqueando Usuario..."
                    Text="Desbloquear Usuario" OnClick="btnDesbloquea_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
