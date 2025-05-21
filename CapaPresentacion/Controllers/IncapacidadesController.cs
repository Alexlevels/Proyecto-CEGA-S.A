using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaPresentacion.Models;
using Microsoft.AspNet.Identity;

namespace CapaPresentacion.Controllers
{
    [Authorize]
    public class IncapacidadesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Empleado: Solicitar incapacidad
        [Authorize(Roles = "Empleado")]
        public ActionResult Solicitar()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Empleado")]
        public ActionResult Solicitar(Incapacidad model, HttpPostedFileBase archivo)
        {
            if (ModelState.IsValid)
            {
                if (archivo != null && archivo.ContentLength > 0)
                {
                    var extension = Path.GetExtension(archivo.FileName).ToLower();
                    if (extension != ".pdf" && extension != ".jpg" && extension != ".png")
                    {
                        ModelState.AddModelError("", "Solo se permiten archivos PDF, JPG o PNG.");
                        return View(model);
                    }

                    string nombreArchivo = Guid.NewGuid() + extension;
                    string ruta = Server.MapPath("~/ArchivosIncapacidades/" + nombreArchivo);
                    archivo.SaveAs(ruta);
                    model.ArchivoComprobante = nombreArchivo;
                }

                model.ApplicationUserId = User.Identity.GetUserId();
                model.Estado = "Pendiente";
                model.Fecha = DateTime.Now;

                db.Incapacidades.Add(model);
                db.SaveChanges();
                return RedirectToAction("MisIncapacidades");
            }
            return View(model);
        }

        [Authorize(Roles = "Empleado")]
        public ActionResult MisIncapacidades()
        {
            var id = User.Identity.GetUserId();
            var datos = db.Incapacidades.Where(i => i.ApplicationUserId == id).ToList();
            return View(datos);
        }

        // Admin: Revisar
        [Authorize(Roles = "Administrador")]
        public ActionResult Revisar()
        {
            var todas = db.Incapacidades.Include("Usuario").ToList();
            return View(todas);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult CambiarEstado(int id, string estado)
        {
            var i = db.Incapacidades.Find(id);
            if (i != null)
            {
                i.Estado = estado;
                db.SaveChanges();
            }
            return RedirectToAction("Revisar");
        }
    }
}
