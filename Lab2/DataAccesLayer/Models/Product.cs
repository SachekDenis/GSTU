using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Supply Supply {get;set;}
        public Characteristics AdditionalInformation { get; set; }

    }
}
