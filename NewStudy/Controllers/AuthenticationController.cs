﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewStudy.Controllers
{
    public class AuthenticationController : BaseController
    {
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }
    }
}