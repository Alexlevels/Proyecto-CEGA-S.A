using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaPresentacion.Models
{
    public class Proyecto
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre del Proyecto")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha de Finalización")]
        public DateTime? FechaFin { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; } // En progreso, Finalizado, Cancelado
    }
}
