using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Managment
{
    class ProductValidator : Validator<Product>
    {
        private readonly IRepository<Order> _orders;

        public ProductValidator(IRepository<Product> products, IRepository<Order> orders):base(products)
        {
            _orders = orders;
        }

        public override void Add(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (item.Manufacturer == null
                || item.Supply == null
                || item.AdditionalInformation == null)
            {
                throw new InvalidOperationException("Product must have manufaturer and supplier");
            }

            _items.Add(item);
        }

        public override void Delete(int itemId)
        {
            var product = _items.GetById(itemId).Result;

            if (product == null)
            {
                throw new InvalidOperationException("Product not found");
            }

            var canDeleteProduct = _orders.GetAll().Result.Any(order => order.ProductId == itemId);

            if (!canDeleteProduct)
            {
                throw new InvalidOperationException("Product connected to order");
            }

            _items.Delete(itemId);
        }

        public override void Update(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var productToUpdate = _items.GetById(item.Id).Result;

            if (productToUpdate == null)
            {
                throw new InvalidOperationException("Product with such id doesn't exist");
            }

            if (item.Manufacturer == null
                || item.Supply == null
                || item.AdditionalInformation == null)
            {
                throw new MissingFieldException("Producat must have manufaturer, supply, additionalInformation");
            }

            if (item.Price < 0)
            {
                throw new FormatException("Price cant be negative");
            }

            _items.Update(item);
        }
    }
}
