using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Validation
{
    class GraphicalCardValidator : Validator<GraphicalCard>
    {
        private readonly IRepository<Product> _products;

        public GraphicalCardValidator(IRepository<GraphicalCard> items, IRepository<Product> products) : base(items)
        {
            _products = products;
        }

        protected override bool ValidateProperties(GraphicalCard item)
        {
            return !(string.IsNullOrEmpty(item.GraphicalProcessor)
                || string.IsNullOrEmpty(item.VramType)
                || item.GpuFrequency < 0
                || item.VramFrequency < 0
                || item.VramSize < 0);
        }

        protected override bool ValidateReferences(GraphicalCard item)
        {
            return !_products.GetAll().Result.Where(product => product.AdditionalInformationId == item.Id).Any();
        }
    }
}
