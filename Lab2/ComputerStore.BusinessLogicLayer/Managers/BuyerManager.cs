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
    public class BuyerManager : IManager<Buyer>
    {
        private readonly IRepository<BuyerDto> _items;
        private readonly IMapper _mapper;
        private readonly IValidator<Buyer> _validator;

        public BuyerManager(IMapper mapper, IValidator<Buyer> buyerValidator, IRepository<BuyerDto> items)
        {
            _mapper = mapper;
            _validator = buyerValidator;
            _items = items;
        }

        public async Task Add(Buyer buyer)
        {
            var buyerDto = _mapper.Map<BuyerDto>(buyer);

            if (!_validator.Validate(buyer))
            {
                throw new ValidationException($"{nameof(buyer)} has invalid data");
            }

            await _items.Add(buyerDto);
            buyer.Id = buyerDto.Id;
        }

        public async Task Delete(int id)
        {
            await _items.Delete(id);
        }

        public async Task Update(Buyer buyer)
        {
            var buyerDto = _mapper.Map<BuyerDto>(buyer);

            if (!_validator.Validate(buyer))
            {
                throw new ValidationException($"{nameof(buyer)} has invalid data");
            }

            await _items.Update(buyerDto);
        }

        public async Task<IEnumerable<Buyer>> GetAll()
        {
            return (await _items.GetAll()).Select(item => _mapper.Map<Buyer>(item));
        }

        public async Task<Buyer> GetById(int id)
        {
            return _mapper.Map<Buyer>(await _items.GetById(id));
        }
    }
}