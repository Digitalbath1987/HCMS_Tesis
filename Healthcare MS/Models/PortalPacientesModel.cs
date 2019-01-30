using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Healthcare_MS.Models
{
    public class PPLoginModel
    {
        [Required(ErrorMessage = "Debes ingresar tu RUT")]
        [Display(Name = "RUT")]
        public string Rut { get; set; }

        [Required(ErrorMessage = "Debes ingresar tu contraseña")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class PPEditarDatosPersonalesModel
    { 
        [Display(Name = "RUT")]
        public string RutCompleto { get; set; }

        public int Rut { get; set; }

        [Required(ErrorMessage = "Debes ingresar tus nombres")]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Debes ingresar tus apellidos")]
        [Display(Name = "Apellidos")]
        public string Apellidos{ get; set; }

        [Required(ErrorMessage = "Debes seleccionar la fecha de nacimiento")]
        [DateRangeValidation(ErrorMessage = "La fecha de nacimiento debe ser real")]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public string Region { get; set; }
        public string Comuna { get; set; }
        public string Direccion { get; set; }
    }

    public class PPEditarDatosContactoModel
    {
        public int Rut { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }

        [EmailAddress(ErrorMessage = "Debes ingresar un formato válido de correo")]
        [Required(ErrorMessage = "Debes ingresar tu correo electrónico")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class PPEditarPasswordModel
    {
        public int Rut { get; set; }

        [Required(ErrorMessage = "Debes ingresar tu contraseña anterior")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Debes ingresar tu nueva contraseña")]
        [StringLength(25, ErrorMessage = "La contraseña debe tener entre 6 y 25 carácteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Debes ingresar tu contraseña nuevamente")]
        [StringLength(25, ErrorMessage = "La contraseña debe tener entre 6 y 25 carácteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string RepeatPassword { get; set; }
    }

    public class PPEditarContactoModel
    {
        public int Rut { get; set; }
        public string NombreContacto { get; set; }
        public string ApellidoContacto { get; set; }
        public string Parentesco { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
    }

    public class PPActualizarDatosModel
    {
        public PPEditarDatosPersonalesModel editarDatosPersonales { get; set; }
        public PPEditarDatosContactoModel editarDatosContacto { get; set; }
        public PPEditarPasswordModel editarPassword { get; set; }
        public PPEditarContactoModel editarContacto { get; set; }
    }

    public class PPReservarHoraModel
    {
        
    }

    public class PPConsultarHoraModel
    {
        [Required(ErrorMessage = "Debes ingresar el código de la hora")]
        [Display(Name = "Código de hora")]
        public string Codigo { get; set; }
    }

    public class PPAnularHoraModel
    {
        [Required(ErrorMessage = "Para anular tu hora médica, debes ingresar el código correspondiente")]
        [Display(Name = "Código de hora médica")]
        public string Codigo { get; set; }
    }

    public class PPAnularHorExamenaModel
    {
        [Required(ErrorMessage = "Para anular tu hora médica, debes ingresar el código correspondiente")]
        [Display(Name = "Código de hora médica")]
        public string Codigo { get; set; }
    }

    public class PPConsultarHoraExamenModel
    {
        [Required(ErrorMessage = "Debes ingresar el código del examen")]
        [Display(Name = "Código de examen")]
        public string Codigo { get; set; }
    }

    public class PPConsultarExamenModel
    {
        [Required(ErrorMessage = "Debes ingresar el código del examen")]
        [Display(Name = "Código de examen")]
        public string Codigo { get; set; }
    }

    public class PPHistorialMedicoModel
    {

    }
}