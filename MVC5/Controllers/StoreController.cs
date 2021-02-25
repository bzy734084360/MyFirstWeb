using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public string Index()
        {
            return "Store.Index()";
        }
        //genre 响应URL传参
        public string Browse(string genre)
        {
            //防止JS或html 标记注入
            string message = HttpUtility.HtmlEncode($"Store.Browse(),Genre={genre}");
            return message;
        }

        public string Details(int id)
        {
            string message = $"Store.Details(),ID={id}";
            return message;
        }

        public ActionResult XSS()
        {
            return PartialView();
            //return View();
        }

    }
}