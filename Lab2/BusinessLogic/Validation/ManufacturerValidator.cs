using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Linq;

namespace BusinessLogic.Managment
{
    class ManufacturerValidator : Validator<Manufacturer>
    {
        private readonly IRepository<Product> _products;
        public ManufacturerValidator(IRepository<Manufacturer> manufacturers,
                                     IRepository<Product> products)
                                     :base(manufacturers)
        {
            _products = products;
        }

        protected override bool ValidateReferences(Manufacturer item)
        {
            return _products.GetAll().Result.Any(product => product.ManufacturerId == item.Id); ;
        }
    }
}
