using ComputerStore.DataAccessLayer.Models;
using ComputerStore.DataAccessLayer.Repo;
using System.Linq;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class OrderValidator : Validator<Order>
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

        protected override bool ValidateReferences(Order item)
        {
            return true;
        }

        protected override bool ValidateProperties(Order item)
        {
            return !(item.Amount < 0
                || !_products.GetAll().Any(product => item.ProductId == product.Id)
                || !_buyers.GetAll().Any(buyer => item.BuyerId == buyer.Id));
        }
    }
}