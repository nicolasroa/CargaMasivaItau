using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Web;

namespace Documental.Global
{
    public static class ArchivoRecursos
    {
        public static string ObtenerValorNodo(string sNombreParametro)
        {
            string strValor = "";
            XDocument xmlDoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/RecursosXML/ArchivoRecursos.xml"));
            var varValores = from valores in xmlDoc.Descendants("Parametro")
                             where (string)valores.Attribute("Nombre") == sNombreParametro
                             select new
                             {
                                 valor = (string)valores.Attribute("Valor")
                             };
            foreach (var item in varValores)
            {
                strValor = item.valor;
            }
            return strValor;
        }
    }
}
