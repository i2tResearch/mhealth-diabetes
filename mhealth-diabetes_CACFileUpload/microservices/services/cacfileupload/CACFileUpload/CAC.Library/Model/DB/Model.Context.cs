﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ModelContainer : DbContext
    {
        public ModelContainer()
            : base("name=ModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<tbl_archivo_cac> tbl_archivo_cac { get; set; }
        public DbSet<tbl_organizacion> tbl_organizacion { get; set; }
        public DbSet<tbl_paciente_prioritario> tbl_paciente_prioritario { get; set; }
        public DbSet<tbl_rol> tbl_rol { get; set; }
        public DbSet<tbl_usuario> tbl_usuario { get; set; }
        public DbSet<tbl_usuario_rol> tbl_usuario_rol { get; set; }
        public DbSet<tbl_validacion_archivo> tbl_validacion_archivo { get; set; }
        public DbSet<tbl_variable_prioritaria> tbl_variable_prioritaria { get; set; }
        public DbSet<tbl_cac> tbl_cac { get; set; }
        public DbSet<tbl_variable_desactualizada> tbl_variable_desactualizada { get; set; }
    }
}
