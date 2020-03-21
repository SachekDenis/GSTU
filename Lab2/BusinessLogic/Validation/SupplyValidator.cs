using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Validation
{
    public class SupplyValidator : Validator<Supply>
    {
        private readonly IRepository<Product> _products;
        private readonly IRepository<Supplier> _suppliers;
        public SupplyValidator(IRepository<Supply> items, IRepository<Product> products,IRepository<Supplier> suppliers) : base(items)
        {
            _products = products;
            _suppliers = suppliers;
        }

        protected override bool ValidateProperties(Supply item)
        {
            return _suppliers.GetAll().Result.Where(supplier => item.SupplierId == supplier.Id).Any();
        }

        protected override bool ValidateReferences(Supply item)
        {
            return !_products.GetAll().Result.Where(product => product.SupplyId == item.Id).Any();
        }
    }
}
