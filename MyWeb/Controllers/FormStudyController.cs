using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace MyWeb.Controllers
{
    public class FormStudyController : Controller
    {
        // GET: FormStudy
        public ActionResult Index()
        {
            return View();
        }
    }
}