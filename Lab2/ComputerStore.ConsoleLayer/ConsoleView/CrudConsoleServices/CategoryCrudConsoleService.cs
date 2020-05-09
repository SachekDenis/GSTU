using System;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.ConsoleLayer.ConsoleView.CrudConsoleServices
{
    public class CategoryCrudConsoleService : ICrudConsoleService<Category>
    {
        private readonly CategoryManager _categoryManager;

        public CategoryCrudConsoleService(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public async Task Delete()
        {
            Console.WriteLine("Enter Id of category to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            await _categoryManager.Delete(id);
        }

        public async Task Add()
        {
            var categoryDto = await CreateModel();
            await _categoryManager.Add(categoryDto);
        }

        public async Task Update()
        {
            Console.WriteLine("Enter Id of category to update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var categoryDto = await CreateModel();
            categoryDto.Id = id;

            await _categoryManager.Update(categoryDto);
        }

        public async Task<Category> CreateModel()
        {
            return await Task.Run(() =>
                                  {
                                      Console.WriteLine("Enter category name");
                                      var name = Console.ReadLine();

                                      var categoryDto = new Category
                                                        {
                                                            Name = name
                                                        };

                                      return categoryDto;
                                  });
        }
    }
}