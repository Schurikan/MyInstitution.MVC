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

        public List<EventDetails> EventDetails { get; set; }

        public EventDetails NewEventDetail { get; set; }

        public int NumberOfMembers { get; set; }

        public bool UserIsMember { get; set; }
    }
}
