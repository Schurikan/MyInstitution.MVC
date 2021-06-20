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
    public class IndexModel : PageModel
    {
        private readonly MyInstitution.MVC.Data.InstitutionContext _context;

        public IndexModel(MyInstitution.MVC.Data.InstitutionContext context)
        {
            _context = context;
        }

        public IList<Test> Test { get;set; }

        public async Task OnGetAsync()
        {
            Test = await _context.Test.ToListAsync();
        }
    }
}
