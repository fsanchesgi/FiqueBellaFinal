using FiqueBellaFinal.Models;

namespace FiqueBellaFinal.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        IEnumerable<Cliente> Clientes { get; }

        Cliente GetCliente(int clinteId);
    }
}
