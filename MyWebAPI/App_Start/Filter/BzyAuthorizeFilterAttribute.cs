using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace MyWebAPI.App_Start.Filter
{
    /// <summary>
    /// 授权过滤
    /// </summary>
    public class BzyAuthorizeFilterAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                || actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
                return;
            //脱裤子放屁~~
            //if (actionContext.Request.Headers.Authorization != null)
            //{
            //    try
            //    {

            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }
            //}
            base.OnAuthorization(actionContext);
        }
    }
}