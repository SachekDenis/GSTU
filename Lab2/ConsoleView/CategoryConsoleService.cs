using System;
using System.Threading.Tasks;
using BusinessLogic.Managers;
using BusinessLogic.Models;

namespace ConsoleApp.ConsoleView
{
    public class CategoryConsoleService
    {
        private readonly CategoryManager _categoryManager;
        private readonly ConsolePrinter _printer;

        public CategoryConsoleService(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
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
                            await Add();
                            break;
                        case 2:
                            await Delete();
                            break;
                        case 3:
                            await Update();
                            break;
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
            var items = _categoryManager.GetAll();
            _printer.WriteCollectionAsTable(items);
        }

        private async Task Delete()
        {
            int id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            await _categoryManager.Delete(id);
        }

        private async Task Add()
        {
            var categoryDto = CreateModel();
            await _categoryManager.Add(categoryDto);
        }

        private async Task Update()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var categoryDto = CreateModel();
            categoryDto.Id = id;

            await _categoryManager.Update(categoryDto);
        }

        private static CategoryDto CreateModel()
        {
            var name = Console.ReadLine();

            var categoryDto = new CategoryDto()
            {
                Name = name,
            };

            return categoryDto;
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Добавить категорию");
            Console.WriteLine("2. Удалить категорию");
            Console.WriteLine("3. Изменить категорию");
        }
    }
}