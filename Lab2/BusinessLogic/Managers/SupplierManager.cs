using AutoMapper;
using BusinessLogic.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers
{
    public class SupplierManager:IManager<SupplierDto>
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

        public async Task Update(SupplierDto supplierDto)
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
