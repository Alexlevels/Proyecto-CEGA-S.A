using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using CapaPresentacion.Models;
using Microsoft.AspNet.Identity;



namespace CapaPresentacion.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // VISTA PRINCIPAL DEL PANEL ADMIN
        public ActionResult Index()
        {
            return View();
        }

        // SECCIONES DEL PANEL DE ADMINISTRACIÓN

        [Authorize(Roles = "Administrador")]
        public ActionResult Empleados()
        {
            var vacaciones = db.Vacaciones.Include("Usuario").ToList();
            var incapacidades = db.Incapacidades.Include("Usuario").ToList();

            ViewBag.Vacaciones = vacaciones;
            ViewBag.Incapacidades = incapacidades;

            return View();
        }


        public ActionResult Reporteria()
        {
            return View();
        }

        public ActionResult Seguimiento()
        {
            return View();
        }

        public ActionResult Marketing()
        {
            return View();
        }

        public ActionResult Contabilidad()
        {
            return View();
        }

        public ActionResult PDF()
        {
            return View();
        }
    }
}
