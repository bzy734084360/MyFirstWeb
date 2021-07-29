using NewStudy.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewStudy.Controllers
{
    [LoginActionFilter]
    public class BaseController : Controller
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        /// <returns></returns>
        public virtual UserData CurrentUser
        {
            get
            {
                return (HttpContext.User as Principal).UserData as UserData;
            }
        }
    }
}