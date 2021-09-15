using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Areas.Admin.Controllers
{
    public class BlogClientController : ControllerBase
    {
        // GET: Admin/BlogClient
        public ActionResult Index()
        {
            return View();
        }
    }
}