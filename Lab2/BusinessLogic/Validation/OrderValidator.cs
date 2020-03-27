using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System.Linq;

namespace BusinessLogic.Validation
{
    internal class OrderValidator : Validator<Order>
    {
        private readonly IRepository<Product> _products;
        private readonly IRepository<Buyer> _buyers;

        public OrderValidator(IRepository<Order> items, IRepository<Product> products, IRepository<Buyer> buyers) : base(items)
        { }

        protected override bool ValidateProperties(Order item)
        {
            return !(item.Count < 0
                || !_products.GetAll().Result.Where(product => item.ProductId == product.Id).Any()
                || !_buyers.GetAll().Result.Where(buyer => item.BuyerId == buyer.Id).Any());
        }
    }
}