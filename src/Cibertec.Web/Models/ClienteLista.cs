using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Web.Models
{
    public class ClienteLista
    {
            public ClienteLista(IEnumerable<Cliente> clientes, int total)
            {
                Clientes = clientes;
                Total = total;
            }
            public IEnumerable<Cliente> Clientes { get; set; }
            public int Total { get; set; }
    }
}
