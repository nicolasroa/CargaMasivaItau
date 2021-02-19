using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Documental.Entidades.Documentos
{
    public class GruposDocumentos : Base
    {
        public string CodGrupoDocumento { get; set; }
        public string GrupoDocumento { get; set; }
        public int Estado_Id { get; set; }
    }

}
