using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class FieldValidator : IValidator<Field>
    {
        public bool Validate(Field item)
        {
            return !string.IsNullOrEmpty(item.Value);
        }
    }
}