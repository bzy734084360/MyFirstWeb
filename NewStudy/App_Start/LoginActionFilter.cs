using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewStudy.App_Start
{
    public class LoginActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Hashtable singleOnline = (Hashtable)filterContext.HttpContext.Application["MyWeb_Online"];
            // 判断当前SessionID是否存在
            if (singleOnline != null && singleOnline.ContainsKey(filterContext.HttpContext.User.Identity.Name))
            {
                if (!singleOnline[filterContext.HttpContext.User.Identity.Name].Equals(filterContext.HttpContext.Session.SessionID))
                {
                    filterContext.Result = new ContentResult() { Content = "<script>if(confirm('你的账号已在别处登陆，是否返回登陆页面重新登陆？')){top.window.location.href='/FormStudy/Login';}else{window.close();}</script>" };
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}