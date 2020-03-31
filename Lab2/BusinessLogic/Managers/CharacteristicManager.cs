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
    }
}
