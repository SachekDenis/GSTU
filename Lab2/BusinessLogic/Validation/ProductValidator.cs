using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Validation
{
    public class ProductValidator : Validator<Product>
    {
        private readonly IRepository<Order> _orders;
        private readonly IRepository<Manufacturer> _manufacturers;
        private readonly IRepository<Supply> _supplyes;
        private readonly IRepository<Ram> _rams;
        private readonly IRepository<Motherboard> _motherboards;
        private readonly IRepository<Cpu> _cpus;
        private readonly IRepository<GraphicalCard> _graphicalCards;

        public ProductValidator(IRepository<Product> products,
            IRepository<Order> orders,
            IRepository<Manufacturer> manufacturers,
            IRepository<Supply> supplyes,
            IRepository<Ram> rams,
            IRepository<Motherboard> motherboards,
            IRepository<Cpu> cpus,
            IRepository<GraphicalCard> graphicalCards) : base(products)
        {
            _orders = orders;
            _manufacturers = manufacturers;
            _supplyes = supplyes;
            _rams = rams;
            _motherboards = motherboards;
            _cpus = cpus;
            _graphicalCards = graphicalCards;
        }

        protected override bool ValidateProperties(Product item)
        {
            return !(!_manufacturers.GetAll().Result.Where(manufacturer => item.ManufacturerId == manufacturer.Id).Any()
                || !_supplyes.GetAll().Result.Where(supply => item.SupplyId == supply.Id).Any()
                || !(_rams.GetAll().Result.Where(ram => item.AdditionalInformationId == ram.Id).Any()
                || _motherboards.GetAll().Result.Where(ram => item.AdditionalInformationId == ram.Id).Any()
                || _cpus.GetAll().Result.Where(ram => item.AdditionalInformationId == ram.Id).Any()
                || _graphicalCards.GetAll().Result.Where(ram => item.AdditionalInformationId == ram.Id).Any())
                || item.Price < 0
                || string.IsNullOrEmpty(item.Name));
        }

        protected override bool ValidateReferences(Product item)
        {
            return _orders.GetAll().Result.Any(order => order.ProductId == item.Id);
        }

    }
}
