using BandManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandManager.Controllers
{
    public class WebsiteController : Controller
    {
        Profile profile;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            profile = Session["currentProfile"] as Profile;
            base.OnActionExecuting(filterContext);
        }

        public ActionResult Index()
        {            
            return View(profile);
        }

        public ActionResult Shows()
        {
            ShowViewData model = new ShowViewData();
            model.UpcomingShows = new List<Show>(profile.Shows.Where(s => s.Date >= DateTime.Now).OrderBy(s => s.Date));
            model.PastShows = new List<Show>(profile.Shows.Where(s => s.Date < DateTime.Now).OrderByDescending(s => s.Date));
            return View(model);
        }

        public ActionResult ShowDetails(int id, string slug)
        {
            Show model = profile.Shows.First(s => s.ID == id);
            return View(model);
        }

        public ActionResult Media()
        {
            ViewBag.ReverbNationID = profile.ReverbNationID;
            var model = profile.Videos.OrderBy(v => v.Position);
            return View(model);
        }

        [HttpPost]
        public ActionResult Media(FormCollection collection)        
        {
            return RedirectToAction("Downloads");            
        }

        public ActionResult Band()
        {
            ViewBag.BandBio = profile.Bio;
            var model = profile.Members.OrderBy(m => m.Position);
            return View(model);
        }

        public ActionResult Contact()
        {
            ContactViewData model = new ContactViewData();
            model.profile = profile;
            model.Links = new List<Link>(profile.Links.OrderBy(l => l.Position));
            return View(model);
        }

        public ActionResult Downloads()
        {
            return View();
        }
    }

    public class ShowViewData
    {
        public List<Show> UpcomingShows { get; set; }
        public List<Show> PastShows { get; set; }
    }

    public class ContactViewData
    {
        public Profile profile { get; set; }
        public ICollection<Link> Links { get; set; }
    }
}
