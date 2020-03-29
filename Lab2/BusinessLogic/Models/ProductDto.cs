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
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public Dictionary<int, string> Characteristics { get; set; }
    }
}