using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class CharacteristicValidator : IValidator<CharacteristicDto>
    {
        public bool Validate(CharacteristicDto item)
        {
            return !string.IsNullOrEmpty(item.Name);
        }
    }
}