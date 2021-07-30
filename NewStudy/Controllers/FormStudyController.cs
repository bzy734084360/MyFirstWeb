using MyUtilities.Utilities;
using NewStudy.FormStudy;
using NewStudy.Model;
using NewStudy.WebUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
            string returnUrl = string.Empty;
            if (Request["ReturnUrl"] != null)
            {
                returnUrl = Request["ReturnUrl"];
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        /// <summary>
        /// 登录认证
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DoLogin(string Account, string Password, string verifyCode)
        {
            string sessionVerify = "session_verifycode" + NetHelper.GetIPAddress();
            //判断验证码
            verifyCode = SecretHelper.AESEncrypt(verifyCode.ToLower());
            if (Session[sessionVerify] == null || string.IsNullOrWhiteSpace(Session[sessionVerify].ToString()) || verifyCode != Session[sessionVerify].ToString())
            {
                return Content("5");
            }
            //姓名正确跳转
            if (Account == "admin" && Password == "1234")
            {
                UserData userData = new UserData()
                {
                    UserName = Account,
                    UserID = 1,
                    UserRole = "Admin"
                };
                HttpFormsAuthentication.SetAuthenticationCookie(Account, userData, 7);
                GetOnline(Account);
                return Content("3");
            }
            //重新登录
            return Content("-1");
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
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VerifyCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }
        /// <summary>
        /// 安全退出
        /// </summary>
        /// <returns></returns>
        public ActionResult OutLogin()
        {
            //清除用户缓存
            CookieHelper.DelCookie(FormsAuthentication.FormsCookieName);
            //清除session
            Session.Abandon();  //取消当前会话
            Session.Clear();    //清除当前浏览器所以Session
            return Content("1");
        }
    }
}