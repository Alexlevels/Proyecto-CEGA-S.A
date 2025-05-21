using CapaPresentacion.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace CapaPresentacion.Models
{ 
public class ComentarioDocumento
{
    public int Id { get; set; }

    [Required]
    public string Texto { get; set; }

    public DateTime Fecha { get; set; }  

    public int DocumentoPdfId { get; set; }

    public virtual DocumentoPdf DocumentoPdf { get; set; }
}
}