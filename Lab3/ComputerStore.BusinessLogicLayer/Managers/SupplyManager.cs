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

        public async Task Add(Supply supply)
        {
            var supplyDto = _mapper.Map<SupplyDto>(supply);

            if (!_validator.Validate(supply))
            {
                throw new ValidationException($"{nameof(supply)} has invalid data");
            }

            await _items.Add(supplyDto);
            supply.Id = supplyDto.Id;
        }

        public async Task Delete(int id)
        {
            await _items.Delete(id);
        }

        public async Task Update(Supply supply)
        {
            var supplyDto = _mapper.Map<SupplyDto>(supply);

            if (!_validator.Validate(supply))
            {
                throw new ValidationException($"{nameof(supply)} has invalid data");
            }

            await _items.Update(supplyDto);
        }

        public async Task<IEnumerable<Supply>> GetAll()
        {
            return (await _items.GetAll()).Select(item => _mapper.Map<Supply>(item));
        }

        public async Task<Supply> GetById(int id)
        {
            return _mapper.Map<Supply>(await _items.GetById(id));
        }
    }
}