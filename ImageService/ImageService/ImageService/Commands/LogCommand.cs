using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    class LogCommand
    {
        private ILoggingService logService;

        public LogCommand(ILoggingService lg)
        {
            logService = lg;
        }
        public string Execute(string[] args, out bool result)
        {
            ObservableCollection<string> obs = logService.list;

            string jsonFormat = JsonConvert.SerializeObject(obs);
            string[] arg = { jsonFormat };
            MsgCommand msg = new MsgCommand(CommandEnum.LogCommand, arg);
            string newCommand = JsonConvert.SerializeObject(msg);
            result = true;
            return newCommand;
        }
    }
}
