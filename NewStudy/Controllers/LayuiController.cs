using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewStudy.Controllers
{
    public class LayuiController : Controller
    {
        // GET: Layui
        public ActionResult Home()
        {
            return View();
        }
        /// <summary>
        /// Layui栅格
        /// </summary>
        /// <returns></returns>
        public ActionResult LayuiGrid()
        {
            return View();
        }

        public ActionResult LayuiDatatTable()
        {
            var dateList = new List<string>();
            for (int i = 1; i <= 50; i++)
            {
                var date = DateTime.Now.AddDays(i);
                var str = date.ToString("yyyy.MM.dd");
                dateList.Add(str);
            }
            ViewBag.Time = JsonConvert.SerializeObject(dateList);
            return View();
        }
    }
}