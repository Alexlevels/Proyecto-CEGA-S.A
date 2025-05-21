using CapaPresentacion.Models;
using System.Data.Entity;
using System.Web.Mvc;
using System.Linq;

public class TareaController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult Index(int proyectoId)
    {
        var tareas = db.Tareas.Where(t => t.ProyectoId == proyectoId).ToList();
        ViewBag.ProyectoId = proyectoId;
        return View(tareas);
    }

    public ActionResult Crear(int proyectoId)
    {
        var nuevaTarea = new Tarea
        {
            ProyectoId = proyectoId
        };

        return View(nuevaTarea);
    }


    [HttpPost]
    public ActionResult Crear(Tarea tarea)
    {
        if (ModelState.IsValid)
        {
            db.Tareas.Add(tarea);
            db.SaveChanges();
            return RedirectToAction("Index", new { proyectoId = tarea.ProyectoId });
        }
        return View(tarea);
    }

    public ActionResult Editar(int id)
    {
        var tarea = db.Tareas.Find(id);
        return View(tarea);
    }

    [HttpPost]
    public ActionResult Editar(Tarea tarea)
    {
        if (ModelState.IsValid)
        {
            db.Entry(tarea).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { proyectoId = tarea.ProyectoId });
        }
        return View(tarea);
    }

    public ActionResult Eliminar(int id)
    {
        var tarea = db.Tareas.Find(id);
        int proyectoId = tarea.ProyectoId;
        db.Tareas.Remove(tarea);
        db.SaveChanges();
        return RedirectToAction("Index", new { proyectoId });
    }
}
