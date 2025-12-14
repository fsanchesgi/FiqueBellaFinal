using FiqueBellaFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace FiqueBellaFinal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // 🔹 DbSets das tabelas
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Contabilidade> Contabilidades { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set; }
        public DbSet<Sugestao> Sugestoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<EntradaSaida> EntradasSaidas { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<ProcedimentoGrafico> ProcedimentosGraficos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais se necessário
            // Exemplo: modelBuilder.Entity<Procedimento>().Property(p => p.Nome).IsRequired();
        }
    }
}
