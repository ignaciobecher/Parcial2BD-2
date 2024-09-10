using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication11.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Models;

namespace WebApplication11.Controllers
{
    public class AccountController : Controller
    {
        private readonly WebApplication11Context _context;

        // Inyectamos el contexto de base de datos en el constructor
        public AccountController(WebApplication11Context context)
        {
            _context = context;
        }

        // Página de inicio de sesión
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Manejo del inicio de sesión (POST)
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Validamos si el usuario existe en la base de datos
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Crear los claims (información del usuario autenticado)
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

                // Crear la identidad del usuario
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Crear el principal del usuario
                var principal = new ClaimsPrincipal(identity);

                // Iniciar la sesión del usuario (autenticación)
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            // Si las credenciales no son correctas
            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View();
        }

        // Cerrar sesión
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // Página de registro
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Manejo del registro de usuario (POST)
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string confirmPassword)
        {
            // Validar si el nombre de usuario ya existe
            if (_context.Users.Any(u => u.Username == username))
            {
                ViewBag.Error = "El nombre de usuario ya existe";
                return View();
            }

            // Validar que las contraseñas coincidan
            if (password != confirmPassword)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            // Crear el nuevo usuario
            var newUser = new User { Username = username, Password = password };
            _context.Users.Add(newUser);  // Añadir el nuevo usuario a la base de datos
            await _context.SaveChangesAsync();  // Guardar cambios

            // Redirigir al inicio de sesión después del registro exitoso
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}