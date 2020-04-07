using System.Text.RegularExpressions;
using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class SupplierValidator : Validator<SupplierDto>
    {
        public override bool Validate(SupplierDto item)
        {
            return !(string.IsNullOrEmpty(item.Name)
                     || !Regex.Match(item.Phone, RegexHelper.PhoneRegex).Success
                     || string.IsNullOrEmpty(item.Address));
        }
    }
}