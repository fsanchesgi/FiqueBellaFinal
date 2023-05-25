using FiqueBellaFinal.Models;
using System.IO;

namespace FiqueBellaFinal.Repositories.Interfaces
{
    public interface IProcedimentoRepository
    {
        IEnumerable<Procedimento> Procedimentos { get; }
        IEnumerable<Procedimento> ProcedimentosPreferidos { get; }
        IEnumerable<Procedimento> ProcedimentosEmPromocao { get; }
        Procedimento GetProcedimento(int procedimentoId);
    }
}
