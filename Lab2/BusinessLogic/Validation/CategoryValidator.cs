using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System.Linq;

namespace BusinessLogic.Validation
{
    public class CategoryValidator : Validator<Category>
    {
        private readonly IRepository<Product> _products;
        private readonly IRepository<Characteristic> _characteristics;

        public CategoryValidator(IRepository<Category> items,
            IRepository<Product> products,
            IRepository<Characteristic> characteristics) : base(items)
        {
            _products = products;
            _characteristics = characteristics;
        }

        protected override bool ValidateProperties(Category item)
        {
            return !string.IsNullOrEmpty(item.Name);
        }

        protected override bool ValidateReferences(Category item)
        {
            return !(_products.GetAll().Any(product => product.CategoryId == item.Id)
                || _characteristics.GetAll().Any(characteristic => characteristic.CategoryId == item.Id));
        }
    }
}
