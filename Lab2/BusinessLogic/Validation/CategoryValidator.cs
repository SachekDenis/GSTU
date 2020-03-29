using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return !(_products.GetAll().Result.Any(product => product.CategoryId == item.Id)
                || _characteristics.GetAll().Result.Any(characteristic => characteristic.CategoryId == item.Id));
        }
    }
}
