using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWeb.Controllers
{
    public class DemoController : Controller
    {
        /// <summary>
        /// 活动(策略模式)
        /// </summary>
        /// <returns></returns>
        public ActionResult Activity()
        {
            return View();
        }
    }
}