using FiqueBellaFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace FiqueBellaFinal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // 🔹 DbSets para todas as entidades usadas nos controllers e repositórios
        public DbSet<Procedimento> Procedimentos { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Produto> Produtos { get; set; } = null!;
        public DbSet<Sugestao> Sugestaos { get; set; } = null!;
        public DbSet<Contabilidade> Contabilidades { get; set; } = null!;
        public DbSet<Tipo> Tipos { get; set; } = null!;
        public DbSet<Agenda> Agendas { get; set; } = null!;
        public DbSet<EntradaSaida> EntradaSaidas { get; set; } = null!;
        public DbSet<ProcedimentoGrafico> ProcedimentoGraficos { get; set; } = null!;
        public DbSet<ConfigurationImagens> ConfigurationImagens { get; set; } = null!;
    }
}
