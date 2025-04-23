using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Service.DTO
{
    public class OrderDetailCreateDTO
    {
        public int Quantity { get; set; }
        [Required]
       // public decimal Price { get; set; }
        public int ProductId { get; set; }
       
    }
}
