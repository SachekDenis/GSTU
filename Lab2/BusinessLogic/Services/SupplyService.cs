using AutoMapper;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Model;

namespace BusinessLogic.Services
{
    class SupplyService
    {
        private readonly IMapper _mapper;
        private readonly Validator<Supply> _validator;

        public SupplyService(IMapper mapper, SupplyValidator supplyValidator)
        {
            _mapper = mapper;
            _validator = supplyValidator;
        }

        public void Add(SupplyDto supplyDto)
        {
            var supply = _mapper.Map<Supply>(supplyDto);
            _validator.Add(supply);
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

        public IEnumerable<Supply> GetAll()
        {
            return _validator.GetAll();
        }
    }
}
