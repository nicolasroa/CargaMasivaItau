using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.Master
{
    public partial class Operaciones : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
        }
        public Anthem.Button CerrarPopup
        {
            get { return btnCerrarPopup; }
        }
        private void IsPostBackJavaScript()
        {
            hdnIsPostBack.Value = IsPostBack ? "Y" : "N";
        }
    }
}