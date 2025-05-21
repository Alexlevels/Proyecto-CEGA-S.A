using System.ComponentModel.DataAnnotations;

namespace CapaPresentacion.Models
{
    public class CorreoMarketing
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        public bool Verificado { get; set; }
    }
}
