using Microsoft.EntityFrameworkCore;

namespace FiqueBellaFinal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Defina seus DbSets aqui, exemplo:
        // public DbSet<Cliente> Clientes { get; set; }
        // public DbSet<Categoria> Categorias { get; set; }
        // public DbSet<Procedimento> Procedimentos { get; set; }
        // public DbSet<Sugestao> Sugestoes { get; set; }
    }
}
