//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CAC.Library.Model.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_usuario
    {
        public tbl_usuario()
        {
            this.tbl_archivo_cac = new HashSet<tbl_archivo_cac>();
            this.tbl_usuario_rol = new HashSet<tbl_usuario_rol>();
        }
    
        public System.Guid id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string numCelular { get; set; }
        public System.Guid idOrganizacion { get; set; }
        public string uid_firebase { get; set; }
    
        public virtual ICollection<tbl_archivo_cac> tbl_archivo_cac { get; set; }
        public virtual tbl_organizacion tbl_organizacion { get; set; }
        public virtual ICollection<tbl_usuario_rol> tbl_usuario_rol { get; set; }
    }
}
