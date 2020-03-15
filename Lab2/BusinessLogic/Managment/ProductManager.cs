using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Managment
{
    class ProductManager : IManager<Product>
    {
        private readonly IRepository<Product> _products;

        private readonly IRepository<Order> _orders;

        public ProductManager(IRepository<Product> products)
        {
            _products = products;
        }

        public void Add(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (item.Manufacturer == null
                || item.Supply == null)
            {
                throw new InvalidOperationException("Product must have manufaturer and supplier");
            }

            _products.Add(item);
        }

        public void Delete(int itemId)
        {
            var product = _products.GetById(itemId).Result;

            if (product == null)
                throw new InvalidOperationException("Product not found");

            var canDeleteProduct = _orders.GetAll().Result.Any(order => order.Product == product);

            if(!canDeleteProduct)
                throw new InvalidOperationException("Product connected to order");
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetById(int itemId)
        {
            throw new NotImplementedException();
        }

        public void Update(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
