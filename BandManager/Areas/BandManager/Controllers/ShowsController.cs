using BandManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandManager.Areas.BandManager.Controllers
{
    [Authorize]
    public class ShowsController : BaseProfileController
    {
        private void UploadPhoto(Show show, HttpPostedFileBase photo)
        {
            if (photo != null && photo.ContentLength > 0)
            {
                try
                {
                    show.PhotoUrl = Path.GetFileName(photo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images/shows"), show.PhotoUrl);
                    photo.SaveAs(path);
                }
                catch (Exception ex)
                {
                    show.PhotoUrl = string.Empty;
                    ModelState.AddModelError("photo", string.Format("Error Uploading Image: {0}", ex.Message));
                }
            }
        }

        public ActionResult Index()
        {
            ViewBag.WebsiteUrl = profile.WebsiteUrl;
            var model = profile.Shows.OrderByDescending(s => s.Date);
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new Show());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Show show, HttpPostedFileBase photo)
        {
            try
            {
                UploadPhoto(show, photo);
                profile.Shows.Add(show);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var show = profile.Shows.First(s => s.ID == id);
            return View(show);
        }

        [HttpPost]
        public ActionResult Edit(Show show, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                UploadPhoto(show, photo);
                profile.Shows.First(s => s.ID == show.ID).update(show);
                return RedirectToAction("Index");
            }

            return View(show);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // needs a confirmation before deleting!
                var show = profile.Shows.First(s=> s.ID == id);
                profile.Shows.Remove(show);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
