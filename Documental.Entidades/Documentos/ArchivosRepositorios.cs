using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Documental.Entidades.Documentos
{
    public class ArchivosRepositorios : Base
    {
        public int IdArchivo { get; set; }
        public int IdTipoDocumento { get; set; }
        public int Rut { get; set; }
        public string NumeroSolicitud { get; set; }
        public string NombreArchivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEmision { get; set; }
        public string ExtensionArchivo { get; set; }
        public string SizeKB { get; set; }
        public string ContentType { get; set; }
        public int Privado  { get; set; }
        public byte[] ArchivoVB { get; set; }
        public int Estado_Id { get; set; }
        //public int IdRol { get; set; }
        //public int IdUsuario { get; set; }
    }

    public class ArchivosRepositoriosInfo : ArchivosRepositorios
    {
        public int IdGrupoDocumento { get; set; }
        public string GrupoDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public int Cantidad { get; set; }
        public string EstadoAlertaDoc { get; set; }
        public bool PermisoVer { get; set; }
		public bool PermisoModificar { get; set; }
		public bool PermisoDescargar { get; set; }
		public bool PermisoEliminar { get; set; }
        public bool PermisoSubir { get; set; }
        public string strRutParticipantes { get; set; }
        public string NombreParticipante { get; set; }
    }
}
