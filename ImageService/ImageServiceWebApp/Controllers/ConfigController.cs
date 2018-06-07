using ImageServiceWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApp.Controllers
{
    public class ConfigController : Controller
    {
        static ConfigInfoModel configModel = ConfigInfoModel.SingeltonConfig;
        // GET: Config
        public ActionResult Config()
        {
            return View(configModel);
        }
    }
}