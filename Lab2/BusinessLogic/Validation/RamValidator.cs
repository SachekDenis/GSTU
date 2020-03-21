using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Validation
{
    public class RamValidator : Validator<Ram>
    {
        private readonly IRepository<Product> _products;
        public RamValidator(IRepository<Ram> items, IRepository<Product> products) : base(items)
        {
            _products = products;
        }

        protected override bool ValidateProperties(Ram item)
        {
            return !(string.IsNullOrEmpty(item.Timings)
                || string.IsNullOrEmpty(item.Type)
                || item.Frequency < 0
                || item.Volume < 0);
        }

        protected override bool ValidateReferences(Ram item)
        {
            return !_products.GetAll().Result.Where(product => product.AdditionalInformationId == item.Id).Any();
        }
    }
}
