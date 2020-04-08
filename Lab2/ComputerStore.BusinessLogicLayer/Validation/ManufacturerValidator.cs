using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class ManufacturerValidator : IValidator<ManufacturerDto>
    {
        public bool Validate(ManufacturerDto item)
        {
            return !(string.IsNullOrEmpty(item.Name)
                     || string.IsNullOrEmpty(item.Country));
        }
    }
}