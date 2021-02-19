using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Documental.Entidades;
using EntityDOC = Documental.Entidades.Documentos;
using Documental.Global;
using Documental.Negocio.Documentos;
using Documental.Negocio.Administracion;
using Documental.Documentos;

namespace Documental.WebApp.Documentos
{
    public partial class TiposDocumentos : System.Web.UI.Page
    {

        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);

            if (!Page.IsPostBack)
            {
                CargarControles();
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrid();
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormulario();", true);
        }

        protected void gvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBusqueda.PageIndex = e.NewPageIndex;
            CargarGrid();
        }

        protected void btnModificarTipoDocumento_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            ObtenerDatos(Id);
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormulario();", true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var Permisos = (RolMenu)NegMenus.Permisos;
            if (Permisos.PermisoCrear == false && hfId.Value == "")
            {
                MostrarMensajeValidacion(Constantes.MensajesUsuario.SinPermisoCrear.ToString());
                return;
            }
            if (Permisos.PermisoModificar == false && hfId.Value != "")
            {
                MostrarMensajeValidacion(Constantes.MensajesUsuario.SinPermisoModificar.ToString());
                return;
            }

            GuardarEntidad();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusqueda();", true);
        }

        #endregion




        #region METODOS

        private void CargarControles()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var ObjetoGrupoDoc = new EntityDOC.GruposDocumentos();
                var NegGrupoDoc = new NegGruposDocumentos();
                var ObjetoResultado = new Resultado<EntityDOC.GruposDocumentos>();

                //Asignación de Variables de Búsqueda
                ObjetoGrupoDoc.Id = -1;
                ObjetoGrupoDoc.GrupoDocumento = "";
                ObjetoGrupoDoc.CodGrupoDocumento = "";
                ObjetoGrupoDoc.Estado_Id = -1;

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegGrupoDoc.Buscar(ObjetoGrupoDoc);

                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<EntityDOC.GruposDocumentos>(ref ddlGruposDocumentosB, ObjetoResultado.Lista, Constantes.StringId, "GrupoDocumento", "-- Todos --", "-1");
                    Controles.CargarCombo<EntityDOC.GruposDocumentos>(ref ddlGrupoDocumento, ObjetoResultado.Lista, Constantes.StringId, "GrupoDocumento", "-- Seleccione --", "-1");
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

        private void CargarGrid()
        {
            try
            {

                //Declaración de Variables de Búsqueda
                var ObjetoTipoDoc = new EntityDOC.TiposDocumentosInfo();
                var NegTipoDoc = new NegTiposDocumentos();
                var ObjetoResultado = new Resultado<EntityDOC.TiposDocumentosInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoTipoDoc.Id = -1;
                ObjetoTipoDoc.IdGrupoDocumento = int.Parse(ddlGruposDocumentosB.SelectedValue);
                ObjetoTipoDoc.CodTipoDocumento = txtCodigoTipoDocumentoB.Text;
                ObjetoTipoDoc.TipoDocumento = txtTipoDocumentoB.Text;
                ObjetoTipoDoc.Estado_Id = 0;

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegTipoDoc.Buscar(ObjetoTipoDoc);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<EntityDOC.TiposDocumentosInfo>(ref gvBusqueda, ObjetoResultado.Lista, new string[] { Constantes.StringId });
                    lblContador.Text = ObjetoResultado.ValorDecimal.ToString() + " Registro(s) Encontrado(s)";
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString() + "Tablas"));
                }
            }
        }

        private void LimpiarFormulario()
        {
            hfId.Value = "";
            ddlGrupoDocumento.SelectedIndex = 0;
            txtCodigoTipoDocumento.Text = "";
            txtTipoDocumentos.Text = "";
            txtDiasCaducidad.Text = "";
            txtDiasAvisoCaducidad.Text = "";
            chkHabilitado.Checked = false;

        }

        private void GuardarEntidad()
        {
            try
            {
                //Declaración de Variables
                var ObjetoTipoDoc = new EntityDOC.TiposDocumentosInfo();
                var NegTipoDoc = new NegTiposDocumentos();
                var ObjetoResultado = new Resultado<EntityDOC.TiposDocumentosInfo>();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    ObjetoTipoDoc.Id = int.Parse(hfId.Value.ToString());
                    ObjetoTipoDoc = DatosEntidad(ObjetoTipoDoc);
                }

                ObjetoTipoDoc.IdGrupoDocumento = int.Parse(ddlGrupoDocumento.SelectedValue);
                ObjetoTipoDoc.TipoDocumento = txtTipoDocumentos.Text;
                ObjetoTipoDoc.CodTipoDocumento = txtCodigoTipoDocumento.Text;
                ObjetoTipoDoc.Estado_Id = chkHabilitado.Checked == true ? 1 : 0;
                ObjetoTipoDoc.DiasCaducidad = int.Parse(txtDiasCaducidad.Text);
                ObjetoTipoDoc.DiasAvisoCaducidad = int.Parse(txtDiasAvisoCaducidad.Text);


                //Ejecucion del procedo para Guardar
                ObjetoResultado = NegTipoDoc.Guardar(ObjetoTipoDoc);

                if (ObjetoResultado.ResultadoGeneral)
                {
                    LimpiarFormulario();
                    //txtNombre.Text = ObjetoGrupoDoc;
                    CargarGrid();
                    //txtNombre.Text = "";
                    Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.RegistroGuardado.ToString()));
                    Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusqueda();", true);
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString() + "TiposDocumentos"));
                }
            }
        }

        private bool ValidarFormulario()
        {

            if (txtCodigoTipoDocumento.Text.Length == 0)
            {

                Controles.MensajeEnControl(txtCodigoTipoDocumento.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarCodigoTipoDocumento.ToString()));
                return false;
            }

            if (txtTipoDocumentos.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtTipoDocumentos.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarNombreTipoDocumento.ToString()));
                return false;
            }

            if (ddlGrupoDocumento.SelectedIndex == 0)
            {

                Controles.MensajeEnControl(ddlGrupoDocumento.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarNombreGrupoDocumento.ToString()));
                return false;
            }

            if (txtDiasAvisoCaducidad.Text.Length == 0)
            {
                txtDiasAvisoCaducidad.Text = "0";
            }
            if (txtDiasCaducidad.Text.Length == 0 || txtDiasCaducidad.Text == "0")
            {
                Controles.MensajeEnControl(txtDiasCaducidad.ClientID, "Debe Ingresar un Valor Mayor a 0");
                return false;
            }


            return true;
        }

        private void MostrarMensajeValidacion(string Validacion)
        {
            Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Validacion));
        }

        private void LlenarFormulario(EntityDOC.TiposDocumentosInfo ObjetoTipoDocumento)
        {
            try
            {
                hfId.Value = ObjetoTipoDocumento.Id.ToString();
                txtCodigoTipoDocumento.Text = ObjetoTipoDocumento.CodTipoDocumento;
                txtTipoDocumentos.Text = ObjetoTipoDocumento.TipoDocumento;
                ddlGrupoDocumento.SelectedValue = ObjetoTipoDocumento.IdGrupoDocumento.ToString();
                chkHabilitado.Checked = ObjetoTipoDocumento.Estado_Id == 1 ? true : false;
                txtDiasCaducidad.Text = ObjetoTipoDocumento.DiasCaducidad.ToString();
                txtDiasAvisoCaducidad.Text = ObjetoTipoDocumento.DiasAvisoCaducidad.ToString();

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString() + "TipoDocumento"));
                }
            }
        }

        private EntityDOC.TiposDocumentosInfo DatosEntidad(EntityDOC.TiposDocumentosInfo Entidad)
        {
            try
            {
                var ObjetoTipoDoc = new EntityDOC.TiposDocumentosInfo();
                var NegTipoDoc = new NegTiposDocumentos();
                var ObjetoResultado = new Resultado<EntityDOC.TiposDocumentosInfo>();

                ObjetoResultado = NegTipoDoc.Buscar(Entidad);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoTipoDoc = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoTipoDoc != null)
                    {
                        return ObjetoTipoDoc;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "TipoDocumento"));
                        }
                        return null;
                    }
                }
                else
                {
                    return null;
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "TipoDocumento"));
                }
                return null;
            }
        }

        private void ObtenerDatos(int Id)
        {
            try
            {
                var ObjetoTipoDoc = new EntityDOC.TiposDocumentosInfo();
                var NegTipoDoc = new NegTiposDocumentos();
                var ObjetoResultado = new Resultado<EntityDOC.TiposDocumentosInfo>();

                ObjetoTipoDoc.Id = Id;
                ObjetoResultado = NegTipoDoc.Buscar(ObjetoTipoDoc);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoTipoDoc = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoTipoDoc != null)
                    {
                        LlenarFormulario(ObjetoTipoDoc);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "TipoDocumento"));
                        }
                    }
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "TipoDocumento"));
                }
            }

        }

        #endregion


    }
}