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
    
    public partial class tbl_organizacion
    {
        public tbl_organizacion()
        {
            this.tbl_usuario = new HashSet<tbl_usuario>();
        }
    
        public System.Guid id { get; set; }
        public string NIT { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string numTelefonico { get; set; }
        public bool eps { get; set; }
    
        public virtual ICollection<tbl_usuario> tbl_usuario { get; set; }
    }
}
