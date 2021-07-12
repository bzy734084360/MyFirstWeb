using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string userName)
        {
            ViewBag.UserName = userName;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "为所欲为";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}