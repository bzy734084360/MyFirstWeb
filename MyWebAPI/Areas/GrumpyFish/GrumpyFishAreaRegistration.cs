using System.Web.Mvc;

namespace MyWebAPI.Areas.GrumpyFish
{
    public class GrumpyFishAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GrumpyFish";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GrumpyFish_default",
                "GrumpyFish/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}