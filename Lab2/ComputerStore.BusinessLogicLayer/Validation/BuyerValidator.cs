using ComputerStore.DataAccessLayer.Models;
using ComputerStore.DataAccessLayer.Repo;
using System.Text.RegularExpressions;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class BuyerValidator : Validator<Buyer>
    {
        private const string EmailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        private const string PhoneRegex = @"\(?\d{3}\)?[-\.]? *\d{3}[-\.]? *[-\.]?\d{4}";
        public BuyerValidator(IRepository<Buyer> items) : base(items)
        { }

        protected override bool ValidateReferences(Buyer item)
        {
            return true;
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
