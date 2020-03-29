using AutoMapper;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Model;

namespace BusinessLogic.Services
{
    class CharacteristicService
    {
        private readonly IMapper _mapper;
        private readonly Validator<Characteristic> _validator;

        public CharacteristicService(IMapper mapper, CharacteristicValidator characteristicValidator)
        {
            _mapper = mapper;
            _validator = characteristicValidator;
        }

        public void Add(CharacteristicDto characteristicDto)
        {
            var characteristic = _mapper.Map<Characteristic>(characteristicDto);
            _validator.Add(characteristic);
        }

        public void Delete(int id)
        {
            _validator.Delete(id);
        }

        public void Update(CharacteristicDto characteristicDto)
        {
            var characteristic = _mapper.Map<Characteristic>(characteristicDto);
            _validator.Update(characteristic);
        }

        public IEnumerable<Characteristic> GetAll()
        {
            return _validator.GetAll();
        }
    }
}
