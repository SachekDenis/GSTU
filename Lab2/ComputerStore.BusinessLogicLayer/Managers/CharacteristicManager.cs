using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.BusinessLogicLayer.Validation;
using ComputerStore.DataAccessLayer.Models;
using ComputerStore.DataAccessLayer.Repo;

namespace ComputerStore.BusinessLogicLayer.Managers
{
    public class CharacteristicManager : IManager<Characteristic>
    {
        private readonly IRepository<CharacteristicDto> _items;
        private readonly IMapper _mapper;
        private readonly IValidator<CharacteristicDto> _validator;

        public CharacteristicManager(IMapper mapper,
            IValidator<CharacteristicDto> characteristicValidator,
            IRepository<CharacteristicDto> items)
        {
            _mapper = mapper;
            _validator = characteristicValidator;
            _items = items;
        }

        public void Add(Characteristic characteristic)
        {
            var characteristicDto = _mapper.Map<CharacteristicDto>(characteristic);

            if (!_validator.Validate(characteristicDto))
            {
                throw new ValidationException($"{nameof(characteristicDto)} has invalid data");
            }

            _items.Add(characteristicDto);
            characteristic.Id = characteristicDto.Id;
        }

        public void Delete(int id)
        {
            _items.Delete(id);
        }

        public void Update(Characteristic characteristic)
        {
            var characteristicDto = _mapper.Map<CharacteristicDto>(characteristic);

            if (!_validator.Validate(characteristicDto))
            {
                throw new ValidationException($"{nameof(characteristicDto)} has invalid data");
            }

            _items.Update(characteristicDto);
        }

        public IEnumerable<Characteristic> GetAll()
        {
            return _items.GetAll().Select(item => _mapper.Map<Characteristic>(item));
        }

        public Characteristic GetById(int id)
        {
            return _mapper.Map<Characteristic>(_items.GetById(id));
        }
    }
}