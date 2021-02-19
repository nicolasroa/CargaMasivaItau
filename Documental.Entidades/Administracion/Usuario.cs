using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Documental.Entidades
{
    public class Usuario : Base
    {
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string Mail { get; set; }
        public int Estado_Id { get; set; }
        public bool ForzarCambioClave { get; set; }
        public DateTime UltimoCambioClave { get; set; }
        public byte? IntentosFallidos { get; set; }
        public bool PrimerInicio { get; set; }
        public string PreguntaSeguridad { get; set; }
        public string RespuestaSeguridad { get; set; }
        public bool? AdministradorAplicaciones { get; set; }

        public Usuario()
        {
            this.Id = -1;
        }



    }
    public class HistorialCambioClave
    {
        public int Id { get; set; }
        public int Usuario_Id { get; set; }
        public string ClaveUsada { get; set; }
        public DateTime FechaCambio { get; set; }
    }

    public class UsuarioRol : Base
    {
        public int Usuario_Id { get; set; }
        public int Rol_Id { get; set; }


    }
    public class UsuarioRolInfo : UsuarioRol
    {
        public string DescripcionRol { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class UsuarioInfo : Usuario
    {
        public string DescripcionEstado { get; set; }
        public string NombreCompleto { get; set; }

    }


    public static class ConfigUsuario
    {

        public const string ValidacionNoEncontrado = "NoEncontrado";
        public const string ValidacionInactivo = "Inactivo";
        public const string ValidacionErrorClave = "ErrorClave";
        public const string ValidacionCambioClave = "CambioClave";
        public const string ValidacionPrimerInicio = "PrimarInicio";
        public const string ValidacionAvisoCambioClave = "AvisoCambioClave";
        public const string ValidacionAprobado = "Aprobado";

        public static string MensajeNoExiste = "MensajeNoExiste";
        public static string MensajeInactivo = "MensajeInactivo";
        public static string MensajeErrorClave = "MensajeErrorClave";
        public static string MensajeCambioClave = "MensajeCambioClave";

        public static string TablaPreguntasSeguridad = "PREGUNTASSEGURIDAD";

        public static string UrlRecuperarContraseña = "~/OperacionesLogin/RecuperarContraseña.aspx";
        public static string UrlCambioContraseña = "~/OperacionesLogin/CambioContraseña.aspx";
        public static string UrlAvisoCambioContraseña = "~/OperacionesLogin/AvisoCambioContraseña.aspx";
    }
}
