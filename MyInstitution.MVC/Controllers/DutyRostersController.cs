using System;
using System.Collections.Generic;
using System.Globalization;
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
        public async Task<IActionResult> Index(short searchYear, short searchWeek, short weekChange)
        {
            var employees = from e in _context.Employees
                            select e;

            var groups = from g in _context.Groups
                         select g;

            CultureInfo myCI = new CultureInfo("de-DE");
            Calendar myCal = myCI.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            System.DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            var dutyRoster = new DutyRoster();

            if (searchYear > 0 && searchWeek > 0)
            {

                if (weekChange == -1)
                {
                    if(searchWeek == 1)
                    {
                        dutyRoster.Year = (short)(searchYear -1);
                        dutyRoster.Week = 52;
                    }
                    else
                    {
                        dutyRoster.Year = searchYear;
                        dutyRoster.Week = (short)(searchWeek - 1);
                    }         
                }
                else if (weekChange == 1)
                {
                    if (searchWeek == 52)
                    {
                        dutyRoster.Year = (short)(searchYear + 1);
                        dutyRoster.Week = 1;
                    }
                    else
                    {
                        dutyRoster.Year = searchYear;
                        dutyRoster.Week = (short)(searchWeek + 1);
                    }

                }
                else
                {
                    dutyRoster.Year = searchYear;
                    dutyRoster.Week = searchWeek;
                }

            }
            else
            {

                // Displays the number of the current week relative to the beginning of the year.
                //Console.WriteLine("The CalendarWeekRule used for the en-US culture is {0}.", myCWR);
                //Console.WriteLine("The FirstDayOfWeek used for the en-US culture is {0}.", myFirstDOW);
                //Console.WriteLine("Therefore, the current week is Week {0} of the current year.", myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW));
                dutyRoster.Year = (short)DateTime.Today.Year;
                dutyRoster.Week = (short)myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);
            }

            var rosters = from r in _context.DutyRosters
                          where r.Year == dutyRoster.Year && r.Week == dutyRoster.Week
                          select r;

            var joinedDataRostersEmployees = rosters.AsQueryable().Join
                                            (employees,
                                                    roster => roster.EmployeeId,
                                                    empoyee => empoyee.Id,
                                                    (roster, employee) =>
                                            new
                                            {
                                                Id = roster.Id,
                                                Year = roster.Year,
                                                Week = roster.Week,
                                                Day = roster.Day,
                                                GroupId = roster.GroupId,
                                                EmployeeId = roster.EmployeeId,
                                                StartTime = roster.StartTime,
                                                EndTime = roster.EndTime,
                                                EmployeeName = employee.Surname + ", " + employee.Forename
                                            });

            List<DutyRosterModel> dutyRosterModels = new List<DutyRosterModel>();
            foreach (var item in joinedDataRostersEmployees)
            {
                dutyRosterModels.Add(
                        new DutyRosterModel
                        {
                            Id = item.Id,
                            Year = item.Year,
                            Week = item.Week,
                            Day = item.Day,
                            GroupId = item.GroupId,
                            EmployeeId = item.EmployeeId,
                            StartTime = item.StartTime,
                            EndTime = item.EndTime,
                            EmployeeName = item.EmployeeName
                        }
                    );
            }

            var firstDayInWeek = ISOWeek.ToDateTime(dutyRoster.Year, dutyRoster.Week, myFirstDOW).Date;

            var daysOfWeek = new List<Models.DayOfWeek>();

            Models.DayOfWeek dayOfWeek = new Models.DayOfWeek();

            for (int i = 0; i < 7; i++)
            {
                dayOfWeek = new Models.DayOfWeek
                {
                    Index = i,
                    DayName = firstDayInWeek.AddDays(i).DayOfWeek.ToString(),
                    Date = firstDayInWeek.AddDays(i)
                };
                daysOfWeek.Add(dayOfWeek);
            }

            var dutyRosterModel = new DutyRosterIndexModel
            {
                DutyRosters = dutyRosterModels,
                Employees = await employees.ToListAsync(),
                Groups = await groups.ToListAsync(),
                DutyRoster = dutyRoster,
                DaysOfWeek = daysOfWeek
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

        // POST: DutyRosters/AddDutyRoster
        [HttpPost]
        public async Task<IActionResult> AddDutyRoster(DutyRoster roster)
        {
            //DutyRoster dutyRoster = (DutyRoster)roster;

            //[Bind("Id,Year,Week,Day,GroupId,EmployeeId,StartTime,EndTime")] DutyRoster dutyRoster
            //if (ModelState.IsValid)
            //{
            //}
            _context.Add(roster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            // return View(roster);
        }
    }
}
