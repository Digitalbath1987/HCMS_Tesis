//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Funcionario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Funcionario()
        {
            this.AgendaExamen = new HashSet<AgendaExamen>();
            this.AgendaExamen1 = new HashSet<AgendaExamen>();
            this.Funcionario_Especialidad = new HashSet<Funcionario_Especialidad>();
            this.Funcionario_Establecimiento = new HashSet<Funcionario_Establecimiento>();
        }
    
        public int Id { get; set; }
        public int fk_IdPersona { get; set; }
        public string Observacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaExamen> AgendaExamen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaExamen> AgendaExamen1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Funcionario_Especialidad> Funcionario_Especialidad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Funcionario_Establecimiento> Funcionario_Establecimiento { get; set; }
        public virtual Persona Persona { get; set; }
    }
}