
using ImageServiceWebApp.Models;
using Newtonsoft.Json.Linq;
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

        // GET: Remove
        public ActionResult RemoveHandler(string remove)
        {
            configModel.HandlerRemove = remove;
            return View(configModel);
        }

        [HttpPost]
        public ActionResult Cancel()
        {
            return RedirectToAction("Config");
        }

        [HttpPost]
        public ActionResult OK()
        {
            string handler = configModel.HandlerRemove;
            configModel.RemoveAction();
            return RedirectToAction("Config");
        }

    }
}