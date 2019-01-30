using Healthcare_MS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Healthcare_MS.Controllers
{
    public class IntranetController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Administrativo")]
        public ActionResult Index()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "HCMS");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(IntranetLogin model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (HelperHCMS.validarRut(model.Rut))
                {
                    using (HCMSEntities db = new HCMSEntities())
                    {
                        try
                        {
                            int rut = Convert.ToInt32(model.Rut.Remove(model.Rut.Length - 1).Replace(".", "").Replace("-", ""));
                            var persona = db.Persona.Where(p => p.Rut == rut).FirstOrDefault();
                            if (persona != null)
                            {
                                if (HelperHCMS.ComprobarPassword(model.Password, Convert.FromBase64String(persona.Sal), Convert.FromBase64String(persona.Password)))
                                {
                                    FormsAuthentication.SetAuthCookie(rut.ToString(), false);
                                    Session["NombreCompleto"] = persona.Nombres.Split(' ')[0];
                                    var userRoles = persona.Rol_Persona.Select(r => r.Rol.Nombre).ToList();
                                    string userData = string.Empty;
                                    foreach (string rol in userRoles) userData += rol + ",";
                                    userData = userData.Remove(userData.Length - 1);
                                    var authTicket = new FormsAuthenticationTicket(1, rut.ToString(), DateTime.Now, DateTime.Now.AddMinutes(30), true, userData);

                                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                                    HttpContext.Response.Cookies.Add(authCookie);

                                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                                       && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                                    {
                                        return Redirect(returnUrl);
                                    }
                                    else
                                    {
                                        return RedirectToAction("RedirectToDefault");
                                    }
                                }
                                else ModelState.AddModelError("NoMatches", "RUT y/o contraseña ingresados no corresponden");
                            }
                            else ModelState.AddModelError("NoMatches", "RUT y/o contraseña ingresados no corresponden");
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError("Error_LogOn_Unhandled", "Ha ocurrido un error, por favor reintentar");
                        }
                    }
                }
                else ModelState.AddModelError("Rut_Invalido", "El RUT ingresado no es válido");
            }
            else ModelState.AddModelError("ModelInvalid", "Debes completar los campos");
            return View(model);
        }

        public ActionResult RedirectToDefault()
        {
            string[] roles = Roles.GetRolesForUser();
            if (roles.Contains("Administrativo"))
            {
                return RedirectToAction("Index", "Intranet");
            }
            else if (roles.Contains("Médico"))
            {
                return RedirectToAction("Index", "Medico");
            }
            else if (roles.Contains("Administrador"))
            {
                return RedirectToAction("Index", "Administrador");
            }
            else if (roles.Contains("Master"))
            {
                return RedirectToAction("Index", "Master");
            }
            else if (roles.Contains("Paciente"))
            {
                return RedirectToAction("Index", "PortalPacientes");
            }
            else
            {
                return RedirectToAction("Index", "HCMS");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrativo")]
        public ActionResult VerListadoPacientes()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrativo")]
        public ActionResult CrearPaciente()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrativo")]
        public ActionResult ModificarPaciente()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrativo")]
        public ActionResult ActualizarDatos()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrativo")]
        public ActionResult ReservarHora()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrativo")]
        public ActionResult ReservarHoraExamen()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrativo")]
        public ActionResult AnularHora()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrativo")]
        public ActionResult AnularHoraExamen()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrativo")]
        public ActionResult RegistroAtencion()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrativo")]
        public ActionResult ActualizarAtencion()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrativo")]
        public ActionResult ConsultarExamen()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrativo")]
        public ActionResult IngresarResultadoExamen()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrativo")]
        public ActionResult TrasladarPacienteCM()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult SinResultado(string message)
        {
            ViewBag.Message = message;
            return PartialView();
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