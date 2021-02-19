using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Documental.Entidades.Documentos
{
    public class RolesTiposDocumentos : Base
    {
        public int IdTipoDocumento { get; set; }
        public int IdRol { get; set; }
        public bool PermisoListar { get; set; }
        public bool PermisoVer { get; set; }
        public bool PermisoAbrir { get; set; }
        public bool PermisoModificar { get; set; }
        public bool PermisoDescargar { get; set; }
        public bool PermisoEliminar { get; set; }
        public bool PermisoSubir { get; set; }
        public int IdGrupoDocumento { get; set; }
        public int Estado_Id { get; set; }
    }

    public class RolesTiposDocumentosInfo : RolesTiposDocumentos
    {
        //public int IdGrupoDocumento { get; set; }
        public string GrupoDocumento { get; set; }
        public string TipoDocumento { get; set; }
    }

    public class V_RolesWF
    {
        public int IdRol { get; set; }
        public string Rol { get; set; }
        public string RolDescripCorta { get; set; }
    }

}
