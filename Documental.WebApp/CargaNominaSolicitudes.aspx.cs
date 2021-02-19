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
    public partial class CargaNominaSolicitudes : System.Web.UI.Page
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



                CargarLog();
            }
        }
        private void CargarLog()
        {
            NegCargaMasiva oNegCarga = new NegCargaMasiva();
            Resultado<LogCargaMasiva> resultCarga = new Resultado<LogCargaMasiva>();
            LogCargaMasiva objSolicitud = new LogCargaMasiva();

            resultCarga = oNegCarga.BuscarLog(objSolicitud);
            if (resultCarga.ResultadoGeneral)
            {
                Controles.CargarGrid<LogCargaMasiva>(ref gvLogCarga, resultCarga.Lista, new string[] { });
            }
            else
            {
                Controles.MostrarMensajeError(resultCarga.Mensaje);
            }



        }
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
                    var Archivo = "CargaMasiva" + DateTime.Now.ToString().Replace(" ", "").Replace("-", "").Replace(":", "") + fileExtension;

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
            NegCargaMasiva oNegCarga = new NegCargaMasiva();
            Resultado<InfoSolicitudes> resultCarga = new Resultado<InfoSolicitudes>();
            string consultaHojaExcel = "Select * from [Solicitudes$]";

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
                dataAdapter.Fill(dataSet, "Solicitudes");//llenamos el dataset
                conexion.Close();//cerramos la conexion

                InfoSolicitudes objInt = new InfoSolicitudes();
                List<InfoSolicitudes> lstSolicitudes = new List<InfoSolicitudes>();
                InfoSolicitudes objSolicitud = new InfoSolicitudes();
                DateTime FechaAprobacion = DateTime.MinValue;
                decimal MontoAprobado = decimal.Zero;







                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {
                    if (Convert.ToString(dr["Rut"]).Length == 0)
                        break;

                    objSolicitud = new InfoSolicitudes();


                    objSolicitud.Rut = dr["Rut"].ToString();
                    objSolicitud.Nombre = dr["Nombre"].ToString();
                    objSolicitud.Paterno = dr["Paterno"].ToString();
                    objSolicitud.Materno = dr["Materno"].ToString();
                    objSolicitud.Canal = dr["Canal"].ToString();
                    objSolicitud.CodigoSucursal = dr["CodigoSucursal"].ToString();
                    objSolicitud.EjecutivoCuentas = "0";
                    objSolicitud.TipoCredito = dr["TipoCredito"].ToString();
                    if (DateTime.TryParse(dr["FechaAprobacion"].ToString(), out FechaAprobacion))
                        objSolicitud.FechaAprobacion = FechaAprobacion;
                    else
                    {
                        objSolicitud.Validado = false;
                        objSolicitud.MensajeError = "Error en el Formato de la Fecha (DD/MM/AAAA)";
                    }

                    if (decimal.TryParse(dr["MontoAprobado"].ToString(), out MontoAprobado))
                        objSolicitud.MontoAprobado = MontoAprobado;
                    else
                    {
                        objSolicitud.Validado = false;
                        objSolicitud.MensajeError = "Error en el Formato del Monto Aprobado";
                    }


                    objSolicitud.Plazo = dr["Plazo"].ToString();
                    objSolicitud.TipoGarantia = dr["TipoGarantia"].ToString();
                    objSolicitud.IndRenegociacion = dr["IndRenegociacion"].ToString();
                    objSolicitud.OperacionRenegociada = dr["OperacionRenegociada"].ToString();

                    lstSolicitudes.Add(objSolicitud);
                }



                foreach (InfoSolicitudes c in lstSolicitudes)
                {
                    if (c.Validado)
                    {
                        c.IndCarga = false;
                        resultCarga = oNegCarga.Guardar(c);
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

                NegCargaMasiva.lstNomina = new List<InfoSolicitudes>();
                NegCargaMasiva.lstNomina = lstSolicitudes;

                Controles.CargarGrid<InfoSolicitudes>(ref gvNomina, lstSolicitudes, new string[] { "Rut" });

                lblTotal.Text = lstSolicitudes.Count().ToString();
                lblAprobados.Text = lstSolicitudes.Where(x => x.Validado == true).Count().ToString();
                lblRechazados.Text = lstSolicitudes.Where(x => x.Validado == false).Count().ToString();

                Controles.MostrarMensajeExito("Lectura de Nómina Exitosa");




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
            if (NegCargaMasiva.lstNomina == null)
            {
                Controles.MostrarMensajeAlerta("No Hay Registros Válidos para Procesar");
                return;
            }

            if (NegCargaMasiva.lstNomina.Where(x => x.Validado == true).Count() == 0)
            {
                Controles.MostrarMensajeAlerta("No Hay Registros Válidos para Procesar");
                return;
            }
            NegCargaMasiva oNegCarga = new NegCargaMasiva();
            Resultado<InfoSolicitudes> resultCarga = new Resultado<InfoSolicitudes>();

            NegCargaMasiva.lstSolicitudesCreadas = new List<InfoSolicitudes>();




            string RolEncode = Base64Decode(Convert.ToString(Request.QueryString["rol"]));
            string UsuarioEncode = Base64Decode(Convert.ToString(Request.QueryString["usu"]));


            foreach (InfoSolicitudes c in NegCargaMasiva.lstNomina.Where(x => x.Validado == true))
            {
                c.IndCarga = true;

                c.UsuarioResponsableCarga = UsuarioEncode;
                c.RolResponsable = RolEncode;
                resultCarga = oNegCarga.Guardar(c);

                if (resultCarga.ResultadoGeneral)
                {
                    if (resultCarga.Objeto.ResultadoCarga == "1")
                    {
                        c.MensajeError = resultCarga.Objeto.MensajeError;
                        c.Validado = false;
                    }
                    else
                    {
                        c.NumeroSolicitud = resultCarga.Objeto.NumeroSolicitud;
                        c.Responsable = resultCarga.Objeto.Responsable;
                        c.IdResponsable = resultCarga.Objeto.IdResponsable;
                        c.FabricaAsignada = resultCarga.Objeto.FabricaAsignada;
                        c.NombreSucursal = resultCarga.Objeto.NombreSucursal;
                       
                    }
                }
                else
                {
                    c.MensajeError = resultCarga.Mensaje;
                    c.Validado = false;
                   
                }

                NegCargaMasiva.lstSolicitudesCreadas.Add(c);
            }

            Controles.CargarGrid<InfoSolicitudes>(ref gvSolicitudesCreadas, NegCargaMasiva.lstSolicitudesCreadas, new string[] { "Rut" });
            CargarLog();
            Controles.MostrarMensajeExito("Carga Terminada");
        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarAExcel();
        }

        private void Limpiar()
        {
            gvNomina.DataSource = null;
            gvNomina.DataBind();

            gvSolicitudesCreadas.DataSource = null;
            gvSolicitudesCreadas.DataBind();
            lblTotal.Text = "";
            lblRechazados.Text = "";
            lblAprobados.Text = "";
        }




        private void ExportarAExcel()
        {
            var Resultado = "";
            if (NegCargaMasiva.lstSolicitudesCreadas == null || NegCargaMasiva.lstSolicitudesCreadas.Count == 0)
            {
                Controles.MostrarMensajeAlerta("No Hay Registros para Generar el Documento");
                return;
            }
            Resultado = Excel.ExportarGrid<InfoSolicitudes>(gvSolicitudesCreadas, NegCargaMasiva.lstSolicitudesCreadas, "Solicitudes Creadas", "Reporte de Solicitudes Creadas por Nómina", new int[] { 1 });

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