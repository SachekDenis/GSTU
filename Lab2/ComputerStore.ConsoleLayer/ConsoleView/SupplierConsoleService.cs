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
            var items = _supplierManager.GetAll();
            _printer.WriteCollectionAsTable(items);
        }

        private void Delete()
        {
            Console.WriteLine("Enter Id of supplier to delete");
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
            Console.WriteLine("Enter Id of supplier to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var supplierDto = CreateModel();
            supplierDto.Id = id;

            _supplierManager.Update(supplierDto);
        }

        private static SupplierDto CreateModel()
        {
            Console.WriteLine("Enter supplier name");
            var name = Console.ReadLine();
            Console.WriteLine("Enter supplier address");
            var address = Console.ReadLine();
            Console.WriteLine("Enter supplier phone");
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
            Console.WriteLine("1. Add supplier");
            Console.WriteLine("2. Delete supplier");
            Console.WriteLine("3. Update supplier");
            Console.WriteLine("4. Back");
        }
    }
}