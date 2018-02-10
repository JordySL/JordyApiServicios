using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Cibertec.Business;
using Cibertec.Models;

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
        public string Get(int id)
        {
            return "value";
        }

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
        public void Delete(int id)
        {
        }
        [HttpPost]
        [Route("GetClientes")]
        public IEnumerable<Cliente> GetClientes([FromBody]ClienteQuery param)
        {
            return _clienteBusiness.GetClientePag(param);
        }
    }
}
