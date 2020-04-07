using System.Text.RegularExpressions;
using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class BuyerValidator : Validator<BuyerDto>
    {
        public override bool Validate(BuyerDto item)
        {
            return !(string.IsNullOrEmpty(item.FirstName)
                     || string.IsNullOrEmpty(item.SecondName)
                     || string.IsNullOrEmpty(item.Address)
                     || string.IsNullOrEmpty(item.Email)
                     || string.IsNullOrEmpty(item.PhoneNumber)
                     || !Regex.Match(item.Email, RegexHelper.EmailRegex).Success
                     || !Regex.Match(item.PhoneNumber, RegexHelper.PhoneRegex).Success);
        }
    }
}