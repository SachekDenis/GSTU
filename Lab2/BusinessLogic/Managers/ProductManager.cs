using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductManager
    {
        private readonly SupplyValidator _supplyValidator;
        private readonly ProductValidator _productValidator;
        private readonly FieldValidator _fieldValidator;
        private readonly IMapper _mapper;

        public ProductManager(
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

        public async Task AddProduct(ProductDto dto)
        {
            var supply = _mapper.Map<Supply>(dto);

            await _supplyValidator.Add(supply);

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
                await _fieldValidator.Add(field);
            }

            await _productValidator.Add(product);
        }

        public async Task DeleteProduct(int id)
        {
            await _productValidator.Delete(id);
        }

        public async Task UpdateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productValidator.Update(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productValidator.GetAll();
        }
    }
}