using NewStudy.FormStudy;
using NewStudy.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace NewStudy.Controllers
{
    public class FormStudyController : BaseController
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
                GetOnline(u.UserName);
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

        /// <summary>
        /// 登录时记录登录的用户ID+SessionID，可利用Application、Cache、数据库等。
        /// </summary>
        /// <param name="userName">用户名</param>
        private void GetOnline(string userName)
        {
            Hashtable SingleOnline = (Hashtable)System.Web.HttpContext.Current.Application["MyWeb"];
            if (SingleOnline == null)
            {
                SingleOnline = new Hashtable();
            }

            Session["mySession"] = "MyWeb_UserOnLine";
            //SessionID
            if (SingleOnline.ContainsKey(userName))
            {
                SingleOnline[userName] = Session.SessionID;
            }
            else
            {
                SingleOnline.Add(userName, Session.SessionID);
            }

            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["MyWeb_Online"] = SingleOnline;
            System.Web.HttpContext.Current.Application.UnLock();
        }
    }
}