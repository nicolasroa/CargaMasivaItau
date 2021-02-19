using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documental.Entidades;
using System.Net.Mail;
using System.Net;
namespace Documental.Global
{
    public class Mail
    {
        public static bool SendMail(string to, string subject, string body, int type, ConfiguracionGeneralInfo ObjetoConfiguracion)
        {
            try
            {
                var smtp = new SmtpClient
                {
                    Host = ObjetoConfiguracion.ServidorSalidaCorreo,
                    Port = int.Parse(ObjetoConfiguracion.PuertoCorreo),
                    EnableSsl = ObjetoConfiguracion.ConexionSeguraMail,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(ObjetoConfiguracion.Correo, ObjetoConfiguracion.ClaveCorreo)
                };
                using (var message = new MailMessage(ObjetoConfiguracion.Correo, to)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }


        public static bool EnviarMensajeNuevoUsuario(UsuarioInfo ObjetoUsuario, ConfiguracionGeneralInfo ObjetoConfiguracion)
        {
            string Mensaje = "";

            Mensaje += "<p>" + ObjetoUsuario.Nombre.ToUpper() + " " + ObjetoUsuario.Apellido.ToUpper() + "</p>";
            Mensaje += "<p>SE HA CREADO UNA CUENTA EN EL SISTEMA " + ArchivoRecursos.ObtenerValorNodo(Constantes.DatosSistema.NombreAplicacion.ToString()) + " ASOCIADA A ESTE CORREO ELECTRÓNICO</p>";
            Mensaje += "<p>PARA ACCEDER LA RUTA ES:<strong> " + ArchivoRecursos.ObtenerValorNodo(Constantes.DatosSistema.Ruta.ToString()) + "</strong></p>";
            Mensaje += "<p>SUNOMBRE DE USUARIO ES:<strong> " + ObjetoUsuario.NombreUsuario + "</strong></p>";
            Mensaje += "<p>SU CLAVE DE ACCESO INICIAL ES:<strong> " + Seguridad.Desencriptar(ObjetoUsuario.Clave) + "</strong></p>";
            Mensaje += DefinirFirma();


            if (!Mail.SendMail(ObjetoUsuario.Mail, "Nuevo Usuario " + ArchivoRecursos.ObtenerValorNodo(Constantes.DatosSistema.NombreAplicacion.ToString()), Mensaje, 1, ObjetoConfiguracion))
            {
                return false;
            }
            return true;
        }

        public static bool EnviarMensajeRecuperacionClave(UsuarioInfo ObjetoUsuario, ConfiguracionGeneralInfo ObjetoConfiguracion)
        {
            string Mensaje = "";

            Mensaje += "<p>" + ObjetoUsuario.Nombre.ToUpper() + " " + ObjetoUsuario.Apellido.ToUpper() + "</p>";
            Mensaje += "<p>SE HA REINICIADO SU CONTRASEÑA PARA INGRESAR AL SISTEMA " + ArchivoRecursos.ObtenerValorNodo(Constantes.DatosSistema.NombreAplicacion.ToString()) + "</p>";
            Mensaje += "<p>PARA ACCEDER AL SITIO LA RUTA ES:<strong> " + ArchivoRecursos.ObtenerValorNodo(Constantes.DatosSistema.Ruta.ToString()) + "</strong></p>";
            Mensaje += "<p>SU NOMBRE DE USUARIO ES:<strong> " + ObjetoUsuario.NombreUsuario + "</strong></p>";
            Mensaje += "<p>SU CLAVE DE ACCESO TEMPORAL ES:<strong> " + Seguridad.Desencriptar(ObjetoUsuario.Clave) + "</strong></p>";
            Mensaje += DefinirFirma();


            if (!Mail.SendMail(ObjetoUsuario.Mail, "Nuevo Usuario " + ArchivoRecursos.ObtenerValorNodo(Constantes.DatosSistema.NombreAplicacion.ToString()), Mensaje, 1, ObjetoConfiguracion))
            {
                return false;
            }
            return true;
        }

        public static bool EnviarMensajeDesbloqueoUsuario(UsuarioInfo ObjetoUsuario, ConfiguracionGeneralInfo ObjetoConfiguracion)
        {
            string Mensaje = "";

            Mensaje += "<p>" + ObjetoUsuario.Nombre.ToUpper() + " " + ObjetoUsuario.Apellido.ToUpper() + "</p>";
            Mensaje += "<p>SE HA REACTIVADO SU CUENTA Y REINICIADO SU CONTRASEÑA PARA INGRESAR AL SISTEMA " + ArchivoRecursos.ObtenerValorNodo(Constantes.DatosSistema.NombreAplicacion.ToString()) + "</p>";
            Mensaje += "<p>PARA ACCEDER AL SITIO LA RUTA ES:<strong> " + ArchivoRecursos.ObtenerValorNodo(Constantes.DatosSistema.Ruta.ToString()) + "</strong></p>";
            Mensaje += "<p>SU NOMBRE DE USUARIO ES:<strong> " + ObjetoUsuario.NombreUsuario + "</strong></p>";
            Mensaje += "<p>SU CLAVE DE ACCESO TEMPORAL ES:<strong> " + Seguridad.Desencriptar(ObjetoUsuario.Clave) + "</strong></p>";
            Mensaje += DefinirFirma();


            if (!Mail.SendMail(ObjetoUsuario.Mail, "Nuevo Usuario " + ArchivoRecursos.ObtenerValorNodo(Constantes.DatosSistema.NombreAplicacion.ToString()), Mensaje, 1, ObjetoConfiguracion))
            {
                return false;
            }
            return true;
        }

        public static string DefinirFirma()
        {
            string Firma = "";
            Firma += "<br><br>Atentamente";
            Firma += "<br>" + ArchivoRecursos.ObtenerValorNodo(Constantes.DatosSistema.NombreAplicacion.ToString()) + "";
            Firma += "<br><p><img src='logo.jpg'></p>";
            return Firma;
        }
    }
}
