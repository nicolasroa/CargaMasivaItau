using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Documental.Entidades;
using Documental.Global;
using Documental.Negocio.Administracion;

namespace WebSite.Administracion
{
    public partial class UsuarioB : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarEstados();
                CargarRoles();
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrid();
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
        protected void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormulario();", true);
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            LimpiarFormulario();
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusqueda();", true);
        }
        protected void gvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBusqueda.PageIndex = e.NewPageIndex;
            CargarGrid();
        }
        protected void btnModificar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvBusqueda.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            ObtenerDatos(Id);
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormulario();", true);
        }
        #endregion
        #region Metodos

        //Carga Inicial
        private void CargarEstados()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo(ConfigBase.TablaEstado);
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlEstado, Lista, Constantes.StringId, Constantes.StringNombre, "-- Todos --", "-1");
                    Controles.CargarCombo<TablaInfo>(ref ddlFormEstado, Lista, Constantes.StringId, Constantes.StringNombre, "-- Estado --", "-1");
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo " + ConfigBase.TablaEstado + " Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString() + "Tablas Estado"));
                }
            }
        }
        private void MostrarMensajeValidacion(string Validacion)
        {
            Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Validacion));
        }
        private void CargarRoles()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo(ConfigBase.TablaRoles);
                if (Lista != null)
                {
                    Controles.CargarListBox<TablaInfo>(ref lstbFormRoles, Lista, Constantes.StringId, Constantes.StringNombre);
                }
                else
                {
                    Controles.MostrarMensajeAlerta("Catálogo " + ConfigBase.TablaEstado + " Sin Datos");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString() + "Tablas Estado"));
                }
            }


        }

        //Metodos Generales
        private bool ValidarFormulario()
        {
            int Seleccionados = 0;
            if (txtFormNombre.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormNombre.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarNombre.ToString()));
                return false;
            }
            if (txtFormApellido.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormApellido.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarApellido.ToString()));
                return false;
            }
            if (txtFormMail.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormMail.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarMail.ToString()));
                return false;
            }
            if (txtFormRut.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFormRut.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarRut.ToString()));
                return false;
            }
            if (ddlFormEstado.SelectedValue == "-1")
            {
                Controles.MensajeEnControl(ddlFormEstado.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarEstado.ToString()));
                return false;
            }

            for (int index = 0; index <= lstbFormRoles.Items.Count - 1; index++)
            {
                if (lstbFormRoles.Items[index].Selected == true)
                    Seleccionados++;
            }

            if (Seleccionados == 0)
            {
                Controles.MensajeEnControl(lstbFormRoles.ClientID,ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarRol.ToString()));
                return false;
            }


            return true;

        }
        private void GuardarEntidad()
        {
            try
            {
                //Declaración de Variables
                var ObjetoUsuario = new UsuarioInfo();
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var NegUsuario = new NegUsuarios();

                if (!ValidarFormulario()) { return; }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    ObjetoUsuario.Id = int.Parse(hfId.Value.ToString());
                    ObjetoUsuario = DatosEntidad(ObjetoUsuario);
                }
                else
                {
                    ObjetoUsuario.IntentosFallidos = 0;
                    ObjetoUsuario.UltimoCambioClave = DateTime.Now;
                    ObjetoUsuario.Clave = NegUsuarios.GenerarClave();
                    ObjetoUsuario.PrimerInicio = true;
                }
                ObjetoUsuario.NombreUsuario = txtFormNombreUsuario.Text;
                ObjetoUsuario.Nombre = txtFormNombre.Text;
                ObjetoUsuario.Apellido = txtFormApellido.Text;
                ObjetoUsuario.Mail = txtFormMail.Text;
                ObjetoUsuario.Rut = txtFormRut.Text;
                ObjetoUsuario.Estado_Id = int.Parse(ddlFormEstado.SelectedValue);
                ObjetoUsuario.AdministradorAplicaciones = chkAdministracionAplicaciones.Checked;
                //Ejecucion del procedo para Guardar
                ObjetoResultado = NegUsuario.Guardar(ref ObjetoUsuario);

                if (ObjetoResultado.ResultadoGeneral)
                {
                    AsignarRoles(ObjetoUsuario);
                    if (hfId.Value.Length == 0)
                    {
                        Mail.EnviarMensajeNuevoUsuario(ObjetoUsuario, NegConfiguracionGeneral.Obtener());
                    }

                    LimpiarFormulario();
                    txtRut.Text = ObjetoUsuario.Rut;
                    CargarGrid();
                    txtRut.Text = "";
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString() + "Usuario"));
                }
            }
        }
        private void CargarGrid()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var ObjetoUsuario = new UsuarioInfo();
                var NegUsuarios = new NegUsuarios();
                var ObjetoResultado = new Resultado<UsuarioInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoUsuario.Nombre = txtNombre.Text;
                ObjetoUsuario.Apellido = txtApellido.Text;
                ObjetoUsuario.Rut = txtRut.Text;
                ObjetoUsuario.Estado_Id = int.Parse(ddlEstado.SelectedValue);

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegUsuarios.Buscar(ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<UsuarioInfo>(ref gvBusqueda, ObjetoResultado.Lista, new string[] { Constantes.StringId, Constantes.StringNombre });
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString() + "Usuarios"));
                }
            }
        }
        private void ObtenerDatos(int Id)
        {
            try
            {
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var ObjetoUsuario = new UsuarioInfo();
                var NegUsuario = new NegUsuarios();

                ObjetoUsuario.Id = Id;
                ObjetoResultado = NegUsuario.Buscar(ObjetoUsuario);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoUsuario = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoUsuario != null)
                    {
                        LlenarFormulario(ObjetoUsuario);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "Usuario"));
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "Usuario"));
                }
            }
        }
        private UsuarioInfo DatosEntidad(UsuarioInfo Entidad)
        {
            try
            {
                var ObjetoResultado = new Resultado<UsuarioInfo>();
                var ObjetoUsuario = new UsuarioInfo();
                var NegUsuario = new NegUsuarios();

                ObjetoResultado = NegUsuario.Buscar(Entidad);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoUsuario = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoUsuario != null)
                    {
                        return ObjetoUsuario;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "Usuario"));
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "Usuario"));

                }
                return null;
            }
        }
        private void LlenarFormulario(UsuarioInfo ObjetoUsuario)
        {
            try
            {
                LimpiarFormulario();
                hfId.Value = ObjetoUsuario.Id.ToString();
                txtFormNombreUsuario.Text = ObjetoUsuario.NombreUsuario;
                txtFormNombre.Text = ObjetoUsuario.Nombre;
                txtFormApellido.Text = ObjetoUsuario.Apellido;
                txtFormMail.Text = ObjetoUsuario.Mail;
                txtFormRut.Text = ObjetoUsuario.Rut;
                ddlFormEstado.SelectedValue = ObjetoUsuario.Estado_Id.ToString();
                chkAdministracionAplicaciones.Checked = ObjetoUsuario.AdministradorAplicaciones == null ? false : (bool)ObjetoUsuario.AdministradorAplicaciones;
                CargarRolesAsignados(ObjetoUsuario.Id);


            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString() + "Usuarios"));
                }
            }
        }
        private void LimpiarFormulario()
        {
            txtFormNombreUsuario.Text = "";
            txtFormNombre.Text = "";
            txtFormApellido.Text = "";
            txtFormMail.Text = "";
            txtFormRut.Text = "";
            ddlFormEstado.ClearSelection();
            chkAdministracionAplicaciones.Checked = false;
            lstbFormRoles.ClearSelection();
            hfId.Value = "";
        }
        private void AsignarRoles(UsuarioInfo Entidad)
        {
            //Declaracion de Variables
            var ObjetoUsuarioRol = new UsuarioRolInfo();
            var NegUsuario = new NegUsuarios();
            var ObjetoResultado = new Resultado<UsuarioRolInfo>();
            //Variables de Busqueda de Usuario para la Asignación
            var ObjetoResultadoUsuario = new Resultado<UsuarioInfo>();


            ObjetoUsuarioRol.Usuario_Id = Entidad.Id;

            foreach (ListItem item in lstbFormRoles.Items)
            {
                if (item.Selected == true)
                {
                    ObjetoUsuarioRol.Rol_Id = int.Parse(item.Value.ToString());

                    ObjetoResultado = NegUsuario.AsignarUsuarioRol(ObjetoUsuarioRol);
                    if (ObjetoResultado.ResultadoGeneral == false)
                    {
                        Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        return;
                    }
                }
                else
                {
                    ObjetoUsuarioRol.Rol_Id = int.Parse(item.Value.ToString());

                    ObjetoResultado = NegUsuario.DesasignarUsuarioRol(ObjetoUsuarioRol);
                    if (ObjetoResultado.ResultadoGeneral == false)
                    {
                        Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        return;
                    }
                }

            }





        }
        private void CargarRolesAsignados(int Usuario_Id)
        {
            try
            {
                //Declaracion de Variables
                var ObjetoUsuarioRol = new UsuarioRolInfo();
                var ObjetoResultado = new Resultado<UsuarioRolInfo>();
                var NegUsuario = new NegUsuarios();

                //Asignacion de Variables
                ObjetoUsuarioRol.Usuario_Id = Usuario_Id;
                ObjetoResultado = NegUsuario.BuscarUsuarioRol(ObjetoUsuarioRol);
                lstbFormRoles.ClearSelection();
                if (ObjetoResultado.ResultadoGeneral)
                {
                    if (ObjetoResultado.Lista != null)
                    {
                        for (int x = 0; x <= ObjetoResultado.Lista.Count - 1; x++)
                        {
                            for (int y = 0; y <= lstbFormRoles.Items.Count - 1; y++)
                            {
                                if (int.Parse(lstbFormRoles.Items[y].Value) == ObjetoResultado.Lista[x].Rol_Id)
                                    lstbFormRoles.Items[y].Selected = true;
                            }
                        }
                    }
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString() + "Usuarios"));
                }
            }
        }
        #endregion
    }
}