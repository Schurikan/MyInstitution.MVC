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
    public class GroupsController : Controller
    {
        private readonly InstitutionContext _context;

        public GroupsController(InstitutionContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            return View(await _context.Groups.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups.FirstOrDefaultAsync(m => m.Id == id);
            if (group == null)
            {
                return NotFound();
            }

            var clients = from c in _context.Clients
                          where c.GroupId == id
                          select c;

            var employees = from e in _context.Employees
                            where e.GroupId == id
                            select e;

            var groupLeader = await _context.Employees.FirstOrDefaultAsync(e => e.Id == group.GroupLeaderEmployeeId);
                //employees.FirstOrDefaultAsync(e => e.Id == group.GroupLeaderEmployeeId);
                //.Where(e => e.Id == group.GroupLeaderEmployeeId);

            var groupMembers = new GroupMemberModel
            {
                Group = group,
                Clients = await clients.ToListAsync(),
                Employees = await employees.ToListAsync(),
                GroupLeader = groupLeader
            };

            return View(groupMembers);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,GroupLeaderEmployeeId,Image,GroupId")] Group @group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }

            var employeesQuery = from e in _context.Employees
                                 orderby e.Surname
                                 select e;

            //var employees = _context.Employees.Select(e => new SelectListItem
            //{
            //    Text = e.Forename + " " + e.Surname,
            //    Value = e.Id
            //};

            var test = _context.Employees.Select(u => new SelectListItem
            {
                Text = u.Forename + " " + u.Surname,
                Value = u.Id.ToString()
            });

            var departmentsQuery = from e in _context.Employees
                                   orderby e.Surname // Sort by name.
                                   select new
                                   {
                                        Id       = e.Id
                                       ,Forename = e.Forename
                                       ,Surname = e.Surname
                                       ,GroupId = e.GroupId 
                                       ,Image = e.Image
                                       ,displayColumn = e.Forename + ' ' + e.Surname
                                   };

            var groupModel = new GroupModel
            {
                Group = group,
                Employees = new SelectList(departmentsQuery.AsNoTracking(),
                        "Id", "displayColumn")
            //_context.Employees.ToArrayAsync();
            };


            //var users = _usersRepository.Users.Select(u => new SelectListItem
            //{
            //    Text = u.FirstName + " " + u.LastName,
            //    Value = SqlFunctions.StringConvert((double?)u.UserID)
            //}

            return View(groupModel);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GroupLeaderEmployeeId,Image")] Group @group)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
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
            return View(@group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @group = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
