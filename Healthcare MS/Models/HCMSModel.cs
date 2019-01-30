using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Healthcare_MS.Models
{
    public class HCMSConsultarHora
    {
        [Required(ErrorMessage = "Debes escribir el código de tu hora médica")]
        [Display(Name = "Código de hora médica")]
        public string Codigo { get; set; }
    }

    public class HCMSConsultarHoraExamen
    {
        [Required(ErrorMessage = "Debes escribir el código de tu examen")]
        [Display(Name = "Código de examen")]
        public string Codigo { get; set; }
    }

    public class HCMSAnularHora
    {
        [Required(ErrorMessage = "Para anular tu hora médica, debes ingresar el código correspondiente")]
        [Display(Name = "Código de hora médica")]
        public string Codigo { get; set; }
    }

    public class HCMSAnularHoraExamen
    {
        [Required(ErrorMessage = "Para anular tu hora, debes ingresar el código de tu examen")]
        [Display(Name = "Código de examen")]
        public string Codigo { get; set; }
    }

    public class HCMSConsultarExamen
    {
        [Required(ErrorMessage = "Debes escribir el código de tu examen")]
        [Display(Name = "Código de examen")]
        public string Codigo { get; set; }
    }
}