using Healthcare_MS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Healthcare_MS.Controllers
{
    public class HCMSController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "HCMS");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated) return RedirectToAction("RedirectToDefault");
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AnularHora()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("RedirectToDefault");
            ViewBag.Message = "";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult DetallesHoraAnular(HCMSAnularHora model)
        {
            ViewBag.Message = string.Empty;
            if (ModelState.IsValid)
            {
                HoraMedica hora;
                HCMSEntities db = new HCMSEntities();
                hora = db.HoraMedica.Where(e => e.Codigo == model.Codigo)
                    .Where(e => e.Disponible == false)
                    .FirstOrDefault();
                if (hora == null) return RedirectToAction("SinResultado", "HCMS", new { message = "El código ingresado no es válido" });
                else if (hora.EstadoHoraMedica.Id == 3) return RedirectToAction("SinResultado", "HCMS", new { message = "La hora fue rechazada" });
                else if (hora.EstadoHoraMedica.Id == 4) return RedirectToAction("SinResultado", "HCMS", new { message = "La hora ya ha sido anulada" });
                else if (hora.FechaHoraCargada < DateTime.Now) return RedirectToAction("SinResultado", "HCMS", new { message = "No puedes anular una hora médica luego de que pasó la fecha destinada de atención" });
                return PartialView("DetallesHoraAnular", hora);
            }
            else return RedirectToAction("SinResultado", "HCMS", new { message = "Debes ingresar el código de la hora" });
        }

        [HttpPost, ActionName("AnularHora")]
        [AllowAnonymous]
        public ActionResult AnularHora(int id)
        {
            if (ModelState.IsValid)
            {
                using (HCMSEntities db = new HCMSEntities())
                {
                    var resp = db.HoraMedica.Find(id);
                    resp.fk_IdEstadoHoraMedica = 4;
                    HoraAnulada anulada = new HoraAnulada();
                    anulada.FechaAnulacion = DateTime.Now;
                    anulada.fk_IdHoraMedica = resp.Id;
                    db.HoraAnulada.Add(anulada);
                    db.SaveChanges();
                }
            }
            else ViewBag.Message = "Han ocurrido errores, por favor intenta nuevamente";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ConsultarHora()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("RedirectToDefault");
            ViewBag.Message = string.Empty;
            return View();
        }

        [HttpGet, ActionName("BuscarHora")]
        [AllowAnonymous]
        public ActionResult _BuscarHora(string Codigo)
        {
            if (!string.IsNullOrWhiteSpace(Codigo))
            {
                HoraMedica hora;
                HCMSEntities db = new HCMSEntities();
                hora = db.HoraMedica.Where(e => e.Codigo == Codigo)
                    .Where(e => e.Disponible == false)
                    .FirstOrDefault();
                if (hora == null) return RedirectToAction("SinResultado", "HCMS", new { message = "El código ingresado no es válido" });
                return PartialView("_BuscarHora", hora);
            }
            else return RedirectToAction("SinResultado", "HCMS", new { message = "Debes ingresar el código" });
        }        

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AnularHoraExamen()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("RedirectToDefault");
            ViewBag.Message = string.Empty;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult DetallesExamenAnular(HCMSAnularHoraExamen model)
        {
            ViewBag.Message = string.Empty;
            if (ModelState.IsValid)
            {
                AgendaExamen examen;
                HCMSEntities db = new HCMSEntities();
                examen = db.AgendaExamen.Where(e => e.Codigo == model.Codigo).FirstOrDefault();
                if (examen == null) return RedirectToAction("SinResultado", "HCMS", new { message = "El código ingresado no es válido" });
                else if (examen.ExamenAnulado.Any()) return RedirectToAction("SinResultado", "HCMS", new { message = "El examen ya fue anulado" });
                else if (examen.FechaAgenda < DateTime.Now) return RedirectToAction("SinResultado", "HCMS", new { message = "No es posible anular un examen después de la fecha asignada" });
                else if (examen.Examen.First().HoraExamen != null) return RedirectToAction("SinResultado", "HCMS", new { message = "No es posible anular un examen que ya fue realizado" });
                else if (examen.Examen.First().Examen_TipoExamen.First().ResultadoExamen != null) return RedirectToAction("SinResultado", "HCMS", new { message = "Este examen ya fue tomado, ¡Consulta si ya tiene sus resultados!" });
                return PartialView("DetallesExamenAnular", examen);
            }
            else return RedirectToAction("SinResultado", "HCMS", new { message = "Debes ingresar el código de la hora" });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AnularHoraExamen(int id)
        {
            if (ModelState.IsValid)
            {
                using (HCMSEntities db = new HCMSEntities())
                {
                    var resp = db.AgendaExamen.Find(id);
                    ExamenAnulado anulado = new ExamenAnulado();
                    anulado.FechaAnulacion = DateTime.Now;
                    anulado.fk_IdAgendaExamen = resp.Id;
                    db.ExamenAnulado.Add(anulado);
                    db.SaveChanges();
                }
            }
            else ViewBag.Message = "Han ocurrido errores, por favor revisa";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ConsultarHoraExamen()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("RedirectToDefault");
            ViewBag.Message = string.Empty;
            return View();
        }

        [HttpGet, ActionName("ResultadoHoraExamen")]
        [AllowAnonymous]
        public ActionResult _ResultadoHoraExamen(HCMSConsultarExamen model)
        {
            ViewBag.Message = string.Empty;
            if (ModelState.IsValid)
            {
                Examen examen;
                HCMSEntities db = new HCMSEntities();
                AgendaExamen ag = db.AgendaExamen.Where(e => e.Codigo == model.Codigo).FirstOrDefault();
                if (ag != null)
                {
                    examen = db.Examen.Where(e => e.AgendaExamen.Codigo == model.Codigo).FirstOrDefault();
                    if (examen.Examen_TipoExamen.First().ResultadoExamen != null) return RedirectToAction("SinResultado", "HCMS", new { message = "Este examen ya fue tomado, ¡Consulta si ya tiene sus resultados!" });
                    return PartialView("_ResultadoExamen", examen);
                }
                else return RedirectToAction("SinResultado", "HCMS", new { message = "El código ingresado no es válido" });
            }
            else return RedirectToAction("SinResultado", "HCMS", new { message = "Debes ingresar el código de la hora" });
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ConsultarExamen()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("RedirectToDefault");
            ViewBag.Message = string.Empty;
            return View();
        }

        [HttpGet, ActionName("ResultadoExamen")]
        [AllowAnonymous]
        public ActionResult _ResultadoExamen(HCMSConsultarExamen model)
        {
            ViewBag.Message = string.Empty;
            if (ModelState.IsValid)
            {
                Examen examen;
                HCMSEntities db = new HCMSEntities();
                AgendaExamen ag = db.AgendaExamen.Where(e => e.Codigo == model.Codigo).FirstOrDefault();
                if (ag != null)
                {
                    examen = db.Examen.Where(e => e.AgendaExamen.Codigo == model.Codigo).FirstOrDefault();
                    if (examen == null) return RedirectToAction("SinResultado", "HCMS", new { message = "No hay resultados del examen, ya que aún no se ha realizado" });
                    return PartialView("_ResultadoExamen", examen);
                }
                else return RedirectToAction("SinResultado", "HCMS", new { message = "El código ingresado no es válido" });
            }
            else return RedirectToAction("SinResultado", "HCMS", new { message = "Debes ingresar el código de la hora" });
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SinResultado(string message)
        {
            ViewBag.Message = message;
            return PartialView();
        }

        public ActionResult RedirectToDefault()
        {
            string[] roles = Roles.GetRolesForUser();
            if (roles.Contains("Administrativo")) return RedirectToAction("Index", "Intranet");
            else if (roles.Contains("Médico")) return RedirectToAction("Index", "Medico");
            else if (roles.Contains("Administrador")) return RedirectToAction("Index", "Administrador");
            else if (roles.Contains("Master")) return RedirectToAction("Index", "Master");
            else if (roles.Contains("Paciente")) return RedirectToAction("Index", "PortalPacientes");
            else return RedirectToAction("Index", "HCMS");
        }
    }
}