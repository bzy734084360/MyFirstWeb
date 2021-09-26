using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using MyBlog.App_Start.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyBlog.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ClaimsBaseAuthorize(ClaimType = "testType", ClaimValue = "testClaim")]
    public class CookieProtectController : ControllerBase
    {
        // GET: CookieProtect
        public ActionResult Index()
        {
            //1.从Cookie中获取加密后的用户信息字符串
            var cookieStr = this.HttpContext.Request.Cookies[".AspNet.ApplicationCookie"].Value.ToString();
            //2.将用户信息字符串以Base64Url的方式转换为二进制数据
            var cookieBytes = TextEncodings.Base64Url.Decode(cookieStr);
            //3.转换后的二进制数据通过MachineKey进行解密(注：MachinKey默认使用User_MacineKey_Protect为主目的，
            //特殊目的由Owin Cookie验证中间件提供)
            var result = MachineKey.Unprotect(cookieBytes,
                new string[] { "Microsoft.Owin.Security.Cookies.CookieAuthenticationMiddleware",
                "ApplicationCookie",
                "v1"});
            TicketSerializer ticketSerializer = new TicketSerializer();
            //4.将解密后的二进制数据反序列化为AuthenticationTicket实例
            var ticket = ticketSerializer.Deserialize(result);

            return View();
        }
    }
}