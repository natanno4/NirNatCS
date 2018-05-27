
using Newtonsoft.Json.Linq;

namespace Infrastructure

{
    public class MsgCommand
    {
        public int commandID { get; set; }
        public string[] args { get; set; }

        /// <summary>
        /// constructor.
        /// id represent the id of command and array of strings.
        /// 
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="arg">array of strings</param>

        public MsgCommand(int id, string[] arg)
        {
            this.commandID = id;
            this.args = arg;
        }

        /// <summary>
        /// FromJSON.
        /// convert from JSON to MsgCommand type.
        /// </summary>
        /// <param name="str">JSON string</param>
        /// <returns></returns>

        public static MsgCommand FromJSON(string str)
        {
            JObject jObject = JObject.Parse(str);
            int command = (int)jObject["commandId"];
            JArray arr = (JArray)jObject["args"];
            string[] commandArgs = arr.ToObject<string[]>();
            return new MsgCommand(command, commandArgs);
        }
        /// <summary>
        /// FromJSON.
        /// convert from JSON to MsgCommand type.
        /// </summary>
        /// <returns>JSON string</returns>
        public string ToJSON()
        {
            JObject jObject = new JObject();
            jObject["commandId"] = commandID;
            jObject["args"] = new JArray(args);
            return jObject.ToString();
        }
    } 
}
