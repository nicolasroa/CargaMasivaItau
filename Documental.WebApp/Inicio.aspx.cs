using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.WebControls;
using Documental.Global;
using Documental.Entidades;
using Documental.Negocio.Administracion;
using Documental.Negocio.Documentos;
using EntityDOC = Documental.Entidades.Documentos;
using Documental.Entidades.Documentos;
using System.IO;
using System.Net.Mail;
namespace Documental
{
    public partial class Inicio : System.Web.UI.Page
    {

        #region EVENTOS

        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    Anthem.Manager.Register(Page);
        //}

        protected void Page_Load(object sender, EventArgs e)
        {

                              

            Anthem.Manager.Register(Page);
            Anthem.Manager.RegisterStartupScript(this.GetType(), "jquerydate", "getjquerydate();", true);
            Anthem.Manager.RegisterStartupScript(this.GetType(), "jquerydateRep", "getjquerydateRep();", true);

            if (!Page.IsPostBack)
            {
                if (!ObtenerDatosWorkflow())
                {
                    return;
                }
                CargarGrupoDocumento();
                CargarTipoDocumentos(-2, 0);
                CargarGrillaDocumentoSolicitud();
                CargarGrillaRepositorioDocumento();
                CargarBandejaGrupoDocumento();
                CargarBandejaGrupoDocumentoRepositorio();
                CargarGrillaBitacora();

                //fluArchivo.UpdateAfterCallBack = true;
            }
        }

        protected void ddlGrupoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fluArchivo.Dispose();
            //fluArchivo.Attributes.Clear();
            CargarTipoDocumentos(int.Parse(ddlGrupoDocumento.SelectedValue), 1);
        }

        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoDocumento.SelectedIndex > 0)
            {
                //fluArchivo.Dispose();
                //fluArchivo.Attributes.Clear();
                var ObjetoTD = NegTiposDocumentos.ObtenerTipoDocumento(int.Parse(ddlTipoDocumento.SelectedValue));
                btnGuardar.Visible = ObjetoTD.PermisoSubir;
            }
            else
                btnGuardar.Visible = false;
        }

        protected void ddlGrupoDocumentoSolicitudBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTipoDocumentos(int.Parse(ddlGrupoDocumentoSolicitudBus.SelectedValue), 2);
            CargarGrillaDocumentoSolicitud();
        }

        protected void ddlTipoDocumentoSolicitudBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrillaDocumentoSolicitud();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarEntidad();
        }

        protected void gvwDocumentosSolicitud_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /*Alerta del Documento*/
                Anthem.Image imgEstadoAlertaDoc = (Anthem.Image)e.Row.FindControl("imgEstadoAlertaDoc");
                Anthem.HiddenField hdnEstadoAlertaDoc = (Anthem.HiddenField)e.Row.FindControl("hdnEstadoAlertaDoc");
                imgEstadoAlertaDoc.ImageUrl = ArchivoRecursos.ObtenerValorNodo(hdnEstadoAlertaDoc.Value);

                /*Imagen dependiendo de la Extension*/
                Anthem.Image imgTipoDocumento = (Anthem.Image)e.Row.FindControl("imgTipoDocumento");
                Anthem.HiddenField hdnTipoDoc = (Anthem.HiddenField)e.Row.FindControl("hdnTipoDocumento");
                imgTipoDocumento.ImageUrl = ArchivoRecursos.ObtenerValorNodo(hdnTipoDoc.Value.ToLower());

                /*Ocultar controles*/
                Anthem.HiddenField hdnId = (Anthem.HiddenField)e.Row.FindControl("IdSolDoc");

                if (string.IsNullOrEmpty(hdnId.Value) || hdnId.Value == "-1")
                {
                    e.Row.Cells[3].Text = "";
                    ((Anthem.ImageButton)e.Row.FindControl("btnDescargar")).Visible = false;
                    ((Anthem.ImageButton)e.Row.FindControl("btnEditarDocumento")).Visible = false;
                    ((Anthem.ImageButton)e.Row.FindControl("btnEliminarDocumento")).Visible = false;
                    ((Anthem.LinkButton)e.Row.FindControl("lnkNombreArchivo")).Enabled = false;
                    imgEstadoAlertaDoc.Visible = false;
                    imgTipoDocumento.Visible = false;
                }
                else
                {
                    ((Anthem.ImageButton)e.Row.FindControl("btnDescargar")).Visible = bool.Parse(((Anthem.HiddenField)e.Row.FindControl("hdnPermisoDescargar")).Value);
                    ((Anthem.ImageButton)e.Row.FindControl("btnEditarDocumento")).Visible = bool.Parse(((Anthem.HiddenField)e.Row.FindControl("hdnPermisoModificar")).Value);
                    ((Anthem.ImageButton)e.Row.FindControl("btnEliminarDocumento")).Visible = bool.Parse(((Anthem.HiddenField)e.Row.FindControl("hdnPermisoEliminar")).Value);

                    ((Anthem.LinkButton)e.Row.FindControl("lnkNombreArchivo")).Enabled = bool.Parse(((Anthem.HiddenField)e.Row.FindControl("hdnPermisoDescargar")).Value);
                }
            }
        }

        protected void btnDescargar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvwDocumentosSolicitud.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            DescargarDocumento(NegArchivosRepositorios.ObtenerArchivoRepositorio(Id));
        }

        protected void btnEditarDocumento_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvwDocumentosSolicitud.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            ObtenerDatos(Id, 1);
            btnGuardar.Visible = true;
            //divFormularioEditar.Visible = true;

            //Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormularioPortalEditar();", true);
        }

        protected void btnEliminarDocumento_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvwDocumentosSolicitud.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            int IdArchivo = int.Parse(gvwDocumentosSolicitud.DataKeys[row.RowIndex].Values["IdArchivo"].ToString());
            EliminarArchivoRepositorio(Id,IdArchivo);
        }

        protected void lnkNombreArchivo_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvwDocumentosSolicitud.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            DescargarDocumento(NegArchivosRepositorios.ObtenerArchivoRepositorio(Id));
        }

        protected void gvwDocumentosSolicitud_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwDocumentosSolicitud.PageIndex = e.NewPageIndex;
            CargarGrillaDocumentoSolicitud();
        }

        protected void ddlGrupoDocumentoRepFil_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTipoDocumentos(int.Parse(ddlGrupoDocumentoRepFil.SelectedValue), 4);
            CargarGrillaRepositorioDocumento();
        }

        protected void ddlTipoDocumentoRepFil_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrillaRepositorioDocumento();
        }

        protected void gvwRepositorioDocumento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwRepositorioDocumento.PageIndex = e.NewPageIndex;
            CargarGrillaRepositorioDocumento();
        }

        protected void gvwRepositorioDocumento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /*Alerta del Documento*/
                Anthem.Image imgEstadoAlertaDoc = (Anthem.Image)e.Row.FindControl("imgEstadoAlertaRepDoc");
                Anthem.HiddenField hdnEstadoAlertaDoc = (Anthem.HiddenField)e.Row.FindControl("hdnEstadoAlertaRepDoc");
                imgEstadoAlertaDoc.ImageUrl = ArchivoRecursos.ObtenerValorNodo(hdnEstadoAlertaDoc.Value);

                /*Imagen dependiendo de la Extension*/
                Anthem.Image imgTipoDocumento = (Anthem.Image)e.Row.FindControl("imgTipoDocumentoRepDoc");
                Anthem.HiddenField hdnTipoDoc = (Anthem.HiddenField)e.Row.FindControl("hdnTipoDocumentoRepDoc");
                imgTipoDocumento.ImageUrl = ArchivoRecursos.ObtenerValorNodo(hdnTipoDoc.Value);

                /*Ocultar controles*/
                Anthem.HiddenField hdnId = (Anthem.HiddenField)e.Row.FindControl("IdRepDoc");

                if (string.IsNullOrEmpty(hdnId.Value) || hdnId.Value == "-1")
                {
                    e.Row.Cells[3].Text = "";
                    ((Anthem.ImageButton)e.Row.FindControl("btnDescargarRepDoc")).Visible = false;
                    ((Anthem.ImageButton)e.Row.FindControl("btnEditarRepDoc")).Visible = false;
                    ((Anthem.ImageButton)e.Row.FindControl("btnEliminarRepDoc")).Visible = false;
                    ((Anthem.LinkButton)e.Row.FindControl("lnkNombreArchivoRepDoc")).Enabled = false;
                    imgEstadoAlertaDoc.Visible = false;
                    imgTipoDocumento.Visible = false;
                }
                else
                {
                    ((Anthem.ImageButton)e.Row.FindControl("btnDescargarRepDoc")).Visible = bool.Parse(((Anthem.HiddenField)e.Row.FindControl("hdnPermisoDescargarRep")).Value);
                    ((Anthem.ImageButton)e.Row.FindControl("btnEditarRepDoc")).Visible = bool.Parse(((Anthem.HiddenField)e.Row.FindControl("hdnPermisoModificarRep")).Value);
                    ((Anthem.ImageButton)e.Row.FindControl("btnEliminarRepDoc")).Visible = bool.Parse(((Anthem.HiddenField)e.Row.FindControl("hdnPermisoEliminarRep")).Value);
                    ((Anthem.LinkButton)e.Row.FindControl("lnkNombreArchivoRepDoc")).Enabled = bool.Parse(((Anthem.HiddenField)e.Row.FindControl("hdnPermisoDescargarRep")).Value);
                }

            }
        }

        protected void gvwBitacora_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwBitacora.PageIndex = e.NewPageIndex;
            CargarGrillaBitacora();
        }
        protected void btnEditarRepDoc_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvwRepositorioDocumento.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            ObtenerDatos(Id, 3);
            btnGuardarRep.Visible = true;
            //divFormularioEditar.Visible = true;

            //Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "MostrarFormularioPortalEditar();", true);
        }

        protected void btnEliminarRepDoc_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvwRepositorioDocumento.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            int IdArchivo = int.Parse(gvwRepositorioDocumento.DataKeys[row.RowIndex].Values["IdArchivo"].ToString());
            EliminarArchivoRepositorio(Id,IdArchivo);
        }
        protected void btnAsignarRepDoc_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvwRepositorioDocumento.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            int IdArchivo = int.Parse(gvwRepositorioDocumento.DataKeys[row.RowIndex].Values["IdArchivo"].ToString());
            AsignarDocumentoSolicitud(Id,IdArchivo);
        }
        protected void lnkNombreArchivoRepDoc_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvwRepositorioDocumento.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            DescargarDocumento(NegArchivosRepositorios.ObtenerRepositorioDocumento(Id));
        }

        protected void btnDescargarRepDoc_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvwRepositorioDocumento.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            DescargarDocumento(NegArchivosRepositorios.ObtenerRepositorioDocumento(Id));
        }

        protected void lnkBanRepDocNombreArchivo_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvwRepositorio.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            ddlGrupoDocumentoRep.SelectedValue = Id.ToString();
            CargarTipoDocumentos(Id, 4);
            ddlTipoDocumentoRep.SelectedValue = "-1";
            CargarGrillaRepositorioDocumento();
        }

        protected void lnkNombreArchivoBanSol_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
            int Id = int.Parse(gvGrupoDocumentosSolicitud.DataKeys[row.RowIndex].Values[Constantes.StringId].ToString());
            ddlGrupoDocumentoSolicitudBus.SelectedValue = Id.ToString();
            CargarTipoDocumentos(Id, 2);
            CargarGrillaDocumentoSolicitud();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        protected void gvwBitacora_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text == " " && e.Row.Cells[2].Text == " " && e.Row.Cells[3].Text == " ")
                {
                    e.Row.Cells[0].Text = " ";
                }
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarAExcel();
        }

        protected void ddlGrupoDocumentoRep_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fluArchivoRep.Dispose();
            //fluArchivoRep.Attributes.Clear();
            CargarTipoDocumentos(int.Parse(ddlGrupoDocumentoRep.SelectedValue), 3);
        }

        protected void ddlTipoDocumentoRep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoDocumentoRep.SelectedIndex > 0)
            {
                //fluArchivoRep.Dispose();
                //fluArchivoRep.Attributes.Clear();
                var ObjetoTD = NegTiposDocumentos.ObtenerTipoDocumento(int.Parse(ddlTipoDocumentoRep.SelectedValue));
                btnGuardarRep.Visible = ObjetoTD.PermisoSubir;
            }
            else
                btnGuardarRep.Visible = false;
        }

        protected void btnGuardarRep_Click(object sender, EventArgs e)
        {
            GuardarEntidadRep();
        }

        protected void btnCancelarRep_Click(object sender, EventArgs e)
        {
            LimpiarFormularioRep();
        }

        //Generacion de Excel
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control) { }

        #endregion

        #region METODOS

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private bool ObtenerDatosWorkflow()
        {
            try
            {
                    string RolEncode = Convert.ToString(Request.QueryString["rol"]);
                    string SolicitudEncode = Convert.ToString(Request.QueryString["sol"]);
                    string UsuarioEncode = Convert.ToString(Request.QueryString["usu"]);

                string RolWF = "";
                string SolicitudWF = "";
                string UsuarioWF = "";

                if (RolEncode != null && SolicitudEncode != null)
                {
                    RolWF = Base64Decode(RolEncode);
                    SolicitudWF = Base64Decode(SolicitudEncode);
                    UsuarioWF = Base64Decode(UsuarioEncode);
                }
                else
                {
                    //Response.Redirect("SinInformacion.aspx");
                    RolWF = "SUPCENHIP";
                        SolicitudWF = " 57133";
                        UsuarioWF = "1";
                }



                //Declaración de Variables de Búsqueda
                var ObjetoDatosWF = new EntityDOC.DatosWorkflow();
                var NegDatosWF = new NegPortal();
                var ObjResultadoWF = new Resultado<EntityDOC.DatosWorkflow>();

                //Asignación datos del WF
                ObjetoDatosWF.NumeroSolicitud = SolicitudWF;
                ObjetoDatosWF.RolWF = RolWF;
                ObjetoDatosWF.UsuarioWF = int.Parse(UsuarioWF);

                //NegPortal.getsetDatosWorkflow = ObjetoDatosWF;


                //Ejecución del Proceso de Búsqueda
                ObjResultadoWF = NegDatosWF.Buscar(ObjetoDatosWF);

                if (ObjResultadoWF.ResultadoGeneral == true)
                {
                    ObjetoDatosWF = ObjResultadoWF.Lista.FirstOrDefault();

                    if (ObjetoDatosWF != null)
                    {
                        HttpContext.Current.Session["UsuarioId"] = ObjetoDatosWF.IdUsuarioWF; // int.Parse(lblUsuarioWF.Text);
                        HttpContext.Current.Session["IdRolWF"] = ObjetoDatosWF.IdRolWF; //int.Parse(lblIdRolWF.Text);
                        //ObjetoDatosWF.IdUsuario = int.Parse(lblUsuarioWF.Text);
                        NegPortal.getsetDatosWorkflow = ObjetoDatosWF;
                        NegPortal.getsetDatosWorkflow.lstParticipantes = new List<Participantes>();
                        if (ObjetoDatosWF.NombreSolicitante != null)
                        {
                            lblNombre.Text = ObjetoDatosWF.NombreSolicitante;
                            lblNroIdentidad.Text = ObjetoDatosWF.Rut.ToString() + '-' + ObjetoDatosWF.Dv;
                            lblNumSolicitud.Text = ObjetoDatosWF.NumeroSolicitud;
                            lblProceso.Text = ObjetoDatosWF.DescripcionMalla;


                            foreach (var participante in ObjResultadoWF.Lista)
                            {
                                NegPortal.getsetDatosWorkflow.lstParticipantes.Add(new Participantes
                                {
                                    RutParticipante = participante.Rut,
                                    DescripcionTipoParticipacion = participante.DescripcionTipoParticipacion,
                                    TipoParticipacion_Id = participante.TipoParticipacion_Id,
                                    NombreParticipante = participante.NombreSolicitante
                                });
                            }

                            Controles.CargarCombo<Participantes>(ref ddlParticipantesRep, NegPortal.getsetDatosWorkflow.lstParticipantes, "RutParticipante", "NombreParticipante", "-- Participantes --", "-1");

                        }
                        else
                        {
                            lblNombre.Text = "Sin Participante Ingresado";
                            lblNroIdentidad.Text = "Sin Participante Ingresado";
                            lblNumSolicitud.Text = ObjetoDatosWF.NumeroSolicitud;
                            lblProceso.Text = ObjetoDatosWF.DescripcionMalla;
                            tabParticipantes.Visible = false;
                        }
                        if (ObjetoDatosWF.Supervisor == false)
                        {
                            tabBitacora.Visible = false;
                            tabs3.Visible = false;
                        }


                        return true;

                    }
                    else
                    {
                        Response.Redirect("SinInformacion.aspx");

                    }
                }
                else
                {
                    if (Constantes.ModoDebug == true)
                    {
                        Controles.MostrarMensajeError(ObjResultadoWF.Mensaje);

                    }
                    else
                    {
                        Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "GrupoDocumento");
                    }
                    return false;
                }

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "GrupoDocumento");
                }
                return false;
            }
            return false;
        }

        private void CargarGrillaDocumentoSolicitud()
        {
            try
            {

                //Declaración de Variables de Búsqueda
                var ObjetoArchRep = new EntityDOC.ArchivosRepositoriosInfo();
                var NegArchRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<EntityDOC.ArchivosRepositoriosInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoArchRep.NumeroSolicitud = NegPortal.getsetDatosWorkflow.NumeroSolicitud;
                ObjetoArchRep.IdGrupoDocumento = int.Parse(ddlGrupoDocumentoSolicitudBus.SelectedValue);
                ObjetoArchRep.IdTipoDocumento = int.Parse(ddlTipoDocumentoSolicitudBus.SelectedValue);
                //ObjetoArchRep.IdRolWF = NegPortal.getsetDatosWorkflow.IdRolWF;


                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegArchRep.Buscar(ObjetoArchRep);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    NegArchivosRepositorios.getsetlstArchivoRepositorio = ObjetoResultado.Lista;
                    Controles.CargarGrid<EntityDOC.ArchivosRepositoriosInfo>(ref gvwDocumentosSolicitud, ObjetoResultado.Lista, new string[] { Constantes.StringId, "IdArchivo" });
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "ArchivosRepositorios");
                }
            }
        }

        private void CargarGrillaRepositorioDocumento()
        {
            try
            {

                //Declaración de Variables de Búsqueda
                var ObjetoArchRep = new EntityDOC.ArchivosRepositoriosInfo();
                var NegArchRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<EntityDOC.ArchivosRepositoriosInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoArchRep.IdGrupoDocumento = int.Parse(ddlGrupoDocumentoRepFil.SelectedValue);
                ObjetoArchRep.IdTipoDocumento = int.Parse(ddlTipoDocumentoRepFil.SelectedValue);


                ObjetoArchRep.strRutParticipantes = "";
                foreach (var item in NegPortal.getsetDatosWorkflow.lstParticipantes)
                {
                    ObjetoArchRep.strRutParticipantes = ObjetoArchRep.strRutParticipantes + "," + item.RutParticipante.ToString();
                }

                //ObjetoArchRep.Rut = NegPortal.getsetDatosWorkflow.Rut;
                ObjetoArchRep.IdRolWF = NegPortal.getsetDatosWorkflow.IdRolWF;


                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegArchRep.Buscar(ObjetoArchRep);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    NegArchivosRepositorios.getsetlstRepositorioDoc = ObjetoResultado.Lista;

                    Controles.CargarGrid<EntityDOC.ArchivosRepositoriosInfo>(ref gvwRepositorioDocumento, ObjetoResultado.Lista, new string[] { Constantes.StringId, "Rut","IdArchivo" });
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "ArchivosRepositorios");
                }
            }
        }

        private void CargarGrillaBitacora()
        {
            try
            {
                if (!tabBitacora.Visible)
                    return;

                //Declaración de Variables de Búsqueda
                var ObjetoBita = new EntityDOC.BitacoraInfo();
                var NegBita = new NegBitacora();
                var ObjetoResultado = new Resultado<EntityDOC.BitacoraInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoBita.NumeroSolicitud = NegPortal.getsetDatosWorkflow.NumeroSolicitud;
                ObjetoBita.strRutParticipantes = "";
                foreach (var item in NegPortal.getsetDatosWorkflow.lstParticipantes)
                {
                    ObjetoBita.strRutParticipantes = ObjetoBita.strRutParticipantes + "," + item.RutParticipante.ToString();
                }
                //ObjetoBita.IdRol = NegPortal.getsetDatosWorkflow.IdRol;
                //ObjetoBita.IdUsuario = NegPortal.getsetDatosWorkflow.IdUsuario;


                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegBita.Buscar(ObjetoBita);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    NegBitacora.getLstBitacora = ObjetoResultado.Lista;
                    Controles.CargarGrid<EntityDOC.BitacoraInfo>(ref gvwBitacora, ObjetoResultado.Lista, new string[] { Constantes.StringId });
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Bitacora");
                }
            }
        }

        private void CargarBandejaGrupoDocumento()
        {
            try
            {

                //Declaración de Variables de Búsqueda
                var ObjetoArchivoRep = new EntityDOC.ArchivosRepositoriosInfo();
                var NegArchivoRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<EntityDOC.ArchivosRepositoriosInfo>();

                //Asignación de Variables de Búsqueda
                ObjetoArchivoRep.NumeroSolicitud = NegPortal.getsetDatosWorkflow.NumeroSolicitud;

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegArchivoRep.BuscarBandejaGrupoDocumento(ObjetoArchivoRep);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<EntityDOC.ArchivosRepositoriosInfo>(ref gvGrupoDocumentosSolicitud, ObjetoResultado.Lista, new string[] { Constantes.StringId });
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Tablas");
                }
            }
        }

        private void CargarBandejaGrupoDocumentoRepositorio()
        {
            try
            {

                //Declaración de Variables de Búsqueda
                var ObjetoArchivoRep = new EntityDOC.ArchivosRepositoriosInfo();
                var NegArchivoRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<EntityDOC.ArchivosRepositoriosInfo>();

                //Asignación de Variables de Búsqueda

                ObjetoArchivoRep.strRutParticipantes = "";
                foreach (var item in NegPortal.getsetDatosWorkflow.lstParticipantes)
                {
                    ObjetoArchivoRep.strRutParticipantes = ObjetoArchivoRep.strRutParticipantes + "," + item.RutParticipante.ToString();
                }

                //Ejecución del Proceso de Búsqueda
                ObjetoResultado = NegArchivoRep.BuscarBandejaGrupoDocumentoRepositorio(ObjetoArchivoRep);
                if (ObjetoResultado.ResultadoGeneral)
                {
                    Controles.CargarGrid<EntityDOC.ArchivosRepositoriosInfo>(ref gvwRepositorio, ObjetoResultado.Lista, new string[] { Constantes.StringId });
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Tablas");
                }
            }
        }

        private void CargarGrupoDocumento()
        {
            try
            {
                //Declaración de Variables de Búsqueda
                var ObjetoGrupoDoc = new EntityDOC.GruposDocumentos();
                var NegGrupoDoc = new NegGruposDocumentos();
                var ObjResultadoGD = new Resultado<EntityDOC.GruposDocumentos>();

                //Asignación Grupos de Documentos
                ObjetoGrupoDoc.Id = -1;
                ObjetoGrupoDoc.GrupoDocumento = "";

                //Ejecución del Proceso de Búsqueda
                ObjResultadoGD = NegGrupoDoc.ObtenerGrupoDocumentos(ObjetoGrupoDoc);

                if (ObjResultadoGD.ResultadoGeneral)
                {
                    Controles.CargarCombo<EntityDOC.GruposDocumentos>(ref ddlGrupoDocumento, ObjResultadoGD.Lista, Constantes.StringId, "GrupoDocumento", "-- Seleccione --", "-1");
                    Controles.CargarCombo<EntityDOC.GruposDocumentos>(ref ddlGrupoDocumentoSolicitudBus, ObjResultadoGD.Lista, Constantes.StringId, "GrupoDocumento", "-- Todos --", "-1");
                    Controles.CargarCombo<EntityDOC.GruposDocumentos>(ref ddlGrupoDocumentoRep, ObjResultadoGD.Lista, Constantes.StringId, "GrupoDocumento", "-- Seleccione --", "-1");
                    Controles.CargarCombo<EntityDOC.GruposDocumentos>(ref ddlGrupoDocumentoRepFil, ObjResultadoGD.Lista, Constantes.StringId, "GrupoDocumento", "-- Todos --", "-1");
                }
                else
                {
                    //Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                    Controles.MostrarMensajeAlerta("Grupos de Documentos Sin Datos");
                }

            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "GrupoDocumento");
                }
            }
        }

        private void CargarTipoDocumentos(int IdGrupoDocumento, int TipoControl)
        {
            try
            {
                var ObjetoTipoDoc = new EntityDOC.TiposDocumentosInfo();
                var NegTipoDoc = new NegTiposDocumentos();
                var ObjResultadoTD = new Resultado<EntityDOC.TiposDocumentosInfo>();

                //Asignación Tipos de Documentos
                ObjetoTipoDoc.Id = -1;
                ObjetoTipoDoc.IdGrupoDocumento = IdGrupoDocumento;

                ObjResultadoTD = NegTipoDoc.ObtenerTipoDocumentos(ObjetoTipoDoc);

                if (ObjResultadoTD.ResultadoGeneral)
                {
                    NegTiposDocumentos.getLstTipoDoc = ObjResultadoTD.Lista;

                    switch (TipoControl)
                    {
                        case 1:
                            Controles.CargarCombo<EntityDOC.TiposDocumentosInfo>(ref ddlTipoDocumento, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Seleccione --", "-1");
                            break;
                        case 2:
                            Controles.CargarCombo<EntityDOC.TiposDocumentosInfo>(ref ddlTipoDocumentoSolicitudBus, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Todos --", "-1");
                            break;
                        case 3:
                            Controles.CargarCombo<EntityDOC.TiposDocumentosInfo>(ref ddlTipoDocumentoRep, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Seleccione --", "-1");
                            break;
                        case 4:
                            Controles.CargarCombo<EntityDOC.TiposDocumentosInfo>(ref ddlTipoDocumentoRepFil, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Todos --", "-1");
                            break;
                        default:
                            Controles.CargarCombo<EntityDOC.TiposDocumentosInfo>(ref ddlTipoDocumento, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Seleccione --", "-1");
                            Controles.CargarCombo<EntityDOC.TiposDocumentosInfo>(ref ddlTipoDocumentoSolicitudBus, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Todos --", "-1");
                            Controles.CargarCombo<EntityDOC.TiposDocumentosInfo>(ref ddlTipoDocumentoRep, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Seleccione --", "-1");
                            Controles.CargarCombo<EntityDOC.TiposDocumentosInfo>(ref ddlTipoDocumentoRepFil, ObjResultadoTD.Lista, Constantes.StringId, "TipoDocumento", "-- Todos --", "-1");
                            break;
                    }

                }
                else
                {
                    //Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                    Controles.MostrarMensajeAlerta("Grupos de Documentos Sin Datos");
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarCombo.ToString()) + "TipoDocumento");
                }
            }
        }

        private void LimpiarFormulario()
        {
            hfId.Value = "";
            hfIdArchivo.Value = "";
            ddlGrupoDocumento.SelectedIndex = 0;
            CargarTipoDocumentos(-2, 1);
            //ddlTipoDocumento.SelectedIndex = 0;
            txtFechaEmision.Text = "";
            fluArchivo.Attributes.Clear();
            txtObservacion.Text = "";

        }

        private void LimpiarFormularioRep()
        {
            hfIdRep.Value = "";
            hfIdArchivoRep.Value = "";
            ddlGrupoDocumentoRep.SelectedIndex = 0;
            CargarTipoDocumentos(-2, 3);
            //ddlTipoDocumentoRep.SelectedIndex = 0;
            txtFechaEmisionRep.Text = "";
            txtObservacionRep.Text = "";
            ddlParticipantesRep.ClearSelection();
            //fluArchivoRep.Attributes.Clear();
            //Anthem.Manager.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), "clearFileUploadRep();", true);

        }

        private void GuardarEntidad()
        {
            try
            {
                //Declaración de Variables
                var ObjetoArchivoRep = new EntityDOC.ArchivosRepositoriosInfo();
                var NegArchivoRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<EntityDOC.ArchivosRepositoriosInfo>();

                if (!ValidarFormulario())
                {
                    return;
                }

                //Asignacion de Variales
                if (hfId.Value.Length != 0)
                {
                    ObjetoArchivoRep.Id = int.Parse(hfId.Value.ToString());
                    ObjetoArchivoRep = DatosEntidad(ObjetoArchivoRep);
                }

                //Agregar documento
                string filePath = fluArchivo.PostedFile.FileName;
                string filename = Path.GetFileName(filePath);
                string ext = Path.GetExtension(filename);
                string contenttype = String.Empty;

                Stream fs = fluArchivo.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                if (hfIdArchivo.Value == "")
                    hfIdArchivo.Value = "-1";
                ObjetoArchivoRep.IdArchivo = int.Parse(hfIdArchivo.Value);
                ObjetoArchivoRep.IdGrupoDocumento = int.Parse(ddlGrupoDocumento.SelectedValue);
                ObjetoArchivoRep.IdTipoDocumento = int.Parse(ddlTipoDocumento.SelectedValue);
                ObjetoArchivoRep.FechaEmision = DateTime.Parse(txtFechaEmision.Text);
                ObjetoArchivoRep.Descripcion = txtObservacion.Text;
                ObjetoArchivoRep.NombreArchivo = System.IO.Path.GetFileName(fluArchivo.FileName);
                ObjetoArchivoRep.ExtensionArchivo = System.IO.Path.GetExtension(fluArchivo.FileName);
                ObjetoArchivoRep.SizeKB = fluArchivo.FileBytes.Length.ToString();
                ObjetoArchivoRep.ContentType = fluArchivo.PostedFile.ContentType;
                ObjetoArchivoRep.ArchivoVB = bytes;
                ObjetoArchivoRep.Estado_Id = 1;
                ObjetoArchivoRep.Rut = -1; //NegPortal.getsetDatosWorkflow.Rut;
                ObjetoArchivoRep.NumeroSolicitud = NegPortal.getsetDatosWorkflow.NumeroSolicitud;

                
                //Ejecucion del procedo para Guardar
                ObjetoResultado = NegArchivoRep.Guardar(ObjetoArchivoRep);

                if (ObjetoResultado.ResultadoGeneral)
                {
                    LimpiarFormulario();
                    btnGuardar.Visible = false;
                    CargarGrillaDocumentoSolicitud();
                    CargarBandejaGrupoDocumento();
                    CargarGrillaBitacora();
                    CargarGrillaRepositorioDocumento();
                    CargarBandejaGrupoDocumentoRepositorio();

                    Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.RegistroGuardado.ToString()));
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Portal");
                }
            }
        }

        private void GuardarEntidadRep()
        {
            try
            {
                //Declaración de Variables
                var ObjetoArchivoRep = new EntityDOC.ArchivosRepositoriosInfo();
                var NegArchivoRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<EntityDOC.ArchivosRepositoriosInfo>();

                if (!ValidarFormularioRep())
                {
                    return;
                }

                //Asignacion de Variales
                if (hfIdRep.Value.Length != 0)
                {
                    ObjetoArchivoRep.Id = int.Parse(hfIdRep.Value.ToString());
                    ObjetoArchivoRep = DatosEntidad(ObjetoArchivoRep);
                }

                //Agregar documento
                string filePath = fluArchivoRep.PostedFile.FileName;
                string filename = Path.GetFileName(filePath);
                string ext = Path.GetExtension(filename);
                string contenttype = String.Empty;

                Stream fs = fluArchivoRep.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                if (hfIdArchivoRep.Value == "")
                    hfIdArchivoRep.Value = "-1";

                ObjetoArchivoRep.IdArchivo = int.Parse(hfIdArchivoRep.Value);
                ObjetoArchivoRep.IdGrupoDocumento = int.Parse(ddlGrupoDocumentoRep.SelectedValue);
                ObjetoArchivoRep.IdTipoDocumento = int.Parse(ddlTipoDocumentoRep.SelectedValue);
                ObjetoArchivoRep.FechaEmision = DateTime.Parse(txtFechaEmisionRep.Text);
                ObjetoArchivoRep.Descripcion = txtObservacionRep.Text;
                ObjetoArchivoRep.NombreArchivo = System.IO.Path.GetFileName(fluArchivoRep.FileName);
                ObjetoArchivoRep.ExtensionArchivo = System.IO.Path.GetExtension(fluArchivoRep.FileName);
                ObjetoArchivoRep.SizeKB = fluArchivoRep.FileBytes.Length.ToString();
                ObjetoArchivoRep.ContentType = fluArchivoRep.PostedFile.ContentType;
                ObjetoArchivoRep.ArchivoVB = bytes;
                ObjetoArchivoRep.Estado_Id = 1;
                ObjetoArchivoRep.Rut = int.Parse(ddlParticipantesRep.SelectedValue);
                ObjetoArchivoRep.NombreParticipante = ddlParticipantesRep.SelectedItem.Text;
                ObjetoArchivoRep.NumeroSolicitud = "-1";//NegPortal.getsetDatosWorkflow.NumeroSolicitud;


                //Ejecucion del procedo para Guardar
                ObjetoResultado = NegArchivoRep.Guardar(ObjetoArchivoRep);

                if (ObjetoResultado.ResultadoGeneral)
                {
                    LimpiarFormularioRep();
                    btnGuardarRep.Visible = false;
                    CargarGrillaRepositorioDocumento();
                    CargarBandejaGrupoDocumentoRepositorio();
                    CargarGrillaBitacora();
                    CargarBandejaGrupoDocumento();
                    CargarGrillaDocumentoSolicitud();
                    Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.RegistroGuardado.ToString()));
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Portal");
                }
            }
        }

        private void EliminarArchivoRepositorio(int Id, int IdArchivo)
        {
            try
            {
                //Declaración de Variables
                var ObjetoArchivoRep = new EntityDOC.ArchivosRepositoriosInfo();
                var NegArchivoRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<EntityDOC.ArchivosRepositoriosInfo>();

                ObjetoArchivoRep.Id = Id;
                ObjetoArchivoRep.IdArchivo = IdArchivo;
                //ObjetoArchivoRep.IdRol = NegPortal.getsetDatosWorkflow.IdRol;
                //ObjetoArchivoRep.IdUsuario = NegPortal.getsetDatosWorkflow.IdUsuario;

                //Ejecucion del procedo para Guardar
                ObjetoResultado = NegArchivoRep.Eliminar(ObjetoArchivoRep);

                if (ObjetoResultado.ResultadoGeneral)
                {

                    CargarGrillaDocumentoSolicitud();
                    CargarBandejaGrupoDocumento();
                    CargarBandejaGrupoDocumentoRepositorio();
                    CargarGrillaBitacora();
                    CargarGrillaRepositorioDocumento();

                    Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.RegistroEliminado.ToString()));
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Portal");
                }
            }
        }



        private void AsignarDocumentoSolicitud(int Id,int IdArchivo)
        {
            try
            {
                //Declaración de Variables
                var ObjetoArchivoRep = new EntityDOC.ArchivosRepositoriosInfo();
                var NegArchivoRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<EntityDOC.ArchivosRepositoriosInfo>();

                ObjetoArchivoRep.Id = Id;
                ObjetoArchivoRep.IdArchivo = IdArchivo;
                ObjetoArchivoRep.NumeroSolicitud = NegPortal.getsetDatosWorkflow.NumeroSolicitud;


                //Ejecucion del procedo para Guardar
                ObjetoResultado = NegArchivoRep.AsignarDocumentoSolicitud(ref ObjetoArchivoRep);

                if (ObjetoResultado.ResultadoGeneral)
                {

                    CargarGrillaDocumentoSolicitud();
                    CargarBandejaGrupoDocumento();
                    CargarBandejaGrupoDocumentoRepositorio();
                    CargarGrillaBitacora();
                    CargarGrillaRepositorioDocumento();

                    Controles.MostrarMensajeExito(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.AsignacionCorrecta.ToString()));
                }
                else
                {
                    Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarGrid.ToString()) + "Portal");
                }
            }
        }

        private bool ValidarFormulario()
        {
            if (ddlGrupoDocumento.SelectedIndex == 0)
            {

                Controles.MensajeEnControl(ddlGrupoDocumento.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.SeleccionarGrupoDocumento.ToString()));
                return false;
            }

            if (ddlTipoDocumento.SelectedIndex == 0)
            {

                Controles.MensajeEnControl(ddlTipoDocumento.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.SeleccionarTipoDocumento.ToString()));
                return false;
            }

            if (txtFechaEmision.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFechaEmision.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.IngresarFechaEmision.ToString()));
                return false;
            }

            if (Convert.ToDateTime(txtFechaEmision.Text) > DateTime.Now)
            {
                Controles.MensajeEnControl(txtFechaEmision.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarFechaEmision.ToString()));
                return false;
            }

            if (!fluArchivo.HasFile)
            {
                Controles.MensajeEnControl(fluArchivo.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.SeleccionarArchivo.ToString()));
                return false;
            }

            var ObjetoConfiguracion = NegConfiguracionGeneral.Obtener();
            if (fluArchivo.PostedFile.ContentLength > ObjetoConfiguracion.TamanioArchivoBytes)
            {
                fluArchivo.Attributes.Clear();
                Controles.MensajeEnControl(fluArchivo.ClientID, string.Format("{0} {1} KB", ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.TamanioMaximoArchivo.ToString()), ObjetoConfiguracion.TamanioMaximoArchivo));
                return false;
            }

            return true;
        }

        private bool ValidarFormularioRep()
        {

            if (ddlParticipantesRep.SelectedIndex == 0)
            {
                Controles.MensajeEnControl(ddlParticipantesRep.ClientID, "Debe Seleccionar un Participante");
                return false;
            }

            if (ddlGrupoDocumentoRep.SelectedIndex == 0)
            {

                Controles.MensajeEnControl(ddlGrupoDocumentoRep.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.SeleccionarGrupoDocumento.ToString()));
                return false;
            }

            if (ddlTipoDocumentoRep.SelectedIndex == 0)
            {

                Controles.MensajeEnControl(ddlTipoDocumentoRep.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.SeleccionarTipoDocumento.ToString()));
                return false;
            }

            if (txtFechaEmisionRep.Text.Length == 0)
            {
                Controles.MensajeEnControl(txtFechaEmisionRep.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.IngresarFechaEmision.ToString()));
                return false;
            }

            if (Convert.ToDateTime(txtFechaEmisionRep.Text) > DateTime.Now)
            {
                Controles.MensajeEnControl(txtFechaEmisionRep.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ValidarFechaEmision.ToString()));
                return false;
            }

            if (!fluArchivoRep.HasFile)
            {
                Controles.MensajeEnControl(fluArchivoRep.ClientID, ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.SeleccionarArchivo.ToString()));
                return false;
            }

            var ObjetoConfiguracion = NegConfiguracionGeneral.Obtener();
            if (fluArchivoRep.PostedFile.ContentLength > ObjetoConfiguracion.TamanioArchivoBytes)
            {
                fluArchivoRep.Attributes.Clear();
                Controles.MensajeEnControl(fluArchivoRep.ClientID, string.Format("{0} {1} KB", ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.TamanioMaximoArchivo.ToString()), ObjetoConfiguracion.TamanioMaximoArchivo));
                return false;
            }

            return true;
        }

        private void MostrarMensajeValidacion(string Validacion)
        {
            Controles.MostrarMensajeAlerta(ArchivoRecursos.ObtenerValorNodo(Validacion));
        }

        private void LlenarFormulario(EntityDOC.ArchivosRepositoriosInfo oArchivoRepositorio)
        {
            try
            {
                hfId.Value = oArchivoRepositorio.Id.ToString();
                hfIdArchivo.Value = oArchivoRepositorio.IdArchivo.ToString();
                ddlGrupoDocumento.SelectedValue = oArchivoRepositorio.IdGrupoDocumento.ToString();
                CargarTipoDocumentos(oArchivoRepositorio.IdGrupoDocumento, 1);
                ddlTipoDocumento.SelectedValue = oArchivoRepositorio.IdTipoDocumento.ToString();
                txtFechaEmision.Text = oArchivoRepositorio.FechaEmision.ToShortDateString();
                txtObservacion.Text = oArchivoRepositorio.Descripcion;
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + "Portal");
                }
            }
        }

        private void LlenarFormularioRep(EntityDOC.ArchivosRepositoriosInfo oArchivoRepositorio)
        {
            try
            {
                hfIdRep.Value = oArchivoRepositorio.Id.ToString();
                hfIdArchivoRep.Value = oArchivoRepositorio.IdArchivo.ToString();
                ddlGrupoDocumentoRep.SelectedValue = oArchivoRepositorio.IdGrupoDocumento.ToString();
                CargarTipoDocumentos(oArchivoRepositorio.IdGrupoDocumento, 3);
                ddlTipoDocumentoRep.SelectedValue = oArchivoRepositorio.IdTipoDocumento.ToString();
                ddlParticipantesRep.SelectedValue = oArchivoRepositorio.Rut.ToString();
                txtFechaEmisionRep.Text = oArchivoRepositorio.FechaEmision.ToShortDateString();
                txtObservacionRep.Text = oArchivoRepositorio.Descripcion;
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarFormulario.ToString()) + "Portal");
                }
            }
        }

        private EntityDOC.ArchivosRepositoriosInfo DatosEntidad(EntityDOC.ArchivosRepositoriosInfo Entidad)
        {
            try
            {
                var ObjetoArchivoRep = new EntityDOC.ArchivosRepositoriosInfo();
                var NegArchivoRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<EntityDOC.ArchivosRepositoriosInfo>();

                ObjetoResultado = NegArchivoRep.Buscar(Entidad);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoArchivoRep = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoArchivoRep != null)
                    {
                        return ObjetoArchivoRep;
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "ArchivoRepositorio");
                        }
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "ArchivoRepositorio");
                }
                return null;
            }
        }

        private void ObtenerDatos(int Id, int flagTab)
        {
            try
            {
                var ObjetoArchivoRep = new EntityDOC.ArchivosRepositoriosInfo();
                var NegArchivoRep = new NegArchivosRepositorios();
                var ObjetoResultado = new Resultado<EntityDOC.ArchivosRepositoriosInfo>();

                ObjetoArchivoRep.Id = Id;
                ObjetoResultado = NegArchivoRep.Buscar(ObjetoArchivoRep);

                if (ObjetoResultado.ResultadoGeneral == true)
                {
                    ObjetoArchivoRep = ObjetoResultado.Lista.FirstOrDefault();

                    if (ObjetoArchivoRep != null)
                    {
                        if (flagTab == 1)
                            LlenarFormulario(ObjetoArchivoRep);
                        else if (flagTab == 3)
                            LlenarFormularioRep(ObjetoArchivoRep);
                    }
                    else
                    {
                        if (Constantes.ModoDebug == true)
                        {
                            Controles.MostrarMensajeError(ObjetoResultado.Mensaje);
                        }
                        else
                        {
                            Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "Portal");
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.ErrorCargarEntidad.ToString()) + "GrupoDocumento");
                }
            }

        }

        private void DescargarDocumento(EntityDOC.ArchivosRepositoriosInfo oInfo)
        {

            try
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + oInfo.NombreArchivo);
                Response.ContentType = oInfo.ContentType;
                Response.BinaryWrite(oInfo.ArchivoVB);
                Response.End();
                Response.Flush();
            }
            catch (Exception Ex)
            {
                if (Constantes.ModoDebug == true)
                {
                    Controles.MostrarMensajeError(Ex.Message);
                }
                else
                {
                    Controles.MostrarMensajeError(ArchivoRecursos.ObtenerValorNodo(Constantes.MensajesUsuario.DescargarArchivo.ToString()) + "Portal");
                }
            }

        }

        private void ExportarAExcel()
        {
            var Resultado = "";
            if (NegBitacora.getLstBitacora == null || NegBitacora.getLstBitacora.Count == 0)
            {
                Controles.MostrarMensajeAlerta("No Hay Registros para Generar el Documento");
                return;
            }
            Resultado = Excel.ExportarGrid<BitacoraInfo>(gvwBitacora, NegBitacora.getLstBitacora, "Bitacora_Solicitud" + NegPortal.getsetDatosWorkflow.NumeroSolicitud.ToString() + "", "Reporte de Bitacora");

            if (Resultado != "")
            {
                Controles.MostrarMensajeError(Resultado);
            }

        }

        #endregion

      



    }
}