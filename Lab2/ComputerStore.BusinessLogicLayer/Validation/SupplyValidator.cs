using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class SupplyValidator : IValidator<SupplyDto>
    {
        public bool Validate(SupplyDto item)
        {
            return true;
        }
    }
}