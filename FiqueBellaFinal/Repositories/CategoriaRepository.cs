using FiqueBellaFinal.Context;
using FiqueBellaFinal.Models;
using FiqueBellaFinal.Repositories.Interfaces;

namespace FiqueBellaFinal.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
