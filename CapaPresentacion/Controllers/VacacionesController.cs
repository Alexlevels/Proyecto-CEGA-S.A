using System;
using System.Linq;
using System.Web.Mvc;
using CapaPresentacion.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity; // ← Para Include()



public class VacacionesController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    // Empleado: Solicitar vacaciones
    [Authorize(Roles = "Empleado")]
    public ActionResult Solicitar()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Empleado")]
    public ActionResult Solicitar(Vacacion model)
    {
        if (ModelState.IsValid)
        {
            model.ApplicationUserId = User.Identity.GetUserId();
            model.Estado = "Pendiente";
            db.Vacaciones.Add(model);
            db.SaveChanges();
            return RedirectToAction("MisSolicitudes");
        }
        return View(model);
    }

    // Empleado: Ver sus propias solicitudes
    [Authorize(Roles = "Empleado")]
    public ActionResult MisSolicitudes()
    {
        var userId = User.Identity.GetUserId();
        var solicitudes = db.Vacaciones.Where(v => v.ApplicationUserId == userId).ToList();
        return View(solicitudes);
    }

    // Admin: Ver todas las solicitudes
    [Authorize(Roles = "Administrador")]
    public ActionResult Revisar()
    {
        var todas = db.Vacaciones.Include("Usuario").ToList();
        return View(todas);
    }

    // Admin: Aprobar o rechazar
    [Authorize(Roles = "Administrador")]
    public ActionResult CambiarEstado(int id, string estado)
    {
        var solicitud = db.Vacaciones.Find(id);
        if (solicitud != null)
        {
            solicitud.Estado = estado;
            db.SaveChanges();
        }
        return RedirectToAction("Revisar");
    }
}
