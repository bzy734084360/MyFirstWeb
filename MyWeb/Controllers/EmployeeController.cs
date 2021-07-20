using MyWeb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWeb.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult AddNew()
        {
            return View("CreateEmployee");
        }
        public ActionResult SaveEmployee(Employee e, string BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "Save Employee":
                    return Content($"{e.FirstName}|{e.LastName}|{e.Salary}");
                case "Cancel":
                    return RedirectToAction("Index", "ModelView");
            }
            return new EmptyResult();
        }

    }
}