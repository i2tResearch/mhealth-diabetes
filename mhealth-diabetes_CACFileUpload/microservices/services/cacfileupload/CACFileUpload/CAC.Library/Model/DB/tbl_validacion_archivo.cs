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
    
    public partial class tbl_validacion_archivo
    {
        public System.Guid id { get; set; }
        public string descripcion { get; set; }
        public System.DateTime fechaCreacion { get; set; }
        public System.Guid idArchivo { get; set; }
    
        public virtual tbl_archivo_cac tbl_archivo_cac { get; set; }
    }
}