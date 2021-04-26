using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documental.Entidades;
using Documental.Data;
using Documental.Entidades.CargaMasiva;
using Documental.Global;
using System.Web;

namespace Documental.Negocio.CargaMasiva
{
    public class NegCargaMasivaPostergados
    {
        public Resultado<InfoPostergado> Guardar(InfoPostergado Entidad)
        {

            var ObjetoResultado = new Resultado<InfoPostergado>();
            try
            {
                //Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.Operacion<InfoPostergado>(ref Entidad, Documental.Data.Global.SP.prc_ProcesarCargaMasivaPostergados, Data.Global.Accion.Guardar, "ConexionSQL_WF");
                if (Entidad != null)
                {
                    ObjetoResultado.Objeto = new InfoPostergado();
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
                ObjetoResultado = AccesoDatos.Buscar<LogCargaMasiva, LogCargaMasiva>(Entidad, Documental.Data.Global.SP.LogCargaMasiva_QRY, "ConexionSQL_WF");
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


        public static List<InfoPostergado> lstNominaPostergados
        {
            get { return (List<InfoPostergado>)HttpContext.Current.Session["NominaPostergados"]; }
            set { HttpContext.Current.Session.Add("NominaPostergados", value); }
        }
        public static List<InfoPostergado> lstPostergadosCreadas
        {
            get { return (List<InfoPostergado>)HttpContext.Current.Session["lstPostergadosCreadas"]; }
            set { HttpContext.Current.Session.Add("lstPostergadosCreadas", value); }
        }
    }
}
