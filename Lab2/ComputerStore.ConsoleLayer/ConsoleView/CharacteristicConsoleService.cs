using System;
using System.Linq;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.ConsoleLayer.ViewModels;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    public class CharacteristicConsoleService:IConsoleService
    {
        private readonly CategoryManager _categoryManager;
        private readonly CharacteristicManager _characteristicManager;

        public CharacteristicConsoleService(CharacteristicManager characteristicManager,
            CategoryManager categoryManager)
        {
            _characteristicManager = characteristicManager;
            _categoryManager = categoryManager;
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
            var items = _characteristicManager.GetAll().ToList()
                .Select(item => new CharacteristicViewModel
                {
                    CharacteristicId = item.Id,
                    CategoryId = item.CategoryId,
                    CategoryName = _categoryManager.GetById(item.CategoryId).Name,
                    CharacteristicName = item.Name
                }).ToList();

            items.WriteCollectionAsTable();
        }

        private void Delete()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _characteristicManager.Delete(id);
        }

        private void Add()
        {
            var characteristicDto = CreateModel();
            _characteristicManager.Add(characteristicDto);
        }

        private void Update()
        {
            Console.WriteLine("Enter Id of characteristic to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var characteristicDto = CreateModel();
            characteristicDto.Id = id;

            _characteristicManager.Update(characteristicDto);
        }

        private Characteristic CreateModel()
        {
            Console.WriteLine("Enter name of characteristic");
            var name = Console.ReadLine();
            _categoryManager.GetAll().WriteCollectionAsTable();
            Console.WriteLine("Enter Id of category");
            var categoryId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var characteristicDto = new Characteristic
            {
                CategoryId = categoryId,
                Name = name
            };

            return characteristicDto;
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Add characteristic");
            Console.WriteLine("2. Delete characteristic");
            Console.WriteLine("3. Update characteristic");
            Console.WriteLine("4. Back");
        }
    }
}