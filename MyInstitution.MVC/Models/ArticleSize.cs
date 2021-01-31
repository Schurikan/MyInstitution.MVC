﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class ArticleSize
    {
        [Key]
        public int ArticleSizeId { get; set; }

        [Required]
        public int ArticleId { get; set; }

        public Article Article { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
