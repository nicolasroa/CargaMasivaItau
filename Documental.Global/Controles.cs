using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;

namespace Documental.Global
{
    public class Controles
    {
        public static void EjecutarJavaScript(string Script)
        {
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), Script, true);

        }
        public static void AbrirPopup(string Url, int Ancho, int Alto, string Titulo)
        {
            Url = ObtenerUrlAbsoluta(Url);
            string _baseAbrirPopup = "ModalURL('@url',@Ancho, @Alto, '@Titulo');";
            _baseAbrirPopup = _baseAbrirPopup.Replace("@url", Url).Replace("@Ancho", Ancho.ToString()).Replace("@Alto", Alto.ToString()).Replace("@Titulo", Titulo);

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _baseAbrirPopup, true);
        }
        public static void AbrirPopupExterno(string Url, int Ancho, int Alto, string Titulo)
        {

            string _baseAbrirPopup = "ModalURL('@url',@Ancho, @Alto, '@Titulo');";
            _baseAbrirPopup = _baseAbrirPopup.Replace("@url", Url).Replace("@Ancho", Ancho.ToString()).Replace("@Alto", Alto.ToString()).Replace("@Titulo", Titulo);

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _baseAbrirPopup, true);
        }
        public static void AbrirModal(string IdDiv, string Titulo)
        {
            string _baseAbrirPopup = "ModalDiv('@IdDiv','@Titulo');";
            _baseAbrirPopup = _baseAbrirPopup.Replace("@IdDiv", IdDiv).Replace("@Titulo", Titulo);

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _baseAbrirPopup, true);
        }
        public static void CerrarModal(string Id)
        {
            string _baseCerrarPopup = "CerrarModal('@Id');";
            _baseCerrarPopup = _baseCerrarPopup.Replace("@Id", Id);
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _baseCerrarPopup, true);
        }
        public static string ObtenerUrlAbsoluta(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return relativeUrl;

            if (HttpContext.Current == null)
                return relativeUrl;

            if (relativeUrl.StartsWith("/"))
                relativeUrl = relativeUrl.Insert(0, "~");
            if (!relativeUrl.StartsWith("~/"))
                relativeUrl = relativeUrl.Insert(0, "~/");

            var url = HttpContext.Current.Request.Url;
            var port = url.Port != 80 ? (":" + url.Port) : String.Empty;

            return String.Format("{0}://{1}{2}{3}", url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));
        }

        public static void CerrarPopup()
        {
            bool Evento = true;
            string _baseCerrarPopup = "CerrarVentana();";
            _baseCerrarPopup = _baseCerrarPopup.Replace("@Evento", Evento.ToString().ToLower());
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _baseCerrarPopup, true);

        }
        public static void CerrarConCargaMenu()
        {
            bool Evento = true;
            string _baseCerrarPopup = "CerrarVentanaMenu();";
            _baseCerrarPopup = _baseCerrarPopup.Replace("@Evento", Evento.ToString().ToLower());
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _baseCerrarPopup, true);

        }
        public static void MostrarMensajeError(string Texto)
        {
            string _baseAbrirPopup = "MostrarMensajeError('@Texto');";
            _baseAbrirPopup = _baseAbrirPopup.Replace("@Texto", Texto.Replace("'", ""));

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _baseAbrirPopup, true);
        }
        public static void MostrarMensajeInfo(string Texto)
        {
            string _baseAbrirPopup = "MostrarMensajeInfo('@Texto');";
            _baseAbrirPopup = _baseAbrirPopup.Replace("@Texto", Texto.Replace("'", ""));

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _baseAbrirPopup, true);
        }
        public static void MostrarMensajeExito(string Texto)
        {
            string _baseAbrirPopup = "MostrarMensajeExito('@Texto');";
            _baseAbrirPopup = _baseAbrirPopup.Replace("@Texto", Texto.Replace("'", ""));

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _baseAbrirPopup, true);
        }
        public static void MostrarMensajeAlerta(string Texto)
        {
            string _baseAbrirPopup = "MostrarMensajeAlerta('@Texto');";
            _baseAbrirPopup = _baseAbrirPopup.Replace("@Texto", Texto.Replace("'", ""));

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _baseAbrirPopup, true);
        }
        public static void MensajeEnControl(string IdControl, string Texto)
        {
            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MensajeEnControl('" + IdControl + "','" + Texto + "');", true);
        }


        //CONTROLES
        public static void CargarCombo<T>(ref DropDownList Combo, List<T> Lista, string Valor, string Texto, string TextoInicial, string valorInicial)
        {

            Combo.DataSource = Lista;
            Combo.DataTextField = Texto;
            Combo.DataValueField = Valor;

            Combo.DataBind();
            if (TextoInicial != "")
            {
                if (valorInicial == "")
                    valorInicial = Guid.Empty.ToString();
                Combo.Items.Insert(0, new ListItem(TextoInicial, valorInicial));
                Combo.SelectedIndex = 0;
            }
        }
        public static void CargarCombo<T>(ref Anthem.DropDownList Combo, List<T> Lista, string Valor, string Texto, string TextoInicial, string valorInicial)
        {

            Combo.DataSource = Lista;
            Combo.DataTextField = Texto;
            Combo.DataValueField = Valor;

            Combo.DataBind();
            if (TextoInicial != "")
            {
                if (valorInicial == "")
                    valorInicial = Guid.Empty.ToString();
                Combo.Items.Insert(0, new ListItem(TextoInicial, valorInicial));
                Combo.SelectedIndex = 0;
            }
        }
        public static void CargarListBox<T>(ref ListBox List, List<T> Lista, string Valor, string Texto)
        {

            List.DataSource = Lista;
            List.DataTextField = Texto;
            List.DataValueField = Valor;
            List.DataBind();

        }
        public static void CargarListBox<T>(ref Anthem.ListBox List, List<T> Lista, string Valor, string Texto)
        {

            List.DataSource = Lista;
            List.DataTextField = Texto;
            List.DataValueField = Valor;
            List.DataBind();
        }
        public static void CargarChekBoxList<T>(ref CheckBoxList ChekBox, List<T> Lista, string Valor, string Texto)
        {

            ChekBox.DataSource = Lista;
            ChekBox.DataTextField = Texto;
            ChekBox.DataValueField = Valor;
            ChekBox.DataBind();

        }
        public static void CargarChekBoxList<T>(ref Anthem.CheckBoxList ChekBox, List<T> Lista, string Valor, string Texto)
        {

            ChekBox.DataSource = Lista;
            ChekBox.DataTextField = Texto;
            ChekBox.DataValueField = Valor;
            ChekBox.DataBind();

        }
        public static void CargarGrid<T>(ref GridView Grid, List<T> Lista, string[] DataKey)
        {

            if (DataKey.Length != 0)
                Grid.DataKeyNames = DataKey;
            if (Lista.Count() != 0)
            {
                Grid.DataSource = Lista;
                Grid.DataBind();
                Grid.UseAccessibleHeader = true;
                Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                Grid.EmptyDataRowStyle.CssClass = "GridHeader";
                Grid.EmptyDataText = "No hay Registros para mostrar";
                Grid.DataBind();
            }


        }

        public static void CargarGrid<T>(ref Anthem.GridView Grid, List<T> Lista, string[] DataKey)
        {

            if (DataKey.Length != 0)
                Grid.DataKeyNames = DataKey;
            if (Lista.Count() != 0)
            {
                Grid.DataSource = Lista;
                Grid.DataBind();
                Grid.UseAccessibleHeader = true;
                Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                Grid.DataSource = null;
                Grid.EmptyDataRowStyle.CssClass = "GridHeader";
                Grid.EmptyDataText = "No hay Registros para mostrar";
                Grid.DataBind();
            }


        }

    }
}
