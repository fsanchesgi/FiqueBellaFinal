using FiqueBellaFinal.Models;
using System.IO;

namespace FiqueBellaFinal.Repositories.Interfaces
{
    public interface IProcedimentoRepository
    {
        IEnumerable<Procedimento> Procedimentos { get; }
        IEnumerable<Procedimento> ProcedimentosPreferidos { get; }
        Procedimento GetProcedimento(int procedimentoId);
    }
}
