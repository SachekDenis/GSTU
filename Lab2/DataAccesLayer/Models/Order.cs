using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
