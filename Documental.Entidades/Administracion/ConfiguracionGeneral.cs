using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Documental.Entidades
{
    public class ConfiguracionGeneral : Base
    {
        public byte? TamanioClave { get; set; }
        public int PlazoValidez { get; set; }
        public byte? Notificacion { get; set; }
        public byte? Intentos { get; set; }
        public string UsuarioCorreo { get; set; }
        public string Correo { get; set; }
        public string ClaveCorreo { get; set; }
        public string ServidorEntradaCorreo { get; set; }
        public string ServidorSalidaCorreo { get; set; }
        public string PuertoCorreo { get; set; }
        public bool ConexionSeguraMail { get; set; }
        public int TamanioMaximoArchivo { get; set; }



    }

    public class ConfiguracionGeneralInfo : ConfiguracionGeneral
    {
        public int TamanioArchivoBytes { get; set; }
    }
}
