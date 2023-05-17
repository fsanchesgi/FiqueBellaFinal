using FiqueBellaFinal.Context;
using FiqueBellaFinal.Models;
using FiqueBellaFinal.Repositories.Interfaces;

namespace FiqueBellaFinal.Repositories
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly AppDbContext _context;

        public AgendaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Agenda> Agendas => _context.Agendas;
    }
}
