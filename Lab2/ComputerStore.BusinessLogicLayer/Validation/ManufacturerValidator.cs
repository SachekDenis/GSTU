using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class ManufacturerValidator : Validator<ManufacturerDto>
    {
        public override bool Validate(ManufacturerDto item)
        {
            return !(string.IsNullOrEmpty(item.Name)
                     || string.IsNullOrEmpty(item.Country));
        }
    }
}