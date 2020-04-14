using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class ProductValidator : IValidator<Product>
    {
        public bool Validate(Product item)
        {
            return !(item.Price < 0
                     || string.IsNullOrEmpty(item.Name)
                     || item.AmountInStorage < 0
                     || item.Price < 0
                     || item.Fields == null
                );
        }
    }
}