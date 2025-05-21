using System.Linq;
using System.Web.Mvc;
using CapaPresentacion.Models;

namespace CapaPresentacion.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class MarketingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var campañas = db.CampanasMarketing.ToList();
            return View(campañas); // Enviamos la lista a la vista
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(CampanaMarketing campania)
        {
            if (ModelState.IsValid)
            {
                db.CampanasMarketing.Add(campania);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(campania);
        }

        public ActionResult Correos()
        {
            var correos = db.CorreosMarketing.ToList();
            return View(correos);
        }

        public ActionResult CrearCorreo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearCorreo(CorreoMarketing correo)
        {
            if (ModelState.IsValid)
            {
                db.CorreosMarketing.Add(correo);
                db.SaveChanges();
                return RedirectToAction("Correos");
            }
            return View(correo);
        }

        public ActionResult EliminarCorreo(int id)
        {
            var correo = db.CorreosMarketing.Find(id);
            if (correo != null)
            {
                db.CorreosMarketing.Remove(correo);
                db.SaveChanges();
            }
            return RedirectToAction("Correos");
        }

        public ActionResult Clientes()
        {
            var clientes = db.ClientesCampana.Include("CampanaMarketing").ToList();
            return View(clientes);
        }

        public ActionResult CrearCliente()
        {
            ViewBag.CampanaMarketingId = new SelectList(db.CampanasMarketing, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public ActionResult CrearCliente(ClienteCampana cliente)
        {
            if (ModelState.IsValid)
            {
                db.ClientesCampana.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Clientes");
            }

            ViewBag.CampanaMarketingId = new SelectList(db.CampanasMarketing, "Id", "Nombre", cliente.CampanaMarketingId);
            return View(cliente);
        }

        public ActionResult EliminarCliente(int id)
        {
            var cliente = db.ClientesCampana.Find(id);
            if (cliente != null)
            {
                db.ClientesCampana.Remove(cliente);
                db.SaveChanges();
            }
            return RedirectToAction("Clientes");
        }


    }


}
