using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.BusinessLogicLayer.Validation;
using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Managers
{
    public class CharacteristicManager:IManager<CharacteristicDto>
    {
        private readonly IMapper _mapper;
        private readonly Validator<Characteristic> _validator;

        public CharacteristicManager(IMapper mapper, CharacteristicValidator characteristicValidator)
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

        public IEnumerable<CharacteristicDto> GetAll()
        {
            return _validator.GetAll().Select(item => _mapper.Map<CharacteristicDto>(item));
        }

        public CharacteristicDto GetById(int id)
        {
            return _mapper.Map<CharacteristicDto>(_validator.GetById(id));
        }
    }
}
