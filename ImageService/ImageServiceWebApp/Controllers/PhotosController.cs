using ImageServiceWebApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApp.Controllers
{
    public class PhotosController : Controller
    {
        static PhotosModel model = new PhotosModel();
        // GET: Photos
        public ActionResult Photos()
        {
            return View(model);
        }

        // GET: PhotoViewer
        public ActionResult PhotoViewer(string showThumbPath)
        {
            model.DeleteFromView = true;
            return View();
        }

        // GET: DeletePhoto
        public ActionResult DeletePhoto(string showThumbPath)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Back()
        {
            model.DeleteFromView = false;
            return RedirectToAction("Photos");
        }

        [HttpGet]
        public JObject Cancel()
        {
            JObject data = new JObject();
            if (model.DeleteFromView)
            {
                data["backTo"] = "PhotoViewer";
            }
            else
            {
                data["backTo"] = "Photos";
            }
            return data;


        }

        [HttpPost]
        public ActionResult OkDelete()
        {
            model.DeleteFromView = false;
            return RedirectToAction("Photos");
        }



    }
}