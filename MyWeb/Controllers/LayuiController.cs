﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWeb.Controllers
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
    }
}