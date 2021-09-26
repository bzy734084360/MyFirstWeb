using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    /// <summary>
    /// 特性路由测试demo
    /// </summary>
    public class DirectRoutingController : ControllerBase
    {
        [Route("dr/test")]
        public ActionResult Index()
        {
            var context = this.ControllerContext;
            return View();
        }
    }
}