using BusinessLogic.Validation;
using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Validation
{
    class CpuValidatior : Validator<Cpu>
    {
        private readonly IRepository<Product> _products;
        public CpuValidatior(IRepository<Cpu> items, IRepository<Product> products) : base(items)
        {
            _products = products;
        }

        protected override bool ValidateProperties(Cpu item)
        {
            return !(string.IsNullOrEmpty(item.Core)
                || string.IsNullOrEmpty(item.ProcessTechnology)
                || string.IsNullOrEmpty(item.Socket)
                || item.Frequency < 0
                || item.NumberOfCores < 1);
        }

        protected override bool ValidateReferences(Cpu item)
        {
            return !_products.GetAll().Result.Where(product => product.AdditionalInformationId == item.Id).Any();
        }
    }
}
