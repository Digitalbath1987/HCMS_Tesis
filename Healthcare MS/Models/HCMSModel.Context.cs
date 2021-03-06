﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Healthcare_MS.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HCMSEntities : DbContext
    {
        public HCMSEntities()
            : base("name=HCMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AgendaExamen> AgendaExamen { get; set; }
        public virtual DbSet<CategoriaExamen> CategoriaExamen { get; set; }
        public virtual DbSet<Cobertura> Cobertura { get; set; }
        public virtual DbSet<Comuna> Comuna { get; set; }
        public virtual DbSet<Especialidad> Especialidad { get; set; }
        public virtual DbSet<Establecimiento> Establecimiento { get; set; }
        public virtual DbSet<EstadoHoraMedica> EstadoHoraMedica { get; set; }
        public virtual DbSet<Examen_TipoExamen> Examen_TipoExamen { get; set; }
        public virtual DbSet<ExamenAnulado> ExamenAnulado { get; set; }
        public virtual DbSet<Funcionario> Funcionario { get; set; }
        public virtual DbSet<Funcionario_Especialidad> Funcionario_Especialidad { get; set; }
        public virtual DbSet<Funcionario_Establecimiento> Funcionario_Establecimiento { get; set; }
        public virtual DbSet<HoraAnulada> HoraAnulada { get; set; }
        public virtual DbSet<HoraMedica> HoraMedica { get; set; }
        public virtual DbSet<Paciente> Paciente { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<ResultadoExamen> ResultadoExamen { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Rol_Persona> Rol_Persona { get; set; }
        public virtual DbSet<Sector> Sector { get; set; }
        public virtual DbSet<TipoExamen> TipoExamen { get; set; }
    }
}
