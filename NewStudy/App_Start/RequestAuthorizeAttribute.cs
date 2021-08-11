using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NewStudy.App_Start
{
    /// <summary>
    /// 授权认证处理
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequestAuthorizeAttribute : AuthorizeAttribute
    {
        //验证
        public override void OnAuthorization(AuthorizationContext context)
        {
            //是否允许匿名访问
            if (context.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false)
                || context.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false))
            {
                return;
            }
            //登录验证
            Principal principal = context.HttpContext.User as Principal;
            if (principal == null)
            {
                SetUnAuthorizedResult(context);
                HandleUnauthorizedRequest(context);
                return;
            }
            //权限认证
            //if (!principal.IsInRole(base.Roles) || !principal.IsInUser(base.Users))
            //{
            //    SetUnAuthorizedResult(context);
            //    HandleUnauthorizedRequest(context);
            //    return;
            //}
            ////验证配置文件
            //if (!ValidateAuthorizeConfig(principal, context))
            //{
            //    SetUnAuthorizedResult(context);
            //    HandleUnauthorizedRequest(context);
            //    return;
            //}
        }

        /// <summary>
        /// 验证不通过时
        /// </summary>
        /// <param name="context"></param>
        public void SetUnAuthorizedResult(AuthorizationContext context)
        {
            HttpRequestBase request = context.HttpContext.Request;
            if (request.IsAjaxRequest())
            {
                //处理ajax请求
                string result = JsonConvert.SerializeObject(new { error = 403 });
                context.Result = new ContentResult() { Content = result };
            }
            else
            {
                //跳转到登录页面
                string loginUrl = FormsAuthentication.LoginUrl + "?ReturnUrl=" + request.Url.AbsoluteUri;
                context.Result = new RedirectResult(loginUrl);
            }
        }
        //override
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.Result != null)
            {
                return;
            }
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}