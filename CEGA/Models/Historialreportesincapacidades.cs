using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaReportesIncapacidades.Models
{

	public class HistorialModificacion
	{
		public int Id { get; set; }

		[Required]
		public int IdIncapacidad { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "El tipo de modificación no puede exceder 50 caracteres")]
		public string TipoModificacion { get; set; }

		[Required]
		[StringLength(1000, ErrorMessage = "La descripción del cambio no puede exceder 1000 caracteres")]
		public string DescripcionCambio { get; set; }

		[Required]
		public DateTime FechaModificacion { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder 50 caracteres")]
		public string Usuario { get; set; }

		[StringLength(500, ErrorMessage = "Los datos anteriores no pueden exceder 500 caracteres")]
		public string DatosAnteriores { get; set; } 

		[StringLength(500, ErrorMessage = "Los datos nuevos no pueden exceder 500 caracteres")]
		public string DatosNuevos { get; set; } 


		public virtual Incapacidad Incapacidad { get; set; }
	}
}