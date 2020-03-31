using AutoMapper;
using BusinessLogic.Managers;
using BusinessLogic.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
using DataAccessLayer.Models;

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

        public void Add(BuyerDto buyerDto)
        {
            var buyer = _mapper.Map<Buyer>(buyerDto);
            _validator.Add(buyer);
        }

        public void Delete(int id)
        {
            _validator.Delete(id);
        }

        public void Update(BuyerDto buyerDto)
        {
            var buyer = _mapper.Map<Buyer>(buyerDto);
            _validator.Update(buyer);
        }

        public IEnumerable<BuyerDto> GetAll()
        {
            return _validator.GetAll().Select(item => _mapper.Map<BuyerDto>(item));
        }
    }
}
