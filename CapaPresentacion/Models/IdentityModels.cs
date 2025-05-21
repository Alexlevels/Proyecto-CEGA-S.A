using System.Collections.Generic;
using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CapaPresentacion.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        // Campos adicionales del empleado
        public string NombreCompleto { get; set; }
        public string Puesto { get; set; }
        public decimal Salario { get; set; }
        public DateTime FechaContratacion { get; set; }

        public virtual ICollection<Vacacion> Vacaciones { get; set; }
        public virtual ICollection<Incapacidad> Incapacidades { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Vacacion> Vacaciones { get; set; }
        public DbSet<Incapacidad> Incapacidades { get; set; }
        public DbSet<CampanaMarketing> CampanasMarketing { get; set; }

        public DbSet<CorreoMarketing> CorreosMarketing { get; set; }
        public DbSet<ClienteCampana> ClientesCampana { get; set; }
        public DbSet<CarpetaDocumentacion> Carpetas { get; set; }
        public DbSet<DocumentoPdf> Documentos { get; set; }
        public DbSet<ComentarioDocumento> Comentarios { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }

        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Subtarea> Subtareas { get; set; }

        public DbSet<ComentarioProyecto> ComentariosProyecto { get; set; }
        public DbSet<ComentarioTarea> ComentariosTarea { get; set; }



    }

}