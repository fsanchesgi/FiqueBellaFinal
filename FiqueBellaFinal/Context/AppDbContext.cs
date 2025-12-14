using Microsoft.EntityFrameworkCore;
using FiqueBellaFinal.Models;

namespace FiqueBellaFinal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set; }
        public DbSet<Sugestao> Sugestaos { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Contabilidade> Contabilidades { get; set; }
        public DbSet<Tipo> Tipos { get; set; }

        public DbSet<ProcedimentoGrafico> ProcedimentoGraficos { get; set; }
        public DbSet<EntradaSaida> EntradasSaidas { get; set; }

        // ⚠️ MANTIDOS
        public DbSet<FileManagerModel> FileManagerModels { get; set; }
        public DbSet<ConfigurationImagens> ConfigurationImagens { get; set; }

        // ✅ ADICIONE ISTO
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // FileManagerModel NÃO é tabela
            modelBuilder.Entity<FileManagerModel>()
                .HasNoKey()
                .ToView(null);

            modelBuilder.Entity<FileManagerModel>()
                .Ignore(x => x.IFormFile)
                .Ignore(x => x.IFormFiles)
                .Ignore(x => x.Files);

            // ConfigurationImagens NÃO é tabela
            modelBuilder.Entity<ConfigurationImagens>()
                .HasNoKey()
                .ToView(null);

            base.OnModelCreating(modelBuilder);
        }
    }
}
