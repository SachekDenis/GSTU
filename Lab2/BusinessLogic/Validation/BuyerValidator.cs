﻿using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System.Text.RegularExpressions;

namespace BusinessLogic.Validation
{
    internal class BuyerValidator : Validator<Buyer>
    {
        private const string emailRegex = @"[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,64}";
        private const string phoneRegex = @"/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/";
        public BuyerValidator(IRepository<Buyer> items) : base(items)
        { }

        protected override bool ValidateProperties(Buyer item)
        {
            return !(string.IsNullOrEmpty(item.FirstName)
                || string.IsNullOrEmpty(item.SecondName)
                || string.IsNullOrEmpty(item.Address)
                || string.IsNullOrEmpty(item.Email)
                || string.IsNullOrEmpty(item.PhoneNumber)
                || !Regex.Match(item.Email, emailRegex).Success
                || !Regex.Match(item.PhoneNumber, phoneRegex).Success);
        }
    }
}
