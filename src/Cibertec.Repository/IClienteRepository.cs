using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        IEnumerable<Cliente> BuscarCliente(string texto);
        IEnumerable<Cliente> GetClientePag(ClienteQuery query);
        void UpdateClienteBd(Cliente cliente);
    }
}

