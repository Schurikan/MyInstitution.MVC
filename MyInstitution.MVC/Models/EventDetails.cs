using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class EventDetails
    {
        [Key]
        public int EventDetailId { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }

        public string Image { get; set; }
    
        [NotMapped]
        [Display(Name = "Bild")]
        public IFormFile FormFile { get; set; }

        public DateTime DateCreate { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
