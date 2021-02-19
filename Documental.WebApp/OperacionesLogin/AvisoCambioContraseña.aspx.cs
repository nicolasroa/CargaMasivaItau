using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using Documental.Entidades;
using Documental.Global;
using Documental.Negocio.Administracion;

namespace WebSite.OperacionesLogin
{
    public partial class AvisoCambioContraseña : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                lblMensaje.Text =  NegUsuarios.MensajeCambioClave.ToString();
               
            }
        }
        protected void btnSi_Click(object sender, EventArgs e)
        {
            var ObjetoUsuario = new UsuarioInfo();
            ObjetoUsuario = (UsuarioInfo)NegUsuarios.Usuario;
            pnlConfirmacion.Visible = false;
            pnlOperacion.Visible = true;
            lblMensaje.Text = "";
            lblUsuario.Text = ObjetoUsuario.NombreCompleto;
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            var ObjetoUsuario = new UsuarioInfo();
            ObjetoUsuario = (UsuarioInfo)NegUsuarios.Usuario;
            NegUsuarios.UsuarioId = ObjetoUsuario.Id;
           NegUsuarios.DireccionarInicio = true;
            
            Controles.CerrarPopup();
            

        }
        protected void btnCambiarClave_Click(object sender, EventArgs e)
        {
            CambiarContraseña();
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Controles.CerrarPopup();
        }
        #endregion


        #region Metodos

       

        private void CambiarContraseña()
        {
            var ObjetoUsuario = new UsuarioInfo();
            var NegUsuario = new NegUsuarios();
            var ObjetoResultado = new Resultado<UsuarioInfo>();

            ObjetoUsuario = (UsuarioInfo)NegUsuarios.Usuario;

            if (Seguridad.Desencriptar(ObjetoUsuario.Clave) != txtClaveActual.Text)
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarClavesAnterior.ToString()));
                return;
            }
            if (txtClaveNueva.Text.Length == 0)
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarClave.ToString()));
                return;
            }
            if (txtClaveNueva.Text.CompareTo(txtClaveNueva2.Text) != 0)
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarClavesDistintas.ToString()));
                return;
            }
            ObjetoUsuario.Clave = Seguridad.Encriptar(txtClaveNueva.Text);
            ObjetoUsuario.UltimoCambioClave = DateTime.Now;
            ObjetoResultado = NegUsuario.Guardar(ObjetoUsuario);
            if (ObjetoResultado.ResultadoGeneral == true)
            {

                pnlOperacion.Visible = false;
                btnCambiarClave.Visible = false;
                btnCancelar.Text = "Salir";
                Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.CambioClaveCorrecto.ToString()));

            }
            else
            {
                Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
            }
        }

        #endregion

    

    }
}