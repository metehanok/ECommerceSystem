using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Core.Models
{
    public class Orders
    {

        public int Id { get; set; }            
        [Required]
        public DateTime? OrderDate { get; set; }
        [Required]
        public int TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public Customers Customers { get; set; }       
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
