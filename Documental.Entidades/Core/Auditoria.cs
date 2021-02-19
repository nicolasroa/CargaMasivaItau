using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Documental.Entidades
{

    public class AuditoriaInfo
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string NombreTabla { get; set; }
        public int? Usuario_Id { get; set; }
        public string NombreCampo { get; set; }
        public string DatoAnterior { get; set; }
        public string DatoNuevo { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
    }
}
