using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Managers;
using BusinessLogic.Models;
using ConsoleApp.ViewModels;

namespace ConsoleApp.ConsoleView
{
    public class CharacteristicConsoleService
    {
        private readonly CharacteristicManager _characteristicManager;
        private readonly ConsolePrinter _printer;
        private readonly CategoryManager _categoryManager;

        public CharacteristicConsoleService(CharacteristicManager characteristicManager, CategoryManager categoryManager)
        {
            _characteristicManager = characteristicManager;
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
            var items = _characteristicManager.GetAll().ToList()
                .Select(item => new CharacteristicViewModel
                {
                    CharacteristicId = item.Id,
                    CategoryId = item.CategoryId,
                    CategoryName = _categoryManager.GetAll().First(category=>category.Id == item.CategoryId).Name,
                    CharacteristicName = item.Name
                }).ToList();

            _printer.WriteCollectionAsTable(items);
        }

        private async Task Delete()
        {
            int id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            await _characteristicManager.Delete(id);
        }

        private async Task Add()
        {
            var characteristicDto = CreateModel();
            await _characteristicManager.Add(characteristicDto);
        }

        private async Task Update()
        {
            Console.WriteLine("Введите Id характеристики для изменения");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _printer.WriteCollectionAsTable(_categoryManager.GetAll());
            var characteristicDto = CreateModel();
            characteristicDto.Id = id;

            await _characteristicManager.Update(characteristicDto);
        }

        private static CharacteristicDto CreateModel()
        {
            Console.WriteLine("Введите имя характеристики");
            var name = Console.ReadLine();
            Console.WriteLine("Введите Id категории");
            var categoryId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var characteristicDto = new CharacteristicDto
            {
                CategoryId = categoryId,
                Name = name
            };

            return characteristicDto;
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Добавить характеристику");
            Console.WriteLine("2. Удалить характеристику");
            Console.WriteLine("3. Изменить характеристику");
        }
    }
}