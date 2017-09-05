using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AzSp.Products.Domain;
using AzSp.Products.Logic;
using AzSp.Products.Persistence;
using Nest;

namespace AzSp.Products.Controllers
{
    [Route("")]
    public class ValuesController : Controller
    {
        private readonly ProductsLogic _productsLogic;

        public ValuesController(ProductsLogic productsLogic)
        {
            _productsLogic = productsLogic;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productsLogic.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            if (id == 0) return null;
            return _productsLogic.Get(id).First();
        }

        [HttpGet("sync")]
        public void SyncElastic()
        {
            _productsLogic.Sync();
        }


        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody]Product value)
        {
            try
            {
                _productsLogic.Save(value);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            //todo
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //todo
        }
    }
}
