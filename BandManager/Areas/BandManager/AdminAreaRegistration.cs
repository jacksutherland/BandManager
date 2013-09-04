using System.Web.Mvc;

namespace BandManager.Areas.BandManager
{
    public class BandManagerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BandManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BandManager_default",
                "BandManager/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
