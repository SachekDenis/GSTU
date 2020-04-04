using AutoMapper;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.BusinessLogicLayer.Validation;
using ComputerStore.DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace ComputerStore.BusinessLogicLayer.Managers
{
    public class CategoryManager : IManager<CategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly Validator<Category> _validator;

        public CategoryManager(IMapper mapper, CategoryValidator categoryValidator)
        {
            _mapper = mapper;
            _validator = categoryValidator;
        }

        public void Add(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _validator.Add(category);
        }

        public void Delete(int id)
        {
            _validator.Delete(id);
        }

        public void Update(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _validator.Update(category);
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return _validator.GetAll().Select(item => _mapper.Map<CategoryDto>(item));
        }

        public CategoryDto GetById(int id)
        {
            return _mapper.Map<CategoryDto>(_validator.GetById(id));
        }
    }
}
