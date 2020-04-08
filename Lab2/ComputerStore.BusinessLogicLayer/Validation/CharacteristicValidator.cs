using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class CharacteristicValidator : IValidator<Characteristic>
    {
        public bool Validate(Characteristic item)
        {
            return !string.IsNullOrEmpty(item.Name);
        }
    }
}