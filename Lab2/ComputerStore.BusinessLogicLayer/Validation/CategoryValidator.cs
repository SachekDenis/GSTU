using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class CategoryValidator : IValidator<CategoryDto>
    {
        public bool Validate(CategoryDto item)
        {
            return !string.IsNullOrEmpty(item.Name);
        }
    }
}