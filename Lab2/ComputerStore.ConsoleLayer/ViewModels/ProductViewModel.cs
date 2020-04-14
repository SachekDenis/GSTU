using System.Collections.Generic;
using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.ConsoleLayer.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string Manufacturer { get; set; }
        public IEnumerable<Field> Fields { get; set; }
    }
}