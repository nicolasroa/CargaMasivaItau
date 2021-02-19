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
    public class NegControles
    {
        public NegControles()
        {

        }
        #region METODOS

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Controles según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Controles</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<ControlesInfo> Guardar(ControlesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ControlesInfo>();
            try
            {
                Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.Operacion<ControlesInfo>(Entidad, Documental.Data.Global.SP.Controles_INS, Documental.Data.Global.Accion.Guardar);
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Controles";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Controless
        /// </summary>
        /// <param name="Entidad">Objeto ControlesInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad ControlesInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ControlesInfo> Buscar(ControlesInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ControlesInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<ControlesInfo, ControlesInfo>(Entidad, Documental.Data.Global.SP.Controles_QRY);
                if (ObjetoResultado.Lista == null)
                {
                    ObjetoResultado.ResultadoGeneral = false;
                    ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Controles";
                }
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Controles";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que guarda la configuracion Efectuada a un control de un Menú Específico
        /// </summary>
        /// <param name="Entidad">Objeto que contienen los datos del control a configurar y los permisos correspondientes</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<RolMenuControlInfo> GuardarConfiguracion(RolMenuControlInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RolMenuControlInfo>();
            try
            {
                Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.Operacion<RolMenuControlInfo>(Entidad, Documental.Data.Global.SP.RolMenuControles_INS, Documental.Data.Global.Accion.Guardar);
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Controles";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla RolMenuControles
        /// </summary>
        /// <param name="Entidad">Objeto ControlesInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad ControlesInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<RolMenuControlInfo> BuscarConfiguracion(RolMenuControlInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RolMenuControlInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<RolMenuControlInfo, RolMenuControlInfo>(Entidad, Documental.Data.Global.SP.RolMenuControles_QRY);
                if (ObjetoResultado.Lista == null)
                {
                    ObjetoResultado.ResultadoGeneral = false;
                    ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Rol Menú Controles";
                }
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Rol Menú Controles";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Menu Control Usuario
        /// </summary>
        /// <param name="Entidad">Objeto ControlesInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad ControlesInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<RolMenuControlInfo> BuscarControlesPorUsuario(RolMenuControlInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RolMenuControlInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<RolMenuControlInfo, RolMenuControlInfo>(Entidad, Documental.Data.Global.SP.MenuControlesUsuario_QRY);
                if (ObjetoResultado.Lista == null)
                {
                    ObjetoResultado.ResultadoGeneral = false;
                    ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Menú Controles Usuario";
                }
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Menú Controles Usuario";
                return ObjetoResultado;
            }
        }


        #endregion




        #region PROPIEDADES
        public MenuInfo MenuPadre
        {
            get { return (MenuInfo)HttpContext.Current.Session[ISesiones.MenuPadre]; }
            set { HttpContext.Current.Session.Add(ISesiones.MenuPadre, value); }
        }
        #endregion


        #region SESIONES
        private class ISesiones
        {

            public const string MenuPadre = "MenuPadre";
        }
        #endregion
    }
}
