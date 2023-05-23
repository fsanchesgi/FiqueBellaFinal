using FiqueBellaFinal.Context;
using FiqueBellaFinal.Models;
using FiqueBellaFinal.Repositories.Interfaces;
using static FiqueBellaFinal.Repositories.SugestaoRepository;

namespace FiqueBellaFinal.Repositories
{
    public class SugestaoRepository : ISugestaoRepository
    {
        public class SugestãoRepository : ISugestaoRepository
        {
            private readonly AppDbContext _context;

            public SugestãoRepository(AppDbContext context)
            {
                _context = context;
            }
            public IEnumerable<Sugestao> Sugestaos => _context.Sugestaos;
        }
    }
}
