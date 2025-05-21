using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaPresentacion.Models
{
    public class CarpetaDocumentacion
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public virtual ICollection<DocumentoPdf> Documentos { get; set; }
    }
}
