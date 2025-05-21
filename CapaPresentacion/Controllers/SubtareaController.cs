using System.Linq;
using System.Web.Mvc;
using CapaPresentacion.Models;

namespace CapaPresentacion.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class SubtareaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int tareaId)
        {
            ViewBag.TareaId = tareaId;

            var tarea = db.Tareas.Find(tareaId);
            ViewBag.ProyectoId = tarea?.ProyectoId ?? 0;

            var subtareas = db.Subtareas.Where(s => s.TareaId == tareaId).ToList();
            return View(subtareas);
        }


        public ActionResult Crear(int tareaId)
        {
            var nuevaSubtarea = new Subtarea
            {
                TareaId = tareaId
            };

            return View(nuevaSubtarea);
        }


        [HttpPost]
        public ActionResult Crear(Subtarea model)
        {
            if (ModelState.IsValid)
            {
                db.Subtareas.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", new { tareaId = model.TareaId });
            }
            ViewBag.TareaId = model.TareaId;
            return View(model);
        }

        public ActionResult Editar(int id)
        {
            var subtarea = db.Subtareas.Find(id);
            if (subtarea == null) return HttpNotFound();
            return View(subtarea);
        }

        [HttpPost]
        public ActionResult Editar(Subtarea model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { tareaId = model.TareaId });
            }
            return View(model);
        }

        public ActionResult Eliminar(int id)
        {
            var subtarea = db.Subtareas.Find(id);
            if (subtarea == null) return HttpNotFound();
            var tareaId = subtarea.TareaId;
            db.Subtareas.Remove(subtarea);
            db.SaveChanges();
            return RedirectToAction("Index", new { tareaId });
        }
    }
}
