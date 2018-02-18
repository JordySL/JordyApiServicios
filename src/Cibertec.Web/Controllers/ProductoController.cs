using Cibertec.Business;
using Cibertec.Models;
using Cibertec.Web.Handlers;
using Cibertec.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace Cibertec.Web.Controllers
{
    [HandleCustomError]
    [LogginFilterAtribute]
    public class ProductoController : Controller
    {
        private IProductoBusiness _productoBusiness;
        public ProductoController(IProductoBusiness productoBusiness)
        {
            _productoBusiness = productoBusiness;
        }
        //
        public IActionResult Index()
        {
            var query = new ProductoQuery()
            {
                //pagina 1 
                Offset = 1,
                //10 Registros
                PerPage = 9
            };
            var response = _productoBusiness.GetProductoPaginado(query).ToList();

            //var response = _productoBusiness.GetProductos().ToList();
            //var responseDTO = Mapper.Map<List<Producto1>>(response);
            var lista = new ProductoLista(response, response.First().Total);
            ViewData["IsLastPage"] = response.Count < 9;
            ViewData["CurrentPage"] = 1;
            return View(lista);
        }
        //
        [HttpGet]
        public IActionResult GetProductosPaginado(string type, int currentPage)
        {
            var offset = 1;
            if (type == "p") offset = currentPage - 1;
            if (type == "n") offset = currentPage + 1;

            var query = new ProductoQuery { Offset = offset, PerPage = 9 };
            var response = _productoBusiness.GetProductoPaginado(query).ToList();

            var lista = new ProductoLista(response, response.First().Total);
            ViewData["IsLastPage"] = response.Count < 9;
            ViewData["CurrentPage"] = offset;

            return View("Index",lista);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //al crear se agregar el archivo{
        public IActionResult Create(Producto producto)
        {
            if (producto.files.Count > 0)
            {
                var file = producto.files.First();
                var fileName = $"{DateTime.Now.ToString("ddMMyyyyhhmmss")}_{file.FileName}";
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles", fileName);
                using (var steam = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(steam);
                }
                producto.SetArchivo(fileName);
            }
            if (_productoBusiness.InsertProducto(producto) > 0) return Redirect("Index");

            return View(producto);
        }
        //}
        public IActionResult Delete(int id)
        {
            var param = new Producto() { Id = id };
            if (_productoBusiness.DeleteProducto(param) > 0) return RedirectToAction("Index");
            return View();
        }
        public IActionResult Edit(int id)
        {
            var entidad = _productoBusiness.GetProducto(id);
            //throw new System.Exception("este error");
            return View(entidad);
        }
        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
            if (_productoBusiness.UpdateProducto(producto) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(producto);
        }
        //
        [HttpPost]
        public IActionResult Search(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return RedirectToAction("Index");
            } 

            var response = _productoBusiness.GetProductoByDesc(texto).ToList();

            var entidad = new ProductoLista(response, response.Count);
            return View("Index", entidad);
        }
        //
        [HttpGet]
        public IActionResult GetImage(string fileName)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles", fileName);
            FileStream fileStream = new FileStream(fullPath, FileMode.Open);
            return File(fileStream, "application/octet-stream", fileName);
        }
    }
}

