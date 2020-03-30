using BusinessLogic.Managers;
using System;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace ConsoleApp.ConsoleView
{
    internal class ManufacturerConsoleService
    {
        private readonly ManufacturerManager _manufacturerService;
        private readonly ConsolePrinter _printer;

        public ManufacturerConsoleService(ManufacturerManager manufacturerService)
        {
            _manufacturerService = manufacturerService;
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
            var items = _manufacturerService.GetAll();
            _printer.WriteCollectionAsTable(items);
        }

        private async Task Delete()
        {
            Console.WriteLine("Введите Id для удаления");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            await _manufacturerService.Delete(id);
        }

        private async Task Add()
        {
            var manufacturerDto = CreateModel();
            await _manufacturerService.Add(manufacturerDto);
        }

        private async Task Update()
        {
            Console.WriteLine("Введите Id для обновления");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var manufacturerDto = CreateModel();
            manufacturerDto.Id = id;

            await _manufacturerService.Update(manufacturerDto);
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
