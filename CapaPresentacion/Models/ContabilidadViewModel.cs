using System;
using System.Collections.Generic;

namespace CapaPresentacion.Models
{
    public class MovimientoViewModel
    {
        public string Tipo { get; set; } // "Ingreso" o "Egreso"
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
    }

    public class ContabilidadViewModel
    {
        public List<MovimientoViewModel> Movimientos { get; set; } = new List<MovimientoViewModel>();
    }
}
