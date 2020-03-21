using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    public class AdminService
    {
        private readonly RamValidator _ramValidator;
        private readonly SupplyValidator _supplyValidator;
        private readonly SupplierValidator _supplierValidator;
        private readonly ProductValidator _productValidator;
        private readonly IMapper _mapper;

        public AdminService(RamValidator ramValidator,
            SupplyValidator supplyValidator,
            ProductValidator productValidator,
            SupplierValidator supplierValidator,
            IMapper mapper)
        {
            _ramValidator = ramValidator;
            _supplyValidator = supplyValidator;
            _productValidator = productValidator;
            _supplierValidator = supplierValidator;
            _mapper = mapper;
        }

        public void AddProduct<TDto,TEntity>(TDto ramDto, Validator<TEntity> validator) where TEntity:Entity
        {
            Supply supply = _mapper.Map<Supply>(ramDto);
            _supplyValidator.Add(supply);
            TEntity ram = _mapper.Map<TEntity>(ramDto);
            validator.Add(ram);
            Product ramProduct = _mapper.Map<Product>(ramDto);
            ramProduct.SupplyId = supply.Id;
            ramProduct.AdditionalInformationId = ram.Id;
            _productValidator.Add(ramProduct);
        }
    }
}
