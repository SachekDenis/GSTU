using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.Validation;
using DataAccesLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Managers
{
    internal class SupplierManager:IManager<SupplierDto>
    {
        private readonly IMapper _mapper;
        private readonly Validator<Supplier> _validator;

        public SupplierManager(IMapper mapper, SupplierValidator supplierValidator)
        {
            _mapper = mapper;
            _validator = supplierValidator;
        }

        public async Task Add(SupplierDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            await _validator.Add(supplier);
        }

        public async Task Delete(int id)
        {
            await _validator.Delete(id);
        }

        public async Task Update(BuyerDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            await _validator.Update(supplier);
        }

        public IEnumerable<SupplierDto> GetAll()
        {
            return _validator.GetAll().Select(item => _mapper.Map<SupplierDto>(item));
        }
    }
}
