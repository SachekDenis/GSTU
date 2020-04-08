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
    public class SupplyManager : IManager<Supply>
    {
        private readonly IRepository<SupplyDto> _items;
        private readonly IMapper _mapper;
        private readonly IValidator<Supply> _validator;

        public SupplyManager(IMapper mapper,
            IValidator<Supply> supplyValidator,
            IRepository<SupplyDto> items)
        {
            _mapper = mapper;
            _validator = supplyValidator;
            _items = items;
        }

        public void Add(Supply supply)
        {
            var supplyDto = _mapper.Map<SupplyDto>(supply);

            if (!_validator.Validate(supply))
            {
                throw new ValidationException($"{nameof(supply)} has invalid data");
            }

            _items.Add(supplyDto);
            supply.Id = supplyDto.Id;
        }

        public void Delete(int id)
        {
            _items.Delete(id);
        }

        public void Update(Supply supply)
        {
            var supplyDto = _mapper.Map<SupplyDto>(supply);

            if (!_validator.Validate(supply))
            {
                throw new ValidationException($"{nameof(supply)} has invalid data");
            }

            _items.Update(supplyDto);
        }

        public IEnumerable<Supply> GetAll()
        {
            return _items.GetAll().Select(item => _mapper.Map<Supply>(item));
        }

        public Supply GetById(int id)
        {
            return _mapper.Map<Supply>(_items.GetById(id));
        }
    }
}