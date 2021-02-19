using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityDoc = Documental.Entidades.Documentos;
using Documental.Entidades;
using Documental.Negocio.Documentos;
using Documental.Global;


namespace Documental.WebApp.Documentos
{
    public partial class RolesTiposDocumentos : System.Web.UI.Page
    {

        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
                CargarGrupoDocumento();
                CargarTipoDocumentos(-1);
            }
        }

        protected void btnVerAsignacion_Click(object sender, EventArgs e)
        {
            if (ValidarFormularioBusqueda())
            {
                var ObjetoRolTipoDoc = new EntityDoc.RolesTiposDocumentosInfo();

                ObjetoRolTipoDoc.IdRol = int.Parse(ddlRoles.SelectedValue);
                ObjetoRolTipoDoc.IdGrupoDocumento = int.Parse(ddlGrupoDocumentos.SelectedValue);
                ObjetoRolTipoDoc.IdTipoDocumento = int.Parse(ddlTipoDocumento.SelectedValue);

                CargarGridAsignacion(ref gvTiposDocumentos, ObjetoRolTipoDoc);
            }
        }

        protected void ddlGrupoDocumentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTipoDocumentos(int.Parse(ddlGrupoDocumentos.SelectedValue));
        }

        protected void chkPermisoListar_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                MarcarTodos(ref gvTiposDocumentos, Constantes.chkListar);
            else
                DesmarcarTodos(ref gvTiposDocumentos, Constantes.chkListar);
        }

        protected void chkPermisoVer_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                MarcarTodos(ref gvTiposDocumentos, Constantes.chkVer);
            else
                DesmarcarTodos(ref gvTiposDocumentos, Constantes.chkVer);
        }

        protected void chkPermisoAbrir_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                MarcarTodos(ref gvTiposDocumentos, Constantes.chkAbrir);
            else
                DesmarcarTodos(ref gvTiposDocumentos, Constantes.chkAbrir);
        }

        protected void chkPermisoModificar_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                MarcarTodos(ref gvTiposDocumentos, Constantes.chkModificar);
            else
                DesmarcarTodos(ref gvTiposDocumentos, Constantes.chkModificar);
        }

        protected void chkPermisoDescargar_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                MarcarTodos(ref gvTiposDocumentos, Constantes.chkDescargar);
            else
                DesmarcarTodos(ref gvTiposDocumentos, Constantes.chkDescargar);
        }

        protected void chkPermisoEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                MarcarTodos(ref gvTiposDocumentos, Constantes.chkEliminar);
            else
                DesmarcarTodos(ref gvTiposDocumentos, Constantes.chkEliminar);
        }

        protected void chkPermisoSubir_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                MarcarTodos(ref gvTiposDocumentos, Constantes.chkSubir);
            else
                DesmarcarTodos(ref gvTiposDocumentos, Constantes.chkSubir);
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            ProcesarAsignacion();
        }

        #endregion


        #region METODOS

        private void CargarRoles()
        {
            try
            {                

               //Asignar Roles
                var ObjRol = new EntityDoc.V_RolesWF();
                var NegRol = new NegRolesTiposDocumentos();
                var ObjResultadoRol = new Resultado<EntityDoc.V_RolesWF>();

                ObjResultadoRol = NegRol.Buscar(ObjRol);

                if (ObjResultadoRol.ResultadoGeneral)
                {
                    Controles.CargarCombo<EntityDoc.V_RolesWF>(ref ddlRoles, ObjResultadoRol.Lista, "IdRol", "Rol", "-- Seleccione --", "-1");
                }
                else
                {
                    //Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                    Controles.MostrarMensajeAlerta("Grupos de Documentos Sin Datos");
                }

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString() + "Roles"));
                }
            }

        }

        private void CargarGrupoDocumento()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var ObjetoGrupoDoc = new EntityDoc.GruposDocumentos();
                var NegGrupoDoc = new NegGruposDocumentos();
                var ObjResultadoGD = new Resultado<EntityDoc.GruposDocumentos>();

                //Asignación Grupos de Documentos
                ObjetoGrupoDoc.Id = -1;
                ObjetoGrupoDoc.GrupoDocumento = "";
                ObjetoGrupoDoc.CodGrupoDocumento = "";
                ObjetoGrupoDoc.Estado_Id = -1;

                //Ejecución del Proceso de Búsqueda
                ObjResultadoGD = NegGrupoDoc.Buscar(ObjetoGrupoDoc);

                if (ObjResultadoGD.ResultadoGeneral)
                {
                    Controles.CargarCombo<EntityDoc.GruposDocumentos>(ref ddlGrupoDocumentos, ObjResultadoGD.Lista, Constantes.StringId, "GrupoDocumento", "-- Todos --", "-1");
                }
                else
                {
                    //Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                    Controles.MostrarMensajeAlerta("Grupos de Documentos Sin Datos");
                }

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString() + "GrupoDocumento"));
                }
            }
        }

        private void CargarTipoDocumentos(int IdGrupoDocumento)
        {
            try
            {
                var ObjetoTipoDoc = new EntityDoc.TiposDocumentosInfo();
                var NegTipoDoc = new NegTiposDocumentos();
                var ObjResultadoTD = new Resultado<EntityDoc.TiposDocumentosInfo>();

                //Asignación Tipos de Documentos
                ObjetoTipoDoc.Id = -1;
                ObjetoTipoDoc.IdGrupoDocumento = IdGrupoDocumento;
                ObjetoTipoDoc.CodTipoDocumento = "";
                ObjetoTipoDoc.TipoDocumento = "";
                ObjetoTipoDoc.Estado_Id = -1;

                ObjResultadoTD = NegTipoDoc.Buscar(ObjetoTipoDoc);

                if (ObjResultadoTD.ResultadoGeneral)
                {
                    Controles.CargarCombo<EntityDoc.TiposDocumentosInfo>(ref ddlTipoDocumento, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Todos --", "-1");
                }
                else
                {
                    //Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                    Controles.MostrarMensajeAlerta("Grupos de Documentos Sin Datos");
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString() + "TipoDocumento"));
                }
            }
        }


        private void CargarGridAsignacion(ref Anthem.GridView Grid, EntityDoc.RolesTiposDocumentosInfo ObjetoRolTipoDoc)
        {
            //Declaracion de Varibles     
            var NegRolTipDoc = new NegRolesTiposDocumentos();
            var Resultado = new Resultado<EntityDoc.RolesTiposDocumentosInfo>();

            try
            {
                Resultado = NegRolTipDoc.Buscar(ObjetoRolTipoDoc);
                if (Resultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<EntityDoc.RolesTiposDocumentosInfo>(ref Grid, Resultado.Lista, new string[] { Constantes.StringId });
                }                
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "RolesTiposDocumentos"));
                }

            }

        }

        private void MarcarTodos(ref Anthem.GridView Grid, string Chek)
        {
            int x = 0;

            for (x = 0; x <= Grid.Rows.Count - 1; x++)
            {
                ((CheckBox)Grid.Rows[x].FindControl(Chek)).Checked = true;
            }
        }

        private void DesmarcarTodos(ref Anthem.GridView Grid, string Chek)
        {
            int x = 0;

            for (x = 0; x <= Grid.Rows.Count - 1; x++)
            {
                ((CheckBox)Grid.Rows[x].FindControl(Chek)).Checked = false;
            }
        }

        private bool ValidarFormularioBusqueda()
        {

            if (ddlRoles.SelectedIndex == 0)
            {

                Controles.MensajeEnControl(ddlRoles.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarRol.ToString()));
                return false;
            }


            return true;
        }

        private void ProcesarAsignacion()
        {
            var ObjetoRolTipoDoc = new EntityDoc.RolesTiposDocumentos();
            var NegRolTipoDoc = new NegRolesTiposDocumentos();
            var ObjetoResultado = new Resultado<EntityDoc.RolesTiposDocumentos>();
            
            try
            {
                
                if (gvTiposDocumentos.Rows.Count != 0)
                {
                    foreach (GridViewRow Row in gvTiposDocumentos.Rows)
                    {
                        ObjetoRolTipoDoc.Id = int.Parse(gvTiposDocumentos.DataKeys[Row.RowIndex].Values[Constantes.StringId].ToString());
                        ObjetoRolTipoDoc.IdRol = int.Parse(ddlRoles.SelectedValue);
                        ObjetoRolTipoDoc.IdGrupoDocumento = int.Parse(((HiddenField)Row.FindControl("hdnIdGrupoDocumento")).Value);
                        ObjetoRolTipoDoc.IdTipoDocumento = int.Parse(((HiddenField)Row.FindControl("hdnIdTipoDocumento")).Value);
                        ObjetoRolTipoDoc.PermisoListar = ((CheckBox)Row.FindControl(Constantes.chkListar)).Checked;
                        ObjetoRolTipoDoc.PermisoVer = ((CheckBox)Row.FindControl(Constantes.chkVer)).Checked;
                        ObjetoRolTipoDoc.PermisoAbrir = ((CheckBox)Row.FindControl(Constantes.chkAbrir)).Checked;
                        ObjetoRolTipoDoc.PermisoModificar = ((CheckBox)Row.FindControl(Constantes.chkModificar)).Checked;
                        ObjetoRolTipoDoc.PermisoDescargar = ((CheckBox)Row.FindControl(Constantes.chkDescargar)).Checked;
                        ObjetoRolTipoDoc.PermisoEliminar = ((CheckBox)Row.FindControl(Constantes.chkEliminar)).Checked;
                        ObjetoRolTipoDoc.PermisoSubir = ((CheckBox)Row.FindControl(Constantes.chkSubir)).Checked;

                        ObjetoResultado = NegRolTipoDoc.Guardar(ObjetoRolTipoDoc);

                    }
                }

                if (ObjetoResultado.ResultadoGeneral == false)
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                    return;
                }
                else
                {
                    Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.AsignacionRolesTipDocumento.ToString()));
                    return;
                }

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorProcesarAsignacionMenu.ToString()));
                }

            }
        }

        #endregion
    }
}