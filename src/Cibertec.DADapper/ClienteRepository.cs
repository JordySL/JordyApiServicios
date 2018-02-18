using System.Collections.Generic;
using Cibertec.Models;
using Cibertec.Repository;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace Cibertec.DADapper
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(string connectionString) : base(connectionString) { }
        public IEnumerable<Cliente> BuscarCliente(string texto)
        {
            var param = new
            { texto = texto };
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Cliente>("dbo.BuscarCliente", param, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<Cliente> GetClientePag(ClienteQuery query)
        {
        using (var con = new SqlConnection(_connectionString))
            {
                return con.Query<Cliente>("dbo.GetClienteByParams", query, commandType: CommandType.StoredProcedure);
            }
        }
        public void UpdateClienteBd(Cliente cliente)
        {
            var param = new
            {
                id = cliente.Id,
                Nombre = cliente.Nombre,
                ApellidoPaterno = cliente.ApellidoPaterno,
                ApellidoMaterno = cliente.ApellidoMaterno,
                Direccion = cliente.Direccion
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("dbo.UpdateCliente", param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
