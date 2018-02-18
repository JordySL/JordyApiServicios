using System.Collections.Generic;
using Cibertec.Models;
using Cibertec.Repository;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using System;

namespace Cibertec.DADapper
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        public ProductoRepository(string connectionString) :
            base(connectionString)
        {

        }
        //
        public IEnumerable<Producto> BuscarProducto(string texto)
        {
            //
            var param = new
            {
                texto = texto
                //param2 = "texto"
            };
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Producto>("dbo.BuscarProducto", param, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<Producto> GetProductoPaginado(ProductoQuery query)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return con.Query<Producto>("dbo.GetProductoByParams", query, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<IEnumerable<Producto>> GetProductosSinStock()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<Producto>("dbo.GetProductoSinStock", commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateProductoBd(Producto producto)
        {
            var param = new
            {
                id = producto.Id,
                descripcion = producto.Descripcion,
                stockMinimo = producto.StockMinimo
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("dbo.UpdateProducto", param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
