using System.ComponentModel.DataAnnotations;

namespace CapaPresentacion.Models
{
    public class Subtarea
    {
        public int Id { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string Estado { get; set; }

        // Clave foránea
        public int TareaId { get; set; }
        public virtual Tarea Tarea { get; set; }
    }
}
