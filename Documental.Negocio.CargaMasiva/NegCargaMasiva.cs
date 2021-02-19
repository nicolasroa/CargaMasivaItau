using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documental.Entidades;
using Documental.Entidades.CargaMasiva;
using Documental.Data;
using Documental.Global;
using System.Web;

namespace Documental.Negocio.CargaMasiva
{
    public class NegCargaMasiva
    {
        public Resultado<InfoSolicitudes> Guardar(InfoSolicitudes Entidad)
        {

            var ObjetoResultado = new Resultado<InfoSolicitudes>();
            try
            {
                //Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.Operacion<InfoSolicitudes>(ref Entidad, Documental.Data.Global.SP.prc_ProcesarCargaMasiva, Data.Global.Accion.Guardar, "ConexionSQL_WF");
                if (Entidad != null)
                {
                    ObjetoResultado.Objeto = new InfoSolicitudes();
                    ObjetoResultado.Objeto = Entidad;
                }
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " CargaMasivaInfo";
                return ObjetoResultado;
            }
        }
        public Resultado<LogCargaMasiva> BuscarLog(LogCargaMasiva Entidad)
        {

            var ObjetoResultado = new Resultado<LogCargaMasiva>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<LogCargaMasiva, LogCargaMasiva>(Entidad, Documental.Data.Global.SP.LogCargaMasiva_QRY,"ConexionSQL_WF");
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + "Bitacora";
                return ObjetoResultado;
            }
        }
        

        public static List<InfoSolicitudes> lstNomina
        {
            get { return (List<InfoSolicitudes>)HttpContext.Current.Session["NominaSolicitudes"]; }
            set { HttpContext.Current.Session.Add("NominaSolicitudes", value); }
        }
        public static List<InfoSolicitudes> lstSolicitudesCreadas
        {
            get { return (List<InfoSolicitudes>)HttpContext.Current.Session["lstSolicitudesCreadas"]; }
            set { HttpContext.Current.Session.Add("lstSolicitudesCreadas", value); }
        }
    }
}
