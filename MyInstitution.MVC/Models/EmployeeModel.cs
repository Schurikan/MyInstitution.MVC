using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class EmployeeModel
    {
        public Employee Employee { get; set; }

        public int GroupId { get; set; }
        public List<SelectListItem> Groups { set; get; }

    }
}
