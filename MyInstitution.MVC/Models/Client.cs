using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string Forename { get; set; }

        [Required]
        public string Surname { get; set; }

        public int GroupId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Image { get; set; }

    }
}
