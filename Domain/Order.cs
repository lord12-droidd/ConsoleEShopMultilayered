using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Order
    {
        public decimal FullCost { set; get; }
        public string Receiver { set; get; }
        public OrderStatus Status { get; set; }
        public List<Product> OrderedProducts { get; set; }
    }
}
