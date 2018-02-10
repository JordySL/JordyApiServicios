using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Api.Models
{
    public class ProductoResponse
    {
        public IEnumerable<Producto> Items { get; set; }

    }
}
