using Microsoft.EntityFrameworkCore;
using RazorPageTut.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RazorPageTut.Services
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
