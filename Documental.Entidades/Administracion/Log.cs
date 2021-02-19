using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Documental.Entidades
{
    public class Log : Base
    {
        public string Procedimiento { get; set; }
        public string Mensaje { get; set; }
        public string PilaEventos { get; set; }


    }
}
