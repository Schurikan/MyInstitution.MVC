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
        //private IWebHostEnvironment _hostingEnvironment;

        public DashboardController(InstitutionContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // var events = await _context.Events.ToListAsync();

            var eventModelList = await (from e in _context.Events

                                            // where iif id == null ? (1 = 1) : (e.Id == id) 
                                            // Status = aa == null ? false : aa.Online;
                                        select new EventModel
                                        {
                                            Event = new Event
                                            {
                                                Id = e.Id,
                                                Name = e.Name,
                                                Summary = e.Summary,
                                                Text = e.Text,
                                                DateBegin = e.DateEnd,
                                                Duration = e.Duration,
                                                Image = e.Image,
                                                Archived = e.Archived
                                            },
                                            EventMembers = new List<EventMember>()
                                        }).ToListAsync();

            int currentUserIsMemberCount = 0;
            string applicationUserId = _userManager.GetUserId(User);

            foreach (var eventItem in eventModelList)
            {
                eventItem.EventMembers = await _context.EventMembers.Where(e => e.EventId == eventItem.Event.Id).ToListAsync();
                eventItem.NumberOfMembers = await _context.EventMembers.Where(e => e.EventId == eventItem.Event.Id).CountAsync();

                currentUserIsMemberCount = eventItem.EventMembers.Where(e => e.ApplicationUserId.Equals(applicationUserId)).Count();

                if (currentUserIsMemberCount > 0)
                    eventItem.UserIsMember = true;
                else
                    eventItem.UserIsMember = false;
            }

            var dashboardModel = new DashboardModel
            {
                Events = eventModelList
            };

            return View(dashboardModel);

            //var events = await _context.Events.ToListAsync();
            //var eventMembers = await _context.EventMembers.ToListAsync();

            //var eventsFiltered = from e in events
            //                     join d in eventMembers on e.Id equals d.EventId
            //                     select new Event
            //                     {
            //                         Id = e.Id,
            //                         Name = e.Name,
            //                         Summary = e.Summary,
            //                         Text = e.Text,
            //                         DateBegin = e.DateEnd,
            //                         Duration = e.Duration,
            //                         Image = e.Image,
            //                         Archived = e.Archived
            //                     };

            //var dashboardModel = new DashboardModel
            //{
            //    Events = eventsFiltered.ToList()
            //};

            //return View(dashboardModel);
        }
    }
}
