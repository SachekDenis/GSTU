using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class CategoryValidator : Validator<CategoryDto>
    {
        public override bool Validate(CategoryDto item)
        {
            return !string.IsNullOrEmpty(item.Name);
        }
    }
}