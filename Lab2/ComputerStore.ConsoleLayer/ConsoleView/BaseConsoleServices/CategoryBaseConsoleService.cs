using System;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.ConsoleLayer.ConsoleView.PrintConsoleServices;

namespace ComputerStore.ConsoleLayer.ConsoleView.BaseConsoleServices
{
    public class CategoryBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Category> _crudCategoryService;
        private readonly IPrintConsoleService _printCategoryService;

        public CategoryBaseConsoleService(ICrudConsoleService<Category> crudCategoryService, CategoryPrintConsoleService printCategoryService)
        {
            _crudCategoryService = crudCategoryService;
            _printCategoryService = printCategoryService;
        }

        public async Task StartConsoleLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    await _printCategoryService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            await _crudCategoryService.Add();
                            break;
                        case 2:
                            await _crudCategoryService.Delete();
                            break;
                        case 3:
                            await _crudCategoryService.Update();
                            break;
                        case 4:
                            return;
                    }
                }
                catch (ValidationException e)
                {
                    Console.WriteLine($"Validation error. Message: {e.Message}");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }


        public void PrintMenu()
        {
            Console.WriteLine("1. Add category");
            Console.WriteLine("2. Delete category");
            Console.WriteLine("3. Update category");
            Console.WriteLine("4. Back");
        }
    }
}