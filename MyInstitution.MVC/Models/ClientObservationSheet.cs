using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class ClientObservationSheet
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Question1 { get; set; }
        public string Answer1 { get; set; }

    }
}
