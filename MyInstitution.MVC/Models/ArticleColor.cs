using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class ArticleColor
    {
        [Key]
        public int ArticleColorId { get; set; }

        [Required]
        public int ArticleId { get; set; }

        public Article Article { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
