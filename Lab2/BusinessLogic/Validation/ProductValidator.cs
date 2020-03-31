using System.Linq;
using DataAccessLayer.Models;
using DataAccessLayer.Repo;

namespace BusinessLogic.Validation
{
    public class ProductValidator : Validator<Product>
    {
        private readonly IRepository<Order> _orders;
        private readonly IRepository<Manufacturer> _manufacturers;
        private readonly IRepository<Supply> _supplyes;
        private readonly IRepository<Category> _categories;
        private readonly IRepository<Field> _fields;

        public ProductValidator(IRepository<Product> products,
            IRepository<Order> orders,
            IRepository<Manufacturer> manufacturers,
            IRepository<Supply> supplyes,
            IRepository<Category> categories,
            IRepository<Field> fields) : base(products)
        {
            _orders = orders;
            _manufacturers = manufacturers;
            _supplyes = supplyes;
            _categories = categories;
            _fields = fields;
        }

        protected override bool ValidateProperties(Product item)
        {
            return !(!_manufacturers.GetAll().Where(manufacturer => item.ManufacturerId == manufacturer.Id).Any()
                || !_supplyes.GetAll().Where(supply => item.SupplyId == supply.Id).Any()
                || !_categories.GetAll().Where(category => item.CategoryId == category.Id).Any()
                || item.Price < 0
                || string.IsNullOrEmpty(item.Name));
        }

        protected override bool ValidateReferences(Product item)
        {
            return !(_orders.GetAll().Any(order => order.ProductId == item.Id)
                || _fields.GetAll().Any(field => field.ProductId == item.Id));
        }

    }
}
