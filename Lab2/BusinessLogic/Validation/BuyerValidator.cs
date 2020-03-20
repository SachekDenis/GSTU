using BusinessLogic.Validation;
using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Validation
{
    class BuyerValidator : Validator<Buyer>
    {
        public BuyerValidator(IRepository<Buyer> items) : base(items)
        {}

        protected override bool ValidateProperties(Buyer item)
        {
            return !(string.IsNullOrEmpty(item.FirstName)
                || string.IsNullOrEmpty(item.SecondName)
                || string.IsNullOrEmpty(item.Address)
                || string.IsNullOrEmpty(item.Email)
                || string.IsNullOrEmpty(item.PhoneNumber));
        }
    }
}
