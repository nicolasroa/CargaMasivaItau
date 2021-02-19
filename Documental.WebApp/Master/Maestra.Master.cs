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

namespace Documental
{
    public partial class Maestra : System.Web.UI.MasterPage
    {
        #region EVENTOS
        protected void Page_Init(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);

            ValidarSession();

            if (!Page.IsPostBack)
            {
                Controles.EjecutarJavaScript("Inicio();");
            }
        }

        protected void btnLogin_Click1(object sender, EventArgs e)
        {
            
        }
        protected void imgBtnLogo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Portal.aspx");
        }
        #endregion

        #region METODOS
        private void ValidarSession()
        {
            if (NegUsuarios.Usuario != null)
            {
                if (NegUsuarios.Usuario.Id != -1)
                {
                    CargarMenu();
                    ValidaControles();
                    ValidarPagina();
                }
            }
            else
            {
                NegUsuarios.Usuario = new UsuarioInfo();
                Response.Redirect("~/Portal.aspx");
            }
        }

        private void CargarMenu()
        {
            try
            {
                var ObjetoUsuario = new UsuarioInfo();
                var ObjetoResultado = new Resultado<MenuInfo>();
                var NegMenu = new NegMenus();
                if (ObjetoUsuario.UsuarioCreacion_Id != null)
                {
                    ObjetoUsuario.Id = (int)ObjetoUsuario.UsuarioCreacion_Id;
                }

                ObjetoUsuario.Id = NegUsuarios.Usuario.Id;

                //ObjetoResultado = NegMenu.CargarMenu(ObjetoUsuario);
                ObjetoResultado = NegMenu.CargarMenu(ref rdMenuPrincipal, ObjetoUsuario);
                if (ObjetoResultado.ResultadoGeneral == false)
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                }
                else
                {
                   
                    rdMenuPrincipal.Visible = true;
                    Controles.EjecutarJavaScript("MostrarMenu('" + ObjetoResultado.ValorString + "');");

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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString() + "Menu"));
                }
            }
        }
        private void ValidarPagina()
        {
            try
            {
                var ObjetoRolMenu = new RolMenu();
                var NegMenu = new NegMenus();
                string Pagina = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;
                String[] url = HttpContext.Current.Request.RawUrl.Split('?');
                var ObjetoUsuario = new UsuarioInfo();
                if (ObjetoUsuario.UsuarioCreacion_Id != null)
                    ObjetoUsuario.Id = (int)ObjetoUsuario.UsuarioCreacion_Id;


                if (url.Count() > 1 && (url[1].Contains("%7e") || url[1].Contains("~")))
                    Pagina = url[1].Replace("%2f", "/").Replace("%7e", "~");
                ObjetoRolMenu.Usuario_Id = ObjetoUsuario.Id;
                ObjetoRolMenu.Url = Pagina;
                ObjetoRolMenu = NegMenu.ValidarAcceso(ObjetoRolMenu);
                //if (ObjetoRolMenu == null)
                //{
                //    Response.Redirect(Constantes.UrlSinAcceso);
                //}
                //if (ObjetoRolMenu.Acceso == false)
                //{
                //    Response.Redirect(Constantes.UrlSinAcceso);

                //}
                NegMenus.Permisos = ObjetoRolMenu;
                this.Page.Title = ObjetoRolMenu.TituloMenu == null ? ArchivoRecursos.ObtenerValorNodo(Constantes.DatosSistema.NombreAplicacion.ToString()) : "WebApp - " + ObjetoRolMenu.TituloMenu;
            }
            catch (Exception ex)
            {
                Controles.MostrarMensajeError(ex.Message);
            }
        }
        private void CerrarSesion()
        {
            Session.Clear();
            Response.Redirect("~/Portal.aspx");
        }
        private void ValidaControles()
        {
            try
            {
                var NegControles = new NegControles();
                var ResultadoControles = new Resultado<RolMenuControlInfo>();
                var ObjetoControles = new RolMenuControlInfo();



                string Pagina = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;
                String[] url = HttpContext.Current.Request.RawUrl.Split('?');

                if (url.Count() > 1 && (url[1].Contains("%7e") || url[1].Contains("~")))
                    Pagina = url[1].Replace("%2f", "/").Replace("%7e", "~");

                ObjetoControles.Url = Pagina;
                if (ObjetoControles.UsuarioCreacion_Id != null)
                {
                    ObjetoControles.Usuario_Id = (int)ObjetoControles.UsuarioCreacion_Id;
                }
                else
                    return;

                ResultadoControles = NegControles.BuscarControlesPorUsuario(ObjetoControles);
                if (ResultadoControles.ResultadoGeneral == false)
                    return;
                else
                {
                    foreach (Control ControlesMaster in Page.Controls)
                    {
                        foreach (Control ControlesPrimarios in ControlesMaster.Controls)
                        {
                            foreach (Control ControlesAValidar in ControlesPrimarios.Controls)
                            {

                                foreach (var item in ResultadoControles.Lista)
                                {
                                    var control = ControlesAValidar.FindControl(item.IdInterno);
                                    if (control != null)
                                    {
                                        control.Visible = item.Visible;
                                        ((WebControl)control).Enabled = item.Activo;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Controles.MostrarMensajeError(ex.Message);
            }
        }



        #endregion


        private void IsPostBackJavaScript()
        {
            hdnIsPostBack.Value = IsPostBack ? "Y" : "N";
        }

        public Anthem.Button CerrarPopup
        {
            get { return btnCerrarPopup; }
        }

       

        protected void imgIniciarSesion_Click(object sender, ImageClickEventArgs e)
        {
            Controles.AbrirPopup("~/OperacionesLogin/Login.aspx", 700, 500, "Ingreso al Sistema");
        }

        protected void lnkIniciarSesion_Click(object sender, EventArgs e)
        {
            Controles.AbrirPopup("~/OperacionesLogin/Login.aspx", 700, 500, "Ingreso al Sistema");
        }

        protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            CerrarSesion();
        }

        protected void imgCerrarSesion_Click(object sender, ImageClickEventArgs e)
        {
            CerrarSesion();
        }
    }





}