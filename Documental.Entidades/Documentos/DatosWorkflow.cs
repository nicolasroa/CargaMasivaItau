using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Documental.Entidades.Documentos
{
    public class DatosWorkflow : Base
    {
        public string NumeroSolicitud { get; set; }
        public int TipoParticipacion_Id { get; set; }
        public string DescripcionTipoParticipacion { get; set; }
        public string NombreSolicitante { get; set; }
        public string Dv { get; set; }
        public int Rut { get; set; }
        public string RolWF { get; set; }
        public int UsuarioWF { get; set; }
        public bool Supervisor { get; set; }
        public List<Participantes> lstParticipantes { get; set; }
    }

    public class Participantes
    {
        public int TipoParticipacion_Id { get; set; }
        public string DescripcionTipoParticipacion { get; set; }
        public int RutParticipante { get; set; }
        public string NombreParticipante { get; set; }
    }
}
