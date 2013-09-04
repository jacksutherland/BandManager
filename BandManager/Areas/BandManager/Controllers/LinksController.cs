using BandManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandManager.Areas.BandManager.Controllers
{
    public class LinksController : BaseProfileController
    {
        public ActionResult Create()
        {
            return View(new Link());
        }

        [HttpPost]
        public ActionResult Create(Link link)
        {
            try
            {
                profile.Links.Add(link);
                return RedirectToAction("Edit", "Profile");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var link = profile.Links.First(l => l.ID == id);
            return View(link);
        }

        [HttpPost]
        public ActionResult Edit(Link newLink)
        {
            if (ModelState.IsValid)
            {
                Link link = profile.Links.First(l => l.ID == newLink.ID);
                link.Name = newLink.Name;
                link.Url = newLink.Url;

                return RedirectToAction("Edit", "Profile");
            }

            return View(newLink);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var link = profile.Links.First(l => l.ID == id);
                profile.Links.Remove(link);
                return RedirectToAction("Edit", "Profile");
            }
            catch
            {
                return View();
            }
        }

    }
}
