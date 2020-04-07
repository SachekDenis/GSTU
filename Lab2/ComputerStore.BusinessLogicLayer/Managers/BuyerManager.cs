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
    public class BuyerManager : IManager<Buyer>
    {
        private readonly IRepository<BuyerDto> _items;
        private readonly IMapper _mapper;
        private readonly Validator<BuyerDto> _validator;

        public BuyerManager(IMapper mapper, BuyerValidator buyerValidator, IRepository<BuyerDto> items)
        {
            _mapper = mapper;
            _validator = buyerValidator;
            _items = items;
        }

        public void Add(Buyer buyer)
        {
            var buyerDto = _mapper.Map<BuyerDto>(buyer);

            if (!_validator.Validate(buyerDto))
            {
                throw new ValidationException($"{nameof(buyerDto)} has invalid data");
            }

            _items.Add(buyerDto);
            buyer.Id = buyerDto.Id;
        }

        public void Delete(int id)
        {
            _items.Delete(id);
        }

        public void Update(Buyer buyer)
        {
            var buyerDto = _mapper.Map<BuyerDto>(buyer);

            if (!_validator.Validate(buyerDto))
            {
                throw new ValidationException($"{nameof(buyerDto)} has invalid data");
            }

            _items.Update(buyerDto);
        }

        public IEnumerable<Buyer> GetAll()
        {
            return _items.GetAll().Select(item => _mapper.Map<Buyer>(item));
        }

        public Buyer GetById(int id)
        {
            return _mapper.Map<Buyer>(_items.GetById(id));
        }
    }
}