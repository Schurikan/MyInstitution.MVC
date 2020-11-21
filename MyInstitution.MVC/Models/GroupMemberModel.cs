using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Models
{
    public class GroupMemberModel
    {
        public Group Group { get; set; }
        public Employee GroupLeader { get; set; }
        public List<Client> Clients { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
