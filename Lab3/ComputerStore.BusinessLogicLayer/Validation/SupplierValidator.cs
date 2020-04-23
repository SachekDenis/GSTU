using System.Text.RegularExpressions;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.BusinessLogicLayer.Validation.RegexStorage;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class SupplierValidator : IValidator<Supplier>
    {
        public bool Validate(Supplier item)
        {
            return !(string.IsNullOrEmpty(item.Name)
                     || !Regex.Match(item.Phone, RegexCollection.PhoneRegex).Success
                     || string.IsNullOrEmpty(item.Address));
        }
    }
}