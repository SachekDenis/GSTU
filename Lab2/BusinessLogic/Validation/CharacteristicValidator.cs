using System.Linq;
using DataAccessLayer.Models;
using DataAccessLayer.Repo;

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
                || !_categories.GetAll().Any(category => category.Id == item.CategoryId));
        }

        protected override bool ValidateReferences(Characteristic item)
        {
            return !_fields.GetAll().Any(field => field.CharacteristicId == item.Id);
        }
    }
}
