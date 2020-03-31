using System;
using AutoMapper;
using BusinessLogic.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Exception;
using BusinessLogic.Models;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers
{
    public class ProductManager : IManager<ProductDto>
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

        public async Task Add(ProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            await _productValidator.Add(product);

            try
            {
                dto.Fields.ForEach(async fieldDto =>
                {
                    var field = _mapper.Map<Field>(fieldDto);
                    field.ProductId = product.Id;
                    await _fieldValidator.Add(field);
                });
            }
            catch (ValidationException e)
            {
                await _productValidator.Delete(product.Id);
                throw;
            }
        }

        public async Task Delete(int id)
        {
            await _productValidator.Delete(id);
            _fieldValidator.GetAll()
                           .Where(field => field.ProductId == id)
                           .ToList()
                           .ForEach(async field => await _fieldValidator.Delete(field.Id));
        }

        public async Task Update(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _fieldValidator.GetAll()
                           .Where(field => field.ProductId == product.Id)
                           .ToList()
                           .ForEach(async field => await _fieldValidator.Update(field));
            await _productValidator.Update(product);
        }

        public IEnumerable<ProductDto> GetAll()
        {
            return _productValidator.GetAll().Select(item =>
            {
                var productDto = _mapper.Map<ProductDto>(item);
                productDto.Fields = _fieldValidator.GetAll()
                                                   .Where(field => field.ProductId == item.Id)
                                                   .Select(field => _mapper.Map<FieldDto>(field))
                                                   .ToList();
                return productDto;
            });
        }
    }
}