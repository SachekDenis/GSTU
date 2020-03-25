using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    class ManufacturerService
    {
        private readonly IMapper _mapper;
        private readonly Validator<Manufacturer> _validator;

        public ManufacturerService(IMapper mapper, ManufacturerValidator manufacturerValidator)
        {
            _mapper = mapper;
            _validator = manufacturerValidator;
        }

        public void Add(ManufacturerDto manufacturerDto)
        {
            var manufaturer = _mapper.Map<Manufacturer>(manufacturerDto);
            _validator.Add(manufaturer);
        }

        public void Delete(int id)
        {
            _validator.Delete(id);
        }

        public void Update(ManufacturerDto manufacturerDto)
        {
            var product = _mapper.Map<Manufacturer>(manufacturerDto);
            _validator.Update(product);
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            return _validator.GetAll();
        }
    }
}
