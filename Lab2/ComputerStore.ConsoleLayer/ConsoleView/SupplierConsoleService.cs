using System;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    public class SupplierConsoleService:CrudConsoleService<Supplier>
    {
        private readonly SupplierManager _supplierManager;

        public SupplierConsoleService(SupplierManager supplierManager)
        {
            _supplierManager = supplierManager;
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
            var items = _supplierManager.GetAll();
            items.WriteCollectionAsTable();
        }

        protected override void Delete()
        {
            Console.WriteLine("Enter Id of supplier to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _supplierManager.Delete(id);
        }

        protected override void Add()
        {
            var manufacturerDto = CreateModel();
            _supplierManager.Add(manufacturerDto);
        }

        protected override void Update()
        {
            Console.WriteLine("Enter Id of supplier to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var supplierDto = CreateModel();
            supplierDto.Id = id;

            _supplierManager.Update(supplierDto);
        }

        protected override Supplier CreateModel()
        {
            Console.WriteLine("Enter supplier name");
            var name = Console.ReadLine();
            Console.WriteLine("Enter supplier address");
            var address = Console.ReadLine();
            Console.WriteLine("Enter supplier phone");
            var phone = Console.ReadLine();

            var supplierDto = new Supplier
            {
                Name = name,
                Address = address,
                Phone = phone
            };

            return supplierDto;
        }

        protected override void PrintMenu()
        {
            Console.WriteLine("1. Add supplier");
            Console.WriteLine("2. Delete supplier");
            Console.WriteLine("3. Update supplier");
            Console.WriteLine("4. Back");
        }
    }
}