using ComputerStore.DataAccessLayer.Models;
using ComputerStore.DataAccessLayer.Repo;
using System.Linq;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class SupplierValidator : Validator<Supplier>
    {
        private readonly IRepository<Supply> _supplies;
        public SupplierValidator(IRepository<Supplier> items, IRepository<Supply> supplies) : base(items)
        {
            _supplies = supplies;
        }

        protected override bool ValidateProperties(Supplier item)
        {
            return !(string.IsNullOrEmpty(item.Name)
                || string.IsNullOrEmpty(item.Phone)
                || string.IsNullOrEmpty(item.Address));
        }

        protected override bool ValidateReferences(Supplier item)
        {
            return !_supplies.GetAll().Any(supply => supply.SupplierId == item.Id);
        }
    }
}
