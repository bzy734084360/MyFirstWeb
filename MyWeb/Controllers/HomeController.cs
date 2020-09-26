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
            //Employee
            return View();
        }

        /// <summary>
        /// 测试禁止放问控制器方法特性
        /// </summary>
        [NonAction]
        public void TestNonAction()
        {

        }

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
    }
}