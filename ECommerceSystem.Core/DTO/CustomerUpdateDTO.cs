using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Service.DTO
{
    public class CustomerUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string EMail { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string Adress { get; set; }
    }
}
