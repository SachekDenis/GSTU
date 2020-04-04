using System.Linq;
using ComputerStore.DataAccessLayer.Models;
using ComputerStore.DataAccessLayer.Repo;
using System.Text.RegularExpressions;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class BuyerValidator : Validator<Buyer>
    {
        private readonly IRepository<Order> _orders;
        private const string EmailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        private const string PhoneRegex = @"\(?\d{3}\)?[-\.]? *\d{3}[-\.]? *[-\.]?\d{4}";
        public BuyerValidator(IRepository<Buyer> items, IRepository<Order> orders) : base(items)
        {
            _orders = orders;
        }

        protected override bool ValidateReferences(Buyer item)
        {
            return !_orders.GetAll().Any(order => order.BuyerId == item.Id);
        }

        protected override bool ValidateProperties(Buyer item)
        {
            return !(string.IsNullOrEmpty(item.FirstName)
                     || string.IsNullOrEmpty(item.SecondName)
                     || string.IsNullOrEmpty(item.Address)
                     || string.IsNullOrEmpty(item.Email)
                     || string.IsNullOrEmpty(item.PhoneNumber)
                     || !Regex.Match(item.Email, EmailRegex).Success
                     || !Regex.Match(item.PhoneNumber, PhoneRegex).Success);
        }
    }
}
