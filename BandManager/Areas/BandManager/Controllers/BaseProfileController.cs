using BandManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandManager.Areas.BandManager.Controllers
{
    public class BaseProfileController : Controller
    {
        protected Profile profile;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            profile = Session["currentProfile"] as Profile;
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod == "POST" || filterContext.ActionDescriptor.ActionName == "Delete")
            {
                profile.ToXml().Save(Server.MapPath("~/App_Data/Profile.xml"));
                Session["currentProfile"] = profile;
            }
            base.OnActionExecuted(filterContext);
        }
    }
}
