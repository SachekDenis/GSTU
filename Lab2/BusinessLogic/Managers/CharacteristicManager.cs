using AutoMapper;
using BusinessLogic.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers
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

        public IEnumerable<CharacteristicDto> GetAll()
        {
            return _validator.GetAll().Select(item => _mapper.Map<CharacteristicDto>(item));
        }
    }
}
