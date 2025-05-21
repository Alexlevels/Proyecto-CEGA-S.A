using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaPresentacion.Models
{
    public class DocumentoPdf
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string RutaArchivo { get; set; }

        public int CarpetaId { get; set; }

        public virtual CarpetaDocumentacion Carpeta { get; set; }

        public virtual ICollection<ComentarioDocumento> Comentarios { get; set; }
    }
}
