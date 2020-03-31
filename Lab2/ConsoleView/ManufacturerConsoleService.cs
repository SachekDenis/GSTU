using BusinessLogic.Managers;
using System;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace ConsoleApp.ConsoleView
{
    internal class ManufacturerConsoleService
    {
        private readonly ManufacturerManager _manufacturerManager;
        private readonly ConsolePrinter _printer;

        public ManufacturerConsoleService(ManufacturerManager manufacturerManager)
        {
            _manufacturerManager = manufacturerManager;
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
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }

        private void PrintAll()
        {
            var items = _manufacturerManager.GetAll();
            _printer.WriteCollectionAsTable(items);
        }

        private void Delete()
        {
            Console.WriteLine("Введите Id для удаления");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
             _manufacturerManager.Delete(id);
        }

        private void Add()
        {
            var manufacturerDto = CreateModel();
             _manufacturerManager.Add(manufacturerDto);
        }

        private void Update()
        {
            Console.WriteLine("Введите Id для обновления");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var manufacturerDto = CreateModel();
            manufacturerDto.Id = id;

             _manufacturerManager.Update(manufacturerDto);
        }

        private static ManufacturerDto CreateModel()
        {
            Console.WriteLine("Введите наименование производителя");
            var name = Console.ReadLine();
            Console.WriteLine("Введите страну производителя");
            var country = Console.ReadLine();

            var manufacturerDto = new ManufacturerDto()
            {
                Name = name,
                Country = country,
            };

            return manufacturerDto;
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Добавить производителя");
            Console.WriteLine("2. Удалить производителя");
            Console.WriteLine("3. Изменить производителя");
            Console.WriteLine("4. Назад");
        }
    }
}
