using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.Models;

namespace ComputerStore.WebUI.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryViewModel CreateCategoryViewModel(this Category category)
        {
            return new CategoryViewModel
                   {
                       Id = category.Id,
                       Name = category.Name
                   };
        }
    }
}