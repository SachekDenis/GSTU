using System;
using System.Threading.Tasks;

namespace ConsoleApp.ConsoleView
{
    internal class MainMenuService
    {
        private readonly ManufacturerConsoleService _manufacturerConsoleService;
        private readonly SupplierConsoleService _supplierConsoleService;

        public MainMenuService(ManufacturerConsoleService manufacturerConsoleService,
            SupplierConsoleService supplierConsoleService)
        {
            _manufacturerConsoleService = manufacturerConsoleService;
            _supplierConsoleService = supplierConsoleService;
        }

        public async Task StartMainLoop()
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
                            await _manufacturerConsoleService.StartConsoleLoop();
                        }
                        break;
                    case 2:
                        {
                            await _supplierConsoleService.StartConsoleLoop();
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
