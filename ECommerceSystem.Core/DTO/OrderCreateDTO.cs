using ECommerceSystem.Service.DTO.HelperDTO;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Service.DTO
{
    public class OrderCreateDTO
    {
        public DateTime? OrderDate { get; set; }
        [Required]
        public required List<OrderDetailCreateDTO> Products { get; set; }
        public int CustomerId { get; set; }
       
    }
}
