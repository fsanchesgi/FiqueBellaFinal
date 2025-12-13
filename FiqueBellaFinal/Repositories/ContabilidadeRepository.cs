using FiqueBellaFinal.Models;
using FiqueBellaFinal.Repositories.Interfaces;
using static FiqueBellaFinal.Repositories.ContabilidadeRepository;
using static FiqueBellaFinal.Repositories.SugestaoRepository;

namespace FiqueBellaFinal.Repositories
{
    public class ContabilidadeRepository : IContabilidadeRepository
    {
        private readonly AppDbContext _context;

        public ContabilidadeRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Contabilidade> Contabilidades => _context.Contabilidades;
    }
}
