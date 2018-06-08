
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
        public ActionResult RemoveHandler()
        {
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
            //configModel.RemoveAction();
            string[] args = new string[2];
            args[0] = handler;
            Infrastructure.MsgCommand cmd = new Infrastructure.MsgCommand((int)ImageService.Infrastructure.Enums.CommandEnum.RemoveHandlerCommand, args);        
            return RedirectToAction("Config");
        }

    }
}