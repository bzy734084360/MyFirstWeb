using NewStudy.App_Start;
using System.Web;
using System.Web.Mvc;

namespace NewStudy
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new EmployeeExceptionFilter());
            filters.Add(new RequestAuthorizeAttribute());
        }
    }
}
