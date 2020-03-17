using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Buyer:Entity
    {
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public List<Order> Orders { get; set; }
    }
}
