using Healthcare_MS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Healthcare_MS.Controllers
{
    public class PortalPacientesController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if(User.Identity.IsAuthenticated) return RedirectToAction("Index");
            ViewBag.Message = string.Empty;
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
        public ActionResult Login(PPLoginModel model, string returnUrl)
        {
            ViewBag.Message = string.Empty;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "HCMS");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            if (!User.IsInRole("Paciente")) return RedirectToAction("RedirectToDefault");
            HCMSEntities db = new HCMSEntities();
            int rut = Convert.ToInt32(User.Identity.Name);
            var paciente = db.Paciente.Where(p => p.Persona.Rut == rut).FirstOrDefault();
            return View(paciente);
        }

        [HttpGet]
        [Authorize(Roles = "Paciente")]
        public ActionResult ActualizarDatos()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            int rut = Convert.ToInt32(User.Identity.Name);
            HCMSEntities db = new HCMSEntities();
            PPActualizarDatosModel act = new PPActualizarDatosModel();
            var persona = db.Persona.Where(p => p.Rut == rut).FirstOrDefault();
            act.editarDatosPersonales = new PPEditarDatosPersonalesModel()
            {
                Rut = persona.Rut,
                RutCompleto = persona.RutCompleto,
                Nombres = persona.Nombres,
                Apellidos = persona.Apellidos,
                FechaNacimiento = persona.FechaNacimiento ?? DateTime.Now,
                Region = persona.Comuna.Region.Nombre,
                Comuna = persona.Comuna.Nombre,
                Direccion = persona.Direccion
            };
            act.editarDatosContacto = new PPEditarDatosContactoModel()
            {
                Rut = persona.Rut,
                Telefono = persona.Telefono,
                Celular = persona.Celular,
                Email = persona.Email
            };
            act.editarContacto = new PPEditarContactoModel()
            {
                Rut = persona.Rut,
                NombreContacto = persona.Paciente.First().NombresContacto,
                ApellidoContacto = persona.Paciente.First().ApellidosContacto,
                Parentesco = persona.Paciente.First().ParentescoContacto,
                Telefono = persona.Paciente.First().TelefonoContacto,
                Celular = persona.Paciente.First().CelularContacto,
                Correo = persona.Paciente.First().EmailContacto
            };
            act.editarPassword = new PPEditarPasswordModel()
            {
                Rut = persona.Rut
            };
            return View(act);
        }

        [HttpPost, ActionName("EditarDatosPersonales")]
        [Authorize(Roles = "Paciente")]
        public ActionResult EditarDatosPersonales(PPEditarDatosPersonalesModel model)
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            string message = string.Empty;
            if(ModelState.IsValid)
            {
                using (HCMSEntities db = new HCMSEntities())
                {
                    var persona = db.Persona.Where(p => p.Rut == model.Rut).FirstOrDefault();
                    if (persona != null)
                    {
                        persona.Nombres = model.Nombres;
                        persona.Apellidos = model.Apellidos;
                        persona.FechaNacimiento = model.FechaNacimiento;
                        db.SaveChanges();
                        return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Éxito", message = "Tus datos han sido modificados" });
                    }
                    else return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Ha ocurrido un error", message = "Ha ocurrido un error inesperado" });
                }
            }
            else return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Ha ocurrido un error", message = "Revisa que todos los campos estén completos" });
        }

        [HttpPost, ActionName("EditarDatosContacto")]
        [Authorize(Roles = "Paciente")]
        public ActionResult EditarDatosContacto(PPEditarDatosContactoModel model)
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                using (HCMSEntities db = new HCMSEntities())
                {
                    var persona = db.Persona.Where(p => p.Rut == model.Rut).FirstOrDefault();
                    if (persona != null)
                    {
                        persona.Telefono = model.Telefono;
                        persona.Celular = model.Celular;
                        persona.Email = model.Email;
                        db.SaveChanges();
                        return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Éxito", message = "Tus datos han sido modificados" });
                    }
                    else return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Ha ocurrido un error", message = "Ha ocurrido un error inesperado" });
                }
            }
            else return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Ha ocurrido un error", message = "Debes ingresar un correo" });
        }

        [HttpPost, ActionName("ActualizarPassword")]
        [Authorize(Roles = "Paciente")]
        public ActionResult ActualizarPassword(PPEditarPasswordModel model)
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            if (ModelState.IsValid)
            {
                using (HCMSEntities db = new HCMSEntities())
                {
                    var persona = db.Persona.Where(p => p.Rut == model.Rut).FirstOrDefault();
                    if (persona != null)
                    {
                        if (HelperHCMS.ComprobarPassword(model.OldPassword, Convert.FromBase64String(persona.Sal), Convert.FromBase64String(persona.Password)))
                        {
                            if (model.NewPassword == model.RepeatPassword)
                            {
                                var Sal = HelperHCMS.GenerarSal();
                                var Pass = HelperHCMS.CalcularHash(model.NewPassword, Sal);
                                persona.Sal = Convert.ToBase64String(Sal);
                                persona.Password = Convert.ToBase64String(Pass);
                                db.SaveChanges();
                                return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Éxito", message = "Tus contraseña ha cambiado" });
                            }
                            else return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Error", message = "Las contraseñas no coinciden" });
                        }
                        else return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Error", message = "Tu contraseña actual no coincide con la ingresada" });
                    }
                    else return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Ha ocurrido un error", message = "Ha ocurrido un error inesperado" });
                }
            }
            else return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Error",  message = "Verifica que todos los datos han sido ingresados" });
        }

        [HttpPost, ActionName("EditarContacto")]
        [Authorize(Roles = "Paciente")]
        public ActionResult EditarContacto(PPEditarContactoModel model)
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            if (ModelState.IsValid)
            {
                using (HCMSEntities db = new HCMSEntities())
                {
                    var persona = db.Persona.Where(p => p.Rut == model.Rut).FirstOrDefault().Paciente.First();
                    if (persona != null)
                    {
                        persona.NombresContacto = model.NombreContacto;
                        persona.ApellidosContacto = model.ApellidoContacto;
                        persona.ParentescoContacto = model.Parentesco;
                        persona.TelefonoContacto = model.Telefono;
                        persona.CelularContacto = model.Celular;
                        persona.EmailContacto = model.Correo;
                        db.SaveChanges();
                        return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Éxito", message = "Los datos de tu contacto han sido modificados" });
                    }
                    else return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Ha ocurrido un error", message = "Ha ocurrido un error inesperado" });
                }
            }
            else return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Ha ocurrido un error", message = "Ha ocurrido un error inesperado" });
        }

        [HttpGet]
        [Authorize(Roles = "Paciente")]
        public ActionResult ReservarHora()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Paciente")]
        public ActionResult ConsultarHora()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
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
                if (hora == null) return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Error", message = "El código ingresado no es válido" });
                return PartialView("_BuscarHora", hora);
            }
            else return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Error", message = "Debes ingresar el código" });
        }

        [HttpGet]
        [Authorize(Roles = "Paciente")]
        public ActionResult ConsultarHoraExamen()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            ViewBag.Message = string.Empty;
            return View();
        }

        [HttpGet, ActionName("ResultadoHoraExamen")]
        [AllowAnonymous]
        public ActionResult _ResultadoHoraExamen(string Codigo)
        {
            ViewBag.Message = string.Empty;
            if (ModelState.IsValid)
            {
                Examen examen;
                HCMSEntities db = new HCMSEntities();
                AgendaExamen ag = db.AgendaExamen.Where(e => e.Codigo == Codigo).FirstOrDefault();
                if (ag != null)
                {
                    examen = db.Examen.Where(e => e.AgendaExamen.Codigo == Codigo).FirstOrDefault();
                    if (examen.Examen_TipoExamen.First().ResultadoExamen != null && examen.Examen_TipoExamen.First().ResultadoExamen.Count > 0) return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Aviso", message = "Este examen ya fue tomado, ¡Consulta si ya tiene sus resultados!" });
                    return PartialView("_ResultadoHoraExamen", examen);
                }
                else return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Error", message = "El código ingresado no es válido" });
            }
            else return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Error", message = "Debes ingresar el código de la hora" });
        }

        [HttpGet]
        [Authorize(Roles = "Paciente")]
        public ActionResult AnularHora()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult DetallesHoraAnular(PPAnularHoraModel model)
        {
            ViewBag.Message = string.Empty;
            if (ModelState.IsValid)
            {
                HoraMedica hora;
                HCMSEntities db = new HCMSEntities();
                hora = db.HoraMedica.Where(e => e.Codigo == model.Codigo)
                    .Where(e => e.Disponible == false)
                    .FirstOrDefault();
                if (hora == null) return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Error", message = "El código ingresado no es válido" });
                else if (hora.EstadoHoraMedica.Id == 3) return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Error", message = "La hora fue rechazada" });
                else if (hora.EstadoHoraMedica.Id == 4) return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Error", message = "La hora ya ha sido anulada" });
                else if (hora.FechaHoraCargada < DateTime.Now) return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Error", message = "No puedes anular una hora médica luego de que pasó la fecha destinada de atención" });
                return PartialView("DetallesHoraAnular", hora);
            }
            else return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Error", message = "Debes ingresar el código de la hora" });
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
                    return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Éxito", message = "La hora ha sido anulada exitosamente" });
                }
            }
            else ViewBag.Message = "Han ocurrido errores, por favor intenta nuevamente";
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Paciente")]
        public ActionResult AnularHoraExamen()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
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
                if (examen == null) return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Error", message = "El código ingresado no es válido" });
                else if (examen.ExamenAnulado.Any()) return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Error", message = "El examen ya fue anulado" });
                else if (examen.FechaAgenda < DateTime.Now) return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Información", message = "No es posible anular un examen después de la fecha asignada" });
                else if (examen.Examen.First().HoraExamen != null) return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Información", message = "No es posible anular un examen que ya fue realizado" });
                else if (examen.Examen.First().Examen_TipoExamen.First().ResultadoExamen != null) return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Información", message = "Este examen ya fue tomado, ¡Consulta si ya tiene sus resultados!" });
                return PartialView("DetallesExamenAnular", examen);
            }
            else return RedirectToAction("SinResultado", "PortalPacientes", new { message = "Debes ingresar el código de la hora" });
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
                    return RedirectToAction("SinResultado", "PortalPacientes", new { title = "Éxito", message = "La hora para examen ha sido anulada exitosamente" });
                }
            }
            else ViewBag.Message = "Han ocurrido errores, por favor revisa";
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Paciente")]
        public ActionResult ConsultarExamen()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
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
        [Authorize(Roles = "Paciente")]
        public ActionResult RegistroAtencion()
        {
            if (Session["NombreCompleto"] == null) Session["NombreCompleto"] = getUserName();
            return View();
        }

        public ActionResult RedirectToDefault()
        {
            string[] roles = Roles.GetRolesForUser();
            if (roles.Contains("Paciente")) return RedirectToAction("Index", "PortalPacientes");
            else if (roles.Contains("Administrativo")) return RedirectToAction("Index", "Intranet");
            else if (roles.Contains("Médico")) return RedirectToAction("Index", "Medico");
            else if (roles.Contains("Administrador")) return RedirectToAction("Index", "Administrador");
            else if (roles.Contains("Master")) return RedirectToAction("Index", "Master");
            else return RedirectToAction("Index", "HCMS");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SinResultado(string title, string message)
        {
            ViewBag.Title = title;
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