using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using System;

namespace ComputerStore.ConsoleLayer.ConsoleView
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

        public void StartConsoleLoop()
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
                            Add();
                            break;
                        case 2:
                            Delete();
                            break;
                        case 3:
                            Update();
                            break;
                        case 4:
                            return;
                        default:
                            break;
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

        private void PrintAll()
        {
            var items = _categoryManager.GetAll();
            _printer.WriteCollectionAsTable(items);
        }

        private void Delete()
        {
            Console.WriteLine("Enter Id of category to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _categoryManager.Delete(id);
        }

        private void Add()
        {
            var categoryDto = CreateModel();
            _categoryManager.Add(categoryDto);
        }

        private void Update()
        {
            Console.WriteLine("Enter Id of category to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var categoryDto = CreateModel();
            categoryDto.Id = id;

            _categoryManager.Update(categoryDto);
        }

        private static CategoryDto CreateModel()
        {
            Console.WriteLine("Enter category name");
            var name = Console.ReadLine();

            var categoryDto = new CategoryDto()
            {
                Name = name,
            };

            return categoryDto;
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Add category");
            Console.WriteLine("2. Delete category");
            Console.WriteLine("3. Update category");
            Console.WriteLine("4. Back");
        }
    }
}