using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Core.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        [Required]
        public  int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public Products Products { get; set; }
        public int OrderId { get; set; }
        public Orders Orders { get; set; }

        public decimal TotalPrice { get; set; }

    }
}
