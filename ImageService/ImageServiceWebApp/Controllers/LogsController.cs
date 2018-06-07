using ImageServiceWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApp.Controllers
{
    public class LogsController : Controller
    {
        static LogsModel model = new LogsModel();
        // GET: Logs
        public ActionResult Logs()
        {
            return View(model);
        }
    }
}