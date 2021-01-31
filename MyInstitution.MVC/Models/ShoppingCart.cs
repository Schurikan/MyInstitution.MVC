using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ShoppingCartId { get; set; }
        [Required]

        public string ApplicationUserId { get; set; }

        [Required]
        public string ArticleId { get; set; }

        public Article Article { get; set; }
        [Required]
        public double Quantity { get; set; }


    }
}
