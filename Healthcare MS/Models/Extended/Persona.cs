using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Healthcare_MS.Models
{
    [MetadataType(typeof(DatosPersonaMetadata))]
    public partial class Persona
    {
        [Display]
        public string NombreCompleto
        {
            get
            {
                return Nombres + " " + Apellidos;
            }
        }

        [Display]
        public string RutCompleto
        {
            get
            {
                return Rut.ToString("N0") + "-" + HelperHCMS.CalcularDV(Rut.ToString());
            }
        }
    }

    public class DatosPersonaMetadata
    {
        [Required(ErrorMessage = "El campo nombres es obligatorio")]
        public string Nombres { get; set; }

        public string Apellidos { get; set; }
        public int Rut { get; set; }
    }
}