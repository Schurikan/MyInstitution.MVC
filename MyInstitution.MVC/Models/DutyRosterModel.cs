using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class DutyRosterModel
    {
        public DutyRoster DutyRoster { get; set; }

        public List<DutyRoster> DutyRosters { get; set; }

        public List<Group> Groups { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
