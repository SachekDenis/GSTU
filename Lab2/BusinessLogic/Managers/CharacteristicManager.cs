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
    class CharacteristicManager
    {
        private readonly IMapper _mapper;
        private readonly Validator<Characteristic> _validator;

        public CharacteristicManager(IMapper mapper, CharacteristicValidator characteristicValidator)
        {
            _mapper = mapper;
            _validator = characteristicValidator;
        }

        public async Task Add(CharacteristicDto characteristicDto)
        {
            var characteristic = _mapper.Map<Characteristic>(characteristicDto);
            await _validator.Add(characteristic);
        }

        public async Task Delete(int id)
        {
            await _validator.Delete(id);
        }

        public async Task Update(CharacteristicDto characteristicDto)
        {
            var characteristic = _mapper.Map<Characteristic>(characteristicDto);
            await _validator.Update(characteristic);
        }

        public IEnumerable<Characteristic> GetAll()
        {
            return _validator.GetAll();
        }
    }
}
