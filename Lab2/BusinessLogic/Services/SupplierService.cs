using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    class SupplierService
    {
        private readonly IMapper _mapper;
        private readonly Validator<Supplier> _validator;

        public SupplierService(IMapper mapper, SupplierValidator supplierValidator)
        {
            _mapper = mapper;
            _validator = supplierValidator;
        }

        public void Add(SupplierDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            _validator.Add(supplier);
        }

        public void Delete(int id)
        {
            _validator.Delete(id);
        }

        public void Update(BuyerDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            _validator.Update(supplier);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _validator.GetAll();
        }
    }
}
