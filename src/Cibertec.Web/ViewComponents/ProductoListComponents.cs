using Cibertec.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cibertec.Web.ViewComponents
{
    [ViewComponent(Name = "ProductoLista")]
    public class ProductoListComponents : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Producto producto)
        {
            return View("ProductoLista", producto);
        }
    }
}
