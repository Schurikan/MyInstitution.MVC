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
    public class DutyRostersController : Controller
    {
        private readonly InstitutionContext _context;

        public DutyRostersController(InstitutionContext context)
        {
            _context = context;
        }

        // GET: DutyRosters
        public async Task<IActionResult> Index(short searchYear, short searchWeek)
        {
            var employees = from e in _context.Employees
                            select e;

            var groups = from g in _context.Groups
                            select g;

            var rosters = from r in _context.DutyRosters
                          where r.Year == searchYear && r.Week == searchWeek
                          select r;


            var dutyRoster = new DutyRoster();

            if (searchYear > 0 && searchWeek > 0)
            {
                rosters = rosters.Where(r => r.Year == searchYear && r.Week == searchWeek);

                dutyRoster.Year = searchYear;
                dutyRoster.Week = searchWeek;
            }
            else
            {
                dutyRoster.Year = (short)DateTime.Today.Year;
                dutyRoster.Week = (short)(DateTime.Today.DayOfYear / 7);
            }

            var dutyRosterModel = new DutyRosterModel
            {
                DutyRosters = await rosters.ToListAsync(),
                Employees = await employees.ToListAsync(),
                Groups = await groups.ToListAsync(),
                DutyRoster = dutyRoster
            };

            return View(dutyRosterModel);
        }

        // GET: DutyRosters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dutyRoster = await _context.DutyRosters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dutyRoster == null)
            {
                return NotFound();
            }

            return View(dutyRoster);
        }

        // GET: DutyRosters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DutyRosters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Year,Week,Day,GroupId,EmployeeId,StartTime,EndTime")] DutyRoster dutyRoster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dutyRoster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dutyRoster);
        }

        // GET: DutyRosters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dutyRoster = await _context.DutyRosters.FindAsync(id);
            if (dutyRoster == null)
            {
                return NotFound();
            }
            return View(dutyRoster);
        }

        // POST: DutyRosters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Year,Week,Day,GroupId,EmployeeId,StartTime,EndTime")] DutyRoster dutyRoster)
        {
            if (id != dutyRoster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dutyRoster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DutyRosterExists(dutyRoster.Id))
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
            return View(dutyRoster);
        }

        // GET: DutyRosters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dutyRoster = await _context.DutyRosters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dutyRoster == null)
            {
                return NotFound();
            }

            return View(dutyRoster);
        }

        // POST: DutyRosters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dutyRoster = await _context.DutyRosters.FindAsync(id);
            _context.DutyRosters.Remove(dutyRoster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DutyRosterExists(int id)
        {
            return _context.DutyRosters.Any(e => e.Id == id);
        }
    }
}
