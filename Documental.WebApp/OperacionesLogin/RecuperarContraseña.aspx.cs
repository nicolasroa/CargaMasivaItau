using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Documental.Entidades;
using Documental.Global;
using Documental.Negocio.Administracion;

namespace WebSite.OperacionesLogin
{
    public partial class RecuperarContraseña : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnValidarUsuario_Click(object sender, EventArgs e)
        {
            Validar();
        }
        protected void btnRecuperarClave_Click(object sender, EventArgs e)
        {
            RecuperarClave();
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Controles.CerrarPopup();
        }

        #endregion


        #region Metodos

        private void Validar()
        {
            var ObjetoResultado = new Resultado<UsuarioInfo>();
            var ObjetoUsuario = new UsuarioInfo();
            var NegUsuario = new NegUsuarios();

            ObjetoUsuario.NombreUsuario = txtUsuario.Text;
            ObjetoResultado = NegUsuario.ObtenerUsuario(ObjetoUsuario);
            if (ObjetoResultado.Objeto == null)
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(ConfigUsuario.MensajeNoExiste));
                return;
            }
            else
            {
                NegUsuarios.UsuarioRecuperarClave = ObjetoResultado.Objeto;
                
                NegUsuarios.UsuarioId = ObjetoResultado.Objeto.Id;
                hfUsuarioId.Value = ObjetoResultado.Objeto.Id.ToString();
                lbPregunta.Text = ObjetoResultado.Objeto.PreguntaSeguridad;
            }
        }


        private void RecuperarClave()
        {
            var ObjetoUsuario = new UsuarioInfo();
            var ObjetoResultado = new Resultado<UsuarioInfo>();
            var NegUsuario = new NegUsuarios();


            if (NegUsuarios.UsuarioRecuperarClave == null)
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(ConfigUsuario.ValidacionNoEncontrado));
                return;
            }
            else
            {
                ObjetoUsuario = (UsuarioInfo)NegUsuarios.UsuarioRecuperarClave;

                if (lbPregunta.Text.Length != 0)
                {
                    if (Seguridad.Desencriptar(ObjetoUsuario.RespuestaSeguridad) != txtRespuesta.Text)
                    {
                        Controles.MostrarMensajeAlerta("Respuesta Incorrecta");
                        return;
                    }
                    else
                    {
                        CambiarClave(ObjetoUsuario);

                    }
                }
                else
                {
                    CambiarClave(ObjetoUsuario);
                }


            }
        }


        private void CambiarClave(UsuarioInfo ObjetoUsuario)
        {
            var ObjetoResultado = new Resultado<UsuarioInfo>();
            var NegUsuario = new NegUsuarios();

            ObjetoUsuario.Clave = NegUsuarios.GenerarClave();
            ObjetoUsuario.UltimoCambioClave = DateTime.Now;
            ObjetoUsuario.PrimerInicio = true;

            ObjetoResultado = NegUsuario.Guardar(ObjetoUsuario);

            if (ObjetoResultado.ResultadoGeneral)
            {
                if (Mail.EnviarMensajeRecuperacionClave(ObjetoUsuario, NegConfiguracionGeneral.Obtener()))
                {
                    btnCancelar.Text = "Salir";
                    pnlLogin.Visible = false;

                    Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.RecuperacionClaveCorrecta.ToString()));
                }
            }
            else
            {
                Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
            }
        }
        #endregion
    }
}