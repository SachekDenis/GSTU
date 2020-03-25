using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    class CategoryService
    {
        private readonly IMapper _mapper;
        private readonly Validator<Category> _validator;

        public CategoryService(IMapper mapper, CategoryValidator categoryValidator)
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

        public IEnumerable<Category> GetAll()
        {
            return _validator.GetAll();
        }
    }
}
