using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Field:Entity
    {
        public string Value { get;set;}
        public ICollection<CategoryField> CategoryFields {get;set;}
    }
}
