using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Winnovative.WnvHtmlConvert;
using Winnovative.WnvHtmlConvert.PdfDocument;

namespace Documental.Global
{
    public class Pdf
    {

        #region METODOS
        public static string GenerarDocumentoPDFIndividual(string urls, string NombreDocumento, PdfPageSize Tamaño)
        {

            try
            {
                Document docPDF = GenerarHtmlaDocumento(urls, Tamaño);
                byte[] pdfBytes;
                try
                {
                    pdfBytes = docPDF.Save();
                }
                finally
                {
                    docPDF.Close();
                }

                System.IO.File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Repositorio/Pdf/" + NombreDocumento), pdfBytes);
                return "";

            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }
        public static Document GenerarHtmlaDocumento(string Url, PdfPageSize Tamaño)
        {
            PdfConverter pdfConvertidor = new PdfConverter();
            PDFPageOrientation pdfPageOrientation;
            pdfConvertidor.LicenseKey = "UXpjcWBkcWlmcWd/YXFiYH9gY39oaGho";
            pdfPageOrientation = PDFPageOrientation.Portrait;
            pdfConvertidor.PdfDocumentOptions.PdfPageSize = Tamaño;
            pdfConvertidor.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
            pdfConvertidor.PdfDocumentOptions.PdfPageOrientation = pdfPageOrientation;
            string thisPageURL = HttpContext.Current.Request.Url.AbsoluteUri;
            string baseUrl = thisPageURL;

            return pdfConvertidor.GetPdfDocumentObjectFromHtmlString(Url, baseUrl);



        }


        public static void MostrarPdfModal(string NombreArchivo)
        {
            var Ancho = 1100;
            var Alto = 800;
            var Titulo = "Documento Pdf";
            string _baseAbrirPopup = "MostrarPdfModal('@NombreArchivo',@Ancho, @Alto, '@Titulo');";
            _baseAbrirPopup = _baseAbrirPopup.Replace("@NombreArchivo", NombreArchivo).Replace("@Ancho", Ancho.ToString()).Replace("@Alto", Alto.ToString()).Replace("@Titulo", Titulo);

            Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _baseAbrirPopup, true);
        }
        public static void MostrarPdfModal(string NombreArchivo, string Titulo)
        {

            System.Web.HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            string name = browser.Browser;

            if (name == "IE" || name == "Mozilla")
            {
                var RutaPdf = HttpContext.Current.Server.MapPath(Constantes.RutaPdf + NombreArchivo);
                System.IO.FileInfo toDownload = new System.IO.FileInfo(RutaPdf);
                if (toDownload.Exists)
                {
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo);
                    HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.WriteFile(RutaPdf);
                    HttpContext.Current.Response.Flush();
                }
            }
            else
            {
                var Ancho = 1100;
                var Alto = 800;
                string _baseAbrirPopup = "MostrarPdfModal('@NombreArchivo',@Ancho, @Alto, '@Titulo');";
                _baseAbrirPopup = _baseAbrirPopup.Replace("@NombreArchivo", NombreArchivo).Replace("@Ancho", Ancho.ToString()).Replace("@Alto", Alto.ToString()).Replace("@Titulo", Titulo);

                Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _baseAbrirPopup, true);
            }

        }
        public static void MostrarPdfModal(string NombreArchivo, string Titulo, int Alto, int Ancho)
        {
            System.Web.HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            string name = browser.Browser;

            if (name == "IE")
            {
                var RutaPdf = HttpContext.Current.Server.MapPath(Constantes.RutaPdf + NombreArchivo);
                System.IO.FileInfo toDownload = new System.IO.FileInfo(RutaPdf);
                if (toDownload.Exists)
                {
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo);
                    HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.WriteFile(RutaPdf);
                    HttpContext.Current.Response.Flush();
                }
            }
            else
            {
                string _baseAbrirPopup = "MostrarPdfModal('@NombreArchivo',@Ancho, @Alto, '@Titulo');";
                _baseAbrirPopup = _baseAbrirPopup.Replace("@NombreArchivo", NombreArchivo).Replace("@Ancho", Ancho.ToString()).Replace("@Alto", Alto.ToString()).Replace("@Titulo", Titulo);
                Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _baseAbrirPopup, true);
            }
        }
        public static void MostrarPdfDiv(string NombreArchivo, string NobreDiv)
        {
            System.Web.HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            string name = browser.Browser;

            if (name == "IE")
            {
                var RutaPdf = HttpContext.Current.Server.MapPath(Constantes.RutaPdf + NombreArchivo);
                System.IO.FileInfo toDownload = new System.IO.FileInfo(RutaPdf);
                if (toDownload.Exists)
                {
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo);
                    HttpContext.Current.Response.AddHeader("Content-Length", toDownload.Length.ToString());
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.WriteFile(RutaPdf);
                    HttpContext.Current.Response.Flush();
                }
            }
            else
            {
                string _baseAbrirPopup = "MostrarPdfDiv('@NombreArchivo','@NombreDiv');";
                _baseAbrirPopup = _baseAbrirPopup.Replace("@NombreArchivo", NombreArchivo).Replace("@NombreDiv", NobreDiv);

                Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), _baseAbrirPopup, true);
            }
        }




        #endregion

        #region PROPIEDADES
        public static string NombreDocumentoPDF
        {
            get { return (string)HttpContext.Current.Session[ISesiones.NombreDocumentoPDF]; }
            set { HttpContext.Current.Session.Add(ISesiones.NombreDocumentoPDF, value); }
        }
        public static string ModuloDocumentoPDF
        {
            get { return (string)HttpContext.Current.Session[ISesiones.ModuloDocumentoPDF]; }
            set { HttpContext.Current.Session.Add(ISesiones.ModuloDocumentoPDF, value); }
        }

        #endregion

        #region SESIONES
        private class ISesiones
        {
            public static string NombreDocumentoPDF = "NombreDocumentoPDF";
            public static string ModuloDocumentoPDF = "ModuloDocumentoPDF";
        }
        #endregion

    }
}
