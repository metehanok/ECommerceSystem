using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Core.Models
{
    public class Products
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }

        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public Categories Category { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }



    }
}
