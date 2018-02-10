using Cibertec.Models;
using Cibertec.UnitOfWork;
using System.Collections.Generic;
using System;

namespace Cibertec.Business
{
    public interface IClienteBusiness
    {
        IEnumerable<Cliente> GetClientes();
        Cliente GetCliente(int id);
        int InsertCliente(Cliente cliente);
        int DeleteCliente(Cliente cliente);
        int UpdateCliente(Cliente cliente);
        IEnumerable<Cliente> GetClienteByNombre(string texto);
        IEnumerable<Cliente> GetClientePag(ClienteQuery query);
    }
    public class ClienteBusiness : IClienteBusiness
    {
        private readonly IUnitOfWork _unitofWork;
        public ClienteBusiness(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IEnumerable<Cliente> GetClientes()
        {
            return _unitofWork.clientes.GetList();
        }
        public Cliente GetCliente(int id)
        {
            return _unitofWork.clientes.GetById(id);
        }
        public int InsertCliente(Cliente cliente)
        {
            return _unitofWork.clientes.Insert(cliente);
        }
        public int DeleteCliente(Cliente cliente)
        {
            return _unitofWork.clientes.Delete(cliente);
        }
        public int UpdateCliente(Cliente cliente)
        {
            return _unitofWork.clientes.Update(cliente);
        }
        public IEnumerable<Cliente> GetClienteByNombre(string texto)
        {
            return _unitofWork.clientes.BuscarCliente(texto);
        }
        public IEnumerable<Cliente> GetClientePag(ClienteQuery query)
        {
            return _unitofWork.clientes.GetClientePag(query);
        }
    }
}