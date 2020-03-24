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
        private readonly SupplyValidator _supplyValidator;
        private readonly SupplierValidator _supplierValidator;
        private readonly ProductValidator _productValidator;
        private readonly ManufacturerValidator _manufacturerValidator;
        private readonly IMapper _mapper;

        public AdminService(
            SupplyValidator supplyValidator,
            ProductValidator productValidator,
            SupplierValidator supplierValidator,
            ManufacturerValidator manufacturerValidator,
            IMapper mapper)
        {
            _supplyValidator = supplyValidator;
            _productValidator = productValidator;
            _supplierValidator = supplierValidator;
            _manufacturerValidator = manufacturerValidator;
            _mapper = mapper;
        }

        private void AddProduct<TDto, TEntity>(TDto dto, Validator<TEntity> validator) where TEntity : Entity
        {
            Supply supply = _mapper.Map<Supply>(dto);
            _supplyValidator.Add(supply);

            TEntity characteristics = _mapper.Map<TEntity>(dto);
            validator.Add(characteristics);

            Product ramProduct = _mapper.Map<Product>(dto);

            ramProduct.SupplyId = supply.Id;
            ramProduct.AdditionalInformationId = characteristics.Id;

            _productValidator.Add(ramProduct);
        }

        public void DeleteProduct(int id)
        {
            var product = _productValidator.GetById(id);
            var characteristics = _characteristicsValidator.GetById(product.AdditionalInformationId);
            _productValidator.Delete(id);
            
        }

        public void AddSupplier(SupplierDto supplierDto)
        {
            Supplier supplier = _mapper.Map<Supplier>(supplierDto);
            _supplierValidator.Add(supplier);
        }

        public void AddManufaturer(ManufacturerDto manufacturerDto)
        {
            Manufacturer manufaturer = _mapper.Map<Manufacturer>(manufacturerDto);
            _manufacturerValidator.Add(manufaturer);
        }
    }
}
