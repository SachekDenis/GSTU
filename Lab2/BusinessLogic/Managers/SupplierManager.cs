using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    class SupplierManager
    {
        private readonly IMapper _mapper;
        private readonly Validator<Supplier> _validator;

        public SupplierManager(IMapper mapper, SupplierValidator supplierValidator)
        {
            _mapper = mapper;
            _validator = supplierValidator;
        }

        public async Task Add(SupplierDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            await _validator.Add(supplier);
        }

        public async Task Delete(int id)
        {
            await _validator.Delete(id);
        }

        public async Task Update(BuyerDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            await _validator.Update(supplier);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _validator.GetAll();
        }
    }
}
