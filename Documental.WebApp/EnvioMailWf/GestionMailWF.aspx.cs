using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Documental.Entidades;
using Documental.Entidades.EnvioMailWF;
using Documental.Negocio.Administracion;
using Documental.Negocio.EnvioMailWF;
using Documental.Global;

namespace Documental.WebApp.EnvioMailWf
{
    public partial class GestionMailWF : System.Web.UI.Page
    {

        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                CargarEstados();
                //CargarFlujos();
                //CargarDestinatario();
                //CargarVariablesWf();
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormulario();", true);
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarBusqueda();", true);
        }
        protected void ddlFormFlujo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFormFlujo.SelectedValue != "-1")
            {
                CargarEventos(int.Parse(ddlFormFlujo.SelectedValue), ref ddlFormEvento);
            }
            else
            {
                ddlFormEvento.DataSource = null;
                ddlFormEvento.DataBind();
                ddlFormDecision.DataSource = null;
                ddlFormDecision.DataBind();
            }
        }
        protected void ddlFormEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFormEvento.SelectedValue != "-1")
            {
                CargarDecisiones(int.Parse(ddlFormEvento.SelectedValue), ref ddlFormDecision);
            }
            else
            {
                ddlFormDecision.DataSource = null;
                ddlFormDecision.DataBind();
            }
        }
        protected void ddlFlujo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFlujo.SelectedValue != "-1")
            {
                CargarEventos(int.Parse(ddlFlujo.SelectedValue), ref ddlEvento);
            }
            else
            {
                ddlEvento.DataSource = null;
                ddlEvento.DataBind();
                ddlDecision.DataSource = null;
                ddlDecision.DataBind();
            }
        }
        protected void ddlEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEvento.SelectedValue != "-1")
            {
                CargarDecisiones(int.Parse(ddlEvento.SelectedValue), ref ddlDecision);
            }
            else
            {
                ddlDecision.DataSource = null;
                ddlDecision.DataBind();
            }
        }

        #endregion
        #region METODOS
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
        private void CargarVariablesWf()
        {
            try
            {

                var ObjResultado = new Resultado<VariablesWf>();
                var negEnvioMail = new NegEnvioMailWf();
                var oVariable = new VariablesWf();
                oVariable.Tipo = "Variables";

                ObjResultado = negEnvioMail.BuscarVariables(oVariable);


                if (ObjResultado.ResultadoGeneral)
                {
                    if (ObjResultado.Lista.Count != 0)
                    {
                        Controles.CargarListBox<VariablesWf>(ref lstFormVariablesWf, ObjResultado.Lista, Constantes.StringId, Constantes.StringNombre);
                    }
                    else
                    {
                        Controles.MostrarMensajeAlerta("No hay Variables de WF Configuradas");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString() + "Tablas Estado"));
                }
            }
        }
        private void CargarFlujos()
        {
            try
            {

                var ObjResultado = new Resultado<VariablesWf>();
                var negEnvioMail = new NegEnvioMailWf();
                var oVariable = new VariablesWf();
                oVariable.Tipo = "Flujos";

                ObjResultado = negEnvioMail.BuscarVariables(oVariable);


                if (ObjResultado.ResultadoGeneral)
                {
                    if (ObjResultado.Lista.Count != 0)
                    {
                        Controles.CargarCombo<VariablesWf>(ref ddlFlujo, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Todos --", "-1");
                        Controles.CargarCombo<VariablesWf>(ref ddlFormFlujo, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Flujos --", "-1");
                    }
                    else
                    {
                        Controles.MostrarMensajeAlerta("No hay Flujos Configurados");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString() + "Tablas Estado"));
                }
            }
        }
        private void CargarEventos(int IdFlujo, ref  Anthem.DropDownList Combo)
        {
            try
            {

                var ObjResultado = new Resultado<VariablesWf>();
                var negEnvioMail = new NegEnvioMailWf();
                var oVariable = new VariablesWf();
                oVariable.Tipo = "Eventos";
                oVariable.IdPadre = IdFlujo;

                ObjResultado = negEnvioMail.BuscarVariables(oVariable);


                if (ObjResultado.ResultadoGeneral)
                {
                    if (ObjResultado.Lista.Count != 0)
                    {
                        Controles.CargarCombo<VariablesWf>(ref Combo, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Eventos --", "-1");
                    }
                    else
                    {
                        Controles.MostrarMensajeAlerta("No hay Eventos Configurados");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString() + "Tablas Estado"));
                }
            }
        }
        private void CargarDecisiones(int IdEvento, ref  Anthem.DropDownList Combo)
        {
            try
            {
                var ObjResultado = new Resultado<VariablesWf>();
                var negEnvioMail = new NegEnvioMailWf();
                var oVariable = new VariablesWf();
                oVariable.Tipo = "Decisiones";
                oVariable.IdPadre = IdEvento;

                ObjResultado = negEnvioMail.BuscarVariables(oVariable);


                if (ObjResultado.ResultadoGeneral)
                {
                    if (ObjResultado.Lista.Count != 0)
                    {
                        Controles.CargarCombo<VariablesWf>(ref Combo, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Decisiones --", "-1");

                    }
                    else
                    {
                        Controles.MostrarMensajeAlerta("No hay Eventos Configurados");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString() + "Tablas Estado"));
                }
            }
        }
        private void CargarDestinatario()
        {
            try
            {

                var ObjResultado = new Resultado<VariablesWf>();
                var negEnvioMail = new NegEnvioMailWf();
                var oVariable = new VariablesWf();
                oVariable.Tipo = "Destinatarios";

                ObjResultado = negEnvioMail.BuscarVariables(oVariable);


                if (ObjResultado.ResultadoGeneral)
                {
                    if (ObjResultado.Lista.Count != 0)
                    {
                        Controles.CargarCombo<VariablesWf>(ref ddlFormDestinatario, ObjResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "-- Todos --", "-1");
                    }
                    else
                    {
                        Controles.MostrarMensajeAlerta("No hay Flujos Configurados");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString() + "Tablas Estado"));
                }
            }
        }

        private void LlenarFormulario(MailWfInfo Entidad)
        {
            try
            {
                ddlFormFlujo.SelectedValue = Entidad.IdFlujo.ToString();
                CargarEventos(Entidad.IdFlujo, ref ddlFormEvento);
                ddlFormEvento.SelectedValue = Entidad.IdEvento.ToString();
                CargarDecisiones(Entidad.IdEvento, ref ddlFormDecision);
                ddlDecision.SelectedValue = Entidad.IdDecision.ToString();
                txtFormTexto.Text = Entidad.Texto;
                CargarVariablesWf();

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

        #endregion

        protected void btnGuardar_Click(object sender, EventArgs e)
        {


        }

        protected void lstFormVariablesWf_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Item = lstFormVariablesWf.SelectedItem;
            txtFormTexto.Text = txtFormTexto.Text + " " + Item.Text;

        }







    }
}