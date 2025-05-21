using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CapaPresentacion.Models;

namespace CapaPresentacion
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles); // <---

            // Crear roles y usuario admin por defecto
            CrearRolesYUsuarios();
        }

        private void CrearRolesYUsuarios()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Crear rol Administrador si no existe
            if (!roleManager.RoleExists("Administrador"))
            {
                var rolAdmin = new IdentityRole("Administrador");
                roleManager.Create(rolAdmin);
            }

            // Crear rol Empleado si no existe
            if (!roleManager.RoleExists("Empleado"))
            {
                var rolEmpleado = new IdentityRole("Empleado");
                roleManager.Create(rolEmpleado);
            }

            // Crear un usuario administrador por defecto
            if (userManager.FindByEmail("admin@admin.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com"
                };

                string password = "Admin123!";
                var result = userManager.Create(user, password);

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrador");
                }
            }
        }
    }
}
