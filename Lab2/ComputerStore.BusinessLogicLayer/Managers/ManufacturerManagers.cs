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
    public class ManufacturerManager : IManager<Manufacturer>
    {
        private readonly IRepository<ManufacturerDto> _items;
        private readonly IMapper _mapper;
        private readonly IValidator<ManufacturerDto> _validator;

        public ManufacturerManager(IMapper mapper,
            ManufacturerValidator manufacturerValidator,
            IRepository<ManufacturerDto> items)
        {
            _mapper = mapper;
            _validator = manufacturerValidator;
            _items = items;
        }

        public void Add(Manufacturer manufacturer)
        {
            var manufacturerDto = _mapper.Map<ManufacturerDto>(manufacturer);

            if (!_validator.Validate(manufacturerDto))
            {
                throw new ValidationException($"{nameof(manufacturerDto)} has invalid data");
            }

            _items.Add(manufacturerDto);
            manufacturer.Id = manufacturerDto.Id;
        }

        public void Delete(int id)
        {
            _items.Delete(id);
        }

        public void Update(Manufacturer manufacturer)
        {
            var manufacturerDto = _mapper.Map<ManufacturerDto>(manufacturer);

            if (!_validator.Validate(manufacturerDto))
            {
                throw new ValidationException($"{nameof(manufacturerDto)} has invalid data");
            }

            _items.Update(manufacturerDto);
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            return _items.GetAll().Select(item => _mapper.Map<Manufacturer>(item));
        }

        public Manufacturer GetById(int id)
        {
            return _mapper.Map<Manufacturer>(_items.GetById(id));
        }
    }
}