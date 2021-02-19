using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documental.Entidades;
using Documental.Global;
using Documental.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.WebControls;

namespace Documental.Negocio.Administracion
{
    public class NegMenus
    {
        public NegMenus()
        {

        }

        #region METODOS

        /// <summary>
        /// Método que Inserta o Modifica una Entidad Menu según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad Menu</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<MenuInfo> Guardar(MenuInfo Entidad)
        {

            var ObjetoResultado = new Resultado<MenuInfo>();
            try
            {
                Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.Operacion<MenuInfo>(Entidad, Documental.Data.Global.SP.Menu_INS, Documental.Data.Global.Accion.Guardar);
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " Menu";
                return ObjetoResultado;
            }
        }
        /// <summary>
        /// Método que Inserta o Modifica una Entidad RolMenu según Corresponda.
        /// </summary>
        /// <param name="Entidad">Objeto de la Entidad RolMenu</param>
        /// <returns>Resultado general de la Acción en el Atributo ResultadoGeneral del Objeto Resultado</returns>
        public Resultado<RolMenu> AsignarMenu(RolMenu Entidad)
        {

            var ObjetoResultado = new Resultado<RolMenu>();
            try
            {
                Entidad.UsuarioModificacion_Id = (int)Administracion.NegUsuarios.UsuarioId;
                ObjetoResultado = AccesoDatos.Operacion<RolMenu>(Entidad, Documental.Data.Global.SP.RolMenu_INS, Documental.Data.Global.Accion.Guardar);

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorGuardar.ToString()) + " RolMenu";
                return ObjetoResultado;
            }
        }
        /// <summary>
        /// Método que realiza una Búsqueda en la tabla Menu
        /// </summary>
        /// <param name="Entidad">Objeto MenuInfo con el Filtro de Búsqueda correspondiente</param>
        /// <returns>Lista de la Entidad MenuInfo en el Atributo Lista del Objeto Resultado.</returns>
        public Resultado<MenuInfo> Buscar(MenuInfo Entidad)
        {

            var ObjetoResultado = new Resultado<MenuInfo>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<MenuInfo, MenuInfo>(Entidad, Documental.Data.Global.SP.Menu_QRY);

                ObjetoResultado.ValorDecimal = ObjetoResultado.Lista.Count;

                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " Menu";
                return ObjetoResultado;
            }
        }
        /// <summary>
        /// Método que valida los permisos que tiene un Usuario en relacion a un Menú
        /// </summary>
        /// <param name="Entidad">Objeto Rol Menu con los atributos para realizar la valodación (Usuario_Id y la URL del Menú ) </param>
        /// <returns>Lista RolMenu con el resultado de la valodacion de los permisos</returns>
        public RolMenu ValidarAcceso(RolMenu Entidad)
        {

            var ObjetoResultado = new Resultado<RolMenu>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<RolMenu, RolMenu>(Entidad, Documental.Data.Global.SP.ValidarAccesoMenu_QRY);
                if (ObjetoResultado.Lista == null)
                {
                    ObjetoResultado.ResultadoGeneral = false;
                    ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " RolMenu";
                    return null;
                }
                else if (ObjetoResultado.Lista.Count() == 0)
                {
                    return new RolMenu
                    {
                        Acceso = false,
                        PermisoCrear = false,
                        PermisoModificar = false,
                        PermisoEliminar = false,
                        TituloMenu = null
                    };

                }
                else if (ObjetoResultado.Lista.Count() > 1)
                {
                    var ObjetoRolMenu = new RolMenu();
                    var Acceso = from x in ObjetoResultado.Lista
                                 where x.Acceso == true
                                 select x;

                    var Crear = from x in ObjetoResultado.Lista
                                where x.PermisoCrear == true
                                select x;

                    var Modificar = from x in ObjetoResultado.Lista
                                    where x.PermisoModificar == true
                                    select x;

                    var Eliminar = from x in ObjetoResultado.Lista
                                   where x.PermisoEliminar == true
                                   select x;

                    ObjetoRolMenu.Acceso = (Acceso.Count() != 0);
                    ObjetoRolMenu.PermisoCrear = (Crear.Count() != 0);
                    ObjetoRolMenu.PermisoModificar = (Modificar.Count() != 0);
                    ObjetoRolMenu.PermisoEliminar = (Eliminar.Count() != 0);
                    ObjetoRolMenu.TituloMenu = ObjetoResultado.Lista.FirstOrDefault().TituloMenu;

                    return ObjetoRolMenu;

                }
                else
                {
                    return ObjetoResultado.Lista.FirstOrDefault();
                }

            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " RolMenu";
                return null;
            }
        }
        /// <summary>
        /// Método que lista el menu que tiene asignado un Usuario
        /// </summary>
        /// <param name="Entidad">Objeto RolMenu con el Usuario_Id</param>
        /// <returns>Lista de MenuUsuario, usado para vargar el menu del sistema</returns>
        public Resultado<MenuUsuario> ListarMenuUsuario(Usuario Entidad)
        {

            var ObjetoResultado = new Resultado<MenuUsuario>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<MenuUsuario, Usuario>(Entidad, Documental.Data.Global.SP.MenuUsuario_QRY);
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " MenuUsuario";
                return ObjetoResultado;
            }
        }
        /// <summary>
        /// Método que retorna un objeto RolMenu
        /// </summary>
        /// <param name="Entidad">Objeto RolMenu con los atributos correspondientes</param>
        /// <returns>Objeto RolMenu en el atributo Objeto del resultado</returns>
        public Resultado<RolMenu> RolMenu(RolMenu Entidad)
        {

            var ObjetoResultado = new Resultado<RolMenu>();
            try
            {
                ObjetoResultado = AccesoDatos.Buscar<RolMenu, RolMenu>(Entidad, Documental.Data.Global.SP.RolMenu_QRY);
                ObjetoResultado.Objeto = ObjetoResultado.Lista.FirstOrDefault();
                return ObjetoResultado;
            }
            catch (Exception)
            {
                ObjetoResultado.ResultadoGeneral = false;
                ObjetoResultado.Mensaje = ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorListar.ToString()) + " RolMenu";
                return ObjetoResultado;
            }
        }
        /// <summary>
        /// Metodo que carga el menú principal según los permisos del rol del Usuario
        /// </summary>
        /// <param name="rdMenu">Control RadMenu de la Master Page</param>
        /// <param name="Entidad">Objeto de la Clase Usuario con el ID del usuario validado.</param>
        /// <returns>Retorna el resultado de la carga asdemas del Menú por Referencia.</returns>
        public Resultado<MenuInfo> CargarMenu(ref RadMenu rdMenu, UsuarioInfo Entidad)
        {
            DataSet dsPadres = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr;
            var ObjetoResultado = new Resultado<MenuInfo>();

            if (ListaMenuUsuario != null)
            {
                if (ListaMenuUsuario.Count != 0)
                {
                    ObjetoResultado.Lista = ListaMenuUsuario;
                }
                else
                {
                    ObjetoResultado = AccesoDatos.Buscar<MenuInfo, UsuarioInfo>(Entidad, Documental.Data.Global.SP.MenuUsuario_QRY);
                    ListaMenuUsuario = ObjetoResultado.Lista;
                }
            }
            else
            {
                ObjetoResultado = AccesoDatos.Buscar<MenuInfo, UsuarioInfo>(Entidad, Documental.Data.Global.SP.MenuUsuario_QRY);
                ListaMenuUsuario = ObjetoResultado.Lista;
            }


            dt.Columns.Add("Id");
            dt.Columns.Add("MenuPadre_Id");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("Url");
            dt.Columns.Add("Nivel");
            dt.Columns.Add("Visible");
            dt.Columns.Add("Orden");

            try
            {
                foreach (MenuInfo ObjetoMenu in ObjetoResultado.Lista)
                {
                    dr = dt.NewRow();
                    dr["Id"] = ObjetoMenu.Id;
                    dr["MenuPadre_Id"] = ObjetoMenu.MenuPadre_Id;
                    dr["Descripcion"] = ObjetoMenu.Descripcion;
                    dr["Url"] = ObjetoMenu.Url;
                    dr["Nivel"] = ObjetoMenu.Nivel;
                    dr["Visible"] = ObjetoMenu.Visible;
                    dr["Orden"] = ObjetoMenu.Orden;
                    dt.Rows.Add(dr);
                }

                dsPadres.Tables.Add(dt);

                dsPadres.Relations.Add("NodeRelation", dsPadres.Tables[0].Columns["Id"], dsPadres.Tables[0].Columns["MenuPadre_Id"]);
                ArbolMenuRaiz(dsPadres.Tables[0].Rows, ref rdMenu);



            }
            catch (Exception ex)
            {
                ObjetoResultado.Mensaje = ex.Message;
                ObjetoResultado.ResultadoGeneral = false;
            }
            return ObjetoResultado;
        }



        public Resultado<MenuInfo> CargarMenu(UsuarioInfo Entidad)
        {
            DataSet dsPadres = new DataSet();
            DataTable dt = new DataTable();
            string HtmlMenu = "";
            var ObjetoResultado = new Resultado<MenuInfo>();

            if (ListaMenuUsuario != null)
            {
                if (ListaMenuUsuario.Count != 0)
                {
                    ObjetoResultado.Lista = ListaMenuUsuario;
                }
                else
                {
                    ObjetoResultado = AccesoDatos.Buscar<MenuInfo, UsuarioInfo>(Entidad, Documental.Data.Global.SP.MenuUsuario_QRY);
                    ListaMenuUsuario = ObjetoResultado.Lista;
                }
            }
            else
            {
                ObjetoResultado = AccesoDatos.Buscar<MenuInfo, UsuarioInfo>(Entidad, Documental.Data.Global.SP.MenuUsuario_QRY);
                ListaMenuUsuario = ObjetoResultado.Lista;
            }


            try
            {
                HtmlMenu = HtmlMenu + "<ul>";

                foreach (var menu1 in ListaMenuUsuario.Where(m => m.Nivel == 1 && m.Visible == true).OrderBy(m => m.Orden))
                {
                    var lstMenu2 = ListaMenuUsuario.Where(m => m.MenuPadre_Id == menu1.Id && m.Visible == true).OrderBy(m => m.Orden);

                    if (lstMenu2.Count() != 0)
                    {
                        HtmlMenu = HtmlMenu + "<li class=" + char.ConvertFromUtf32(34) + "has-sub" + char.ConvertFromUtf32(34) + "><a href=" + char.ConvertFromUtf32(34) + "" + menu1.UrlHtml.Replace("~/", "") + "" + char.ConvertFromUtf32(34) + ">" + menu1.Descripcion + "</a>";
                    }
                    else
                    {
                        HtmlMenu = HtmlMenu + "<li><a href=" + char.ConvertFromUtf32(34) + "" + menu1.UrlHtml.Replace("~/", "") + "" + char.ConvertFromUtf32(34) + ">" + menu1.Descripcion + "</a></li>";
                    }

                    if (lstMenu2.Count() != 0)
                    {
                        HtmlMenu = HtmlMenu + "<ul>";
                        foreach (var menu2 in lstMenu2)
                        {
                            var lstMenu3 = ListaMenuUsuario.Where(m => m.MenuPadre_Id == menu2.Id && m.Visible == true).OrderBy(m => m.Orden);
                            if (lstMenu3.Count() != 0)
                            {
                                HtmlMenu = HtmlMenu + "<li class=" + char.ConvertFromUtf32(34) + "has-sub" + char.ConvertFromUtf32(34) + "><a href=" + char.ConvertFromUtf32(34) + "" + menu2.UrlHtml.Replace("~/", "") + "" + char.ConvertFromUtf32(34) + ">" + menu2.Descripcion + "</a>";
                            }
                            else
                            {
                                HtmlMenu = HtmlMenu + "<li><a href=" + char.ConvertFromUtf32(34) + "" + menu2.UrlHtml.Replace("~/", "") + "" + char.ConvertFromUtf32(34) + ">" + menu2.Descripcion + "</a></li>";
                            }

                            if (lstMenu3.Count() != 0)
                            {
                                HtmlMenu = HtmlMenu + "<ul>";
                                foreach (var menu3 in lstMenu3)
                                {
                                    HtmlMenu = HtmlMenu + "<li><a href=" + char.ConvertFromUtf32(34) + "" + menu3.UrlHtml.Replace("~/", "") + "" + char.ConvertFromUtf32(34) + ">" + menu3.Descripcion + "</a></li>";
                                }



                                HtmlMenu = HtmlMenu + "</ul>";
                            }

                            if (lstMenu3.Count() != 0)
                            {
                                HtmlMenu = HtmlMenu + "</li>";
                            }

                        }
                        HtmlMenu = HtmlMenu + "</ul>";
                    }
                    if (lstMenu2.Count() != 0)
                    {
                        HtmlMenu = HtmlMenu + "</li>";
                    }
                    HtmlMenu = HtmlMenu + "</ul>";


                }


                ObjetoResultado.ValorString = HtmlMenu;

            }
            catch (Exception ex)
            {
                ObjetoResultado.Mensaje = ex.Message;
                ObjetoResultado.ResultadoGeneral = false;
            }
            return ObjetoResultado;
        }











        public Resultado<MenuInfo> CargarMenuASP(ref System.Web.UI.WebControls.Menu rdMenu, UsuarioInfo Entidad)
        {
            DataSet dsPadres = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr;
            var ObjetoResultado = new Resultado<MenuInfo>();


            ObjetoResultado = AccesoDatos.Buscar<MenuInfo, UsuarioInfo>(Entidad, Documental.Data.Global.SP.MenuUsuario_QRY);

            dt.Columns.Add("Id");
            dt.Columns.Add("MenuPadre_Id");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("Url");
            dt.Columns.Add("Nivel");
            dt.Columns.Add("Visible");
            dt.Columns.Add("Orden");

            try
            {
                foreach (MenuInfo ObjetoMenu in ObjetoResultado.Lista)
                {
                    dr = dt.NewRow();
                    dr["Id"] = ObjetoMenu.Id;
                    dr["MenuPadre_Id"] = ObjetoMenu.MenuPadre_Id;
                    dr["Descripcion"] = ObjetoMenu.Descripcion;
                    dr["Url"] = ObjetoMenu.Url;
                    dr["Nivel"] = ObjetoMenu.Nivel;
                    dr["Visible"] = ObjetoMenu.Visible;
                    dr["Orden"] = ObjetoMenu.Orden;
                    dt.Rows.Add(dr);
                }

                dsPadres.Tables.Add(dt);

                dsPadres.Relations.Add("NodeRelation", dsPadres.Tables[0].Columns["Id"], dsPadres.Tables[0].Columns["MenuPadre_Id"]);
                ArbolMenuRaizASP(dsPadres.Tables[0].Rows, ref rdMenu);



            }
            catch (Exception ex)
            {
                ObjetoResultado.Mensaje = ex.Message;
                ObjetoResultado.ResultadoGeneral = false;
            }
            return ObjetoResultado;
        }
        /// <summary>
        /// Metodo que carga los Nodos Principales del Menu
        /// </summary>
        /// <param name="filas">Data con la lista de menu relacionada</param>
        /// <param name="menu"> ref del Menu</param>
        private void ArbolMenuRaiz(DataRowCollection filas, ref RadMenu menu)
        {
            bool ConHijos = false;

            foreach (System.Data.DataRow fila in filas)
            {
                RadMenuItem hijo = new RadMenuItem();

                hijo.Text = fila["Descripcion"].ToString();

                if (fila["Url"].ToString() != "..")
                    hijo.NavigateUrl = fila["Url"].ToString();

                if (fila.IsNull("MenuPadre_Id") & fila["Visible"].ToString() == "True")
                {




                    ArbolMenuNodo(fila.GetChildRows("NodeRelation"), ref hijo, ref ConHijos);
                    if (ConHijos)
                        menu.Items.Add(hijo);




                }
            }
        }

        private void ArbolMenuRaizASP(DataRowCollection filas, ref System.Web.UI.WebControls.Menu menu)
        {
            bool ConHijos = false;

            foreach (System.Data.DataRow fila in filas)
            {
                MenuItem hijo = new MenuItem();

                hijo.Text = fila["Descripcion"].ToString();

                if (fila["Url"].ToString() != "..")
                    hijo.NavigateUrl = fila["Url"].ToString();

                if (fila.IsNull("MenuPadre_Id") & fila["Visible"].ToString() == "True")
                {




                    ArbolMenuNodoASP(fila.GetChildRows("NodeRelation"), ref hijo, ref ConHijos);
                    if (ConHijos)
                        menu.Items.Add(hijo);




                }
            }
        }
        /// <summary>
        /// Metodo que carga los hijos.
        /// </summary>
        /// <param name="filas"> Data del Nodo Padre</param>
        /// <param name="nodo">Nodo a llenar</param>
        private void ArbolMenuNodo(DataRow[] filas, ref RadMenuItem nodo, ref bool ConHijos)
        {
            bool Hijos = false;
            foreach (DataRow fila in filas)
            {
                RadMenuItem hijo = new RadMenuItem();


                hijo.Text = fila["Descripcion"].ToString();

                if (fila["Url"].ToString() != "..")
                    hijo.NavigateUrl = fila["url"].ToString();
                var asd = fila["Visible"].ToString();

                if (fila["Visible"].ToString() == "True")
                {
                    ConHijos = true;
                    nodo.Items.Add(hijo);
                    ArbolMenuNodo(fila.GetChildRows("NodeRelation"), ref hijo, ref Hijos);
                }
            }
        }

        private void ArbolMenuNodoASP(DataRow[] filas, ref MenuItem nodo, ref bool ConHijos)
        {
            bool Hijos = false;
            foreach (DataRow fila in filas)
            {
                MenuItem hijo = new MenuItem();


                hijo.Text = fila["Descripcion"].ToString();

                if (fila["Url"].ToString() != "..")
                    hijo.NavigateUrl = fila["url"].ToString();
                var asd = fila["Visible"].ToString();

                if (fila["Visible"].ToString() == "True")
                {
                    ConHijos = true;
                    nodo.ChildItems.Add(hijo);
                    ArbolMenuNodoASP(fila.GetChildRows("NodeRelation"), ref hijo, ref Hijos);
                }
            }
        }
        public Resultado<RolMenu> AsignacionMenu(GridView Grid, RolMenu ObjetoRolMenu, int MenuPadre_Id)
        {

            var ResultadoRolMenu = new Resultado<RolMenu>();
            var NegMenu = new NegMenus();
            bool AsignacionPadre = false;
            int ContadorActivos = 0;
            if (MenuPadre_Id != -1)
            {
                foreach (GridViewRow Row in Grid.Rows)
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
                        ObjetoRolMenu.Menu_Id = MenuPadre_Id;
                        ResultadoRolMenu = NegMenu.AsignarMenu(ObjetoRolMenu);
                        AsignacionPadre = true;
                    }
                }
                else
                {
                    if (!AsignacionPadre)
                    {
                        ObjetoRolMenu.Acceso = false;
                        ObjetoRolMenu.Menu_Id = MenuPadre_Id;
                        ResultadoRolMenu = NegMenu.AsignarMenu(ObjetoRolMenu);
                        AsignacionPadre = true;
                    }
                }

            }

            foreach (GridViewRow Row in Grid.Rows)
            {
                CheckBox chkAcceso = (CheckBox)Row.FindControl(Constantes.chkAcceso);

                ObjetoRolMenu.Acceso = chkAcceso.Checked;


                if (Grid.ID == Constantes.gvHijosMantenedores || Grid.ID == Constantes.gvMantenedores)
                {
                    CheckBox chkCrear = (CheckBox)Row.FindControl(Constantes.chkCrear);
                    CheckBox chkModificar = (CheckBox)Row.FindControl(Constantes.chkModificar);
                    CheckBox chkEliminar = (CheckBox)Row.FindControl(Constantes.chkEliminar);

                    ObjetoRolMenu.PermisoCrear = chkCrear.Checked;
                    ObjetoRolMenu.PermisoModificar = chkModificar.Checked;
                    ObjetoRolMenu.PermisoEliminar = chkEliminar.Checked;
                }
                ObjetoRolMenu.Menu_Id = int.Parse(Grid.DataKeys[Row.RowIndex].Values[Constantes.StringId].ToString());
                ResultadoRolMenu = NegMenu.AsignarMenu(ObjetoRolMenu);

            }


            return ResultadoRolMenu;



        }

        #endregion

        #region PROPIEDADES
        public static RolMenu Permisos
        {
            get { return (RolMenu)HttpContext.Current.Session[ISesiones.Permisos]; }
            set { HttpContext.Current.Session.Add(ISesiones.Permisos, value); }
        }


        public static List<MenuInfo> ListaMenuUsuario
        {
            get { return (List<MenuInfo>)HttpContext.Current.Session[ISesiones.MenuUsuario]; }
            set { HttpContext.Current.Session.Add(ISesiones.MenuUsuario, value); }
        }
        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string Permisos = "PermisosDeAcceso";
            public static string MenuUsuario = "LstMenuUsuario";
        }
        #endregion

    }
}
