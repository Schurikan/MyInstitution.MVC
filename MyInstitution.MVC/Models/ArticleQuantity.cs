using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class ArticleQuantity
    {
        [Key]
        public int ArticleQuantityId { get; set; }

        [Required]
        public int ArticleId { get; set; }

        public Article Article { get; set; }

        [Required]
        public double Quantity { get; set; }

        public string ApplicationUserId { get; set; }

        [MaxLength(100)]
        public string Comment { get; set; }
    }
}
