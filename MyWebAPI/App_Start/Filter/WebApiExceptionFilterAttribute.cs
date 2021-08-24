using Bzy.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MyWebAPI.App_Start.Filter
{
    /// <summary>
    /// 公共类异常处理
    /// </summary>
    public class WebApiExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// 重构异常
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is ApiCustomException)
            {
                /*
                 * 抛出自定义异常
                 */
                var exception = (ApiCustomException)actionExecutedContext.Exception;
                var result = new
                {
                    code = exception.ErrorCode,
                    message = actionExecutedContext.Exception.Message,
                    expand = exception.ExtMsg
                };

                actionExecutedContext.Response = new HttpResponseMessage
                {
                    Content = new StringContent(result.ToApiJson(), Encoding.GetEncoding("UTF-8"), "application/json")
                };
            }
            else
            {
                //日志记录(暂定Nlog 未开发)
                //WriteLog(actionExecutedContext);
                var oResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message, Encoding.UTF8, "text/plain"),
                    ReasonPhrase = "An internal exception has occurred in the call."
                };
                actionExecutedContext.Response = oResponse;
            }

            base.OnException(actionExecutedContext);
        }
        /// <summary>
        /// 错误日志写入
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        private void WriteLog(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext == null)
                return;

            try
            {
                var userName = GetUserName(actionExecutedContext.ActionContext.ControllerContext.Controller);
                //var logger = LogFactory.GetLogger(actionExecutedContext.ActionContext.ControllerContext.Controller.ToString());

                //Exception error = actionExecutedContext.Exception;
                //LogMessage logMessage = new LogMessage
                //{
                //    OperationTime = DateTime.Now,
                //    Url = actionExecutedContext.Request?.RequestUri?.ToString(),
                //    Class = actionExecutedContext.ActionContext.ControllerContext.Controller.ToString(),
                //    Ip = BigStarFK.Utilities.NetHelper.Ip,
                //    Host = BigStarFK.Utilities.NetHelper.Host,
                //    Browser = BigStarFK.Utilities.NetHelper.Browser,
                //    UserName = userName,
                //    ExceptionInfo = error.InnerException?.Message ?? error.Message
                //};

                //logMessage.ExceptionSource = error.Source;
                //logMessage.ExceptionRemark = error.StackTrace;
                //string strMessage = new LogFormat().ExceptionFormat(logMessage);

                //logger.Error(strMessage);
            }
            catch
            {
                //Ignore
            }
        }

        /// <summary>
        /// 获取用户姓名
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        private string GetUserName(IHttpController controller)
        {
            string userName = string.Empty;
            try
            {
                var claimsIdentity = ((ApiController)controller).User.Identity as System.Security.Claims.ClaimsIdentity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.UserData);
                UserData user = claim.Value.JsonToEntity<UserData>();
                userName = user.UserName;
            }
            catch
            {
                //Ignore
            }
            return userName;
        }
    }
}