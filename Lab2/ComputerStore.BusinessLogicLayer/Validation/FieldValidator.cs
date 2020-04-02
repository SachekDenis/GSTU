using System.Linq;
using ComputerStore.DataAccessLayer.Models;
using ComputerStore.DataAccessLayer.Repo;

namespace ComputerStore.BusinessLogicLayer.Validation
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

        protected override bool ValidateReferences(Field item)
        {
            return true;
        }

        protected override bool ValidateProperties(Field item)
        {
            return !(string.IsNullOrEmpty(item.Value)
                || !_products.GetAll().Any(product => product.Id == item.ProductId)
                || !_characteristics.GetAll().Any(characteristic => characteristic.Id == item.CharacteristicId));
        }
    }
}
