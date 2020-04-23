using System.Text.RegularExpressions;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.BusinessLogicLayer.Validation.RegexStorage;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class BuyerValidator : IValidator<Buyer>
    {
        public bool Validate(Buyer item)
        {
            return !(string.IsNullOrEmpty(item.FirstName)
                     || string.IsNullOrEmpty(item.SecondName)
                     || string.IsNullOrEmpty(item.Address)
                     || string.IsNullOrEmpty(item.Email)
                     || string.IsNullOrEmpty(item.PhoneNumber)
                     || !Regex.Match(item.Email, RegexCollection.EmailRegex).Success
                     || !Regex.Match(item.PhoneNumber, RegexCollection.PhoneRegex).Success);
        }
    }
}