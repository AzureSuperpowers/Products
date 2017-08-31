using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using AzSp.Products.Domain;
using AzSp.Products.Persistence;
using Nest;

namespace AzSp.Products.Controllers
{
    [Route("")]
    public class ValuesController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly AppConfiguration _appConfiguration;

        public ValuesController(ProductRepository productRepository, AppConfiguration appConfiguration)
        {
            _productRepository = productRepository;
            _appConfiguration = appConfiguration;


            if (!ElasticClient.IndexExists("products").Exists)
            {
                var createIndexResponse = ElasticClient.CreateIndex("products", c => c
                    .Settings(s => s
                        .NumberOfShards(1)
                        .NumberOfReplicas(0)
                    )
                    .Mappings(m => m
                        .Map<Product>(d => d
                            .AutoMap()
                        )
                    )
                );
            }
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productRepository.GetAll();
        }

        private ElasticClient ElasticClient
        {
            get
            {
                var node = new Uri(_appConfiguration.ElasticSearch);
                var settings = new Nest.ConnectionSettings(node).DefaultIndex("products");
                settings.BasicAuthentication("elastic", "changeme");
                var client = new ElasticClient(settings);
                return client;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            if (id == 0) return null;
            var product = _productRepository.GetByID(id);
            ElasticClient.Index(product, f => f.Id(id));
            return product;
        }

        [HttpGet("elastic-get-all")]
        public IReadOnlyCollection<Product> GetElastic()
        {
            return ElasticClient.Search<Product>(s => s.From(0).Size(10).MatchAll()).Documents;
        }

        [HttpGet("elastic-get/{id}")]
        public IReadOnlyCollection<Product> GetElastic(int id)
        {
            return ElasticClient.Search<Product>(s => s.From(0).Size(10).Query(q => q.Term(t => t.ProductId, id))).Documents;
        }

        [HttpGet("elastic-sync")]
        public void SyncElastic()
        {
            foreach (var prod in _productRepository.GetAll())
            {
                ElasticClient.Index(prod, f => f.Id(prod.ProductId));
            }
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
    }
}
