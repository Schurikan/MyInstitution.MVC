using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class Category
    {

        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int CategoryParentId { get; set; }
        public Category CategoryParent { get; set; }
    }
}
