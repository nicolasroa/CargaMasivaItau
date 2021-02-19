using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Web.UI;
namespace Documental.Global
{
    public class Excel
    {



        #region METODOS

        //EJEMPLO DE LLAMADO
        // Excel.ExportarGrid<AuditoriaInfo>(gvBusqueda, NegAuditoria.ListaAuditoriaPDF, "Auditoria","Reporte de Auditoria", new int[] { 1, 3 });

        //Agregar Método
        //public override void VerifyRenderingInServerForm(Control control){ }

        //Agregar en el Encabezado del Código HTML
        // EnableEventValidation="false"


        public static string ExportarGrid<T>(Anthem.GridView Grid, List<T> Lista, string NombreArchivo, string TituloReporte, int[] ColumnasAEliminar = null)
        {
            try
            {
                string Codigo = "";

                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                if (ColumnasAEliminar != null)
                {
                    foreach (var Columna in ColumnasAEliminar)
                    {
                        if (Columna <= Grid.Columns.Count && Columna >= 1)
                            Grid.Columns[Columna - 1].Visible = false;
                    }
                }

                Grid.DataBind();
                Grid.AllowPaging = false;
                Grid.DataSource = Lista;
                Grid.DataBind();
                Grid.EnableViewState = false;
                Grid.RenderControl(htw);

                Codigo = Codigo + "<p>" + TituloReporte + "</p> <br />";

                Codigo = Codigo + sb.ToString();

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + NombreArchivo + ".xls");
                HttpContext.Current.Response.Charset = "UTF-8";
                HttpContext.Current.Response.ContentEncoding = Encoding.Default;
                HttpContext.Current.Response.Write(Codigo);
                HttpContext.Current.Response.End();

                Grid.AllowPaging = true;
                if (ColumnasAEliminar != null)
                {
                    foreach (var Columna in ColumnasAEliminar)
                    {
                        if (Columna <= Grid.Columns.Count && Columna >= 1)
                            Grid.Columns[Columna - 1].Visible = true;
                    }
                }
                Grid.DataSource = Lista;
                Grid.DataBind();
                return "";

            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }

        #endregion

        #region PROPIEDADES
        public static string NombreDocumentoXls
        {
            get { return (string)HttpContext.Current.Session[ISesiones.NombreDocumentoXLS]; }
            set { HttpContext.Current.Session.Add(ISesiones.RutaDocumentoXLS, value); }
        }
        public static string RutaDocumentoXls
        {
            get { return (string)HttpContext.Current.Session[ISesiones.RutaDocumentoXLS]; }
            set { HttpContext.Current.Session.Add(ISesiones.RutaDocumentoXLS, value); }
        }


        #endregion

        #region SESIONES
        private class ISesiones
        {

            public static string RutaDocumentoXLS = "ModuloDocumentoXLS";
            public static string NombreDocumentoXLS = "ModuloDocumentoXLS";
        }
        #endregion
    }
}
