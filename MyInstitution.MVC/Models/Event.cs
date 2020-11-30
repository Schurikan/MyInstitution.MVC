using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Summary { get; set; }

        public string Text { get; set; }

        
        //[DisplayFormat(DataFormatString = "{dd.mm.yyyy mm:hh}", ApplyFormatInEditMode = true)]
        public DateTime DateBegin { get; set; }

        //[DisplayFormat(DataFormatString = "{dd.mm.yyyy mm:hh}", ApplyFormatInEditMode = true)]
        public DateTime DateEnd{ get; set; }

        public int Duration { get; set;}

        public string Image { get; set; }

        public bool Archived { get; set; }
    }
}
