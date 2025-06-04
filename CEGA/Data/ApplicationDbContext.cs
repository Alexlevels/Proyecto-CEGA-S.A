using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CEGA.Models;


namespace CEGA.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CampMarketing> CampsMarketing { get; set; }
        public DbSet<PoolCorreo> PoolsCorreo { get; set; }
        public DbSet<ProgramacionDistribucion> ProgramacionesDistribucion { get; set; }
        public DbSet<DistribucionMarketing> DistribucionesMarketing { get; set; }
        public DbSet<ClienteMarketing> ClientesMarketing { get; set; }
        public DbSet<EmpleadoSalario> EmpleadosSalarios { get; set; }
        public DbSet<VacacionesEmpleado> VacacionesEmpleados { get; set; }
        public DbSet<PuestoEmpleado> PuestosEmpleado { get; set; }
        public DbSet<HistorialPuestos> HistorialPuestos { get; set; }
        public DbSet<IncapacidadEmpleado> IncapacidadesEmpleado { get; set; }

    }
}
