using NewStudy.DataAccessLayer;
using NewStudy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewStudy.BusinessLayer
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            StudayDal salesDal = new StudayDal();
            return salesDal.Employees.ToList();
            //List<Employee> employees = new List<Employee>();
            //Employee emp = new Employee();
            //emp.FistName = "johnson";
            //emp.LastName = "fernandes";
            //emp.Salary = 14000;
            //employees.Add(emp);

            //emp = new Employee();
            //emp.FistName = "michael";
            //emp.LastName = "jackson";
            //emp.Salary = 16000;
            //employees.Add(emp);

            //emp = new Employee();
            //emp.FistName = "robert";
            //emp.LastName = "pattinson";
            //emp.Salary = 20000;
            //employees.Add(emp);

            //return employees;
        }
        public Employee SaveEmployee(Employee e)
        {
            StudayDal salesDal = new StudayDal();
            salesDal.Employees.Add(e);
            salesDal.SaveChanges();
            return e;
        }
    }
}