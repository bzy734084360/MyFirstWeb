using NewStudy.App_Start;
using NewStudy.BusinessLayer;
using NewStudy.Model;
using NewStudy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewStudy.Controllers
{
    public class EmployeeController : BaseController
    {
        [HeaderFooterFilter]
        public ActionResult Index()
        {
            ViewBag.UserName = this.CurrentUser.UserName;
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

            return View(employeeListViewModel);
        }
        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel createEmployeeViewModel = new CreateEmployeeViewModel();
            return View("CreateEmployee", createEmployeeViewModel);
        }
        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult SaveEmployee(Employee e, string BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "Save Employee":
                    if (ModelState.IsValid)
                    {
                        EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
                        bal.SaveEmployee(e);
                        return RedirectToAction("Index", "Employee");
                    }
                    else
                    {
                        CreateEmployeeViewModel vm = new CreateEmployeeViewModel();
                        vm.FirstName = e.FirstName;
                        vm.LastName = e.LastName;
                        if (e.Salary.HasValue)
                        {
                            vm.Salary = e.Salary.ToString();
                        }
                        else
                        {
                            vm.Salary = ModelState["Salary"].Value.AttemptedValue;
                        }
                        return View("CreateEmployee", vm);
                    }
                case "Cancel":
                    return RedirectToAction("Index", "Employee");
            }
            return new EmptyResult();
        }

        public ActionResult GetAddNewLink()
        {
            if (CurrentUser.UserRole == "Admin")
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }

    }
}