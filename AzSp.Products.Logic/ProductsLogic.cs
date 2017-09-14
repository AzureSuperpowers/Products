using System.Collections.Generic;
using AzSp.Products.Domain;
using AzSp.Products.Persistence;

namespace AzSp.Products.Logic
{
    public class ProductsLogic
    {

        private readonly ProductRepository _productRepository;
        private readonly Elastic _elastic;

        public ProductsLogic(ProductRepository productRepository, Elastic elastic)
        {
            _productRepository = productRepository;
            _elastic = elastic;
        }

        public IReadOnlyCollection<Product> GetAll()
        {
            return _elastic.Client.Search<Product>(s => s.From(0).Size(1000).MatchAll()).Documents;
        }

        public IReadOnlyCollection<Product> Get(int id)
        {
            return _elastic.Client.Search<Product>(s => s.From(0).Size(10).Query(q => q.Term(t => t.ProductId, id))).Documents;
        }

        public void Save(Product model)
        {
            _productRepository.Update(model);
            _elastic.Client.Index(model, f => f.Id(model.ProductId));
        }

        public void Sync()
        {
            foreach (var prod in _productRepository.GetAll())
            {
                _elastic.Client.Index(prod, f => f.Id(prod.ProductId));
            }
        }
    }
}
