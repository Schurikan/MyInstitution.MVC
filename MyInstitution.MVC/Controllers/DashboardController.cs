using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyInstitution.MVC.Areas.Identity.Data;
using MyInstitution.MVC.Data;
using MyInstitution.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly InstitutionContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IWebHostEnvironment _hostingEnvironment;

        public DashboardController(InstitutionContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var events = await _context.Events.ToListAsync();
            var eventMembers = await _context.EventMembers.ToListAsync();

            var eventsFiltered = from e in events
                                 join d in eventMembers on e.Id equals d.EventId
                                 select new Event
                                 {
                                     Id = e.Id,
                                     Name = e.Name,
                                     Summary = e.Summary,
                                     Text = e.Text,
                                     DateBegin = e.DateEnd,
                                     Duration = e.Duration,
                                     Image = e.Image,
                                     Archived = e.Archived
                                 };

            var dashboardModel = new DashboardModel
            {
                Events = eventsFiltered.ToList()
            };

            return View(dashboardModel);
        }
    }
}
