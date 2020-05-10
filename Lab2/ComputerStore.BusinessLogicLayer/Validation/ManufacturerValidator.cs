using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class ManufacturerValidator : IValidator<Manufacturer>
    {
        public bool Validate(Manufacturer item)
        {
            return !(string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.Country));
        }
    }
}