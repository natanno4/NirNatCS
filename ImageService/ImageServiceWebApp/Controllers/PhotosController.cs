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
        public ActionResult PhotoViewer(Photo showThumbPath)
        {
            model.DeleteFromView = true;
            model.viewPhoto = showThumbPath;
            return View();
        }

        // GET: DeletePhoto
        public ActionResult DeletePhoto(Photo showThumbPath)
        {
            if(model.viewPhoto == null)
            {
                model.photoToDelete = showThumbPath;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Back()
        {
            model.DeleteFromView = false;
            model.viewPhoto = null;
            return RedirectToAction("Photos");
        }

        [HttpGet]
        public JObject Cancel()
        {
            if(model.photoToDelete != null)
            {
                model.photoToDelete = null;
            }
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
            if(model.photoToDelete == null)
            {
                

            } else
            {
                if(model.viewPhoto == null)
                {

                }
            }
            model.DeleteFromView = false;
            model.viewPhoto = null;
            model.photoToDelete = null;
            return RedirectToAction("Photos");
        }



    }
}