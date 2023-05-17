using FiqueBellaFinal.Models;

namespace FiqueBellaFinal.Repositories.Interfaces
{
    public interface IAgendaRepository
    {
        IEnumerable<Agenda> Agendas { get; }

        Agenda GetAgenda(int AgendaId);
    }
}
