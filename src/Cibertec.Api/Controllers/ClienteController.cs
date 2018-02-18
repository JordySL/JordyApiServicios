using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Cibertec.Business;
using Cibertec.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using System.IO;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Cibertec.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        private readonly IClienteBusiness _clienteBusiness;
        public ClienteController(IClienteBusiness clienteBusiness)
        {
            _clienteBusiness = clienteBusiness;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<Cliente> Get()
        {

            return _clienteBusiness.GetClientes();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Cliente Get(int id) => _clienteBusiness.GetCliente(id);

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) => _clienteBusiness.DeleteCliente(new Cliente {Id = id });
        [HttpPost]
        [Route("GetClientes")]
        public IEnumerable<Cliente> GetClientes([FromBody]ClienteQuery param)
        {
            return _clienteBusiness.GetClientePag(param);
        }
        [HttpPost]
        [Route("GuardarCliente")]
        public void InsertarCliente(Cliente cliente, ICollection<IFormFile> files)
        {
            if (files.Count > 0)
            {
                var archivo = files.First();
                var fileName = $"{DateTime.Now.ToString("ddMMyyyyhhmmss")}_{archivo.FileName}";
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    archivo.CopyTo(stream);
                }
                cliente.SetFoto(fileName);
            }
            _clienteBusiness.InsertCliente(cliente);
        }
        [HttpPost]
        [Route("GetArchivo")]
        public FileStreamResult GetArchivo([FromBody]Descargar descargar)
        {
            if (string.IsNullOrWhiteSpace(descargar.archivo))
                throw new Exception("No existe el archivo");
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Archivos", descargar.archivo);
            FileStream fileStream = new FileStream(fullPath, FileMode.Open);
            return File(fileStream, "application/octet-stream", descargar.archivo);
        }
        [HttpPost]
        [Route("UpdateCliente")]
        public void UpdateCliente([FromBody]Cliente cliente)
        { 
            _clienteBusiness.UpdateClienteBd(cliente);
        }
}   
    public class Descargar
        {
            public string archivo { get; set; }
        }
}