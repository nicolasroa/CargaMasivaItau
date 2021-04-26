using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Documental.Global;
using System.Data.OleDb;
using System.Data;
using Documental.Entidades.CargaMasiva;
using Documental.Negocio.CargaMasiva;
using Documental.Entidades;

namespace Documental.WebApp
{
    public partial class CargaNominaPostergados : System.Web.UI.Page
    {

        //Generacion de Excel
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control) { }

        protected void Page_Load(object sender, EventArgs e)
        {
            Anthem.Manager.Register(Page);
            if (!Page.IsPostBack)
            {
                //if (Convert.ToString(Request.QueryString["rol"]) == null)
                //    Response.Redirect("SinAcceso.aspx?Error=Se debe Especificar un Rol desde el WF para acceder");
                //if (Convert.ToString(Request.QueryString["usu"]) == null)
                //    Response.Redirect("SinAcceso.aspx?Error=Se debe Especificar un Usuario desde el WF para acceder");



                //CargarLog();
            }
        }
        //private void CargarLog()
        //{
        //    NegCargaMasiva oNegCarga = new NegCargaMasiva();
        //    Resultado<LogCargaMasiva> resultCarga = new Resultado<LogCargaMasiva>();
        //    LogCargaMasiva objSolicitud = new LogCargaMasiva();

        //    resultCarga = oNegCarga.BuscarLog(objSolicitud);
        //    if (resultCarga.ResultadoGeneral)
        //    {
        //        Controles.CargarGrid<LogCargaMasiva>(ref gvLogCarga, resultCarga.Lista, new string[] { });
        //    }
        //    else
        //    {
        //        Controles.MostrarMensajeError(resultCarga.Mensaje);
        //    }



        //}
        private void CargarArchivo()
        {

            Boolean fileOK = false;
            String path = Server.MapPath("~/Repositorio/CargasMasivas/");
            String fileExtension = "";
            if (fupCargaInterface.HasFile)
            {
                fileExtension = System.IO.Path.GetExtension(fupCargaInterface.FileName).ToLower();
                String[] allowedExtensions = { ".xls", ".xlsx" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }
            else
            {
                Controles.MostrarMensajeAlerta("Archivo No Válido");
                return;
            }

            if (fileOK)
            {
                try
                {
                    var Archivo = "CargaPostergado" + DateTime.Now.ToString().Replace(" ", "").Replace("-", "").Replace(":", "") + fileExtension;

                    fupCargaInterface.PostedFile.SaveAs(path + Archivo);
                    LeerArchivo(path + Archivo, fileExtension);

                }
                catch (Exception ex)
                {
                    Controles.MostrarMensajeError(ex.Message);
                }
            }
            else
            {
                Controles.MostrarMensajeError("Favor validar extencion del Archivo");
            }



        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private void LeerArchivo(string Archivo, string extencion, string Tipo = "Existe")
        {

            OleDbConnection conexion = null;
            DataSet dataSet = null;
            OleDbDataAdapter dataAdapter = null;
            NegCargaMasivaPostergados oNegCarga = new NegCargaMasivaPostergados();
            Resultado<InfoPostergado> resultCarga = new Resultado<InfoPostergado>();
            string consultaHojaExcel = "Select * from [CreditosPostergados$]";

            var Ruta = Archivo;
            string cadenaConexionArchivoExcel = "";
            if (extencion == ".xlsx")
                cadenaConexionArchivoExcel = "provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + Ruta + "';Extended Properties=Excel 12.0;";
            else
                cadenaConexionArchivoExcel = "provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + Ruta + "';Extended Properties=Excel 8.0;";

            //Validamos que el usuario ingrese el nombre de la hoja del archivo de excel a leer

            try
            {

                //Si el usuario escribio el nombre de la hoja se procedera con la busqueda
                conexion = new OleDbConnection(cadenaConexionArchivoExcel);//creamos la conexion con la hoja de excel
                conexion.Open(); //abrimos la conexion
                dataAdapter = new OleDbDataAdapter(consultaHojaExcel, conexion); //traemos los datos de la hoja y las guardamos en un dataSdapter
                dataSet = new DataSet(); // creamos la instancia del objeto DataSet
                dataAdapter.Fill(dataSet, "CreditosPostergados");//llenamos el dataset
                conexion.Close();//cerramos la conexion

                InfoPostergado objInt = new InfoPostergado();
                List<InfoPostergado> lstPostergados = new List<InfoPostergado>();
                InfoPostergado objPostergado = new InfoPostergado();
                DateTime FechaAprobacion = DateTime.MinValue;
                decimal Tasa = decimal.Zero;
                int Plazo = 0;
                int Gracia = 0;
                int NroDividendosAPostergar = 0;
                int FechaEscritura = 0;
                string rutcliente = "";


                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    rutcliente = dr["Rut"].ToString();

                    if (rutcliente == null)
                        break;

                    objPostergado = new InfoPostergado();

                    objPostergado.Rut = rutcliente;

                    if (DateTime.TryParse(dr["FechaAprobacion"].ToString(), out FechaAprobacion))
                        objPostergado.FechaAprobacion = FechaAprobacion.ToShortDateString();
                    else
                    {
                        objPostergado.Validado = false;
                        objPostergado.MensajeError = "Error en el Formato de la Fecha Aprobacion (DD/MM/AAAA)";
                    }
                    objPostergado.NroOperacion = dr["NroOperacion"].ToString();
                    if (int.TryParse(dr["Plazo"].ToString(), out Plazo))
                        objPostergado.Plazo = Plazo;
                    else
                    {
                        objPostergado.Validado = false;
                        objPostergado.MensajeError = "Plazo no es numero entero";
                    }

                    if (int.TryParse(dr["Gracia"].ToString(), out Gracia))
                        objPostergado.Gracia = Gracia;
                    else
                    {
                        objPostergado.Validado = false;
                        objPostergado.MensajeError = "Gracia no es numero entero";
                    }

                    if (int.TryParse(dr["NroDividendosAPostergar"].ToString(), out NroDividendosAPostergar))
                        objPostergado.NroDividendosAPostergar = NroDividendosAPostergar;
                    else
                    {
                        objPostergado.Validado = false;
                        objPostergado.MensajeError = "Dividendos a Postergar no es numero entero";
                    }

                    if (decimal.TryParse(dr["Tasa"].ToString(), out Tasa))
                        objPostergado.Tasa = Tasa;
                    else
                    {
                        objPostergado.Validado = false;
                        objPostergado.MensajeError = "Error en el Formato de la tasa";
                    }

                    if (int.TryParse(dr["FechaEscritura"].ToString(), out FechaEscritura))
                        objPostergado.FechaEscritura = FechaEscritura;
                    else
                    {
                        objPostergado.Validado = false;
                        objPostergado.MensajeError = "Fecha Escritura no es numero entero";
                    }
                  
                    lstPostergados.Add(objPostergado);
                }

                foreach (InfoPostergado c in lstPostergados)
                {
                    if (c.Validado)
                    {
                        c.IndCarga = false;
                        resultCarga =  oNegCarga.Guardar(c);
                        if (resultCarga.ResultadoGeneral)
                        {
                            if (resultCarga.Objeto.ResultadoCarga == "1")
                            {
                                c.MensajeError = resultCarga.Objeto.MensajeError;
                                c.Validado = false;
                            }
                        }
                    }
                }

                NegCargaMasivaPostergados.lstNominaPostergados = new List<InfoPostergado>();
                NegCargaMasivaPostergados.lstNominaPostergados = lstPostergados;


                Controles.CargarGrid<InfoPostergado>(ref gvPostergados, lstPostergados, new string[] { "Rut" });

                lblTotal.Text = lstPostergados.Count().ToString();
                lblAprobados.Text = lstPostergados.Where(x => x.Validado == true).Count().ToString();
                lblRechazados.Text = lstPostergados.Where(x => x.Validado == false).Count().ToString();

                Controles.MostrarMensajeExito("Lectura de Créditos Postergados Exitosa");
                
            }
            catch (Exception ex)
            {
                Controles.MostrarMensajeError(ex.Message);

            }

        }

        protected void btnValidarNomina_Click(object sender, EventArgs e)
        {
            Limpiar();
            CargarArchivo();
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {

            if (NegCargaMasivaPostergados.lstNominaPostergados == null)
            {
                Controles.MostrarMensajeAlerta("No Hay Registros Válidos para Procesar");
                return;
            }

            if (NegCargaMasivaPostergados.lstNominaPostergados.Where(x => x.Validado == true).Count() == 0)
            {
                Controles.MostrarMensajeAlerta("No Hay Registros Válidos para Procesar");
                return;
            }

            NegCargaMasivaPostergados oNegCarga = new NegCargaMasivaPostergados();
            Resultado<InfoPostergado> resultCarga = new Resultado<InfoPostergado>();

            NegCargaMasivaPostergados.lstPostergadosCreadas = new List<InfoPostergado>();
            
            //string RolEncode = Base64Decode(Convert.ToString(Request.QueryString["rol"]));
            //string UsuarioEncode = Base64Decode(Convert.ToString(Request.QueryString["usu"]));

            foreach (InfoPostergado c in NegCargaMasivaPostergados.lstNominaPostergados.Where(x => x.Validado == true))
            {
                c.IndCarga = true;

                //c.UsuarioResponsableCarga = UsuarioEncode;
                //c.RolResponsable = RolEncode;
                resultCarga = oNegCarga.Guardar(c);

                if (resultCarga.ResultadoGeneral)
                {
                    if (resultCarga.Objeto.ResultadoCarga == "1")
                    {
                        c.MensajeError = resultCarga.Objeto.MensajeError;
                        c.Validado = false;
                    }
                    //else
                    //{
                    //    c.NumeroSolicitud = resultCarga.Objeto.NumeroSolicitud;
                    //    c.Responsable = resultCarga.Objeto.Responsable;
                    //    c.IdResponsable = resultCarga.Objeto.IdResponsable;
                    //    c.FabricaAsignada = resultCarga.Objeto.FabricaAsignada;
                    //    c.NombreSucursal = resultCarga.Objeto.NombreSucursal;
                       
                    //}
                }
                else
                {
                    c.MensajeError = resultCarga.Mensaje;
                    c.Validado = false;
                   
                }

                NegCargaMasivaPostergados.lstPostergadosCreadas.Add(c);
            }

            Controles.CargarGrid<InfoPostergado>(ref gvPostergadosCreadas, NegCargaMasivaPostergados.lstPostergadosCreadas, new string[] { "Rut" });
            //CargarLog();
            Controles.MostrarMensajeExito("Carga Terminada");
        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarAExcel();
        }

        private void Limpiar()
        {
            gvPostergados.DataSource = null;
            gvPostergados.DataBind();

            gvPostergadosCreadas.DataSource = null;
            gvPostergadosCreadas.DataBind();
            lblTotal.Text = "";
            lblRechazados.Text = "";
            lblAprobados.Text = "";
        }




        private void ExportarAExcel()
        {
            var Resultado = "";
            if (NegCargaMasivaPostergados.lstPostergadosCreadas == null || NegCargaMasivaPostergados.lstPostergadosCreadas.Count == 0)
            {
                Controles.MostrarMensajeAlerta("No Hay Registros para Generar el Documento");
                return;
            }
            Resultado = Excel.ExportarGrid<InfoPostergado>(gvPostergadosCreadas, NegCargaMasivaPostergados.lstPostergadosCreadas, "Créditos Postergados", "Reporte de Créditos Postergados creadas por Nómina", new int[] { 1 });

            if (Resultado != "")
            {
                Controles.MostrarMensajeError(Resultado);
            }

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

    }
}