using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graficos;

namespace Documental.Global
{
    public class Graficos
    {
        public static string RutaGraficos = "../Recursos/Graficos/";


        /*
         * 
         * EJEMPLO DE IMPLEMENTACION
        SE LE ASIGNA EL RESULTADO DEL METODO A UN DIV
        Grafico.InnerHtml = GetMonthlySalesChartHtml();
        public string GetMonthlySalesChartHtml()
        {

            List<Indicadores> Lista = new List<Indicadores>();


            Lista.Add(new Indicadores { NombreIndicador = "Ind 1", ValorIndicador= 123 });
            Lista.Add(new Indicadores { NombreIndicador = "Ind 2", ValorIndicador = 57.3M });
            Lista.Add(new Indicadores { NombreIndicador = "Ind 3", ValorIndicador = 879.543M });
            Lista.Add(new Indicadores { NombreIndicador = "Ind 4", ValorIndicador = 132 });
            Lista.Add(new Indicadores { NombreIndicador = "Ind 5", ValorIndicador = 145 });
            Lista.Add(new Indicadores { NombreIndicador = "Ind 6", ValorIndicador = 56 });
            Lista.Add(new Indicadores { NombreIndicador = "Ind 7", ValorIndicador = 535 });
            return Documental.Global.Graficos.GenerarGraficoBarra3D(Lista, "Grádifo de Prueba", "EjeX", "EjeY");
        }


        */
        public static string GenerarGraficoBarra3D(List<Indicadores> Datos, string Titulo, string EjeX, string EjeY, int Ancho = 600, int Alto = 300)
        {
            try
            {
                StringBuilder xmlData = new StringBuilder();
                xmlData.Append("<chart caption='" + Titulo + "' xAxisName='" + EjeX + "' yAxisName='" + EjeY + "' showValues='1' formatNumberScale='0' showBorder='1' decimals ='3' showLegend='1'>");

                foreach (var Dato in Datos)
                {
                    xmlData.Append("<set label='" + Dato.NombreIndicador + "' value='" + Dato.ValorIndicador.ToString().Replace(",", ".") + "' />");
                }
                xmlData.Append("</chart>");
                return FusionCharts.RenderChart(RutaGraficos + "Column3D.swf", "", xmlData.ToString(), "myNext", Ancho.ToString(), Alto.ToString(), false, false);
            }
            catch (Exception ex)
            {
                return "ERROR#" + ex.Message;
                throw;
            }
        }

        public static string GenerarGraficoBarraDoble3D(List<Indicadores> Datos, string Titulo, string EjeX, string EjeY, string Categoria1, string Categoria2, int Ancho = 600, int Alto = 300)
        {
            try
            {
                StringBuilder xmlData = new StringBuilder();
                //xmlData.Append("<chart caption='" + Titulo + "' xAxisName='" + EjeX + "' yAxisName='" + EjeY + "' showValues='1' formatNumberScale='0' showBorder='1' decimals ='3' showLegend='1'>");
                xmlData.Append("<chart palette='3' caption='" + Titulo + "' yaxisname='" + EjeY + "' showvalues='1' numvdivlines='10' divlinealpha='30' drawanchors='0' labelpadding='10' yaxisvaluespadding='10' useroundedges='1' legendborderalpha='0' showborder='0'>");
                //SE CARGAN LAS CATEGORIAS
                xmlData.Append("<categories>");

                foreach (var Dato in Datos)
                {
                    xmlData.Append("<category label='" + Dato.NombreIndicador + "' />");

                }
                xmlData.Append("</categories>");
                //SE LLENA LA CATEGORIA1
                xmlData.Append("<dataset seriesname='" + Categoria1 + "'>");
                foreach (var Dato in Datos)
                {
                    xmlData.Append("<set value='" + Dato.ValorIndicador1.ToString().Replace(",", ".") + "' />");
                }
                xmlData.Append("</dataset>");


                //SE LLENA LA CATEGORIA2
                xmlData.Append("<dataset seriesname='" + Categoria2 + "'>");
                foreach (var Dato in Datos)
                {
                    xmlData.Append("<set value='" + Dato.ValorIndicador2.ToString().Replace(",", ".") + "' />");
                }
                xmlData.Append("</dataset>");
                //_________________________________________________-
                xmlData.Append("</chart>");
                var Resultado = FusionCharts.RenderChartHTML(RutaGraficos + "MSColumn3D.swf", "", xmlData.ToString(), "myNext", Ancho.ToString(), Alto.ToString(), false, false);

                return Resultado;
            }
            catch (Exception ex)
            {
                return "ERROR#" + ex.Message;
                throw;
            }
        }
    }

    public class Indicadores
    {
        public string NombreIndicador { get; set; }
        public decimal ValorIndicador { get; set; }
        public decimal ValorIndicador1 { get; set; }
        public decimal ValorIndicador2 { get; set; }
        public decimal ValorIndicador3 { get; set; }
        public decimal ValorIndicador4 { get; set; }
        public decimal ValorIndicador5 { get; set; }
        public decimal ValorIndicador6 { get; set; }
        public decimal ValorIndicador7 { get; set; }
        public decimal ValorIndicador8 { get; set; }

    }
}
