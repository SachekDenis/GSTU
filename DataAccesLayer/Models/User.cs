using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        public List<Order> Orders { get; set; }
    }
}
