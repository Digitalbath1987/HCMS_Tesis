using Healthcare_MS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Healthcare_MS.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
        [Authorize(Roles = "Master")]
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

        [Authorize(Roles = "Master")]
        public ActionResult ListadoCM()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [Authorize(Roles = "Master")]
        public ActionResult CrearCM()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [Authorize(Roles = "Master")]
        public ActionResult ModificarCM()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [Authorize(Roles = "Master")]
        public ActionResult CrearAdminCM()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [Authorize(Roles = "Master")]
        public ActionResult BloquearDesbloquearCM()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [Authorize(Roles = "Master")]
        public ActionResult Especialidades()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [Authorize(Roles = "Master")]
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