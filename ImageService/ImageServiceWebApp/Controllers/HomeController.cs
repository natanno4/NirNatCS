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

        // GET: ImageWeb
        public ActionResult ImageWeb()
        {
            ImageWebModel imgwModel = new ImageWebModel(); 
            ViewBag.IsConnected = true; 
            return View(imgwModel);
        }
    }
}