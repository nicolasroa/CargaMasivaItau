using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documental.Entidades;
using Documental.Entidades.EnvioMailWF;
using Documental.Data;
using Documental.Global;
using System.Web;

namespace Documental.Negocio.EnvioMailWF
{
    public class NegEnvioMailWf
    {
        #region PROPIEDADES

        public static List<VariablesWf> lstVariablesWf
        {
            get { return (List<VariablesWf>)HttpContext.Current.Session[ISesiones.VariablesWF]; }
            set { HttpContext.Current.Session.Add(ISesiones.VariablesWF, value); }
        }


        #endregion

        public Resultado<MailWfInfo> Guardar(MailWfInfo Entidad)
        {

            var ObjetoResultado = new Resultado<MailWfInfo>();
            try
            {
                //Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.Operacion<MailWfInfo>(Entidad, Documental.Data.Global.SP.MailWf_INS, Documental.Data.Global.Accion.Guardar);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Mail Wf Info";
                return ObjetoResultado;
            }
        }

        public Resultado<MailWfInfo> Buscar(MailWfInfo Entidad)
        {

            var ObjetoResultado = new Resultado<MailWfInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<MailWfInfo, MailWfInfo>(Entidad, Documental.Data.Global.SP.MailWf_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + "Mail Wf Info";
                return ObjetoResultado;
            }
        }
        public Resultado<VariablesWf> BuscarVariables(VariablesWf Entidad)
        {

            var ObjetoResultado = new Resultado<VariablesWf>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<VariablesWf, VariablesWf>(Entidad, Documental.Data.Global.SP.VariablesWf_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + "VariablesWf";
                return ObjetoResultado;
            }
        }

        #region SESIONES

        private class ISesiones
        {

            public static string VariablesWF = "VariablesWF";

        }
        #endregion
    }
}
