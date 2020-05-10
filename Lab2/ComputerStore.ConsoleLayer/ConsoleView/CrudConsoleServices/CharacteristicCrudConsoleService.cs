using System;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.ConsoleLayer.ConsoleView.CrudConsoleServices
{
    public class CharacteristicCrudConsoleService : ICrudConsoleService<Characteristic>
    {
        private readonly CategoryManager _categoryManager;
        private readonly CharacteristicManager _characteristicManager;

        public CharacteristicCrudConsoleService(CharacteristicManager characteristicManager, CategoryManager categoryManager)
        {
            _characteristicManager = characteristicManager;
            _categoryManager = categoryManager;
        }

        public async Task Delete()
        {
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            await _characteristicManager.Delete(id);
        }

        public async Task Add()
        {
            var characteristicDto = await CreateModel();
            await _characteristicManager.Add(characteristicDto);
        }

        public async Task Update()
        {
            Console.WriteLine("Enter Id of characteristic to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var characteristicDto = await CreateModel();
            characteristicDto.Id = id;

            await _characteristicManager.Update(characteristicDto);
        }

        public async Task<Characteristic> CreateModel()
        {
            Console.WriteLine("Enter name of characteristic");
            var name = Console.ReadLine();
            (await _categoryManager.GetAll()).WriteCollectionAsTable();
            Console.WriteLine("Enter Id of category");
            var categoryId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var characteristicDto = new Characteristic
                                    {
                                        CategoryId = categoryId,
                                        Name = name
                                    };

            return characteristicDto;
        }
    }
}