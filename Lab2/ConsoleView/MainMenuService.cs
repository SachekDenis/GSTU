using System;
using System.Threading.Tasks;

namespace ConsoleApp.ConsoleView
{
    internal class MainMenuService
    {
        private readonly ManufacturerConsoleService _manufacturerConsoleService;
        private readonly SupplierConsoleService _supplierConsoleService;
        private readonly CategoryConsoleService _categoryConsoleService;
        private readonly CharacteristicConsoleService _characteristicConsoleService;
        private readonly ProductListConsoleService _productListConsoleService;

        public MainMenuService(ManufacturerConsoleService manufacturerConsoleService,
            SupplierConsoleService supplierConsoleService,
            CategoryConsoleService categoryConsoleService,
            CharacteristicConsoleService characteristicConsoleService,
            ProductListConsoleService productListConsoleService)
        {
            _manufacturerConsoleService = manufacturerConsoleService;
            _supplierConsoleService = supplierConsoleService;
            _categoryConsoleService = categoryConsoleService;
            _productListConsoleService = productListConsoleService;
            _characteristicConsoleService = characteristicConsoleService;
        }

        public void StartMainLoop()
        {
            while (true)
            {
                Console.Clear();
                PrintMenu();
                var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                switch (menuTab)
                {
                    case 1:
                        {
                             _manufacturerConsoleService.StartConsoleLoop();
                        }
                        break;
                    case 2:
                        {
                             _supplierConsoleService.StartConsoleLoop();
                        }
                        break;
                    case 3:
                        {
                             _characteristicConsoleService.StartConsoleLoop();
                        }
                        break;
                    case 4:
                        {
                             _categoryConsoleService.StartConsoleLoop();
                        }
                        break;
                    case 5:
                        {
                             _productListConsoleService.StartConsoleLoop();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Производители");
            Console.WriteLine("2. Поставщики");
            Console.WriteLine("3. Характеристики");
            Console.WriteLine("4. Категории");
            Console.WriteLine("5. Товары");
        }
    }
}
