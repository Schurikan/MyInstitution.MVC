using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class EventDetailsController : Controller
    {
        private readonly InstitutionContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IWebHostEnvironment _hostingEnvironment;
        public EventDetailsController(InstitutionContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = environment;
        }

        // GET: EventDetails
        public async Task<IActionResult> Index()
        {
            var institutionContext = _context.EventDetails.Include(e => e.Event);

            Dictionary<long, string> mydict = new Dictionary<long, string>();

            //foreach (var item in institutionContext)
            //{
            //    if (item.Image != null)
            //        mydict.Add(item.EventDetailId, ViewImage(item.Image));
            //}

            ViewBag.Images = mydict;

            return View(await institutionContext.ToListAsync());
        }

        // GET: EventDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventDetails = await _context.EventDetails
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.EventDetailId == id);
            if (eventDetails == null)
            {
                return NotFound();
            }

            //ViewBag.Image = ViewImage(eventDetails.Image);

            return View(eventDetails);
        }

        [NonAction]

        private string ViewImage(byte[] arrayImage)
        {
            string base64String = Convert.ToBase64String(arrayImage, 0, arrayImage.Length);
            return "data:image/png;base64," + base64String;
        }

        // GET: EventDetails/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name");
            return View();
        }

        // POST: EventDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventDetailId,EventId,Title,Text,Image,FormFile")] EventDetails eventDetails)
        {
            if (ModelState.IsValid)
            {
                eventDetails.DateCreate = DateTime.Now;
                eventDetails.ApplicationUserId = _userManager.GetUserId(User);
                eventDetails.ApplicationUser = await _userManager.GetUserAsync(User);

                _context.Add(eventDetails);
                await _context.SaveChangesAsync();

                if (eventDetails.FormFile != null)
                {
                    var newImageName = Path.Combine(eventDetails.EventDetailId.ToString(), Path.GetExtension(eventDetails.FormFile.FileName));
                    eventDetails.Image = newImageName;
                    _context.Update(eventDetails);
                    await _context.SaveChangesAsync();

                    // full path to file in temp location
                    var filePath = Path.GetTempFileName();
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img/event-details");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    var fullPath = Path.Combine(uploadsFolder, eventDetails.Image);

                    if (!Directory.Exists(fullPath))
                        eventDetails.FormFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name", eventDetails.EventId);
            return View(eventDetails);
        }



        //public class BufferedSingleFileUploadDb
        //{
        //    [Required]
        //    [Display(Name = "File")]
        //    public Microsoft.AspNetCore.Http.IFormFile FormFile { get; set; }
        //}

        // GET: EventDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventDetails = await _context.EventDetails.FindAsync(id);
            if (eventDetails == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name", eventDetails.EventId);
            return View(eventDetails);
        }

        // POST: EventDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventDetailId,EventId,Title,Text,Image,FormFile")] EventDetails eventDetails)
        {
            if (id != eventDetails.EventDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (eventDetails.FormFile != null)
                    {
                        var newImageName = eventDetails.EventDetailId.ToString() + Path.GetExtension(eventDetails.FormFile.FileName);
                        eventDetails.Image = newImageName;

                        // full path to file in temp location
                        var filePath = Path.GetTempFileName();
                        var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img/event-details");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }
                        var fullPath = Path.Combine(uploadsFolder, eventDetails.Image);

                        if (!Directory.Exists(fullPath))
                            eventDetails.FormFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }

                    _context.Update(eventDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventDetailsExists(eventDetails.EventDetailId))
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
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name", eventDetails.EventId);
            return View(eventDetails);
        }

        // GET: EventDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventDetails = await _context.EventDetails
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.EventDetailId == id);
            if (eventDetails == null)
            {
                return NotFound();
            }

            return View(eventDetails);
        }

        // POST: EventDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventDetails = await _context.EventDetails.FindAsync(id);
            _context.EventDetails.Remove(eventDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventDetailsExists(int id)
        {
            return _context.EventDetails.Any(e => e.EventDetailId == id);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventModel eventModel)
        {
            if (ModelState.IsValid)
            {
                eventModel.NewEventDetail.DateCreate = DateTime.Now;
                eventModel.NewEventDetail.ApplicationUserId = _userManager.GetUserId(User);
                eventModel.NewEventDetail.ApplicationUser = await _userManager.GetUserAsync(User);

                _context.Add(eventModel.NewEventDetail);
                await _context.SaveChangesAsync();
                // return RedirectToAction("Index", "Events", eventModel.NewEventDetail.EventId); // (nameof(Index));

                return RedirectToAction("Details", "Events", new { id = eventModel.NewEventDetail.EventId }); // (nameof(Index));
            }
            //return redirectto(nameof(Index));
            return View(eventModel.Event);
        }
    }
}
