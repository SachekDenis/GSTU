using System;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.ConsoleLayer.ConsoleView.CrudConsoleServices
{
    public class ManufacturerCrudConsoleService : ICrudConsoleService<Manufacturer>
    {
        private readonly ManufacturerManager _manufacturerManager;

        public ManufacturerCrudConsoleService(ManufacturerManager manufacturerManager)
        {
            _manufacturerManager = manufacturerManager;
        }

        public async Task Delete()
        {
            Console.WriteLine("Enter Id of manufacturer for delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            await _manufacturerManager.Delete(id);
        }

        public async Task Add()
        {
            var manufacturerDto = await CreateModel();
            await _manufacturerManager.Add(manufacturerDto);
        }

        public async Task Update()
        {
            Console.WriteLine("Enter Id of manufacturer for update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var manufacturerDto = await CreateModel();
            manufacturerDto.Id = id;

            await _manufacturerManager.Update(manufacturerDto);
        }

        public async Task<Manufacturer> CreateModel()
        {
            return await Task.Run(() =>
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
            });
        }
    }
}