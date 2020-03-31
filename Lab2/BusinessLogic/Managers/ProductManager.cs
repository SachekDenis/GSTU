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

        public void Add(ProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            _productValidator.Add(product);

            dto.Id = product.Id;

            try
            {
                dto.Fields.ForEach(fieldDto =>
                {
                    var field = _mapper.Map<Field>(fieldDto);
                    field.ProductId = product.Id;
                    _fieldValidator.Add(field);
                });
            }
            catch (ValidationException)
            {
                _productValidator.Delete(product.Id);
                throw;
            }
        }

        public void Delete(int id)
        {
            _supplyValidator.GetAll()
                .Where(supply => supply.ProductId == id)
                .ToList()
                .ForEach(supply => _supplyValidator.Delete(supply.Id));
            _fieldValidator.GetAll()
                .Where(field => field.ProductId == id)
                .ToList()
                .ForEach(field => _fieldValidator.Delete(field.Id));

            _productValidator.Delete(id);

        }

        public void Update(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            productDto.Fields.ForEach(fieldDto =>
                {
                    fieldDto.Id = _fieldValidator.GetAll().First(field =>
                        field.ProductId == product.Id && field.CharacteristicId == fieldDto.CharacteristicId).Id;
                    _fieldValidator.Update(_mapper.Map<Field>(fieldDto));
                });

            _productValidator.Update(product);
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