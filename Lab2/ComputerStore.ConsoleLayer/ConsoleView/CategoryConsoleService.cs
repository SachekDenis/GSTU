using System;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    public class CategoryConsoleService : CrudConsoleService<Category>
    {
        private readonly CategoryManager _categoryManager;

        public CategoryConsoleService(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public override void StartConsoleLoop()
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

        protected override void PrintAll()
        {
            var items = _categoryManager.GetAll();
            items.WriteCollectionAsTable();
        }

        protected override void Delete()
        {
            Console.WriteLine("Enter Id of category to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _categoryManager.Delete(id);
        }

        protected override void Add()
        {
            var categoryDto = CreateModel();
            _categoryManager.Add(categoryDto);
        }

        protected override void Update()
        {
            Console.WriteLine("Enter Id of category to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var categoryDto = CreateModel();
            categoryDto.Id = id;

            _categoryManager.Update(categoryDto);
        }

        protected override Category CreateModel()
        {
            Console.WriteLine("Enter category name");
            var name = Console.ReadLine();

            var categoryDto = new Category
            {
                Name = name
            };

            return categoryDto;
        }

        protected override void PrintMenu()
        {
            Console.WriteLine("1. Add category");
            Console.WriteLine("2. Delete category");
            Console.WriteLine("3. Update category");
            Console.WriteLine("4. Back");
        }
    }
}