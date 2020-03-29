using AutoMapper;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Model;

namespace BusinessLogic.Services
{
    class BuyerService
    {
        private readonly IMapper _mapper;
        private readonly Validator<Buyer> _validator;

        public BuyerService(IMapper mapper, BuyerValidator buyerValidator)
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

        public IEnumerable<Buyer> GetAll()
        {
            return _validator.GetAll();
        }
    }
}
