using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.BusinessLogicLayer.Validation;
using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Managers
{
    public class ManufacturerManager : IManager<ManufacturerDto>
    {
        private readonly IMapper _mapper;
        private readonly Validator<Manufacturer> _validator;

        public ManufacturerManager(IMapper mapper, ManufacturerValidator manufacturerValidator)
        {
            _mapper = mapper;
            _validator = manufacturerValidator;
        }

        public void Add(ManufacturerDto manufacturerDto)
        {
            var manufacturer = _mapper.Map<Manufacturer>(manufacturerDto);
            _validator.Add(manufacturer);
        }

        public void Delete(int id)
        {
            _validator.Delete(id);
        }

        public void Update(ManufacturerDto manufacturerDto)
        {
            var manufacturer = _mapper.Map<Manufacturer>(manufacturerDto);
            _validator.Update(manufacturer);
        }

        public IEnumerable<ManufacturerDto> GetAll()
        {
            return _validator.GetAll().Select(item => _mapper.Map<ManufacturerDto>(item));
        }

        public ManufacturerDto GetById(int id)
        {
            return _mapper.Map<ManufacturerDto>(_validator.GetById(id));
        }
    }
}
