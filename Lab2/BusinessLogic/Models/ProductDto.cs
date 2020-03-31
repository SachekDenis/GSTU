﻿using System;
using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public int ManufacturerId { get; set; }
        public DateTime Date { get; set; }
        public int SupplyId { get; set; }
        public int CategoryId { get; set; }
        public List<FieldDto> Fields { get; set; }
    }
}