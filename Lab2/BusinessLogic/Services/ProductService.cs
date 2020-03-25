using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    public class ProductService
    {
        private readonly SupplyValidator _supplyValidator;
        private readonly ProductValidator _productValidator;
        private readonly FieldValidator _fieldValidator;
        private readonly IMapper _mapper;

        public ProductService(
            SupplyValidator supplyValidator,
            ProductValidator productValidator,
            FieldValidator fieldValidator,
            IMapper mapper)
        {
            _supplyValidator = supplyValidator;
            _productValidator = productValidator;
            _fieldValidator = fieldValidator;
            _mapper = mapper;
        }

        public void AddProduct(ProductDto dto)
        {
            var supply = _mapper.Map<Supply>(dto);

            _supplyValidator.Add(supply);

            var product = _mapper.Map<Product>(dto);

            product.SupplyId = supply.Id;

            foreach (var characteristic in dto.Characteristics)
            {
                var field = new Field()
                {
                    CharacteristicId = characteristic.Key,
                    ProductId = product.Id,
                    Value = characteristic.Value
                };
                _fieldValidator.Add(field);
            }

            _productValidator.Add(product);
        }

        public void DeleteProduct(int id)
        { 
            _productValidator.Delete(id);
        }

        public void UpdateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _productValidator.Update(product);
        }
        
        public IEnumerable<Product> GetAllProducts()
        {
            return _productValidator.GetAll();
        }
    }
}
