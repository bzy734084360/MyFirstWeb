using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWeb.Controllers
{
    /// <summary>
    /// Vue Study
    /// </summary>
    public class VueController : Controller
    {
        // GET: Vue
        public ActionResult Index()
        {
            return View();
        }
    }
}