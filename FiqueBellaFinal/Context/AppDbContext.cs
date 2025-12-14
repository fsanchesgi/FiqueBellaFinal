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

        // =========================
        // Tabelas reais
        // =========================
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set; }
        public DbSet<Sugestao> Sugestaos { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Contabilidade> Contabilidades { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<EntradaSaida> EntradasSaidas { get; set; }

        // =========================
        // Queries / Views (SEM TABELA)
        // =========================
        public DbSet<ProcedimentoGrafico> ProcedimentoGraficos { get; set; }

        // =========================
        // Models auxiliares (NÃO são tabelas)
        // =========================
        public DbSet<FileManagerModel> FileManagerModels { get; set; }
        public DbSet<ConfigurationImagens> ConfigurationImagens { get; set; }

        // =========================
        // Configuração EF Core
        // =========================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // -------------------------
            // FileManagerModel (NÃO tabela)
            // -------------------------
            modelBuilder.Entity<FileManagerModel>()
                .HasNoKey()
                .ToView(null);

            modelBuilder.Entity<FileManagerModel>()
                .Ignore(x => x.IFormFile)
                .Ignore(x => x.IFormFiles)
                .Ignore(x => x.Files);

            // -------------------------
            // ConfigurationImagens (NÃO tabela)
            // -------------------------
            modelBuilder.Entity<ConfigurationImagens>()
                .HasNoKey()
                .ToView(null);

            // -------------------------
            // ProcedimentoGrafico (Query / View)
            // -------------------------
            modelBuilder.Entity<ProcedimentoGrafico>()
                .HasNoKey();

            modelBuilder.Entity<ProcedimentoGrafico>()
                .Property(p => p.ProcedimentoValorTotal)
                .HasPrecision(18, 2);

            // -------------------------
            // Precisão para valores monetários
            // -------------------------
            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
