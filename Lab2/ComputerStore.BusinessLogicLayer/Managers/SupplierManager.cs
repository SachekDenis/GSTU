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
    public class SupplierManager : IManager<Supplier>
    {
        private readonly IRepository<SupplierDto> _items;
        private readonly IMapper _mapper;
        private readonly IValidator<SupplierDto> _validator;

        public SupplierManager(IMapper mapper, SupplierValidator supplierValidator, IRepository<SupplierDto> items)
        {
            _mapper = mapper;
            _validator = supplierValidator;
            _items = items;
        }

        public void Add(Supplier supplier)
        {
            var supplierDto = _mapper.Map<SupplierDto>(supplier);

            if (!_validator.Validate(supplierDto))
            {
                throw new ValidationException($"{nameof(supplierDto)} has invalid data");
            }

            _items.Add(supplierDto);
            supplier.Id = supplierDto.Id;
        }

        public void Delete(int id)
        {
            _items.Delete(id);
        }

        public void Update(Supplier supplier)
        {
            var supplierDto = _mapper.Map<SupplierDto>(supplier);

            if (!_validator.Validate(supplierDto))
            {
                throw new ValidationException($"{nameof(supplierDto)} has invalid data");
            }

            _items.Update(supplierDto);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _items.GetAll().Select(item => _mapper.Map<Supplier>(item));
        }

        public Supplier GetById(int id)
        {
            return _mapper.Map<Supplier>(_items.GetById(id));
        }
    }
}