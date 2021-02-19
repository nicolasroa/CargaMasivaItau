<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SinInformacion.aspx.cs"
    Inherits="Documental.WebApp.SinInformacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/EstiloItau/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="Css/EstiloItau/jquery-ui.structure.css" rel="stylesheet" type="text/css" />
    <link href="Css/EstiloItau/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="Css/layout-default.css" rel="stylesheet" type="text/css" />
    <link href="Css/Estilo.css" rel="stylesheet" type="text/css" />
    <link href="Css/jquery.dropdown.css" rel="stylesheet" type="text/css" />
    <link href="Css/liteaccordion.css" rel="stylesheet" type="text/css" />
    <link href="Css/Menu.css" rel="stylesheet" type="text/css" />
    <link href="Css/tipped.css" rel="stylesheet" type="text/css" />
    <link href="Css/Portal.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                </td>
                <td class="tdLogo" valign="top">
                    <table class="EncabezadoTitulo" style="width: 100%;">
                        <tr>
                            <td align="left" rowspan="4" style="padding-left: 30px; font-size: medium;" width="650">
                                <anthem:Image ID="imgLogoItau" runat="server" ImageUrl="~/Img/logo-itau.png" />
                                <br />
                                Sistema Documental
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td class="tdTitulo01" align="center" style="text-align: center; font-size: 20px;">
                                SOLICITUD SIN INFORMACIÓN DISPONIBLE EN PLATAFORMA DOCUMENTAL
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                               <input name="button" type="button" class=myButton2 onclick="window.close();" value="Cerrar esta ventana" /> 

                            </td>
                            <td>
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
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <div class="footer">
                        <img src="Img/asicom.jpg" width="150" height="24" alt="Asicom" longdesc="http://www.asicom.cl" />
                    </div>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
