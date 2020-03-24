using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Characteristic:Entity
    {
        public int CategoryId {get;set;}
        public string Name { get;set; }
    }
}
