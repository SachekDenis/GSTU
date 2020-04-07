using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class SupplyValidator : Validator<SupplyDto>
    {
        public override bool Validate(SupplyDto item)
        {
            return true;
        }
    }
}