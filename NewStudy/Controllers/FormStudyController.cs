using NewStudy.FormStudy;
using NewStudy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace NewStudy.Controllers
{
    public class FormStudyController : Controller
    {
        /// <summary>
        /// 登录首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View(new UserDetails());
        }
        /// <summary>
        /// 登录认证
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DoLogin(UserDetails u)
        {
            //姓名正确跳转
            if (u.UserName == "张三" && u.Password == "1234")
            {
                UserData userData = new UserData()
                {
                    UserName = u.UserName,
                    UserID = 1,
                    UserRole = "Admin"
                };
                HttpFormsAuthentication.SetAuthenticationCookie(u.UserName, userData, 7);
                return RedirectToAction("Index", "Home");
            }
            //重新登录
            return View("Login");
        }
        // GET: FormStudy
        public ActionResult Index()
        {
            return View();
        }
    }
}