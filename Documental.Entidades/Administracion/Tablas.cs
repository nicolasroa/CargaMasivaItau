using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Documental.Entidades
{
    public class Tabla : Base
    {
        public int? TablaPadre_Id { get; set; }
        public string Nombre { get; set; }
        public string NombreTablaPadre { get; set; }
        public string Codigo { get; set; }
        public string Concepto { get; set; }
        public string ConceptoPadre { get; set; }
        public int Estado_Id { get; set; }

    }

    public class TablaInfo : Tabla
    {
        public string NombreEstado { get; set; }
        public string CodigoEstado { get; set; }




    }
}
