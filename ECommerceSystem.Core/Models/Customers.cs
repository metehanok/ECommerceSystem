using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ECommerceSystem.Core.Models
{
    [Index(nameof(EMail), IsUnique = true)]
    public class Customers
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
       
        [Required]
        [EmailAddress]
        public string EMail { get; set; }
        [Required]
        
        public string  Password { get; set; }
        [Required]
        public string Adress { get; set; }
        public ICollection<Orders>  Orders { get; set; }

    }
}
