using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class CharacteristicValidator : Validator<CharacteristicDto>
    {
        public override bool Validate(CharacteristicDto item)
        {
            return !string.IsNullOrEmpty(item.Name);
        }
    }
}