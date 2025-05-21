using System;
using System.ComponentModel.DataAnnotations;

namespace CapaPresentacion.Models
{
    public class CampanaMarketing
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha de Fin")]
        public DateTime FechaFin { get; set; }

        public string Estado { get; set; } // Ej: "Activa", "Finalizada"
    }
}
