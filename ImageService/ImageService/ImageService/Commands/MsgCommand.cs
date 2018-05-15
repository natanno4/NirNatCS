using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using Newtonsoft.Json.Linq.JObject;
using ImageService.Infrastructure.Enums;
namespace ImageService.Commands
{
    class MsgCommand
    {
        public CommandEnum commandID { get; set; }
        public ICommand command { get; set; }

        public static MsgCommand FromJSON(string str)
        {
            //צריך לשנות
            MsgCommand commandMess = new MsgCommand();
            JObject obj = JObject.Parse(str);
            commandMess.commandID = (int)obj["commandID"];
            commandMess.command = (ICommand)obj["command"];
            return commandMess;
        }
        public string ToJSON(ICommand cmd)
        {
            //צריך לשנות
            MsgCommand cmnd = new MsgCommand();
            JObject obj = new JObject();
            obj["commandID"] = cmnd.commandID;
            obj["command"] = cmnd.command;
            return obj.ToString();
        }
    } 
}
