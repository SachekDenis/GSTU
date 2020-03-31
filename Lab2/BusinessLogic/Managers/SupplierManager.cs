using AutoMapper;
using BusinessLogic.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
using DataAccessLayer.Models;

namespace BusinessLogic.Managers
{
    public class SupplierManager : IManager<SupplierDto>
    {
        private readonly IMapper _mapper;
        private readonly Validator<Supplier> _validator;

        public SupplierManager(IMapper mapper, SupplierValidator supplierValidator)
        {
            _mapper = mapper;
            _validator = supplierValidator;
        }

        public void Add(SupplierDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            _validator.Add(supplier);
        }

        public void Delete(int id)
        {
            _validator.Delete(id);
        }

        public void Update(SupplierDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            _validator.Update(supplier);
        }

        public IEnumerable<SupplierDto> GetAll()
        {
            return _validator.GetAll().Select(item => _mapper.Map<SupplierDto>(item));
        }
    }
}
