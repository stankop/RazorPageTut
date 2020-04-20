using Microsoft.AspNetCore.Mvc;
using RazorPageTut.Models;
using RazorPageTut.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageTut.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly IEmployeeRepository employeeRepository;

        public HeadCountViewComponent(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IViewComponentResult Invoke(Dept? department = null)
        {

            var result = employeeRepository.EmployeeCountByDepartment(department);
            return View(result);
        }
    }
}
