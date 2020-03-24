using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Field : Entity
    {
        public int ProductId { get; set; }
        public int CharacteristicId { get; set; }
        string Value { get; set; }
    }
}
