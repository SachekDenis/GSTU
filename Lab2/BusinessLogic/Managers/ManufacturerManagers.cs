using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ManufacturerManager
    {
        private readonly IMapper _mapper;
        private readonly Validator<Manufacturer> _validator;

        public ManufacturerManager(IMapper mapper, ManufacturerValidator manufacturerValidator)
        {
            _mapper = mapper;
            _validator = manufacturerValidator;
        }

        public async Task Add(ManufacturerDto manufacturerDto)
        {
            var manufaturer = _mapper.Map<Manufacturer>(manufacturerDto);
            await _validator.Add(manufaturer);
        }

        public async Task Delete(int id)
        {
            await _validator.Delete(id);
        }

        public async Task Update(ManufacturerDto manufacturerDto)
        {
            var manufacturer = _mapper.Map<Manufacturer>(manufacturerDto);
            await _validator.Update(manufacturer);
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            return _validator.GetAll();
        }
    }
}
