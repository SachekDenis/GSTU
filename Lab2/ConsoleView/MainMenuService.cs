using BusinessLogic.Managers;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.ConsoleView
{
    internal class MainMenuService
    {
        private readonly ManufacturerConsoleService _manufacturerConsoleService;

        public MainMenuService(ManufacturerConsoleService manufacturerConsoleService)
        {
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
            Console.WriteLine("1. Производители");
            Console.WriteLine("2. Поставщики");
            Console.WriteLine("3. Характеристики");
            Console.WriteLine("4. Категории");
            Console.WriteLine("5. Товары");
        }
    }
}
