using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documental.Entidades;
using Documental.Data;
using Documental.Global;

namespace Documental.Negocio.Administracion
{
    public class NegConfiguracionGeneral
    {


        public NegConfiguracionGeneral()
        {

        }

        /// <summary>
        /// Método que Inserta o Modifica una Entidad ConfiguracionGeneral según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad ConfiguracionGeneral</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<ConfiguracionGeneralInfo> Guardar(ConfiguracionGeneralInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ConfiguracionGeneralInfo>();
            try
            {
                Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.Operacion<ConfiguracionGeneralInfo>(Entidad, Documental.Data.Global.SP.ConfiguracionGeneral_INS, Documental.Data.Global.Accion.Guardar);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " ConfiguracionGeneral";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la Tabla ConfiguracionGeneral
        /// </summary>
        /// <param name="Entidad">Objeto ConfiguracionGeneral con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad UsuarioInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<ConfiguracionGeneralInfo> Buscar(ConfiguracionGeneralInfo Entidad)
        {

            var ObjetoResultado = new Resultado<ConfiguracionGeneralInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<ConfiguracionGeneralInfo, ConfiguracionGeneralInfo>(Entidad, Documental.Data.Global.SP.ConfiguracionGeneral_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Configuracion General";
                return ObjetoResultado;
            }
        }

        public static ConfiguracionGeneralInfo Obtener()
        {

            var ObjetoResultado = new Resultado<ConfiguracionGeneralInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<ConfiguracionGeneralInfo, ConfiguracionGeneralInfo>(new ConfiguracionGeneralInfo(), Documental.Data.Global.SP.ConfiguracionGeneral_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();
                if (ObjetoResultado.Lista == null)
                {
                    return null;
                }

                return ObjetoResultado.Lista.FirstOrDefault();
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Configuracion General";
                return null;
            }
        }
    }
}
