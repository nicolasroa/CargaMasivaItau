using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.WebControls;
using Documental.Global;
using Documental.Entidades;
using Documental.Negocio.Administracion;

namespace Documental.OperacionesLogin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
        }

        private void ValidarUsuario()
        {
            //Declaración de Variables
            var NegUsuario = new NegUsuarios();
            var ObjetoResultado = new Resultado<UsuarioInfo>();
            var ObjetoUsuario = new UsuarioInfo();
            var ObjetoConfiguracion = new ConfiguracionGeneralInfo();

            //Validacion de Formulario
            if (txtUsuario.Text.Length == 0)
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarNombreUsuario.ToString()));
                return;
            }
            if (txtClave.Text.Length == 0)
            {
                Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarClave.ToString()));
                return;
            }

            //Asignación de Variables
            ObjetoUsuario.NombreUsuario = txtUsuario.Text;
            ObjetoUsuario.Clave = txtClave.Text;


            //CargarConfiguracionGeneral
            ObjetoConfiguracion = NegConfiguracionGeneral.Obtener();
            if (ObjetoConfiguracion == null)
            {
                Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorConfiguracionNoEncontrada.ToString()));
                return;
            }

            //Proceso de Validación

            ObjetoResultado = NegUsuario.Validar(ObjetoUsuario);
            if (ObjetoResultado.ResultadoGeneral == true)
            {
                if (ObjetoResultado.Objeto != null)
                {
                    NegUsuarios.UsuarioId = ObjetoResultado.Objeto.Id;
                }
                switch (ObjetoResultado.ValorString)
                {
                    case ConfigUsuario.ValidacionNoEncontrado:
                        Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(ConfigUsuario.MensajeNoExiste));
                        break;
                    case ConfigUsuario.ValidacionInactivo:
                        Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(ConfigUsuario.MensajeInactivo));
                        break;
                    case ConfigUsuario.ValidacionErrorClave:
                        IntentoFallido(ObjetoResultado.Objeto, ObjetoConfiguracion);
                        Controles.MostrarMensajeAlerta("Clave no corresponde, le quedan " + (ObjetoConfiguracion.Intentos - ObjetoResultado.Objeto.IntentosFallidos).ToString() + " Intentos");
                        break;
                    case ConfigUsuario.ValidacionCambioClave:
                        NegUsuarios.Usuario = ObjetoResultado.Objeto;

                        Controles.AbrirPopup(ConfigUsuario.UrlCambioContraseña, 700, 450, "Su Clave ha Caducado, Favor Realice el Cambio");
                        break;
                    case ConfigUsuario.ValidacionPrimerInicio:
                        NegUsuarios.Usuario = ObjetoResultado.Objeto;

                        Controles.AbrirPopup(ConfigUsuario.UrlCambioContraseña, 700, 450, "Primer Inicio, debe Cambiar la Contraseña.");
                        break;
                    case ConfigUsuario.ValidacionAvisoCambioClave:
                        NegUsuarios.Usuario = ObjetoResultado.Objeto;
                        NegUsuarios.MensajeCambioClave = "Le quedan " + ObjetoResultado.ValorInt.ToString() + " Dias para que la Contraseña caduque, desea realizar el cambio ahora?";
                        Controles.AbrirPopup(ConfigUsuario.UrlAvisoCambioContraseña, 700, 450, "Aviso de Cambio de Contraseña.");
                        break;
                    case ConfigUsuario.ValidacionAprobado:
                        NegUsuarios.Usuario = ObjetoResultado.Objeto;
                        NegUsuarios.UsuarioId = ObjetoResultado.Objeto.Id;

                        ReiniciarIntentos(ObjetoResultado.Objeto);
                        //Response.Redirect(Constantes.UrlInicio);
                        Controles.CerrarConCargaMenu();
                        break;
                }
            }
        }

        private void IntentoFallido(UsuarioInfo ObjetoUsuario, ConfiguracionGeneralInfo ObjetoConfiguracion)
        {

            var NegUsuario = new NegUsuarios();
            var ObjetoResultado = new Resultado<UsuarioInfo>();
            try
            {
                if (ObjetoUsuario != null)
                {
                    if (ObjetoUsuario.IntentosFallidos == null)
                    {
                        ObjetoUsuario.IntentosFallidos = 1;
                    }
                    else
                    {
                        ObjetoUsuario.IntentosFallidos++;
                    }

                    if (ObjetoUsuario.IntentosFallidos >= ObjetoConfiguracion.Intentos)
                    {
                        ObjetoUsuario.Estado_Id = (int)NegTablas.IdentificadorMaestro(ConfigBase.TablaEstado, ConfigBase.CodigoInactivo);
                    }

                    ObjetoResultado = NegUsuario.Guardar(ObjetoUsuario);

                    if (!ObjetoResultado.ResultadoGeneral)
                    {
                        Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString() + "Usuario"));

                }
            }
        }

        private void ReiniciarIntentos(UsuarioInfo ObjetoUsuario)
        {
            var NegUsuario = new NegUsuarios();
            var ObjetoResultado = new Resultado<UsuarioInfo>();
            try
            {

                ObjetoUsuario.IntentosFallidos = 0;
                ObjetoUsuario.UsuarioModificacion_Id = ((UsuarioInfo)NegUsuarios.Usuario).Id;

                ObjetoResultado = NegUsuario.Guardar(ObjetoUsuario);

                if (!ObjetoResultado.ResultadoGeneral)
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

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            ValidarUsuario();
        }

        protected void lkbtnRecuperarContraseña_Click(object sender, EventArgs e)
        {
            Controles.AbrirPopup(ConfigUsuario.UrlRecuperarContraseña, 700, 450, "Recuperar Contraseña");
        }
    }
}