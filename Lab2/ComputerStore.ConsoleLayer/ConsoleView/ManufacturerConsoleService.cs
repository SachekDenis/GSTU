using System;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    internal class ManufacturerConsoleService:IConsoleService
    {
        private readonly ManufacturerManager _manufacturerManager;

        public ManufacturerConsoleService(ManufacturerManager manufacturerManager)
        {
            _manufacturerManager = manufacturerManager;
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
            var items = _manufacturerManager.GetAll();
            items.WriteCollectionAsTable();
        }

        private void Delete()
        {
            Console.WriteLine("Enter Id of manufacturer for delete");
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
            Console.WriteLine("Enter Id of manufacturer for update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var manufacturerDto = CreateModel();
            manufacturerDto.Id = id;

            _manufacturerManager.Update(manufacturerDto);
        }

        private static Manufacturer CreateModel()
        {
            Console.WriteLine("Enter name of manufacturer");
            var name = Console.ReadLine();
            Console.WriteLine("Enter country of manufacturer");
            var country = Console.ReadLine();

            var manufacturerDto = new Manufacturer
            {
                Name = name,
                Country = country
            };

            return manufacturerDto;
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Add manufacturer");
            Console.WriteLine("2. Delete manufacturer");
            Console.WriteLine("3. Update manufacturer");
            Console.WriteLine("4. Back");
        }
    }
}