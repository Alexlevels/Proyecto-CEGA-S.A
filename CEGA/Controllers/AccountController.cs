using CEGA.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CEGA.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // LOGIN
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email!);
            if (user == null)
            {
                ModelState.AddModelError("", "Credenciales no registradas");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password!, false, true);

            if (result.Succeeded)
            {
                TempData["Mensaje"] = "Login exitoso";
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", result.IsLockedOut ? "Cuenta bloqueada por intentos fallidos" : "Credenciales incorrectas");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        // RECUPERACIÓN DE CONTRASEÑA
        [HttpGet]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email!);
            if (user == null)
            {
                ModelState.AddModelError("", "Correo no encontrado en el sistema");
                return View(model);
            }

            TempData["Mensaje"] = "Se ha enviado un correo con las instrucciones para restablecer la contraseña (simulado).";
            return RedirectToAction("Login");
        }

        // EDITAR PERFIL
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            return View(new EditProfileViewModel
            {
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Email = user.Email,
                Telefono = user.PhoneNumber
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            user.Nombre = model.Nombre;
            user.Apellido = model.Apellido;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.PhoneNumber = model.Telefono;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["Mensaje"] = "Perfil actualizado correctamente.";
                return RedirectToAction("EditProfile");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // GESTIÓN DE USUARIOS (vista general)
        [HttpGet]
        public IActionResult GestionUsuarios()
        {
            return View(); // Mostrar botones: Filtrar, Eliminar, Asignar rol, etc.
        }

        // FILTRAR USUARIOS POR TEXTO Y ROL
        [HttpGet]
        public async Task<IActionResult> ListaUsuarios(string? filtroTexto, string? rolSeleccionado)
        {
            var usuarios = _userManager.Users.ToList();
            var rolesDisponibles = new List<string> { "Admin", "Empleado", "Cliente" };

            if (!string.IsNullOrWhiteSpace(filtroTexto))
            {
                filtroTexto = filtroTexto.ToLower();
                usuarios = usuarios.Where(u =>
                    (u.Nombre + " " + u.Apellido).ToLower().Contains(filtroTexto)
                    || (u.Email ?? "").ToLower().Contains(filtroTexto)
                    || (u.UserName ?? "").ToLower().Contains(filtroTexto)
                ).ToList();
            }

            if (!string.IsNullOrWhiteSpace(rolSeleccionado))
            {
                var usuariosConRol = new List<ApplicationUser>();
                foreach (var usuario in usuarios)
                {
                    var roles = await _userManager.GetRolesAsync(usuario);
                    if (roles.Contains(rolSeleccionado))
                        usuariosConRol.Add(usuario);
                }
                usuarios = usuariosConRol;
            }

            var modelo = new BuscarUsuarioFiltroViewModel
            {
                FiltroTexto = filtroTexto,
                RolSeleccionado = rolSeleccionado,
                UsuariosFiltrados = usuarios,
                RolesDisponibles = rolesDisponibles
            };

            return View(modelo);
        }

        // ELIMINAR USUARIO
        [HttpPost]
        public async Task<IActionResult> EliminarUsuario(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                TempData["Error"] = "El usuario no fue encontrado.";
                return RedirectToAction("ListaUsuarios");
            }

            var resultado = await _userManager.DeleteAsync(usuario);
            TempData[resultado.Succeeded ? "Mensaje" : "Error"] = resultado.Succeeded
                ? "Usuario eliminado exitosamente."
                : "Error al eliminar el usuario.";

            return RedirectToAction("ListaUsuarios");
        }

        // ELIMINAR CUENTA PERSONAL
        [HttpGet]
        public IActionResult EliminarCuenta() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarCuentaConfirmado()
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return RedirectToAction("Login");

            await _signInManager.SignOutAsync();
            var resultado = await _userManager.DeleteAsync(usuario);

            if (resultado.Succeeded)
            {
                TempData["Mensaje"] = "Tu cuenta ha sido eliminada exitosamente.";
                return RedirectToAction("Login");
            }

            TempData["Error"] = "No se pudo eliminar la cuenta.";
            return RedirectToAction("EliminarCuenta");
        }

        // ASIGNAR ROL
        [HttpGet]
        public async Task<IActionResult> AsignarRol(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null) return RedirectToAction("ListaUsuarios");

            var rolesActuales = await _userManager.GetRolesAsync(usuario);
            var rolActual = rolesActuales.FirstOrDefault();

            var modelo = new AsignarRolViewModel
            {
                UsuarioId = usuario.Id,
                Email = usuario.Email!,
                RolSeleccionado = rolActual,
                RolesDisponibles = new List<string> { "Admin", "Empleado", "Cliente" },
                SubRolesDisponibles = new List<string> { "Arquitecto", "Ingeniero", "Dibujante" }
            };

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> AsignarRol(AsignarRolViewModel modelo)
        {
            modelo.RolesDisponibles = new List<string> { "Admin", "Empleado", "Cliente" };
            modelo.SubRolesDisponibles = new List<string> { "Arquitecto", "Ingeniero", "Dibujante" };

            if (!ModelState.IsValid) return View(modelo);

            var usuario = await _userManager.FindByIdAsync(modelo.UsuarioId!);
            if (usuario == null)
            {
                TempData["Error"] = "Usuario no encontrado.";
                return RedirectToAction("ListaUsuarios");
            }

            var rolesActuales = await _userManager.GetRolesAsync(usuario);
            await _userManager.RemoveFromRolesAsync(usuario, rolesActuales);
            await _userManager.AddToRoleAsync(usuario, modelo.RolSeleccionado!);

            if (modelo.RolSeleccionado == "Empleado" && !string.IsNullOrWhiteSpace(modelo.SubRol))
            {
                usuario.SubRol = modelo.SubRol;
                await _userManager.UpdateAsync(usuario);
            }

            TempData["Mensaje"] = $"Rol actualizado a '{modelo.RolSeleccionado}' para el usuario {usuario.Email}.";
            return RedirectToAction("ListaUsuarios");
        }
    }
}
