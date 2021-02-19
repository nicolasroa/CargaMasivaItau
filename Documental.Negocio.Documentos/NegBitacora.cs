using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documental.Entidades.Documentos;
using Documental.Entidades;
using Documental.Data;
using Documental.Global;
using System.Web;

namespace Documental.Negocio.Documentos
{
    public class NegBitacora
    {

        #region PROPIEDADES

        public static List<BitacoraInfo> getLstBitacora
        {
            get { return (List<BitacoraInfo>)HttpContext.Current.Session[ISesiones.lstBitacora]; }
            set { HttpContext.Current.Session.Add(ISesiones.lstBitacora, value); }
        }

        #endregion

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Bitacora
        /// </summary>
        /// <param name="Entidad">Objeto Bitacora con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad Bitacora en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<BitacoraInfo> Buscar(BitacoraInfo Entidad)
        {

            var ObjetoResultado = new Resultado<BitacoraInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<BitacoraInfo, BitacoraInfo>(Entidad, Documental.Data.Global.SP.Bitacora_QRY);
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

        #region SESIONES

        private class ISesiones
        {
            public static string lstBitacora = "lstBitacora";
        }
        #endregion
    }
}
