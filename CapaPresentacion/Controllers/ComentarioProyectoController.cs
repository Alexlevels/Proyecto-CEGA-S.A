using System.Linq;
using System.Web.Mvc;
using CapaPresentacion.Models;

namespace CapaPresentacion.Controllers
{
    [Authorize(Roles = "Administrador,Empleado")]
    public class ComentarioProyectoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int proyectoId)
        {
            var comentarios = db.ComentariosProyecto.Where(c => c.ProyectoId == proyectoId).ToList();
            ViewBag.ProyectoId = proyectoId;
            return View(comentarios);
        }

        [HttpPost]
        public ActionResult Agregar(int proyectoId, string texto)
        {
            if (!string.IsNullOrWhiteSpace(texto))
            {
                db.ComentariosProyecto.Add(new ComentarioProyecto
                {
                    ProyectoId = proyectoId,
                    Texto = texto,
                    Fecha = System.DateTime.Now
                });
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { proyectoId });
        }

        public ActionResult Eliminar(int id)
        {
            var comentario = db.ComentariosProyecto.Find(id);
            int proyectoId = comentario.ProyectoId;
            db.ComentariosProyecto.Remove(comentario);
            db.SaveChanges();
            return RedirectToAction("Index", new { proyectoId });
        }
    }
}
