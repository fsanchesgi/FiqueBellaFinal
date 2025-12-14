using FiqueBellaFinal.Data;
using FiqueBellaFinal.Models;
using FiqueBellaFinal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiqueBellaFinal.Repositories
{
    public class ProcedimentoRepository : IProcedimentoRepository
    {
        private readonly AppDbContext _context;

        public ProcedimentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Procedimento> ProcedimentosPreferidos
        {
            get
            {
                try
                {
                    return _context.Procedimentos
                        .Where(p => p.Preferido)
                        .AsNoTracking()
                        .ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao buscar ProcedimentosPreferidos: " + ex.Message);
                    return Enumerable.Empty<Procedimento>();
                }
            }
        }

        public IEnumerable<Procedimento> ProcedimentosEmPromocao
        {
            get
            {
                try
                {
                    return _context.Procedimentos
                        .Where(p => p.EmPromocao)
                        .AsNoTracking()
                        .ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao buscar ProcedimentosEmPromocao: " + ex.Message);
                    return Enumerable.Empty<Procedimento>();
                }
            }
        }

        public IEnumerable<Procedimento> Procedimentos
        {
            get
            {
                try
                {
                    return _context.Procedimentos
                        .AsNoTracking()
                        .ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao buscar Procedimentos: " + ex.Message);
                    return Enumerable.Empty<Procedimento>();
                }
            }
        }

        public Procedimento GetProcedimentoById(int procedimentoId)
        {
            try
            {
                return _context.Procedimentos
                    .AsNoTracking()
                    .FirstOrDefault(p => p.ProcedimentoId == procedimentoId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar Procedimento por ID: " + ex.Message);
                return null;
            }
        }
    }
}
