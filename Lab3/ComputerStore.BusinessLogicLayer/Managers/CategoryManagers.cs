using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.BusinessLogicLayer.Validation;
using ComputerStore.DataAccessLayer.Models;
using ComputerStore.DataAccessLayer.Repo;

namespace ComputerStore.BusinessLogicLayer.Managers
{
    public class CategoryManager : IManager<Category>
    {
        private readonly IRepository<CategoryDto> _items;
        private readonly IMapper _mapper;
        private readonly IValidator<Category> _validator;

        public CategoryManager(IMapper mapper,
            IValidator<Category> categoryValidator,
            IRepository<CategoryDto> items)
        {
            _mapper = mapper;
            _validator = categoryValidator;
            _items = items;
        }

        public async Task Add(Category category)
        {
            var categoryDto = _mapper.Map<CategoryDto>(category);

            if (!_validator.Validate(category))
            {
                throw new ValidationException($"{nameof(category)} has invalid data");
            }

            await _items.Add(categoryDto);

            category.Id = categoryDto.Id;
        }

        public async Task Delete(int id)
        {
            await _items.Delete(id);
        }

        public async Task Update(Category category)
        {
            var categoryDto = _mapper.Map<CategoryDto>(category);

            if (!_validator.Validate(category))
            {
                throw new ValidationException($"{nameof(category)} has invalid data");
            }

            await _items.Update(categoryDto);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return (await _items.GetAll()).Select(item => _mapper.Map<Category>(item));
        }

        public async Task<Category> GetById(int id)
        {
            return _mapper.Map<Category>(await _items.GetById(id));
        }
    }
}