using System.Linq;
using ComputerStore.DataAccessLayer.Models;
using ComputerStore.DataAccessLayer.Repo;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class ManufacturerValidator : Validator<Manufacturer>
    {
        private readonly IRepository<Product> _products;
        public ManufacturerValidator(IRepository<Manufacturer> manufacturers,
                                     IRepository<Product> products)
                                     : base(manufacturers)
        {
            _products = products;
        }

        protected override bool ValidateProperties(Manufacturer item)
        {
            return !(string.IsNullOrEmpty(item.Name)
                || string.IsNullOrEmpty(item.Country));
        }

        protected override bool ValidateReferences(Manufacturer item)
        {
            return !_products.GetAll().Any(product => product.ManufacturerId == item.Id);
        }
    }
}
