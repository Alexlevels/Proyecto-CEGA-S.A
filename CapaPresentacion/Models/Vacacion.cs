using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaPresentacion.Models
{
    public class Vacacion
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }

        [Display(Name = "Fecha de inicio")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha de fin")]
        public DateTime FechaFin { get; set; }

        public string Estado { get; set; } // Pendiente, Aprobada, Rechazada
    }

}
