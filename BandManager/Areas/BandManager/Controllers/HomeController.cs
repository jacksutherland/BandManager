using BandManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandManager.Areas.BandManager.Controllers
{    
    public class HomeController : Controller
    {
        BandManagerDB _db = new BandManagerDB();
                
        public ActionResult Index()
        {
            var profile = Session["currentProfile"] as Profile;

            return View(profile);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
