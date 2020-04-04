using ComputerStore.DataAccessLayer.Models;
using ComputerStore.DataAccessLayer.Repo;
using System.Linq;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class SupplierValidator : Validator<Supplier>
    {
        private readonly IRepository<Supply> _supplyes;
        public SupplierValidator(IRepository<Supplier> items, IRepository<Supply> supplyes) : base(items)
        {
            _supplyes = supplyes;
        }

        protected override bool ValidateProperties(Supplier item)
        {
            return !(string.IsNullOrEmpty(item.Name)
                || string.IsNullOrEmpty(item.Phone)
                || string.IsNullOrEmpty(item.Address));
        }

        protected override bool ValidateReferences(Supplier item)
        {
            return !_supplyes.GetAll().Where(supply => supply.SupplierId == item.Id).Any();
        }
    }
}
