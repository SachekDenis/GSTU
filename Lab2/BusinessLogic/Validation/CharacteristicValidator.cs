using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Validation
{
    public class CharacteristicValidator : Validator<Characteristic>
    {
        private readonly IRepository<Category> _categories;
        private readonly IRepository<Field> _fields;
        public CharacteristicValidator(IRepository<Characteristic> items,
            IRepository<Category> categories,
            IRepository<Field> fields
            ) : base(items)
        {
            _categories = categories;
            _fields = fields;
        }

        protected override bool ValidateProperties(Characteristic item)
        {
            return !(string.IsNullOrEmpty(item.Name)
                || _categories.GetAll().Result.All(category => category.Id != item.CategoryId));
        }

        protected override bool ValidateReferences(Characteristic item)
        {
            return _fields.GetAll().Result.All(field => field.CharacteristicId != item.Id);
        }
    }
}
