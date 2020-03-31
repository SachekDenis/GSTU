using AutoMapper;
using BusinessLogic.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers
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
    }
}
