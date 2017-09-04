using System;
using AzSp.Products.Domain;
using AzSp.Products.Persistence;
using Nest;

namespace AzSp.Products.Logic
{
    public class Elastic
    {
        private readonly AppConfiguration _appConfiguration;

        public Elastic(AppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;


            if (!Client.IndexExists("products").Exists)
            {
                var createIndexResponse = Client.CreateIndex("products", c => c
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
        public ElasticClient Client
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
    }
}