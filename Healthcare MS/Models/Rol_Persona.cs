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
    
    public partial class Rol_Persona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rol_Persona()
        {
            this.HoraAnulada = new HashSet<HoraAnulada>();
        }
    
        public int Id { get; set; }
        public int fk_IdRol { get; set; }
        public int fk_IdPersona { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoraAnulada> HoraAnulada { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
