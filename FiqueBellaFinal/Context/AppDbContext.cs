using Microsoft.EntityFrameworkCore;
using FiqueBellaFinal.Models;

namespace FiqueBellaFinal.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set; }
        public DbSet<Contabilidade> Contabilidades { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Sugestao> Sugestaos { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<EntradaSaida> EntradasSaidas { get; set; }

        // Adicione outros DbSet conforme necessário
    }
}
