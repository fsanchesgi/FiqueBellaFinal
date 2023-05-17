using FiqueBellaFinal.Models;

namespace FiqueBellaFinal.Repositories.Interfaces
{
    public interface IProcedimentoRepository
    {
        IEnumerable<Procedimento> Procedimentos { get; }
        IEnumerable<Procedimento> ProcedimentoPreferidos { get; }
    }
}
