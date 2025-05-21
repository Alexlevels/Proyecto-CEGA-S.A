using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaPresentacion.Models
{
    public class ComentarioProyecto
    {
        public int Id { get; set; }

        [Required]
        public int ProyectoId { get; set; }

        [ForeignKey("ProyectoId")]
        public virtual Proyecto Proyecto { get; set; }

        [Required]
        public string Texto { get; set; }

        public DateTime Fecha { get; set; }
    }
}
