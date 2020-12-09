using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class EventMember
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
