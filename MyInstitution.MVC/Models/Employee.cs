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
        public string Forename { get; set; }

        [Required]
        public string Surname { get; set; }

        public string Image { get; set; }

        //public List<UserRole> UserRoles { get; set; }

        //public ICollection<Group> Groups { get; set; }

    }
}
