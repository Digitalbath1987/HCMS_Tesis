using Healthcare_MS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Healthcare_MS.Controllers
{
    public class AdministradorController : Controller
    {
        // GET: Administrador
        [Authorize(Roles = "Administrador")]
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

        [Authorize(Roles = "Administrador")]
        public ActionResult VerDatosCM()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult ModificarDatosCM()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Sectores()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult CrearFuncionario()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult ModificarFuncionario()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult BloquearDesbloquear()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [Authorize(Roles = "Administrador")]
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