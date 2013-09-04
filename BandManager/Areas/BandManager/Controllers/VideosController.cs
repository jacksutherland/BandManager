using BandManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandManager.Areas.BandManager.Controllers
{
    public class VideosController : BaseProfileController
    {
        public ActionResult Index()
        {
            var model = profile.Videos.OrderBy(v => v.Position);
            return View(model);
        }

        public ActionResult Create()
        {
            var video = new Video();
            return View(video);
        }

        [HttpPost]
        public ActionResult Create(Video video)
        {
            try
            {
                video.Position = profile.Videos.Count;
                profile.Videos.Add(video);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var video = profile.Videos.First(v => v.ID == id);
            return View(video);
        }

        [HttpPost]
        public ActionResult Edit(Video newVideo)
        {
            if (ModelState.IsValid)
            {
                Video video = profile.Videos.First(v => v.ID == newVideo.ID);
                video.Name = newVideo.Name;
                video.VideoHtml = newVideo.VideoHtml;

                return RedirectToAction("Index");
            }

            return View(newVideo);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var video = profile.Videos.First(v => v.ID == id);
                profile.Videos.Remove(video);
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
                int iid;
                if (int.TryParse(id, out iid))
                {
                    var video = profile.Videos.First(v => v.ID == iid);
                    video.Position = position++;
                }
            }
        }

    }
}
