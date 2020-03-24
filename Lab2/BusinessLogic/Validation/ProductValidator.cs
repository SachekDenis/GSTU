using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Validation
{
    public class ProductValidator : Validator<Product>
    {
        private readonly IRepository<Order> _orders;
        private readonly IRepository<Manufacturer> _manufacturers;
        private readonly IRepository<Supply> _supplyes;

        public ProductValidator(IRepository<Product> products,
            IRepository<Order> orders,
            IRepository<Manufacturer> manufacturers,
            IRepository<Supply> supplyes) : base(products)
        {
            _orders = orders;
            _manufacturers = manufacturers;
            _supplyes = supplyes;
        }

        protected override bool ValidateProperties(Product item)
        {
            return !(!_manufacturers.GetAll().Result.Where(manufacturer => item.ManufacturerId == manufacturer.Id).Any()
                || !_supplyes.GetAll().Result.Where(supply => item.SupplyId == supply.Id).Any()
                || item.Price < 0
                || string.IsNullOrEmpty(item.Name));
        }

        protected override bool ValidateReferences(Product item)
        {
            return _orders.GetAll().Result.Any(order => order.ProductId == item.Id);
        }

    }
}
