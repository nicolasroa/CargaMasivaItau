using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Documental.Entidades.EnvioMailWF
{
    public class MailWfBase : Base
    {
        public string Asunto { get; set; }
        public int IdFlujo { get; set; }
        public int IdEvento { get; set; }
        public int IdDecision { get; set; }
        public int IdDestinatario { get; set; }
        public bool SoloUnEnvio { get; set; }
        public byte[] ImagenEncabezado { get; set; }
        public byte[] ImagenPieFirma { get; set; }
        public string Texto { get; set; }
    }


    public class MailWfInfo : MailWfBase
    {

        public string DescripcioFlujo { get; set; }
        public string DescripcionEvento { get; set; }
        public string DescipcionDecision { get; set; }
        public string DescripcionDestinatario { get; set; }

    }

    public class VariablesWf
    {
        public int Id { get; set; }
        public int IdPadre { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
    }

}
