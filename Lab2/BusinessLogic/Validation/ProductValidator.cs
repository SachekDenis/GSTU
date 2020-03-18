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

        public ProductValidator(IRepository<Product> products, IRepository<Order> orders) : base(products)
        {
            _orders = orders;
        }

        protected override bool ValidateProperties(Product item)
        {
            return item.Manufacturer == null
                || item.Supply == null
                || item.AdditionalInformation == null;
        }

        protected override bool ValidateReferences(Product item)
        {
            return _orders.GetAll().Result.Any(order => order.ProductId == item.Id); ;
        }

    }
}
