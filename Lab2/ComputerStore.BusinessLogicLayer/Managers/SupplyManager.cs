using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.BusinessLogicLayer.Validation;
using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Managers
{
    public class SupplyManager : IManager<SupplyDto>
    {
        private readonly IMapper _mapper;
        private readonly Validator<Supply> _validator;

        public SupplyManager(IMapper mapper, SupplyValidator supplyValidator)
        {
            _mapper = mapper;
            _validator = supplyValidator;
        }

        public void Add(SupplyDto supplyDto)
        {
            var supply = _mapper.Map<Supply>(supplyDto);
            _validator.Add(supply);
            supplyDto.Id = supply.Id;
        }

        public void Delete(int id)
        {
            _validator.Delete(id);
        }

        public void Update(SupplyDto supplyDto)
        {
            var supply = _mapper.Map<Supply>(supplyDto);
            _validator.Update(supply);
        }

        public IEnumerable<SupplyDto> GetAll()
        {
            return _validator.GetAll().Select(item => _mapper.Map<SupplyDto>(item));
        }

        public SupplyDto GetById(int id)
        {
            return _mapper.Map<SupplyDto>(_validator.GetById(id));
        }
    }
}
