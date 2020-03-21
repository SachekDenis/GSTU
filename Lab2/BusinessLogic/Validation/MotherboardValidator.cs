using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Validation
{
    class MotherboardValidator : Validator<Motherboard>
    {
        private readonly IRepository<Product> _products;
        public MotherboardValidator(IRepository<Motherboard> items, IRepository<Product> products) : base(items)
        {
            _products = products;
        }

        protected override bool ValidateProperties(Motherboard item)
        {
            return !(string.IsNullOrEmpty(item.Chipset)
                || string.IsNullOrEmpty(item.Audio)
                || string.IsNullOrEmpty(item.Ethernet)
                || string.IsNullOrEmpty(item.ExtensionSlots)
                || string.IsNullOrEmpty(item.Interfaces)
                || string.IsNullOrEmpty(item.RamType)
                || string.IsNullOrEmpty(item.Socket)
                || item.RamCount < 1);
        }

        protected override bool ValidateReferences(Motherboard item)
        {
            return !_products.GetAll().Result.Where(product => product.AdditionalInformationId == item.Id).Any();
        }
    }
}
