using MyWeb.DataAccessLayer;
using MyWeb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeb.BusinessLayer
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
    }
}