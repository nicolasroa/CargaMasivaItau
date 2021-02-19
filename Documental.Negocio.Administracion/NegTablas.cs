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
    public class NegTablas
    {

        public NegTablas()
        {

        }

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Tabla según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Tabla</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<TablaInfo> Guardar(TablaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TablaInfo>();
            try
            {
                Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.Operacion<TablaInfo>(Entidad, Documental.Data.Global.SP.Tablas_INS, Documental.Data.Global.Accion.Guardar);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Tabla";
                return ObjetoResultado;
            }
        }
        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Tablas
        /// </summary>
        /// <param name="Entidad">Objeto Tabla con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad UsuarioInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<TablaInfo> Buscar(TablaInfo Entidad)
        {

            var ObjetoResultado = new Resultado<TablaInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<TablaInfo, TablaInfo>(Entidad, Documental.Data.Global.SP.Tablas_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Tabla";
                return ObjetoResultado;
            }
        }
        /// <summary>
        /// Metodo que busca el Id de un registro de la Entidad Tabla
        /// </summary>
        /// <param name="Entidad">Objeto Tabla con los Parámetros de busqueda</param>
        /// <returns>Retorna un valor de tipo int</returns>
        public static int? IdentificadorMaestro(string Concepto, string Codigo)
        {

            Tabla Entidad = new Tabla { Concepto = Concepto, Codigo = Codigo };
            var ObjetoResultado = new Resultado<TablaInfo>();
            var negTablas = new NegTablas();
            try
            {
                int ID = new int();
                if (negTablas.ConceptosRegistrados == null)
                    negTablas.ConceptosRegistrados = new List<TablaInfo>();
                if (negTablas.ConceptosRegistrados.Count(c => c.ConceptoPadre == Concepto && c.Codigo == Codigo) != 0)
                {
                    ID = negTablas.ConceptosRegistrados.FirstOrDefault(c => c.ConceptoPadre == Concepto && c.Codigo == Codigo).Id;
                    return ID;
                }
                else
                {
                    ObjetoResultado = AccesoDatos.Buscar<TablaInfo, Tabla>(Entidad, Documental.Data.Global.SP.Tablas_QRY);
                    if (ObjetoResultado.Lista == null)
                    {
                        return null;
                    }
                    else
                    {
                        if (ObjetoResultado.Lista.Count() == 0)
                        {
                            return null;
                        }
                        else
                        {
                            ID = ObjetoResultado.Lista.FirstOrDefault().Id;
                            negTablas.ConceptosRegistrados.Add(ObjetoResultado.Lista.FirstOrDefault());
                            return ID;
                        }

                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        public static List<TablaInfo> BuscarCatalogo(string Concepto)
        {

            try
            {
                //Declaración de Variables de Búsqueda
                var ObjetoTabla = new TablaInfo();
                var NegTablas = new NegTablas();
                var ObjetoResultado = new Resultado<TablaInfo>();
                int? Estado_Id = new int();
                Estado_Id = NegTablas.IdentificadorMaestro(ConfigBase.TablaEstado, ConfigBase.CodigoActivo);

                ObjetoTabla.Concepto = Concepto;
                if (Estado_Id != null) { ObjetoTabla.Estado_Id = (int)Estado_Id; }

                ObjetoResultado = NegTablas.Buscar(ObjetoTabla);

                if (ObjetoResultado.ResultadoGeneral)
                {
                    return ObjetoResultado.Lista;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }



        }

        #region PROPIEDADES


        public List<TablaInfo> ConceptosRegistrados
        {
            get { return (List<TablaInfo>)HttpContext.Current.Session[ISesiones.ConceptosRegistrados]; }
            set { HttpContext.Current.Session.Add(ISesiones.ConceptosRegistrados, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {

            public const string ConceptosRegistrados = "ConceptosRegistrados";
        }
        #endregion

    }
}
