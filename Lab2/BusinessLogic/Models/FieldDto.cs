using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class FieldDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CharacteristicId { get; set; }
        public string Value { get; set; }
    }
}
