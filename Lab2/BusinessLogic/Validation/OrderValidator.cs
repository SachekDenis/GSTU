using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System.Linq;

namespace BusinessLogic.Validation
{
    internal class OrderValidator : Validator<Order>
    {
        private readonly IRepository<Product> _products;
        private readonly IRepository<Buyer> _buyers;

        public OrderValidator(IRepository<Order> items,
            IRepository<Product> products, 
            IRepository<Buyer> buyers) : base(items)
        {
            _products = products;
            _buyers = buyers;
        }

        protected override bool ValidateProperties(Order item)
        {
            return !(item.Count < 0
                || !_products.GetAll().Where(product => item.ProductId == product.Id).Any()
                || !_buyers.GetAll().Where(buyer => item.BuyerId == buyer.Id).Any());
        }
    }
}