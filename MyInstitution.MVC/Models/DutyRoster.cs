using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class DutyRoster
    {
        public int Id { get; set; }

        public short Year { get; set; }

        public short Week { get; set; }

        public DateTime Day { get; set; }

        public int GroupId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }



    }
}
