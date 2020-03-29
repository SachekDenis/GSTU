using BusinessLogic.Dto;
using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.ConsoleView
{
    class ManufacturerConsoleService
    {
        protected readonly ManufacturerManager _manufacturerService;
        private readonly ConsolePrinter _printer;

        public ManufacturerConsoleService(ManufacturerManager manufacturerService)
        {
            _manufacturerService = manufacturerService;
            _printer = new ConsolePrinter();
        }

        public async Task StartConsoleLoop()
        {
            while(true)
            {
                Console.Clear();
                PrintAll();
                PrintMenu();

                var menuTab = int.Parse(Console.ReadLine());

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
        }

        public void PrintAll()
        {
            var items = _manufacturerService.GetAll();
            _printer.WriteCollectionAsTable(items);
        }

        public async Task Delete()
        {
            int id = int.Parse(Console.ReadLine());
            await _manufacturerService.Delete(id);
        }

        public async Task Add()
        {
            var manufacturerDto = CreateDto();
            await _manufacturerService.Add(manufacturerDto);
        }

        public async Task Update()
        {
            var id = int.Parse(Console.ReadLine());
            var manufacturerDto = CreateDto();
            manufacturerDto.Id = id;

            await _manufacturerService.Update(manufacturerDto);
        }

        private ManufacturerDto CreateDto()
        {
            var name = Console.ReadLine();
            var country = Console.ReadLine();

            var manufacturerDto = new ManufacturerDto()
            {
                Name = name,
                Country = country,
            };

            return manufacturerDto;
        }

        private void PrintMenu()
        {
            Console.WriteLine("1. Добавить производителя");
            Console.WriteLine("2. Удалить производителя");
            Console.WriteLine("3. Изменить производителя");
        }
    }
}
