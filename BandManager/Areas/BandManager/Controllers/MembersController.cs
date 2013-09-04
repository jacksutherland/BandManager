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
    public class MembersController : BaseProfileController
    {
        private void UploadPhoto(Member member, HttpPostedFileBase photo)
        {
            if (photo != null && photo.ContentLength > 0)
            {
                try
                {
                    member.PhotoUrl = Path.GetFileName(photo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images/photos"), member.PhotoUrl);
                    photo.SaveAs(path);
                }
                catch (Exception ex)
                {
                    member.PhotoUrl = string.Empty;
                    ModelState.AddModelError("photo", string.Format("Error Uploading Image: {0}", ex.Message));
                }
            }
        }

        public ActionResult Index()
        {
            ViewBag.WebsiteUrl = profile.WebsiteUrl;
            var model = profile.Members.OrderBy(m => m.Position);
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new Member());
        }

        [HttpPost]
        public ActionResult Create(Member member, HttpPostedFileBase photo)
        {
            try
            {
                UploadPhoto(member, photo);
                member.ID = Guid.NewGuid();
                member.Position = profile.Members.Count;
                profile.Members.Add(member);                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(string id)
        {
            var member = profile.Members.First(m => m.ID == new Guid(id));
            return View(member);
        }

        [HttpPost]
        public ActionResult Edit(Member member, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                UploadPhoto(member, photo);
                profile.Members.First(m => m.ID == member.ID).update(member);            
                return RedirectToAction("Index");
            }

            return View(member);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                var member = profile.Members.First(m => m.ID == new Guid(id));
                profile.Members.Remove(member);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public void UpdatePosition(string serialized)
        {
            string[] ids = serialized.Split(',');
            int position = 0;
            
            foreach (string id in ids)
            {
                Guid gid;
                if (Guid.TryParse(id, out gid))
                {
                    var member = profile.Members.First(m => m.ID == gid);
                    member.Position = position++;
                }
            }
        }

    }
}
