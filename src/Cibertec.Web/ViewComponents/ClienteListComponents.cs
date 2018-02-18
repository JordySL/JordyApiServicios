using Cibertec.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cibertec.Web.ViewComponents
{
    [ViewComponent(Name = "ClienteLista")]
    public class ClienteListComponents : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Cliente cliente)
        {
            return View("ClienteLista", cliente);
        }
    }
}
