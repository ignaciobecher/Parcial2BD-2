using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuración del DbContext
builder.Services.AddDbContext<WebApplication11Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApplication11Context") ?? throw new InvalidOperationException("Connection string 'WebApplication11Context' not found.")));

// Configuración de la autenticación
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
    {
        config.Cookie.Name = "UserLoginCookie";
        config.LoginPath = "/Account/AccessDenied";  // Ruta para redirigir a la página de inicio de sesión
        config.AccessDeniedPath = "/Account/AccessDenied";  // Ruta para acceso denegado
    });

builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Debe ir antes de UseAuthorization
app.UseAuthorization();   // Debe ir después de UseAuthentication

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
