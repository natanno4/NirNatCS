using ImageServiceWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApp.Controllers
{
    public class HomeController : Controller
    {
        private static ImageWebModel imgwModel = new ImageWebModel();
        // GET: ImageWeb
        public ActionResult ImageWeb()
        {
            imgwModel.NumberOfPhotos = imgwModel.UpdateNumberOFphotos();
            
            return View(imgwModel);
        }
    }
}