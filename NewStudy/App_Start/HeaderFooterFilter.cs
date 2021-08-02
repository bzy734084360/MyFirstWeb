using NewStudy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewStudy.App_Start
{
    public class HeaderFooterFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewResult v = filterContext.Result as ViewResult;
            if (v != null)
            {
                BaseViewModel bvm = v.Model as BaseViewModel;
                if (bvm != null)
                {
                    var user = (HttpContext.Current.User as Principal).UserData as UserData;
                    bvm.FooterData = new FooterViewModel();
                    bvm.FooterData.CompanyName = "GrumpyFish";
                    bvm.FooterData.Year = "2021-3021";
                    bvm.UserName = user.UserName;
                }
            }
        }
    }
}