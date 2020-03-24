using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Category:Entity
    {
        public string Name { get;set; }
        public ICollection<CategoryField> CategoryFields { get;set;}
    }
}
