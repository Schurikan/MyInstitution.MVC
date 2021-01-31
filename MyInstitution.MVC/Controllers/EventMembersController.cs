using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyInstitution.MVC.Data;
using MyInstitution.MVC.Models;

namespace MyInstitution.MVC.Controllers
{
    public class EventMembersController : Controller
    {
        private readonly InstitutionContext _context;

        public EventMembersController(InstitutionContext context)
        {
            _context = context;
        }

        // GET: EventMembers
        public async Task<IActionResult> Index()
        {
            return View(await _context.EventMembers.ToListAsync());
        }

        // GET: EventMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventMember = await _context.EventMembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventMember == null)
            {
                return NotFound();
            }

            return View(eventMember);
        }

        // GET: EventMembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventMembers/Create
        // To protect f overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventMember eventMember)
        {
            //if (ModelState.IsValid)
            //{
            //}

            var eventMembers = from e in _context.EventMembers
                               where e.EventId == eventMember.EventId && e.ApplicationUserId == eventMember.ApplicationUserId
                               select e;

            if(eventMembers.Count() == 0)
            {
                _context.Add(eventMember);
                await _context.SaveChangesAsync();
            }

            return Redirect("/Privacy");
            // return RedirectToAction(nameof(Index));

            //return View(eventMember);
        }

        // GET: EventMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventMember = await _context.EventMembers.FindAsync(id);
            if (eventMember == null)
            {
                return NotFound();
            }
            return View(eventMember);
        }

        // POST: EventMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventId,ApplicationUserId")] EventMember eventMember)
        {
            if (id != eventMember.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventMemberExists(eventMember.Id))
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
            return View(eventMember);
        }

        // GET: EventMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventMember = await _context.EventMembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventMember == null)
            {
                return NotFound();
            }

            return View(eventMember);
        }

        // POST: EventMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventMember = await _context.EventMembers.FindAsync(id);
            _context.EventMembers.Remove(eventMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventMemberExists(int id)
        {
            return _context.EventMembers.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> StatusChanged(EventMember eventMember)
        {
            try
            {
                var member = await _context.EventMembers.Where(e => e.EventId == eventMember.EventId && e.ApplicationUserId == eventMember.ApplicationUserId).FirstOrDefaultAsync();

                if (member == null)
                {
                    await _context.AddAsync(eventMember);
                }
                else
                {

                    _context.EventMembers.Remove(member);
                }

                await _context.SaveChangesAsync();

                return View();
            }
            catch (Exception ex)
            {

                throw;
            }



        }
    }
}
