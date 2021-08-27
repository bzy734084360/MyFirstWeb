using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MyWeb.Controllers
{
    //待接入登录授权处理
    //[Authorize]
    public class OAuth2Controller : Controller
    {
        // GET: OAuth2
        public ActionResult Authorize()
        {
            if (Request.HttpMethod == "POST")
            {
                //POST请求返回授权信息
                var identity = this.User.Identity as ClaimsIdentity;
                identity = new ClaimsIdentity(identity.Claims, "Bearer", identity.NameClaimType, identity.RoleClaimType);
                var authentication = HttpContext.GetOwinContext().Authentication;
                authentication.SignIn(identity);
            }
            return View();
        }
    }
}