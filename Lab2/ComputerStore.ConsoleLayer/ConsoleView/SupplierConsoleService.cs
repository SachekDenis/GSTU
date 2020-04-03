using System;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.ConsoleLayer.ConsoleView
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
                    Console.WriteLine($"Ошибка валидации. Сообщение {e.Message}");
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
            var items = _supplierManager.GetAll();
            _printer.WriteCollectionAsTable(items);
        }

        private void Delete()
        {
            Console.WriteLine("Введите Id поставщика для удаления");
            int id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _supplierManager.Delete(id);
        }

        private void Add()
        {
            var manufacturerDto = CreateModel();
            _supplierManager.Add(manufacturerDto);
        }

        private void Update()
        {
            Console.WriteLine("Введите Id поставщика для обновления");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var manufacturerDto = CreateModel();
            manufacturerDto.Id = id;

            _supplierManager.Update(manufacturerDto);
        }

        private static SupplierDto CreateModel()
        {
            Console.WriteLine("Введите наименование поставщика");
            var name = Console.ReadLine();
            Console.WriteLine("Введите адрес поставщика");
            var address = Console.ReadLine();
            Console.WriteLine("Введите телефон поставщика");
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
            Console.WriteLine("4. Назад");
        }
    }
}