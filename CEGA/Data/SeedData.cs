using CEGA.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CEGA.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var logger = serviceProvider.GetRequiredService<ILogger<SeedData>>();

            // Crear rol administrador si no existe
            string[] roles = { "Admin", "Empleado", "Cliente" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!result.Succeeded)
                    {
                        logger.LogError("Error al crear el rol {Role}: {Errors}", role, string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }
            }

            // Crear usuario administrador si no existe
            var adminEmail = "admin@cega.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Nombre = "Admin",
                    Apellido = "Principal",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
