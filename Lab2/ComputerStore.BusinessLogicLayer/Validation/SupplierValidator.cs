using System.Text.RegularExpressions;
using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class SupplierValidator : IValidator<SupplierDto>
    {
        public bool Validate(SupplierDto item)
        {
            return !(string.IsNullOrEmpty(item.Name)
                     || !Regex.Match(item.Phone, RegexCollection.PhoneRegex).Success
                     || string.IsNullOrEmpty(item.Address));
        }
    }
}