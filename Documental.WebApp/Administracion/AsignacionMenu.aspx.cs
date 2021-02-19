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
    public partial class AsignacionMenu : System.Web.UI.Page
    {

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "Inicio();", true);
            if (!Page.IsPostBack)
            {
                CargaRoles();
                CargarSessiones();
            }
        }
        protected void chkAccesoTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                MarcarTodos(ref gvMantenedores, Constantes.chkAcceso);
            else
                DesmarcarTodos(ref gvMantenedores, Constantes.chkAcceso);

        }
        protected void chkCrearTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                MarcarTodos(ref gvMantenedores, Constantes.chkCrear);
            else
                DesmarcarTodos(ref gvMantenedores, Constantes.chkCrear);

        }
        protected void chkModificarTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                MarcarTodos(ref gvMantenedores, Constantes.chkModificar);
            else
                DesmarcarTodos(ref gvMantenedores, Constantes.chkModificar);

        }
        protected void chkEliminarTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                MarcarTodos(ref gvMantenedores, Constantes.chkEliminar);
            else
                DesmarcarTodos(ref gvMantenedores, Constantes.chkEliminar);

        }
        protected void chkAcceso2Todos_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                MarcarTodos(ref gvOperacionales, Constantes.chkAcceso);
            else
                DesmarcarTodos(ref gvOperacionales, Constantes.chkAcceso);

        }
        protected void imgBotonExpandir_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            ListarHijos(gvMantenedores, row);
        }
        protected void imgBotonExpandirOperacionales_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            ListarHijos(gvOperacionales, row);
        }
        protected void btnVerAsignacion_Click(object sender, EventArgs e)
        {
            var ObjetoMenu = new MenuInfo();
            ObjetoMenu.Nivel = 2;
            ObjetoMenu.Mantenedor = true;
            ObjetoMenu.MenuPadre_Id = int.Parse(ddlMenu.SelectedValue);
            CargarGridAsignacion(ref gvMantenedores, ObjetoMenu);
            ObjetoMenu.Mantenedor = false;
            CargarGridAsignacion(ref gvOperacionales, ObjetoMenu);
        }
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            ProcesarAsignacion();
        }

        #endregion

        #region Metodos

        //Metodos de Carga Inicial
        private void CargaRoles()
        {
            try
            {
                var Lista = new List<TablaInfo>();

                Lista = NegTablas.BuscarCatalogo(ConfigBase.TablaRoles);
                if (Lista != null)
                {
                    Controles.CargarCombo<TablaInfo>(ref ddlRol, Lista, Constantes.StringId, Constantes.StringNombre, "", "-1");
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
        private void CargarSessiones()
        {
            try
            {
                //Declaracion de Varibles
                var ObjetoMenu = new MenuInfo();
                var NegMenu = new NegMenus();
                var ObjetoResultado = new Resultado<MenuInfo>();

                //Asignacion de Variables
                ObjetoMenu.Id = -1;
                ObjetoMenu.MenuPadre_Id = -1;
                ObjetoMenu.Descripcion = "";
                ObjetoMenu.Nivel = 1;

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegMenu.Buscar(ObjetoMenu);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarCombo<MenuInfo>(ref ddlMenu, ObjetoResultado.Lista, Constantes.StringId, Constantes.StringDescripcion, "", "");
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "Menu"));
                }
            }
        }

        //Metodos Particulares
        private void CargarGridAsignacion(ref Anthem.GridView Grid, MenuInfo ObjetoMenu)
        {
            //Declaracion de Varibles
            var ResultadoMenu = new Resultado<MenuInfo>();
            var ResultadoRolMenu = new Resultado<RolMenu>();
            var NegMenu = new NegMenus();
            var ObjetoRolMenu = new RolMenu();

            try
            {
                ResultadoMenu = NegMenu.Buscar(ObjetoMenu);
                if (ResultadoMenu.ResultadoGeneral)
                {
                    Controles.CargarGrid<MenuInfo>(ref Grid, ResultadoMenu.Lista, new string[] { "Id" });
                }
                string ID = Grid.ID;
                for (int i = 0; i <= Grid.Rows.Count - 1; i++)
                {
                    ObjetoRolMenu = new RolMenu();
                    ObjetoRolMenu.Menu_Id = int.Parse(Grid.DataKeys[i].Values["Id"].ToString());
                    ObjetoRolMenu.Rol_Id = int.Parse(ddlRol.SelectedValue.ToString());
                    ResultadoRolMenu = NegMenu.RolMenu(ObjetoRolMenu);

                    ObjetoRolMenu = ResultadoRolMenu.Objeto;
                    if (ObjetoRolMenu != null)
                    {
                        if (Grid.ID == Constantes.gvMantenedores || Grid.ID == Constantes.gvHijosMantenedores)
                        {
                            ((CheckBox)Grid.Rows[i].FindControl(Constantes.chkAcceso)).Checked = (ObjetoRolMenu.Acceso == true);

                            ((CheckBox)Grid.Rows[i].FindControl(Constantes.chkCrear)).Checked = (ObjetoRolMenu.PermisoCrear == true);

                            ((CheckBox)Grid.Rows[i].FindControl(Constantes.chkModificar)).Checked = (ObjetoRolMenu.PermisoModificar == true);

                            ((CheckBox)Grid.Rows[i].FindControl(Constantes.chkEliminar)).Checked = (ObjetoRolMenu.PermisoEliminar == true);
                        }
                        else if (Grid.ID == Constantes.gvOperacionales || Grid.ID == Constantes.gvHijosOperacionales)
                            ((CheckBox)Grid.Rows[i].FindControl(Constantes.chkAcceso)).Checked = (ObjetoRolMenu.Acceso == true);
                    }
                    else
                        ((CheckBox)Grid.Rows[i].FindControl(Constantes.chkAcceso)).Checked = false;

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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "ConfiguracionGeneral"));
                }

            }

        }
        private void ListarHijos(Anthem.GridView Grid, GridViewRow row)
        {
            int index = 0;
            int idPadre;

            Anthem.GridView GridHijos = new Anthem.GridView();

            try
            {
                for (index = 0; index <= Grid.Rows.Count - 1; index++)
                {
                    if (index != row.RowIndex)      
                    {
                        ((PlaceHolder)Grid.Rows[index].FindControl("Operacionales")).Visible = false;
                        ((PlaceHolder)Grid.Rows[index].FindControl("Mantenedores")).Visible = false;
                        ((ImageButton)Grid.Rows[index].FindControl("imgBotonExpandir")).ImageUrl = Constantes.UrlImgPlus;
                    }
                }

                if (((ImageButton)Grid.Rows[row.RowIndex].FindControl("imgBotonExpandir")).ImageUrl == Constantes.UrlImgMinus)
                {
                    ((PlaceHolder)Grid.Rows[row.RowIndex].FindControl("Operacionales")).Visible = false;
                    ((PlaceHolder)Grid.Rows[row.RowIndex].FindControl("Mantenedores")).Visible = false;
                    ((ImageButton)Grid.Rows[row.RowIndex].FindControl("imgBotonExpandir")).ImageUrl = Constantes.UrlImgPlus;
                }
                else
                {
                    ((PlaceHolder)Grid.Rows[row.RowIndex].FindControl("Operacionales")).Visible = true;
                    ((PlaceHolder)Grid.Rows[row.RowIndex].FindControl("Mantenedores")).Visible = true;
                    ((ImageButton)Grid.Rows[row.RowIndex].FindControl("imgBotonExpandir")).ImageUrl = Constantes.UrlImgMinus;

                    idPadre = (int)(Grid.DataKeys[row.RowIndex].Values["Id"]);

                    GridHijos = ((Anthem.GridView)Grid.Rows[row.RowIndex].FindControl("gvHijosOperacionales"));
                    CargarHijos(ref GridHijos, idPadre);

                    GridHijos = ((Anthem.GridView)Grid.Rows[row.RowIndex].FindControl("gvHijosMantenedores"));
                    CargarHijos(ref GridHijos, idPadre);
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
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString() + "ConfiguracionGeneral"));
                }
            }
        }
        private void CargarHijos(ref Anthem.GridView Grid, int MenuPadre_Id)
        {
            var ObjetoMenu = new MenuInfo();
            var NegMenu = new NegMenus();
            var ObjetoResultado = new Resultado<MenuInfo>();

            //Asignacion de Variables
            ObjetoMenu.Id = -1;
            ObjetoMenu.MenuPadre_Id = MenuPadre_Id;
            ObjetoMenu.Descripcion = "";
            ObjetoMenu.Nivel = 3;

            if (Grid.ID == Constantes.gvHijosMantenedores)
            {
                ObjetoMenu.Mantenedor = true;
                CargarGridAsignacion(ref Grid, ObjetoMenu);
            }
            else if (Grid.ID == Constantes.gvHijosOperacionales)
            {
                ObjetoMenu.Mantenedor = false;
                CargarGridAsignacion(ref Grid, ObjetoMenu);
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
        private void ProcesarAsignacion()
        {
            var ObjetoRolMenu = new RolMenu();
            var NegMenu = new NegMenus();
            var ObjetoResultado = new Resultado<RolMenu>();
            bool AsignacionPadre = false;
            int ContadorActivos = 0;
            try
            {
                ObjetoRolMenu.Rol_Id = int.Parse(ddlRol.SelectedValue);
                foreach (GridViewRow Row in gvMantenedores.Rows)
                {
                    CheckBox chkAcceso = (CheckBox)Row.FindControl(Constantes.chkAcceso);
                    if (chkAcceso.Checked)
                    {
                        ContadorActivos = ContadorActivos + 1;
                    }
                }
                foreach (GridViewRow Row in gvOperacionales.Rows)
                {
                    CheckBox chkAcceso = (CheckBox)Row.FindControl(Constantes.chkAcceso);
                    if (chkAcceso.Checked)
                    {
                        ContadorActivos = ContadorActivos + 1;
                    }
                }

                if (ContadorActivos != 0)
                {
                    if (!AsignacionPadre)
                    {
                        ObjetoRolMenu.Acceso = true;
                        ObjetoRolMenu.Menu_Id = int.Parse(ddlMenu.SelectedValue);
                        ObjetoResultado = new NegMenus().AsignarMenu(ObjetoRolMenu);
                        AsignacionPadre = true;
                    }
                }
                else
                {
                    if (!AsignacionPadre)
                    {
                        ObjetoRolMenu.Acceso = false;
                        ObjetoRolMenu.Menu_Id = int.Parse(ddlMenu.SelectedValue);
                        ObjetoResultado = NegMenu.AsignarMenu(ObjetoRolMenu);
                        AsignacionPadre = true;
                    }
                }

                if (ObjetoResultado.ResultadoGeneral == false) { goto Error; }



                if (gvMantenedores.Rows.Count != 0)
                {
                    ObjetoResultado = NegMenu.AsignacionMenu(gvMantenedores, ObjetoRolMenu, -1);
                    if (ObjetoResultado.ResultadoGeneral == false) { goto Error; }
                    foreach (GridViewRow Row in gvMantenedores.Rows)
                    {
                        GridView gvHijosMantenedores = (GridView)Row.FindControl(Constantes.gvHijosMantenedores);
                        GridView gvHijosOperacionales = (GridView)Row.FindControl(Constantes.gvHijosOperacionales);
                        if (gvHijosMantenedores.Rows.Count != 0)
                        {
                            int MenuPadre_Id = int.Parse(gvMantenedores.DataKeys[Row.RowIndex].Values[Constantes.StringId].ToString());
                            ObjetoResultado = NegMenu.AsignacionMenu(gvHijosMantenedores, ObjetoRolMenu, MenuPadre_Id);
                            if (ObjetoResultado.ResultadoGeneral == false) { goto Error; }
                        }

                        if (gvHijosOperacionales.Rows.Count != 0)
                        {
                            int MenuPadre_Id = int.Parse(gvMantenedores.DataKeys[Row.RowIndex].Values[Constantes.StringId].ToString());
                            ObjetoResultado = NegMenu.AsignacionMenu(gvHijosOperacionales, ObjetoRolMenu, MenuPadre_Id);
                            if (ObjetoResultado.ResultadoGeneral == false) { goto Error; }
                        }
                    }
                }
                if (gvOperacionales.Rows.Count != 0)
                {
                    ObjetoResultado = NegMenu.AsignacionMenu(gvOperacionales, ObjetoRolMenu, -1);
                    if (ObjetoResultado.ResultadoGeneral == false) { goto Error; }
                    foreach (GridViewRow Row in gvOperacionales.Rows)
                    {
                        GridView gvHijosMantenedores = (GridView)Row.FindControl(Constantes.gvHijosMantenedores);
                        GridView gvHijosOperacionales = (GridView)Row.FindControl(Constantes.gvHijosOperacionales);
                        if (gvHijosMantenedores.Rows.Count != 0)
                        {
                            int MenuPadre_Id = int.Parse(gvOperacionales.DataKeys[Row.RowIndex].Values[Constantes.StringId].ToString());
                            ObjetoResultado = NegMenu.AsignacionMenu(gvHijosMantenedores, ObjetoRolMenu, MenuPadre_Id);
                            if (ObjetoResultado.ResultadoGeneral == false) { goto Error; }
                        }

                        if (gvHijosOperacionales.Rows.Count != 0)
                        {
                            int MenuPadre_Id = int.Parse(gvOperacionales.DataKeys[Row.RowIndex].Values[Constantes.StringId].ToString());
                            ObjetoResultado = NegMenu.AsignacionMenu(gvHijosOperacionales, ObjetoRolMenu, MenuPadre_Id);
                            if (ObjetoResultado.ResultadoGeneral == false) { goto Error; }
                        }
                    }
                }
                Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.AsignacionMenuCompleta.ToString()));
                return;
            Error:
                if (ObjetoResultado.ResultadoGeneral == false)
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
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