using BusinessLogic.Managers;
using ConsoleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.ConsoleView
{
    class ProductListConsoleService
    {
        private readonly ProductManager _productManager;
        private readonly CategoryManager _categoryManager;
        private readonly ConsolePrinter _printer;
        private readonly ProductConsoleService _productConsoleService;

        public ProductListConsoleService(ProductManager productManager,
            CategoryManager categoryManager,
            ProductConsoleService productConsoleService)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _productConsoleService = productConsoleService;
            _printer = new ConsolePrinter();
        }

        public async Task StartConsoleLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                        {
                            var productId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                            await _productConsoleService.StartConsoleLoop(productId);
                        }
                            break;
                        case 2:
                            return;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }

        private void PrintAll()
        {
            var categories = _categoryManager.GetAll();
            var items = _productManager.GetAll().Select(item => new ProductListViewModel()
            {
                Id = item.Id,
                Category = categories.First(category => category.Id == item.CategoryId).Name,
                Name = item.Name
            });
            _printer.WriteCollectionAsTable(items);
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Вывести детали товара");
            Console.WriteLine("2. Назад");
        }
    }
}
