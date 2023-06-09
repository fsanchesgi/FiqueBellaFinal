using FiqueBellaFinal.Models;

namespace FiqueBellaFinal.Repositories.Interfaces
{
    public class IContabilidadeRepository
    {
        IEnumerable<Contabilidade> Contabilidades { get; }
    }
}
