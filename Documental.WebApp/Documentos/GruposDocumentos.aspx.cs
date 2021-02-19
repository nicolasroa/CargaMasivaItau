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

namespace Documental.Documentos
{
    public partial class GruposDocumentos : System.Web.UI.Page
    {
        
        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);

            if (!Page.IsPostBack)
            {

            }
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrid();
        }

        protected void gvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBusqueda.PageIndex = e.NewPageIndex;
            CargarGrid();
        }

        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
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

        protected void btnModificarGrupoDocumento_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            ObtenerDatos(Id);
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormulario();", true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusqueda();", true);
        }

        #endregion

        #region METODOS

        private void CargarGrid()
        {
            try
            {

                //Declaración de Variables de Búsqueda
                var ObjetoGrupoDoc = new EntityDOC.GruposDocumentos();
                var NegGrupoDoc = new NegGruposDocumentos();
                var ObjetoResultado = new Resultado<EntityDOC.GruposDocumentos>();

                //Asignación de Variables de Búsqueda
                ObjetoGrupoDoc.Id = -1;
                ObjetoGrupoDoc.GrupoDocumento = txtGrupoDocumento.Text;
                ObjetoGrupoDoc.CodGrupoDocumento = txtCodigoDocumento.Text;
                ObjetoGrupoDoc.Estado_Id = 0; 

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegGrupoDoc.Buscar(ObjetoGrupoDoc);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<EntityDOC.GruposDocumentos>(ref gvBusqueda, ObjetoResultado.Lista, new string[] { Constantes.StringId });
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
            txtCodGrupoDocumento.Text = "";
            txtGrupoDocumentos.Text = "";
            chkHabilitado.Checked = false;

        }

        private void GuardarEntidad()
        {
            try
            {
                //Declaración de Variables
                var ObjetoGrupoDoc = new EntityDOC.GruposDocumentos();
                var NegGrupoDoc = new NegGruposDocumentos();
                var ObjetoResultado = new Resultado<EntityDOC.GruposDocumentos>();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    ObjetoGrupoDoc.Id = int.Parse(hfId.Value.ToString());
                    ObjetoGrupoDoc = DatosEntidad(ObjetoGrupoDoc);
                }

                ObjetoGrupoDoc.CodGrupoDocumento = txtCodGrupoDocumento.Text;
                ObjetoGrupoDoc.GrupoDocumento = txtGrupoDocumentos.Text;
                ObjetoGrupoDoc.Estado_Id = chkHabilitado.Checked == true ? 1 : 0;

                //Ejecucion del procedo para Guardar
                ObjetoResultado = NegGrupoDoc.Guardar(ObjetoGrupoDoc);

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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString() + "GruposDocumentos"));
                }
            }
        }

        private bool ValidarFormulario()
        {

            if (txtCodGrupoDocumento.Text.Length == 0)
            {

                Controles.MensajeEnControl(txtCodGrupoDocumento.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarCodigoGrupoDocumento.ToString()));
                return false;
            }

            if (txtGrupoDocumentos.Text.Length == 0)
            {

                Controles.MensajeEnControl(txtGrupoDocumentos.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarNombreGrupoDocumento.ToString()));
                return false;
            }

            return true;
        }

        private void MostrarMensajeValidacion(string Validacion)
        {
            Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Validacion));
        }

        private void LlenarFormulario(EntityDOC.GruposDocumentos ObjetoGrupoDocumento)
        {
            try
            {
                hfId.Value = ObjetoGrupoDocumento.Id.ToString();
                txtCodGrupoDocumento.Text = ObjetoGrupoDocumento.CodGrupoDocumento;
                txtGrupoDocumentos.Text = ObjetoGrupoDocumento.GrupoDocumento;
                chkHabilitado.Checked = ObjetoGrupoDocumento.Estado_Id == 1 ? true : false;

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString() + "GrupoDocumento"));
                }
            }
        }

        private EntityDOC.GruposDocumentos DatosEntidad(EntityDOC.GruposDocumentos Entidad)
        {
            try
            {
                var ObjetoGrupoDoc = new EntityDOC.GruposDocumentos();
                var NegGrupoDoc = new NegGruposDocumentos();
                var ObjetoResultado = new Resultado<EntityDOC.GruposDocumentos>();

                ObjetoResultado = NegGrupoDoc.Buscar(Entidad);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoGrupoDoc = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoGrupoDoc != null)
                    {
                        return ObjetoGrupoDoc;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "GrupoDocumento"));
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "GrupoDocumento"));
                }
                return null;
            }
        }

        private void ObtenerDatos(int Id)
        {
            try
            {
                var ObjetoGrupoDoc = new EntityDOC.GruposDocumentos();
                var NegGrupoDoc = new NegGruposDocumentos();
                var ObjetoResultado = new Resultado<EntityDOC.GruposDocumentos>();

                ObjetoGrupoDoc.Id = Id;
                ObjetoResultado = NegGrupoDoc.Buscar(ObjetoGrupoDoc);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoGrupoDoc = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoGrupoDoc != null)
                    {
                        LlenarFormulario(ObjetoGrupoDoc);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "GrupoDocumento"));
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "GrupoDocumento"));
                }
            }

        }

        #endregion

       

        
               
        
        
    }
}