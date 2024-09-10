using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Models;

namespace WebApplication11.Data
{
    public class WebApplication11Context : DbContext
    {
        public WebApplication11Context (DbContextOptions<WebApplication11Context> options)
            : base(options)
        {
        }

        public DbSet<WebApplication11.Models.Medico> Medico { get; set; } = default!;
        public DbSet<WebApplication11.Models.Paciente> Paciente { get; set; } = default!;
        public DbSet<WebApplication11.Models.Tratamiento> Tratamiento { get; set; } = default!;
        public DbSet<WebApplication11.Models.Cita> Cita { get; set; } = default!;
        public DbSet<WebApplication11.Models.Facturacion> Facturacion { get; set; } = default!;

        public DbSet<WebApplication11.Models.User> Users { get; set; } = default;


    }
}
