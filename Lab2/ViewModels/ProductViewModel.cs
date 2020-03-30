using System.Collections.Generic;
using BusinessLogic.Models;

namespace ConsoleApp.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string Manufacturer { get; set; }
        public List<FieldDto> Fields { get; set; }

    }
}