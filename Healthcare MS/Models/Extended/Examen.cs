using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Healthcare_MS.Models
{
    [MetadataType(typeof(ExamenMetadata))]
    public partial class Examen
    {
        public string fechaParseada
        {
            get
            {
                return HoraExamen.ToString("dd-MM-yyyy HH:mm");
            }
        }
    }

    public class ExamenMetadata
    {
        public DateTime HoraExamen { get; set; }
    }

}