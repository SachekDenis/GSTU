using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class CategoryValidator : IValidator<Category>
    {
        public bool Validate(Category item)
        {
            return !string.IsNullOrEmpty(item.Name);
        }
    }
}