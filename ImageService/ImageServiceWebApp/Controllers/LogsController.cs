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
        static LogsModel model = new LogsModel();
        // GET: Logs
        public ActionResult Logs()
        {
            return View(model);
        }
        /*
        [HttpPost]
        public JArray GetList(string type)
        {
            JArray list = new JArray();
            if(string.IsNullOrEmpty(type) || (type != "INFO" && type != "WARNING" && type != "FAIL"))
            {
                foreach(Log log in model.Logs)
                {
                    JObject data = new JObject();
                    data["Message"] = log.Message;
                    data["Type"] = log.Type;
                    list.Add(data);
                }
            } else
            {
                foreach (Log log in model.Logs)
                {
                    if (log.Type == type)
                    {
                        JObject data = new JObject();
                        data["Message"] = log.Message;
                        data["Type"] = log.Type;
                        list.Add(data);
                    }
                }
            }
            return list;
        }*/

        [HttpPost]
        public ActionResult Logs(LogsModel LModel)
        {
            string type = LModel.TypeChose;
            if(string.IsNullOrEmpty(type) || (!type.Equals("INFO") && !type.Equals("FAIL") && !type.Equals("WARNING")))
            {
                foreach (Log log in model.Logs)
                {
                    model.LogsFilter.Add(log);
                }
            } else
            {
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