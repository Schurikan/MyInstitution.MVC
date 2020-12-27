using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Forename { get; set; }

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        public string Image { get; set; }

        [Display(Name = "Gruppe")]
        public int GroupId { get; set; }

    }
}
