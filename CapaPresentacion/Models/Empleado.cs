using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaPresentacion.Models
{
    public class Empleado
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre completo")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Correo electrónico")]
        [EmailAddress]
        public string Correo { get; set; }

        [Display(Name = "Puesto actual")]
        public string Puesto { get; set; }

        [Display(Name = "Salario actual")]
        [DataType(DataType.Currency)]
        public decimal Salario { get; set; }

        public DateTime FechaContratacion { get; set; }

        // Relación con otras entidades
        public virtual ICollection<Vacacion> Vacaciones { get; set; }
        public virtual ICollection<Incapacidad> Incapacidades { get; set; }
    }
}
