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
            {
                throw new InvalidOperationException("Product not found");
            }

            var canDeleteProduct = _orders.GetAll().Result.Any(order => order.ProductId == itemId);

            if (!canDeleteProduct)
            {
                throw new InvalidOperationException("Product connected to order");
            }

            _products.Delete(itemId);
        }

        public IEnumerable<Product> GetAll()
        {
            return _products.GetAll().Result;
        }

        public Product GetById(int itemId)
        {
            var product = _products.GetById(itemId).Result;

            return product;
        }

        public void Update(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var productToUpdate = _products.GetById(item.Id).Result;

            if (item == null)
            {
                throw new InvalidOperationException("Product with such id doesn't exist");
            }

            if (item.Manufacturer == null || item.Supply == null || item.AdditionalInformation == null)
            {
                throw new MissingFieldException("Producat must have manufaturer, supply, additionalInformation");
            }

            if(item.Price < 0)
            {
                throw new FormatException("Price cant be negative");
            }

            productToUpdate.Manufacturer = item.Manufacturer;
            productToUpdate.Name = item.Name;
            productToUpdate.Price = item.Price;
            productToUpdate.Supply = item.Supply;
            productToUpdate.AdditionalInformation = item.AdditionalInformation;

            _products.Update(productToUpdate);
        }
    }
}
