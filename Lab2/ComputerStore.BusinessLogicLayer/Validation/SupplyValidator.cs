using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class SupplyValidator : IValidator<Supply>
    {
        public bool Validate(Supply item)
        {
            return true;
        }
    }
}