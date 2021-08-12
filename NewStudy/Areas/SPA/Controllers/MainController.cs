using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewStudy.BusinessLayer;
using NewStudy.Controllers;
using NewStudy.Model;
using NewStudy.ViewModels.SPA;
using OldViewModel = NewStudy.ViewModels;
namespace NewStudy.Areas.SPA.Controllers
{
    public class MainController : BaseController
    {
        // GET: SPA/Main
        public ActionResult Index()
        {
            MainViewModel v = new MainViewModel();
            v.UserName = this.CurrentUser.UserName;
            v.FooterData = new OldViewModel.FooterViewModel();
            v.FooterData.CompanyName = "XL";
            v.FooterData.Year = DateTime.Now.Year.ToString();
            return View("Index", v);
        }

        public ActionResult EmployeeList()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            List<Employee> employees = empBal.GetEmployees();
            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();
            foreach (var emp in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
                empViewModel.Salary = emp.Salary.Value.ToString("C");
                if (emp.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                empViewModels.Add(empViewModel);
            }
            employeeListViewModel.Employees = empViewModels;
            return View("EmployeeList", employeeListViewModel);
        }
    }
}