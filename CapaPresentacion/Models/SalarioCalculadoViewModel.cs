namespace CapaPresentacion.Models
{
    public class SalarioCalculadoViewModel
    {
        public string NombreEmpleado { get; set; }
        public decimal SalarioBase { get; set; }
        public int DiasNoTrabajados { get; set; }
        public decimal Descuento { get; set; }
        public decimal SalarioNeto { get; set; }
    }
}
