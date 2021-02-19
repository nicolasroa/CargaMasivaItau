using System;
using System.Linq;
using Documental.Data;
using Documental.Entidades;
using Documental.Entidades.Documentos;
using Documental.Global;

namespace Documental.Negocio.Documentos
{
    public class NegGruposDocumentos
    {
        public NegGruposDocumentos()
        {

        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Grupos Documentos
        /// </summary>
        /// <param name="Entidad">Objeto GrupoDocumento con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad UsuarioInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<GruposDocumentos> Buscar(GruposDocumentos Entidad)
        {

            var ObjetoResultado = new Resultado<GruposDocumentos>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<GruposDocumentos, GruposDocumentos>(Entidad, Documental.Data.Global.SP.GruposDocumentos_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();


                return ObjetoResultado;
            }
            catch (Exception)
            {              
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " GruposDocumentos";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Grupos Documentos
        /// </summary>
        /// <param name="Entidad">Objeto GrupoDocumento con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad UsuarioInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<GruposDocumentos> ObtenerGrupoDocumentos(GruposDocumentos Entidad)
        {

            var ObjetoResultado = new Resultado<GruposDocumentos>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<GruposDocumentos, GruposDocumentos>(Entidad, Documental.Data.Global.SP.GruposDocumentosPortal_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " GruposDocumentosPortal";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Tabla según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Tabla</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<GruposDocumentos> Guardar(GruposDocumentos Entidad)
        {

            var ObjetoResultado = new Resultado<GruposDocumentos>();
            try
            {
                //Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.Operacion<GruposDocumentos>(Entidad, Documental.Data.Global.SP.GruposDocumentos_INS, Documental.Data.Global.Accion.Guardar);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " GruposDocumentos";
                return ObjetoResultado;
            }
        }
       

    }
}
