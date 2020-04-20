using RazorPageTut.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RazorPageTut.Services
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        List<Employee> _employees;
        public MockEmployeeRepository()
        {
            this._employees = new List<Employee>()
                { 
                    new Employee() { Id=1, Department= Dept.HR, Email = "stanko@gmail.com", Name= "Stanko", PhotoPath = "Desert.jpg" },
                    new Employee() { Id=2, Department= Dept.IT, Email = "stanko@gmail.com", Name= "Janko", PhotoPath = "Hydrangeas.jpg" },
                    new Employee() { Id=3, Department= Dept.PayRoll, Email = "stanko@gmail.com", Name= "Bananko", PhotoPath = "Jellyfish.jpg" },
                    new Employee() { Id=4, Department= Dept.HR, Email = "stanko@gmail.com", Name= "Usranko", PhotoPath = "Koala.jpg" },
                    new Employee() { Id=5, Department= Dept.IT, Email = "stanko@gmail.com", Name= "Mali", PhotoPath = "Lighthouse.jpg" },
                    new Employee() { Id=6, Department= Dept.HR, Email = "stanko@gmail.com", Name= "Bali" },



                };



        }
        public IEnumerable<Employee> GetEmployees()
        {
            return this._employees;
        }

        public Employee GetEmployee(int id)
        {

            return this._employees.FirstOrDefault(emp => emp.Id == id);
        }

        public Employee Update(Employee employee)
        {
            
            Employee emp = this._employees.FirstOrDefault(empl => empl.Id == employee.Id);
            if (emp != null)
            {
                emp.Department = employee.Department;
                emp.Email = employee.Email;
                emp.Name = employee.Name;
                emp.PhotoPath = employee.PhotoPath;
               
            }
            
            return emp;

        }

        public Employee Add(Employee newEmployee)
        {
            newEmployee.Id = this._employees.Max(emp => emp.Id) + 1;
            this._employees.Add(newEmployee);
            return newEmployee;
        }

        public Employee Delete(int id)
        {
            Employee emp = this._employees.FirstOrDefault(empl => empl.Id == id);
            if (emp != null)
            {
                this._employees.Remove(emp);
            }
            return emp;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDepartment(Dept? dept)
        {
            IEnumerable<Employee>  query = _employees;
            if (dept.HasValue)
            {
                query = query.Where(emp => emp.Department == dept);
            }
            return query.GroupBy(emp => emp.Department).
                                            Select(g => new DeptHeadCount()
                                            { Department = g.Key.Value,
                                              Count = g.Count()}).ToList();
        }

        public IEnumerable<Employee> Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return this._employees;
            }
            return this._employees.Where(emp => emp.Name.Contains(search) || emp.Email.Contains(search));
        }
    }
}
