using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaPresentacion.Models
{
    public class Incapacidad
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }

        public DateTime Fecha { get; set; }

        [Display(Name = "Comprobante (PDF, JPG, PNG)")]
        public string ArchivoComprobante { get; set; }

        public string Estado { get; set; } // Pendiente, Aceptada, Rechazada
    }

}
