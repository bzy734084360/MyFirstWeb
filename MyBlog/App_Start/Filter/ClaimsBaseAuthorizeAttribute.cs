using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.App_Start.Filter
{
    public class ClaimsBaseAuthorizeAttribute : AuthorizeAttribute
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User as ClaimsPrincipal;
            //匹配指定声明类型以及声明值
            if (user != null && user.HasClaim(ClaimType, ClaimValue))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }

        }
    }
}