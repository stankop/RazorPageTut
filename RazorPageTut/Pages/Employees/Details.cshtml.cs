using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageTut.Models;
using RazorPageTut.Services;

namespace RazorPageTut.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly IEmployeeRepository employeeRepostitory;
        public Employee Employee { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Message { get; set; }

        public DetailsModel(IEmployeeRepository employeeRepostitory)
        {
            this.employeeRepostitory = employeeRepostitory;
        }
        public IActionResult OnGet(int id)
        {
            Employee = employeeRepostitory.GetEmployee(id);
            if (Employee == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }
    }
}