using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPageTut.Models;
using RazorPageTut.Services;

namespace RazorPageTut.Pages.V2
{
    public class IndexModel : PageModel
    {
        private readonly RazorPageTut.Services.AppDBContext _context;

        public IndexModel(RazorPageTut.Services.AppDBContext context)
        {
            _context = context;
        }

        public IList<Employee> Employee { get;set; }

        public async Task OnGetAsync()
        {
            Employee = await _context.Employees.ToListAsync();
        }
    }
}
