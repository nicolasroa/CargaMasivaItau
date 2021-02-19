using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Documental.Entidades;
using Documental.Global;
using Documental.Negocio.Administracion;

namespace WebSite.Master
{
    public partial class DocumentosPDF : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarDatos();
            }
        }


        private void CargarDatos()
        {
            var ObjetoUsuario = new UsuarioInfo();
            ObjetoUsuario = (UsuarioInfo)NegUsuarios.Usuario;

            lblResponsable.Text = ObjetoUsuario.NombreCompleto;
            lblFechaCreacion.Text = string.Format("{0:g}", DateTime.Now);
            lblNombreDocumento.Text =  Pdf.NombreDocumentoPDF;
            lblModulo.Text = Pdf.ModuloDocumentoPDF;
         
                
        }

    }
}