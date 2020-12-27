using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class DutyRosterIndexModel
    {
        public DutyRoster DutyRoster { get; set; }

        public List<DayOfWeek> DaysOfWeek { get; set; }

        public List<DutyRosterModel> DutyRosters { get; set; }

        public List<Group> Groups { get; set; }

        [BindProperty]
        public List<Employee> Employees { get; set; }
    }
}
