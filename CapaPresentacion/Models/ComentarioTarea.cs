using CapaPresentacion.Models;
using System;

namespace CapaPresentacion.Models
{
    public class ComentarioTarea
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public string ArchivoRuta { get; set; } // para PDF o imágenes
        public DateTime Fecha { get; set; }

        public int TareaId { get; set; }
        public virtual Tarea Tarea { get; set; }

        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}