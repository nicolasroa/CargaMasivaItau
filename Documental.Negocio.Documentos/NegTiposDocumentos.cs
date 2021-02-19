using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Documental.Entidades;
using Documental.Global;
using Documental.Data;
using System.Web;
using Documental.Entidades.Documentos;

namespace Documental.Negocio.Documentos
{
    public class NegTiposDocumentos
    {

        #region PROPIEDADES

        public static List<TiposDocumentosInfo> getLstTipoDoc
        {
            get { return (List<TiposDocumentosInfo>)HttpContext.Current.Session[ISesiones.lstTipoDoc]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstTipoDoc, value); }
        }

        #endregion

        public NegTiposDocumentos()
        {

        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Tipos Documentos
        /// </summary>
        /// <param name="Entidad">Objeto TipoDocumento con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad TipoDocumento en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<TiposDocumentosInfo> Buscar(TiposDocumentosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TiposDocumentosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<TiposDocumentosInfo, TiposDocumentosInfo>(Entidad, Documental.Data.Global.SP.TiposDocumentos_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " TiposDocumentos";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Tipos Documentos
        /// </summary>
        /// <param name="Entidad">Objeto TipoDocumento con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad TipoDocumento en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<TiposDocumentosInfo> ObtenerTipoDocumentos(TiposDocumentosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TiposDocumentosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<TiposDocumentosInfo, TiposDocumentosInfo>(Entidad, Documental.Data.Global.SP.TiposDocumentosPortal_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " TiposDocumentosPortal";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Tabla según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Tabla</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<TiposDocumentosInfo> Guardar(TiposDocumentosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TiposDocumentosInfo>();
            try
            {
                //Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.Operacion<TiposDocumentosInfo>(Entidad, Documental.Data.Global.SP.TiposDocumentos_INS, Documental.Data.Global.Accion.Guardar);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " TiposDocumentos";
                return ObjetoResultado;
            }
        }

        public static TiposDocumentosInfo ObtenerTipoDocumento(int Id)
        {
            var lstInfo = new List<TiposDocumentosInfo>();
            lstInfo = (List<TiposDocumentosInfo>)NegTiposDocumentos.getLstTipoDoc;
            var ObjetoResultado = new TiposDocumentosInfo();

            ObjetoResultado = (from x in lstInfo.AsEnumerable() where x.Id == Id select x).ToList()[0];

            return ObjetoResultado;
        }

        #region SESIONES

        private class ISesiones
        {
            public static string lstTipoDoc = "lstTipoDoc";
        }
        #endregion
    }
}
