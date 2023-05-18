using FiqueBellaFinal.Models;

namespace FiqueBellaFinal.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }
    }
}
