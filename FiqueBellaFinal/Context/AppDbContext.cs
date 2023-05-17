using FiqueBellaFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace FiqueBellaFinal.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {          
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set;}
    }
}
