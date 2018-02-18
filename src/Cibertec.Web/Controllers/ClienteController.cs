using Cibertec.Business;
using Cibertec.Models;
using Cibertec.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace Cibertec.Web.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteBusiness _clienteBusiness;
        public ClienteController(IClienteBusiness clienteBusiness)
        {
            _clienteBusiness = clienteBusiness;
        }
        public IActionResult Index()
        {
            var query = new ClienteQuery()
            {
                Offset = 1,
                PerPage = 3
            };
            var response = _clienteBusiness.GetClientePag(query).ToList();

            var lista = new ClienteLista(response, response.First().Total);
            ViewData["IsLastPage"] = response.Count < 3;
            ViewData["CurrentPage"] = 1;
            return View(lista);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            var param = new Cliente() { Id = id };
            if (_clienteBusiness.DeleteCliente(param) > 0) return RedirectToAction("Index");
            return View();
        }
        public IActionResult Edit(int id)
        {
            var entidad = _clienteBusiness.GetCliente(id);
            return View(entidad);
        }
        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            if (cliente.Imagen.Count > 0)
            {
                var img = cliente.Imagen.First();
                var imgName = $"{cliente.Id}{cliente.Nombre}{".png"}";
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Imagenes", imgName);
                using (var steam = new FileStream(fullPath, FileMode.Create))
                { img.CopyTo(steam); }
                cliente.SetFoto(imgName);
            }

            if (cliente.Archivo.Count > 0)
            {
                var archivo = cliente.Archivo.First();
                var archivoName = $"{cliente.Id}{cliente.Nombre}_{archivo.FileName}";
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Documentos", archivoName);
                using (var steam = new FileStream(fullPath, FileMode.Create))
                { archivo.CopyTo(steam); }
                cliente.SetDoc(archivoName);
            }

            if (_clienteBusiness.InsertCliente(cliente) > 0) return Redirect("Index");

            return View(cliente);
        }
        [HttpPost]
        public IActionResult Edit(Cliente cliente)
        {
            if (cliente.Imagen.Count > 0)
            {
                var img = cliente.Imagen.First();
                var imgName = $"{cliente.Id}{cliente.Nombre}{".png"}";
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Imagenes", imgName);
                using (var steam = new FileStream(fullPath, FileMode.Create))
                { img.CopyTo(steam); }
                cliente.SetFoto(imgName);
            }
            if (cliente.Archivo.Count > 0)
            {
                var archivo = cliente.Archivo.First();
                var archivoName = $"{cliente.Id}{cliente.Nombre}_{archivo.FileName}";
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Documentos", archivoName);
                using (var steam = new FileStream(fullPath, FileMode.Create))
                { archivo.CopyTo(steam); }
                cliente.SetDoc(archivoName);
            }

            if (_clienteBusiness.UpdateCliente(cliente) > 0) return RedirectToAction("Index");
            return View(cliente);
        }
        [HttpPost]
        public IActionResult Search(string texto)
        {
            var query = new ClienteQuery()
            {
                Offset = 1,
                PerPage = 3
            };
            if (string.IsNullOrWhiteSpace(texto))
            {return RedirectToAction("Index");}

            var response = _clienteBusiness.GetClienteByNombre(texto).ToList();

            var entidad = new ClienteLista(response, response.Count);
            ViewData["IsLastPage"] = response.Count < 3;
            ViewData["CurrentPage"] = 1;
            return View("Index", entidad);
        }
        [HttpGet]
        public IActionResult GetClientePag(string type, int currentPage)
        {
            var offset = 1;
            if (type == "p") offset = currentPage - 1;
            if (type == "n") offset = currentPage + 1;

            var query = new ClienteQuery { Offset = offset, PerPage = 3 };
            var response = _clienteBusiness.GetClientePag(query).ToList();

            var lista = new ClienteLista(response, response.First().Total);
            ViewData["IsLastPage"] = response.Count < 3;
            ViewData["CurrentPage"] = offset;

            return View("Index", lista);
        }
        [HttpGet]
        public IActionResult GetImage(string imgName)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Imagenes", imgName);
            FileStream fileStream = new FileStream(fullPath, FileMode.Open);
            return File(fileStream, "application/octet-stream", imgName);
        }
        [HttpGet]
        public IActionResult GetArchivo(string archivoName)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Documentos", archivoName);
            FileStream fileStream = new FileStream(fullPath, FileMode.Open);
            return File(fileStream, "application/octet-stream", archivoName);
        }
    }
}
