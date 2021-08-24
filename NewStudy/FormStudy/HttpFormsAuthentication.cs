using Bzy.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace NewStudy.FormStudy
{
    public class HttpFormsAuthentication
    {
        /// <summary>
        /// 设置用户缓存
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userdata"></param>
        /// <param name="rememberDays"></param>
        public static void SetAuthenticationCookie(string userName, IUserData userdata, double rememberDays = 0)
        {
            //保存在cookie中的信息
            string userJson = JsonConvert.SerializeObject(userdata);

            //创建用户票据
            double tickekDays = rememberDays == 0 ? 7 : rememberDays;
            var ticket = new FormsAuthenticationTicket(2, userName, DateTime.Now, DateTime.Now.AddDays(tickekDays), false, userJson);

            //FomrAuthentication 提供web forms身份验证
            //加密
            string encryptValue = FormsAuthentication.Encrypt(ticket);
            //创建cookie
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptValue);
            cookie.HttpOnly = true;
            cookie.Domain = FormsAuthentication.CookieDomain;

            if (rememberDays > 0)
            {
                cookie.Expires = DateTime.Now.AddDays(rememberDays);
            }
            HttpContext.Current.Response.Cookies.Remove(cookie.Name);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// 从缓存中解析出用户数据
        /// </summary>
        /// <typeparam name="TUserData"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Principal TryParsePrincipal<TUserData>(HttpContext context) where TUserData : IUserData
        {
            HttpRequest request = context.Request;
            HttpCookie cookie = request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                return null;
            }
            //解密cookie值 获取用户票据
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (ticket == null || string.IsNullOrEmpty(ticket.UserData))
            {
                return null;
            }
            IUserData userData = JsonConvert.DeserializeObject<TUserData>(ticket.UserData);
            return new Principal(ticket, userData);
        }
    }
}