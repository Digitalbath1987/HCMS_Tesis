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
    
    public partial class ResultadoExamen
    {
        public int id { get; set; }
        public int fk_IdExamen_TipoExamen { get; set; }
        public string resultado { get; set; }
        public string valor_referencia { get; set; }
    
        public virtual Examen_TipoExamen Examen_TipoExamen { get; set; }
    }
}
