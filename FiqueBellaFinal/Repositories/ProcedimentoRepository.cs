using FiqueBellaFinal.Models;
using FiqueBellaFinal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace FiqueBellaFinal.Repositories
{
    public class ProcedimentoRepository : IProcedimentoRepository
    {
        private readonly AppDbContext _contexto;
        public ProcedimentoRepository(AppDbContext contexto)
        {
            _contexto = contexto;
        }
        public IEnumerable<Procedimento> Procedimentos => _contexto.Procedimentos.Include(c => c.Categoria);

        public IEnumerable<Procedimento> ProcedimentosPreferidos => _contexto.Procedimentos
                                                      .Where(p => p.IsProcedimentoPreferido)
                                                      .Include(c => c.Categoria);

        public IEnumerable<Procedimento> ProcedimentosEmPromocao => _contexto.Procedimentos
                                                      .Where(p => p.EmPromocao)
                                                      .Include(c => c.Categoria);

        public Procedimento GetProcedimento(int procedimentoId)
        {
            return _contexto.Procedimentos.FirstOrDefault(p => p.ProcedimentoId == procedimentoId);
        }

    }
}
