using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ReporteriaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
