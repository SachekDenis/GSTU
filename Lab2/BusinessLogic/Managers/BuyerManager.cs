using AutoMapper;
using BusinessLogic.Managers;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Managers
{
    internal class BuyerManager:IManager<BuyerDto>
    {
        private readonly IMapper _mapper;
        private readonly Validator<Buyer> _validator;

        public BuyerManager(IMapper mapper, BuyerValidator buyerValidator)
        {
            _mapper = mapper;
            _validator = buyerValidator;
        }

        public async Task Add(BuyerDto buyerDto)
        {
            var buyer = _mapper.Map<Buyer>(buyerDto);
            await _validator.Add(buyer);
        }

        public async Task Delete(int id)
        {
            await _validator.Delete(id);
        }

        public async Task Update(BuyerDto buyerDto)
        {
            var buyer = _mapper.Map<Buyer>(buyerDto);
            await _validator.Update(buyer);
        }

        public IEnumerable<BuyerDto> GetAll()
        {
            return _validator.GetAll().Select(item => _mapper.Map<BuyerDto>(item));
        }
    }
}
