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
            model.ListPhotos.Clear();
            model.InitiliazeList(model.OutputDir);
            return View(model);
        }

        // GET: PhotoViewer
        public ActionResult PhotoViewer(string photo)
        {
            model.DeleteFromView = true;
            foreach(Photo p in model.ListPhotos)
            {
                if(p.PhotoThumbPath.Equals(photo))
                {
                    model.viewPhoto = p;
                    break;
                }
            }
            
            return View(model);
        }

        // GET: DeletePhoto
        public ActionResult DeletePhoto(string photo)
        {
            Photo temp = null;
            if(model.viewPhoto == null)
            {
                foreach (Photo p in model.ListPhotos)
                {
                    if (p.PhotoThumbPath.Equals(photo))
                    {
                        temp = p;
                        break;
                    }
                }
                model.photoToDelete = temp;
            }
            return View(model);
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
                model.RemovePhoto(model.viewPhoto);

            } else
            {
                if(model.viewPhoto == null)
                {
                    model.RemovePhoto(model.photoToDelete);
                }
            }
            model.DeleteFromView = false;
            model.viewPhoto = null;
            model.photoToDelete = null;
            return RedirectToAction("Photos");
        }



    }
}