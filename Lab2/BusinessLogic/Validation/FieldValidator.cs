using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Validation
{
    public class FieldValidator : Validator<Field>
    {
        private readonly IRepository<Product> _products;
        private readonly IRepository<Characteristic> _characteristics;
        public FieldValidator(IRepository<Field> items,
            IRepository<Product> products,
            IRepository<Characteristic> characteristics
            ) : base(items)
        {
            _products = products;
            _characteristics = characteristics;
        }

        protected override bool ValidateProperties(Field item)
        {
            return !(string.IsNullOrEmpty(item.Value)
                || !_products.GetAll().Result.Any(product => product.Id == item.ProductId)
                || !_characteristics.GetAll().Result.Any(characteristic => characteristic.Id == item.CharacteristicId));
        }
    }
}
