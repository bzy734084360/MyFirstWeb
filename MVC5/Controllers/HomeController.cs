using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Controllers
{
    //[RoutePrefix("home")]
    //[Route("{action=Index}/{id?}")]//多个默认可选值
    public class HomeController : Controller
    {
        //[Route("~/")]
        //[Route("")]
        //[Route("index")]
        public ActionResult Index(string userName)
        {
            ViewBag.UserName = userName;
            return View();
        }
        //[Route("about")]
        public ActionResult About()
        {
            ViewBag.Message = "为所欲为";

            return View();
        }

        //[Route("contact/{id:int}")]//内联约束
        public ActionResult Contact(int id)
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.InputValue = id;
            return View();
        }
        //[Route("contact/{name}")]
        public ActionResult Contact(string name)
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.InputValue = name;
            return View();
        }
    }
}