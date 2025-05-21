using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaPresentacion.Models
{
    public class ClienteCampana
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Display(Name = "Campaña Asignada")]
        public int? CampanaMarketingId { get; set; }

        [ForeignKey("CampanaMarketingId")]
        public virtual CampanaMarketing CampanaMarketing { get; set; }
    }
}
