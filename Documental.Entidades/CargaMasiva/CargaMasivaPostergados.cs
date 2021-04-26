using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Documental.Entidades.CargaMasiva
{
    public class CargaMasivaPostergadosInfo
    {
        public string Rut { get; set; }
        public string FechaAprobacion { get; set; }
        public string NroOperacion { get; set; }
        public int Plazo { get; set; }
        public int Gracia { get; set; }
        public decimal tasa { get; set; }
        public int NroDividendosAPostergar { get; set; }
        public int FechaEscritura { get; set; }
        

        public CargaMasivaPostergadosInfo()
        {
 
        }
    }

    public class InfoPostergado
    {
        public string Rut { get; set; }
        public string FechaAprobacion { get; set; }
        public string NroOperacion { get; set; }
        public int Plazo { get; set; }
        public int Gracia { get; set; }
        public decimal Tasa { get; set; }
        public int NroDividendosAPostergar { get; set; }
        public int FechaEscritura { get; set; }
        public string ResultadoCarga { get; set; }
        public string MensajeError { get; set; }
        public bool IndCarga { get; set; }
        public bool Validado { get; set; }
        //public int NumeroSolicitud { get; set; }
        //public string Responsable { get; set; }
        //public string IdResponsable { get; set; }
        //public string FabricaAsignada { get; set; }
        //public string NombreSucursal { get; set; }
        //public string RolResponsable { get; set; }
        //public string UsuarioResponsableCarga { get; set; }

        public InfoPostergado()
        {
            Validado = true;
            ResultadoCarga = "0";
        }
    }
}
