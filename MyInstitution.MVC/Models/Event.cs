using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyInstitution.MVC.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Summary { get; set; }
        
        public string Text { get; set; }

        [Display(Name = "Begin")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime DateBegin { get; set; }

        [Display(Name = "Ende")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime DateEnd{ get; set; }

        public int Duration { get; set;}

        public string Image { get; set; }

        [NotMapped]
        public IFormFile FormFile { get; set; }

        public bool Archived { get; set; }
    }
}
