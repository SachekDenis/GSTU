using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dto
{
    public class OrderDto
    {
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public int Count { get; set; }
    }
}
