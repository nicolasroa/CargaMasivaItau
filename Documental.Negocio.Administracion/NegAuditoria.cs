using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documental.Entidades;
using Documental.Global;
using Documental.Data;
using System.Web;


namespace Documental.Negocio.Administracion
{
    public class NegAuditoria
    {
        public NegAuditoria()
        {

        }

        #region METODOS
        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Auditoria
        /// </summary>
        /// <param name="Entidad">Objeto AuditoriaInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad AuditoriaInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<AuditoriaInfo> Buscar(AuditoriaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<AuditoriaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<AuditoriaInfo, AuditoriaInfo>(Entidad, Documental.Data.Global.SP.Auditoria_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Auditoria";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que lista a los Usuarios Respopnsables que estan presentes en la tablas Auditoria
        /// </summary>
        /// <returns>>Lista de la Entidad AuditoriaInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<AuditoriaInfo> ListarResponsables()
        {

            var ObjetoResultado = new Resultado<AuditoriaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<AuditoriaInfo, AuditoriaInfo>(new AuditoriaInfo(), Documental.Data.Global.SP.ResponsablesAuditoria_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Responsables Auditoria";
                return ObjetoResultado;
            }
        }

        #endregion


        #region PROPIEDADES
        public static List<AuditoriaInfo> ListaAuditoriaPDF
        {
            get { return (List<AuditoriaInfo>)HttpContext.Current.Session[ISesiones.ListaAuditoriaPDF]; }
            set { HttpContext.Current.Session.Add(ISesiones.ListaAuditoriaPDF, value); }
        }


        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string ListaAuditoriaPDF = "ListaAuditoriaPDF";
        }
        #endregion
    }
}
