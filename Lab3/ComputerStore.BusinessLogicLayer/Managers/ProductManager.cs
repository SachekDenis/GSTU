using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IValidator<Field> _fieldValidator;
        private readonly IMapper _mapper;
        private readonly IRepository<ProductDto> _products;
        private readonly IValidator<Product> _productValidator;

        public ProductManager(
            IValidator<Product> productValidator,
            IValidator<Field> fieldValidator,
            IMapper mapper,
            IRepository<FieldDto> fields,
            IRepository<ProductDto> products)
        {
            _productValidator = productValidator;
            _fieldValidator = fieldValidator;
            _mapper = mapper;
            _fields = fields;
            _products = products;
        }

        public async Task Add(Product product)
        {
            var productDto = _mapper.Map<ProductDto>(product);

            if (!_productValidator.Validate(product))
            {
                throw new ValidationException($"{nameof(product)} has invalid data");
            }

            await _products.Add(productDto);

            try
            {
                foreach (var field in product.Fields)
                {
                    var fieldDto = _mapper.Map<FieldDto>(field);
                    fieldDto.ProductId = productDto.Id;

                    if (!_fieldValidator.Validate(field))
                    {
                        throw new
                            ValidationException($"{nameof(field)} has invalid data");
                    }

                    await _fields.Add(fieldDto);
                }
            }
            catch (ValidationException)
            {
                await _products.Delete(productDto.Id);
                throw;
            }

            product.Id = productDto.Id;
        }

        public async Task Delete(int id)
        {
            foreach (var fieldDto in (await _fields.GetAll()).Where(fieldDto => fieldDto.ProductId == id))
            {
                await _fields.Delete(fieldDto.Id);
            }

            await _products.Delete(id);
        }

        public async Task Update(Product product)
        {
            var productDto = _mapper.Map<ProductDto>(product);

            if (!_productValidator.Validate(product))
            {
                throw new ValidationException($"{nameof(product)} has invalid data");
            }

            var fieldList = product.Fields;

            foreach (var field in product.Fields)

            {
                field.ProductId = product.Id;
                var productField = (await _fields.GetAll()).FirstOrDefault(fieldDto =>
                                                                               fieldDto.ProductId == product.Id &&
                                                                               fieldDto.CharacteristicId == field.CharacteristicId);
                var fieldDto = _mapper.Map<FieldDto>(field);

                if (!_fieldValidator.Validate(field))
                {
                    throw new ValidationException($"{nameof(field)} has invalid data");
                }

                if (productField != null)
                {
                    fieldDto.Id = productField.Id;

                    await _fields.Update(fieldDto);
                }
                else
                {
                    await _fields.Add(fieldDto);
                }
            }

            product.Fields = fieldList;

            await _products.Update(productDto);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var fields = await _fields.GetAll();
            return (await _products.GetAll()).Select(item => MapProduct(item, fields));
        }

        public async Task<Product> GetById(int id)
        {
            var fields = await _fields.GetAll();
            return MapProduct(await _products.GetById(id), fields);
        }

        private Product MapProduct(ProductDto productDto, IEnumerable<FieldDto> fields)
        {
            var product = _mapper.Map<Product>(productDto);
            product.Fields = fields
                             .Where(fieldDto => fieldDto.ProductId == product.Id)
                             .Select(fieldDto => _mapper.Map<Field>(fieldDto))
                             .ToList();
            return product;
        }
    }
}