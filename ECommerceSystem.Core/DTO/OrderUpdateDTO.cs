using ECommerceSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Service.DTO
{
    public class OrderUpdateDTO
    {
        public int Id { get; set; }
        public int TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }

    }
}
