using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyInstitution.MVC.Areas.Identity.Data;
using MyInstitution.MVC.Data;
using MyInstitution.MVC.Models;

namespace MyInstitution.MVC.Controllers
{
    public class EventsController : Controller
    {
        private readonly InstitutionContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IWebHostEnvironment _hostingEnvironment;

        public EventsController(InstitutionContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = environment;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventModel = await (from e in _context.Events
                                    where e.Id == id
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
                                    }).FirstOrDefaultAsync();

            int currentUserIsMemberCount = 0;
            string applicationUserId = _userManager.GetUserId(User);
            
            eventModel.EventDetails = await _context.EventDetails.Where(e => e.EventId == eventModel.Event.Id).ToListAsync();
            foreach (var item in eventModel.EventDetails)
            {
                item.ApplicationUser = await _userManager.GetUserAsync(User);
            }
            eventModel.EventMembers = await _context.EventMembers.Where(e => e.EventId == eventModel.Event.Id).ToListAsync();
            eventModel.NumberOfMembers = await _context.EventMembers.Where(e => e.EventId == eventModel.Event.Id).CountAsync();
            currentUserIsMemberCount = eventModel.EventMembers.Where(e => e.ApplicationUserId.Equals(applicationUserId)).Count();

            if (currentUserIsMemberCount > 0)
                eventModel.UserIsMember = true;
            else
                eventModel.UserIsMember = false;

            eventModel.NewEventDetail = new EventDetails();
            eventModel.NewEventDetail.EventId = eventModel.Event.Id;

            return View(eventModel);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Summary,Text,DateBegin,DateEnd,Duration,Image,FormFile")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();

                if (@event.FormFile != null)
                {
                    var newImageName = @event.Id.ToString() + Path.GetExtension(@event.FormFile.FileName);
                    @event.Image = newImageName;
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }

                if (@event.FormFile != null)
                {
                    // full path to file in temp location
                    var filePath = Path.GetTempFileName();

                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "img/events");
                    var fullPath = Path.Combine(uploads, @event.Image);

                    if (!Directory.Exists(fullPath))
                        @event.FormFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Summary,Text,DateBegin,DateEnd,Duration,Image,FormFile")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (@event.FormFile != null)
                    {
                        var newImageName = @event.Id.ToString() + Path.GetExtension(@event.FormFile.FileName);
                        @event.Image = newImageName;

                        // full path to file in temp location
                        var filePath = Path.GetTempFileName();
                        var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "img/events");
                        var fullPath = Path.Combine(uploads, @event.Image);

                        if (!Directory.Exists(fullPath))
                            @event.FormFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }

                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
