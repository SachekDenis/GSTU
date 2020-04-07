using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.BusinessLogicLayer.Validation;
using ComputerStore.DataAccessLayer.Models;
using ComputerStore.DataAccessLayer.Repo;

namespace ComputerStore.BusinessLogicLayer.Managers
{
    public class ProductManager : IManager<Product>
    {
        private readonly IRepository<FieldDto> _fields;
        private readonly FieldValidator _fieldValidator;
        private readonly IMapper _mapper;
        private readonly IRepository<ProductDto> _products;
        private readonly ProductValidator _productValidator;
        private readonly IRepository<SupplyDto> _supplies;

        public ProductManager(
            ProductValidator productValidator,
            FieldValidator fieldValidator,
            IMapper mapper,
            IRepository<FieldDto> fields,
            IRepository<ProductDto> products,
            IRepository<SupplyDto> supplies)
        {
            _productValidator = productValidator;
            _fieldValidator = fieldValidator;
            _mapper = mapper;
            _fields = fields;
            _products = products;
            _supplies = supplies;
        }

        public void Add(Product product)
        {
            var productDto = _mapper.Map<ProductDto>(product);

            if (!_productValidator.Validate(productDto))
            {
                throw new ValidationException($"{nameof(productDto)} has invalid data");
            }

            _products.Add(productDto);

            try
            {
                product.Fields.ToList().ForEach(field =>
                {
                    var fieldDto = _mapper.Map<FieldDto>(field);
                    fieldDto.ProductId = productDto.Id;

                    if (!_fieldValidator.Validate(fieldDto))
                    {
                        throw new ValidationException($"{nameof(fieldDto)} has invalid data");
                    }

                    _fields.Add(fieldDto);
                });
            }
            catch (ValidationException)
            {
                _products.Delete(productDto.Id);
                throw;
            }

            product.Id = productDto.Id;
        }

        public void Delete(int id)
        {
            _supplies.GetAll()
                .Where(supplyDto => supplyDto.ProductId == id)
                .ToList()
                .ForEach(supplyDto => _supplies.Delete(supplyDto.Id));
            _fields.GetAll()
                .Where(fieldDto => fieldDto.ProductId == id)
                .ToList()
                .ForEach(fieldDto => _fields.Delete(fieldDto.Id));

            _products.Delete(id);
        }

        public void Update(Product product)
        {
            var productDto = _mapper.Map<ProductDto>(product);

            if (!_productValidator.Validate(productDto))
            {
                throw new ValidationException($"{nameof(product)} has invalid data");
            }

            var fieldList = product.Fields.ToList();

            fieldList.ForEach(field =>
            {
                field.Id = _fields.GetAll().First(fieldDto =>
                    fieldDto.ProductId == product.Id && fieldDto.CharacteristicId == field.CharacteristicId).Id;

                var fieldDto = _mapper.Map<FieldDto>(field);

                if (!_fieldValidator.Validate(fieldDto))
                {
                    throw new ValidationException($"{nameof(fieldDto)} has invalid data");
                }

                _fields.Update(fieldDto);
            });

            product.Fields = fieldList;

            _products.Update(productDto);
        }

        public IEnumerable<Product> GetAll()
        {
            return _products.GetAll().Select(MapProduct);
        }

        public Product GetById(int id)
        {
            return MapProduct(_products.GetById(id));
        }

        private Product MapProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            product.Fields = _fields.GetAll()
                .Where(fieldDto => fieldDto.ProductId == product.Id)
                .Select(fieldDto => _mapper.Map<Field>(fieldDto))
                .ToList();
            return product;
        }
    }
}