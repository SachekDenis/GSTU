using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class OrderValidator : IValidator<OrderDto>
    {
        public bool Validate(OrderDto item)
        {
            return !(item.Amount < 0);
        }
    }
}