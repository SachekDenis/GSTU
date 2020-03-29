using System;
using System.Threading.Tasks;
using BusinessLogic.Managers;
using BusinessLogic.Models;

namespace ConsoleApp.ConsoleView
{
    public class SupplierConsoleService
    {
        private readonly SupplierManager _supplierManager;
        private readonly ConsolePrinter _printer;

        public SupplierConsoleService(SupplierManager supplierManager)
        {
            _supplierManager = supplierManager;
            _printer = new ConsolePrinter();
        }

        public async Task StartConsoleLoop()
        {
            while (true)
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
        }

        private void PrintAll()
        {
            var items = _supplierManager.GetAll();
            _printer.WriteCollectionAsTable(items);
        }

        private async Task Delete()
        {
            int id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            await _supplierManager.Delete(id);
        }

        private async Task Add()
        {
            var manufacturerDto = CreateModel();
            await _supplierManager.Add(manufacturerDto);
        }

        private async Task Update()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var manufacturerDto = CreateModel();
            manufacturerDto.Id = id;

            await _supplierManager.Update(manufacturerDto);
        }

        private static SupplierDto CreateModel()
        {
            var name = Console.ReadLine();
            var address = Console.ReadLine();
            var phone = Console.ReadLine();

            var supplierDto = new SupplierDto()
            {
                Name = name,
                Address = address,
                Phone = phone
            };

            return supplierDto;
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Добавить поставщика");
            Console.WriteLine("2. Удалить поставщика");
            Console.WriteLine("3. Изменить поставщика");
        }
    }
}