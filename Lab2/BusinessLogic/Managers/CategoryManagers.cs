using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    internal class CategoryManager
    {
        private readonly IMapper _mapper;
        private readonly Validator<Category> _validator;

        public CategoryManager(IMapper mapper, CategoryValidator categoryValidator)
        {
            _mapper = mapper;
            _validator = categoryValidator;
        }

        public async Task Add(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _validator.Add(category);
        }

        public async Task Delete(int id)
        {
            await _validator.Delete(id);
        }

        public async Task Update(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _validator.Update(category);
        }

        public IEnumerable<Category> GetAll()
        {
            return _validator.GetAll();
        }
    }
}
