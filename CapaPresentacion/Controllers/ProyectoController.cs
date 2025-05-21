using System.Linq;
using System.Web.Mvc;
using CapaPresentacion.Models;

namespace CapaPresentacion.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ProyectoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var proyectos = db.Proyectos.ToList();
            var tareas = db.Tareas.ToList();

            var modelo = proyectos.Select(p =>
            {
                var tareasProyecto = tareas.Where(t => t.ProyectoId == p.Id).ToList();
                var total = tareasProyecto.Count;
                var completadas = tareasProyecto.Count(t => t.Estado == "Completada");
                var porcentaje = total > 0 ? (int)((completadas / (double)total) * 100) : 0;

                return new ProyectoConAvanceViewModel
                {
                    Proyecto = p,
                    Porcentaje = porcentaje
                };
            }).ToList();

            return View(modelo);
        }


        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                db.Proyectos.Add(proyecto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proyecto);
        }

        public ActionResult Editar(int id)
        {
            var proyecto = db.Proyectos.Find(id);
            if (proyecto == null) return HttpNotFound();
            return View(proyecto);
        }

        [HttpPost]
        public ActionResult Editar(Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proyecto).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proyecto);
        }

        public ActionResult Eliminar(int id)
        {
            var proyecto = db.Proyectos.Find(id);
            if (proyecto == null) return HttpNotFound();
            db.Proyectos.Remove(proyecto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
