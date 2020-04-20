using Microsoft.EntityFrameworkCore;
using RazorPageTut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorPageTut.Services
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDBContext context;

        public SQLEmployeeRepository(AppDBContext context)
        {
            this.context = context;
        }
        public Employee Add(Employee newEmployee)
        {
            context.Employees.Add(newEmployee);
            context.SaveChanges();
            return newEmployee;
        }

        public Employee Delete(int id)
        {
            Employee emp =  context.Employees.Find(id);
            if (emp != null)
            {
                context.Employees.Remove(emp);
                context.SaveChanges(); 
            }
            return emp;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDepartment(Dept? dept)
        {
            IEnumerable<Employee> query = context.Employees;
            if (dept.HasValue)
            {
                query = query.Where(emp => emp.Department == dept);
            }
            return query.GroupBy(emp => emp.Department).
                                            Select(g => new DeptHeadCount()
                                            {
                                                Department = g.Key.Value,
                                                Count = g.Count()
                                            }).ToList();
        }

        public Employee GetEmployee(int id)
        {
            return context.Employees.FromSqlRaw<Employee>(@"spGetEmployeeByID {0}", id).ToList().FirstOrDefault();
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return context.Employees.FromSqlRaw<Employee>("select * from Employees").ToList(); 
        }

        public IEnumerable<Employee> Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return this.context.Employees;
            }
            return this.context.Employees.Where(emp => emp.Name.Contains(search) || emp.Email.Contains(search));
        }

        public Employee Update(Employee employee)
        {
            var emp = context.Employees.Attach(employee);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChangesAsync();
            return employee;
        }
    }
}
