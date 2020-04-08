﻿using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public class ProductValidator : IValidator<ProductDto>
    {
        public bool Validate(ProductDto item)
        {
            return !(item.Price < 0
                     || string.IsNullOrEmpty(item.Name)
                     || item.AmountInStorage < 0
                );
        }
    }
}