using System.Linq;
using System.Web.Mvc;
using CapaPresentacion.Models;

namespace CapaPresentacion.Controllers
{
    [Authorize(Roles = "Administrador,Empleado")]
    public class ComentarioTareaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int tareaId)
        {
            var comentarios = db.ComentariosTarea.Where(c => c.TareaId == tareaId).ToList();
            ViewBag.TareaId = tareaId;
            return View(comentarios);
        }

        [HttpPost]
        public ActionResult Agregar(int tareaId, string texto)
        {
            if (!string.IsNullOrWhiteSpace(texto))
            {
                db.ComentariosTarea.Add(new ComentarioTarea
                {
                    TareaId = tareaId,
                    Texto = texto,
                    Fecha = System.DateTime.Now
                });
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { tareaId });
        }

        public ActionResult Eliminar(int id)
        {
            var comentario = db.ComentariosTarea.Find(id);
            int tareaId = comentario.TareaId;
            db.ComentariosTarea.Remove(comentario);
            db.SaveChanges();
            return RedirectToAction("Index", new { tareaId });
        }
    }
}
