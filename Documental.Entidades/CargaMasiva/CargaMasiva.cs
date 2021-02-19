using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Documental.Entidades.CargaMasiva
{
    public class CargaMasivaInfo
    {
        public int numOpe { get; set; }
        public int idUsu { get; set; }
        public int numRut { get; set; }
        public string digRut { get; set; }
        public string nomCliSol { get; set; }
        public string APEPATSOL { get; set; }
        public string APEMATSOL { get; set; }
        public int ejeCom { get; set; }
        public int ofiHip { get; set; }
        public int EJENEG { get; set; }
        public int CODOFICINA { get; set; }
        public int CODTIPPER { get; set; }
        public string NOMJUR { get; set; }
        public string RAZSOCIAL { get; set; }
        public int CodConvenio { get; set; }
        public int CodProyecto { get; set; }
        public int Proser { get; set; }
        public int Indctacte { get; set; }
        public int IndChip { get; set; }
        public int IndCruce { get; set; }
        public int INDCONVENIO { get; set; }
        public int Terminalista { get; set; }
        public int IndCnv { get; set; }
        public int RutTer { get; set; }
        public DateTime FecApro { get; set; }
        public decimal MonApro { get; set; }
        public int Campana { get; set; }
        public int TipCre { get; set; }
        public DateTime FecAproCre { get; set; }
        public int CODOFICINA2 { get; set; }
        public int INDSMS { get; set; }
        public string CELULAR { get; set; }
        public decimal MONCREAPRO { get; set; }
        public int SegFAF { get; set; }
        public int EJECANV { get; set; }
        public int CODOFICINAG { get; set; }
        public int CANALVENTA { get; set; }
        public int CODG { get; set; }
        public int plazo { get; set; }
        public string Rol { get; set; }
        public string opePrinRefi { get; set; }
        public int tipGar { get; set; }
        public int codSegVida { get; set; }
        public int ciaSegvida { get; set; }
        public int codSegInc { get; set; }
        public int ciaSegInc { get; set; }
        public int compraConjunta { get; set; }
        public int RED { get; set; }
        public int codmalla { get; set; }


        public CargaMasivaInfo()
        {
 
        }
    }


    public class InfoSolicitudes
    {
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Canal { get; set; }
        public string CodigoSucursal { get; set; }
        public string EjecutivoCuentas { get; set; }
        public string TipoCredito { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public decimal MontoAprobado { get; set; }
        public string Plazo { get; set; }
        public string TipoGarantia { get; set; }
        public string IndRenegociacion { get; set; }
        public string OperacionRenegociada { get; set; }
        public string ResultadoCarga { get; set; }
        public string MensajeError { get; set; }
        public bool Validado { get; set; }
        public int NumeroSolicitud { get; set; }
        public bool IndCarga { get; set; }
        public string Responsable { get; set; }
        public string IdResponsable { get; set; }
        public string FabricaAsignada { get; set; }
        public string NombreSucursal { get; set; }
        public string RolResponsable { get; set; }
        public string UsuarioResponsableCarga { get; set; }
        public InfoSolicitudes()
        {
            Validado = true;
            ResultadoCarga = "0";
            
        }




 
    }


    public class LogCargaMasiva
    {
        public int NumeroSolicitud { get; set; }
        public string Cliente { get; set; }
        public string UsuarioResponsable { get; set; }
        public decimal MontoAprobado { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public DateTime FechaCarga { get; set; }
        public string EventoActual { get; set; }
        public string CanalVenta { get; set; }
    }
}
