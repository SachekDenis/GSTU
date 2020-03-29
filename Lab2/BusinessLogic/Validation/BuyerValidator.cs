using BusinessLogic.Validation;
using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogic.Validation
{
    class BuyerValidator : Validator<Buyer>
    {
        private const string EmailRegex = @"[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,64}";
        private const string PhoneRegex = @"/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/";
        public BuyerValidator(IRepository<Buyer> items) : base(items)
        {}

        protected override bool ValidateProperties(Buyer item)
        {
            return !(string.IsNullOrEmpty(item.FirstName)
                || string.IsNullOrEmpty(item.SecondName)
                || string.IsNullOrEmpty(item.Address)
                || string.IsNullOrEmpty(item.Email)
                || string.IsNullOrEmpty(item.PhoneNumber)
                || !Regex.Match(item.Email,EmailRegex).Success
                || !Regex.Match(item.PhoneNumber,PhoneRegex).Success);
        }
    }
}
