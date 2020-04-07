using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class OrderValidator : Validator<OrderDto>
    {
        public override bool Validate(OrderDto item)
        {
            return !(item.Amount < 0);
        }
    }
}