using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class FieldValidator : Validator<FieldDto>
    {
        public override bool Validate(FieldDto item)
        {
            return !string.IsNullOrEmpty(item.Value);
        }
    }
}