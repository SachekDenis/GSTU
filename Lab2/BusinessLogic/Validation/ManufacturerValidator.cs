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

        public override void Delete(int itemId)
        {
            var manufaturer = _items.GetById(itemId).Result;

            if (manufaturer  == null)
            {
                throw new InvalidOperationException("Manufacturer not found");
            }

            var canDeleteManufaturer = _products.GetAll().Result.Any(product => product.ManufacturerId == itemId);

            if (!canDeleteManufaturer)
            {
                throw new InvalidOperationException("Product references to this manufaturer");
            }

            _items.Delete(itemId);
        }
    }
}
