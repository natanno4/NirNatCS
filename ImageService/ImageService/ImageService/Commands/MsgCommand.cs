using ImageService.Infrastructure.Enums;
using Newtonsoft.Json.Linq;

namespace ImageService.Commands

{
    public class MsgCommand
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
        public string ToJSON()
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
