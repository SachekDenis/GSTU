using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;

namespace ComputerStore.ConsoleLayer.ConsoleView.PrintConsoleServices
{
    public class CategoryPrintConsoleService : IPrintConsoleService
    {
        private readonly CategoryManager _categoryManager;

        public CategoryPrintConsoleService(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public async Task PrintAll()
        {
            var items = await _categoryManager.GetAll();
            items.WriteCollectionAsTable();
        }
    }
}