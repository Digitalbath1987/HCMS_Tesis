using Healthcare_MS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Healthcare_MS.Controllers
{
    public class MedicoController : Controller
    {
        // GET: Medico
        [HttpGet]
        [Authorize(Roles = "Médico")]
        public ActionResult Index()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "HCMS");
        }

        [HttpGet]
        [Authorize(Roles = "Médico")]
        public ActionResult CargarHora()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Médico")]
        public ActionResult VerHoras()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Médico")]
        public ActionResult ReservarHora()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Médico")]
        public ActionResult RegistrarAtencion()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Médico")]
        public ActionResult ModificarPaciente()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Médico")]
        public ActionResult RegistroAtencion()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Médico")]
        public ActionResult HistorialMedico()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Médico")]
        public ActionResult ConsultarExamen()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Médico")]
        public ActionResult ActualizarDatos()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        private string getUserName()
        {
            using (HCMSEntities db = new HCMSEntities())
            {
                var rut = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name);
                var persona = db.Persona.Where(p => p.Rut == rut).FirstOrDefault();
                return persona.Nombres.Split(' ')[0];
            }
        }
    }
}