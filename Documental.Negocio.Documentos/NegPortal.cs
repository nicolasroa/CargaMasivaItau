using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documental.Entidades.Documentos;
using System.Web;
using Documental.Entidades;
using Documental.Data;
using Documental.Global;

namespace Documental.Negocio.Documentos
{
    public class NegPortal
    {
        #region PROPIEDADES

        public static DatosWorkflow getsetDatosWorkflow
        {
            get { return (DatosWorkflow)HttpContext.Current.Session[ISesiones.DatosWorkflow]; }
            set { HttpContext.Current.Session.Add(ISesiones.DatosWorkflow, value); }
        }     

        #endregion

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Grupos Documentos
        /// </summary>
        /// <param name="Entidad">Objeto GrupoDocumento con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad UsuarioInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<DatosWorkflow> Buscar(DatosWorkflow Entidad)
        {
            var ObjetoResultado = new Resultado<DatosWorkflow>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<DatosWorkflow, DatosWorkflow>(Entidad, Documental.Data.Global.SP.DOC_DatosWorkflow_QRY, "ConexionSQL_WF");
                //ObjetoResultado = AccesoDatos.Buscar<DatosWorkflow, DatosWorkflow>(Entidad, Documental.Data.Global.SP.DatosWorkflow_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " DatosWorkflow";
                return ObjetoResultado;
            }
        }

        #region SESIONES

        private class ISesiones
        {
            public static string DatosWorkflow = "DatosWorkflow";
        }
        #endregion
    }
}
