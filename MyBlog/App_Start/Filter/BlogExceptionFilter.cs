using Bzy.Logger;
using Bzy.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.App_Start.Filter
{
    public class BlogExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //记录错误日志
            WriteLog(filterContext);
        }

        protected virtual void WriteLog(ExceptionContext actionExecutedContext)
        {
            if (actionExecutedContext == null)
                return;

            try
            {
                Exception error = actionExecutedContext.Exception;
                LogMessage logMessage = new LogMessage
                {
                    OperationTime = DateTime.Now,
                    Url = actionExecutedContext.HttpContext.Request?.Url?.ToString(),
                    Class = actionExecutedContext.Controller.ToString(),
                    Ip = NetHelper.Ip,
                    Host = NetHelper.Host,
                    Browser = NetHelper.Browser,
                    UserName = actionExecutedContext.HttpContext.User.Identity.Name,
                    ExceptionInfo = error.InnerException?.Message ?? error.Message
                };

                logMessage.ExceptionSource = error.Source;
                logMessage.ExceptionRemark = error.StackTrace;
                string strMessage = new LogFormat().ExceptionFormat(logMessage);
                LogFactory.GetLogger(this.GetType()).Error(strMessage);
            }
            catch
            {
                //Ignore
            }
        }
    }
}