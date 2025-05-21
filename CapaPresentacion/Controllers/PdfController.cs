using CapaPresentacion.Models;
using System.Web.Mvc;
using System.Web;
using System.Linq;
using System;

public class PdfController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    // Carpeta
    public ActionResult Carpetas() => View(db.Carpetas.ToList());

    public ActionResult CrearCarpeta() => View();

    [HttpPost]
    public ActionResult CrearCarpeta(CarpetaDocumentacion model)
    {
        if (ModelState.IsValid)
        {
            db.Carpetas.Add(model);
            db.SaveChanges();
            return RedirectToAction("Carpetas");
        }
        return View(model);
    }

    // Documentos
    public ActionResult Documentos(int carpetaId)
    {
        var documentos = db.Documentos.Where(d => d.CarpetaId == carpetaId).ToList();
        ViewBag.CarpetaId = carpetaId;
        return View(documentos);
    }

    public ActionResult SubirPdf(int carpetaId)
    {
        ViewBag.CarpetaId = carpetaId;
        return View();
    }

    [HttpPost]
    public ActionResult SubirPdf(HttpPostedFileBase file, int carpetaId)
    {
        if (file != null && file.ContentLength > 0)
        {
            var nombreArchivo = System.IO.Path.GetFileName(file.FileName);
            var ruta = System.IO.Path.Combine(Server.MapPath("~/ArchivosPDF"), nombreArchivo);
            file.SaveAs(ruta);

            db.Documentos.Add(new DocumentoPdf
            {
                Nombre = nombreArchivo,
                RutaArchivo = "/ArchivosPDF/" + nombreArchivo,
                CarpetaId = carpetaId
            });

            db.SaveChanges();
            return RedirectToAction("Documentos", new { carpetaId });
        }

        ViewBag.CarpetaId = carpetaId;
        return View();
    }

    // Comentarios
    public ActionResult Comentarios(int documentoId)
    {
        var comentarios = db.Comentarios.Where(c => c.DocumentoPdfId == documentoId).ToList();
        ViewBag.DocumentoId = documentoId;
        return View(comentarios);
    }

    [HttpPost]
    public ActionResult AgregarComentario(int documentoId, string texto)
    {
        if (!string.IsNullOrWhiteSpace(texto))
        {
            db.Comentarios.Add(new ComentarioDocumento
            {
                DocumentoPdfId = documentoId,
                Texto = texto,
                Fecha = DateTime.Now
            });
            db.SaveChanges();
        }
        return RedirectToAction("Comentarios", new { documentoId });
    }

    public FileResult Descargar(int id)
    {
        var doc = db.Documentos.Find(id);
        if (doc != null)
        {
            return File(Server.MapPath(doc.RutaArchivo), "application/pdf", doc.Nombre);
        }
        return null;
    }
}
