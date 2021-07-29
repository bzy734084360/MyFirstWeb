using MyWeb.BusinessLayer;
using MyWeb.Model;
using MyWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Employee emp = new Employee();
            //emp.FistName = "fish";
            //emp.LastName = "yu";
            //emp.Salary = 2000;
            //ViewData["Employee"] = emp;
            //ViewBag.EmployeeBag = emp;
            return View();
        }

        /// <summary>
        /// 测试禁止访问控制器方法特性
        /// </summary>
        [NonAction]
        public void TestNonAction()
        {

        }
        /// <summary>
        /// 一个控制器方法返回不同页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MoreView()
        {
            if (string.IsNullOrEmpty(Request["ID"]))
            {
                return View("MyView");
            }
            else
            {
                return View("YourView");
            }
        }
        /// <summary>
        /// ModelView练习
        /// </summary>
        /// <returns></returns>
        public ActionResult ModelView()
        {
            //Employee emp = new Employee();
            //emp.FistName = "fish";
            //emp.LastName = "yu";
            //emp.Salary = 2000;

            //EmployeeViewModel vmEmp = new EmployeeViewModel();
            //vmEmp.EmployeeName = emp.FistName + "" + emp.LastName;
            //vmEmp.Salary = emp.Salary.ToString("C");
            //if (emp.Salary > 15000)
            //{
            //    vmEmp.SalaryColor = "yellow";
            //}
            //else
            //{
            //    vmEmp.SalaryColor = "green";
            //}
            ////vmEmp.UserName = "Admin";
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            EmployeeBusinessLayer empbal = new EmployeeBusinessLayer();
            List<Employee> employees = empbal.GetEmployees();
            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();

            foreach (Employee item in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = item.FirstName + " " + item.LastName;
                empViewModel.Salary = item.Salary.ToString();
                if (item.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";
                }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                employeeViewModels.Add(empViewModel);
            }
            employeeListViewModel.Employees = employeeViewModels;
            employeeListViewModel.UserName = "Admin";

            return View(employeeListViewModel);
        }
    }
}