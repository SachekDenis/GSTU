using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.ConsoleView
{
    class MainMenuService
    {
        private readonly ProductManager _productService;
        private readonly ManufacturerConsoleService _manufacturerConsoleService;

        public MainMenuService(ProductManager productService, 
            ManufacturerConsoleService manufacturerConsoleService)
        {
            _productService = productService;
            _manufacturerConsoleService = manufacturerConsoleService;
        }

        public async Task StartMainLoop()
        {
            while (true)
            {
                Console.Clear();
                PrintMenu();
                var menuTab = int.Parse(Console.ReadLine());

                switch (menuTab)
                {
                    case 1:
                        {
                            await _manufacturerConsoleService.StartConsoleLoop();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine("1. Добавить производителя");
            Console.WriteLine("2. Добавить поставщика");
            Console.WriteLine("3. Добавить характеристику");
            Console.WriteLine("4. Добавить категорию");
            Console.WriteLine("5. Добавить товар");
        }
    }
}
