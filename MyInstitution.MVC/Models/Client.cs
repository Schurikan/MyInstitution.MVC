using Microsoft.AspNetCore.Authorization;
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

        [Display(Name = "Vorname")]
        [Required]
        [MaxLength(50)]
        public string Forename { get; set; }

        [Display(Name = "Nachname")]
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Display(Name = "Gruppennummer")]
        public int GroupId { get; set; }

        [Display(Name = "Geburtstag")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public string Image { get; set; }

    }
}
