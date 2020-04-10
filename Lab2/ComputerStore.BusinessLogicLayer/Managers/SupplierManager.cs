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
    public class SupplierManager : IManager<Supplier>
    {
        private readonly IRepository<SupplierDto> _items;
        private readonly IMapper _mapper;
        private readonly IValidator<Supplier> _validator;

        public SupplierManager(IMapper mapper,
            IValidator<Supplier> supplierValidator,
            IRepository<SupplierDto> items)
        {
            _mapper = mapper;
            _validator = supplierValidator;
            _items = items;
        }

        public async Task Add(Supplier supplier)
        {
            var supplierDto = _mapper.Map<SupplierDto>(supplier);

            if (!_validator.Validate(supplier))
            {
                throw new ValidationException($"{nameof(supplier)} has invalid data");
            }

            await _items.Add(supplierDto);
            supplier.Id = supplierDto.Id;
        }

        public async Task Delete(int id)
        {
            await _items.Delete(id);
        }

        public async Task Update(Supplier supplier)
        {
            var supplierDto = _mapper.Map<SupplierDto>(supplier);

            if (!_validator.Validate(supplier))
            {
                throw new ValidationException($"{nameof(supplier)} has invalid data");
            }

            await _items.Update(supplierDto);
        }

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            return (await _items.GetAll()).Select(item => _mapper.Map<Supplier>(item));
        }

        public async Task<Supplier> GetById(int id)
        {
            return _mapper.Map<Supplier>(await _items.GetById(id));
        }
    }
}