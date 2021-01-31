using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class Article
    {

        [Key]
        public int ArticleId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public string Image { get; set; }

        public string Material { get; set; }

        //[Required]
        //public int ArticleCategoryId { get; set; }

        //public Category ArticleCategory { get; set; }

        public int ArticleSexId { get; set; }

        public ArticleSex ArticleSex { get; set; }
    }
}
