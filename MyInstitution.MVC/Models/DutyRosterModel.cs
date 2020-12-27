using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class DutyRosterModel
    {
        public int Id { get; set; }

        public short Year { get; set; }

        public short Week { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Day { get; set; }

        public int GroupId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string EmployeeName { get; set; }

    }
}
