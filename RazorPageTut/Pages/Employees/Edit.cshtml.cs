using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageTut.Models;
using RazorPageTut.Services;

namespace RazorPageTut.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment webHostEnwironment;

        [BindProperty]
        public Employee Employee { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public bool Notify { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public EditModel(IEmployeeRepository employeeRepository,
                           IWebHostEnvironment webHostEnwironment)
        {
            this.employeeRepository = employeeRepository;
            this.webHostEnwironment = webHostEnwironment;
        }
        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Employee = employeeRepository.GetEmployee(id.Value);
            }
            else 
            {
                Employee = new Employee();
            }
            
            if (Employee == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (Employee.PhotoPath != null)
                    {
                        string filePath = Path.Combine(this.webHostEnwironment.WebRootPath,
                            "images", Employee.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    Employee.PhotoPath = UploadedFilePath();
                }
                if (Employee.Id > 0)
                {
                    Employee = employeeRepository.Update(Employee);
                }
                else
                {
                    Employee = employeeRepository.Add(Employee);
                }
               



                return RedirectToPage("index");
            }
            return Page();

        }

        private string UploadedFilePath()
        {
            string uniqFilePath = null;
            if (Photo != null)
            {
                string uploadFolder = Path.Combine(webHostEnwironment.WebRootPath, "images");
                uniqFilePath = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePah = Path.Combine(uploadFolder, uniqFilePath);
                using (var fileStream = new FileStream(filePah, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqFilePath;
        }

        public IActionResult  OnPostUpdateNotification(int id)
        {

            if (Notify)
            {
                Message = "Thank you for using notification for us.";
            }
            else
            {
                Message = "Please turn on notification.";
            }
            TempData["message"] = Message;
            //Employee = employeeRepository.GetEmployee(id);
            return RedirectToPage("Details", new { id = id});
        }
    }
}