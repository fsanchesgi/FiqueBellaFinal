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

        public DbSet<FileManagerModel> FileManagerModels { get; set; }
        public DbSet<ConfigurationImagens> ConfigurationImagens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileManagerModel>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);

                entity.Ignore(x => x.IFormFile);
                entity.Ignore(x => x.IFormFiles);
                entity.Ignore(x => x.Files);
            });

            modelBuilder.Entity<ConfigurationImagens>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

            modelBuilder.Entity<ProcedimentoGrafico>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
