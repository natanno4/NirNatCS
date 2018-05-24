using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure.Enums;

namespace ImageService.Commands
{
    class GetConfigCommand
    {
        public string Execute(string[] args, out bool result)
        {
            string[] strConfig = new string[5];
            strConfig[0] = ConfigurationManager.AppSettings.Get("OutputDir");
            strConfig[1] = ConfigurationManager.AppSettings.Get("SourceName");
            strConfig[2] = ConfigurationManager.AppSettings.Get("LogName");
            strConfig[3] = ConfigurationManager.AppSettings.Get("ThumbnailSize");

            MsgCommand cmnd = new MsgCommand(CommandEnum.GetConfigCommand, strConfig);
            string jsonFormat = JsonConvert.SerializeObject(cmnd);
            result = true;
            return jsonFormat;
        }
    }
}
