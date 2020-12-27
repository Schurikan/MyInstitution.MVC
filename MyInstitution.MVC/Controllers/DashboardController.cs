using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyInstitution.MVC.Data;
using MyInstitution.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInstitution.MVC.Controllers
{
    public class PrivacyController : Controller
    {
        private readonly InstitutionContext _context;

        public PrivacyController(InstitutionContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }
    }
}
