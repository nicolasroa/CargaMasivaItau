<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Maestra.Master" AutoEventWireup="true"
    CodeBehind="ConfiguracionGeneralB.aspx.cs" Inherits="WebSite.Administracion.ConfiguracionGeneralB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 166px;
        }
        .style2
        {
            width: 246px;
        }
        .style3
        {
            width: 164px;
        }
        .style4
        {
            width: 249px;
        }
        .style9
        {
            width: 166px;
            height: 27px;
        }
        .style10
        {
            height: 27px;
        }
        .style11
        {
            width: 246px;
            height: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="tdTitulo01">
                Configuración General
            </td>
        </tr>
        <tr>
            <td class="tdTitulo02">
                Datos Generales del Sistema
                <anthem:Label AutoUpdateAfterCallBack="true" ID="lblFechaActualizacion" runat="server"></anthem:Label>
            </td>
        </tr>
        <tr>
            <td class="tdTitulo03">
                Validación de Usuario
            </td>
        </tr>
        <tr>
            <td class="tdFormulario01">
                <table style="width: 100%;">
                    <tr>
                        <td class="style9">
                            Tamaño Mínimo de la Clave:
                        </td>
                        <td class="style10">
                            <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtTamanioClave" runat="server"></anthem:TextBox>
                        </td>
                        <td class="style11">
                            N° de Intentos Fallidos de Inicio de Session:
                        </td>
                        <td class="style10">
                            <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtIntentos" runat="server"></anthem:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Validez de la Clave:
                        </td>
                        <td>
                            <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtPlazoValidez" runat="server"></anthem:TextBox>
                            Dias
                        </td>
                        <td class="style2">
                            Notificar Caducidad de la Clave a los:
                        </td>
                        <td>
                            <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtNotificacion" runat="server"></anthem:TextBox>
                            Dias
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdTitulo03">
                Correo Electrónico del Sistema
            </td>
        </tr>
        <tr>
            <td class="tdFormulario01">
                <table style="width: 100%;">
                    <tr>
                        <td class="style3">
                            Usuario:
                        </td>
                        <td>
                            <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtUsuarioCorreo" 
                                runat="server" Width="400px"></anthem:TextBox>
                        </td>
                        <td class="style4">
                            Clave:
                        </td>
                        <td>
                            <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtClaveCorreo" runat="server"></anthem:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            Dirección de Correo:
                        </td>
                        <td>
                            <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtCorreo" runat="server" 
                                Width="400px"></anthem:TextBox>
                        </td>
                        <td class="style4">
                            Puerto:
                        </td>
                        <td>
                            <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtPuertoCorreo" runat="server"></anthem:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            Servidor de Entrada:
                        </td>
                        <td>
                            <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtServidorEntradaCorreo" runat="server"></anthem:TextBox>
                        </td>
                        <td class="style4">
                            Servidor de Salida:
                        </td>
                        <td>
                            <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtServidorSalidaCorreo" runat="server"></anthem:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            Conexion Segura:
                        </td>
                        <td>
                            <asp:CheckBox ID="chkConexion" runat="server" />
                        </td>
                        <td class="style4">
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
            <td class="tdTitulo03">
                Portal
            </td>
        </tr>
        <tr>
            <td class="tdFormulario01">
                <table>
                    <tr>
                        <td class="style3">
                            Tamaño Maximo Archivo:
                        </td>
                        <td>
                            <anthem:TextBox AutoUpdateAfterCallBack="true" ID="txtTamanioArchivo" 
                                runat="server" Width="150px"></anthem:TextBox>&nbsp;KB
                        </td>
                        <td class="style4">                            
                        </td>
                        <td>                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
                <td align="center">
                    <anthem:Button  PreCallBackFunction="this.disabled = true" PostCallBackFunction="this.disabled = false" EnabledDuringCallBack="False"  ID="btnGuardar" runat="server" CssClass="myButton" TextDuringCallBack="Guardando..."
                        OnClick="btnGuardar_Click" Text="Guardar" />
                </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hfId" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
