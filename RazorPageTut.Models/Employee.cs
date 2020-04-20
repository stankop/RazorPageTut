using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RazorPageTut.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(4,ErrorMessage ="Mora biti krace od 4 slova")]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhotoPath { get; set; }

        public Dept? Department { get; set; }
    }
}
