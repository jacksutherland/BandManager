using BandManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandManager.Areas.BandManager.Controllers
{
    [Authorize]
    public class ProfileController : BaseProfileController
    {
        public ActionResult Edit()
        {
            return View(profile);
        }

        [HttpPost]
        public ActionResult Edit(Profile newProfile)
        {
            if (ModelState.IsValid)
            {
                profile.Name = newProfile.Name;
                profile.WebsiteUrl = newProfile.WebsiteUrl;
                profile.Description = newProfile.Description;
                profile.Bio = newProfile.Bio;
                profile.ReverbNationID = newProfile.ReverbNationID;
                profile.ToXml().Save(Server.MapPath("~/App_Data/Profile.xml"));
                Session["currentProfile"] = profile;

                return RedirectToAction("Index", "Home");
            }

            return View(profile);
        }

        [HttpPost]
        public void UpdatePosition(string serialized)
        {
            string[] ids = serialized.Split(',');
            int position = 0;

            foreach (string id in ids)
            {
                int iid;
                if (int.TryParse(id, out iid))
                {
                    var link = profile.Links.First(l => l.ID == iid);
                    link.Position = position++;
                }
            }
        }
    }
}
