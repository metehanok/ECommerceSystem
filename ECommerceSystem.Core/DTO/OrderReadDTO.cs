﻿using ECommerceSystem.Service.DTO.HelperDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Service.DTO
{
    public class OrderReadDTO
    {
        public int Id { get; set; }
        //public string CustomerName { get; set; }//automapper ile maplenecek

        public int CustomerId { get; set; }
        [Required]
        public DateTime? OrderDate { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
}
