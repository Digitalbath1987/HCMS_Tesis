using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Healthcare_MS.Models
{
    public class IntranetLogin
    {
        [Required(ErrorMessage = "Debes ingresar tu RUT")]
        [Display(Name = "RUT")]
        public string Rut { get; set; }

        [Required(ErrorMessage = "Debes ingresar tu contraseña")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class IntranetVerListadoPacientes
    {

    }

    public class IntranetCrearPacientes
    {

    }

    public class IntranetModificarPaciente
    {

    }

    public class IntranetActualizarDatos
    {

    }

    public class IntranetReservarHora
    {

    }

    public class IntranetReservarHoraExamen
    {

    }

    public class IntranetAnularHora
    {

    }

    public class IntranetAnularHoraExamen
    {

    }

    public class IntranetRegistroAtencion
    {

    }

    public class IntranetActualizarAtencion
    {

    }

    public class IntranetConsultarExamen
    {

    }

    public class IntranetIngresarResultadoExamen
    {

    }

    public class IntranetTrasladarPacienteCM
    {

    }

}