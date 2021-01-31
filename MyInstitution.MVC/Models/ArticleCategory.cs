using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class ArticleCategory
    {

        [Key]
        public int ArticleCategoryId { get; set; }

        [Required]
        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
