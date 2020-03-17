using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Product:Entity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ManufacturerId { get; set; }
        public int SupplyId {get;set;}
        public int AdditionalInformationId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Supply Supply {get;set;}
        public Characteristics AdditionalInformation { get; set; }

    }
}
