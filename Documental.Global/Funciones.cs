using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using System.Web;

namespace Documental.Global
{
    public static class Funciones
    {
        /// <summary>
        /// Devuelve un valor Long que especifica el número de
        /// intervalos de tiempo entre dos valores Date.
        /// </summary>
        /// <param name="interval">Obligatorio. Valor de enumeración
        /// DateInterval o expresión String que representa el intervalo
        /// de tiempo que se desea utilizar como unidad de diferencia
        /// entre Date1 y Date2.</param>
        /// <param name="date1">Obligatorio. Date. Primer valor de
        /// fecha u hora que se desea utilizar en el cálculo.</param>
        /// <param name="date2">Obligatorio. Date. Segundo valor de
        /// fecha u hora que se desea utilizar en el cálculo.</param>
        /// <returns></returns>
        public static long DateDiff(DateInterval interval, DateTime date1, DateTime date2)
        {
            long rs = 0;
            TimeSpan diff = date2.Subtract(date1);
            switch (interval)
            {
                case DateInterval.Day:
                case DateInterval.DayOfYear:
                    rs = (long)diff.TotalDays;
                    break;
                case DateInterval.Hour:
                    rs = (long)diff.TotalHours;
                    break;
                case DateInterval.Minute:
                    rs = (long)diff.TotalMinutes;
                    break;
                case DateInterval.Month:
                    rs = (date2.Month - date1.Month) + (12 * DateDiff(DateInterval.Year, date1, date2));
                    break;
                case DateInterval.Quarter:
                    rs = (long)Math.Ceiling((double)(DateDiff(DateInterval.Month, date1, date2) / 3.0));
                    break;
                case DateInterval.Second:
                    rs = (long)diff.TotalSeconds;
                    break;
                case DateInterval.Weekday:
                case DateInterval.WeekOfYear:
                    rs = (long)(diff.TotalDays / 7);
                    break;
                case DateInterval.Year:
                    rs = date2.Year - date1.Year;
                    break;
            }//switch
            return rs;
        }//DateDiff
        public static string generarFolioPadreReal(string folio)
        {
            int x = 0;
            string FP = null;
            string _fp = null;
            bool guarda = false;
            guarda = false;

            FP = "";
            _fp = "";
            for (x = folio.Length - 1; x >= 0; x += -1)
            {
                if (folio[x].ToString() == "-" || string.IsNullOrEmpty(folio[x].ToString()))
                {
                    guarda = true;
                }
                if (guarda == true)
                {
                    FP = FP + folio[x];
                }
            }

            if (string.IsNullOrEmpty(FP))
            {
                return folio;
            }

            for (x = FP.Length - 1; x >= 1; x += -1)
            {
                _fp = _fp + FP[x];
            }

            return (_fp);
        }
        public static int GenerarOrdenFolio(string folio)
        {
            int x = 0;
            int foliopadre = 0;
            string FP = null;
            FP = "";


            for (x = 0; x <= folio.Length - 1; x++)
            {


                if (folio[x].ToString() == " " || folio[x].ToString() == "-")
                {
                    break; // TODO: might not be correct. Was : Exit For
                }
                else
                {
                    FP = FP + folio[x];
                }
            }

            foliopadre = Convert.ToInt32(FP);

            return (foliopadre);

        }
        public static string RutInvalido()
        {
            int modulo = 0;
            int index = 0;
            int x = 0;
            int y = 0;
            int multiplo = 2;
            long NumeroAleatorio = 0;
            string digito = null;
            string rut = null;
            string rutValido = null;

            NumeroAleatorio = Aleatorio();

            rut = strReverse(Convert.ToString(NumeroAleatorio));
            rutValido = Convert.ToString(NumeroAleatorio);

            for (index = 0; index <= rut.Length - 1; index++)
            {
                if (multiplo == 7)
                {
                    modulo = modulo + Convert.ToInt32(rut[index].ToString()) * multiplo;
                    multiplo = 2;
                }
                else
                {
                    modulo = modulo + Convert.ToInt32(rut[index].ToString()) * multiplo;
                    multiplo = multiplo + 1;

                }
                y = Convert.ToInt32(rut[index].ToString());
                x = Convert.ToInt32(rut[index].ToString()) * multiplo;



            }
            modulo = modulo % 11;
            digito = Convert.ToString(11 - modulo);
            if (digito == "11")
            {
                digito = "0";
            }
            else if (digito == "10")
            {
                digito = "K";
            }

            return (rutValido + "-" + digito);







        }
        private static long Aleatorio()
        {
            Random R = new Random();
            return R.Next(999, 99999);
        }
        private static string strReverse(string Expression)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= Expression.Length; i++)
            {
                result.Append(Expression.Substring(Expression.Length - i, 1));
            }
            return result.ToString();
        }
        public static string NombreArchivo(TipoArchivo Tipo)
        {
            var Archivo = "";
            switch (Tipo)
            {
                case TipoArchivo.Auditoria:
                    Archivo = "Auditoria_" + DateTime.Now.ToString().Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "");
                    break;
                case TipoArchivo.Reporte:
                    Archivo = "Reporte" + DateTime.Now.ToString().Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "");
                    break;
                default:
                    Archivo = "Archivo" + DateTime.Now.ToString().Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "");
                    break;
            }
            return Archivo;
        }
        public enum TipoArchivo
        {
            Auditoria,
            Reporte
        }




        public static XmlDocument ToXML<T>(List<T> Lista)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    T obj = Lista.FirstOrDefault();
                    XElement XMLInicial = new XElement(obj.GetType().Name + "s");
                    foreach (T Objeto in Lista)
                    {
                        XElement ElementoXml = new XElement(obj.GetType().Name);
                        foreach (PropertyInfo proinfo in Objeto.GetType().GetProperties())
                        {
                            var Tipo = proinfo.PropertyType.Name;
                            switch (Tipo)
                            {
                                case "DateTime":
                                    ElementoXml.Add(new XElement(proinfo.Name, string.Format("{0:d}", proinfo.GetValue(Objeto, null))));

                                    break;
                                case "Decimal":
                                    ElementoXml.Add(new XElement(proinfo.Name, string.Format("{0:N2}", proinfo.GetValue(Objeto, null))));
                                    break;
                                default:
                                    ElementoXml.Add(new XElement(proinfo.Name, proinfo.GetValue(Objeto, null)));
                                    break;
                            }
                        }
                        XMLInicial.Add(ElementoXml);
                    }

                    var xmlDocument = new XmlDocument();
                    using (var xmlReader = XMLInicial.CreateReader())
                    {
                        xmlDocument.Load(xmlReader);
                    }
                    xmlDocument.Save(HttpContext.Current.Server.MapPath("~/RecursosXML/" + Guid.NewGuid().ToString() + ".xml"));
                    return xmlDocument;
                }
            }
            catch
            {
                return null;
            }

        }



    }

    /// <summary>
    /// Enumerados que definen los tipos de
    /// intervalos de tiempo posibles.
    /// </summary>
    public enum DateInterval
    {
        Day,
        DayOfYear,
        Hour,
        Minute,
        Month,
        Quarter,
        Second,
        Weekday,
        WeekOfYear,
        Year
    }
}
