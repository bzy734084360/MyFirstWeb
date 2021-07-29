using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewStudy.Controllers
{
    public class MVCController : BaseController
    {
        // GET: MVC
        public ActionResult Index()
        {
            return View();
        }
    }
}