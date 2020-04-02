using System.Linq;
using ComputerStore.DataAccessLayer.Models;
using ComputerStore.DataAccessLayer.Repo;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class SupplyValidator : Validator<Supply>
    {
        private readonly IRepository<Product> _products;
        private readonly IRepository<Supplier> _suppliers;

        public SupplyValidator(IRepository<Supply> items, IRepository<Product> products, IRepository<Supplier> suppliers) : base(items)
        {
            _products = products;
            _suppliers = suppliers;
        }

        protected override bool ValidateReferences(Supply item)
        {
            return true;
        }

        protected override bool ValidateProperties(Supply item)
        {
            return !(!_suppliers.GetAll().Where(supplier => item.SupplierId == supplier.Id).Any() 
                     || !_products.GetAll().Where(product => item.ProductId == product.Id).Any());
        }
    }
}