using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class PrivacyModel
    {
        [BindProperty]
        public List<EventModel> Events { get; set; }
    }
}
