using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.ConsoleLayer.ViewModels;

namespace ComputerStore.ConsoleLayer.ConsoleView.PrintConsoleServices
{
    public class CharacteristicPrintConsoleService : IPrintConsoleService
    {
        private readonly CategoryManager _categoryManager;
        private readonly CharacteristicManager _characteristicManager;

        public CharacteristicPrintConsoleService(CharacteristicManager characteristicManager,
            CategoryManager categoryManager)
        {
            _characteristicManager = characteristicManager;
            _categoryManager = categoryManager;
        }

        public async Task PrintAll()
        {
            var categories = await _categoryManager.GetAll();

            var items = (await _characteristicManager.GetAll())
                .Select(item => new CharacteristicViewModel
                {
                    CharacteristicId = item.Id,
                    CategoryId = item.CategoryId,
                    CategoryName = categories.First(category => category.Id == item.CategoryId).Name,
                    CharacteristicName = item.Name
                });

            items.WriteCollectionAsTable();
        }
    }
}