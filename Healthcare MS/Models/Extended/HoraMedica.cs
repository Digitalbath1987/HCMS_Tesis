using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Healthcare_MS.Models
{
    [MetadataType(typeof(HCMSAnularHoraMetadata))]
    public partial class HoraMedica
    {
        public string fechaParseada
        {
            get
            {
                return FechaHoraCargada.ToString("dd-MM-yyyy HH:mm");
            }
        }
    }

    public class HCMSAnularHoraMetadata
    {
        public System.DateTime FechaHoraCargada { get; set; }
        public int fk_IdFuncionario_Especialidad { get; set; }
        public bool Disponible { get; set; }
        public Nullable<int> fk_IdPaciente { get; set; }
        public int fk_IdEstadoHoraMedica { get; set; }
        public int fk_IdSector { get; set; }
        public string Codigo { get; set; }

        public virtual EstadoHoraMedica EstadoHoraMedica { get; set; }
        public virtual Funcionario_Especialidad Funcionario_Especialidad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoraAnulada> HoraAnulada { get; set; }
        public virtual Paciente Paciente { get; set; }
        public virtual Sector Sector { get; set; }
    }
}