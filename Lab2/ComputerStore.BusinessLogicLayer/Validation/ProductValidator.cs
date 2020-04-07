using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class ProductValidator : Validator<ProductDto>
    {
        public override bool Validate(ProductDto item)
        {
            return !(item.Price < 0
                     || string.IsNullOrEmpty(item.Name)
                     || item.AmountInStorage < 0
                );
        }
    }
}