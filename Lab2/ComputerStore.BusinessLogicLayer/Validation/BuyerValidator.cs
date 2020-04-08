using System.Text.RegularExpressions;
using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class BuyerValidator : IValidator<BuyerDto>
    {
        public bool Validate(BuyerDto item)
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