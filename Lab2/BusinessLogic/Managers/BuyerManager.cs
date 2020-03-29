using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    internal class BuyerManager
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

        public IEnumerable<Buyer> GetAll()
        {
            return _validator.GetAll();
        }
    }
}
