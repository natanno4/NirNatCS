
using Newtonsoft.Json.Linq;

namespace Infrastructure

{
    public class MsgCommand
    {
        public int commandID { get; set; }
        public string[] args { get; set; }
       
        public MsgCommand(int id, string[] arg)
        {
            this.commandID = id;
            this.args = arg;
        }

       
        public static MsgCommand FromJSON(string str)
        {
            JObject jObject = JObject.Parse(str);
            int command = (int)jObject["commandId"];
            JArray arr = (JArray)jObject["args"];
            string[] commandArgs = arr.ToObject<string[]>();
            return new MsgCommand(command, commandArgs);
        }
        public string ToJSON()
        {
            JObject jObject = new JObject();
            jObject["commandId"] = commandID;
            jObject["args"] = new JArray(args);
            return jObject.ToString();
        }
    } 
}
