using RazorPageTut.Models;
using System;
using System.Collections.Generic;

namespace RazorPageTut.Services
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Search(string search);
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployee(int id);

        Employee Update(Employee employee);

        Employee Add(Employee newEmployee);

        Employee Delete(int id);

        IEnumerable<DeptHeadCount> EmployeeCountByDepartment(Dept? dept);
    }
}
