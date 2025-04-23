using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Core.DTO
{
    public class OrderDetailUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]

        public int ProductId {get;set;}
    }
}
