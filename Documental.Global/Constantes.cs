using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Documental.Global
{
    public static class Constantes
    {

        #region NodosXML

        public enum DatosSistema
        {
            NombreAplicacion,
            Ruta,
            NombrePagina,
            ModoDebug

        }

        #region Mensajes al Usuario

        public enum MensajesUsuario
        {
            //Errores
            ErrorGuardar,
            ErrorEliminar,
            ErrorListar,
            ErrorLimpiar,
            ErrorCargarGrid,
            ErrorCargarCombo,
            ErrorCargarCheckBoxList,
            ErrorCargarRadioButtonList,
            ErrorCargarEntidad,
            ErrorCargarFormulario,
            ErrorConfiguracionNoEncontrada,
            ErrorCodigoInactivoNoEncontrado,
            ErrorProcesarAsignacionMenu,
            ErrorProcesarAsignacionControles,
            ErrorValidarEntidad,
            //Permisos
            SinPermisoCrear,
            SinPermisoModificar,
            SinPermisoEliminar,

            //Procesos Terminados
            RegistroGuardado,
            RegistroEliminado,
            FechaActualizacion,
            AsignacionMenuCompleta,
            AsignacionControlesCompleta,
            CambioClaveCorrecto,
            RecuperacionClaveCorrecta,
            DesbloqueoUsuarioCorrecto,

            //Validaciones
            ValidarNombre,
            ValidarEstado,
            ValidarTamanioClave,
            ValidarPlazoValidez,
            ValidarIntentos,
            ValidarNotifiacion,
            ValidarNombreUsuario,
            ValidarClave,
            ValidarDescripcion,
            ValidarTipoMenu,
            ValidarApellido,
            ValidarRut,
            ValidarMail,
            ValidarEstadoCivil,
            ValidarRol,
            ValidarClavesDistintas,
            ValidarClavesAnterior,
            ValidarIdInterno,

            //Seguridad
            ValidarUrlSitio,
            ValidarFormatoClave,
            ValidarSeleccionUsuario,
            ValidarPreguntaSecreta,
            ValidarRespuesta,
            ValidarRespuestaDistintas,
            ValidarTipoUsuario,

            //Parametros

            ValidarRolControlInterno,
            ValidarRolExplotacion,



            //Monedas
            ValidarUF,
            ValidarFechaUltimaUF,
            ValidarIPC,
            ValidarUfIngresada,

            //Ipc
            ValidarValorIpc,
            ValidarVariacionIpc,

            //Servidores
            ValidarDescripcionServidor,
            ValidarDireccionIp,
            ValidarAmbiente,

            //Aplicacion

            ValidarServidor,

            //Dias de No Proceso
            ValidarTipoAsignacion,
            ValidarRegion,
            ValidarAplicacion,


            //Grupos Documentos
            ValidarNombreGrupoDocumento,
            ValidarCodigoGrupoDocumento,

            //Tipos Documentos
            ValidarNombreTipoDocumento,
            ValidarCodigoTipoDocumento,
            AsignacionRolesTipDocumento,

            //Portal
            ValidarFechaEmision,
            ValidarSubirArchivo,
            DescargarArchivo,
            SeleccionarGrupoDocumento,
            SeleccionarTipoDocumento,
            IngresarFechaEmision,
            SeleccionarArchivo,
            TamanioMaximoArchivo,
            AsignacionCorrecta,

        }
        public enum TitulosMensaje
        {
            Error,
            Validacion,
            Informacion

        };

        #endregion

        #endregion

        #region Variables Globales

        public static bool ModoDebug = bool.Parse(ArchivoRecursos.ObtenerValorNodo("ModoDebug"));
        public static string StringId = "Id";
        public static string StringIdUsuario = "Usuario_Id";
        public static string StringDescripcion = "Descripcion";
        public static string StringNombre = "Nombre";
        public static string StringNombreCompleto = "NombreCompleto";

        #endregion

        #region Rutas


        public static string UrlInicio = "~/Inicio.aspx";
        public static string UrlLogin = "~/Login.aspx";
        public static string UrlSinAcceso = "~/SinAcceso.aspx";
        public static string UrlSinAccesoPoPup = "~/SinAccesoPoPup.aspx";

        //Rutas a Imagenes
        public static string UrlImgPlus = "~/Img/Controles/GridView/plus.gif";
        public static string UrlImgMinus = "~/Img/Controles/GridView/minus.gif";
        //Ruta Repositorio Archivos PDF
        public static string RutaPdf = "~/Repositorio/Pdf/";
        //RutaPlantillaExcel
        public static string RutaXLS = "~/Plantillas/XLS/";

        #endregion

        #region Controles

        public static string gvOperacionales = "gvOperacionales";
        public static string gvHijosOperacionales = "gvHijosOperacionales";
        public static string gvMantenedores = "gvMantenedores";
        public static string gvHijosMantenedores = "gvHijosMantenedores";
        public static string chkAcceso = "chkAcceso";
        public static string chkCrear = "chkCrear";
        public static string chkActivo = "chkActivo";
        public static string chkVisible = "chkVisible";
        public static string chkModificar = "chkModificar";
        public static string chkEliminar = "chkEliminar";

        public static string chkListar = "chkListar";
        public static string chkVer = "chkVer";
        public static string chkAbrir = "chkAbrir";
        //public static string chkModificar = "chkModificar";
        public static string chkDescargar = "chkDescargar";
        //public static string chkEliminar = "chkEliminar";
        public static string chkSubir = "chkSubir";

        #endregion


    }
}
