using CapaPresentacion.Models;
using System.ComponentModel.DataAnnotations;
using System;
namespace CapaPresentacion.Models
{
    public class Tarea
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string Estado { get; set; } // "Pendiente", "En Progreso", "Completada", etc.

        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public int ProyectoId { get; set; }
        public virtual Proyecto Proyecto { get; set; }
    }
}