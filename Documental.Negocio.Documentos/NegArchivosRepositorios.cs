using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documental.Entidades;
using Documental.Entidades.Documentos;
using Documental.Data;
using Documental.Global;
using System.Web;

namespace Documental.Negocio.Documentos
{
    public class NegArchivosRepositorios
    {

        #region PROPIEDADES

        public static List<ArchivosRepositoriosInfo> getsetlstArchivoRepositorio
        {
            get { return (List<ArchivosRepositoriosInfo>)HttpContext.Current.Session[ISesiones.ArchivoRepositorio]; }
            set { HttpContext.Current.Session.Add(ISesiones.ArchivoRepositorio, value); }
        }

        public static List<ArchivosRepositoriosInfo> getsetlstRepositorioDoc
        {
            get { return (List<ArchivosRepositoriosInfo>)HttpContext.Current.Session[ISesiones.ArchivoRepositorioPersona]; }
            set { HttpContext.Current.Session.Add(ISesiones.ArchivoRepositorioPersona, value); }
        }

        #endregion

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Archivo Repositorio
        /// </summary>
        /// <param name="Entidad">Objeto Archivo Repositorio con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad Archivo Repositorio en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ArchivosRepositoriosInfo> Buscar(ArchivosRepositoriosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<ArchivosRepositoriosInfo, ArchivosRepositoriosInfo>(Entidad, Documental.Data.Global.SP.ArchivosRepositorios_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + "ArchivosRepositorios";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Archivo Repositorio
        /// </summary>
        /// <param name="Entidad">Objeto Archivo Repositorio con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad Archivo Repositorio en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ArchivosRepositoriosInfo> BuscarBandejaGrupoDocumento(ArchivosRepositoriosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<ArchivosRepositoriosInfo, ArchivosRepositoriosInfo>(Entidad, Documental.Data.Global.SP.BandejaGrupoDocumentoSolicitud_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + "BandejaGruposDocumentos";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Archivo Repositorio
        /// </summary>
        /// <param name="Entidad">Objeto Archivo Repositorio con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad Archivo Repositorio en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ArchivosRepositoriosInfo> BuscarBandejaGrupoDocumentoRepositorio(ArchivosRepositoriosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<ArchivosRepositoriosInfo, ArchivosRepositoriosInfo>(Entidad, Documental.Data.Global.SP.BandejaGrupoDocumentoRepositorio_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + "BandejaGruposDocumentosRepositorio";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Tabla según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Tabla</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<ArchivosRepositoriosInfo> Guardar(ArchivosRepositoriosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();
            try
            {
                //Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.Operacion<ArchivosRepositoriosInfo>(Entidad, Documental.Data.Global.SP.ArchivosRepositorios_INS, Documental.Data.Global.Accion.Guardar);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " ArchivosRepositorios";
                return ObjetoResultado;
            }
        }

        public static ArchivosRepositoriosInfo ObtenerArchivoRepositorio(int Id)
        {
            var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();
            var Entidad = new ArchivosRepositoriosInfo();
            Entidad.Id = Id;
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<ArchivosRepositoriosInfo, ArchivosRepositoriosInfo>(Entidad, Documental.Data.Global.SP.ArchivosRepositorios_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();

                return ObjetoResultado.Lista.FirstOrDefault();
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + "ArchivosRepositorios";
                return new ArchivosRepositoriosInfo();
            }
        }

        public static ArchivosRepositoriosInfo ObtenerRepositorioDocumento(int Id)
        {
            var lstInfo = new List<ArchivosRepositoriosInfo>();
            lstInfo = (List<ArchivosRepositoriosInfo>)NegArchivosRepositorios.getsetlstRepositorioDoc;
            var ObjetoResultado = new ArchivosRepositoriosInfo();

            ObjetoResultado = (from x in lstInfo.AsEnumerable() where x.Id == Id select x).ToList()[0];

            return ObjetoResultado;







        }

        public Resultado<ArchivosRepositoriosInfo> Eliminar(ArchivosRepositoriosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();
            try
            {
                //Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.Operacion<ArchivosRepositoriosInfo>(Entidad, Documental.Data.Global.SP.ArchivosRepositorios_DEL, Documental.Data.Global.Accion.Eliminar);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " ArchivosRepositorios";
                return ObjetoResultado;
            }
        }

        public Resultado<ArchivosRepositoriosInfo> AsignarDocumentoSolicitud(ref ArchivosRepositoriosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ArchivosRepositoriosInfo>();
            try
            {
                //Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.Operacion<ArchivosRepositoriosInfo>(ref Entidad, Documental.Data.Global.SP.AsignarDocumentoSolicitud, Documental.Data.Global.Accion.Guardar);
                if (Entidad.Id == -1)
                {
                    ObjetoResultado.Mensaje = "Documento ya Asignado a la Solicitud";
                    ObjetoResultado.ResultadoGeneral = false;
                }
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " ArchivosRepositorios";
                return ObjetoResultado;
            }
        }

        #region SESIONES

        private class ISesiones
        {
            public static string ArchivoRepositorio = "ArchivoRepositorio";
            public static string ArchivoRepositorioPersona = "ArchivoRepositorioPersona";
            public static string RepositorioDoc = "RepositorioDoc";
        }
        #endregion

    }
}
