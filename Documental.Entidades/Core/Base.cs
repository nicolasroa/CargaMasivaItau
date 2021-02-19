using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Documental.Entidades
{
    public class Base
    {
        public int Id { get; set; }
        public int? UsuarioCreacion_Id { get; set; }
        public int? UsuarioModificacion_Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdRolWF { get; set; }
        public int? IdUsuarioWF { get; set; }
        public string DescripcionMalla { get; set; }

        public Base()
        {
            Id = -1;
            if (HttpContext.Current.Session["UsuarioId"] != null)
            {
                int? IdUsuario = (int)HttpContext.Current.Session["UsuarioId"];
                UsuarioCreacion_Id = IdUsuario;
                UsuarioModificacion_Id = IdUsuario;
                IdUsuarioWF = IdUsuario;
            }

            if (HttpContext.Current.Session["IdRolWF"] != null)
            {
                IdRolWF = (int)HttpContext.Current.Session["IdRolWF"];
            }

            FechaCreacion = DateTime.Now;
            FechaModificacion = DateTime.Now;
        }
    }


    public static class ConfigBase
    {

        #region Nombre Tablas Fijas

        public static string TablaEstado = "ESTADOS";
        public static string TablaRegiones = "REGIONES";
        public static string TablaRoles = "ROLES";
        public static string TablasCriticas = "TABLASCRITICAS";
        public static string TablaComunas = "COMUNAS";

        #endregion

        #region Codigos Fijos

        public static string CodigoActivo = "A";
        public static string CodigoInactivo = "I";
        #endregion

        public static string ConexionSQL = "ConexionSQL";


    }
}
