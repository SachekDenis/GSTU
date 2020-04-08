using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class OrderValidator : IValidator<Order>
    {
        public bool Validate(Order item)
        {
            return !(item.Amount < 0);
        }
    }
}