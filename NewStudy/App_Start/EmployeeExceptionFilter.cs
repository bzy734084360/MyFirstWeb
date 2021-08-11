using NewStudy.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewStudy.App_Start
{
    public class EmployeeExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            FileLogger logger = new FileLogger();
            logger.LogException(filterContext.Exception);
            //base.OnException(filterContext);
            //实现自定义返回结果
            filterContext.ExceptionHandled = true;//赋值异常是否处理
            filterContext.Result = new ContentResult()
            {
                Content = "出现了异常"
            };
        }
    }
}