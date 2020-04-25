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
    public class ManufacturerManager : IManager<Manufacturer>
    {
        private readonly IRepository<ManufacturerDto> _items;
        private readonly IMapper _mapper;
        private readonly IValidator<Manufacturer> _validator;

        public ManufacturerManager(IMapper mapper,
                                   IValidator<Manufacturer> manufacturerValidator,
                                   IRepository<ManufacturerDto> items)
        {
            _mapper = mapper;
            _validator = manufacturerValidator;
            _items = items;
        }

        public async Task Add(Manufacturer manufacturer)
        {
            var manufacturerDto = _mapper.Map<ManufacturerDto>(manufacturer);

            if (!_validator.Validate(manufacturer))
            {
                throw new ValidationException($"{nameof(manufacturer)} has invalid data");
            }

            await _items.Add(manufacturerDto);
            manufacturer.Id = manufacturerDto.Id;
        }

        public async Task Delete(int id)
        {
            await _items.Delete(id);
        }

        public async Task Update(Manufacturer manufacturer)
        {
            var manufacturerDto = _mapper.Map<ManufacturerDto>(manufacturer);

            if (!_validator.Validate(manufacturer))
            {
                throw new ValidationException($"{nameof(manufacturer)} has invalid data");
            }

            await _items.Update(manufacturerDto);
        }

        public async Task<IEnumerable<Manufacturer>> GetAll()
        {
            return (await _items.GetAll()).Select(item => _mapper.Map<Manufacturer>(item));
        }

        public async Task<Manufacturer> GetById(int id)
        {
            return _mapper.Map<Manufacturer>(await _items.GetById(id));
        }
    }
}