using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class FieldValidator : IValidator<FieldDto>
    {
        public bool Validate(FieldDto item)
        {
            return !string.IsNullOrEmpty(item.Value);
        }
    }
}