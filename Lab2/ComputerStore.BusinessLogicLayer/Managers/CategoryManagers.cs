using System.Collections.Generic;
using System.Linq;
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
        private readonly IValidator<CategoryDto> _validator;

        public CategoryManager(IMapper mapper, 
            IValidator<CategoryDto> categoryValidator,
            IRepository<CategoryDto> items)
        {
            _mapper = mapper;
            _validator = categoryValidator;
            _items = items;
        }

        public void Add(Category category)
        {
            var categoryDto = _mapper.Map<CategoryDto>(category);

            if (!_validator.Validate(categoryDto))
            {
                throw new ValidationException($"{nameof(categoryDto)} has invalid data");
            }

            _items.Add(categoryDto);

            category.Id = categoryDto.Id;
        }

        public void Delete(int id)
        {
            _items.Delete(id);
        }

        public void Update(Category category)
        {
            var categoryDto = _mapper.Map<CategoryDto>(category);

            if (!_validator.Validate(categoryDto))
            {
                throw new ValidationException($"{nameof(categoryDto)} has invalid data");
            }

            _items.Update(categoryDto);
        }

        public IEnumerable<Category> GetAll()
        {
            return _items.GetAll().Select(item => _mapper.Map<Category>(item));
        }

        public Category GetById(int id)
        {
            return _mapper.Map<Category>(_items.GetById(id));
        }
    }
}