using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IValidator<Characteristic> _validator;

        public CharacteristicManager(IMapper mapper,
            IValidator<Characteristic> characteristicValidator,
            IRepository<CharacteristicDto> items)
        {
            _mapper = mapper;
            _validator = characteristicValidator;
            _items = items;
        }

        public async Task Add(Characteristic characteristic)
        {
            var characteristicDto = _mapper.Map<CharacteristicDto>(characteristic);

            if (!_validator.Validate(characteristic))
            {
                throw new ValidationException($"{nameof(characteristic)} has invalid data");
            }

            await _items.Add(characteristicDto);
            characteristic.Id = characteristicDto.Id;
        }

        public async Task Delete(int id)
        {
            await _items.Delete(id);
        }

        public async Task Update(Characteristic characteristic)
        {
            var characteristicDto = _mapper.Map<CharacteristicDto>(characteristic);

            if (!_validator.Validate(characteristic))
            {
                throw new ValidationException($"{nameof(characteristic)} has invalid data");
            }

            await _items.Update(characteristicDto);
        }

        public async Task<IEnumerable<Characteristic>> GetAll()
        {
            return (await _items.GetAll()).Select(item => _mapper.Map<Characteristic>(item));
        }

        public async Task<Characteristic> GetById(int id)
        {
            return _mapper.Map<Characteristic>(await _items.GetById(id));
        }
    }
}