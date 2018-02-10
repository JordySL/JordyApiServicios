using System;
using Cibertec.Repository;
using Cibertec.UnitOfWork;

namespace Cibertec.DADapper
{
    public class CibertecUnitOfWork : IUnitOfWork
    {
        public CibertecUnitOfWork(string connectionString)
        {
            productos = new ProductoRepository(connectionString);
            clientes = new ClienteRepository(connectionString);
        }
        public IProductoRepository productos { get; private set; }
        public IClienteRepository clientes { get; private set; }
    }

}
