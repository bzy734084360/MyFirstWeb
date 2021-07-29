using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewStudy.Controllers
{
    /// <summary>
    /// Vue Study
    /// </summary>
    public class VueController : BaseController
    {
        /// <summary>
        /// Vue介绍
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Vue实例
        /// </summary>
        /// <returns></returns>
        public ActionResult VueExamples()
        {
            return View();
        }
    }
}