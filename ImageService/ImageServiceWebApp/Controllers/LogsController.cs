using ImageService.Logging.Modal;
using ImageServiceWebApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ImageServiceWebApp.Controllers
{
    public class LogsController : Controller
    {
        private static LogsModel model = new LogsModel();
        // GET: Logs
        public ActionResult Logs()
        {
            model.LogsFilter.Clear();
            return View(model);
        }

        [HttpPost]
        public ActionResult Logs(LogsModel LModel)
        {
            string type = LModel.TypeChose;
            //check for entered type
            if(string.IsNullOrEmpty(type) || (!type.Equals("INFO") && !type.Equals("FAIL") && !type.Equals("WARNING")))
            {
                //no enterd or not valid
                foreach (Log log in model.Logs)
                {
                    model.LogsFilter.Add(log);
                }
            } else
            {
                //filter the logs by the given type
                model.LogsFilter.Clear();
                foreach(Log log in model.Logs)
                {
                    if(log.Type.Equals(type))
                    {
                        model.LogsFilter.Add(log);
                    }
                }
            }
            return View(model);

        }
    }
}