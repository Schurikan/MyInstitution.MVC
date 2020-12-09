using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class EventModel
    {
       public Event Event { get; set; }

        public List<EventMember> EventMembers { get; set; } 
    }
}
