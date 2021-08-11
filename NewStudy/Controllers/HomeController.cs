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
    public class HomeController : BaseController
    {
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
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
    }
}