using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.ConsoleLayer.ViewModels;

namespace ComputerStore.ConsoleLayer.ConsoleView.PrintConsoleServices
{
    public class ProductListPrintConsoleService : IPrintConsoleService
    {
        private readonly CategoryManager _categoryManager;
        private readonly ProductManager _productManager;

        public ProductListPrintConsoleService(CategoryManager categoryManager, ProductManager productManager)
        {
            _categoryManager = categoryManager;
            _productManager = productManager;
        }

        public async Task PrintAll()
        {
            var categories = await _categoryManager.GetAll();
            var items = (await _productManager.GetAll()).Select(item => new ProductListViewModel
                                                                        {
                                                                            Id = item.Id,
                                                                            Category = categories
                                                                                       .First(category => category.Id == item.CategoryId)
                                                                                       .Name,
                                                                            Name = item.Name
                                                                        });

            items.WriteCollectionAsTable();
        }
    }
}