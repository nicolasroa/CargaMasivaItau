using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documental.Entidades.Documentos;
using Documental.Entidades;
using Documental.Data;
using Documental.Global;
using System.Web.UI.WebControls;

namespace Documental.Negocio.Documentos
{
    public class NegRolesTiposDocumentos
    {
        public NegRolesTiposDocumentos()
        { 
        
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Roles Tipos Documentos
        /// </summary>
        /// <param name="Entidad">Objeto Roles Tipos Documentos con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad Roles Tipos Documentos en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<RolesTiposDocumentosInfo> Buscar(RolesTiposDocumentosInfo Entidad)
        {

            var ObjetoResultado = new Resultado<RolesTiposDocumentosInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<RolesTiposDocumentosInfo, RolesTiposDocumentosInfo>(Entidad, Documental.Data.Global.SP.RolesTiposDocumentos_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " RolesTiposDocumentos";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Roles Tipos Documentos según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Roles Tipos Documentos</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<RolesTiposDocumentos> Guardar(RolesTiposDocumentos Entidad)
        {

            var ObjetoResultado = new Resultado<RolesTiposDocumentos>();
            try
            {
                //Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.Operacion<RolesTiposDocumentos>(Entidad, Documental.Data.Global.SP.RolesTiposDocumentos_INS, Documental.Data.Global.Accion.Guardar);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " RolesTiposDocumentos";
                return ObjetoResultado;
            }
        }

        /// <summary>
        /// Método que realiza una Búsqueda en la vista Roles wf
        /// </summary>
        /// <param name="Entidad">Objeto Roles wf</param>
        /// <returns>Lista de la Entidad Roles wf en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<V_RolesWF> Buscar(V_RolesWF Entidad)
        {

            var ObjetoResultado = new Resultado<V_RolesWF>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<V_RolesWF, V_RolesWF>(Entidad, Documental.Data.Global.SP.V_Roles_QRY);
                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count();


                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " RolesWF";
                return ObjetoResultado;
            }
        }
             
    }
}
