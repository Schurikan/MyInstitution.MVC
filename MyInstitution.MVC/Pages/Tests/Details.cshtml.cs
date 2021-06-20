using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyInstitution.MVC.Data;
using MyInstitution.MVC.Models;

namespace MyInstitution.MVC.Pages.Tests
{
    public class DetailsModel : PageModel
    {
        private readonly MyInstitution.MVC.Data.InstitutionContext _context;

        public DetailsModel(MyInstitution.MVC.Data.InstitutionContext context)
        {
            _context = context;
        }

        public Test Test { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Test = await _context.Test.FirstOrDefaultAsync(m => m.ID == id);

            if (Test == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
