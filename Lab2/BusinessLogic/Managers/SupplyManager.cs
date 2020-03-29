﻿using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Managers
{
    internal class SupplyManager:IManager<SupplyDto>
    {
        private readonly IMapper _mapper;
        private readonly Validator<Supply> _validator;

        public SupplyManager(IMapper mapper, SupplyValidator supplyValidator)
        {
            _mapper = mapper;
            _validator = supplyValidator;
        }

        public async Task Add(SupplyDto supplyDto)
        {
            var supply = _mapper.Map<Supply>(supplyDto);
            await _validator.Add(supply);
        }

        public async Task Delete(int id)
        {
            await _validator.Delete(id);
        }

        public async Task Update(SupplyDto supplyDto)
        {
            var supply = _mapper.Map<Supply>(supplyDto);
            await _validator.Update(supply);
        }

        public IEnumerable<SupplyDto> GetAll()
        {
            return _validator.GetAll().Select(item => _mapper.Map<SupplyDto>(item));
        }
    }
}
